namespace SQLServerSearcher.Presenters
{
    using System;
    using System.Data.SqlClient;

    using Model;
    using Views;

    public class FrmLoginPresenter
    {
        private readonly IFrmLogin _view;

        public FrmLoginPresenter(IFrmLogin view)
        {
            _view = view;

            Initialize();
        }

        private void Initialize()
        {
            _view.BtnLoginClick += DoBtnLoginClick;
            _view.BtnCancelClick += DoBtnCancelClick;
            _view.EnableDisableTextBoxes += DoEnableDisableTextBoxes;
        }

        private void DoEnableDisableTextBoxes(object sender, EventArgs e)
        {
            _view.TxtLoginEnabled = _view.RbSqlServerLogon;
            _view.TxtPasswordEnabled = _view.RbSqlServerLogon;
        }

        private void DoBtnLoginClick(object sender, LoginEventArgs e)
        {
            string connectionString;
            if (_view.RbWindowsLogon)
            {
                _view.AppState.LastUsedLoginMethod = ApplicationState.LastUsedLoginMethods.WindowsLogon;
                connectionString = String.Format("Data Source={0};Integrated Security=SSPI;", e.Server);
            }
            else
            {
                _view.AppState.LastUsedLoginMethod = ApplicationState.LastUsedLoginMethods.SqlServerLogon;
                _view.AppState.LastUsedLogin = e.Login;
                connectionString = String.Format("Data Source={0};User ID={1};Password={2}", e.Server, e.Login, e.Password);
            }
            _view.AppState.CurrentConnection = new SqlConnection(connectionString);
            _view.CloseForm();
        }

        private void DoBtnCancelClick(object sender, EventArgs e)
        {
            _view.CloseForm();
        }
    }
}
