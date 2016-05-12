using Honda.HttpLib;
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
    public partial class RecordPage : BasePage, IComponentConnector
    {
        public RecordPage()
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
                string strUri =
                    string.Format(
                        "http://" + HttpBase.m_strServerDomain + ":" + HttpBase.m_strServerPort +
                        "/imp/app/evalRecords/initEvalRecords?logId={0}&shopId={1}",
                        DMUser.INSTANCE.CurrentMUser.UserAccount, DMStoreTour.INSTANCE.CurrentMStore.shopId);

                using (var process = new System.Diagnostics.Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "rundll32.exe",
                        Arguments = "InetCpl.cpl,ClearMyTracksByProcess 8"
                    }

                })
                {
                    process.Start();
                }

                //System.Diagnostics.Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 8");
                this._web.Navigate(new Uri(strUri));

            }
            catch (System.Exception)
            {
            }
        }

        private void Viewbox_Loaded(object sender, RoutedEventArgs e)
        {
            this.tbkStoreName.Text = DMStoreTour.INSTANCE.CurrentMStore.StoreName;
            LoadData();
        }
    }
}