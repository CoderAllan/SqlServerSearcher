namespace SQLServerSearcher.Views
{
    using System;

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
    }
}
