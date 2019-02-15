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
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.entitySelection = new xrmtb.XrmToolBox.Controls.EntitiesDropdownControl();
            this.loadGroupBox = new System.Windows.Forms.GroupBox();
            this.previewGroup = new System.Windows.Forms.GroupBox();
            this.metadataView = new System.Windows.Forms.DataGridView();
            this.checkButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.resultsGroupBox = new System.Windows.Forms.GroupBox();
            this.resultsView = new System.Windows.Forms.DataGridView();
            this.toolStripMenu.SuspendLayout();
            this.loadGroupBox.SuspendLayout();
            this.previewGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metadataView)).BeginInit();
            this.resultsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultsView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(1386, 25);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(86, 22);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // entitySelection
            // 
            this.entitySelection.AutoLoadData = false;
            this.entitySelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entitySelection.LanguageCode = 1033;
            this.entitySelection.Location = new System.Drawing.Point(3, 16);
            this.entitySelection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.entitySelection.Name = "entitySelection";
            this.entitySelection.Service = null;
            this.entitySelection.Size = new System.Drawing.Size(469, 32);
            this.entitySelection.SolutionFilter = null;
            this.entitySelection.TabIndex = 5;
            this.entitySelection.SelectedItemChanged += new System.EventHandler(this.entitySelection_SelectedItemChanged);
            this.entitySelection.Load += new System.EventHandler(this.entitySelection_Load);
            // 
            // loadGroupBox
            // 
            this.loadGroupBox.Controls.Add(this.entitySelection);
            this.loadGroupBox.Location = new System.Drawing.Point(8, 28);
            this.loadGroupBox.Name = "loadGroupBox";
            this.loadGroupBox.Size = new System.Drawing.Size(475, 51);
            this.loadGroupBox.TabIndex = 7;
            this.loadGroupBox.TabStop = false;
            this.loadGroupBox.Text = "Load Metadata";
            // 
            // previewGroup
            // 
            this.previewGroup.Controls.Add(this.metadataView);
            this.previewGroup.Location = new System.Drawing.Point(8, 86);
            this.previewGroup.Name = "previewGroup";
            this.previewGroup.Size = new System.Drawing.Size(480, 375);
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
            this.metadataView.Size = new System.Drawing.Size(474, 356);
            this.metadataView.TabIndex = 0;
            this.metadataView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.metadataView_CellEnter);
            // 
            // checkButton
            // 
            this.checkButton.Location = new System.Drawing.Point(8, 464);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(473, 30);
            this.checkButton.TabIndex = 9;
            this.checkButton.Text = "Validate Selected Fields ->";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 500);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(473, 30);
            this.button1.TabIndex = 9;
            this.button1.Text = "Validate All Fields ->";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.checkAllButton_Click);
            // 
            // resultsGroupBox
            // 
            this.resultsGroupBox.Controls.Add(this.resultsView);
            this.resultsGroupBox.Location = new System.Drawing.Point(494, 28);
            this.resultsGroupBox.Name = "resultsGroupBox";
            this.resultsGroupBox.Size = new System.Drawing.Size(450, 502);
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
            this.resultsView.Size = new System.Drawing.Size(444, 483);
            this.resultsView.TabIndex = 1;
            this.resultsView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.resultsView_CellMouseDoubleClick);
            // 
            // DataExportValidationCheckerPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkButton);
            this.Controls.Add(this.previewGroup);
            this.Controls.Add(this.resultsGroupBox);
            this.Controls.Add(this.loadGroupBox);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "DataExportValidationCheckerPluginControl";
            this.Size = new System.Drawing.Size(1386, 771);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.loadGroupBox.ResumeLayout(false);
            this.previewGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metadataView)).EndInit();
            this.resultsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultsView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private xrmtb.XrmToolBox.Controls.EntitiesDropdownControl entitySelection;
        private System.Windows.Forms.GroupBox loadGroupBox;
        private System.Windows.Forms.GroupBox previewGroup;
        private System.Windows.Forms.DataGridView metadataView;
        private System.Windows.Forms.Button checkButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox resultsGroupBox;
        private System.Windows.Forms.DataGridView resultsView;
    }
}
