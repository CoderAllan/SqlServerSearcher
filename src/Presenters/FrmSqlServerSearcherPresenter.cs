namespace SQLServerSearcher.Presenters
{
    using System;
    using System.Linq;

    using DAL;
    using Model;
    using Views;

    public class FrmSqlServerSearcherPresenter
    {
        private readonly IFrmSqlServerSearcher _view;
        private readonly ISearches _searches;

        public FrmSqlServerSearcherPresenter(IFrmSqlServerSearcher view, ISearches searches)
        {
            _view = view;
            _searches = searches;

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
                var databases = _searches.GetDatabases();
                foreach (var database in databases.OrderBy(p => p.Name))
                {
                    _view.InsertDatabaseIntoCombobox(database.Name);
                }
            }
        }

        private void DoBtnFindClick(object sender, FindEventArgs args)
        {
            _view.ClearResults();
            if (args.LookInTables)
            {
                var tables = _searches.FindTables(args.Database, args.FindWhat);
                _view.InsertTableIntoTreeview(tables);
            }
            if (args.LookInViews)
            {
                var views = _searches.FindViews(args.Database, args.FindWhat);
                _view.InsertViewIntoTreeview(views);
            }
            if (args.LookInIndexes)
            {
                var indexes = _searches.FindIndexes(args.Database, args.FindWhat);
                _view.InsertIndexIntoTreeview(indexes);
            }
            if (args.LookInStoredProcedures)
            {
                var procedures = _searches.FindProcedures(args.Database, args.FindWhat);
                _view.InsertProcedureIntoTreeview(procedures);
            }
            _view.InsertSearchQueryIntoCombobox(args.FindWhat);
        }

        private void DoEnableDisableBtnConnect(object sender, EventArgs e)
        {
            _view.BtnConnectEnabled = !String.IsNullOrEmpty(_view.CmbServerText);
        }

    }
}
