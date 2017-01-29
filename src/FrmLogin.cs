namespace SQLServerSearcher
{
    using System;
    using System.Windows.Forms;

    using Model;
    using Presenters;
    using Views;

    public partial class FrmLogin : Form, IBaseForm, IFrmLogin
    {
        private readonly ApplicationState _appState;
        private readonly BaseFormPresenter _baseFormPresenter;
        private readonly FrmLoginPresenter _frmLoginPresenter;

        private readonly string _server;

        public FrmLogin(string server, ApplicationState appState)
        {
            _appState = appState;
            _baseFormPresenter = new BaseFormPresenter(this);
            _frmLoginPresenter = new FrmLoginPresenter(this);

            InitializeComponent();

            var eventArgs = _appState.GetFormLocationAndPosition(this);
            DoFormLoad(this, eventArgs);

            _server = server;
            rbWindowsLogon.Checked = _appState.LastUsedLoginMethod == ApplicationState.LastUsedLoginMethods.WindowsLogon;
            rbSqlServerLogon.Checked = _appState.LastUsedLoginMethod == ApplicationState.LastUsedLoginMethods.SqlServerLogon;
        }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<LoginEventArgs> BtnLoginClick;
        public event EventHandler<EventArgs> BtnCancelClick;
        public event EventHandler<EventArgs> EnableDisableTextBoxes;

        public ApplicationState AppState
        {
            get { return _appState; }
        }

        public bool RbWindowsLogon
        {
            get { return rbWindowsLogon.Checked; } 
            set { rbWindowsLogon.Checked = value; }
        }

        public bool RbSqlServerLogon
        {
            get { return rbSqlServerLogon.Checked; }
            set { rbSqlServerLogon.Checked = value; }
        }

        public bool TxtLoginEnabled
        {
            get { return txtLogin.Enabled; }
            set { txtLogin.Enabled = value; }
        }

        public bool TxtPasswordEnabled
        {
            get { return txtPassword.Enabled; }
            set { txtPassword.Enabled = value; }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            if (EnableDisableTextBoxes != null)
            {
                EnableDisableTextBoxes(sender, e);
            }
        }
        
        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            _appState.PersistFormLocationAndPosition(this);
        }

        private void rbWindowsLogon_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableDisableTextBoxes != null)
            {
                EnableDisableTextBoxes(sender, e);
            }
        }

        private void rbSqlServerLogon_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableDisableTextBoxes != null)
            {
                EnableDisableTextBoxes(sender, e);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (BtnLoginClick != null)
            {
                var loginAgs = new LoginEventArgs
                {
                    Server = _server,
                    Login = txtLogin.Text,
                    Password = txtPassword.Text,
                    SQLServerLogin = rbSqlServerLogon.Checked,
                    WindowsLogin = rbWindowsLogon.Checked
                };
                BtnLoginClick(sender, loginAgs);
            }
        }

        public void CloseForm()
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (BtnCancelClick != null)
            {
                BtnCancelClick(sender, e);
            }
        }
    }
}
