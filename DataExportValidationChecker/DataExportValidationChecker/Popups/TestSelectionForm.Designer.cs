namespace DataExportValidationChecker.Popups
{
    partial class TestSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.whichLabel = new System.Windows.Forms.Label();
            this.metadataValidation = new System.Windows.Forms.CheckBox();
            this.regexValidation = new System.Windows.Forms.CheckBox();
            this.regexChoice = new System.Windows.Forms.ComboBox();
            this.okayButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.regexWarning = new System.Windows.Forms.PictureBox();
            this.applyToAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.regexWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // whichLabel
            // 
            this.whichLabel.AutoSize = true;
            this.whichLabel.Location = new System.Drawing.Point(12, 9);
            this.whichLabel.Name = "whichLabel";
            this.whichLabel.Size = new System.Drawing.Size(184, 13);
            this.whichLabel.TabIndex = 0;
            this.whichLabel.Text = "What tests would you like to perform?";
            // 
            // metadataValidation
            // 
            this.metadataValidation.AutoSize = true;
            this.metadataValidation.Location = new System.Drawing.Point(15, 29);
            this.metadataValidation.Name = "metadataValidation";
            this.metadataValidation.Size = new System.Drawing.Size(120, 17);
            this.metadataValidation.TabIndex = 1;
            this.metadataValidation.Text = "Metadata Validation";
            this.metadataValidation.UseVisualStyleBackColor = true;
            // 
            // regexValidation
            // 
            this.regexValidation.AutoSize = true;
            this.regexValidation.Location = new System.Drawing.Point(15, 52);
            this.regexValidation.Name = "regexValidation";
            this.regexValidation.Size = new System.Drawing.Size(106, 17);
            this.regexValidation.TabIndex = 1;
            this.regexValidation.Text = "Regex Validation";
            this.regexValidation.UseVisualStyleBackColor = true;
            this.regexValidation.CheckStateChanged += new System.EventHandler(this.regexValidation_CheckStateChanged);
            // 
            // regexChoice
            // 
            this.regexChoice.FormattingEnabled = true;
            this.regexChoice.Items.AddRange(new object[] {
            "Email",
            "UK Telephone",
            "US Telephone",
            "UK Postcode",
            "US Zipcode"});
            this.regexChoice.Location = new System.Drawing.Point(185, 48);
            this.regexChoice.Name = "regexChoice";
            this.regexChoice.Size = new System.Drawing.Size(121, 21);
            this.regexChoice.TabIndex = 2;
            this.regexChoice.Visible = false;
            // 
            // okayButton
            // 
            this.okayButton.Location = new System.Drawing.Point(150, 171);
            this.okayButton.Name = "okayButton";
            this.okayButton.Size = new System.Drawing.Size(75, 23);
            this.okayButton.TabIndex = 3;
            this.okayButton.Text = "Submit";
            this.okayButton.UseVisualStyleBackColor = true;
            this.okayButton.Click += new System.EventHandler(this.okayButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(231, 171);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // regexWarning
            // 
            this.regexWarning.Location = new System.Drawing.Point(163, 48);
            this.regexWarning.Name = "regexWarning";
            this.regexWarning.Size = new System.Drawing.Size(21, 21);
            this.regexWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.regexWarning.TabIndex = 4;
            this.regexWarning.TabStop = false;
            this.regexWarning.Visible = false;
            // 
            // applyToAll
            // 
            this.applyToAll.AutoSize = true;
            this.applyToAll.Location = new System.Drawing.Point(12, 175);
            this.applyToAll.Name = "applyToAll";
            this.applyToAll.Size = new System.Drawing.Size(97, 17);
            this.applyToAll.TabIndex = 1;
            this.applyToAll.Text = "Apply rule to all";
            this.applyToAll.UseVisualStyleBackColor = true;
            // 
            // TestSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 206);
            this.Controls.Add(this.regexWarning);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okayButton);
            this.Controls.Add(this.regexChoice);
            this.Controls.Add(this.regexValidation);
            this.Controls.Add(this.applyToAll);
            this.Controls.Add(this.metadataValidation);
            this.Controls.Add(this.whichLabel);
            this.Name = "TestSelectionForm";
            this.Text = "Test Selections";
            ((System.ComponentModel.ISupportInitialize)(this.regexWarning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label whichLabel;
        private System.Windows.Forms.CheckBox metadataValidation;
        private System.Windows.Forms.CheckBox regexValidation;
        private System.Windows.Forms.ComboBox regexChoice;
        private System.Windows.Forms.Button okayButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.PictureBox regexWarning;
        private System.Windows.Forms.CheckBox applyToAll;
    }
}