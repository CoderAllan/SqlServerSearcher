namespace SQLServerSearcher.Views
{
    using System;
    using System.Collections.Generic;

    using Model;
    using Model.EventArgs;

    public interface IFrmSqlServerSearcher
    {
        ApplicationState AppState { get; }

        event EventHandler<ConnectEventArgs> BtnConnectClick;
        event EventHandler<FindEventArgs> BtnFindClick;
        event EventHandler<EventArgs> EnableDisableBtnConnect;
        event EventHandler<TreeviewNodeClickEventArgs> TreeviewNodeClick;
        event EventHandler<FindEventArgs> CopyQueryToClipboardToolStripMenuItemClick;
        event EventHandler<CopyNameEventArgs> CopyNameToClipboardToolStripMenuItemClick;
        event EventHandler<CopyInformationEventArgs> CopyInformationToClipboardToolStripMenuItemClick;
        event EventHandler<EventArgs> CopyServerInformationClick;

        bool BtnConnectEnabled { get; set; }
        string CmbServerText { get; }

        bool ShowLoginDialog(string server);
        void ShowNoResultsFound();
        void ShowErrorDialog(string text);
        void InsertServerIntoCombobox(string server);
        void InsertDatabaseIntoCombobox(string database);
        void InsertSearchQueryIntoCombobox(string searchQuery);
        void SetLblServerName();
        void SetLblDatabase();
        void SetExecutionTime(TimeSpan executionTime);
        void SetLblRowCount(int rowCount);
        void ClearResults();
        void ClearObjectInformation();
        void ShowServerInfo(ServerInfo serverInfo);
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
        void CopyStringToSlipBoard(string text);
    }
}
