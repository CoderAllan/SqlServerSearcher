namespace SQLServerSearcher.Views
{
    using System;
    using System.Drawing;

    public interface IBaseForm
    {
        int Width { get; set; }
        int Height { get; set; }
        Point Location { get; set; }

        event EventHandler<BaseFormEventArgs> DoFormLoad;
        //event EventHandler<BasicFormEventArgs> DoFormClosing;
    }

    public class BaseFormEventArgs : EventArgs
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Location { get; set; }
    }
}
