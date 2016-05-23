using Honda.Globals;
using Honda.HttpLib;
using Honda.Model;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace Honda.View
{
    public partial class StoreKPIPage : BasePage, IComponentConnector
    {
        public StoreKPIPage()
        {
            this.InitializeComponent();
            this.LoadData();
        }

        private void LoadData()
        {
            this._web.LoadCompleted +=
                delegate(object o, NavigationEventArgs e) { this._web.Visibility = Visibility.Visible; };
            try
            {
                var urlInfo =
                    SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.BI_REPORT_URL_INFO) as MBiReportInfo;
                if (urlInfo == null || string.IsNullOrWhiteSpace(urlInfo.BiReportURL))
                {
                    MessageBox.Show("û���㹻��Ȩ�޷��ʴ�ҳ�棡");
                    return;
                }
                this._web.Navigate(new Uri(urlInfo.BiReportURL));
            }
            catch (System.Exception)
            {
            }
        }

        private void Viewbox_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}