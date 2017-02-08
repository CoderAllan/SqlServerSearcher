namespace SQLServerSearcher.Model
{
    using System;

    public class TreeviewNodeClickEventArgs : EventArgs
    {
        public string ParentNodeName { get; set; }
        public object NodeTag { get; set; }
    }
}
