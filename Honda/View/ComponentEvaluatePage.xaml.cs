using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Interface;
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
    public partial class ComponentEvaluatePage : BasePage
    {
        #region Var、Constration Fun
        public EvaluationOfTourPage evaluationOfTourPage;

        ComponentEvaluateVM _ViewModel;
        /// <summary>
        /// 需要显示的Grid的名字
        /// </summary>
        private string _needShowGridName;

        /// <summary>
        /// 防止构造函数加载完一次控件后，Load函数再次加载第二次
        /// </summary>
        bool isFirstInitForm;       

        public ComponentEvaluatePage()
        {
            InitializeComponent();
            InitPanel();
            _ViewModel = (ComponentEvaluateVM)DataContext;

            Messenger.Default.Register<string>(this, GlobalValue._COMPONENT_LOAD_DATA, msg =>
            {
                LoadBaseBusinessControl();
                LoadMarMarketingManageControl();
                LoadRepertoryManageControl();
                LoadStoreManagementControl();
                LoadPersonnel();
                //优化
                if (!isFirstInitForm)
                {
                    showGrid();
                }
                isFirstInitForm = false;
            }); 
        }

        #endregion

        #region 把基础业务、营销管理、库存管理、仓库管理和人员管理的显示面板，加到自定义控件中，根据用户的点击切换界面
        /// <summary>
        /// 把基础业务、营销管理、库存管理、仓库管理和人员管理的显示面板，加到自定义控件中，根据用户的点击切换界面
        /// </summary>
        void InitPanel()
        {
            isFirstInitForm = true;
            UCMyTabBtn.AddItem("基础业务", gd1);
            UCMyTabBtn.AddItem("营销管理", gd2);
            UCMyTabBtn.AddItem("库存管理", gd3);
            UCMyTabBtn.AddItem("仓库管理", gd4);
            UCMyTabBtn.AddItem("人员管理", gd5);
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
            sp5.Children.Clear();

            switch (_needShowGridName)
            {
                case "gd1":
                    LoadBaseBusinessControl();
                    break;
                case "gd2":
                    LoadMarMarketingManageControl();
                    break;
                case "gd3":
                    LoadRepertoryManageControl();
                    break;
                case "gd4":
                    LoadStoreManagementControl();
                    break;
                case "gd5":
                    LoadPersonnel();
                    break;
                
            }
        }
        #endregion

        #region 加载基础业务
        /// <summary>
        /// 加载基础业务模块表格
        /// </summary>
        void LoadBaseBusinessControl()
        {
            if (_ViewModel == null || _ViewModel._currentBaseBusiness == null) return;
            sp1.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ViewModel._currentBaseBusiness.ListGroup)
            {
                SetLoadForm(_Group, SetBaseBusinessPageScore, sp1);
            }
            SetBaseBusinessPageScore();

        }

        void SetBaseBusinessPageScore()
        {
            tbkEvaluationTourScore1.Text = _ViewModel._currentBaseBusiness._pageTourScore.ToString();
            tbkSelfEvaluationScore1.Text = _ViewModel._currentBaseBusiness._pageSelfScore.ToString();
            tbkLastScore1.Text = _ViewModel._currentBaseBusiness._pageLastScore.ToString();
            NotificationUpdateScore();
        }
        #endregion

        #region 营销管理
        /// <summary>
        /// 加载营销管理模块表格
        /// </summary>
        void LoadMarMarketingManageControl()
        {
            if (_ViewModel == null || _ViewModel._currentMarketingManage == null) return;
            sp2.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ViewModel._currentMarketingManage.ListGroup)
            {
                SetLoadForm(_Group, SetMarMarketingManagePageScore, sp2);
            }
            SetMarMarketingManagePageScore();

        }

        void SetMarMarketingManagePageScore()
        {
            tbkEvaluationTourScore2.Text = _ViewModel._currentMarketingManage._pageTourScore.ToString();
            tbkSelfEvaluationScore2.Text = _ViewModel._currentMarketingManage._pageSelfScore.ToString();
            tbkLastScore2.Text = _ViewModel._currentMarketingManage._pageLastScore.ToString();
            NotificationUpdateScore();
        }
        #endregion

        #region 库存管理
        /// <summary>
        /// 加库存管理模块表格
        /// </summary>
        void LoadRepertoryManageControl()
        {
            if (_ViewModel == null || _ViewModel._currentRepertoryManage == null) return;
            sp3.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ViewModel._currentRepertoryManage.ListGroup)
            {
                SetLoadForm(_Group, SetRepertoryManagePageScore, sp3);
            }
            SetRepertoryManagePageScore();

        }

        void SetRepertoryManagePageScore()
        {
            tbkEvaluationTourScore3.Text = _ViewModel._currentRepertoryManage._pageTourScore.ToString();
            tbkSelfEvaluationScore3.Text = _ViewModel._currentRepertoryManage._pageSelfScore.ToString();
            tbkLastScore3.Text = _ViewModel._currentRepertoryManage._pageLastScore.ToString();
            NotificationUpdateScore();
        }
        #endregion

        #region 仓库管理
        /// <summary>
        /// 仓库管理模块表格
        /// </summary>
        void LoadStoreManagementControl()
        {
            if (_ViewModel == null || _ViewModel._currentStoreManagement == null) return;
            sp4.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ViewModel._currentStoreManagement.ListGroup)
            {
                SetLoadForm(_Group, SetStoreManagementPageScore, sp4);
            }
            SetStoreManagementPageScore();

        }

        void SetStoreManagementPageScore()
        {
            tbkEvaluationTourScore4.Text = _ViewModel._currentStoreManagement._pageTourScore.ToString();
            tbkSelfEvaluationScore4.Text = _ViewModel._currentStoreManagement._pageSelfScore.ToString();
            tbkLastScore4.Text = _ViewModel._currentStoreManagement._pageLastScore.ToString();
            NotificationUpdateScore();
        }
        #endregion

        #region 人员管理
       

        #region 加载人员表格的类型
        void LoadPersonnel()
        {
            if (_ViewModel == null && _ViewModel._currentPersnnel == null && _ViewModel._currentPersnnel.LstGroup == null) return;
            sp5.Children.Clear();
            foreach (IPersonnelGroup _group in _ViewModel._currentPersnnel.LstGroup)
            {
                switch (_group._enum_personel_group_form) //根据表格的类型开始选择要加载的表格的
                {
                    case _Enum_Personel_Group_Form._configuration:

                        M_Personnel_Configuration_Group _configuraton_group = (M_Personnel_Configuration_Group)_group;
                        LoadConfiguration(_configuraton_group);
                        break;

                    case _Enum_Personel_Group_Form._evaluation:

                        M_Personnel_Evaluation_Group _evaluation_group = (M_Personnel_Evaluation_Group)_group;
                        LoadEvaluation(_evaluation_group);
                        break;

                    case _Enum_Personel_Group_Form._train:
                        M_Personnel_Train_Group _train_group = (M_Personnel_Train_Group)_group;
                        LoadTrain(_train_group);
                        break;
                }
            }

            SetPersonnelScore();

        }

        /// <summary>
        /// 加载配置人员类型的表格
        /// </summary>
        /// <param name="_configuraton_group"></param>
        void LoadConfiguration(M_Personnel_Configuration_Group _configuraton_group)
        {
            //设置该组底部的分数Bar
            ScoreBottomBar scorebar = new ScoreBottomBar();
            scorebar.SetColumnScole(9.444444, 1, 1, 2, 1);
            scorebar.SetScore(_configuraton_group._level_One_LastScore, _configuraton_group._level_One_SelfScore, _configuraton_group._level_One_TourScore);
            scorebar._EvaluationCriterion = _configuraton_group._EvaluationCriterion;
            scorebar._InspectionMethod = _configuraton_group._InspectionMethod;

            //设置这一小组的数据
            ItemPanel itemPanel = new ItemPanel();
            //设置两列的宽度的比例
            itemPanel.setColumnRatio(2, 18);
            itemPanel.m_groupName = _configuraton_group._EvaluationGroupContent;
            itemPanel.m_score = _configuraton_group._GroupTotalScore.ToString();  //设置该组的总分数

            foreach (IPersonnelGroupConfiguration _cell in _configuraton_group.ListGroup)
            {
                ItemRowControl itemControl = null;
                if (_cell._enum_personel_group_configuration_form == _Enum_Personel_Group_Configuration_Form._machineDescribe)
                {
                    // 加载描述机器类型的基础表格

                    MItem_personnel_A _machineItem = (MItem_personnel_A)_cell;
                    itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNEL_A, _machineItem);

                    //更新分数
                    itemControl.UpdateScore(() =>
                    {
                        SetPersonnelScore();
                        scorebar.SetScore(_configuraton_group._level_One_LastScore, _configuraton_group._level_One_SelfScore, _configuraton_group._level_One_TourScore);
                    });

                }
                else if (_cell._enum_personel_group_configuration_form == _Enum_Personel_Group_Configuration_Form._postDescribe)
                {
                    // 加载岗位描述类型的基础表格
                    MItem_personnel_B _postItem = (MItem_personnel_B)_cell;
                    itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNEL_B, _postItem);

                    //更新分数
                    itemControl.UpdateScore(() =>
                    {
                        SetPersonnelScore();
                        scorebar.SetScore(_configuraton_group._level_One_LastScore, _configuraton_group._level_One_SelfScore, _configuraton_group._level_One_TourScore);
                    });
                }
                else if(_cell._enum_personel_group_configuration_form == _Enum_Personel_Group_Configuration_Form.other)
                {
                    MItem_FiveSAndSafe fives = _cell as MItem_FiveSAndSafe;
                    if (fives == null) return;
                    itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNELL_OF_FIVES_AND_SAFE, fives);

                    //更新分数
                    itemControl.UpdateScore(() =>
                    {
                        SetPersonnelScore();
                        scorebar.SetScore(_configuraton_group._level_One_LastScore, _configuraton_group._level_One_SelfScore, _configuraton_group._level_One_TourScore);
                    });
                }

                itemPanel.AddItem(itemControl);
            }
            sp5.Children.Add(itemPanel);
            sp5.Children.Add(scorebar);
        }


        /// <summary>
        /// 加载人员能力评估类型的表格
        /// </summary>
        /// <param name="_evaluation_group"></param>
        void LoadEvaluation(M_Personnel_Evaluation_Group _evaluation_group)
        {
            //设置小组底部的分数bar
            ScoreBottomBar scorebar = new ScoreBottomBar();
            scorebar.SetColumnScole(9.444444, 1, 1, 2, 1);
            scorebar.SetScore(_evaluation_group._level_One_LastScore, _evaluation_group._level_One_SelfScore, _evaluation_group._level_One_TourScore);
            scorebar._InspectionMethod = _evaluation_group._InspectionMethod;
            scorebar._EvaluationCriterion = _evaluation_group._EvaluationCriterion;

            //设置这一小组的数据
            ItemPanel itemPanel = new ItemPanel();
            //设置两列的宽度的比例
            itemPanel.setColumnRatio(2, 18);
            itemPanel.m_groupName = _evaluation_group._EvaluationGroupContent;
            itemPanel.m_score = _evaluation_group._GroupTotalScore.ToString();

            foreach (MItem_personnel_C _personnel in _evaluation_group.ListGroup)
            {
                ItemRowControl itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNEL_C, _personnel);
                itemControl.IsHeighAuto = true;
                //更新分数
                itemControl.UpdateScore(() =>
                {
                    SetPersonnelScore();
                    scorebar.SetScore(_evaluation_group._level_One_LastScore, _evaluation_group._level_One_SelfScore, _evaluation_group._level_One_TourScore);
                });

                itemPanel.AddItem(itemControl);
            }

            sp5.Children.Add(itemPanel);
            sp5.Children.Add(scorebar);

        }

        /// <summary>
        /// 加载人员培训类型的表格
        /// </summary>
        void LoadTrain(M_Personnel_Train_Group _train_group)
        {
            //设置小组底部的分数bar
            ScoreBottomBar scorebar = new ScoreBottomBar();
            scorebar.SetColumnScole(9.444444, 1, 1, 2, 1);
            scorebar.SetScore(_train_group._level_One_LastScore, _train_group._level_One_SelfScore, _train_group._level_One_TourScore);
            scorebar._EvaluationCriterion = _train_group._EvaluationCriterion;
            scorebar._InspectionMethod = _train_group._InspectionMethod;

            //设置这一小组的数据
            ItemPanel itemPanel = new ItemPanel();
            //设置两列的宽度的比例
            itemPanel.setColumnRatio(2, 18);
            itemPanel.m_groupName = _train_group._EvaluationGroupContent;
            itemPanel.m_score = _train_group._GroupTotalScore.ToString();

            foreach (MItem_FiveSAndSafe _fiveS in _train_group.ListGroup)
            {
                ItemRowControl itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNELL_OF_FIVES_AND_SAFE, _fiveS);
                itemControl.IsHeighAuto = true;
                //更新分数
                itemControl.UpdateScore(() =>
                {
                    SetPersonnelScore();
                    scorebar.SetScore(_train_group._level_One_LastScore, _train_group._level_One_SelfScore, _train_group._level_One_TourScore);
                });

                itemPanel.AddItem(itemControl);

            }

            sp5.Children.Add(itemPanel);
            sp5.Children.Add(scorebar);
        }

        /// <summary>
        /// 更新人员页面的分数数值
        /// </summary>
        void SetPersonnelScore()
        {
            tbkLastScore5.Text = _ViewModel._currentPersnnel._pageLastScore.ToString();
            tbkSelfEvaluationScore5.Text = _ViewModel._currentPersnnel._pageSelfScore.ToString();
            tbkEvaluationTourScore5.Text = _ViewModel._currentPersnnel._pageTourScore.ToString();
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
            bottomBar._InspectionMethod = _Group._InspectionMethod;
            bottomBar._EvaluationCriterion = _Group._EvaluationCriterion;

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

                //跳到五星级仓库
                itemControl.GoToAntherPlane(() =>
                {
                    NavigationService _navigate = NavigationService.GetNavigationService(this);
                    while (_navigate.CanGoBack)
                    {
                        _navigate.RemoveBackEntry();
                    }

                    if (evaluationOfTourPage != null)
                    {
                        evaluationOfTourPage.JumpToFiveStar();
                    }
                    
                });
                itemGroup.AddItem(itemControl);
            }

            sp.Children.Add(itemGroup);
            sp.Children.Add(bottomBar);
        }
        #endregion
    }
}
