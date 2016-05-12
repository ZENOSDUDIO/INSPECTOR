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

    public partial class ImprovePage : BasePage
    {
        public ImprovePage()
        {
            InitializeComponent();

            ////评价员1
            //Messenger.Default.Register<string>(this, GlobalValue._IMPROVE_VALUATOR1, msg =>
            //{
            //    byte[] buffer = File.ReadAllBytes(msg);
            //    imaValuator1.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            //});

            ////评价员2
            ////Messenger.Default.Register<string>(this, GlobalValue._IMPROVE_VALUATOR2, msg =>
            ////{
            ////    byte[] buffer = File.ReadAllBytes(msg);
            ////    imaValuator2.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            ////});

            ////零部件经理
            //Messenger.Default.Register<string>(this, GlobalValue._IMPROVE_COMPONT_MANGER, msg =>
            //{
            //    byte[] buffer = File.ReadAllBytes(msg);
            //    imaComponent.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            //});

            ////服务经理
            //Messenger.Default.Register<string>(this, GlobalValue._IMPROVE_SERVE_MANGER, msg =>
            //{
            //    byte[] buffer = File.ReadAllBytes(msg);
            //    imaServe.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            //});

            ////总经理
            //Messenger.Default.Register<string>(this, GlobalValue._IMPROVE_GENERAL_MANAGER, msg =>
            //{
            //    byte[] buffer = File.ReadAllBytes(msg);
            //    ImaGeneral.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            //});
        }
    }
}