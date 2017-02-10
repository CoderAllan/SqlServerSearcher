namespace SQLServerSearcher
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ScintillaNET;

    using Model;
    using Presenters;
    using Views;

    public partial class FrmViewSource : Form, IBaseForm, IFrmViewSource
    {
        private readonly ApplicationState _appState;
        private readonly ProcedureObject _procedureObject;
        private readonly BaseFormPresenter _baseFormPresenter;
        private readonly FrmViewSourcePresenter _frmViewSourcePresenter;

        private Scintilla _textArea;
        private const int NumberMargin = 1;

        public event EventHandler<BaseFormEventArgs> DoFormLoad;
        public event EventHandler<FrmViewSourceFrmLoadEventArgs> FrmLoad;
        public event EventHandler<LineOrColumnChangedEventArgs> LineOrColumnChanged;

        public FrmViewSource(ApplicationState appState, ProcedureObject procedureObject)
        {
            _appState = appState;
            _procedureObject = procedureObject;
            _baseFormPresenter = new BaseFormPresenter(this);
            _frmViewSourcePresenter = new FrmViewSourcePresenter(this);
            
            InitializeComponent();

            var eventArgs = _appState.GetFormLocationAndPosition(this);
            DoFormLoad(this, eventArgs);
        }

        private void FrmViewSource_Load(object sender, EventArgs e)
        {
            InitializeTextArea();

            if (FrmLoad != null)
            {
                FrmLoad(sender, new FrmViewSourceFrmLoadEventArgs {Text = _procedureObject.Definition});
            }
        }

        private void FrmViewSource_FormClosing(object sender, FormClosingEventArgs e)
        {
            _appState.PersistFormLocationAndPosition(this);
        }

        private void InitializeTextArea()
        {
            _textArea = new Scintilla();
            pnlText.Controls.Add(_textArea);
            _textArea.KeyUp += UpdateLineAndColumn;
            _textArea.MouseClick += UpdateLineAndColumn;
            _textArea.Dock = DockStyle.Fill;
            _textArea.WrapMode = WrapMode.None;
            _textArea.IndentationGuides = IndentView.LookBoth;
            _textArea.Styles[Style.LineNumber].BackColor = Color.LightGray;
            _textArea.Styles[Style.LineNumber].ForeColor = Color.DimGray;
            _textArea.Styles[Style.IndentGuide].BackColor = Color.LightGray;
            _textArea.Styles[Style.IndentGuide].ForeColor = Color.DimGray;

            var nums = _textArea.Margins[NumberMargin];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            _textArea.Lexer = Lexer.Sql;
            // The ms sql syntax highlighting was found here: https://gist.github.com/jcouture100/f5d58df816445a1d74df10883618eab4
            // Set the Styles
            _textArea.Styles[Style.LineNumber].ForeColor = Color.FromArgb(255, 128, 128, 128);  //Dark Gray
            _textArea.Styles[Style.LineNumber].BackColor = Color.FromArgb(255, 228, 228, 228);  //Light Gray
            _textArea.Styles[Style.Sql.Comment].ForeColor = Color.Green;
            _textArea.Styles[Style.Sql.CommentLine].ForeColor = Color.Green;
            _textArea.Styles[Style.Sql.CommentLineDoc].ForeColor = Color.Green;
            _textArea.Styles[Style.Sql.Number].ForeColor = Color.Maroon;
            _textArea.Styles[Style.Sql.Word].ForeColor = Color.Blue;
            _textArea.Styles[Style.Sql.Word2].ForeColor = Color.Fuchsia;
            _textArea.Styles[Style.Sql.User1].ForeColor = Color.Gray;
            _textArea.Styles[Style.Sql.User2].ForeColor = Color.FromArgb(255, 00, 128, 192);    //Medium Blue-Green
            _textArea.Styles[Style.Sql.String].ForeColor = Color.Red;
            _textArea.Styles[Style.Sql.Character].ForeColor = Color.Red;
            _textArea.Styles[Style.Sql.Operator].ForeColor = Color.Black;

            // Set keyword lists
            // Word = 0
            _textArea.SetKeywords(0, @"add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go ");
            // Word2 = 1
            _textArea.SetKeywords(1, @"ascii cast char charindex ceiling coalesce collate contains convert current_date current_time current_timestamp current_user floor isnull max min nullif object_id session_user substring system_user tsequal ");
            // User1 = 4
            _textArea.SetKeywords(4, @"all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) * ");
            // User2 = 5
            _textArea.SetKeywords(5, @"sys objects sysobjects ");            
        }

        private void UpdateLineAndColumn(object sender, EventArgs eventArgs)
        {
            if (LineOrColumnChanged != null)
            {
                LineOrColumnChanged(sender, new LineOrColumnChangedEventArgs
                {
                    CurrentLine = _textArea.CurrentLine + 1,
                    CurrentColumn = _textArea.GetColumn(_textArea.CurrentPosition) + 1
                });
            }
        }

        public void SetTextAreaText(string text)
        {
            _textArea.ReadOnly = false;
            _textArea.Text = text;
            _textArea.ReadOnly = true;
        }

        public void SetCurrentLine(string currentLine)
        {
            tslblLine.Text = currentLine;
        }

        public void SetCurrentColumn(string currentColumn)
        {
            tslblColumn.Text = currentColumn;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int wmKeydown = 0x100;
            const int wmSyskeydown = 0x104;
            if ((msg.Msg == wmKeydown) || (msg.Msg == wmSyskeydown))
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        Close();
                        break;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
