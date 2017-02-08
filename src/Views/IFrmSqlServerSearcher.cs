namespace SQLServerSearcher.Views
{
    using System;
    using System.Collections.Generic;

    using Model;

    public interface IFrmSqlServerSearcher
    {
        ApplicationState AppState { get; }

        event EventHandler<ConnectEventArgs> BtnConnectClick;
        event EventHandler<FindEventArgs> BtnFindClick;
        event EventHandler<EventArgs> EnableDisableBtnConnect;
        event EventHandler<TreeviewNodeClickEventArgs> TreeviewNodeClick;

        bool BtnConnectEnabled { get; set; }
        string CmbServerText { get; }

        bool ShowLoginDialog(string server);
        void InsertServerIntoCombobox(string server);
        void InsertDatabaseIntoCombobox(string database);
        void InsertSearchQueryIntoCombobox(string searchQuery);
        void SetLblServerVersion(string text);
        void SetLblServerName();
        void SetLblDatabase();
        void SetExecutionTime(TimeSpan executionTime);
        void SetLblRowCount(int rowCount);
        void ClearResults();
        void InsertTableIntoTreeview(List<Table> tables);
        void ShowTableInfo(Table table);
        void InsertViewIntoTreeview(List<View> views);
        void ShowViewInfo(View view);
        void InsertIndexIntoTreeview(List<Index> indexes);
        void ShowIndexInfo(Index index);
        void InsertProcedureIntoTreeview(List<Procedure> procedures);
        void ShowProcedureInfo(Procedure procedure);
        void InsertFunctionIntoTreeview(List<Function> functions);
        void ShowFunctionInfo(Function function);
    }
}
