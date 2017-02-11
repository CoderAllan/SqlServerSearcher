namespace SQLServerSearcher.Presenters
{
    using System;

    using Model;
    using Views;

    public class FrmChangelogPresenter
    {
        private readonly IFrmChangelog _view;

        public FrmChangelogPresenter(IFrmChangelog view)
        {
            _view = view;

            Initialize();
        }

        private void Initialize()
        {
            _view.FrmLoad += DoFrmLoad;
        }

        private void DoFrmLoad(object sender, EventArgs e)
        {
            var changelog = new Changelog();
            _view.SetTxtChangelogText(changelog.ToString());            
        }
    }
}
