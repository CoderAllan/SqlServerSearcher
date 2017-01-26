namespace SQLServerSearcher
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Model;
    using Presenters;
    using Views;

    public partial class FrmSqlServerSearcher : Form, IBaseForm
    {
        #region static void Main()
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmSqlServerSearcher());
        }
        #endregion

        private readonly ApplicationState _appState;
        private readonly BaseFormPresenter _baseFormPresenter;

        public FrmSqlServerSearcher()
        {
            _appState = ApplicationState.ReadApplicationState();
            _baseFormPresenter = new BaseFormPresenter(this);

            InitializeComponent();

            var eventArgs = new BaseFormEventArgs
            {
                Height = _appState.FrmSqlServerSearcherHeight,
                Width = _appState.FrmSqlServerSearcherWidth,
                Location = new Point(_appState.FrmSqlServerSearcherPosX, _appState.FrmSqlServerSearcherPosY)
            };
            DoFormLoad(this, eventArgs);
        }

        public ApplicationState AppState
        {
            get { return _appState; }
        }

        public event EventHandler<BaseFormEventArgs> DoFormLoad;


        public void CloseApplication()
        {
            _appState.PersistFrmStandaloneReview(this);
            ApplicationState.WriteApplicationState(_appState);
        }

        private void FrmSqlServerSearcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseApplication();
        }

    }
}
