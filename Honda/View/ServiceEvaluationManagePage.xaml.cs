using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model.Form;
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
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class ServiceEvaluationManagePage : BasePage
    {
        #region Var 、 construction Fun
        
        ServiceEvaluationManageVM _ViewModel;

        /// <summary>
        /// 防止构造函数加载完一次控件后，Load函数再次加载第二次
        /// </summary>
        bool isFirstInitForm;

        /// <summary>
        /// 需要显示的Grid的名字
        /// </summary>
        private string _needShowGridName;

        public ServiceEvaluationManagePage()
        {
            InitializeComponent();
            _ViewModel = (ServiceEvaluationManageVM)DataContext;
            InitPanel();            
            Messenger.Default.Register<string>(this, GlobalValue._SERVICE_MANAGEMENT_LOAD_DATA, msg =>
            {
                //优化
                if (!isFirstInitForm)
                {
                    showGrid();
                }
                isFirstInitForm = false;
            }); 
        }
        #endregion

        #region 初始化 满意度管理、客户维系管理、来厂促进管理和重点业务的显示面板
        /// <summary>
        /// 初始化 满意度管理、客户维系管理、来厂促进管理和重点业务的显示面板
        /// </summary>
        void InitPanel()
        {
            isFirstInitForm = true;
            UCMyTabBtn.AddItem("满意度管理", gd1);
            UCMyTabBtn.AddItem("客户维系管理", gd2);
            UCMyTabBtn.AddItem("来厂促进管理", gd3);
            UCMyTabBtn.AddItem("重点业务", gd4);

            //根据所点击的按钮，返回所要显示的Grid的名字
            UCMyTabBtn.SetShowGridName((gdName) =>
            {
                _needShowGridName = gdName;
                showGrid();
            });

        }

        /// <summary>
        /// 显示Grid，并把其他隐藏的Grid的中的子控件清理掉
        /// </summary>
        void showGrid()
        {
            sp1.Children.Clear();
            sp2.Children.Clear();
            sp3.Children.Clear();
            sp4.Children.Clear();
            switch (_needShowGridName)
            {
                case "gd1":
                    LoadSatisfactionManagementControl();
                    break;
                case "gd2":
                    LoadClientManagementControl();
                    break;
                case "gd3":
                    LoadfactoryManagementControl();
                    break;
                case "gd4":
                    LoadEmphasisBusinessControl();
                    break;               
            }
        }
        #endregion


        #region 加载满意度管理模块表格

        /// <summary>
        /// 加载满意度管理模块表格
        /// </summary>
        void LoadSatisfactionManagementControl()
        {
            if (_ViewModel == null || _ViewModel._currentSatisfactionManagement == null) return;
            sp1.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ViewModel._currentSatisfactionManagement.ListGroup)
            {
                SetLoadForm(_Group, SetReceiveGuestsFlowPageScore,sp1);  
            }
            SetReceiveGuestsFlowPageScore();

        }        

        void SetReceiveGuestsFlowPageScore()
        {
            tbkEvaluationTourScore1.Text = _ViewModel._currentSatisfactionManagement._pageTourScore.ToString();
            tbkSelfEvaluationScore1.Text = _ViewModel._currentSatisfactionManagement._pageSelfScore.ToString();
            tbkLastScore1.Text = _ViewModel._currentSatisfactionManagement._pageLastScore.ToString();
            NotificationUpdateScore();
        }

        #endregion

        #region 加载客户维系管理模块表格
        /// <summary>
        /// 加载客户维系管理模块表格
        /// </summary>
        void LoadClientManagementControl()
        {
            if (_ViewModel == null || _ViewModel._currentClientMange == null) return;
            sp2.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ViewModel._currentClientMange.ListGroup)
            {
                SetLoadForm(_Group, SetClientManagementScore, sp2);
            }
            SetClientManagementScore();

        }

        void SetClientManagementScore()
        {
            tbkEvaluationTourScore2.Text = _ViewModel._currentClientMange._pageTourScore.ToString();
            tbkSelfEvaluationScore2.Text = _ViewModel._currentClientMange._pageSelfScore.ToString();
            tbkLastScore2.Text = _ViewModel._currentClientMange._pageLastScore.ToString();
            NotificationUpdateScore();
        }
        #endregion


        #region 加载来厂促进管理模块表格

        /// <summary>
        /// 加载来厂促进管理模块表格
        /// </summary>
        void LoadfactoryManagementControl()
        {
            if (_ViewModel == null || _ViewModel._currentMangement == null) return;
            sp3.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ViewModel._currentMangement.ListGroup)
            {
                SetLoadForm(_Group, SetfactoryManagementScore, sp3);
            }
            SetfactoryManagementScore();

        }

        void SetfactoryManagementScore()
        {
            tbkEvaluationTourScore3.Text = _ViewModel._currentMangement._pageTourScore.ToString();
            tbkSelfEvaluationScore3.Text = _ViewModel._currentMangement._pageSelfScore.ToString();
            tbkLastScore3.Text = _ViewModel._currentMangement._pageLastScore.ToString();
            NotificationUpdateScore();
        }
        #endregion


        #region 加载重点业务模块表格

        /// <summary>
        /// 加载重点业务模块表格
        /// </summary>
        void LoadEmphasisBusinessControl()
        {
            if (_ViewModel == null || _ViewModel._currentEmphasisBusiness == null) return;
            sp4.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ViewModel._currentEmphasisBusiness.ListGroup)
            {
                SetLoadForm(_Group, SetEmphasisBusinessScore, sp4);
            }
            SetEmphasisBusinessScore();

        }

        void SetEmphasisBusinessScore()
        {
            tbkEvaluationTourScore4.Text = _ViewModel._currentEmphasisBusiness._pageTourScore.ToString();
            tbkSelfEvaluationScore4.Text = _ViewModel._currentEmphasisBusiness._pageSelfScore.ToString();
            tbkLastScore4.Text = _ViewModel._currentEmphasisBusiness._pageLastScore.ToString();
            NotificationUpdateScore();
        }
        #endregion

        /// <summary>
        /// 加载一组表格的数据
        /// </summary>
        /// <param name="_Group">这一组表格的数据List</param>
        /// <param name="updateScore"> 回掉函数，当操作分数区域时更新代码</param>
        /// <param name="sp">需要添加表格的面板</param>
        void SetLoadForm(M_Common_Groupcs _Group, Action updateScore, StackPanel sp)
        {
            ItemPanel itemGroup = new ItemPanel();
            itemGroup.m_groupName = _Group._EvaluationGroupContent;
            itemGroup.m_score = _Group._GroupTotalScore.ToString();

            //设置显示这一组的分数的bar
            ScoreBottomBar bottomBar = new ScoreBottomBar();
            bottomBar.SetColumnScole(859, 142, 142, 284, 142);
            bottomBar.SetScore(_Group._level_One_LastScore, _Group._level_One_SelfScore, _Group._level_One_TourScore);
            bottomBar._EvaluationCriterion = _Group._EvaluationCriterion;
            bottomBar._InspectionMethod = _Group._InspectionMethod;

            foreach (MItem_FiveSAndSafe _item in _Group.LstItem)
            {
                ItemRowControl itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_FIVES_AND_SAFE, _item);
                itemControl.IsHeighAuto = true;
                //刷新分数
                itemControl.UpdateScore(() =>
                {
                    updateScore();
                    bottomBar.SetScore(_Group._level_One_LastScore, _Group._level_One_SelfScore, _Group._level_One_TourScore);
                    
                });
                itemGroup.AddItem(itemControl);
            }

            sp.Children.Add(itemGroup);
            sp.Children.Add(bottomBar);
        }



    }
}
