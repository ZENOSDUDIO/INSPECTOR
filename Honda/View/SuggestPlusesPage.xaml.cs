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
    public partial class SuggestPlusesPage : BasePage
    {
        #region Var 、 construction Fun

        public JumpPlaneEnum jumptoPlane { get; set; }

        SuggestPlusesVM _viewModel;

        /// <summary>
        /// 防止构造函数加载完一次控件后，Load函数再次加载第二次
        /// </summary>
        bool isFirstInitForm;

        /// <summary>
        /// 需要显示的Grid的名字
        /// </summary>
        private string _needShowGridName;

        public SuggestPlusesPage(JumpPlaneEnum _jumptoPlane = JumpPlaneEnum.defaulted)
        {
            InitializeComponent();
            jumptoPlane = _jumptoPlane;
            InitPanel();
            _viewModel = (SuggestPlusesVM)(DataContext);

            Messenger.Default.Register<string>(this, GlobalValue._SUGGEST_PUSES_LOAD_DATA, msg =>
            {
                //根据所点击的按钮，返回所要显示的Grid的名字
                //优化
                if (!isFirstInitForm)
                {
                    if (jumptoPlane == JumpPlaneEnum.FiveStar)
                    {
                        jumptoPlane = JumpPlaneEnum.defaulted;
                        UCMyTabBtn.ShowPlane("五星级仓库评价表");
                    }
                    else
                    {
                        showGrid();
                    }


                }
                isFirstInitForm = false;
            });
        }
        #endregion

        #region 加分项和五星级仓库评价表的显示面板 加到自定控件中，根据用户点击显示相应的界面
        /// <summary>
        /// 将 加分项和五星级仓库评价表的显示面板 加到自定控件中，根据用户点击显示相应的界面
        /// </summary>
        void InitPanel()
        {
            isFirstInitForm = true;
            UCMyTabBtn.AddItem("加分项", gd1);
            UCMyTabBtn.AddItem("五星级仓库评价表", gd2);
            if (jumptoPlane == JumpPlaneEnum.FiveStar)
            {
                UCMyTabBtn.JumpPlaneName = "五星级仓库评价表";
                jumptoPlane = JumpPlaneEnum.defaulted;
            }

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

            switch (_needShowGridName)
            {
                case "gd1":
                    LoadSuggestPluses();
                    break;
                case "gd2":
                    LoadFiveStar();
                    break;

            }
        }

        #endregion


        #region 加载加分项表格

        /// <summary>
        /// 加载加分项表格
        /// </summary>
        void LoadSuggestPluses()
        {
            sp1.Children.Clear();
            if (_viewModel == null || _viewModel._currentPlusProject == null) return;
            foreach (MItem_Suggest_PlusProject _sugg in _viewModel._currentPlusProject.LstGroup)
            {
                ItemRowControl ctr = new ItemRowControl(ItemStyle.ITEM_STYLE_SUGGEST_A, _sugg);
                //跳到五星级仓库
                ctr.GoToAntherPlane(() =>
                {
                    UCMyTabBtn.ShowPlane("五星级仓库评价表");
                });
                ctr.IsHeighAuto = true;
                //跟新分数
                ctr.UpdateScore(() =>
                {
                    setPlusProject();
                });
                sp1.Children.Add(ctr);
            }

            setPlusProject();

        }

        /// <summary>
        /// 设置加分项的页面的分数
        /// </summary>
        void setPlusProject()
        {
            tbkEvaluationTourScore1.Text = _viewModel._currentPlusProject._pageTourScore.ToString();
            tbkSelfEvaluationScore1.Text = _viewModel._currentPlusProject._pageSelfScore.ToString();
            tbkLastScore1.Text = _viewModel._currentPlusProject._pageLastScore.ToString();
            NotificationUpdateScore();
        }

        #endregion


        #region 加载五星级仓库表格
        /// <summary>
        /// 加载五星级仓库表格
        /// </summary>
        void LoadFiveStar()
        {
            if (_viewModel == null || _viewModel._currentWarehouse == null) return;
            sp2.Children.Clear();

            foreach (M_Suggest_Warehouse_Group _group in _viewModel._currentWarehouse.LstGroup)
            {
                //三级组
                ItemPanel _groupPanel = new ItemPanel();
                _groupPanel.m_groupName = _group._EvaluationGroupContent;
                _groupPanel.setColumnRatio(2, 24);
                sp2.Children.Add(_groupPanel);

                foreach (M_Suggest_Warehouse_Level_Two _leveTwoGroup in _group.ListGroup)
                {
                    //四级组
                    ItemPanel _Level_two_Panel = new ItemPanel();
                    _Level_two_Panel.m_groupName = _leveTwoGroup._GroupName;
                    _Level_two_Panel.setColumnRatio(2, 22);
                    _groupPanel.AddItem(_Level_two_Panel);

                    //五级组
                    foreach (M_Suggest_Warehouse_Level_Three _leveThreeGroup in _leveTwoGroup)
                    {
                        ItemPanel _level_three_Panel = new ItemPanel();
                        _level_three_Panel.setColumnRatio(2, 20);
                        _level_three_Panel.m_groupName = _leveThreeGroup._GroupName;
                        _Level_two_Panel.AddItem(_level_three_Panel);

                        //最小组
                        foreach (MItem_Suggest_Warehouse cell in _leveThreeGroup)
                        {
                            //巡回评价分数默认与标准分数一致
                            if (cell._cellTourScore == 0)
                            {
                                cell._cellTourScore = cell._itemScore;
                            }

                            //检查巡回分数是否超标
                            if (cell._cellTourScore < 0 || cell._cellTourScore > cell._itemScore)
                            {
                                cell.bIsTourScoreOutOfRange = true;
                            }
                            else
                            {
                                cell.bIsTourScoreOutOfRange = false;
                            }

                            ItemRowControl item = new ItemRowControl(ItemStyle.ITEM_STYLE_SUGGEST_B, cell);

                            item.UpdateScore(() =>
                            {
                                SetFiveStarScore();
                            });

                            _level_three_Panel.AddItem(item);
                        }
                    }
                }


            }

            SetFiveStarScore();

        }

        void SetFiveStarScore()
        {
            tbkLastScore2.Text = _viewModel._currentWarehouse._pageLastScore.ToString();
            tbkSelfEvaluationScore2.Text = _viewModel._currentWarehouse._pageSelfScore.ToString();
            tbkEvaluationTourScore2.Text = _viewModel._currentWarehouse._pageTourScore.ToString();
            NotificationUpdateScore();
        }
        #endregion
    }


    /// <summary>
    /// 导航到此页面的时候需要跳到面板的类型
    /// </summary>
    public enum JumpPlaneEnum
    {
        /// <summary>
        /// 跳到加分项面板
        /// </summary>
        Pluses = 0,
        /// <summary>
        /// 跳到五星级评价表
        /// </summary>
        FiveStar = 1,
        /// <summary>
        /// 默认
        /// </summary>
        defaulted = 2
    }
}
