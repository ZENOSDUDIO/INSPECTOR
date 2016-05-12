using Honda.Globals;
using Honda.Interface;
using Honda.Model.Form;
using Honda.UserCtrl;
using Honda.View.BaseView;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Honda.View
{
    /*
     * 
     * 
     */


    public partial class FormGroupDetail : BaseWindow
    {
        private IGroup _group;

        public FormGroupDetail(IGroup group)
        {
            InitializeComponent();
            SetOwner();

            _group = group;
            if (group != null)
            {
                LoadForm();
            }
        }

        /// <summary>
        /// 加载表格
        /// </summary>
        private void LoadForm()
        {
            HideGrid();
            ItemGroupTyple itemGroupType = _group.itemGroupType;
            //switch (itemGroupType)
            //{
            //    case ItemGroupTyple.HardwareToolType:
            //        ToolCell();

            //        break;

            //    case ItemGroupTyple.HardwareNoToolType:
            //        NoToolCell();

            //        break;
            //    case ItemGroupTyple.fiveSType:

            //        LoadFiveSCell();
            //        break;

            //    case ItemGroupTyple.normalType:

            //        LoadNormalCell();
            //        break;

            //    case ItemGroupTyple.personnelTypeA:

            //        LoadPersonnelACell();
            //        break;

            //    case ItemGroupTyple.personnelTypeB:
            //        LoadPersonnelBCell();
            //        break;

            //    case ItemGroupTyple.personnelTypeC:
            //        LoadPersonnelCCell();
            //        break;

            //    case ItemGroupTyple.suggestType:
            //        LoadSuggestCell();
            //        break;
            //}
        }

        #region 5s&安全类型表格

        //5s&安全类型表格
        //void LoadFiveSCell()
        //{
        //    sp1.Children.Clear();
        //    gd1.Visibility = System.Windows.Visibility.Visible;
        //    M_FiveSAndSafes_Source Source = (M_FiveSAndSafes_Source)_group;
        //    if (Source != null)
        //    {

        //        for (int i = 0; i < Source.LstGroup.Count; i++)
        //        {
        //            ItemPanel item = new ItemPanel();
        //            item.m_groupName = Source.LstGroup[i].GroupName;
        //            item.m_score = Source.LstGroup[i].TotalScore.ToString();

        //            foreach (MItem_FiveSAndSafe fives in Source.LstGroup[i])
        //            {
        //                ItemRowControl trc = new ItemRowControl(ItemStyle.ITEM_STYLE_FIVES_AND_SAFE, fives);
        //                trc.IsDetail = true;
        //                item.AddItem(trc);
        //            }
        //            sp1.Children.Add(item);
        //        }

        //    }

        //    //给页面设置分数
        //    tbkEvaluationTourScore1.Text = Source._pageTourScore.ToString();
        //    tbkLastScore1.Text = Source._pageLastScore.ToString();
        //    tbkSelfEvaluationScore1.Text = Source._pageSelfScore.ToString();

        //    //设置评分标准
        //    tbkStandardForEvaluation1.Text = "评分标准：" + Source.EvaluationCriterion;
        //}

        #endregion

        #region 一般的类型表格

        //一般的类型表格
        private void LoadNormalCell()
        {
            gd2.Visibility = Visibility.Visible;
            sp2.Children.Clear();
            M_Common_Groupcs Group = (M_Common_Groupcs) _group;
            ItemPanel itemGroup = new ItemPanel();
            itemGroup.m_groupName = Group._EvaluationGroupContent;

            if (Group != null)
            {
                foreach (MItemNormal _item in Group.LstItem)
                {
                    ItemRowControl itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_NORMAL, _item);
                    itemControl.IsDetail = true;
                    itemGroup.AddItem(itemControl);
                }
                sp2.Children.Add(itemGroup);
            }

            //设置分数和评价标准
            tbkLastScore2.Text = Group._level_One_LastScore.ToString();
            tbkSelfEvaluationScore2.Text = Group._level_One_SelfScore.ToString();
            tbkEvaluationTourScore2.Text = Group._level_One_TourScore.ToString();
            tbkStandardForEvaluation2.Text = "评分标准：" + Group._EvaluationCriterion;
        }

        #endregion

        //#region 硬件类型表格
        ////工具类型
        //void ToolCell()
        //{
        //    M_Hardware_TOOL_Group tool_group = (M_Hardware_TOOL_Group)_group;
        //    if (tool_group == null)
        //    {
        //        return;
        //    }

        //    gd3.Visibility = System.Windows.Visibility.Visible;
        //    sp3.Children.Clear();
        //    LoadHardwareTool(tool_group);

        //    //设置分数和评价标准
        //    tbkStandardForEvaluation3.Text = "评分标准：" + tool_group._EvaluationCriterion;
        //    tbkLastScore3.Text = tool_group._level_One_LastScore.ToString();
        //    tbkSelfEvaluationScore3.Text = tool_group._level_One_SelfScore.ToString();
        //    tbkEvaluationTourScore3.Text = tool_group._level_One_TourScore.ToString();
        //}

        ///// <summary>
        ///// 加载工具类型表格
        ///// </summary>
        ///// <param name="tool_group"></param>
        //void LoadHardwareTool(M_Hardware_TOOL_Group tool_group)
        //{
        //    if (tool_group == null) return;
        //    ItemPanel _itemPanel = new ItemPanel();
        //    _itemPanel.m_groupName = tool_group._EvaluationGroupContent;
        //    _itemPanel.setColumnRatio(4, 36);
        //    _itemPanel.m_score = tool_group._GroupTotalScore.ToString();

        //    foreach (IHardwareTool _tool_group in tool_group.LstItem)
        //    {
        //        switch (_tool_group._hardwareTool_Form_Typle)
        //        {
        //            case HardwareTool_Form_Typle._TOOL_QUICK:
        //                M_Hardware_TOOL_Level_Two_A _tool_level_two_a = (M_Hardware_TOOL_Level_Two_A)_tool_group;

        //                LoadHardwareToolA(_tool_level_two_a, _itemPanel);

        //                break;

        //            case HardwareTool_Form_Typle._TOOL_SHEET:
        //                M_Hardware_TOOL_Level_Two_B _tool_level_two_b = (M_Hardware_TOOL_Level_Two_B)_tool_group;
        //                LoadHardwareToolB(_tool_level_two_b, _itemPanel);

        //                break;

        //            case HardwareTool_Form_Typle._TOOL_MACHINE:
        //                M_Hardware_TOOL_Level_Two_C _tool_level_two_c = (M_Hardware_TOOL_Level_Two_C)_tool_group;
        //                LoadHardwareToolC(_tool_level_two_c, _itemPanel);

        //                break;
        //        }
        //    }

        //    sp3.Children.Add(_itemPanel);

        //}


        //bool isHaveToolBar = false;
        //void LoadHardwareToolA(M_Hardware_TOOL_Level_Two_A tool_level_two_a, ItemPanel _itemPanel)
        //{
        //    ItemPanel_Style2 item = new ItemPanel_Style2(tool_level_two_a._toolGroupName, tool_level_two_a._number);
        //    item.setColumnRatio(3, 3, 30);
        //    foreach (MItem_FiveSAndSafe _five_tool in tool_level_two_a)
        //    {
        //        ItemRowControl trc = new ItemRowControl(ItemStyle.ITEM_STYLE_TOOL_OF_FIVES_AND_SAFE_B, _five_tool);
        //        trc.IsDetail = true;
        //        item.AddItem(trc);
        //    }
        //    _itemPanel.AddItem(item);
        //}

        //void LoadHardwareToolB(M_Hardware_TOOL_Level_Two_B tool_level_two_b, ItemPanel _itemPanel)
        //{
        //    if (!isHaveToolBar)
        //    {
        //        toolBottomBar toolBar = new toolBottomBar();
        //        _itemPanel.AddItem(toolBar);
        //        isHaveToolBar = true;
        //    }


        //    ItemPanel_Style2 item = new ItemPanel_Style2(tool_level_two_b._ToolGroupName, tool_level_two_b._Number);
        //    item.setColumnRatio(3, 3, 30);

        //    foreach (MItem_Tool_A _tool_a in tool_level_two_b)
        //    {
        //        ItemRowControl trc = new ItemRowControl(ItemStyle.ITEM_STYLE_TOOL_A, _tool_a);
        //        trc.IsDetail = true;
        //        item.AddItem(trc);
        //    }
        //    _itemPanel.AddItem(item);
        //}

        //void LoadHardwareToolC(M_Hardware_TOOL_Level_Two_C tool_level_two_c, ItemPanel _itemPanel)
        //{
        //    ItemPanel_Style2 item = new ItemPanel_Style2(tool_level_two_c._toolGroupName, tool_level_two_c._number);
        //    item.setColumnRatio(3, 3, 30);
        //    foreach (M_Tools_B _tool_b in tool_level_two_c)
        //    {
        //        ItemPanel _panel = new ItemPanel();
        //        _panel.m_groupName = _tool_b._deviceGroupName;
        //        _panel.setColumnRatio(5, 25);
        //        foreach (MItem_Tool_B _tool in _tool_b)
        //        {
        //            ItemRowControl trc = new ItemRowControl(ItemStyle.ITEM_STYLE_TOOL_B, _tool);
        //            trc.IsDetail = true;
        //            _panel.AddItem(trc);
        //        }

        //        item.AddItem(_panel);
        //    }

        //    _itemPanel.AddItem(item);

        //}


        ///// <summary>
        ///// 硬件非工具类型的表格
        ///// </summary>
        //void NoToolCell()
        //{
        //    M_Hardware_NO_TOOL_Group _no_tool_group = (M_Hardware_NO_TOOL_Group)_group;
        //    if(_no_tool_group == null)
        //    {
        //        return;
        //    }
        //    sp8.Children.Clear();
        //    gd8.Visibility = System.Windows.Visibility.Visible;
        //    LoadHardwareNoTool(_no_tool_group);

        //    //设置分数和评价标准
        //    tbkStandardForEvaluation8.Text = "评分标准：" + _no_tool_group._EvaluationCriterion;
        //    tbkLastScore8.Text = _no_tool_group._level_One_LastScore.ToString();
        //    tbkSelfEvaluationScore8.Text = _no_tool_group._level_One_SelfScore.ToString();
        //    tbkEvaluationTourScore8.Text = _no_tool_group._level_One_TourScore.ToString();
        //}

        ///// <summary>
        ///// 加载非工具类型
        ///// </summary>
        //void LoadHardwareNoTool(M_Hardware_NO_TOOL_Group no_tool_group)
        //{
        //    if (no_tool_group == null) return;

        //    //设置组表格
        //    ItemPanel item = new ItemPanel();
        //    item.setColumnRatio(4, 36);
        //    item.m_groupName = no_tool_group._EvaluationGroupContent;
        //    item.m_score = no_tool_group._GroupTotalScore.ToString();

        //    foreach (MItem_FiveSAndSafe temp in no_tool_group.LstItem)
        //    {

        //        ItemRowControl trc = new ItemRowControl(ItemStyle.ITEM_STYLE_TOOL_OF_FIVES_AND_SAFE_A, temp);
        //        trc.IsDetail = true;              
        //        item.AddItem(trc);
        //    }

        //    sp8.Children.Add(item);
        //}
        //#endregion

        #region 人员类型表格

        #region 人员表格A类型

        ///// <summary>
        ///// 人员类型表格A类型
        ///// </summary>
        //void LoadPersonnelACell()
        //{

        //    M_Personnel_Configuration_Group _configuraton_group = (M_Personnel_Configuration_Group)_group;
        //    if (_configuraton_group == null)
        //    {
        //        return;
        //    }
        //    gd4.Visibility = System.Windows.Visibility.Visible;
        //    sp4.Children.Clear();
        //    LoadConfiguration(_configuraton_group);

        //    //设置分数和评价标准
        //    tbkStandardForEvaluation4.Text = "评价标准：" + _configuraton_group._EvaluationCriterion;
        //    tbkLastScore4.Text = _configuraton_group._level_One_LastScore.ToString();
        //    tbkSelfEvaluationScore4.Text = _configuraton_group._level_One_SelfScore.ToString();
        //    tbkEvaluationTourScore4.Text = _configuraton_group._level_One_TourScore.ToString();
        //}

        ///// <summary>
        ///// 加载配置人员类型的表格
        ///// </summary>
        ///// <param name="_configuraton_group"></param>
        //void LoadConfiguration(M_Personnel_Configuration_Group _configuraton_group)
        //{
        //    //设置这一小组的数据
        //    ItemPanel itemPanel = new ItemPanel();
        //    //设置两列的宽度的比例
        //    itemPanel.setColumnRatio(67, 312, 1352);
        //    itemPanel.m_groupName = _configuraton_group._EvaluationGroupContent;
        //    itemPanel.m_score = _configuraton_group._GroupTotalScore.ToString();

        //    foreach (IPersonnelGroupConfiguration _cell in _configuraton_group.ListGroup)
        //    {
        //        ItemRowControl itemControl = null;
        //        if (_cell._enum_personel_group_configuration_form == _Enum_Personel_Group_Configuration_Form._machineDescribe)
        //        {
        //            // 加载描述机器类型的基础表格

        //            MItem_personnel_A _machineItem = (MItem_personnel_A)_cell;
        //            itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNEL_A, _machineItem);
        //            itemControl.IsDetail = true;

        //        }
        //        else if (_cell._enum_personel_group_configuration_form == _Enum_Personel_Group_Configuration_Form._postDescribe)
        //        {
        //            // 加载岗位描述类型的基础表格
        //            MItem_personnel_B _postItem = (MItem_personnel_B)_cell;
        //            itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNEL_B, _postItem);
        //            itemControl.IsDetail = true;
        //        }

        //        itemPanel.AddItem(itemControl);
        //    }
        //    sp4.Children.Add(itemPanel);
        //}

        #endregion

        //#region 人员表格C类型

        ///// <summary>
        ///// 人员表格C类型
        ///// </summary>
        //void LoadPersonnelCCell()
        //{
        //    M_Personnel_Evaluation_Group _evaluation_group = (M_Personnel_Evaluation_Group)_group;
        //    if (_evaluation_group == null)
        //    {
        //        return;
        //    }
        //    gd5.Visibility = System.Windows.Visibility.Visible;
        //    sp5.Children.Clear();
        //    LoadEvaluation(_evaluation_group);


        //    //设置分数和评价标准
        //    tbkStandardForEvaluation5.Text ="评分标准：" + _evaluation_group._EvaluationCriterion;
        //    tbkLastScore5.Text = _evaluation_group._level_One_LastScore.ToString();
        //    tbkSelfEvaluationScore5.Text = _evaluation_group._level_One_SelfScore.ToString();
        //    tbkEvaluationTourScore5.Text = _evaluation_group._level_One_TourScore.ToString();
        //}

        ///// <summary>
        ///// 加载人员能力评估类型的表格
        ///// </summary>
        ///// <param name="_evaluation_group"></param>
        //void LoadEvaluation(M_Personnel_Evaluation_Group _evaluation_group)
        //{          
        //    //设置这一小组的数据
        //    ItemPanel itemPanel = new ItemPanel();
        //    //设置两列的宽度的比例
        //    itemPanel.setColumnRatio(2, 18);
        //    itemPanel.m_groupName = _evaluation_group._EvaluationGroupContent;
        //    itemPanel.m_score = _evaluation_group._GroupTotalScore.ToString();

        //    foreach (MItem_personnel_C _personnel in _evaluation_group.ListGroup)
        //    {
        //        ItemRowControl itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNEL_C, _personnel);
        //        itemControl.IsDetail = true;
        //        itemPanel.AddItem(itemControl);
        //    }

        //    sp5.Children.Add(itemPanel);
        //}

        //#endregion

        //#region 人员表格B类型

        ///// <summary>
        ///// 人员表格B类型
        ///// </summary>
        //void LoadPersonnelBCell()
        //{
        //    M_Personnel_Train_Group _train_group = (M_Personnel_Train_Group)_group;
        //    if (_train_group == null)
        //    {
        //        return;
        //    }

        //    sp6.Children.Clear();
        //    gd6.Visibility = System.Windows.Visibility.Visible;
        //    LoadTrain(_train_group);

        //    //设置分数和评价标准
        //    tbkStandardForEvaluation6.Text = "评分标准：" + _train_group._EvaluationCriterion;
        //    tbkLastScore6.Text = _train_group._level_One_LastScore.ToString();
        //    tbkSelfEvaluationScore6.Text = _train_group._level_One_SelfScore.ToString();
        //    tbkEvaluationTourScore6.Text = _train_group._level_One_TourScore.ToString();


        //}

        ///// <summary>
        ///// 加载人员训练类型的表格
        ///// </summary>
        //void LoadTrain(M_Personnel_Train_Group _train_group)
        //{

        //    //设置这一小组的数据
        //    ItemPanel itemPanel = new ItemPanel();
        //    //设置两列的宽度的比例
        //    itemPanel.setColumnRatio(2, 18);
        //    itemPanel.m_groupName = _train_group._EvaluationGroupContent;
        //    itemPanel.m_score = _train_group._GroupTotalScore.ToString();

        //    foreach (MItem_FiveSAndSafe _fiveS in _train_group.ListGroup)
        //    {
        //        ItemRowControl itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PERSONNELL_OF_FIVES_AND_SAFE, _fiveS);
        //        itemControl.IsDetail = true;
        //        itemPanel.AddItem(itemControl);
        //    }

        //    sp6.Children.Add(itemPanel);
        //}
        //#endregion

        #endregion

        #region 建议加分项 - 加分项表格类型

        /// <summary>
        /// 加载建议加分项 - 加分项表格类型
        /// </summary>
        private void LoadSuggestCell()
        {
            //M_Suggest_PlusProject_Source group = (M_Suggest_PlusProject_Source)_group;
            //if(group == null)
            //{
            //    return;
            //}
            //sp7.Children.Clear();
            //gd7.Visibility = Visibility.Visible;
            //foreach (MItem_Suggest_PlusProject _sugg in group.LstGroup)
            //{
            //    ItemRowControl ctr = new ItemRowControl(ItemStyle.ITEM_STYLE_SUGGEST_A, _sugg);
            //    ctr.IsHeighAuto = true;
            //    ctr.IsDetail = true;
            //    sp7.Children.Add(ctr);
            //}

            ////给页面设置分数和评价标准
            //tbkStandardForEvaluation7.Text = "评分标准：" + group.EvaluationCriterion;
            //tbkLastScore7.Text = group._level_One_LastScore.ToString();
            //tbkSelfEvaluationScore7.Text = group._level_One_SelfScore.ToString();
            //tbkEvaluationTourScore7.Text = group._level_One_TourScore.ToString();
        }

        #endregion

        /// <summary>
        /// 隐藏所有的面板
        /// </summary>
        private void HideGrid()
        {
            gd1.Visibility = Visibility.Collapsed;
            gd2.Visibility = Visibility.Collapsed;
            gd3.Visibility = Visibility.Collapsed;
            gd4.Visibility = Visibility.Collapsed;
            gd5.Visibility = Visibility.Collapsed;
            gd6.Visibility = Visibility.Collapsed;
            gd7.Visibility = Visibility.Collapsed;
            gd8.Visibility = Visibility.Collapsed;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}