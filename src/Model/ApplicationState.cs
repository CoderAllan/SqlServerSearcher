namespace SQLServerSearcher.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("AppState")]
    public class ApplicationState
    {
        [XmlIgnore]
        public static readonly string AppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SQLServerSearcher");

        public string ApplicationLocale;
        public int FrmSqlServerSearcherWidth;
        public int FrmSqlServerSearcherHeight;
        public int FrmSqlServerSearcherPosX;
        public int FrmSqlServerSearcherPosY;

        public static void WriteApplicationState(ApplicationState state)
        {
            XmlTextWriter writer = null;
            try
            {
                var s = new XmlSerializer(typeof (ApplicationState));
                if (!Directory.Exists(AppFolder))
                {
                    Directory.CreateDirectory(AppFolder);
                }
                writer = new XmlTextWriter(new StreamWriter(Path.Combine(AppFolder, "AppState.xml"), false));
                s.Serialize(writer, state);
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        public static ApplicationState ReadApplicationState()
        {
            XmlTextReader reader = null;
            try
            {
                string runLogPath = Path.Combine(AppFolder, "AppState.xml");
                if (File.Exists(runLogPath))
                {
                    reader = new XmlTextReader(new StreamReader(runLogPath, false));
                }
                if (reader == null)
                {
                    return new ApplicationState();
                }
                var xmlSerializer = new XmlSerializer(typeof (ApplicationState));
                return (ApplicationState) xmlSerializer.Deserialize(reader);
            }
            catch
            {
                return new ApplicationState();
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        public void ReadComboBoxElements(ComboBox combobox, List<string> appStateList, Action<string, int> addElementToComboBox)
        {
            if (appStateList != null && (appStateList.Count > 0))
            {
                for (int i = appStateList.Count() - 1; i >= 0; i--) 
                {
                    if (combobox.Items.Count >= 20) break;
                    addElementToComboBox(appStateList[i], i);
                }
                if (combobox.Items.Count > 0)
                {
                    combobox.SelectedIndex = 0;
                }
            }
        }

        internal void PersistFrmStandaloneReview(FrmSqlServerSearcher form)
        {
            FrmSqlServerSearcherHeight = form.Height;
            FrmSqlServerSearcherWidth = form.Width;
            FrmSqlServerSearcherPosX = form.Location.X;
            FrmSqlServerSearcherPosY = form.Location.Y;
        }
    }
}
