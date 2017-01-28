namespace SQLServerSearcher.Views
{
    using System;
    using System.Drawing;

    using Model;

    public interface IBaseForm
    {
        int Width { get; set; }
        int Height { get; set; }
        Point Location { get; set; }

        event EventHandler<BaseFormEventArgs> DoFormLoad;
        //event EventHandler<BasicFormEventArgs> DoFormClosing;
    }
}
