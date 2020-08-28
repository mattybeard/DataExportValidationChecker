namespace DataExportValidationChecker
{
    partial class DataExportValidationCheckerPluginControl2
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
            this.noIssuesLabel = new System.Windows.Forms.Label();
            this.resultsView = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.toolStripMenu.SuspendLayout();
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
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(841, 25);
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
            this.entitySelection.Size = new System.Drawing.Size(504, 32);
            this.entitySelection.SolutionFilter = null;
            this.entitySelection.TabIndex = 5;
            this.entitySelection.SelectedItemChanged += new System.EventHandler(this.entitySelection_SelectedItemChanged);
            this.entitySelection.Load += new System.EventHandler(this.entitySelection_Load);
            // 
            // loadGroupBox
            // 
            this.loadGroupBox.Controls.Add(this.entitySelection);
            this.loadGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.loadGroupBox.Location = new System.Drawing.Point(0, 0);
            this.loadGroupBox.Name = "loadGroupBox";
            this.loadGroupBox.Size = new System.Drawing.Size(510, 51);
            this.loadGroupBox.TabIndex = 7;
            this.loadGroupBox.TabStop = false;
            this.loadGroupBox.Text = "Load Metadata";
            // 
            // previewGroup
            // 
            this.previewGroup.Controls.Add(this.metadataView);
            this.previewGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewGroup.Location = new System.Drawing.Point(0, 51);
            this.previewGroup.Name = "previewGroup";
            this.previewGroup.Size = new System.Drawing.Size(510, 433);
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
            this.metadataView.Size = new System.Drawing.Size(504, 414);
            this.metadataView.TabIndex = 0;
            this.metadataView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.metadataView_CellClick);
            this.metadataView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.metadataView_CellEnter);
            // 
            // checkButton
            // 
            this.checkButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkButton.Location = new System.Drawing.Point(3, 3);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(504, 30);
            this.checkButton.TabIndex = 9;
            this.checkButton.Text = "Validate Selected Fields ->";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(3, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(504, 30);
            this.button1.TabIndex = 9;
            this.button1.Text = "Validate All Fields ->";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.checkAllButton_Click);
            // 
            // resultsGroupBox
            // 
            this.resultsGroupBox.Controls.Add(this.noIssuesLabel);
            this.resultsGroupBox.Controls.Add(this.resultsView);
            this.resultsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.resultsGroupBox.Name = "resultsGroupBox";
            this.resultsGroupBox.Size = new System.Drawing.Size(327, 560);
            this.resultsGroupBox.TabIndex = 7;
            this.resultsGroupBox.TabStop = false;
            this.resultsGroupBox.Text = "Results";
            // 
            // noIssuesLabel
            // 
            this.noIssuesLabel.AutoSize = true;
            this.noIssuesLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.noIssuesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noIssuesLabel.Location = new System.Drawing.Point(3, 16);
            this.noIssuesLabel.Name = "noIssuesLabel";
            this.noIssuesLabel.Size = new System.Drawing.Size(424, 39);
            this.noIssuesLabel.TabIndex = 2;
            this.noIssuesLabel.Text = "No validation issues found!";
            this.noIssuesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.noIssuesLabel.Visible = false;
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
            this.resultsView.Size = new System.Drawing.Size(321, 541);
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
            this.splitContainer1.Size = new System.Drawing.Size(841, 560);
            this.splitContainer1.SplitterDistance = 510;
            this.splitContainer1.TabIndex = 10;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.button1);
            this.panelButtons.Controls.Add(this.checkButton);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 484);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(510, 76);
            this.panelButtons.TabIndex = 8;
            // 
            // DataExportValidationCheckerPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "DataExportValidationCheckerPluginControl";
            this.Size = new System.Drawing.Size(841, 585);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.loadGroupBox.ResumeLayout(false);
            this.previewGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metadataView)).EndInit();
            this.resultsGroupBox.ResumeLayout(false);
            this.resultsGroupBox.PerformLayout();
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.DataGridView resultsView;
        private System.Windows.Forms.Label noIssuesLabel;
    }
}
