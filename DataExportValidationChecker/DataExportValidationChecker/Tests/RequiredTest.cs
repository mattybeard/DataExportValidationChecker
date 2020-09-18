using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataExportValidationChecker.Tests
{
    public class RequiredTest : BaseTest
    {
        public override string TestTitle => $"Is Required";

        public override bool Execute(SearchAttributeDetails field, List<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var populated = true;
                if (!entity.Attributes.ContainsKey(field.LogicalName))
                {
                    populated = false;
                }
                else
                {
                    var attrValue = entity[field.LogicalName];
                    if (attrValue == null)
                    {
                        populated = false;
                    }
                }

                if (!populated)
                {
                    field.FailedRecords.Add(new FailedRecord()
                    {
                        Id = entity.Id,
                        FailureReason = $"Required field not populated",
                        FailedValue = ""
                    });
                }
            }

            return field.FailedCount > 0;
        }
    }
}
