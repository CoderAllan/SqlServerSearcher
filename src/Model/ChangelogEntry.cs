namespace SQLServerSearcher.Model
{
    using System;

    public class ChangelogEntry
    {
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int Revision { get; set; }
        public string Changes { get; set; }

        public string Version()
        {
            return MajorVersion + "." + MinorVersion + (Revision > 0 ? "." + Revision : "");
        }

        public override string ToString()
        {
            return Version() + Environment.NewLine + Changes.Trim();
        }
    }
}
