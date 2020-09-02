using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExportValidationChecker
{
    public class FailedRecord
    {
        public Guid Id { get; set; }
        public string FailureReason { get; internal set; }
        public string FailedValue { get; internal set; }
    }
}
