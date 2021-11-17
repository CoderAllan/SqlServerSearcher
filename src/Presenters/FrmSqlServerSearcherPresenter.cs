namespace SQLServerSearcher.Presenters
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using DAL;
    using DAL.Contracts;
    using Model;
    using Model.EventArgs;
    using Views;
    using Index = Model.Index;

    public class FrmSqlServerSearcherPresenter
    {
        private readonly IFrmSqlServerSearcher _view;
        private readonly IServer _server;
        private readonly IDatabases _databases;
        private readonly ITables _tables;
        private readonly IViews _views;
        private readonly IIndexes _indexes;
        private readonly IStoredProcedures _storedProcedures;
        private readonly IFunctions _functions;

        public FrmSqlServerSearcherPresenter(IFrmSqlServerSearcher view, IServer server, IDatabases databases, ITables tables, IViews views, IIndexes indexes, IStoredProcedures storedProcedures, IFunctions functions)
        {
            _view = view;
            _server = server;
            _databases = databases;
            _tables = tables;
            _views = views;
            _indexes = indexes;
            _storedProcedures = storedProcedures;
            _functions = functions;

            Initialize();
        }
        public FrmSqlServerSearcherPresenter(IFrmSqlServerSearcher view) : this(
            view,
            new Server(view.AppState),
            new Databases(view.AppState),
            new Tables(view.AppState),
            new Views(view.AppState),
            new Indexes(view.AppState),
            new StoredProcedures(view.AppState),
            new Functions(view.AppState)
            )
        {
        }

        private void Initialize()
        {
            _view.BtnFindClick += DoBtnFindClick;
            _view.BtnConnectClick += DoBtnConnectClick;
            _view.EnableDisableBtnConnect += DoEnableDisableBtnConnect;
            _view.TreeviewNodeClick += DoTreeviewNodeClick;
            _view.CopyQueryToClipboardToolStripMenuItemClick += DoCopyQueryToClipboardToolStripMenuItemClick;
            _view.CopyNameToClipboardToolStripMenuItemClick += DoCopyNameToClipboardToolStripMenuItemClick;
            _view.CopyInformationToClipboardToolStripMenuItemClick += DoCopyInformationToClipboardToolStripMenuItemClick;
            _view.CopyServerInformationClick += DoCopyServerInformationClick;
            _view.CopyListToClipboardToolStripMenuItemClick += DoCopyListToClipboardToolStripMenuItemClick;
            _view.DatabaseSelectedIndexChanged += DoDatabaseSelectedIndexChanged;
        }

        private void DoBtnConnectClick(object sender, ConnectEventArgs args)
        {
            if (_view.ShowLoginDialog(args.Server))
            {
                _view.InsertServerIntoCombobox(args.Server);
                try
                {
                    _view.AppState.CurrentConnection.Open();
                    var databases = _databases.GetDatabases();
                    _view.ClearDatabasesCombobox();
                    foreach (var database in databases.OrderBy(p => p.Name))
                    {
                        _view.InsertDatabaseIntoCombobox(database.Name);
                    }
                    var serverInfo = _server.GetServerInfo();
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

        private void DoDatabaseSelectedIndexChanged(object sender, DatabaseChangedEventArgs args)
        {
            _view.ClearDatabaseInformation();
            var databaseMetaInfo = _databases.FindDatabaseMetaInfo(args.Database);
            _view.ShowDatabaseInfo(databaseMetaInfo);
            _view.SetLblDatabase();
        }

        private void DoBtnFindClick(object sender, FindEventArgs args)
        {
            _view.ClearResults();
            _view.ClearObjectInformation();
            var startTime = DateTime.Now;
            int rowCount = 0;
            rowCount += DoFindTables(args);
            rowCount += DoFindViews(args);
            rowCount += DoFindIndexes(args);
            rowCount += DoFindStoredProcedures(args);
            rowCount += DoFindFunctions(args);
            _view.InsertSearchQueryIntoCombobox(args.FindWhat);
            var executionTime = DateTime.Now - startTime;
            _view.SetExecutionTime(executionTime);
            _view.SetLblRowCount(rowCount);
            if (rowCount == 0)
            {
                _view.ShowNoResultsFound();
            }
        }

        private int DoFindTables(FindEventArgs args)
        {
            int rowCount = 0;
            if (args.LookInTables)
            {
                var tables = _tables.FindTables(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    tables = tables.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0).ToList();
                }
                tables = tables.OrderBy(p => p.SchemaName).ThenBy(p => p.Name).ThenBy(p => p.ColumnName).ToList();
                _view.InsertTableIntoTreeview(tables);
                rowCount = tables.Count;
                rowCount += DoFindTableExtendedProperties(args);
            }
            return rowCount;
        }

        private int DoFindTableExtendedProperties(FindEventArgs args)
        {
            int rowCount = 0;
            if (args.LookInExtendedProperties)
            {
                var tableExtendedProperties = _tables.FindTableExtendedProperties(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    tableExtendedProperties = tableExtendedProperties.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0).ToList();
                }
                tableExtendedProperties = tableExtendedProperties.OrderBy(p => p.SchemaName).ThenBy(p => p.TableName).ThenBy(p => p.ColumnName).ThenBy(p => p.Name).ToList();
                _view.InsertTableExtendedPropertiesIntoTreeview(tableExtendedProperties);
                rowCount = tableExtendedProperties.Count;
            }
            return rowCount;
        }

        private int DoFindViews(FindEventArgs args)
        {
            int rowCount = 0;
            if (args.LookInViews)
            {
                var views = _views.FindViews(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    views = views.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0).ToList();
                }
                views = views.OrderBy(p => p.SchemaName).ThenBy(p => p.Name).ThenBy(p => p.ColumnName).ToList();
                _view.InsertViewIntoTreeview(views);
                rowCount = views.Count;
                rowCount += DoFindViewExtendedProperties(args);
            }
            return rowCount;
        }

        private int DoFindViewExtendedProperties(FindEventArgs args)
        {
            int rowCount = 0;
            if (args.LookInExtendedProperties)
            {
                var viewExtendedProperties = _views.FindViewExtendedProperties(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    viewExtendedProperties = viewExtendedProperties.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0).ToList();
                }
                viewExtendedProperties = viewExtendedProperties.OrderBy(p => p.SchemaName).ThenBy(p => p.TableName).ThenBy(p => p.ColumnName).ThenBy(p => p.Name).ToList();
                _view.InsertViewExtendedPropertiesIntoTreeview(viewExtendedProperties);
                rowCount = viewExtendedProperties.Count;
            }
            return rowCount;
        }

        private int DoFindIndexes(FindEventArgs args)
        {
            int rowCount = 0;
            if (args.LookInIndexes)
            {
                var indexes = _indexes.FindIndexes(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    indexes = indexes.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0).ToList();
                }
                indexes = indexes.OrderBy(p => p.TableName).ThenBy(p => p.Name).ThenBy(p => p.ColumnName).ToList();
                _view.InsertIndexIntoTreeview(indexes);
                rowCount = indexes.Count;
            }
            return rowCount;
        }

        private int DoFindStoredProcedures(FindEventArgs args)
        {
            int rowCount = 0;
            if (args.LookInStoredProcedures)
            {
                var procedures = _storedProcedures.FindProcedures(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    procedures = procedures.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0 || p.Definition.IndexOf(args.FindWhat, StringComparison.Ordinal) >= 0).ToList();
                }
                procedures = procedures.OrderBy(p => p.SchemaName).ThenBy(p => p.Name).ThenBy(p => p.ParameterName).ToList();
                _view.InsertProcedureIntoTreeview(procedures);
                rowCount = procedures.Count;
                rowCount += DoFindStoredProcedureExtendedProperties(args);
            }
            return rowCount;
        }

        private int DoFindStoredProcedureExtendedProperties(FindEventArgs args)
        {
            int rowCount = 0;
            if (args.LookInExtendedProperties)
            {
                var procedureExtendedProperties = _storedProcedures.FindProcedureExtendedProperties(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    procedureExtendedProperties = procedureExtendedProperties.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0).ToList();
                }
                procedureExtendedProperties = procedureExtendedProperties.OrderBy(p => p.SchemaName).ThenBy(p => p.ProcedureName).ThenBy(p => p.ParameterName).ThenBy(p => p.Name).ToList();
                _view.InsertProcedureExtendedPropertiesIntoTreeview(procedureExtendedProperties);
                rowCount = procedureExtendedProperties.Count;
            }
            return rowCount;
        }

        private int DoFindFunctions(FindEventArgs args)
        {
            int rowCount = 0;
            if (args.LookInFunctions)
            {
                var functions = _functions.FindFunctions(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    functions = functions.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0 || p.Definition.IndexOf(args.FindWhat, StringComparison.Ordinal) >= 0).ToList();
                }
                functions = functions.OrderBy(p => p.SchemaName).ThenBy(p => p.Name).ThenBy(p => p.ParameterName).ToList();
                _view.InsertFunctionIntoTreeview(functions);
                rowCount = functions.Count;
                rowCount += DoFindFunctionExtendedProperties(args);
            }
            return rowCount;
        }

        private int DoFindFunctionExtendedProperties(FindEventArgs args)
        {
            int rowCount = 0;
            if (args.LookInExtendedProperties)
            {
                var functionExtendedProperties = _functions.FindFunctionExtendedProperties(args.Database, args.FindWhat);
                if (args.MatchCase)
                {
                    functionExtendedProperties = functionExtendedProperties.Where(p => string.Compare(p.Name, args.FindWhat, StringComparison.Ordinal) == 0).ToList();
                }
                functionExtendedProperties = functionExtendedProperties.OrderBy(p => p.SchemaName).ThenBy(p => p.ProcedureName).ThenBy(p => p.ParameterName).ThenBy(p => p.Name).ToList();
                _view.InsertFunctionExtendedPropertiesIntoTreeview(functionExtendedProperties);
                rowCount = functionExtendedProperties.Count;
            }
            return rowCount;
        }

        private void DoEnableDisableBtnConnect(object sender, EventArgs e)
        {
            _view.BtnConnectEnabled = !String.IsNullOrEmpty(_view.CmbServerText);
        }

        private void DoTreeviewNodeClick(object sender, TreeviewNodeClickEventArgs args)
        {
            switch (args.ParentNodeName)
            {
                case "ViewsNode":
                    ViewNodeClicked(args);
                    break;
                case "TablesNode":
                    TableNodeClicked(args);
                    break;
                case "IndexesNode":
                    IndexNodeClicked(args);
                    break;
                case "StoredProceduresNode":
                    StoredProcedureNodeClicked(args);
                    break;
                case "FunctionsNode":
                    FunctionsNodeClicked(args);
                    break;
                default:
                    return;
            }
        }

        private void TableNodeClicked(TreeviewNodeClickEventArgs args)
        {
            var table = args.NodeTag as Table;
            if (table != null)
            {
                table.RowCount = _tables.FindTableRowCount(args.Database, table.SchemaName + ".[" + table.Name + "]");
                _view.ShowTableInfo(table);
            }
            else
            {
                var tableExtendedInfo = args.NodeTag as TableExtendedProperty;
                if (tableExtendedInfo != null)
                {
                    _view.ShowTableExtendedPropertyInfo(tableExtendedInfo);
                }
            }
        }

        private void ViewNodeClicked(TreeviewNodeClickEventArgs args)
        {
            var view = args.NodeTag as View;
            if (view != null)
            {
                _view.ShowViewInfo(view);
            }
            else
            {
                var viewExtendedInfo = args.NodeTag as ViewExtendedProperty;
                if (viewExtendedInfo != null)
                {
                    _view.ShowViewExtendedPropertyInfo(viewExtendedInfo);
                }
            }
        }


        private void IndexNodeClicked(TreeviewNodeClickEventArgs args)
        {
            var index = (Index)args.NodeTag;
            _view.ShowIndexInfo(index);
        }

        private void StoredProcedureNodeClicked(TreeviewNodeClickEventArgs args)
        {
            var procedure = args.NodeTag as Procedure;
            if (procedure != null)
            {
                _view.ShowProcedureInfo(procedure);
            }
            else
            {
                var procedureExtendedInfo = args.NodeTag as ProcedureExtendedProperty;
                if (procedureExtendedInfo != null)
                {
                    _view.ShowProcedureExtendedPropertyInfo(procedureExtendedInfo);
                }
            }
        }

        private void FunctionsNodeClicked(TreeviewNodeClickEventArgs args)
        {
            var function = args.NodeTag as Function;
            if (function != null)
            {
                _view.ShowFunctionInfo(function);
            }
            else
            {
                var functionExtendedInfo = args.NodeTag as FunctionExtendedProperty;
                if (functionExtendedInfo != null)
                {
                    _view.ShowFunctionExtendedPropertyInfo(functionExtendedInfo);
                }
            }
        }

        private void DoCopyQueryToClipboardToolStripMenuItemClick(object sender, FindEventArgs e)
        {
            var sb = new StringBuilder();
            if (e.LookInTables)
            {
                AppendSqlSection(_tables.GetFindTablesSql(e.Database, e.FindWhat), sb);
            }
            if (e.LookInViews)
            {
                AppendSqlSection(_views.GetFindViewsSql(e.Database, e.FindWhat), sb);
            }
            if (e.LookInIndexes)
            {
                AppendSqlSection(_indexes.GetFindIndexesSql(e.Database, e.FindWhat), sb);
            }
            if (e.LookInStoredProcedures)
            {
                AppendSqlSection(_storedProcedures.GetFindStoredProceduresSql(e.Database, e.FindWhat), sb);
            }
            if (e.LookInFunctions)
            {
                AppendSqlSection(_functions.GetFindFunctionsSql(e.Database, e.FindWhat), sb);
            }
            string sql = sb.ToString();
            sql = Regex.Replace(sql, @"[ \t]+", " ");
            _view.CopyStringToSlipBoard(sql);
        }

        private void AppendSqlSection(string sql, StringBuilder sb)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                sb.AppendLine(sql);
                sb.AppendLine("GO");
                sb.AppendLine();
            }

        }

        private void DoCopyNameToClipboardToolStripMenuItemClick(object sender, CopyNameEventArgs e)
        {
            _view.CopyStringToSlipBoard(e.Name);
        }

        private void DoCopyInformationToClipboardToolStripMenuItemClick(object sender, CopyInformationEventArgs e)
        {
            string information = "";
            foreach (var arr in e.DatabaseObject.ToArrayList())
            {
                information += arr[0] + " " + arr[1] + Environment.NewLine;
            }
            _view.CopyStringToSlipBoard(information);
        }

        private void DoCopyServerInformationClick(object sender, EventArgs e)
        {
            var serverInfo = _server.GetServerInfo();
            string serverInformation = "";
            foreach (var arr in serverInfo.ToArrayList())
            {
                serverInformation += arr[0] + " " + arr[1] + Environment.NewLine;
            }
            _view.CopyStringToSlipBoard(serverInformation);
        }

        private void DoCopyListToClipboardToolStripMenuItemClick(object sender, CopyListToClipboardEventArgs e)
        {
            _view.CopyStringToSlipBoard(string.Join(Environment.NewLine, e.NameList));
        }
    }
}
