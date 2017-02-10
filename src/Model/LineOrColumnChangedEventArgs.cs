namespace SQLServerSearcher.Model
{
    using System;

    public class LineOrColumnChangedEventArgs : EventArgs
    {
        public int CurrentLine { get; set; }
        public int CurrentColumn { get; set; }
    }
}
