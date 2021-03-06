SqlServerSearcher
====

This application has been implemented in order to make it easier to quickly search a Microsoft SQL Server database. The application makes it possible to search in:

* Table names
* Table column names
* View names
* View column names
* Index names
* Stored procedure names
* Stored procedure definitions
* Stored procedure parameter names
* Function names
* Function definitions
* Function parameter names
* Extended properties

The application searches the specified database system views for the information.

When right-clicking a found stored procedure name og function name in the treeview, it is possible to show the definition of the object.

When right-clicking any found object and choosing 'Find all references', a search is performed for that objects name.

Some of information the application shows, is only accessible to logins that have sysadmin permissions. When informations is not available then the peroperty is shown as 'N/A' (Not available) in the listviews.

Shortcut keys
----

* Alt-C: Connect
* F3 or Alt-F: Find
* F12: Show definition of stored procedure og function

Task list
----

- [ ] Make it possible to use more than one search term
- [x] Search in extended properties
- [x] Implement Find in the view definition dialog
- [ ] Show column names in the information list
- [ ] Show parameter names in the information list
- [x] Implement the 'Match case' checkbox functionality
- [x] Display metadata about the found element when clicking the TreeView node
- [x] Display metadata about the connected server
- [x] Display metadata about the selected database
- [x] Right-click in treeview to find all references
- [x] Right-click in treeview to show content of stored procedure or function
- [x] Highlight the matches found in the definition of stored procedures and functions

Screenshots
----

The login dialog

![logindialog](Screenshots/LoginDialog.png)

Search

![search](Screenshots/Search.png)

Contextmenu

![contextmenu](Screenshots/ContextMenu.png)

View definition

![search](Screenshots/ViewSource.png)

Binaries
----

A zipfile with the latest build can be here: [https://github.com/CoderAllan/SqlServerSearcher/tree/master/binaries]([https://github.com/CoderAllan/SqlServerSearcher/tree/master/binaries])

To install the application, just unzip the file and doubleclick the SQLServerSearcher.exe file.

The application saves all user settings in a AppState.xml file that is placed in the current users Roaming folder:
`C:\Users\##USER NAME##\AppData\Roaming\SQLServerSearcher\`
This folder has to be deleted manually to remove all traces of the tool if you decide not to keep the tool.


Icons
----

Icons for the application is from: [https://github.com/ioBroker/ioBroker.icons-open-icon-library-png]([https://github.com/ioBroker/ioBroker.icons-open-icon-library-png])

Packages/Components used
----

* [ScintillaNET](https://github.com/jacobslusser/ScintillaNET)
* [ScintillaNET-FindReplaceDialog](https://github.com/Stumpii/ScintillaNET-FindReplaceDialog)
