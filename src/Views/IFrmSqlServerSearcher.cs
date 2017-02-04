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

        bool BtnConnectEnabled { get; set; }
        string CmbServerText { get; }

        bool ShowLoginDialog(string server);
        void InsertServerIntoCombobox(string server);
        void InsertDatabaseIntoCombobox(string database);
        void InsertSearchQueryIntoCombobox(string searchQuery);
        void SetLblServerVersion(string text);
        void SetText(string text);
        void ClearResults();
        void InsertTableIntoTreeview(List<Table> tables);
        void InsertViewIntoTreeview(List<View> views);
    }
}
