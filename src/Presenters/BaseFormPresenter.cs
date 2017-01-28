namespace SQLServerSearcher.Presenters
{
    using System.Drawing;
    using System.Windows.Forms;

    using Model;
    using Views;

    public class BaseFormPresenter
    {
        private readonly IBaseForm _baseForm;

        public BaseFormPresenter(IBaseForm basicForm)
        {
            _baseForm = basicForm;
            Initialize();
        }

        public void Initialize()
        {
            _baseForm.DoFormLoad += DoFormLoad;
        }

        public void DoFormLoad(object sender, BaseFormEventArgs e)
        {
            var theForm = (Form)sender;
            if (e.Width > 0)
            {
                theForm.Width = e.Width;
            }
            if (e.Height > 0)
            {
                theForm.Height = e.Height;
            }
            int locationX = e.Location.X;
            if (locationX <= 0 && theForm.Location.X <= 0)
            {
                locationX = 1;
            }
            int locationY = e.Location.Y;
            if (locationY <= 0 && theForm.Location.Y <= 0)
            {
                locationY = 1;
            }

            theForm.Location = new Point(locationX, locationY);
        }
    }
}
