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

        public FrmLogin()
        {
            _appState = ApplicationState.ReadApplicationState();
            _baseFormPresenter = new BaseFormPresenter(this);

            InitializeComponent();

            var eventArgs = _appState.GetFormLocationAndPosition(this);
            DoFormLoad(this, eventArgs);
        }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<LoginEventArgs> BtnLoginClick;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (BtnLoginClick != null)
            {
                var loginAgs = new LoginEventArgs
                {
                    Login = txtLogin.Text,
                    Password = txtPassword.Text,
                    SQLServerLogin = rbSqlServerLogon.Checked,
                    WindowsLogin = rbWindowsLogon.Checked
                };
                BtnLoginClick(sender, loginAgs);
            }
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            _appState.PersistFormLocationAndPosition(this);
        }
    }
}
