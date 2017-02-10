namespace SQLServerSearcher.Presenters
{
    using Model;
    using Views;

    public class FrmViewSourcePresenter
    {
        private readonly IFrmViewSource _view;

        public FrmViewSourcePresenter(IFrmViewSource view)
        {
            _view = view;

            Initialize();
        }

        private void Initialize()
        {
            _view.FrmLoad += DoFrmLoad;
            _view.LineOrColumnChanged += DoLineOrColumnChanged;
        }

        private void DoFrmLoad(object sender, FrmViewSourceFrmLoadEventArgs args)
        {
            _view.SetTextAreaText(args.Text);
        }

        private void DoLineOrColumnChanged(object sender, LineOrColumnChangedEventArgs args)
        {
            _view.SetCurrentLine(string.Format("Ln: {0}", args.CurrentLine));
            _view.SetCurrentColumn(string.Format("Col: {0}", args.CurrentColumn));
        }
    }
}
