This application uses the Model-View-Presenter pattern to seperate presentation and the business logic
Read about the pattern here:
http://www.dreamincode.net/forums/topic/342849-introducing-mvp-model-view-presenter-pattern-winforms/
http://www.developerfusion.com/article/145904/an-introduction-to-testing-with-the-modelviewpresenter-pattern-for-web-forms-development/

In the Presenteren there should be no references to and System.Windows.Forms or classes derived from that namespace.

The Presenters for this application are all placed in /Presenters
The Views for this application are all placed in /Views
