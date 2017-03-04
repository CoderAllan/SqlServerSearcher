namespace SQLServerSearcher.Model.EventArgs
{
    using System;

    public class TreeviewNodeClickEventArgs : EventArgs
    {
        public string Database { get; set; }
        public string ParentNodeName { get; set; }
        public object NodeTag { get; set; }
    }
}
