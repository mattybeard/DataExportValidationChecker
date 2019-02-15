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

        [DisplayName("Max Length")]
        public int? MaxLength { get; set; }

        [DisplayName("Failed Validation (n=)")]
        public int OverCount => OverIds.Count;

        [Browsable(false)]
        public int EmptyCount { get; set; }

        [Browsable(false)]
        public int PopulatedCount { get; set; }

        [Browsable(false)]
        public List<Guid> OverIds { get; set; }

        [Browsable(false)]
        public List<ResultDetails> Results { get; set; }

        public SearchAttributeDetails()
        {
            OverIds = new List<Guid>();
            Results = new List<ResultDetails>();
        }
    }
}
