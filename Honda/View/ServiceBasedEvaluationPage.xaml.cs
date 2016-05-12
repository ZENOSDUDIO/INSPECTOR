using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Interface;
using Honda.Model.Form;
using Honda.UserCtrl;
using Honda.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Drawing;
using Honda.View.BaseView;

namespace Honda.View
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class ServiceBasedEvaluationPage : BasePage
    {
        #region Var

         ServiceBasedEvaluationVM _ServiceViewModel;

        /// <summary>
        /// 需要显示的Grid的名字
        /// </summary>
        private string _needShowGridName;

        /// <summary>
        /// 防止构造函数加载完一次控件后，Load函数再次加载第二次
        /// </summary>
        bool isFirstInitForm;
        #endregion


        public ServiceBasedEvaluationPage()
        {
            InitializeComponent();
            _ServiceViewModel = (ServiceBasedEvaluationVM)DataContext;
            InitPanel();
            //load 事件时ViewModel发回来的信息
            Messenger.Default.Register<string>(this, GlobalValue._EVALUATE_LOAD_DATA, msg =>
            {
                //优化
                if (!isFirstInitForm)
                {
                    showGrid();
                }
                isFirstInitForm = false;
            });             
        }

        #region 把 5S&安全、硬件、人员、接待流程、快修流程、BP流程、数据准确性的显示面板 ，加到自定的Tab控件中，根据用户点击，切换界面
        /// <summary>
        /// 把 5S&安全、硬件、人员、接待流程、快修流程、BP流程、数据准确性的显示面板 ，加到自定的Tab控件中，根据用户点击，切换界面
        /// </summary>
        void InitPanel()
        {
            isFirstInitForm = true;
            UCMyTabBtn.AddItem("5S&安全", gd1);
            UCMyTabBtn.AddItem("硬件", gd2);
            UCMyTabBtn.AddItem("人员", gd3);
            UCMyTabBtn.AddItem("接待流程", gd4);
            UCMyTabBtn.AddItem("快修流程", gd5);
            UCMyTabBtn.AddItem("BP流程", gd6);
            UCMyTabBtn.AddItem("数据准确性", gd7);

            //根据所点击的按钮，返回所要显示的Grid的名字
            UCMyTabBtn.SetShowGridName((gdName) => {
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
            sp6.Children.Clear();
            sp7.Children.Clear();
            switch (_needShowGridName)
            {
                case "gd1":
                    LoadFiveSControl();
                    break;
                case "gd2":
                    LoadHardwareControl();
                    break;
                case "gd3":
                    LoadPersonnel();
                    break;
                case "gd4":
                    LoadReceiveGuestsFlowControl();
                    break;
                case "gd5":
                    LoadQuickServiceControl();
                    break;
                case "gd6":

                    LoadBpFlowControl();
                    break;
                case "gd7":
                    LoadM_DataAccuracySourceControl();
                    break;

            }
        }
        #endregion

        

        #region 加载5S&安全 类型的表格
        
        /// <summary>
        /// 加载5S&安全 类型的表格
        /// </summary>
        void LoadFiveSControl()
        {
            sp1.Children.Clear();
            M_FiveSAndSafes_Source Source = _ServiceViewModel._currentFiveSAndSafesSource;
            if (_ServiceViewModel != null && Source != null)
            {
                //ItemPanel
               
                for (int i = 0; i < Source.LstGroup.Count; i++)
                {
                    ItemPanel item = new ItemPanel();
                    item.m_groupName = Source.LstGroup[i].GroupName;
                    item.m_score = Source.LstGroup[i].TotalScore.ToString();

                    foreach (MItem_FiveSAndSafe fives in Source.LstGroup[i])
                    {
                        ItemRowControl trc = new ItemRowControl(ItemStyle.ITEM_STYLE_FIVES_AND_SAFE, fives);

                        //点击操作区域更新分数
                        trc.UpdateScore(() => {
                            double score1 = _ServiceViewModel._currentFiveSAndSafesSource._pageTourScore;
                            tbkEvaluationTourScore1.Text = score1.ToString();
                            NotificationUpdateScore();
                        });
                        item.AddItem(trc);
                    }
                    sp1.Children.Add(item);
                }

                //给页面设置分数
                tbkEvaluationTourScore1.Text = Source._pageTourScore.ToString();
                tbkLastScore1.Text = Source._pageLastScore.ToString();
                tbkSelfEvaluationScore1.Text = Source._pageSelfScore.ToString();

            }

          
        }
        #endregion


        #region 加载硬件表格 （这个表格比较复杂，首先分为两大类型的数据组，非工具类型和工具类型的，其中工具类型的有分为： 快修工具类型、钣喷工具、机修工具类型）

        /// <summary>
        /// 硬件中所有工具类型的集合
        /// </summary>
        M_Hardware_TOOL_Group m_Tool_Group;

        /// <summary>
        /// 加载硬件类型的表格
        /// </summary>
        void LoadHardwareControl()
        {
            sp2.Children.Clear();
            M_Hardware_Source Source = _ServiceViewModel._currentHardware;
            if (Source == null) return;
            foreach (IHardware hardware in Source.LstHardware)
            {
                if(hardware._Enum_hardware_Typle == Hardware_Form_Typle._NO_TOOL_STYLE) //如果是非工具类型的
                {
                    M_Hardware_NO_TOOL_Group _no_tool_group = (M_Hardware_NO_TOOL_Group)hardware;
                    LoadHardwareNoTool(_no_tool_group);
                }
                else
                {
                    M_Hardware_TOOL_Group tool_group = (M_Hardware_TOOL_Group)hardware;
                    m_Tool_Group = tool_group;
                    LoadHardwareTool(tool_group);
                }
            }

            UpdateHardwarePageScore();
        }

        /// <summary>
        /// 加载非工具类型
        /// </summary>
        void LoadHardwareNoTool(M_Hardware_NO_TOOL_Group no_tool_group)
        {
            if (no_tool_group == null) return;

            //设置这个小组底部的分数Bar
            ScoreBottomBar bottomBar = new ScoreBottomBar();
            bottomBar._EvaluationCriterion = no_tool_group._EvaluationCriterion;
            bottomBar._InspectionMethod = no_tool_group._InspectionMethod;

            //设置组表格
            ItemPanel item = new ItemPanel();
            item.setColumnRatio(4, 36);
            item.m_groupName = no_tool_group._EvaluationGroupContent;
            item.m_score = no_tool_group._GroupTotalScore.ToString();

            foreach (MItem_FiveSAndSafe temp in no_tool_group.LstItem)
            {

                ItemRowControl trc = new ItemRowControl(ItemStyle.ITEM_STYLE_TOOL_OF_FIVES_AND_SAFE_A, temp);
                trc.IsHeighAuto = true;

                //更新分数
                trc.UpdateScore(() => {
                    bottomBar.SetScore(no_tool_group._level_One_LastScore, no_tool_group._level_One_SelfScore, no_tool_group._level_One_TourScore);
                    UpdateHardwarePageScore();
                });
                item.AddItem(trc);
            }

            //设置这一小组的分数
            bottomBar.SetScore(no_tool_group._level_One_LastScore, no_tool_group._level_One_SelfScore, no_tool_group._level_One_TourScore);
            sp2.Children.Add(item);
            sp2.Children.Add(bottomBar);

        }



        /// <summary>
        /// 显示工具类型所有评分数值
        /// </summary>
        ScoreBottomBar _toolBottomBar ;

        /// <summary>
        /// 加载工具类型
        /// </summary>
        /// <param name="tool_group"></param>
        void LoadHardwareTool(M_Hardware_TOOL_Group tool_group)
        {
            if (tool_group == null) return;

            //设置这一小组的分数bar
            _toolBottomBar = new ScoreBottomBar();
            _toolBottomBar._EvaluationCriterion = tool_group._EvaluationCriterion;
            _toolBottomBar._InspectionMethod = tool_group._InspectionMethod;

            ItemPanel _itemPanel = new ItemPanel();
            _itemPanel.m_groupName = tool_group._EvaluationGroupContent;
            _itemPanel.m_score = tool_group._GroupTotalScore.ToString();
            _itemPanel.setColumnRatio(4, 36);

            foreach(IHardwareTool _tool_group in tool_group.LstItem)
            {
                switch(_tool_group._hardwareTool_Form_Typle)
                {
                    case HardwareTool_Form_Typle._TOOL_QUICK:
                        M_Hardware_TOOL_Level_Two_A _tool_level_two_a = (M_Hardware_TOOL_Level_Two_A)_tool_group;

                        LoadHardwareToolA(_tool_level_two_a, _itemPanel);

                        break;

                    case HardwareTool_Form_Typle._TOOL_SHEET:
                        M_Hardware_TOOL_Level_Two_B _tool_level_two_b = (M_Hardware_TOOL_Level_Two_B)_tool_group;
                        LoadHardwareToolB(_tool_level_two_b, _itemPanel);

                        break;

                    case HardwareTool_Form_Typle._TOOL_MACHINE:
                        M_Hardware_TOOL_Level_Two_C _tool_level_two_c = (M_Hardware_TOOL_Level_Two_C)_tool_group;
                        LoadHardwareToolC(_tool_level_two_c, _itemPanel);

                        break;
                }
            }
            _toolBottomBar.SetScore(m_Tool_Group._level_One_LastScore,m_Tool_Group._level_One_SelfScore,m_Tool_Group._level_One_TourScore);
            sp2.Children.Add(_itemPanel);
            sp2.Children.Add(_toolBottomBar);

        }

        void LoadHardwareToolA(M_Hardware_TOOL_Level_Two_A tool_level_two_a, ItemPanel _itemPanel)
        {
            ItemPanel_Style2 item = new ItemPanel_Style2(tool_level_two_a._toolGroupName, tool_level_two_a._number);
            item.setColumnRatio(3, 3,30);
            foreach (MItem_FiveSAndSafe _five_tool in tool_level_two_a)
            {
                ItemRowControl trc = new ItemRowControl(ItemStyle.ITEM_STYLE_TOOL_OF_FIVES_AND_SAFE_B, _five_tool);
                trc.IsHeighAuto = true;
                //更新分数
                trc.UpdateScore(() => {
                    UpdateHardwarePageScore();
                    _toolBottomBar.SetScore(m_Tool_Group._level_One_LastScore, m_Tool_Group._level_One_SelfScore, m_Tool_Group._level_One_TourScore);
                });

                item.AddItem(trc);
            }
            _itemPanel.AddItem(item);
            
        }

        
        void LoadHardwareToolB(M_Hardware_TOOL_Level_Two_B tool_level_two_b, ItemPanel _itemPanel)
        {
            toolBottomBar toolBar = new toolBottomBar();
            _itemPanel.AddItem(toolBar);
            
            ItemPanel_Style2 item = new ItemPanel_Style2(tool_level_two_b._ToolGroupName, tool_level_two_b._Number);
            item.setColumnRatio(3, 3, 30);

            foreach (MItem_Tool_A _tool_a in tool_level_two_b)
            {
                ItemRowControl trc = new ItemRowControl(ItemStyle.ITEM_STYLE_TOOL_A, _tool_a);
                trc.IsHeighAuto = true;

                trc.UpdateScore(() => {
                    UpdateHardwarePageScore();
                    _toolBottomBar.SetScore(m_Tool_Group._level_One_LastScore, m_Tool_Group._level_One_SelfScore, m_Tool_Group._level_One_TourScore);
                });
                item.AddItem(trc);
            }
            _itemPanel.AddItem(item);
        }

        void LoadHardwareToolC(M_Hardware_TOOL_Level_Two_C tool_level_two_c, ItemPanel _itemPanel)
        {
            ItemPanel_Style2 item = new ItemPanel_Style2(tool_level_two_c._toolGroupName, tool_level_two_c._number);
            item.setColumnRatio(3, 3, 30);
            foreach (M_Tools_B _tool_b in tool_level_two_c)
            {
                ItemPanel _panel = new ItemPanel();
                _panel.m_groupName = _tool_b._deviceGroupName;
                _panel.setColumnRatio(5, 25);
                foreach(MItem_Tool_B _tool in _tool_b)
                {
                    ItemRowControl trc = new ItemRowControl(ItemStyle.ITEM_STYLE_TOOL_B, _tool);
                    trc.IsHeighAuto = true;

                    //更新分数
                    trc.UpdateScore(() => {
                        UpdateHardwarePageScore();
                        _toolBottomBar.SetScore(m_Tool_Group._level_One_LastScore, m_Tool_Group._level_One_SelfScore, m_Tool_Group._level_One_TourScore);
                    });
                    _panel.AddItem(trc);
                }

                item.AddItem(_panel);
            }

            _itemPanel.AddItem(item);

        }

        /// <summary>
        /// 更新硬件页面上的分数
        /// </summary>
        void UpdateHardwarePageScore()
        {
            tbkEvaluationTourScore2.Text = _ServiceViewModel._currentHardware._pageTourScore.ToString();
            tbkSelfEvaluationScore2.Text = _ServiceViewModel._currentHardware._pageSelfScore.ToString();
            tbkLastScore2.Text = _ServiceViewModel._currentHardware._pageLastScore.ToString();
            NotificationUpdateScore();
        }
      
        #endregion


        #region 加载人员表格的类型
        void LoadPersonnel()
        {
            if (_ServiceViewModel == null && _ServiceViewModel._currentPersnnel == null && _ServiceViewModel._currentPersnnel.LstGroup == null) return;
            sp3.Children.Clear();
            foreach(IPersonnelGroup _group in _ServiceViewModel._currentPersnnel.LstGroup)
            {
                switch(_group._enum_personel_group_form) //根据表格的类型开始选择要加载的表格的
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
            itemPanel.setColumnRatio(2,18);
            itemPanel.m_groupName = _configuraton_group._EvaluationGroupContent;
            itemPanel.m_score = _configuraton_group._GroupTotalScore.ToString();

            foreach(IPersonnelGroupConfiguration _cell in _configuraton_group.ListGroup)
            {
                ItemRowControl itemControl = null;
                if(_cell._enum_personel_group_configuration_form == _Enum_Personel_Group_Configuration_Form._machineDescribe)
                {
                    // 加载描述机器类型的基础表格

                    MItem_personnel_A _machineItem = (MItem_personnel_A)_cell;
                    itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNEL_A,_machineItem);
                    
                    //更新分数
                    itemControl.UpdateScore(() => {
                        SetPersonnelScore();
                    });

                }else if(_cell._enum_personel_group_configuration_form == _Enum_Personel_Group_Configuration_Form._postDescribe)
                {
                    // 加载岗位描述类型的基础表格
                    MItem_personnel_B _postItem = (MItem_personnel_B)_cell;
                    itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNEL_B,_postItem);

                    //更新分数
                    itemControl.UpdateScore(() =>
                    {
                        SetPersonnelScore();
                    });                   
                }

                itemPanel.AddItem(itemControl);
            }
            sp3.Children.Add(itemPanel);
            sp3.Children.Add(scorebar);
        }


        /// <summary>
        /// 加载人员能力评估类型的表格
        /// </summary>
        /// <param name="_evaluation_group"></param>
        void LoadEvaluation(M_Personnel_Evaluation_Group _evaluation_group)
        {
            //设置该组底部的分数Bar
            ScoreBottomBar scorebar = new ScoreBottomBar();
            scorebar.SetColumnScole(9.444444,1,1,2,1);
            scorebar.SetScore(_evaluation_group._level_One_LastScore, _evaluation_group._level_One_SelfScore, _evaluation_group._level_One_TourScore);
            scorebar._EvaluationCriterion = _evaluation_group._EvaluationCriterion;
            scorebar._InspectionMethod = _evaluation_group._InspectionMethod;

            //设置这一小组的数据
            ItemPanel itemPanel = new ItemPanel();
            //设置两列的宽度的比例
            itemPanel.setColumnRatio(2, 18);
            itemPanel.m_groupName = _evaluation_group._EvaluationGroupContent;
            itemPanel.m_score = _evaluation_group._GroupTotalScore.ToString();

            foreach(MItem_personnel_C _personnel in _evaluation_group.ListGroup)
            {
                ItemRowControl itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNEL_C, _personnel);
                //更新分数
                itemControl.UpdateScore(() =>
                {
                    SetPersonnelScore();
                    scorebar.SetScore(_evaluation_group._level_One_LastScore, _evaluation_group._level_One_SelfScore, _evaluation_group._level_One_TourScore);
                });

                itemPanel.AddItem(itemControl);
            }

            sp3.Children.Add(itemPanel);
            sp3.Children.Add(scorebar);

        }

        /// <summary>
        /// 加载人员训练类型的表格
        /// </summary>
        void LoadTrain(M_Personnel_Train_Group _train_group)
        {
            //设置分数-未完成
            ScoreBottomBar scorebar = new ScoreBottomBar();
            scorebar.SetColumnScole(9.444444, 1, 1, 2, 1);
            scorebar.SetScore(_train_group._level_One_LastScore, _train_group._level_One_SelfScore, _train_group._level_One_TourScore);

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

            sp3.Children.Add(itemPanel);
            sp3.Children.Add(scorebar);
        }

        /// <summary>
        /// 更新人员页面的分数数值
        /// </summary>
        void SetPersonnelScore()
        {
            tbkLastScore3.Text = _ServiceViewModel._currentPersnnel._pageLastScore.ToString();
            tbkSelfEvaluationScore3.Text = _ServiceViewModel._currentPersnnel._pageSelfScore.ToString();
            tbkEvaluationTourScore3.Text = _ServiceViewModel._currentPersnnel._pageTourScore.ToString();
            NotificationUpdateScore();
        }

        #endregion


        #region 加载接待流程表格的类型
        void LoadReceiveGuestsFlowControl()
        {
            if (_ServiceViewModel == null || _ServiceViewModel._currentReceiveGuestsFlow == null) return;
            sp4.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ServiceViewModel._currentReceiveGuestsFlow.ListGroup)
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

               foreach(MItem_FiveSAndSafe _item in _Group.LstItem)
               {
                   ItemRowControl itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_FIVES_AND_SAFE,_item);
                   //刷新分数
                   itemControl.UpdateScore(() => {

                       SetReceiveGuestsFlowPageScore();
                       bottomBar.SetScore(_Group._level_One_LastScore, _Group._level_One_SelfScore, _Group._level_One_TourScore);
                       
                   });
                   itemGroup.AddItem(itemControl);
               }

               sp4.Children.Add(itemGroup);
               sp4.Children.Add(bottomBar);
           }
           SetReceiveGuestsFlowPageScore();
   
        }

        void SetReceiveGuestsFlowPageScore()
        {
            tbkEvaluationTourScore4.Text = _ServiceViewModel._currentReceiveGuestsFlow._pageTourScore.ToString();
            tbkSelfEvaluationScore4.Text = _ServiceViewModel._currentReceiveGuestsFlow._pageSelfScore.ToString();
            tbkLastScore4.Text = _ServiceViewModel._currentReceiveGuestsFlow._pageLastScore.ToString();
            NotificationUpdateScore();
        }
        #endregion


        #region 加载快修流程表格的类型
        void LoadQuickServiceControl()
        {
            if (_ServiceViewModel == null || _ServiceViewModel._currentQuickService == null) return;
            sp5.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ServiceViewModel._currentQuickService.ListGroup)
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
                    //刷新分数
                    itemControl.UpdateScore(() =>
                    {
                        SetQuickServicePageScore();
                        bottomBar.SetScore(_Group._level_One_LastScore, _Group._level_One_SelfScore, _Group._level_One_TourScore);                      
                    });
                    itemGroup.AddItem(itemControl);
                }

                sp5.Children.Add(itemGroup);
                sp5.Children.Add(bottomBar);
            }
            SetQuickServicePageScore();

        }


        void SetQuickServicePageScore()
        {
            tbkEvaluationTourScore5.Text = _ServiceViewModel._currentQuickService._pageTourScore.ToString();
            tbkSelfEvaluationScore5.Text = _ServiceViewModel._currentQuickService._pageSelfScore.ToString();
            tbkLastScore5.Text = _ServiceViewModel._currentQuickService._pageLastScore.ToString();
            NotificationUpdateScore();
        }

        #endregion


        #region 加载BP流程表格的类型
        void LoadBpFlowControl()
        {
            if (_ServiceViewModel == null || _ServiceViewModel._currentBpFlow == null) return;
            sp6.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ServiceViewModel._currentBpFlow.ListGroup)
            {
                ItemPanel itemGroup = new ItemPanel();
                itemGroup.m_groupName = _Group._EvaluationGroupContent;
                itemGroup.m_score = _Group._GroupTotalScore.ToString();

                //设置显示这一组的分数的bar
                ScoreBottomBar bottomBar = new ScoreBottomBar();
                bottomBar.SetColumnScole(859, 142, 142, 284, 142);
                bottomBar.SetScore(_Group._level_One_LastScore, _Group._level_One_SelfScore, _Group._level_One_TourScore);

                foreach (MItem_FiveSAndSafe _item in _Group.LstItem)
                {
                    ItemRowControl itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_FIVES_AND_SAFE, _item);
                    //刷新分数
                    itemControl.UpdateScore(() =>
                    {
                        SetBpFlowPageScore();
                        bottomBar.SetScore(_Group._level_One_LastScore, _Group._level_One_SelfScore, _Group._level_One_TourScore);                       
                    });
                    itemGroup.AddItem(itemControl);
                }

                sp6.Children.Add(itemGroup);
                sp6.Children.Add(bottomBar);
            }
            SetBpFlowPageScore();

        }

        void SetBpFlowPageScore()
        {
            tbkEvaluationTourScore6.Text = _ServiceViewModel._currentBpFlow._pageTourScore.ToString();
            tbkSelfEvaluationScore6.Text = _ServiceViewModel._currentBpFlow._pageSelfScore.ToString();
            tbkLastScore6.Text = _ServiceViewModel._currentBpFlow._pageLastScore.ToString();
            NotificationUpdateScore();
        }

        #endregion


        #region 加载数据准确性表格的类型
        void LoadM_DataAccuracySourceControl()
        {
            if (_ServiceViewModel == null || _ServiceViewModel._currentM_DataAccuracySource == null) return;
            sp7.Children.Clear();
            foreach (M_Common_Groupcs _Group in _ServiceViewModel._currentM_DataAccuracySource.ListGroup)
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
                    //刷新分数
                    itemControl.UpdateScore(() =>
                    {
                        SetM_DataAccuracySourcePageScore();
                        bottomBar.SetScore(_Group._level_One_LastScore, _Group._level_One_SelfScore, _Group._level_One_TourScore);                        
                    });
                    itemGroup.AddItem(itemControl);
                }

                sp7.Children.Add(itemGroup);
                sp7.Children.Add(bottomBar);
            }
            SetM_DataAccuracySourcePageScore();

        }

        void SetM_DataAccuracySourcePageScore()
        {
            tbkEvaluationTourScore7.Text = _ServiceViewModel._currentM_DataAccuracySource._pageTourScore.ToString();
            tbkSelfEvaluationScore7.Text = _ServiceViewModel._currentM_DataAccuracySource._pageSelfScore.ToString();
            tbkLastScore7.Text = _ServiceViewModel._currentM_DataAccuracySource._pageLastScore.ToString();
            NotificationUpdateScore();
        }

        #endregion

     

        private void btnSV_Click(object sender, RoutedEventArgs e)
        {
            if (gd1.Visibility == Visibility.Visible)
             {
                 scrollViewer1.ScrollToHome();                              
                 
             }
             else if (gd2.Visibility == Visibility.Visible)
             {
                  scrollViewer2.ScrollToHome();
             }
             else if (gd3.Visibility == Visibility.Visible)
             {
                  scrollViewer3.ScrollToHome();
             }
             else if (gd4.Visibility == Visibility.Visible)
             {
                  scrollViewer4.ScrollToHome();
             }
             else if (gd5.Visibility == Visibility.Visible)
             {
                  scrollViewer5.ScrollToHome();
             }
             else if (gd6.Visibility == Visibility.Visible)
             {
                  scrollViewer6.ScrollToHome();
             }
             else if (gd7.Visibility == Visibility.Visible)
             {
                  scrollViewer7.ScrollToHome();
             }


        }

    }
}
