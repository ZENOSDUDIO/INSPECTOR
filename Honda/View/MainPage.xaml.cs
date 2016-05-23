using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Honda.View
{
    /// <summary>
    /// 主页
    /// </summary>
    public partial class MainPage : BasePage
    {
        private MainVM _viewModel;

        /// <summary>
        /// 店列表
        /// </summary>
        private StoreTourPage _storeTourPage;

        /// <summary>
        /// 日程管理
        /// </summary>
        private ScheduleManagePage _scheduleMangePage;

        /// <summary>
        /// 日程管理
        /// </summary>
        private Settings _settingsPage;

        /// <summary>
        /// 特约店KPI
        /// </summary>
        private StoreKPIPage _storeKPIPage;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = (MainVM) DataContext;
            _viewModel._thisPage = this;
            Messenger.Default.Register<string>(this, GlobalValue.COMMAND_MAIN_PAGE, msg => { NavigateToPage(msg); });
            tbkHintUserName.Text = "欢迎您，" + DMUser.INSTANCE.CurrentMUser.UserName;
        }

        /// <summary>
        /// 导航到相应的页面
        /// </summary>
        /// <param name="pageName"></param>
        private void NavigateToPage(string pageName)
        {
            Dispatcher.InvokeAsync(() =>
            {
                HidenPoup();
                while (mainFrame.CanGoBack)
                {
                    mainFrame.RemoveBackEntry();
                }

                switch (pageName)
                {
                    case "巡回评价管理":
                        if (_storeTourPage == null)
                        {
                            _storeTourPage = new StoreTourPage();
                        }
                        mainFrame.Navigate(_storeTourPage);
                        break;

                    case "特约店指导":
                        break;

                    case "日程管理":
                        if (_scheduleMangePage == null)
                        {
                            _scheduleMangePage = new ScheduleManagePage();
                        }
                        _scheduleMangePage.SetIsShowMainPage(false);
                        mainFrame.Navigate(_scheduleMangePage);
                        break;

                    case "主页":
                        if (_scheduleMangePage == null)
                        {
                            _scheduleMangePage = new ScheduleManagePage();
                        }
                        _scheduleMangePage.SetIsShowMainPage(true);
                        mainFrame.Navigate(_scheduleMangePage);
                        break;

                    case "系统设置":
                        if (_settingsPage == null)
                        {
                            _settingsPage = new Settings();
                        }
                        mainFrame.Navigate(_settingsPage);
                        break;

                    case "特约店KPI":
                        if (_storeKPIPage == null)
                        {
                            _storeKPIPage = new StoreKPIPage();
                        }
                        mainFrame.Navigate(_storeKPIPage);
                        break;
                }
            });
        }

        #region 主页弹出框的效果显示代码

        private void btnSpecialShopManage_Click(object sender, RoutedEventArgs e)
        {
            popSpecialShop.IsOpen = !popSpecialShop.IsOpen;
        }

        private void popSpecialShop_Closed(object sender, EventArgs e)
        {
            gdShopManage.Background = null;
        }

        private void popSpecialShop_Opened(object sender, EventArgs e)
        {
            gdShopManage.Background = (SolidColorBrush) Application.Current.Resources["ButtonOpenBackgroundBrush"];
        }


        private void HidenPoup()
        {
            if (popSpecialShop.IsOpen == true)
            {
                popSpecialShop.IsOpen = false;
            }
        }

        #endregion

        private bool isEnter = false;

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            sp_wd_btn.Visibility = Visibility.Visible;
            isEnter = true;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            delayTime();
        }

        private async Task delayTime()
        {
            isEnter = false;
            await Task.Delay(1000);
            if (isEnter)
            {
                return;
            }
            sp_wd_btn.Visibility = Visibility.Collapsed;
        }
    }
}