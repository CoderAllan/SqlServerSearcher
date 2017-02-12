namespace SQLServerSearcher.Model.EventArgs
{
    using System;

    public class LineOrColumnChangedEventArgs : EventArgs
    {
        public int CurrentLine { get; set; }
        public int CurrentColumn { get; set; }
    }
}
