namespace SQLServerSearcher
{
    partial class FrmSqlServerSearcher
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Tables");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Views");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Stored procedures");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Functions");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Indexes");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSqlServerSearcher));
            this.btnFind = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFindText = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTables = new System.Windows.Forms.CheckBox();
            this.chkStoredProcedures = new System.Windows.Forms.CheckBox();
            this.chkViews = new System.Windows.Forms.CheckBox();
            this.chkFunctions = new System.Windows.Forms.CheckBox();
            this.chkMatchCase = new System.Windows.Forms.CheckBox();
            this.tvResults = new System.Windows.Forms.TreeView();
            this.cmsResults = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmViewSource = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFindAllReferences = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopyListToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopyNameToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopyInformationToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopyQueryToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvDatabaseProperties = new System.Windows.Forms.ListView();
            this.colDatabasePropertyName = new System.Windows.Forms.ColumnHeader();
            this.colDatabasePropertyValue = new System.Windows.Forms.ColumnHeader();
            this.cmsServerInformation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmCopyServerInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.lvServerProperties = new System.Windows.Forms.ListView();
            this.colServerPropertyName = new System.Windows.Forms.ColumnHeader();
            this.colServerPropertyValue = new System.Windows.Forms.ColumnHeader();
            this.lvObjectInformation = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colValue = new System.Windows.Forms.ColumnHeader();
            this.cmsObjectInformation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmCopyInformationToClipboardToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbDatabase = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkIndexes = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsLblRowCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLblExecutionTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLblDatabase = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLblServer = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbSettings = new System.Windows.Forms.PictureBox();
            this.cmsSettings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ttSettings = new System.Windows.Forms.ToolTip(this.components);
            this.chkExtendedProperties = new System.Windows.Forms.CheckBox();
            this.cmsResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmsServerInformation.SuspendLayout();
            this.cmsObjectInformation.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).BeginInit();
            this.cmsSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(438, 63);
            this.btnFind.Margin = new System.Windows.Forms.Padding(2);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(93, 27);
            this.btnFind.TabIndex = 10;
            this.btnFind.Text = "&Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find what:";
            // 
            // cmbFindText
            // 
            this.cmbFindText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFindText.FormattingEnabled = true;
            this.cmbFindText.Location = new System.Drawing.Point(75, 63);
            this.cmbFindText.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFindText.Name = "cmbFindText";
            this.cmbFindText.Size = new System.Drawing.Size(358, 23);
            this.cmbFindText.TabIndex = 9;
            this.cmbFindText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbFindText_KeyUp);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(541, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Look in:";
            // 
            // chkTables
            // 
            this.chkTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTables.AutoSize = true;
            this.chkTables.Location = new System.Drawing.Point(550, 30);
            this.chkTables.Margin = new System.Windows.Forms.Padding(2);
            this.chkTables.Name = "chkTables";
            this.chkTables.Size = new System.Drawing.Size(58, 19);
            this.chkTables.TabIndex = 4;
            this.chkTables.Text = "Tables";
            this.chkTables.UseVisualStyleBackColor = true;
            // 
            // chkStoredProcedures
            // 
            this.chkStoredProcedures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkStoredProcedures.AutoSize = true;
            this.chkStoredProcedures.Location = new System.Drawing.Point(632, 30);
            this.chkStoredProcedures.Margin = new System.Windows.Forms.Padding(2);
            this.chkStoredProcedures.Name = "chkStoredProcedures";
            this.chkStoredProcedures.Size = new System.Drawing.Size(122, 19);
            this.chkStoredProcedures.TabIndex = 7;
            this.chkStoredProcedures.Text = "Stored procedures";
            this.chkStoredProcedures.UseVisualStyleBackColor = true;
            // 
            // chkViews
            // 
            this.chkViews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkViews.AutoSize = true;
            this.chkViews.Location = new System.Drawing.Point(550, 52);
            this.chkViews.Margin = new System.Windows.Forms.Padding(2);
            this.chkViews.Name = "chkViews";
            this.chkViews.Size = new System.Drawing.Size(56, 19);
            this.chkViews.TabIndex = 5;
            this.chkViews.Text = "Views";
            this.chkViews.UseVisualStyleBackColor = true;
            // 
            // chkFunctions
            // 
            this.chkFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFunctions.AutoSize = true;
            this.chkFunctions.Location = new System.Drawing.Point(632, 52);
            this.chkFunctions.Margin = new System.Windows.Forms.Padding(2);
            this.chkFunctions.Name = "chkFunctions";
            this.chkFunctions.Size = new System.Drawing.Size(78, 19);
            this.chkFunctions.TabIndex = 8;
            this.chkFunctions.Text = "Functions";
            this.chkFunctions.UseVisualStyleBackColor = true;
            // 
            // chkMatchCase
            // 
            this.chkMatchCase.AutoSize = true;
            this.chkMatchCase.Enabled = false;
            this.chkMatchCase.Location = new System.Drawing.Point(12, 88);
            this.chkMatchCase.Margin = new System.Windows.Forms.Padding(2);
            this.chkMatchCase.Name = "chkMatchCase";
            this.chkMatchCase.Size = new System.Drawing.Size(86, 19);
            this.chkMatchCase.TabIndex = 11;
            this.chkMatchCase.Text = "Match case";
            this.chkMatchCase.UseVisualStyleBackColor = true;
            // 
            // tvResults
            // 
            this.tvResults.ContextMenuStrip = this.cmsResults;
            this.tvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvResults.Location = new System.Drawing.Point(0, 0);
            this.tvResults.Margin = new System.Windows.Forms.Padding(2);
            this.tvResults.Name = "tvResults";
            treeNode1.Name = "TablesNode";
            treeNode1.Text = "Tables";
            treeNode2.Name = "ViewsNode";
            treeNode2.Text = "Views";
            treeNode3.Name = "StoredProceduresNode";
            treeNode3.Text = "Stored procedures";
            treeNode4.Name = "FunctionsNode";
            treeNode4.Text = "Functions";
            treeNode5.Name = "IndexesNode";
            treeNode5.Text = "Indexes";
            this.tvResults.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.tvResults.Size = new System.Drawing.Size(254, 443);
            this.tvResults.TabIndex = 12;
            this.tvResults.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvResults_NodeMouseClick);
            this.tvResults.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvResults_KeyUp);
            // 
            // cmsResults
            // 
            this.cmsResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmViewSource,
            this.tsmFindAllReferences,
            this.tsmCopyListToClipboardToolStripMenuItem,
            this.tsmCopyNameToClipboardToolStripMenuItem,
            this.tsmCopyInformationToClipboardToolStripMenuItem,
            this.tsmCopyQueryToClipboardToolStripMenuItem});
            this.cmsResults.Name = "cmsResults";
            this.cmsResults.Size = new System.Drawing.Size(236, 136);
            this.cmsResults.Opening += new System.ComponentModel.CancelEventHandler(this.cmsResults_Opening);
            // 
            // tsmViewSource
            // 
            this.tsmViewSource.Name = "tsmViewSource";
            this.tsmViewSource.Size = new System.Drawing.Size(235, 22);
            this.tsmViewSource.Text = "&View source";
            this.tsmViewSource.Click += new System.EventHandler(this.tsmViewSource_Click);
            // 
            // tsmFindAllReferences
            // 
            this.tsmFindAllReferences.Name = "tsmFindAllReferences";
            this.tsmFindAllReferences.Size = new System.Drawing.Size(235, 22);
            this.tsmFindAllReferences.Text = "&Find all references";
            this.tsmFindAllReferences.Click += new System.EventHandler(this.tsmFindAllReferences_Click);
            // 
            // tsmCopyListToClipboardToolStripMenuItem
            // 
            this.tsmCopyListToClipboardToolStripMenuItem.Name = "tsmCopyListToClipboardToolStripMenuItem";
            this.tsmCopyListToClipboardToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.tsmCopyListToClipboardToolStripMenuItem.Text = "Copy list to clipboard";
            this.tsmCopyListToClipboardToolStripMenuItem.Click += new System.EventHandler(this.tsmCopyListToClipboardToolStripMenuItem_Click);
            // 
            // tsmCopyNameToClipboardToolStripMenuItem
            // 
            this.tsmCopyNameToClipboardToolStripMenuItem.Name = "tsmCopyNameToClipboardToolStripMenuItem";
            this.tsmCopyNameToClipboardToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.tsmCopyNameToClipboardToolStripMenuItem.Text = "Copy name to clipboard";
            this.tsmCopyNameToClipboardToolStripMenuItem.Click += new System.EventHandler(this.tsmCopyNameToClipboardToolStripMenuItem_Click);
            // 
            // tsmCopyInformationToClipboardToolStripMenuItem
            // 
            this.tsmCopyInformationToClipboardToolStripMenuItem.Name = "tsmCopyInformationToClipboardToolStripMenuItem";
            this.tsmCopyInformationToClipboardToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.tsmCopyInformationToClipboardToolStripMenuItem.Text = "Copy information to clipboard";
            this.tsmCopyInformationToClipboardToolStripMenuItem.Click += new System.EventHandler(this.tsmCopyInformationToClipboardToolStripMenuItem_Click);
            // 
            // tsmCopyQueryToClipboardToolStripMenuItem
            // 
            this.tsmCopyQueryToClipboardToolStripMenuItem.Name = "tsmCopyQueryToClipboardToolStripMenuItem";
            this.tsmCopyQueryToClipboardToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.tsmCopyQueryToClipboardToolStripMenuItem.Text = "Copy query to clipboard";
            this.tsmCopyQueryToClipboardToolStripMenuItem.Click += new System.EventHandler(this.tsmCopyQueryToClipboardToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Server:";
            // 
            // cmbServer
            // 
            this.cmbServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Location = new System.Drawing.Point(75, 13);
            this.cmbServer.Margin = new System.Windows.Forms.Padding(2);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(358, 23);
            this.cmbServer.TabIndex = 1;
            this.cmbServer.TextChanged += new System.EventHandler(this.cmbServer_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 111);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvResults);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvDatabaseProperties);
            this.splitContainer1.Panel2.Controls.Add(this.lvServerProperties);
            this.splitContainer1.Panel2.Controls.Add(this.lvObjectInformation);
            this.splitContainer1.Size = new System.Drawing.Size(771, 443);
            this.splitContainer1.SplitterDistance = 254;
            this.splitContainer1.TabIndex = 14;
            // 
            // lvDatabaseProperties
            // 
            this.lvDatabaseProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDatabasePropertyName,
            this.colDatabasePropertyValue});
            this.lvDatabaseProperties.ContextMenuStrip = this.cmsServerInformation;
            this.lvDatabaseProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvDatabaseProperties.FullRowSelect = true;
            this.lvDatabaseProperties.GridLines = true;
            this.lvDatabaseProperties.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDatabaseProperties.HideSelection = false;
            this.lvDatabaseProperties.Location = new System.Drawing.Point(0, 136);
            this.lvDatabaseProperties.Margin = new System.Windows.Forms.Padding(2);
            this.lvDatabaseProperties.Name = "lvDatabaseProperties";
            this.lvDatabaseProperties.Size = new System.Drawing.Size(513, 136);
            this.lvDatabaseProperties.TabIndex = 15;
            this.lvDatabaseProperties.UseCompatibleStateImageBehavior = false;
            this.lvDatabaseProperties.View = System.Windows.Forms.View.Details;
            // 
            // colDatabasePropertyName
            // 
            this.colDatabasePropertyName.Text = "Property";
            this.colDatabasePropertyName.Width = 135;
            // 
            // colDatabasePropertyValue
            // 
            this.colDatabasePropertyValue.Text = "Value";
            this.colDatabasePropertyValue.Width = 170;
            // 
            // cmsServerInformation
            // 
            this.cmsServerInformation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCopyServerInformation});
            this.cmsServerInformation.Name = "cmsServerInformation";
            this.cmsServerInformation.Size = new System.Drawing.Size(203, 26);
            // 
            // tsmCopyServerInformation
            // 
            this.tsmCopyServerInformation.Name = "tsmCopyServerInformation";
            this.tsmCopyServerInformation.Size = new System.Drawing.Size(202, 22);
            this.tsmCopyServerInformation.Text = "Copy server information";
            this.tsmCopyServerInformation.Click += new System.EventHandler(this.tsmCopyServerInformation_Click);
            // 
            // lvServerProperties
            // 
            this.lvServerProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colServerPropertyName,
            this.colServerPropertyValue});
            this.lvServerProperties.ContextMenuStrip = this.cmsServerInformation;
            this.lvServerProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvServerProperties.FullRowSelect = true;
            this.lvServerProperties.GridLines = true;
            this.lvServerProperties.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvServerProperties.HideSelection = false;
            this.lvServerProperties.Location = new System.Drawing.Point(0, 0);
            this.lvServerProperties.Margin = new System.Windows.Forms.Padding(2);
            this.lvServerProperties.Name = "lvServerProperties";
            this.lvServerProperties.Size = new System.Drawing.Size(513, 136);
            this.lvServerProperties.TabIndex = 13;
            this.lvServerProperties.UseCompatibleStateImageBehavior = false;
            this.lvServerProperties.View = System.Windows.Forms.View.Details;
            // 
            // colServerPropertyName
            // 
            this.colServerPropertyName.Text = "Property";
            this.colServerPropertyName.Width = 135;
            // 
            // colServerPropertyValue
            // 
            this.colServerPropertyValue.Text = "Value";
            this.colServerPropertyValue.Width = 170;
            // 
            // lvObjectInformation
            // 
            this.lvObjectInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvObjectInformation.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colValue});
            this.lvObjectInformation.ContextMenuStrip = this.cmsObjectInformation;
            this.lvObjectInformation.FullRowSelect = true;
            this.lvObjectInformation.GridLines = true;
            this.lvObjectInformation.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvObjectInformation.HideSelection = false;
            this.lvObjectInformation.Location = new System.Drawing.Point(0, 270);
            this.lvObjectInformation.Margin = new System.Windows.Forms.Padding(2);
            this.lvObjectInformation.MultiSelect = false;
            this.lvObjectInformation.Name = "lvObjectInformation";
            this.lvObjectInformation.Size = new System.Drawing.Size(513, 177);
            this.lvObjectInformation.TabIndex = 14;
            this.lvObjectInformation.UseCompatibleStateImageBehavior = false;
            this.lvObjectInformation.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Property";
            this.colName.Width = 140;
            // 
            // colValue
            // 
            this.colValue.Text = "Value";
            this.colValue.Width = 140;
            // 
            // cmsObjectInformation
            // 
            this.cmsObjectInformation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCopyInformationToClipboardToolStripMenuItem2});
            this.cmsObjectInformation.Name = "cmsObjectInformation";
            this.cmsObjectInformation.Size = new System.Drawing.Size(236, 26);
            this.cmsObjectInformation.Opening += new System.ComponentModel.CancelEventHandler(this.cmsObjectInformation_Opening);
            // 
            // tsmCopyInformationToClipboardToolStripMenuItem2
            // 
            this.tsmCopyInformationToClipboardToolStripMenuItem2.Name = "tsmCopyInformationToClipboardToolStripMenuItem2";
            this.tsmCopyInformationToClipboardToolStripMenuItem2.Size = new System.Drawing.Size(235, 22);
            this.tsmCopyInformationToClipboardToolStripMenuItem2.Text = "Copy information to clipboard";
            this.tsmCopyInformationToClipboardToolStripMenuItem2.Click += new System.EventHandler(this.tsmCopyInformationToClipboardToolStripMenuItem_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(438, 9);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(93, 24);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmbDatabase
            // 
            this.cmbDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDatabase.FormattingEnabled = true;
            this.cmbDatabase.Location = new System.Drawing.Point(75, 38);
            this.cmbDatabase.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDatabase.Name = "cmbDatabase";
            this.cmbDatabase.Size = new System.Drawing.Size(455, 23);
            this.cmbDatabase.TabIndex = 3;
            this.cmbDatabase.SelectedIndexChanged += new System.EventHandler(this.cmbDatabase_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Database:";
            // 
            // chkIndexes
            // 
            this.chkIndexes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIndexes.AutoSize = true;
            this.chkIndexes.Location = new System.Drawing.Point(550, 76);
            this.chkIndexes.Margin = new System.Windows.Forms.Padding(2);
            this.chkIndexes.Name = "chkIndexes";
            this.chkIndexes.Size = new System.Drawing.Size(66, 19);
            this.chkIndexes.TabIndex = 6;
            this.chkIndexes.Text = "Indexes";
            this.chkIndexes.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLblRowCount,
            this.tsLblExecutionTime,
            this.tsLblDatabase,
            this.tsLblServer});
            this.statusStrip1.Location = new System.Drawing.Point(0, 558);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(796, 24);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsLblRowCount
            // 
            this.tsLblRowCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsLblRowCount.Name = "tsLblRowCount";
            this.tsLblRowCount.Size = new System.Drawing.Size(51, 19);
            this.tsLblRowCount.Text = "Rows: 0";
            // 
            // tsLblExecutionTime
            // 
            this.tsLblExecutionTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsLblExecutionTime.Name = "tsLblExecutionTime";
            this.tsLblExecutionTime.Size = new System.Drawing.Size(53, 19);
            this.tsLblExecutionTime.Text = "00:00:00";
            // 
            // tsLblDatabase
            // 
            this.tsLblDatabase.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tsLblDatabase.Name = "tsLblDatabase";
            this.tsLblDatabase.Size = new System.Drawing.Size(33, 19);
            this.tsLblDatabase.Text = "N/A";
            // 
            // tsLblServer
            // 
            this.tsLblServer.Name = "tsLblServer";
            this.tsLblServer.Size = new System.Drawing.Size(67, 19);
            this.tsLblServer.Text = "Server: N/A";
            // 
            // pbSettings
            // 
            this.pbSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSettings.ContextMenuStrip = this.cmsSettings;
            this.pbSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSettings.Image = ((System.Drawing.Image)(resources.GetObject("pbSettings.Image")));
            this.pbSettings.Location = new System.Drawing.Point(758, 9);
            this.pbSettings.Margin = new System.Windows.Forms.Padding(2);
            this.pbSettings.Name = "pbSettings";
            this.pbSettings.Size = new System.Drawing.Size(24, 24);
            this.pbSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSettings.TabIndex = 20;
            this.pbSettings.TabStop = false;
            this.ttSettings.SetToolTip(this.pbSettings, "Settings");
            this.pbSettings.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbSettings_MouseUp);
            // 
            // cmsSettings
            // 
            this.cmsSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changelogToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.cmsSettings.Name = "cmsSettings";
            this.cmsSettings.Size = new System.Drawing.Size(133, 48);
            // 
            // changelogToolStripMenuItem
            // 
            this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            this.changelogToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.changelogToolStripMenuItem.Text = "Changelog";
            this.changelogToolStripMenuItem.Click += new System.EventHandler(this.changelogToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // chkExtendedProperties
            // 
            this.chkExtendedProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkExtendedProperties.AutoSize = true;
            this.chkExtendedProperties.Location = new System.Drawing.Point(632, 75);
            this.chkExtendedProperties.Margin = new System.Windows.Forms.Padding(2);
            this.chkExtendedProperties.Name = "chkExtendedProperties";
            this.chkExtendedProperties.Size = new System.Drawing.Size(131, 19);
            this.chkExtendedProperties.TabIndex = 21;
            this.chkExtendedProperties.Text = "Extended properties";
            this.chkExtendedProperties.UseVisualStyleBackColor = true;
            // 
            // FrmSqlServerSearcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 582);
            this.Controls.Add(this.chkExtendedProperties);
            this.Controls.Add(this.pbSettings);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkIndexes);
            this.Controls.Add(this.cmbDatabase);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmbServer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkMatchCase);
            this.Controls.Add(this.chkFunctions);
            this.Controls.Add(this.chkViews);
            this.Controls.Add(this.chkStoredProcedures);
            this.Controls.Add(this.chkTables);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbFindText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFind);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmSqlServerSearcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Sql Server Searcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSqlServerSearcher_FormClosing);
            this.cmsResults.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmsServerInformation.ResumeLayout(false);
            this.cmsObjectInformation.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).EndInit();
            this.cmsSettings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFindText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkTables;
        private System.Windows.Forms.CheckBox chkStoredProcedures;
        private System.Windows.Forms.CheckBox chkViews;
        private System.Windows.Forms.CheckBox chkFunctions;
        private System.Windows.Forms.CheckBox chkMatchCase;
        private System.Windows.Forms.TreeView tvResults;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbDatabase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkIndexes;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsLblRowCount;
        private System.Windows.Forms.ToolStripStatusLabel tsLblExecutionTime;
        private System.Windows.Forms.ToolStripStatusLabel tsLblDatabase;
        private System.Windows.Forms.ToolStripStatusLabel tsLblServer;
        private System.Windows.Forms.ListView lvObjectInformation;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.ListView lvServerProperties;
        private System.Windows.Forms.ColumnHeader colServerPropertyName;
        private System.Windows.Forms.ColumnHeader colServerPropertyValue;
        private System.Windows.Forms.ContextMenuStrip cmsResults;
        private System.Windows.Forms.ToolStripMenuItem tsmViewSource;
        private System.Windows.Forms.ToolStripMenuItem tsmFindAllReferences;
        private System.Windows.Forms.PictureBox pbSettings;
        private System.Windows.Forms.ToolTip ttSettings;
        private System.Windows.Forms.ContextMenuStrip cmsSettings;
        private System.Windows.Forms.ToolStripMenuItem changelogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyListToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyNameToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyInformationToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyQueryToClipboardToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsObjectInformation;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyInformationToClipboardToolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip cmsServerInformation;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyServerInformation;
        private System.Windows.Forms.CheckBox chkExtendedProperties;
        private System.Windows.Forms.ListView lvDatabaseProperties;
        private System.Windows.Forms.ColumnHeader colDatabasePropertyName;
        private System.Windows.Forms.ColumnHeader colDatabasePropertyValue;
    }
}

