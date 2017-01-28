namespace SQLServerSearcher.Views
{
    using System;

    using Model;

    public interface IFrmLogin
    {
        event EventHandler<LoginEventArgs> BtnLoginClick;
    }
}