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
using XrmToolBox.Extensibility.Interfaces;
using DataExportValidationChecker.Popups;
using System.Reflection;

namespace DataExportValidationChecker
{
    public partial class DataExportValidationCheckerPluginControl : PluginControlBase, IGitHubPlugin
    {
        private AppInsights ai;
        private const string aiEndpoint = "https://dc.services.visualstudio.com/v2/track";
        private const string aiKey = "8f6d21d6-a6f2-4d31-82cc-c20efecbe729";

        private Settings mySettings;
        private List<SearchAttributeDetails> _searchingDetails;

        public DataExportValidationCheckerPluginControl()
        {
            InitializeComponent();
            
            ai = new AppInsights(aiEndpoint, aiKey, Assembly.GetExecutingAssembly());
            ai.WriteEvent("Loaded");
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
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
            if (entitySelection.SelectedEntity == null)
                return;

            var retrieveEntityReq = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Attributes,
                LogicalName = entitySelection.SelectedEntity.LogicalName
            };

            _searchingDetails = new List<SearchAttributeDetails>();

            var entityMetadata = (RetrieveEntityResponse)Service.Execute(retrieveEntityReq);
            var stringAttrs = entityMetadata.EntityMetadata.Attributes.Where(a =>
                a.AttributeType == AttributeTypeCode.String && string.IsNullOrEmpty(a.AttributeOf))
                .Select(t => (StringAttributeMetadata)t).ToList();

            var memoAttrs = entityMetadata.EntityMetadata.Attributes.Where(a =>
                a.AttributeType == AttributeTypeCode.Memo && string.IsNullOrEmpty(a.AttributeOf))
                .Select(t => (MemoAttributeMetadata)t).ToList();

            var doubleAttr = entityMetadata.EntityMetadata.Attributes.Where(a =>
                    a.AttributeType == AttributeTypeCode.Double && string.IsNullOrEmpty(a.AttributeOf))
                .Select(t => (DoubleAttributeMetadata)t).ToList();

            var intAttr = entityMetadata.EntityMetadata.Attributes.Where(a =>
                    a.AttributeType == AttributeTypeCode.Integer && string.IsNullOrEmpty(a.AttributeOf))
                .Select(t => (IntegerAttributeMetadata)t).ToList();

            var decimalAttr = entityMetadata.EntityMetadata.Attributes.Where(a =>
                    a.AttributeType == AttributeTypeCode.Decimal && string.IsNullOrEmpty(a.AttributeOf))
                .Select(t => (DecimalAttributeMetadata)t).ToList();

            var bigIntAttr = entityMetadata.EntityMetadata.Attributes.Where(a =>
                    a.AttributeType == AttributeTypeCode.BigInt && string.IsNullOrEmpty(a.AttributeOf))
                .Select(t => (BigIntAttributeMetadata)t).ToList();

            var picklistAttr = entityMetadata.EntityMetadata.Attributes.Where(a =>
                    a.AttributeType == AttributeTypeCode.Picklist && string.IsNullOrEmpty(a.AttributeOf))
                .Select(t => (PicklistAttributeMetadata) t).ToList();

            var stateAttr = entityMetadata.EntityMetadata.Attributes.Where(a =>
                    a.AttributeType == AttributeTypeCode.State && string.IsNullOrEmpty(a.AttributeOf))
                .Select(t => (StateAttributeMetadata)t).ToList();

            var statusAttr = entityMetadata.EntityMetadata.Attributes.Where(a =>
                    a.AttributeType == AttributeTypeCode.Status && string.IsNullOrEmpty(a.AttributeOf))
                .Select(t => (StatusAttributeMetadata)t).ToList();

