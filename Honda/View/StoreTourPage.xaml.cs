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
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class StoreTourPage : BasePage
    {
        public StoreTourPage()
        {
            InitializeComponent();
            ((StoreTourVM) DataContext)._thisPage = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigate;
            _navigate = NavigationService.GetNavigationService(this);

            WorkLightspotAndIdeaPage _workLightspotPage = new WorkLightspotAndIdeaPage();
            _navigate.Content = _workLightspotPage;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            NavigationService _navigate;
            _navigate = NavigationService.GetNavigationService(this);

            BusinessPolicyPage page = new BusinessPolicyPage();
            _navigate.Content = page;
        }

        private void Button_Click8(object sender, RoutedEventArgs e) //Button_Click9
        {
            NavigationService _navigate;
            _navigate = NavigationService.GetNavigationService(this);

            ImproveCheckPage page = new ImproveCheckPage();
            _navigate.Content = page;
        }

        private void Button_Click9(object sender, RoutedEventArgs e) //Button_Click
        {
            NavigationService _navigate;
            _navigate = NavigationService.GetNavigationService(this);

            ImprovePage page = new ImprovePage();
            _navigate.Content = page;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService _navigate = NavigationService.GetNavigationService(this);

            EvaluationOfTourPage _evalutionPage = new EvaluationOfTourPage();
            _navigate.Content = _evalutionPage;
        }
    }
}