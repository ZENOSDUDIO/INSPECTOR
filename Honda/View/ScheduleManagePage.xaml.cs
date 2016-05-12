using GalaSoft.MvvmLight.Messaging;
using Honda.HttpLib;
using Honda.Model;
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
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Honda.View
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class ScheduleManagePage : BasePage
    {
        private ScheduleManageVM _viewModel;

        public ScheduleManagePage()
        {
            InitializeComponent();
            _viewModel = (ScheduleManageVM) DataContext;
            _viewModel.thisPage = this;
            //Messenger.Default.Register<string>(this, "wwwwww", msg =>
            //{

            //    foreach(MTask task in DMScheduleManage.INSTANCE._listTask)
            //    {
            //        lbTask.Items.Add(task);
            //    }
            //    //lbTask.ItemsSource = DMScheduleManage.INSTANCE._listTask;
            //});
            LoadData();
        }

        public void SetIsShowMainPage(bool isMainPage)
        {
            if (isMainPage)
            {
                _web.Width = 1590;
                gdTask.Visibility = Visibility.Visible;
            }
            else
            {
                _web.Width = 1920;
                gdTask.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadData()
        {
            _web.LoadCompleted += (o, e) => { _web.Visibility = System.Windows.Visibility.Visible; };
            try
            {
                //string strUri = string.Format("http://10.251.68.242/imp/web/tourMember/scheduleManage?logId={0}", DMUser.INSTANCE.CurrentMUser.UserAccount);
                //string strUri = string.Format("http://10.251.68.242/imp/web/tourMember/scheduleManage?logId={0}", DMUser.INSTANCE.CurrentMUser.UserAccount);
                string strUri = string.Format("http://{0}:{1}/imp/web/tourMember/scheduleManage?logId={2}",
                    HttpBase.m_strServerDomain, HttpBase.m_strServerPort, DMUser.INSTANCE.CurrentMUser.UserAccount);
                //string strUri = "http://10.48.64.51:8087/imp/web/tourMember/scheduleManage?logId=MSAD$CHENK";
                _web.Navigate(new Uri(strUri));
            }
            catch (Exception)
            {
                Debug.WriteLine("**************err******************加载网页错误");
            }
        }

        private void lbTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MTask task = lbTask.SelectedItem as MTask;
            if (task == null)
                return;
        }

        private void Viewbox_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }


    public class DataContextProxy : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            var parent = this.AssociatedObject as DependencyObject;
            var fe = this.AssociatedObject as FrameworkElement;
            while (parent != null)
            {
                if (parent.GetType() == typeof (ListBox))
                {
                    var context = parent as ListBox;
                    fe.DataContext = context.DataContext;
                    break;
                }
                parent = VisualTreeHelper.GetParent(parent) as DependencyObject;
            }
        }
    }
}