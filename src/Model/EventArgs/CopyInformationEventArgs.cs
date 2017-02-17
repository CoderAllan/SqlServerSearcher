namespace SQLServerSearcher.Model.EventArgs
{
    using System;

    public class CopyInformationEventArgs : EventArgs
    {
        public IDatabaseObject DatabaseObject { get; set; }
    }
}
