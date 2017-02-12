namespace SQLServerSearcher
{
    using System;
    using System.Windows.Forms;

    using Presenters;
    using Views;

    partial class FrmAbout : Form, IFrmAbout
    {
        private FrmAboutPresenter _frmAboutPresenter;

        public FrmAbout()
        {
            _frmAboutPresenter = new FrmAboutPresenter(this);
            InitializeComponent();
        }

        public event EventHandler<EventArgs> FrmLoad;


        private void FrmAbout_Load(object sender, EventArgs e)
        {
            if (FrmLoad != null)
            {
                FrmLoad(sender, EventArgs.Empty);
            }
        }
        
        public void SetText(string text)
        {
            Text = text;
        }

        public void SetDescription(string description)
        {
            textBoxDescription.Text = description;
        }

        public void SetProductName(string productName)
        {
            labelProductName.Text = productName;
        }

        public void SetVersion(string version)
        {
            labelVersion.Text = version;
        }
    }
}
