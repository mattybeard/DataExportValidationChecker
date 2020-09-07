using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExportValidationChecker.Tests
{
    public class MatchesMetadataTest : BaseTest
    {
        public override string TestTitle => "Matches metadata";
        public override bool Execute(SearchAttributeDetails field, List<Entity> entities)
        {
            if (field.AttrType == SearchAttributeDetails.AttributeType.String)
            {
                field.PopulatedCount += entities.Count(t => !string.IsNullOrEmpty(t.GetAttributeValue<string>(field.LogicalName)));

                var invalidRecords = entities.Where(t => (t.GetAttributeValue<string>(field.LogicalName) ?? "").Length > field.MaxLength).ToList();
                field.FailedRecords.AddRange(invalidRecords.Select(r => new FailedRecord()
                {
                    Id = r.Id,
                    FailureReason = "Text too long",
                    FailedValue = r.GetAttributeValue<string>(field.LogicalName)
                }).ToList());
            }

            if (field.AttrType == SearchAttributeDetails.AttributeType.Int)
            {
                if (!field.IntMinValue.HasValue)
                    field.IntMinValue = Int32.MinValue;

                if (!field.IntMaxValue.HasValue)
                    field.IntMaxValue = Int32.MaxValue;

                var invalidRecords = entities.Where(t =>
                    t.GetAttributeValue<int?>(field.LogicalName) != null &&
                    t.GetAttributeValue<int?>(field.LogicalName).HasValue &&
                    (t.GetAttributeValue<int?>(field.LogicalName).Value < field.IntMinValue || t.GetAttributeValue<int?>(field.LogicalName).Value > field.IntMaxValue)).ToList();

                field.FailedRecords.AddRange(invalidRecords.Select(r => new FailedRecord()
                {
                    Id = r.Id,
                    FailureReason = $"Number out of bounds",
                    FailedValue = r.GetAttributeValue<string>(field.LogicalName)
                }).ToList());
            }

            if (field.AttrType == SearchAttributeDetails.AttributeType.BigInt)
            {
                if (!field.BigIntMinValue.HasValue)
                    field.BigIntMinValue = long.MinValue;

                if (!field.BigIntMaxValue.HasValue)
                    field.BigIntMaxValue = long.MaxValue;

                var invalidRecords = entities.Where(t =>
                    t.GetAttributeValue<long?>(field.LogicalName) != null &&
                    t.GetAttributeValue<long?>(field.LogicalName).HasValue &&
                    (t.GetAttributeValue<long?>(field.LogicalName).Value <= field.BigIntMinValue || t.GetAttributeValue<long?>(field.LogicalName).Value >= field.BigIntMaxValue)).ToList();

                field.FailedRecords.AddRange(invalidRecords.Select(r => new FailedRecord()
                {
                    Id = r.Id,
                    FailureReason = $"Number out of bounds",
                    FailedValue = r.GetAttributeValue<string>(field.LogicalName)
                }).ToList());
            }

            if (field.AttrType == SearchAttributeDetails.AttributeType.Double)
            {
                if (!field.DoubleMinValue.HasValue)
                    field.DoubleMinValue = double.MinValue;

                if (!field.DoubleMaxValue.HasValue)
                    field.DoubleMaxValue = double.MaxValue;

                var invalidRecords = entities.Where(t =>
                    t.GetAttributeValue<double?>(field.LogicalName) != null &&
                    t.GetAttributeValue<double?>(field.LogicalName).HasValue &&
                    (t.GetAttributeValue<double?>(field.LogicalName).Value <= field.DoubleMinValue || t.GetAttributeValue<double?>(field.LogicalName).Value >= field.DoubleMaxValue)).ToList();

                field.FailedRecords.AddRange(invalidRecords.Select(r => new FailedRecord()
                {
                    Id = r.Id,
                    FailureReason = $"Number out of bounds",
                    FailedValue = r.GetAttributeValue<string>(field.LogicalName)
                }).ToList());
            }

            if (field.AttrType == SearchAttributeDetails.AttributeType.Decimal)
            {
                if (!field.DecimalMinValue.HasValue)
                    field.DecimalMinValue = decimal.MinValue;

                if (!field.DecimalMaxValue.HasValue)
                    field.DecimalMaxValue = decimal.MaxValue;

                var invalidRecords = entities.Where(t =>
                    t.GetAttributeValue<decimal?>(field.LogicalName) != null &&
                    t.GetAttributeValue<decimal?>(field.LogicalName).HasValue &&
                    (t.GetAttributeValue<decimal?>(field.LogicalName).Value <= field.DecimalMinValue || t.GetAttributeValue<decimal?>(field.LogicalName).Value >= field.DecimalMaxValue)).ToList();
                field.FailedRecords.AddRange(invalidRecords.Select(r => new FailedRecord()
                {
                    Id = r.Id,
                    FailureReason = $"Number out of bounds",
                    FailedValue = r.GetAttributeValue<string>(field.LogicalName)
                }).ToList());
            }

            if (field.AttrType == SearchAttributeDetails.AttributeType.Picklist || field.AttrType == SearchAttributeDetails.AttributeType.State)
            {
                var invalidRecords = entities.Where(t =>
                    t.GetAttributeValue<OptionSetValue>(field.LogicalName) != null &&
                    !field.AllowableValues.Contains(t.GetAttributeValue<OptionSetValue>(field.LogicalName).Value)
                ).ToList();

                field.FailedRecords.AddRange(invalidRecords.Select(r => new FailedRecord()
                {
                    Id = r.Id,
                    FailureReason = $"Invalid option set value",
                    FailedValue = r.GetAttributeValue<OptionSetValue>(field.LogicalName).Value.ToString()
                }).ToList());
            }

            if (field.AttrType == SearchAttributeDetails.AttributeType.Status)
            {
                foreach (var record in entities.Where(t => t.GetAttributeValue<OptionSetValue>(field.LogicalName) != null))
                {
                    var state = record.GetAttributeValue<OptionSetValue>("statecode");
                    var status = record.GetAttributeValue<OptionSetValue>("statuscode");

                    var matchingConfig = field.StatusLookups.SingleOrDefault(s => s.StatusCode == status.Value);
                    if (matchingConfig == null)
                    {
                        field.FailedRecords.Add(new FailedRecord()
                        {
                            Id = record.Id,
                            FailureReason = "Invalid status code",
                            FailedValue = record.GetAttributeValue<OptionSetValue>(field.LogicalName).Value.ToString()
                        });
                    }
                    else if (matchingConfig.StateCode != state.Value)
                    {
                        field.FailedRecords.Add(new FailedRecord()
                        {
                            Id = record.Id,
                            FailureReason = "State code does not correlate with status code"
                        });
                    }
                }
            }

            return field.FailedRecords.Any();
        }
    }
}
