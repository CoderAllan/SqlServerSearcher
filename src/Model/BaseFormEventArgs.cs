namespace SQLServerSearcher.Model
{
    using System;
    using System.Drawing;

    public class BaseFormEventArgs : EventArgs
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Location { get; set; }
    }
}
