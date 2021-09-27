using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExportValidationChecker.Models
{
    public class TableMetadataComboItem
    {
        public EntityMetadata Metadata { get; set; }
        public string DisplayName
        {
            get
            {
                if (Metadata == null)
                    return String.Empty;

                if (Metadata.DisplayName.UserLocalizedLabel == null)
                    return String.Empty;

                return $"{Metadata.DisplayName.UserLocalizedLabel.Label} ({Metadata.LogicalName})";
            }
        }

        public string LogicalName
        {
            get
            {
                if (Metadata == null)
                    return String.Empty;

                return Metadata.LogicalName;
            }
        }

        public string PrimaryIdAttribute
        {
            get
            {
                if (Metadata == null)
                    return String.Empty;

                return Metadata.PrimaryIdAttribute;
            }
        }

        public string PrimaryNameAttribute
        {
            get
            {
                if (Metadata == null)
                    return String.Empty;

                return Metadata.PrimaryNameAttribute;
            }
        }
    }
}
