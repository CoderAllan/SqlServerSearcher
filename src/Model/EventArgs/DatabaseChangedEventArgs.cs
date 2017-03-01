namespace SQLServerSearcher.Model.EventArgs
{
    using System;

    public class DatabaseChangedEventArgs : EventArgs
    {
        public string Database { get; set; }
    }
}
