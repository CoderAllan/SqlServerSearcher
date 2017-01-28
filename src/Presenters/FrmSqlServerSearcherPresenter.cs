namespace SQLServerSearcher.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Views;

    public class FrmSqlServerSearcherPresenter
    {
        private readonly IFrmSqlServerSearcher _view;

        public FrmSqlServerSearcherPresenter(IFrmSqlServerSearcher view)
        {
            _view = view;

            Initialize();
        }

        private void Initialize()
        {
            _view.BtnFindClick += DoBtnFindClick;
            _view.BtnConnectClick += DoBtnConnectClick;
        }

        private void DoBtnConnectClick(object sender, ConnectEventArgs args)
        {

        }

        private void DoBtnFindClick(object sender, FindEventArgs args)
        {
            
        }

    }
}
