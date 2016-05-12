using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.UserCtrl;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Honda.View
{
    /*
   * 评价表的主页面，内置导航控件mainFrame，根据用户左测点击，导航到相应的页面。
   * 
   */

    public partial class WorkLightspotAndIdeaPage : BasePage
    {
        public WorkLightspotAndIdeaPage()
        {
            InitializeComponent();

            Messenger.Default.Register<string>(this, GlobalValue._NAVIGATE_TO, msg => { });
        }
    }
}