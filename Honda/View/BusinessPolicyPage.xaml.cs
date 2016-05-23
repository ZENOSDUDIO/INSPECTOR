using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.UserCtrl;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
     * 商务政策
     * 
     */

    public partial class BusinessPolicyPage : BasePage
    {
        public BusinessPolicyPage()
        {
            InitializeComponent();
            Tabbar.AddItem("纯正性：非纯正零部件使用情况", gd1);
            Tabbar.AddItem("外销：零部件对外销售情况", gd2);
            Tabbar.AddItem("价格：零部件价格执行", gd3);
            ////评价员
            //Messenger.Default.Register<string>(this, GlobalValue._BUSINESS_POLICY_VALUATOR, msg =>
            //{
            //    byte[] buffer = File.ReadAllBytes(msg);
            //    imaValuator1.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            //});

            ////零部件经理
            //Messenger.Default.Register<string>(this, GlobalValue._BUSINESS_POLICY_COMPONT_MANGER, msg =>
            //{
            //    byte[] buffer = File.ReadAllBytes(msg);
            //    imaComponent.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            //});

            ////总经理
            //Messenger.Default.Register<string>(this, GlobalValue._BUSINESS_POLICY_GENERAL_MANAGER, msg =>
            //{
            //    byte[] buffer = File.ReadAllBytes(msg);
            //    ImaGeneral.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            //});
        }
    }
}