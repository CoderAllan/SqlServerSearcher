namespace SQLServerSearcher.Views
{
    using System;

    using Model;

    public interface IFrmViewSource
    {
        event EventHandler<FrmViewSourceFrmLoadEventArgs> FrmLoad;
        event EventHandler<LineOrColumnChangedEventArgs> LineOrColumnChanged;

        void SetTextAreaText(string text);
        void SetCurrentLine(string currentLine);
        void SetCurrentColumn(string currentColumn);
        void HighlightWord(string text);
    }
}
