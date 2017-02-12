namespace SQLServerSearcher
{
    using System;
    using System.Windows.Forms;

    using Model;
    using Model.EventArgs;
    using Presenters;
    using Views;

    public partial class FrmChangelog : Form, IBaseForm, IFrmChangelog
    {
        private readonly ApplicationState _appState;
        private readonly BaseFormPresenter _baseFormPresenter;
        private readonly FrmChangelogPresenter _changelogPresenter;

        public event EventHandler<EventArgs> FrmLoad;

        public FrmChangelog(ApplicationState appState)
        {
            _appState = appState;
            _baseFormPresenter = new BaseFormPresenter(this);
            _changelogPresenter = new FrmChangelogPresenter(this);

            InitializeComponent();
        }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;

        private void FrmChangelog_Load(object sender, EventArgs e)
        {
            var eventArgs = _appState.GetFormLocationAndPosition(this);
            DoFormLoad(this, eventArgs);

            if (FrmLoad != null)
            {
                FrmLoad(null, EventArgs.Empty);
            }
        }

        private void FrmChangelog_FormClosing(object sender, FormClosingEventArgs e)
        {
            _appState.PersistFormLocationAndPosition(this);
        }

        public void SetTxtChangelogText(string text)
        {
            txtChangelog.Text = text;
            txtChangelog.Select(0, 0);
        }
    }
}
