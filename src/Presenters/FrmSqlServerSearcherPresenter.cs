namespace SQLServerSearcher.Presenters
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

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
            _view.CopyQueryToClipboardToolStripMenuItemClick += DoCopyQueryToClipboardToolStripMenuItemClick;
            _view.CopyNameToClipboardToolStripMenuItemClick += DoCopyNameToClipboardToolStripMenuItemClick;
        }

        private void DoBtnConnectClick(object sender, ConnectEventArgs args)
        {
            if (_view.ShowLoginDialog(args.Server))
            {
                _view.InsertServerIntoCombobox(args.Server);
                try
                {
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
                catch (SqlException sqlEx)
                {
                    _view.ShowErrorDialog(string.Format("Connection to server failed: {0}", sqlEx.Message));
                }
                catch (Exception ex)
                {
                    _view.ShowErrorDialog(string.Format("Connection to server failed: {0}", ex));
                }
            }
        }

        private void DoBtnFindClick(object sender, FindEventArgs args)
        {
            _view.ClearResults();
            _view.ClearObjectInformation();
            var startTime = DateTime.Now;
            int rowCount = 0;
            if (args.LookInTables)
            {
                var tables = _searches.FindTables(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    tables = tables.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0)
                                   .OrderBy(p => p.SchemaName)
                                   .ThenBy(p => p.Name)
                                   .ThenBy(p => p.ColumnName).ToList();
                }
                _view.InsertTableIntoTreeview(tables);
                rowCount += tables.Count;
            }
            if (args.LookInViews)
            {
                var views = _searches.FindViews(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    views = views.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0)
                                 .OrderBy(p => p.SchemaName)
                                 .ThenBy(p => p.Name)
                                 .ThenBy(p => p.ColumnName).ToList();
                }
                _view.InsertViewIntoTreeview(views);
                rowCount += views.Count;
            }
            if (args.LookInIndexes)
            {
                var indexes = _searches.FindIndexes(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    indexes = indexes.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0)
                                     .OrderBy(p => p.TableName)
                                     .ThenBy(p => p.Name)
                                     .ThenBy(p => p.ColumnName).ToList();
                }
                _view.InsertIndexIntoTreeview(indexes);
                rowCount += indexes.Count;
            }
            if (args.LookInStoredProcedures)
            {
                var procedures = _searches.FindProcedures(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    procedures = procedures.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0 || p.Definition.IndexOf(args.FindWhat, StringComparison.Ordinal) >= 0)
                                           .OrderBy(p => p.SchemaName)
                                           .ThenBy(p => p.Name)
                                           .ThenBy(p => p.ParameterName).ToList();
                }
                _view.InsertProcedureIntoTreeview(procedures);
                rowCount += procedures.Count;
            }
            if (args.LookInFunctions)
            {
                var functions = _searches.FindFunctions(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    functions = functions.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0 || p.Definition.IndexOf(args.FindWhat, StringComparison.Ordinal) >= 0)
                                         .OrderBy(p => p.SchemaName)
                                         .ThenBy(p => p.Name)
                                         .ThenBy(p => p.ParameterName).ToList();
                }
                _view.InsertFunctionIntoTreeview(functions);
                rowCount += functions.Count;
            }
            _view.InsertSearchQueryIntoCombobox(args.FindWhat);
            var executionTime = DateTime.Now - startTime;
            _view.SetExecutionTime(executionTime);
            _view.SetLblRowCount(rowCount);
            if (rowCount == 0)
            {
                _view.ShowNoResultsFound();
            }
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

        public void DoCopyQueryToClipboardToolStripMenuItemClick(object sender, FindEventArgs e)
        {
            var sb = new StringBuilder();
            if (e.LookInTables)
            {
                sb.AppendLine(_searches.GetFindTablesSql(e.Database, e.FindWhat));
                sb.AppendLine("GO");
                sb.AppendLine();
            }
            if (e.LookInViews)
            {
                sb.AppendLine(_searches.GetFindViewsSql(e.Database, e.FindWhat));
                sb.AppendLine("GO");
                sb.AppendLine();
            }
            if (e.LookInIndexes)
            {
                sb.AppendLine(_searches.GetFindIndexesSql(e.Database, e.FindWhat));
                sb.AppendLine("GO");
                sb.AppendLine();
            }
            if (e.LookInStoredProcedures)
            {
                sb.AppendLine(_searches.GetFindStoredProceduresSql(e.Database, e.FindWhat));
                sb.AppendLine("GO");
                sb.AppendLine();
            }
            if (e.LookInFunctions)
            {
                sb.AppendLine(_searches.GetFindFunctionsSql(e.Database, e.FindWhat));
                sb.AppendLine("GO");
                sb.AppendLine();
            }
            string sql = sb.ToString();
            sql = Regex.Replace(sql, @"[ \t]+", " ");
            _view.CopyStringToSlipBoard(sql);
        }

        public void DoCopyNameToClipboardToolStripMenuItemClick(object sender, CopyNameEventArgs e)
        {
            _view.CopyStringToSlipBoard(e.Name);
        }
    }
}
