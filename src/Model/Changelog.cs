namespace SQLServerSearcher.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    public class Changelog
    {
        private readonly List<ChangelogEntry> _changeLogEntries = new List<ChangelogEntry>();
        public void InitChangeLogEntryList()
        {
            ChangelogEntry changeLog;

            /* Remember to change the versionnumber in the AssemblyInfo.cs file, when adding a new version. */

            changeLog = new ChangelogEntry
            {
                MajorVersion = 1,
                MinorVersion = 1,
                Changes = @"
Bug: Connect failed with exception if collation for a database was null.
Bug: Exceptions during connection is now handled.
Bug: Taborder set correctly on all forms.
Bug: The search results are now ordered correctly.
Bug: When using the arrowkeys to navigate the results, the object information is now shown correctly.
Bug: Tables was not found if only a column contained the search string.
Clicking the Enter key now executes the search.
The object information is now cleared when clicking the tables, views, indexes, stored procedure or functions nodes in the results treeview.
"
            };
            _changeLogEntries.Add(changeLog);

            changeLog = new ChangelogEntry
            {
                MajorVersion = 1,
                MinorVersion = 0,
                Changes = @"
Implemented Find all references.
Refactoring.
"
            };
            _changeLogEntries.Add(changeLog);

            changeLog = new ChangelogEntry
            {
                MajorVersion = 0,
                MinorVersion = 8,
                Changes = @"
Implemented the Match case function when searching.
Highlight the matches found in the definition of stored procedures and functions.
Added Settings button.
Added About box.
Added Changelog.
"
            };
            _changeLogEntries.Add(changeLog);

            changeLog = new ChangelogEntry
            {
                MajorVersion = 0,
                MinorVersion = 7,
                Changes = @"
Added the show definition dialog.
"
            };
            _changeLogEntries.Add(changeLog);

            changeLog = new ChangelogEntry
            {
                MajorVersion = 0,
                MinorVersion = 1,
                Changes = @"
Project created.
"
            };
            _changeLogEntries.Add(changeLog);
        }

        public Changelog()
        {
            InitChangeLogEntryList();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var changelogEntry in _changeLogEntries
                .OrderByDescending(p => p.MajorVersion)
                .ThenByDescending(p => p.MinorVersion)
                .ThenByDescending(p => p.Revision))
            {
                sb.Append(changelogEntry + Environment.NewLine + Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
