namespace SQLServerSearcher.Views
{
    using System;

    public interface IFrmAbout
    {
        event EventHandler<EventArgs> FrmLoad;

        void SetText(string text);
        void SetDescription(string description);
        void SetProductName(string productName);
        void SetVersion(string version);
    }
}
