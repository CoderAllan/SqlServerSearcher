namespace SQLServerSearcher.Views
{
    using System;

    using Model;

    public interface IFrmSqlServerSearcher
    {
        ApplicationState AppState { get; }

        event EventHandler<ConnectEventArgs> BtnConnectClick;
        event EventHandler<FindEventArgs> BtnFindClick;
    }

    public class ConnectEventArgs : EventArgs
    {
        public string Server { get; set; }
    }

    public class FindEventArgs : EventArgs
    {
        public string FindWhat { get; set; }
        public bool MatchCase { get; set; }
        public bool LookInTables { get; set; }
        public bool LookInViews { get; set; }
        public bool LookInStoredProcedures { get; set; }
        public bool LookInFunctions { get; set; }
    }
}
