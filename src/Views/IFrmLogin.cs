namespace SQLServerSearcher.Views
{
    using System;

    public interface IFrmLogin
    {
        event EventHandler<LoginEventArgs> BtnLoginClick;
    }

    public class LoginEventArgs : EventArgs
    {
        public bool WindowsLogin { get; set; }
        public bool SQLServerLogin { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}