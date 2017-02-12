namespace SQLServerSearcher.Presenters
{
    using System;
    using System.Linq;

    using DAL;
    using Model;
    using Model.EventArgs;
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
            _view.TreeviewNodeClick += DoTreeviewNodeClick;
        }

        private void DoBtnConnectClick(object sender, ConnectEventArgs args)
        {
            if (_view.ShowLoginDialog(args.Server))
            {
                _view.InsertServerIntoCombobox(args.Server);
                _view.AppState.CurrentConnection.Open();
                var databases = _searches.GetDatabases();
                foreach (var database in databases.OrderBy(p => p.Name))
                {
                    _view.InsertDatabaseIntoCombobox(database.Name);
                }
                var serverInfo = _searches.GetServerInfo();
                _view.ShowServerInfo(serverInfo);
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
                if (args.MatchCase)
                {
                    tables = tables.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0).ToList();
                }
                _view.InsertTableIntoTreeview(tables);
                rowCount += tables.Count;
            }
            if (args.LookInViews)
            {
                var views = _searches.FindViews(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    views = views.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0).ToList();
                }
                _view.InsertViewIntoTreeview(views);
                rowCount += views.Count;
            }
            if (args.LookInIndexes)
            {
                var indexes = _searches.FindIndexes(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    indexes = indexes.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0).ToList();
                }
                _view.InsertIndexIntoTreeview(indexes);
                rowCount += indexes.Count;
            }
            if (args.LookInStoredProcedures)
            {
                var procedures = _searches.FindProcedures(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    procedures = procedures.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0 || p.Definition.IndexOf(args.FindWhat, StringComparison.Ordinal) >= 0).ToList();
                }
                _view.InsertProcedureIntoTreeview(procedures);
                rowCount += procedures.Count;
            }
            if (args.LookInFunctions)
            {
                var functions = _searches.FindFunctions(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    functions = functions.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0 || p.Definition.IndexOf(args.FindWhat, StringComparison.Ordinal) >= 0).ToList();
                }
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

        public void DoTreeviewNodeClick(object sender, TreeviewNodeClickEventArgs args)
        {
            switch (args.ParentNodeName)
            {
                case "ViewsNode":
                    var view = (View)args.NodeTag;
                    _view.ShowViewInfo(view);
                    break;
                case "TablesNode":
                    var table = (Table)args.NodeTag;
                    _view.ShowTableInfo(table);
                    break;
                case "IndexesNode":
                    var index = (Index)args.NodeTag;
                    _view.ShowIndexInfo(index);
                    break;
                case "StoredProceduresNode":
                    var procedure = (Procedure)args.NodeTag;
                    _view.ShowProcedureInfo(procedure);
                    break;
                case "FunctionsNode":
                    var function = (Function)args.NodeTag;
                    _view.ShowFunctionInfo(function);
                    break;
                default:
                    return;
            }
        }
    }
}
