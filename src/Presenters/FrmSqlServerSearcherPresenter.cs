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
                _view.SetLblServerName();
            }
        }

        private void DoBtnFindClick(object sender, FindEventArgs args)
        {
            _view.ClearResults();
            var startTime = DateTime.Now;
            int rowCount = 0;
            if (args.LookInTables)
            {
                var tables = _searches.FindTables(args.Database, args.FindWhat);
                _view.InsertTableIntoTreeview(tables);
                rowCount += tables.Count;
            }
            if (args.LookInViews)
            {
                var views = _searches.FindViews(args.Database, args.FindWhat);
                _view.InsertViewIntoTreeview(views);
                rowCount += views.Count;
            }
            if (args.LookInIndexes)
            {
                var indexes = _searches.FindIndexes(args.Database, args.FindWhat);
                _view.InsertIndexIntoTreeview(indexes);
                rowCount += indexes.Count;
            }
            if (args.LookInStoredProcedures)
            {
                var procedures = _searches.FindProcedures(args.Database, args.FindWhat);
                _view.InsertProcedureIntoTreeview(procedures);
                rowCount += procedures.Count;
            }
            if (args.LookInFunctions)
            {
                var functions = _searches.FindFunctions(args.Database, args.FindWhat);
                _view.InsertFunctionIntoTreeview(functions);
                rowCount += functions.Count;
            }
            _view.InsertSearchQueryIntoCombobox(args.FindWhat);
            var executionTime = DateTime.Now - startTime;
            _view.SetExecutionTime(executionTime);
            _view.SetLblRowCount(rowCount);
        }

        private void DoEnableDisableBtnConnect(object sender, EventArgs e)
        {
            _view.BtnConnectEnabled = !String.IsNullOrEmpty(_view.CmbServerText);
        }

    }
}
