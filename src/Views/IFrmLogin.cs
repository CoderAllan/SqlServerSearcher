namespace SQLServerSearcher.Views
{
    using System;

    using Model;
    using Model.EventArgs;

    public interface IFrmLogin
    {
        event EventHandler<LoginEventArgs> BtnLoginClick;
        event EventHandler<EventArgs> BtnCancelClick;
        event EventHandler<EventArgs> EnableDisableTextBoxes;

        ApplicationState AppState { get; }

        bool RbWindowsLogon { get; set; }
        bool RbSqlServerLogon { get; set; }
        bool TxtLoginEnabled { get; set; }
        bool TxtPasswordEnabled { get; set; }

        void CloseForm();
    }
}