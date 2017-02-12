namespace SQLServerSearcher.Presenters
{
    using System;
    using System.Reflection;

    using Views;

    public class FrmAboutPresenter
    {
        private readonly IFrmAbout _view;

        public FrmAboutPresenter(IFrmAbout view)
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
            _view.SetText(string.Format("About {0}", AssemblyTitle));
            _view.SetProductName(AssemblyProduct);
            _view.SetVersion(string.Format("Version {0}", AssemblyVersion));
            _view.SetDescription(AssemblyDescription);
        }


        private string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        private string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        private string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        private string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }
    }
}
