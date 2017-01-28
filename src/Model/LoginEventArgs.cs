namespace SQLServerSearcher.Model
{
    using System;
    public class LoginEventArgs : EventArgs
    {
        public bool WindowsLogin { get; set; }
        public bool SQLServerLogin { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
