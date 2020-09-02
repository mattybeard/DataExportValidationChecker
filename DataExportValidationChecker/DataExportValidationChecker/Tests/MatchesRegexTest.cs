using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataExportValidationChecker.Tests
{
    public class MatchesRegexTest : BaseTest
    {
        public override string TestTitle => $"Matches {RegexType} regex";
        public string RegexType { get; set; }

        public override bool Execute(SearchAttributeDetails field, List<Entity> entities)
        {
            Regex regex;
            switch (RegexType)
            {
                case "Email":
                    // Using regex found here: https://www.rhyous.com/2010/06/15/csharp-email-regular-expression/
                    var emailPattern = @".+\@.+\..+";
                    regex = new Regex(emailPattern, RegexOptions.Compiled);
                    break;
                case "UK Telephone":
                    var ukPhoneRegex = @"(\s*\(?(0|\+44)(\s*|-)\d{4}\)?(\s*|-)\d{3}(\s*|-)\d{3}\s*)|(\s*\(?(0|\+44)(\s*|-)\d{3}\)?(\s*|-)\d{3}(\s*|-)\d{4}\s*)|(\s*\(?(0|\+44)(\s*|-)\d{2}\)?(\s*|-)\d{4}(\s*|-)\d{4}\s*)|(\s*(7|8)(\d{7}|\d{3}(\-|\s{1})\d{4})\s*)|(\s*\(?(0|\+44)(\s*|-)\d{3}\s\d{2}\)?(\s*|-)\d{4,5}\s*)";
                    regex = new Regex(ukPhoneRegex, RegexOptions.Compiled);
                    break;
                case "US Telephone":
                    var usPhoneRegex = @"^(\([0-9]{3}\) |[0-9]{3}-)[0-9]{3}-[0-9]{4}$";
                    regex = new Regex(usPhoneRegex, RegexOptions.Compiled);
                    break;
                case "UK Postcode":
                    var ukPostcodeRegex = @"^(([A-Z]{1,2}[0-9][A-Z0-9]?|ASCN|STHL|TDCU|BBND|[BFS]IQQ|PCRN|TKCA) ?[0-9][A-Z]{2}|BFPO ?[0-9]{1,4}|(KY[0-9]|MSR|VG|AI)[ -]?[0-9]{4}|[A-Z]{2} ?[0-9]{2}|GE ?CX|GIR ?0A{2}|SAN ?TA1)$";
                    regex = new Regex(ukPostcodeRegex, RegexOptions.Compiled);
                    break;
                case "US  Zipcode":
                    var usZipcodeRegex = @"^[0-9]{5}(?:-[0-9]{4})?$";
                    regex = new Regex(usZipcodeRegex, RegexOptions.Compiled);
                    break;
                default:
                    regex = new Regex("");
                    break;
            }

            foreach (var entity in entities)
            {
                var fieldValue = entity.GetAttributeValue<string>(field.LogicalName);
                if (!string.IsNullOrEmpty(fieldValue))
                {
                    var regexMatch = regex.IsMatch(fieldValue);
                    if (!regexMatch)
                    {
                        field.FailedRecords.Add(new FailedRecord()
                        {
                            Id = entity.Id,
                            FailureReason = $"Failed {RegexType} regex",
                            FailedValue = fieldValue
                        });
                    }
                }
            }

            return field.FailedCount > 0;
        }
    }
}
