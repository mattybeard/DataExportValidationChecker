using DataExportValidationChecker.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataExportValidationChecker.Popups
{
    internal partial class TestSelectionForm : Form
    {
        private List<SearchAttributeDetails> allAttributes { get; set; }
        private SearchAttributeDetails attributeDetails { get; set; }
        public TestSelectionForm(List<SearchAttributeDetails> allAttributes, SearchAttributeDetails attributeDetails)
        {
            InitializeComponent();
            this.allAttributes = allAttributes;
            this.attributeDetails = attributeDetails;
        }

        private void regexValidation_CheckStateChanged(object sender, EventArgs e)
        {
            regexChoice.Visible = regexValidation.Checked;
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            if (metadataValidation.Checked)
            {
                if (applyToAll.Checked)
                {
                    foreach (var attr in allAttributes)
                        attr.AddTest(new MatchesMetadataTest());
                }
                else
                {
                    attributeDetails.AddTest(new MatchesMetadataTest());
                }
            }

            if (regexValidation.Checked)
            {
                if (regexChoice.SelectedIndex == -1)
                {
                    regexWarning.Image = SystemIcons.Warning.ToBitmap();
                    regexWarning.Visible = true;
                    return;
                }

                if (applyToAll.Checked)
                {
                    foreach (var attr in allAttributes)
                        attr.AddTest(new MatchesRegexTest() { RegexType = (string)regexChoice.SelectedItem });
                }
                else
                {
                    attributeDetails.AddTest(new MatchesRegexTest() { RegexType = (string)regexChoice.SelectedItem });
                }
            }

            if (applyToAll.Checked)
            {
                foreach (var attr in allAttributes)
                    attr.TestsStr = String.Join(", ", attributeDetails.Tests.Select(t => t.TestTitle));
            }
            else
            {
                attributeDetails.TestsStr = String.Join(", ", attributeDetails.Tests.Select(t => t.TestTitle));
            }

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
