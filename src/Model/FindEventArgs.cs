namespace SQLServerSearcher.Model
{
    using System;

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
