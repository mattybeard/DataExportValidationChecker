using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace DataExportValidationChecker
{
    public partial class DataExportValidationCheckerPluginControl : PluginControlBase
    {
        private Settings mySettings;
        private List<SearchAttributeDetails> _searchingAttributes;

        public DataExportValidationCheckerPluginControl()
        {
            InitializeComponent();
            _searchingAttributes = new List<SearchAttributeDetails>();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            //ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbSample_Click(object sender, EventArgs e)
        {
            // The ExecuteMethod method handles connecting to an
            // organization if XrmToolBox is not yet connected
            ExecuteMethod(GetAccounts);
        }

        private void GetAccounts()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting accounts",
                Work = (worker, args) =>
                {
                    args.Result = Service.RetrieveMultiple(new QueryExpression("account")
                    {
                        TopCount = 50
                    });
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;
                    if (result != null)
                    {
                        MessageBox.Show($"Found {result.Entities.Count} accounts");
                    }
                }
            });
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private void entitySelection_Load(object sender, EventArgs e)
        {
            entitySelection.Service = Service;
        }

        private void entitySelection_SelectedItemChanged(object sender, EventArgs e)
        {
            var retrieveEntityReq = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Attributes,
                LogicalName = entitySelection.SelectedEntity.LogicalName
            };

            var entityMetadata = (RetrieveEntityResponse)Service.Execute(retrieveEntityReq);
            var stringAttrs = entityMetadata.EntityMetadata.Attributes.Where(a =>
                a.AttributeType == AttributeTypeCode.String)
                .Select(t => (StringAttributeMetadata)t).ToList();

            BindDataToTable(stringAttrs
                .Where(a => string.IsNullOrEmpty(a.AttributeOf))
                .Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                MaxLength = t.MaxLength
            }).OrderBy(a => a.LogicalName).ToList());

            for (int i = 1; i < metadataView.ColumnCount; i++)
                metadataView.Columns[i].ReadOnly = true;

            metadataView.Columns[0].Width = 50;
            previewGroup.Visible = true;
        }

        private void BindDataToTable(List<SearchAttributeDetails> attributes)
        {
            var bindedAttrs = new BindingList<SearchAttributeDetails>(attributes);
            metadataView.DataSource = bindedAttrs;
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            _searchingAttributes = new List<SearchAttributeDetails>();
            foreach (DataGridViewRow row in metadataView.Rows)
            {
                if((bool)row.Cells["Include"].Value)
                    _searchingAttributes.Add((SearchAttributeDetails)row.DataBoundItem);
            }

            ExecuteMethod(CalculateValidationLevel);
        }

        private void checkAllButton_Click(object sender, EventArgs e)
        {
            _searchingAttributes = new List<SearchAttributeDetails>();
            foreach (DataGridViewRow row in metadataView.Rows)
                _searchingAttributes.Add((SearchAttributeDetails)row.DataBoundItem);

            ExecuteMethod(CalculateValidationLevel);
        }

        private void CalculateValidationLevel()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Calculating results...",
                Work = (worker, args) =>
                {
                    var entities = new EntityCollection();
                    var qry = new QueryExpression(entitySelection.SelectedEntity.LogicalName)
                    {
                        ColumnSet = new ColumnSet(_searchingAttributes.Select(t => t.LogicalName).ToArray()),
                        PageInfo = new PagingInfo()
                        {
                            Count = 5000,
                            PageNumber = 1,
                            ReturnTotalRecordCount = true
                        }
                    };

                    var totalCount = 0;
                    while (true)
                    {
                        var results = Service.RetrieveMultiple(qry);
                        totalCount += results.Entities.Count;
                        foreach (var field in _searchingAttributes)
                        {
                            field.EmptyCount += results.Entities.Count(t => string.IsNullOrEmpty(t.GetAttributeValue<string>(field.LogicalName)));
                            field.PopulatedCount += results.Entities.Count(t => !string.IsNullOrEmpty(t.GetAttributeValue<string>(field.LogicalName)));                            
                            field.OverIds.AddRange(results.Entities.Where(t => (t.GetAttributeValue<string>(field.LogicalName) ?? "").Length > field.MaxLength).Select(t => t.Id).ToList());
                        }

                        worker.ReportProgress(-1, $"Analysed {totalCount:N1} records");

                        if (!results.MoreRecords)
                        {
                            _searchingAttributes = _searchingAttributes.OrderByDescending(a => a.OverCount).ToList();
                            break;
                        }

                        qry.PageInfo.PageNumber++;
                        qry.PageInfo.PagingCookie = results.PagingCookie;
                    }
                },
                ProgressChanged = e =>
                {
                    SetWorkingMessage(e.UserState.ToString());
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    var overColumns = _searchingAttributes.Where(f => f.OverCount > 0).ToArray();
                    if (overColumns.Any())
                        BindDataToTable(_searchingAttributes);
                }
            });
        }

        private void metadataView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            metadataView.Rows[e.RowIndex].Selected = true;

            var matchingData = _searchingAttributes.FirstOrDefault(t => t.LogicalName == (string) metadataView[1, e.RowIndex].Value);
            if (matchingData != null)
            {
                PopulateResults(matchingData);
            }
        }

        private void PopulateResults(SearchAttributeDetails matchingData)
        {
            if (!matchingData.Results.Any())
            {
                // We haven't already calculated the results, so let's do that now.
                foreach (var id in matchingData.OverIds)
                {
                    var entity = Service.Retrieve(entitySelection.SelectedEntity.LogicalName, id, new ColumnSet(matchingData.LogicalName, entitySelection.SelectedEntity.PrimaryNameAttribute));
                    matchingData.Results.Add(new ResultDetails()
                    {
                        Id = id,
                        Name = entity.GetAttributeValue<string>(entitySelection.SelectedEntity.PrimaryNameAttribute),
                        ProblemValue = entity.GetAttributeValue<string>(matchingData.LogicalName),
                    });
                }
            }

            resultsView.DataSource = new BindingList<ResultDetails>(matchingData.Results);

            for (var i = 0; i < resultsView.ColumnCount; i++)
                resultsView.Columns[i].Width = 150;
        }

        private void resultsView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            resultsView.Rows[e.RowIndex].Selected = true;
            var id = resultsView[0, e.RowIndex].Value;

            OpenRecord((Guid)id, entitySelection.SelectedEntity.LogicalName);
        }

        private void OpenRecord(Guid guid, string logicalName)
        {
            var url = ConnectionDetail.WebApplicationUrl;
            if (string.IsNullOrEmpty(url))
            {
                url = string.Concat(ConnectionDetail.ServerName, "/", ConnectionDetail.Organization);
                if (!url.ToLower().StartsWith("http"))
                {
                    url = string.Concat("http://", url);
                }
            }
            url = string.Concat(url,
                url.EndsWith("/") ? "" : "/",
                "main.aspx?etn=",
                logicalName,
                "&pagetype=entityrecord&id=",
                guid.ToString());

            if (!string.IsNullOrEmpty(url))
            {
                Process.Start(url);
            }
        }
    }
}