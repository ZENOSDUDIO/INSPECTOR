using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.IO;
using Honda.HttpLib;
using Honda.Model;

namespace Honda.View
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : BasePage
    {
        public Settings()
        {
            InitializeComponent();


            if (DMUser.INSTANCE.CurrentMUser.IsAutoLogin)
            {
                this.cbRecordUserNumber.IsChecked = false;
                this.cbaAutomaticLogin.IsChecked = true;
            }
            else
            {
                this.cbRecordUserNumber.IsChecked = true;
                this.cbaAutomaticLogin.IsChecked = false;
            }
        }

        private void cbRecordUserNumber_Checked(object sender, RoutedEventArgs e)
        {
            DMUser.INSTANCE.CurrentMUser.IsAutoLogin = false;
            this.cbaAutomaticLogin.IsChecked = false;
        }

        private void cbaAutomaticLogin_Checked(object sender, RoutedEventArgs e)
        {
            DMUser.INSTANCE.CurrentMUser.IsAutoLogin = true;
            this.cbRecordUserNumber.IsChecked = false;
        }
    }
}