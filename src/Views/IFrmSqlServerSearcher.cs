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
        event EventHandler<CopyListToClipboardEventArgs> CopyListToClipboardToolStripMenuItemClick;
        event EventHandler<DatabaseChangedEventArgs> DatabaseSelectedIndexChanged;

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
        void ClearDatabaseInformation();
        void ShowServerInfo(ServerInfo serverInfo);
        void ShowDatabaseInfo(DatabaseMetaInfo databaseMetaInfo);
        
        void InsertTableIntoTreeview(List<Table> tables);
        void InsertTableExtendedPropertiesIntoTreeview(List<TableExtendedProperty> tableExtendedProperties);
        void ShowTableInfo(Table table);
        void ShowTableExtendedPropertyInfo(TableExtendedProperty tableExtendedProperty);
        
        void InsertViewIntoTreeview(List<View> views);
        void InsertViewExtendedPropertiesIntoTreeview(List<ViewExtendedProperty> viewExtendedProperties);
        void ShowViewInfo(View view);
        void ShowViewExtendedPropertyInfo(ViewExtendedProperty viewExtendedProperty);
        
        void InsertIndexIntoTreeview(List<Index> indexes);
        void ShowIndexInfo(Index index);
        
        void InsertProcedureIntoTreeview(List<Procedure> procedures);
        void InsertProcedureExtendedPropertiesIntoTreeview(List<ProcedureExtendedProperty> procedureExtendedProperties);
        void ShowProcedureInfo(Procedure procedure);
        void ShowProcedureExtendedPropertyInfo(ProcedureExtendedProperty procedureExtendedProperty);
        
        void InsertFunctionIntoTreeview(List<Function> functions);
        void InsertFunctionExtendedPropertiesIntoTreeview(List<FunctionExtendedProperty> functionExtendedProperties);
        void ShowFunctionInfo(Function function);
        void ShowFunctionExtendedPropertyInfo(FunctionExtendedProperty functionExtendedProperty);
        
        void CopyStringToSlipBoard(string text);
    }
}
