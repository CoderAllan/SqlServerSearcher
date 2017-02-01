namespace SQLServerSearcher.Presenters
{
    using System;
    using System.Data;

    using Model;
    using Views;

    public class FrmSqlServerSearcherPresenter
    {
        private readonly IFrmSqlServerSearcher _view;

        public FrmSqlServerSearcherPresenter(IFrmSqlServerSearcher view)
        {
            _view = view;

            Initialize();
        }

        private void Initialize()
        {
            _view.BtnFindClick += DoBtnFindClick;
            _view.BtnConnectClick += DoBtnConnectClick;
            _view.EnableDisableBtnConnect += DoEnableDisableBtnConnect;
        }

        private void DoBtnConnectClick(object sender, ConnectEventArgs args)
        {
            if (_view.ShowLoginDialog(args.Server))
            {
                _view.InsertServerIntoCombobox(args.Server);
                _view.AppState.CurrentConnection.Open();
                _view.SetLblServerVersion(string.Format("Server version: {0}", _view.AppState.CurrentConnection.ServerVersion));
                DataTable databases = _view.AppState.CurrentConnection.GetSchema("Databases");
                foreach (DataRow database in databases.Rows)
                {
                    var databaseName = database.Field<String>("database_name");
                    _view.InsertDatabaseIntoCombobox(databaseName);
                    short dbID = database.Field<short>("dbid");
                    DateTime creationDate = database.Field<DateTime>("create_date");
                }
            }
        }

        private void DoBtnFindClick(object sender, FindEventArgs args)
        {
            
        }

        private void DoEnableDisableBtnConnect(object sender, EventArgs e)
        {
            _view.BtnConnectEnabled = !String.IsNullOrEmpty(_view.CmbServerText);
        }

    }
}
