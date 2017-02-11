namespace SQLServerSearcher.Views
{
    using System;

    public interface IFrmChangelog
    {
        event EventHandler<EventArgs> FrmLoad;

        void SetTxtChangelogText(string text);
    }
}
