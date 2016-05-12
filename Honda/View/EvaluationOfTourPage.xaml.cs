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

    public partial class EvaluationOfTourPage : BasePage
    {
        /// <summary>
        /// 建议加分项页面
        /// </summary>
        private UnivesalEvaluationPage _evaluationPage;

        public EvaluationOfTourPage()
        {
            InitializeComponent();
            GetTotallScore();
            ((EvaluationOfTourVM) DataContext)._thisPage = this;
            Messenger.Default.Register<string>(this, GlobalValue._NAVIGATE_TO, msg => { NavigateToPage(msg); });

            Messenger.Default.Register<string>(this, GlobalValue.FIRST_FORM_NAVIGATE, msg =>
            {
                //listBox.SelectedIndex = 0;
                NavigateToPage("巡回员评价页面");
            });

            //通知每一次评分都会更新总分分数
            Messenger.Default.Register<string>(this, GlobalValue._UPDATE_TOTAL_SCORE, msg => { GetTotallScore(); });
            //设置内嵌的导航控件维护自己的导航的记录。
            mainFrame.JournalOwnership = JournalOwnership.OwnsJournal;
            //隐藏自带的导航按钮
            mainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        private void GetTotallScore()
        {
            double tourScore = 0;
            double tourTotal = 0;

            foreach (var item in DMUnivesalEvaluate.INSTANCE.DataBaseUniversal[DMStoreTour.INSTANCE.CurrentMStore.shopId])
            {
                tourScore += item._pageTourScore;
                tourTotal += item._pageTotalScore;
            }

            //把分数呈现给UI
            tbkCurrentScore.Text = tourScore.ToString();
            tbkTotalScore.Text = string.Format("({0})", tourTotal.ToString());
        }

        #region 导航到相应的页面

        /// <summary>
        /// 导航到页面
        /// </summary>
        /// <param name="pageName"></param>
        private void NavigateToPage(string pageName)
        {
            //ClearNavigateData();

            if (_evaluationPage == null)
            {
                _evaluationPage = new UnivesalEvaluationPage();
            }
            mainFrame.Navigate(_evaluationPage);
        }

        /// <summary>
        /// 清理mainFrame控件的导航堆栈里的数据
        /// </summary>
        public void ClearNavigateData()
        {
            while (mainFrame.CanGoBack)
            {
                mainFrame.RemoveBackEntry();
            }
        }

        #endregion
    }
}