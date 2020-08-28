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
        private SearchAttributeDetails attributeDetails { get; set; }
        public TestSelectionForm(SearchAttributeDetails attributeDetails)
        {
            InitializeComponent();
            this.attributeDetails = attributeDetails;
        }

        private void regexValidation_CheckStateChanged(object sender, EventArgs e)
        {
            regexChoice.Visible = regexValidation.Checked;
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            attributeDetails.Tests = new List<BaseTest>();
            
            if (metadataValidation.Checked)
                attributeDetails.Tests.Add(new MatchesMetadataTest());

            if (regexValidation.Checked)
            {
                if (regexChoice.SelectedIndex == -1)
                {
                    regexWarning.Image = SystemIcons.Warning.ToBitmap();
                    regexWarning.Visible = true;
                    return;
                }

                attributeDetails.Tests.Add(new MatchesRegexTest() { RegexType = (string)regexChoice.SelectedItem });
            }

            attributeDetails.TestsStr = String.Join(", ", attributeDetails.Tests.Select(t => t.TestTitle));

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
