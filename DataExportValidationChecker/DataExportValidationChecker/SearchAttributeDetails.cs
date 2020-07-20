using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataExportValidationChecker
{
    public class SearchAttributeDetails
    {
        public bool Include { get; set; }

        [DisplayName("Logical Name")]
        public string LogicalName { get; set; }

        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
        [DisplayName("Attribute Type")]
        public AttributeType AttrType { get; set; }

        [Browsable(false)]
        public int? MaxLength { get; set; }

        [DisplayName("Failed Validation (n=)")]
        public int FailedCount => InvalidIds.Count;

        [Browsable(false)]
        public int EmptyCount { get; set; }

        [Browsable(false)]
        public int PopulatedCount { get; set; }

        [Browsable(false)]
        public List<Guid> InvalidIds { get; set; }

        [Browsable(false)]
        public List<ResultDetails> Results { get; set; }


        [Browsable(false)]
        public double? DoubleMinValue { get; set; }
        [Browsable(false)]
        public double? DoubleMaxValue { get; set; }
        [Browsable(false)]
        public decimal? DecimalMinValue { get; set; }
        [Browsable(false)]
        public decimal? DecimalMaxValue { get; set; }
        [Browsable(false)]
        public int? IntMinValue { get; set; }
        [Browsable(false)]
        public int? IntMaxValue { get; set; }
        [Browsable(false)]
        public long? BigIntMinValue { get; set; }
        [Browsable(false)]
        public long? BigIntMaxValue { get; set; }
        [Browsable(false)]
        public int[] AllowableValues { get; set; }

        public SearchAttributeDetails()
        {
            Reset();
        }

        public void Reset()
        {
            InvalidIds = new List<Guid>();
            Results = new List<ResultDetails>();
            EmptyCount = 0;
            PopulatedCount = 0;
        }

        public enum AttributeType
        {
            String,
            BigInt,
            Int,
            Decimal,
            Double,
            Lookup,
            Picklist
        }
    }
}
