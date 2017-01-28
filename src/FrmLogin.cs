namespace SQLServerSearcher
{
    using System;
    using System.Drawing;
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

            var eventArgs = new BaseFormEventArgs
            {
                Height = _appState.FrmSqlServerSearcherHeight,
                Width = _appState.FrmSqlServerSearcherWidth,
                Location = new Point(_appState.FrmSqlServerSearcherPosX, _appState.FrmSqlServerSearcherPosY)
            };
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

        }
    }
}
