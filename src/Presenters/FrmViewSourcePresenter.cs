namespace SQLServerSearcher.Presenters
{
    using Model;
    using Model.EventArgs;
    using Views;

    public class FrmViewSourcePresenter
    {
        private readonly ApplicationState _appState;
        private readonly IFrmViewSource _view;

        public FrmViewSourcePresenter(ApplicationState appState, IFrmViewSource view)
        {
            _appState = appState;
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
            if (_appState.PreviousSearches != null && _appState.PreviousSearches.Count > 0)
            {
                var lastSearch = _appState.PreviousSearches[_appState.PreviousSearches.Count - 1];
                _view.HighlightWord(lastSearch);
            }
        }

        private void DoLineOrColumnChanged(object sender, LineOrColumnChangedEventArgs args)
        {
            _view.SetCurrentLine(string.Format("Ln: {0}", args.CurrentLine));
            _view.SetCurrentColumn(string.Format("Col: {0}", args.CurrentColumn));
        }
    }
}
