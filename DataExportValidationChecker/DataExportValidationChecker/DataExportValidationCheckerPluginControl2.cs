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

namespace DataExportValidationChecker
{
    public partial class DataExportValidationCheckerPluginControl2 : PluginControlBase, IGitHubPlugin
    {
        private Settings mySettings;
        private List<SearchAttributeDetails> _searchingAttributes;

        public DataExportValidationCheckerPluginControl2()
        {
            InitializeComponent();
            _searchingAttributes = new List<SearchAttributeDetails>();
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

            var searchDetails = new List<SearchAttributeDetails>();
            searchDetails.AddRange(stringAttrs.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.String,
                MaxLength = t.MaxLength,
                Format = t.FormatName.Value
            }));

            searchDetails.AddRange(memoAttrs.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.String,
                MaxLength = t.MaxLength
            }));

            searchDetails.AddRange(doubleAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.Double,
                DoubleMinValue = t.MinValue,
                DoubleMaxValue = t.MaxValue
            }));

            searchDetails.AddRange(decimalAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.Decimal,
                DecimalMinValue = t.MinValue,
                DecimalMaxValue = t.MaxValue
            }));

            searchDetails.AddRange(intAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.Int,
                IntMinValue = t.MinValue,
                IntMaxValue = t.MaxValue
            }));

            searchDetails.AddRange(bigIntAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.BigInt,
                BigIntMinValue = t.MinValue,
                BigIntMaxValue = t.MaxValue
            }));

            searchDetails.AddRange(picklistAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.Picklist,
                AllowableValues = t.OptionSet.Options.Where(o => o.Value.HasValue).Select(o => o.Value.Value).ToArray()
            }));

            searchDetails.AddRange(stateAttr.Select(t => new SearchAttributeDetails()
            {
                LogicalName = t.LogicalName,
                DisplayName = t.DisplayName.UserLocalizedLabel?.Label ?? t.LogicalName,
                AttrType = SearchAttributeDetails.AttributeType.State,
                AllowableValues = t.OptionSet.Options.Where(o => o.Value.HasValue).Select(o => o.Value.Value).ToArray()
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

                searchDetails.Add(detail);
            }

            BindDataToTable(searchDetails.OrderBy(a => a.LogicalName).ToList());

            for (int i = 1; i < metadataView.ColumnCount; i++)
                metadataView.Columns[i].ReadOnly = true;

            metadataView.Columns[0].Width = 50;
            metadataView.Columns[5].Width = 275;
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
                if ((bool)row.Cells["Include"].Value)
                {
                    _searchingAttributes.Add((SearchAttributeDetails)row.DataBoundItem);

                    if ((string)row.Cells["LogicalName"].Value == "statuscode")
                    {
                        foreach (DataGridViewRow updateRow in metadataView.Rows)
                        {
                            if ((string)updateRow.Cells["LogicalName"].Value == "statecode")
                                _searchingAttributes.Add((SearchAttributeDetails)updateRow.DataBoundItem);
                        }
                    }
                }
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
            // Clear any previous results
            foreach (var field in _searchingAttributes)
                field.Reset();

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Calculating results...",
                Work = (worker, args) =>
                {
                    var entities = new EntityCollection();
                    var qry = new QueryExpression(entitySelection.SelectedEntity.LogicalName)
                    {
                        ColumnSet = new ColumnSet(_searchingAttributes.Select(t => t.LogicalName).Union(new[] { entitySelection.SelectedEntity.PrimaryNameAttribute }).ToArray()),
                        PageInfo = new PagingInfo()
                        {
                            Count = 5000,
                            PageNumber = 1
                        }
                    };

                    // Only load records that have one of the values we're interested in
                    qry.Criteria.FilterOperator = LogicalOperator.Or;

                    foreach (var field in _searchingAttributes)
                        qry.Criteria.AddCondition(field.LogicalName, ConditionOperator.NotNull);

                    var totalCount = 0;
                    while (true)
                    {
                        var results = Service.RetrieveMultiple(qry);
                        totalCount += results.Entities.Count;
                        foreach (var field in _searchingAttributes)
                        {
                            if (field.AttrType == SearchAttributeDetails.AttributeType.String)
                            {
                                field.EmptyCount += results.Entities.Count(t => string.IsNullOrEmpty(t.GetAttributeValue<string>(field.LogicalName)));
                                field.PopulatedCount += results.Entities.Count(t => !string.IsNullOrEmpty(t.GetAttributeValue<string>(field.LogicalName)));

                                var invalidRecords = results.Entities.Where(t => (t.GetAttributeValue<string>(field.LogicalName) ?? "").Length > field.MaxLength).ToList();
                                field.InvalidIds.AddRange(invalidRecords.Select(t => t.Id).ToList());
                                field.Results = invalidRecords.Select(r => new ResultDetails()
                                {
                                    Id = r.Id,
                                    Name = r.GetAttributeValue<string>(entitySelection.SelectedEntity.PrimaryNameAttribute),
                                    Failure = $"Invalid data: {r[field.LogicalName]}"
                                }).ToList();
                            }

                            if (field.AttrType == SearchAttributeDetails.AttributeType.Int)
                            {
                                if(!field.IntMinValue.HasValue)
                                    field.IntMinValue = Int32.MinValue;

                                if (!field.IntMaxValue.HasValue)
                                    field.IntMaxValue = Int32.MaxValue;

                                var invalidRecords = results.Entities.Where(t =>
                                    t.GetAttributeValue<int?>(field.LogicalName) != null &&
                                    t.GetAttributeValue<int?>(field.LogicalName).HasValue &&
                                    (t.GetAttributeValue<int?>(field.LogicalName).Value < field.IntMinValue || t.GetAttributeValue<int?>(field.LogicalName).Value > field.IntMaxValue)).ToList();

                                field.InvalidIds.AddRange(invalidRecords.Select(t => t.Id));
                                field.Results = invalidRecords.Select(r => new ResultDetails()
                                {
                                    Id = r.Id,
                                    Name = r.GetAttributeValue<string>(entitySelection.SelectedEntity.PrimaryNameAttribute),
                                    Failure = $"Invalid data: {r[field.LogicalName]}"
                                }).ToList();
                            }

                            if (field.AttrType == SearchAttributeDetails.AttributeType.BigInt)
                            {
                                if (!field.BigIntMinValue.HasValue)
                                    field.BigIntMinValue = long.MinValue;

                                if (!field.BigIntMaxValue.HasValue)
                                    field.BigIntMaxValue = long.MaxValue;

                                var invalidRecords = results.Entities.Where(t =>
                                    t.GetAttributeValue<long?>(field.LogicalName) != null &&
                                    t.GetAttributeValue<long?>(field.LogicalName).HasValue &&
                                    (t.GetAttributeValue<long?>(field.LogicalName).Value <= field.BigIntMinValue || t.GetAttributeValue<long?>(field.LogicalName).Value >= field.BigIntMaxValue)).ToList();

                                field.InvalidIds.AddRange(invalidRecords.Select(t => t.Id));
                                field.Results = invalidRecords.Select(r => new ResultDetails()
                                {
                                    Id = r.Id,
                                    Name = r.GetAttributeValue<string>(entitySelection.SelectedEntity.PrimaryNameAttribute),
                                    Failure = $"Invalid data: {r[field.LogicalName]}"
                                }).ToList();
                            }

                            if (field.AttrType == SearchAttributeDetails.AttributeType.Double)
                            {
                                if (!field.DoubleMinValue.HasValue)
                                    field.DoubleMinValue = double.MinValue;

                                if (!field.DoubleMaxValue.HasValue)
                                    field.DoubleMaxValue = double.MaxValue;

                                var invalidRecords = results.Entities.Where(t =>
                                    t.GetAttributeValue<double?>(field.LogicalName) != null &&
                                    t.GetAttributeValue<double?>(field.LogicalName).HasValue &&
                                    (t.GetAttributeValue<double?>(field.LogicalName).Value <= field.DoubleMinValue || t.GetAttributeValue<double?>(field.LogicalName).Value >= field.DoubleMaxValue)).ToList();

                                field.InvalidIds.AddRange(invalidRecords.Select(t => t.Id));
                                field.Results = invalidRecords.Select(r => new ResultDetails()
                                {
                                    Id = r.Id,
                                    Name = r.GetAttributeValue<string>(entitySelection.SelectedEntity.PrimaryNameAttribute),
                                    Failure = $"Invalid data: {r[field.LogicalName]}"
                                }).ToList();
                            }

                            if (field.AttrType == SearchAttributeDetails.AttributeType.Decimal)
                            {
                                if (!field.DecimalMinValue.HasValue)
                                    field.DecimalMinValue = decimal.MinValue;

                                if (!field.DecimalMaxValue.HasValue)
                                    field.DecimalMaxValue = decimal.MaxValue;

                                var invalidRecords = results.Entities.Where(t =>
                                    t.GetAttributeValue<decimal?>(field.LogicalName) != null &&
                                    t.GetAttributeValue<decimal?>(field.LogicalName).HasValue &&
                                    (t.GetAttributeValue<decimal?>(field.LogicalName).Value <= field.DecimalMinValue || t.GetAttributeValue<decimal?>(field.LogicalName).Value >= field.DecimalMaxValue)).ToList();
                                field.InvalidIds.AddRange(invalidRecords.Select(t => t.Id));
                                field.Results = invalidRecords.Select(r => new ResultDetails()
                                {
                                    Id = r.Id,
                                    Name = r.GetAttributeValue<string>(entitySelection.SelectedEntity.PrimaryNameAttribute),
                                    Failure = $"Invalid data: {r[field.LogicalName]}"
                                }).ToList();
                            }

                            if (field.AttrType == SearchAttributeDetails.AttributeType.Picklist || field.AttrType == SearchAttributeDetails.AttributeType.State)
                            {
                                var invalidRecords = results.Entities.Where(t =>
                                    t.GetAttributeValue<OptionSetValue>(field.LogicalName) != null &&
                                    !field.AllowableValues.Contains(t.GetAttributeValue<OptionSetValue>(field.LogicalName).Value)
                                ).ToList();

                                field.InvalidIds.AddRange(invalidRecords.Select(t => t.Id));
                                field.Results = invalidRecords.Select(r => new ResultDetails()
                                {
                                    Id = r.Id,
                                    Name = r.GetAttributeValue<string>(entitySelection.SelectedEntity.PrimaryNameAttribute),
                                    Failure = $"Invalid data: {r.GetAttributeValue<OptionSetValue>(field.LogicalName).Value}"
                                }).ToList();
                            }

                            if (field.AttrType == SearchAttributeDetails.AttributeType.Status)
                            {
                                foreach (var record in results.Entities.Where(t => t.GetAttributeValue<OptionSetValue>(field.LogicalName) != null))
                                {
                                    var state = record.GetAttributeValue<OptionSetValue>("statecode");
                                    var status = record.GetAttributeValue<OptionSetValue>("statuscode");

                                    var matchingConfig = field.StatusLookups.SingleOrDefault(s => s.StatusCode == status.Value);
                                    if (matchingConfig == null)
                                    {
                                        field.InvalidIds.Add(record.Id);
                                        field.Results.Add(new ResultDetails()
                                        {
                                            Id = record.Id,
                                            Name = record.GetAttributeValue<string>(entitySelection.SelectedEntity.PrimaryNameAttribute),
                                            Failure = $"Invalid status code: {record.GetAttributeValue<OptionSetValue>(field.LogicalName).Value}"
                                        });
                                    } else if (matchingConfig.StateCode != state.Value)
                                    {
                                        field.InvalidIds.Add(record.Id);
                                        field.Results.Add(new ResultDetails()
                                        {
                                            Id = record.Id,
                                            Name = record.GetAttributeValue<string>(entitySelection.SelectedEntity.PrimaryNameAttribute),
                                            Failure = $"State code does not correlate with status code"
                                        });
                                    }

                                }
                            }
                        }

                        worker.ReportProgress(-1, $"Analysed {totalCount:N0} records");

                        if (!results.MoreRecords)
                        {
                            _searchingAttributes = _searchingAttributes.OrderByDescending(a => a.FailedCount).ToList();
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

                    var overColumns = _searchingAttributes.Where(f => f.FailedCount > 0).ToArray();
                    resultsView.Visible = overColumns.Any();
                    noIssuesLabel.Visible = !resultsView.Visible;

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
                foreach (var id in matchingData.InvalidIds)
                {
                    var entity = Service.Retrieve(entitySelection.SelectedEntity.LogicalName, id, new ColumnSet(matchingData.LogicalName, entitySelection.SelectedEntity.PrimaryNameAttribute));
                    var result = new ResultDetails()
                    {
                        Id = id,
                        Name = entity.GetAttributeValue<string>(entitySelection.SelectedEntity.PrimaryNameAttribute),
                        Failure = $"Invalid data: {entity[matchingData.LogicalName]}"
                    };

                    if (entity[matchingData.LogicalName] is OptionSetValue)
                        result.Failure = $"Invalid data: {entity.GetAttributeValue<OptionSetValue>(matchingData.LogicalName).Value}";

                    matchingData.Results.Add(result);
                }
            }

            resultsView.ColumnHeadersVisible = false;
            resultsView.DataSource = new BindingList<ResultDetails>(matchingData.Results);
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

        private void metadataView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) 
                return;

            if(metadataView.Columns[e.ColumnIndex].HeaderText != "Tests")
                return;

            var row = metadataView.Rows[e.RowIndex].DataBoundItem as SearchAttributeDetails;
            var testSelectionForm = new TestSelectionForm(row);
            testSelectionForm.ShowDialog();
        }
    }
}