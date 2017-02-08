namespace SQLServerSearcher
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Windows.Forms;

    using Model;
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
            EnableDisableControls();
        }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<ConnectEventArgs> BtnConnectClick;
        public event EventHandler<FindEventArgs> BtnFindClick;
        public event EventHandler<EventArgs> EnableDisableBtnConnect;
        public event EventHandler<TreeviewNodeClickEventArgs> TreeviewNodeClick;

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

        private void btnFind_Click(object sender, EventArgs e)
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
                BtnFindClick(sender, findArgs);
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

        public void SetLblServerVersion(string text)
        {
            lblServerVersion.Text = text;
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
            tsLblRowCount.Text = @"Rows: " + rowCount.ToString();
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

        private void AddObjectToListView(IDatabaseObject dbObject)
        {
            lvObjectInformation.BeginUpdate();
            lvObjectInformation.Items.Clear();
            foreach (var row in dbObject.ToArrayList())
            {
                var item = new ListViewItem
                {
                    Text = row[0]
                };
                item.SubItems.Add(row[1]);
                lvObjectInformation.Items.Add(item);
            }
            lvObjectInformation.EndUpdate();            
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
            AddObjectToListView(table);
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
            AddObjectToListView(view);
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
            AddObjectToListView(index);
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
            AddObjectToListView(procedure);
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
            AddObjectToListView(function);
        }

        private void tvResults_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (TreeviewNodeClick != null)
            {
                var parentNode = e.Node.Parent;
                if (parentNode == null)
                {
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
    }
}