            _searchingDetails.AddRange(stringAttrs.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.String,
                MaxLength = t.MaxLength,
                Format = t.FormatName.Value,
                RequiredLevel = t.RequiredLevel.Value
            }));

            _searchingDetails.AddRange(memoAttrs.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.String,
                MaxLength = t.MaxLength,
                RequiredLevel = t.RequiredLevel.Value
            }));

            _searchingDetails.AddRange(doubleAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.Double,
                DoubleMinValue = t.MinValue,
                DoubleMaxValue = t.MaxValue,
                RequiredLevel = t.RequiredLevel.Value
            }));

            _searchingDetails.AddRange(decimalAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.Decimal,
                DecimalMinValue = t.MinValue,
                DecimalMaxValue = t.MaxValue,
                RequiredLevel = t.RequiredLevel.Value
            }));

            _searchingDetails.AddRange(intAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.Int,
                IntMinValue = t.MinValue,
                IntMaxValue = t.MaxValue,
                RequiredLevel = t.RequiredLevel.Value
            }));

            _searchingDetails.AddRange(bigIntAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.BigInt,
                BigIntMinValue = t.MinValue,
                BigIntMaxValue = t.MaxValue,
                RequiredLevel = t.RequiredLevel.Value
            }));

            _searchingDetails.AddRange(picklistAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.Picklist,
                AllowableValues = t.OptionSet.Options.Where(o => o.Value.HasValue).Select(o => o.Value.Value).ToArray(),
                RequiredLevel = t.RequiredLevel.Value
            }));

            _searchingDetails.AddRange(stateAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.State,
                AllowableValues = t.OptionSet.Options.Where(o => o.Value.HasValue).Select(o => o.Value.Value).ToArray(),
                RequiredLevel = t.RequiredLevel.Value
            }));

            foreach (var status in statusAttr)
            {
                var detail = new SearchAttributeDetails()
                {
                    LogicalName = status.LogicalName,
                    DisplayName = status.DisplayName.UserLocalizedLabel?.Label ?? status.LogicalName,
                    AttrType = SearchAttributeDetails.AttributeType.Status,
                };

                foreach (StatusOptionMetadata option in status.OptionSet.Options)
                {
                    detail.StatusLookups.Add(new StatusCodeLookup()
                    {
                        StateCode = option.State.Value,
                        StatusCode = option.Value.Value
                    });
                }

                _searchingDetails.Add(detail);
            }

            _searchingDetails = _searchingDetails.OrderBy(a => a.LogicalName).ToList();

            var bindedAttrs = new BindingList<SearchAttributeDetails>(_searchingDetails);
            metadataView.DataSource = bindedAttrs;

            for (int i = 1; i < metadataView.ColumnCount; i++)
                metadataView.Columns[i].ReadOnly = true;

            metadataView.Columns[4].Width = 275;
            previewGroup.Visible = true;
        }

        private void BindDataToTable(List<SearchAttributeDetails> attributes)
        {
            var bindedAttrs = new BindingList<SearchAttributeDetails>(attributes);
            metadataView.DataSource = bindedAttrs;
        }

        private void checkAllButton_Click(object sender, EventArgs e)
        {
            ExecuteMethod(CalculateValidation);
        }

        private void CalculateValidation()
        {
            // Clear any previous results
            foreach (var attributeDetail in _searchingDetails)
                attributeDetail.Reset();

            var searchingAttributes = new List<SearchAttributeDetails>();
            foreach (DataGridViewRow row in metadataView.Rows)
            {
                var item = (SearchAttributeDetails)row.DataBoundItem;
                if (item.Tests != null && item.Tests.Any())
                    searchingAttributes.Add(item);
            }

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Calculating results...",
                Work = (worker, args) =>
                {
                    ai.WriteEvent($"Running tests against {entitySelection.SelectedEntity.LogicalName}");

                    var entities = new EntityCollection();
                    var qry = new QueryExpression(entitySelection.SelectedEntity.LogicalName)
                    {
                        ColumnSet = new ColumnSet(searchingAttributes.Select(t => t.LogicalName).Union(new[] { entitySelection.SelectedEntity.PrimaryNameAttribute }).ToArray()),
                        PageInfo = new PagingInfo()
                        {
                            Count = 5000,
                            PageNumber = 1
                        }
                    };

                    // Only load records that have one of the values we're interested in
                    qry.Criteria.FilterOperator = LogicalOperator.Or;

                    foreach (var field in searchingAttributes)
                        qry.Criteria.AddCondition(field.LogicalName, ConditionOperator.NotNull);

                    var totalCount = 0;
                    while (true)
                    {
                        var retriveResponse = Service.RetrieveMultiple(qry);
                        var results = retriveResponse.Entities.ToList();
                        totalCount += results.Count;

                        foreach (var field in searchingAttributes)
                        {
                            field.RunTests(results);   
                        }

                        worker.ReportProgress(-1, $"Analysed {totalCount:N0} records");

                        if (!retriveResponse.MoreRecords)
                        {
                            searchingAttributes = searchingAttributes.OrderByDescending(a => a.FailedCount).ToList();
                            break;
                        }

                        qry.PageInfo.PageNumber++;
                        qry.PageInfo.PagingCookie = retriveResponse.PagingCookie;
                    }

                    ai.WriteEvent($"Analysing records", totalCount);
                },
                ProgressChanged = e =>
                {
                    SetWorkingMessage(e.UserState.ToString());
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                        MessageBox.Show(args.Error.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    var erroredColumns = searchingAttributes.Where(f => f.FailedCount > 0).ToArray();
                    ai.WriteEvent($"Failed records", erroredColumns.Length);

                    if (erroredColumns.Any())
                        BindDataToTable(searchingAttributes);
                    else
                        MessageBox.Show("Congratulations, all tests passed!", @"Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            });
        }

        private void metadataView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            metadataView.Rows[e.RowIndex].Selected = true;

            var matchingData = _searchingDetails.FirstOrDefault(t => t.LogicalName == (string) metadataView[0, e.RowIndex].Value);
            if (matchingData != null)
            {
                PopulateResults(matchingData);
            }
        }

        private void PopulateResults(SearchAttributeDetails matchingData)
        {
            if (!matchingData.FailedRecords.Any())
            {
                // We haven't already calculated the results, so let's do that now.
                foreach (var failedRecord in matchingData.FailedRecords)
                {
                    var id = failedRecord.Id;
                    var entity = Service.Retrieve(entitySelection.SelectedEntity.LogicalName, id, new ColumnSet(matchingData.LogicalName, entitySelection.SelectedEntity.PrimaryNameAttribute));
                    var result = new FailedRecord()
                    {
                        Id = id,
                        FailureReason = "Invalid data",
                        FailedValue = entity.GetAttributeValue<string>(matchingData.LogicalName)
                    };

                    if (entity[matchingData.LogicalName] is OptionSetValue)
                        result.FailedValue = entity.GetAttributeValue<OptionSetValue>(matchingData.LogicalName).Value.ToString();

                    matchingData.FailedRecords.Add(result);
                }
            }

            resultsView.ColumnHeadersVisible = false;
            resultsView.DataSource = new BindingList<FailedRecord>(matchingData.FailedRecords);
            resultsView.ColumnHeadersVisible = true;

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

        public string RepositoryName => "DataExportValidationChecker";
        public string UserName => "mattybeard";

        private void metadataView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (metadataView.Columns[e.ColumnIndex].HeaderText != "Tests")
                return;

            var row = metadataView.Rows[e.RowIndex].DataBoundItem as SearchAttributeDetails;
            var testSelectionForm = new TestSelectionForm(_searchingDetails, row);
            testSelectionForm.ShowDialog();

            metadataView.Refresh();
        }
    }
}