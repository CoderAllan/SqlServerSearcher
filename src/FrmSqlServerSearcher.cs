namespace SQLServerSearcher
{
    using System;
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
            _frmSqlServerSearcherPresenter = new FrmSqlServerSearcherPresenter(this);

            InitializeComponent();

            var eventArgs = _appState.GetFormLocationAndPosition(this);
            DoFormLoad(this, eventArgs);
            if (EnableDisableBtnConnect != null)
            {
                EnableDisableBtnConnect(null, EventArgs.Empty);
            }
            _appState.ReadComboBoxElements(cmbServer, _appState.Servers, (server, i) => cmbServer.Items.Add(server));
        }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<ConnectEventArgs> BtnConnectClick;
        public event EventHandler<FindEventArgs> BtnFindClick;
        public event EventHandler<EventArgs> EnableDisableBtnConnect;

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
            _appState.PersistComboBox(cmbServer, _appState.Servers);
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
                    FindWhat = cmbFindText.Text,
                    MatchCase = chkMatchCase.Checked,
                    LookInTables = chkTables.Checked,
                    LookInViews = chkViews.Checked,
                    LookInStoredProcedures = chkStoredProcedures.Checked,
                    LookInFunctions = chkFunctions.Checked
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
            }
        }

        public bool ShowLoginDialog(string server)
        {
            var frmLogin = new FrmLogin(server, _appState);
            var result = frmLogin.ShowDialog();
            return (result == DialogResult.OK);
        }

        private void cmbServer_TextChanged(object sender, EventArgs e)
        {
            if (EnableDisableBtnConnect != null)
            {
                EnableDisableBtnConnect(sender, e);
            }
        }

        public void InsertServerIntoCombobox(string server)
        {
            cmbServer.Items.Remove(server);
            cmbServer.Items.Insert(0, server);
            cmbServer.SelectedIndex = 0;
        }
    }
}
