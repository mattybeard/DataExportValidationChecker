namespace DataExportValidationChecker
{
    partial class DataExportValidationCheckerPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.loadGroupBox = new System.Windows.Forms.GroupBox();
            this.tableSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.previewGroup = new System.Windows.Forms.GroupBox();
            this.metadataView = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.resultsGroupBox = new System.Windows.Forms.GroupBox();
            this.resultsView = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.loadGroupBox.SuspendLayout();
            this.previewGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metadataView)).BeginInit();
            this.resultsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadGroupBox
            // 
            this.loadGroupBox.Controls.Add(this.tableSelectionComboBox);
            this.loadGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.loadGroupBox.Location = new System.Drawing.Point(0, 0);
            this.loadGroupBox.Name = "loadGroupBox";
            this.loadGroupBox.Size = new System.Drawing.Size(500, 51);
            this.loadGroupBox.TabIndex = 7;
            this.loadGroupBox.TabStop = false;
            this.loadGroupBox.Text = "Load Metadata";
            // 
            // tableSelectionComboBox
            // 
            this.tableSelectionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tableSelectionComboBox.FormattingEnabled = true;
            this.tableSelectionComboBox.Location = new System.Drawing.Point(3, 16);
            this.tableSelectionComboBox.Name = "tableSelectionComboBox";
            this.tableSelectionComboBox.Size = new System.Drawing.Size(494, 21);
            this.tableSelectionComboBox.TabIndex = 0;
            this.tableSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.tableSelectionComboBox_SelectedIndexChanged);
            // 
            // previewGroup
            // 
            this.previewGroup.Controls.Add(this.metadataView);
            this.previewGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewGroup.Location = new System.Drawing.Point(0, 51);
            this.previewGroup.Name = "previewGroup";
            this.previewGroup.Size = new System.Drawing.Size(500, 512);
            this.previewGroup.TabIndex = 8;
            this.previewGroup.TabStop = false;
            this.previewGroup.Text = "Preview Metadata";
            this.previewGroup.Visible = false;
            // 
            // metadataView
            // 
            this.metadataView.AllowUserToAddRows = false;
            this.metadataView.AllowUserToDeleteRows = false;
            this.metadataView.AllowUserToResizeRows = false;
            this.metadataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.metadataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metadataView.Location = new System.Drawing.Point(3, 16);
            this.metadataView.Name = "metadataView";
            this.metadataView.RowHeadersVisible = false;
            this.metadataView.Size = new System.Drawing.Size(494, 493);
            this.metadataView.TabIndex = 0;
            this.metadataView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.metadataView_CellDoubleClick);
            this.metadataView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.metadataView_CellEnter);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(5, 5);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(5);
            this.button1.Size = new System.Drawing.Size(490, 52);
            this.button1.TabIndex = 9;
            this.button1.Text = "Validate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.checkAllButton_Click);
            // 
            // resultsGroupBox
            // 
            this.resultsGroupBox.Controls.Add(this.resultsView);
            this.resultsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.resultsGroupBox.Name = "resultsGroupBox";
            this.resultsGroupBox.Size = new System.Drawing.Size(496, 625);
            this.resultsGroupBox.TabIndex = 7;
            this.resultsGroupBox.TabStop = false;
            this.resultsGroupBox.Text = "Results";
            // 
            // resultsView
            // 
            this.resultsView.AllowUserToAddRows = false;
            this.resultsView.AllowUserToDeleteRows = false;
            this.resultsView.AllowUserToResizeRows = false;
            this.resultsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsView.Location = new System.Drawing.Point(3, 16);
            this.resultsView.Name = "resultsView";
            this.resultsView.RowHeadersVisible = false;
            this.resultsView.Size = new System.Drawing.Size(490, 606);
            this.resultsView.TabIndex = 1;
            this.resultsView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.resultsView_CellMouseDoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.previewGroup);
            this.splitContainer1.Panel1.Controls.Add(this.panelButtons);
            this.splitContainer1.Panel1.Controls.Add(this.loadGroupBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.resultsGroupBox);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 625);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 10;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.button1);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 563);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(5);
            this.panelButtons.Size = new System.Drawing.Size(500, 62);
            this.panelButtons.TabIndex = 8;
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(1000, 25);
            this.mainToolStrip.TabIndex = 11;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // DataExportValidationCheckerPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainToolStrip);
            this.Name = "DataExportValidationCheckerPluginControl";
            this.Size = new System.Drawing.Size(1000, 650);
            this.Load += new System.EventHandler(this.PluginControl_Load);
            this.loadGroupBox.ResumeLayout(false);
            this.previewGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metadataView)).EndInit();
            this.resultsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultsView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox loadGroupBox;
        private System.Windows.Forms.GroupBox previewGroup;
        private System.Windows.Forms.DataGridView metadataView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox resultsGroupBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.DataGridView resultsView;
        private System.Windows.Forms.ComboBox tableSelectionComboBox;
        private System.Windows.Forms.ToolStrip mainToolStrip;
    }
}
