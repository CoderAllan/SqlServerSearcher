namespace SQLServerSearcher
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;

    using Model;
    using Model.EventArgs;
    using Presenters;
    using Views;

    public partial class FrmSqlServerSearcher : Form, IBaseForm, IFrmSqlServerSearcher
    {
        #region static void Main()
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmSqlServerSearcher());
        }
        #endregion

        private readonly ApplicationState _appState;
        private readonly BaseFormPresenter _baseFormPresenter;
        private readonly FrmSqlServerSearcherPresenter _frmSqlServerSearcherPresenter;

        public FrmSqlServerSearcher()
        {
            _appState = ApplicationState.ReadApplicationState();
            _baseFormPresenter = new BaseFormPresenter(this);
            var searches = new DAL.Searches(_appState);
            _frmSqlServerSearcherPresenter = new FrmSqlServerSearcherPresenter(this, searches);

            InitializeComponent();

            var eventArgs = _appState.GetFormLocationAndPosition(this);
            DoFormLoad(this, eventArgs);
            if (EnableDisableBtnConnect != null)
            {
                EnableDisableBtnConnect(null, EventArgs.Empty);
            }
            _appState.ReadComboBoxElements(cmbServer, _appState.Servers, (server, i) => cmbServer.Items.Add(server));
            _appState.ReadComboBoxElements(cmbFindText, _appState.PreviousSearches, (query, i) => cmbFindText.Items.Add(query));
            chkTables.Checked = _appState.LookInTables;
            chkIndexes.Checked = _appState.LookInIndexes;
            chkViews.Checked = _appState.LookInViews;
            chkStoredProcedures.Checked = _appState.LookInStoredProcedures;
            chkFunctions.Checked = _appState.LookInFunctions;
            chkMatchCase.Checked = _appState.MatchCase;
            if (_appState.NameColumnWith > 0)
            {
                colName.Width = _appState.NameColumnWith;
            }
            if (_appState.ValueColumnWith > 0)
            {
                colValue.Width = _appState.ValueColumnWith;
            }
            if (_appState.ServerPropertyNameColumnWith > 0)
            {
                colServerPropertyName.Width = _appState.ServerPropertyNameColumnWith;
            }
            if (_appState.ServerPropertyValueColumnWith > 0)
            {
                colServerPropertyValue.Width = _appState.ServerPropertyValueColumnWith;
            }
            tvResults.NodeMouseClick += (sender, args) => tvResults.SelectedNode = args.Node; // To make sure that when rightclicking a node it is selected. Solution found here: http://stackoverflow.com/questions/4784258/right-click-select-on-net-treenode
            EnableDisableControls();
        }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<ConnectEventArgs> BtnConnectClick;
        public event EventHandler<FindEventArgs> BtnFindClick;
        public event EventHandler<EventArgs> EnableDisableBtnConnect;
        public event EventHandler<TreeviewNodeClickEventArgs> TreeviewNodeClick;
        public event EventHandler<FindEventArgs> CopyQueryToClipboardToolStripMenuItemClick;
        public event EventHandler<CopyNameEventArgs> CopyNameToClipboardToolStripMenuItemClick;
        public event EventHandler<CopyInformationEventArgs> CopyInformationToClipboardToolStripMenuItemClick;
        public event EventHandler<EventArgs> CopyServerInformationClick;
        public event EventHandler<CopyListToClipboardEventArgs> CopyListToClipboardToolStripMenuItemClick;

        public ApplicationState AppState
        {
            get { return _appState; }
        }

        public bool BtnConnectEnabled
        {
            get { return btnConnect.Enabled; }
            set { btnConnect.Enabled = value; }
        }

        public string CmbServerText
        {
            get { return cmbServer.Text; }
        }

        public void CloseApplication()
        {
            _appState.PersistFormLocationAndPosition(this);
            _appState.MatchCase = chkMatchCase.Checked;
            _appState.LookInTables = chkTables.Checked;
            _appState.LookInViews = chkViews.Checked;
            _appState.LookInStoredProcedures = chkStoredProcedures.Checked;
            _appState.LookInFunctions = chkFunctions.Checked;
            _appState.LookInIndexes = chkIndexes.Checked;
            _appState.NameColumnWith = colName.Width;
            _appState.ValueColumnWith = colValue.Width;
            _appState.ServerPropertyNameColumnWith = colServerPropertyName.Width;
            _appState.ServerPropertyValueColumnWith = colServerPropertyValue.Width;
            _appState.PersistComboBox(cmbServer, _appState.Servers);
            if (cmbDatabase.SelectedItem != null)
            {
                _appState.LastUsedBatabase = cmbDatabase.SelectedItem.ToString();
            }
            _appState.PersistComboBox(cmbFindText, _appState.PreviousSearches);
            ApplicationState.WriteApplicationState(_appState);
        }

        private void FrmSqlServerSearcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseApplication();
        }

        private void DoFind()
        {
            if (BtnFindClick != null)
            {
                var findArgs = new FindEventArgs
                {
                    Database = cmbDatabase.SelectedItem.ToString(),
                    FindWhat = cmbFindText.Text,
                    MatchCase = chkMatchCase.Checked,
                    LookInTables = chkTables.Checked,
                    LookInViews = chkViews.Checked,
                    LookInStoredProcedures = chkStoredProcedures.Checked,
                    LookInFunctions = chkFunctions.Checked,
                    LookInIndexes = chkIndexes.Checked
                };
                BtnFindClick(null, findArgs);
                _appState.PersistComboBox(cmbFindText, _appState.PreviousSearches);
                tvResults.Nodes["TablesNode"].EnsureVisible();
                tvResults.Focus();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            DoFind();
        }

        private void cmbFindText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoFind();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (BtnConnectClick != null)
            {
                var connectEventArgs = new ConnectEventArgs
                {
                    Server = cmbServer.Text,
                };
                BtnConnectClick(sender, connectEventArgs);
                if (!string.IsNullOrEmpty(_appState.LastUsedBatabase))
                {
                    int dbId = cmbDatabase.Items.IndexOf(_appState.LastUsedBatabase);
                    if (dbId >= 0)
                    {
                        cmbDatabase.SelectedIndex = dbId;
                    }
                }
                EnableDisableControls();
            }
        }

        public bool ShowLoginDialog(string server)
        {
            var frmLogin = new FrmLogin(server, _appState);
            var result = frmLogin.ShowDialog();
            return (result == DialogResult.OK);
        }

        private void ShowViewSourceDialog(string definition)
        {
            var frmViewSource = new FrmViewSource(_appState, definition);
            frmViewSource.ShowDialog();
        }

        public void ShowNoResultsFound()
        {
            MessageBox.Show(@"The search returned no results.", @"No results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowErrorDialog(string text)
        {
            MessageBox.Show(text, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void EnableDisableControls()
        {
            var enabled = _appState.CurrentConnection != null && _appState.CurrentConnection.State == ConnectionState.Open;
            btnFind.Enabled = enabled;
            cmbDatabase.Enabled = enabled;
            cmbFindText.Enabled = enabled;
            chkFunctions.Checked = enabled;
            chkIndexes.Enabled = enabled;
            chkTables.Enabled = enabled;
            chkViews.Enabled = enabled;
            chkStoredProcedures.Enabled = enabled;
            chkFunctions.Enabled = enabled;
            chkMatchCase.Enabled = enabled;
            tvResults.Enabled = enabled;
        }

        private void cmbServer_TextChanged(object sender, EventArgs e)
        {
            if (EnableDisableBtnConnect != null)
            {
                EnableDisableBtnConnect(sender, e);
            }
        }

        private void cmbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetLblDatabase();
        }

        public void InsertServerIntoCombobox(string server)
        {
            cmbServer.Items.Remove(server);
            cmbServer.Items.Insert(0, server);
            cmbServer.SelectedIndex = 0;
        }

        public void InsertDatabaseIntoCombobox(string database)
        {
            cmbDatabase.Items.Add(database);
            cmbDatabase.SelectedIndex = 0;
        }

        public void InsertSearchQueryIntoCombobox(string searchQuery)
        {
            cmbFindText.Items.Remove(searchQuery);
            cmbFindText.Items.Insert(0, searchQuery);
            cmbFindText.SelectedIndex = 0;
            if (cmbFindText.Items.Count > 20)
            {
                cmbFindText.Items.RemoveAt(20);
            }
        }

        public void SetLblServerName()
        {
            tsLblServer.Text = cmbServer.SelectedItem.ToString();
        }

        public void SetLblDatabase()
        {
            var databaseName = cmbDatabase.SelectedItem == null ? "N/A" : cmbDatabase.SelectedItem.ToString();
            tsLblDatabase.Text = databaseName;
        }

        public void SetExecutionTime(TimeSpan executionTime)
        {
            tsLblExecutionTime.Text = executionTime.ToString(@"hh\:mm\:ss\.fff");
        }

        public void SetLblRowCount(int rowCount)
        {
            tsLblRowCount.Text = @"Rows: " + rowCount.ToString(CultureInfo.InvariantCulture);
        }


        public void ClearResults()
        {
            var tableNodes = tvResults.Nodes["TablesNode"];
            tableNodes.Nodes.Clear();
            var viewsNodes = tvResults.Nodes["ViewsNode"];
            viewsNodes.Nodes.Clear();
            var storedProcedureNodes = tvResults.Nodes["StoredProceduresNode"];
            storedProcedureNodes.Nodes.Clear();
            var functionsNodes = tvResults.Nodes["FunctionsNode"];
            functionsNodes.Nodes.Clear();
            var indexesNodes = tvResults.Nodes["IndexesNode"];
            indexesNodes.Nodes.Clear();
        }

        public void ClearObjectInformation()
        {
            lvObjectInformation.Items.Clear();
        }

        private static void AddNewResultNode(string nodeName, TreeNode resultNode, object result)
        {
            if (!resultNode.Nodes.ContainsKey(nodeName))
            {
                var newResultNode = new TreeNode
                {
                    Name = nodeName,
                    Text = nodeName,
                    Tag = result
                };
                resultNode.Nodes.Add(newResultNode);
            }
        }

        private static string FormatNodeName(string part1, string part2, string part3){
            var nodeName = !string.IsNullOrEmpty(part3) ? string.Format("{0}.{1}.{2}", part1, part2, part3) : string.Format("{0}.{1}", part1, part2);
            return nodeName;
        }

        private void AddObjectToListView(ListView listView, IDatabaseObject dbObject)
        {
            listView.BeginUpdate();
            listView.Items.Clear();
            foreach (var row in dbObject.ToArrayList())
            {
                var item = new ListViewItem
                {
                    Text = row[0]
                };
                item.SubItems.Add(row[1]);
                listView.Items.Add(item);
            }
            listView.EndUpdate();            
        }

        public void ShowServerInfo(ServerInfo serverInfo)
        {
            AddObjectToListView(lvServerProperties, serverInfo);
        }

        public void InsertTableIntoTreeview(List<Table> tables)
        {
            if (tables != null && tables.Count > 0)
            {
                tvResults.BeginUpdate();
                var tableNodes = tvResults.Nodes["TablesNode"];
                foreach (var table in tables)
                {
                    var nodeName = FormatNodeName(table.SchemaName, table.Name, table.ColumnName);
                    AddNewResultNode(nodeName, tableNodes, table);
                }
                tableNodes.ExpandAll();
                tvResults.EndUpdate();
            }
        }

        public void ShowTableInfo(Table table)
        {
            AddObjectToListView(lvObjectInformation, table);
        }

        public void InsertViewIntoTreeview(List<Model.View> views)
        {
            if (views != null && views.Count > 0)
            {
                tvResults.BeginUpdate();
                var viewsNodes = tvResults.Nodes["ViewsNode"];
                foreach (var view in views)
                {
                    var nodeName = FormatNodeName(view.SchemaName, view.Name, view.ColumnName);
                    AddNewResultNode(nodeName, viewsNodes, view);
                }
                viewsNodes.ExpandAll();
                tvResults.EndUpdate();
            }
        }

        public void ShowViewInfo(Model.View view)
        {
            AddObjectToListView(lvObjectInformation, view);
        }

        public void InsertIndexIntoTreeview(List<Index> indexes)
        {
            if (indexes != null && indexes.Count > 0)
            {
                tvResults.BeginUpdate();
                var indexesNodes = tvResults.Nodes["IndexesNode"];
                foreach (var index in indexes)
                {
                    var nodeName = FormatNodeName(index.TableName, index.Name, index.ColumnName);
                    AddNewResultNode(nodeName, indexesNodes, index);
                }
                indexesNodes.ExpandAll();
                tvResults.EndUpdate();
            }
        }

        public void ShowIndexInfo(Index index)
        {
            AddObjectToListView(lvObjectInformation, index);
        }

        public void InsertProcedureIntoTreeview(List<Procedure> procedures)
        {
            if (procedures != null && procedures.Count > 0)
            {
                tvResults.BeginUpdate();
                var proceduresNode = tvResults.Nodes["StoredProceduresNode"];
                foreach (var procedure in procedures)
                {
                    var nodeName = FormatNodeName(procedure.SchemaName, procedure.Name, procedure.ParameterName);
                    AddNewResultNode(nodeName, proceduresNode, procedure);
                }
                proceduresNode.ExpandAll();
                tvResults.EndUpdate();
            }
        }

        public void ShowProcedureInfo(Procedure procedure)
        {
            AddObjectToListView(lvObjectInformation, procedure);
        }

        public void InsertFunctionIntoTreeview(List<Function> functions)
        {
            if (functions != null && functions.Count > 0)
            {
                tvResults.BeginUpdate();
                var functionsNode = tvResults.Nodes["FunctionsNode"];
                foreach (var function in functions)
                {
                    var nodeName = FormatNodeName(function.SchemaName, function.Name, function.ParameterName);
                    AddNewResultNode(nodeName, functionsNode, function);
                }
                functionsNode.ExpandAll();
                tvResults.EndUpdate();
            }
        }

        public void ShowFunctionInfo(Function function)
        {
            AddObjectToListView(lvObjectInformation, function);
        }

        private void tvResults_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (TreeviewNodeClick != null)
            {
                var parentNode = e.Node.Parent;
                if (parentNode == null)
                {
                    ClearObjectInformation();
                    return;
                }
                var treeviewNodeClickEventrgs = new TreeviewNodeClickEventArgs
                {
                    ParentNodeName = parentNode.Name,
                    NodeTag = e.Node.Tag
                };
                TreeviewNodeClick(sender, treeviewNodeClickEventrgs);
            }
        }

        private void tvResults_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoShowViewSourceDialog();
                return;
            }
            if (TreeviewNodeClick != null)
            {
                var selectedNode = tvResults.SelectedNode;
                if (selectedNode != null)
                {
                    var parentNode = selectedNode.Parent;
                    if (parentNode == null)
                    {
                        ClearObjectInformation();
                        return;
                    }
                    var treeviewNodeClickEventrgs = new TreeviewNodeClickEventArgs
                    {
                        ParentNodeName = parentNode.Name,
                        NodeTag = selectedNode.Tag
                    };
                    TreeviewNodeClick(sender, treeviewNodeClickEventrgs);
                }
            }
        }

        private void cmsResults_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsNoResultAdded())
            {
                tsmViewSource.Enabled = false;
                tsmFindAllReferences.Enabled = false;
                tsmCopyListToClipboardToolStripMenuItem.Enabled = false;
                tsmCopyNameToClipboardToolStripMenuItem.Enabled = false;
                tsmCopyInformationToClipboardToolStripMenuItem.Enabled = false;
                tsmCopyQueryToClipboardToolStripMenuItem.Enabled = cmbFindText.SelectedItem != null;
            }
            else
            {
                var selectedNode = tvResults.SelectedNode;
                if (IsTopNodeSelected(selectedNode))
                {
                    tsmViewSource.Enabled = false;
                    tsmFindAllReferences.Enabled = false;
                    tsmCopyListToClipboardToolStripMenuItem.Enabled = true;
                    tsmCopyNameToClipboardToolStripMenuItem.Enabled = false;
                    tsmCopyInformationToClipboardToolStripMenuItem.Enabled = false;
                }
                else
                {
                    tsmViewSource.Enabled = selectedNode.Parent.Name.Equals("StoredProceduresNode") ||
                                            selectedNode.Parent.Name.Equals("FunctionsNode") || 
                                            selectedNode.Parent.Name.Equals("ViewsNode");
                    tsmFindAllReferences.Enabled = true;
                    tsmCopyListToClipboardToolStripMenuItem.Enabled = false;
                    tsmCopyNameToClipboardToolStripMenuItem.Enabled = true;
                    tsmCopyInformationToClipboardToolStripMenuItem.Enabled = true;
                }
            }
        }

        private void cmsObjectInformation_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsNoResultAdded())
            {
                tsmCopyInformationToClipboardToolStripMenuItem2.Enabled = false;
            }
            else
            {
                var selectedNode = tvResults.SelectedNode;
                tsmCopyInformationToClipboardToolStripMenuItem2.Enabled = !IsTopNodeSelected(selectedNode);
            }
        }

        private bool IsNoResultAdded()
        {
            return tvResults.Nodes[0].Nodes.Count == 0 &&
                   tvResults.Nodes[1].Nodes.Count == 0 &&
                   tvResults.Nodes[2].Nodes.Count == 0 &&
                   tvResults.Nodes[3].Nodes.Count == 0 &&
                   tvResults.Nodes[4].Nodes.Count == 0;
        }

        private bool IsTopNodeSelected(TreeNode selectedNode)
        {
            return selectedNode != null &&
                   (selectedNode.Name.Equals("TablesNode") ||
                    selectedNode.Name.Equals("ViewsNode") ||
                    selectedNode.Name.Equals("IndexesNode") ||
                    selectedNode.Name.Equals("StoredProceduresNode") ||
                    selectedNode.Name.Equals("FunctionsNode"));
        }

        private void tsmFindAllReferences_Click(object sender, EventArgs e)
        {
            if (BtnFindClick != null)
            {
                var selectedNode = tvResults.SelectedNode;
                if (selectedNode != null && selectedNode.Tag != null && selectedNode.Parent != null)
                {
                    string name;
                    if (selectedNode.Parent.Name.Equals("TablesNode") || selectedNode.Parent.Name.Equals("ViewsNode"))
                    {
                        var tableObject = (TableObject)selectedNode.Tag;
                        name = !string.IsNullOrEmpty(tableObject.ColumnName) ? tableObject.ColumnName : tableObject.Name;
                    }
                    else
                    {
                        if (selectedNode.Parent.Name.Equals("TablesNode") || selectedNode.Parent.Name.Equals("ViewsNode"))
                        {
                            var procedureObject = (ProcedureObject)selectedNode.Tag;
                            name = !string.IsNullOrEmpty(procedureObject.ParameterName) ? procedureObject.ParameterName : procedureObject.Name;
                        }
                        else
                        {
                            var databaseObject = (IDatabaseObject)selectedNode.Tag;
                            name = databaseObject.Name;
                        }
                    }

                    var findArgs = new FindEventArgs
                    {
                        Database = cmbDatabase.SelectedItem.ToString(),
                        FindWhat = name,
                        MatchCase = false,
                        LookInTables = true,
                        LookInViews = true,
                        LookInStoredProcedures = true,
                        LookInFunctions = true,
                        LookInIndexes = true
                    };
                    BtnFindClick(null, findArgs);
                    _appState.PersistComboBox(cmbFindText, _appState.PreviousSearches);
                    tvResults.Focus();
                }
            }
        }

        private void DoShowViewSourceDialog()
        {
            var selectedNode = tvResults.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                var parentNode = selectedNode.Parent;
                if (parentNode == null)
                {
                    ClearObjectInformation();
                    return;
                }
                if (selectedNode.Parent.Name.Equals("StoredProceduresNode") || selectedNode.Parent.Name.Equals("FunctionsNode"))
                {
                    var procedureObject = (ProcedureObject)selectedNode.Tag;
                    ShowViewSourceDialog(procedureObject.Definition);
                }
                else if (selectedNode.Parent.Name.Equals("ViewsNode"))
                {
                    var view = (Model.View)selectedNode.Tag;
                    ShowViewSourceDialog(view.Definition);
                }

            }
        }

        private void tsmViewSource_Click(object sender, EventArgs e)
        {
            DoShowViewSourceDialog();
        }

        private void tsmCopyListToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CopyListToClipboardToolStripMenuItemClick != null)
            {
                var selectedNode = tvResults.SelectedNode;
                if (selectedNode != null && selectedNode.Parent == null)
                {
                    var copyListToClipboard = new CopyListToClipboardEventArgs
                    {
                        NameList = new List<string>()
                    };
                    foreach (TreeNode node in selectedNode.Nodes)
                    {
                        var dbObject = (IDatabaseObject) node.Tag;
                        var nameValue = dbObject.ToArrayList().FirstOrDefault(p => p[0].Equals("Name:"));
                        if (nameValue != null)
                        {
                            copyListToClipboard.NameList.Add(nameValue[1]);
                        }
                    }
                    CopyListToClipboardToolStripMenuItemClick(sender, copyListToClipboard);
                }
            }
        }

        private void tsmCopyNameToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CopyNameToClipboardToolStripMenuItemClick != null)
            {
                var selectedNode = tvResults.SelectedNode;
                if (selectedNode != null && selectedNode.Tag != null && selectedNode.Parent != null)
                {
                    var dbObject = (IDatabaseObject) selectedNode.Tag;
                    var copyNameArgs = new CopyNameEventArgs
                    {
                        Name = dbObject.Name
                    };
                    CopyNameToClipboardToolStripMenuItemClick(sender, copyNameArgs);
                }
            }
        }

        private void tsmCopyInformationToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CopyInformationToClipboardToolStripMenuItemClick != null)
            {
                var selectedNode = tvResults.SelectedNode;
                if (selectedNode != null && selectedNode.Tag != null && selectedNode.Parent != null)
                {
                    var dbObject = (IDatabaseObject)selectedNode.Tag;
                    var copyInformationEventArgs = new CopyInformationEventArgs()
                    {
                        DatabaseObject = dbObject
                    };
                    CopyInformationToClipboardToolStripMenuItemClick(sender, copyInformationEventArgs);
                }
            }
        }

        private void tsmCopyQueryToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CopyQueryToClipboardToolStripMenuItemClick != null)
            {
                var findArgs = new FindEventArgs
                {
                    Database = cmbDatabase.SelectedItem.ToString(),
                    FindWhat = cmbFindText.Text,
                    MatchCase = chkMatchCase.Checked,
                    LookInTables = chkTables.Checked,
                    LookInViews = chkViews.Checked,
                    LookInStoredProcedures = chkStoredProcedures.Checked,
                    LookInFunctions = chkFunctions.Checked,
                    LookInIndexes = chkIndexes.Checked
                };
                CopyQueryToClipboardToolStripMenuItemClick(sender, findArgs);
            }
        }

        private void tsmCopyServerInformation_Click(object sender, EventArgs e)
        {
            if (CopyServerInformationClick != null)
            {
                CopyServerInformationClick(sender, EventArgs.Empty);
            }
        }

        public void CopyStringToSlipBoard(string text)
        {
            Clipboard.SetText(text);
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int wmKeydown = 0x100;
            const int wmSyskeydown = 0x104;
            if ((msg.Msg == wmKeydown) || (msg.Msg == wmSyskeydown))
            {
                switch (keyData)
                {
                    case Keys.F3:
                        DoFind();
                        break;
                    case Keys.F12:
                        DoShowViewSourceDialog();
                        break;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmAboutBox = new FrmAbout();
            frmAboutBox.ShowDialog();
        }

        private void pbSettings_MouseUp(object sender, MouseEventArgs e)
        {
            var settings = (PictureBox)sender;
            var ptLowerLeft = new Point(0, settings.Height);
            ptLowerLeft = settings.PointToScreen(ptLowerLeft);
            cmsSettings.Show(ptLowerLeft);
        }

        private void changelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmChangelog = new FrmChangelog(_appState);
            frmChangelog.ShowDialog();
        }
    }
}
