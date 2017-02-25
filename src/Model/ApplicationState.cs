namespace SQLServerSearcher.Model
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Serialization;

    using EventArgs;

    [XmlRoot("AppState")]
    public class ApplicationState
    {
        public enum LastUsedLoginMethods
        {
            WindowsLogon,
            SqlServerLogon
        };
        [XmlIgnore]
        public static readonly string AppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SQLServerSearcher");

        public string ApplicationLocale;
        public bool MatchCase;
        public bool LookInTables;
        public bool LookInViews;
        public bool LookInFunctions;
        public bool LookInStoredProcedures;
        public bool LookInIndexes;
        public bool LookInExtendedProperties;
        public LastUsedLoginMethods LastUsedLoginMethod;
        public string LastUsedLogin;
        public string LastUsedBatabase;
        public List<string> Servers;
        public List<string> PreviousSearches;
        public List<FormLocationAndPosition> FormLocationsAndPositions;
        public int NameColumnWith;
        public int ValueColumnWith;
        public int ServerPropertyNameColumnWith;
        public int ServerPropertyValueColumnWith;

        [XmlIgnore]
        public SqlConnection CurrentConnection;

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

        public void PersistComboBox(ComboBox combobox, List<string> appStateList)
        {
            if (appStateList != null)
            {
                appStateList.Clear();
                for (int i = combobox.Items.Count - 1; i >= 0; i--)
                {
                    appStateList.Add(combobox.Items[i].ToString());
                }
            }
        }

        internal BaseFormEventArgs GetFormLocationAndPosition(Form form)
        {
            if (FormLocationsAndPositions == null)
            {
                FormLocationsAndPositions = new List<FormLocationAndPosition>();
            }
            var formLocAndPos = FormLocationsAndPositions.FirstOrDefault(p => p.FormName.Equals(form.Name));
            var locAndPos = new BaseFormEventArgs
            {
                Height = formLocAndPos == null ? 0 : formLocAndPos.Height,
                Width = formLocAndPos == null ? 0 : formLocAndPos.Width,
                Location = new Point(formLocAndPos == null ? 0 : formLocAndPos.PosX, formLocAndPos == null ? 0 : formLocAndPos.PosY)
            };
            return locAndPos;
        }

        internal void PersistFormLocationAndPosition(Form form)
        {
            FormLocationsAndPositions.RemoveAll(p => p.FormName.Equals(form.Name));
            var locAndPos = new FormLocationAndPosition
            {
                FormName = form.Name,
                Height = form.Height,
                Width = form.Width,
                PosX = form.Location.X,
                PosY = form.Location.Y
            };
            FormLocationsAndPositions.Add(locAndPos);
        }
    }
}
