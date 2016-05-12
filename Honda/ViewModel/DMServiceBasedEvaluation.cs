using Honda.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honda.HttpLib;
using Honda.HttpLib.JsonModelData;
using System.Threading;
using Honda.Interface;
using System.Diagnostics;
using Honda.Model.Form.Form1;
using Honda.Globals;
using System.Windows;

namespace Honda.ViewModel
{
    /// <summary>
    /// 服务基础评价
    /// </summary>
    public class DMServiceBasedEvaluation
    {

         #region Var
        /*
         * 1、当所有请求都有返回数据时，页面在能操作。
         * 2、每当发一次请求时LoadDataCount加1，数据返回一次时，LoadDataCount减1，当LoadDataCount==0时，
         * 表示所有请求完成
         */
        int LoadDataCount = 0;

        /// <summary>
        /// 当数据都请求下来完成时，回调
        /// </summary>
        Action<bool> action_finish;

        public static DMServiceBasedEvaluation INSTANCE = new DMServiceBasedEvaluation();

        private Queue<string> CODES;
        
        ///// <summary>
        // /// 5S & 安全数据源
        ///// </summary>
        public M_FiveSAndSafes_Source currentFiveSAndSafesSource { get; set; }
        

        /// <summary>
        /// 硬件数据源
        /// </summary>
        public M_Hardware_Source currentHardware { get; set; }

        /// <summary>
        /// 硬件的子项数据源- 工具
        /// </summary>
        public M_Hardware_TOOL_Group _Hardware_TOOL_Group { get; set; }

        /// <summary>
        /// 人员表格的数据源
        /// </summary>
        public M_Personnel_SourceEX1 currentPersonnel { get; set; }

        /// <summary>
        /// 接待流程表格的数据源
        /// </summary>
        public M_ReceiveGuestsFlowSource currentReceiveGuestsFlow { get; set; }

        /// <summary>
        /// 快修流程表格的数据源
        /// </summary>
        public M_QuickService currentQuickService { get; set; }

        /// <summary>
        /// BP流程表格的数据源
        /// </summary>
        public M_BpFlowSource currentBpFlow { get; set; }

        /// <summary>
        ///数据准确性表格的数据源
        /// </summary>
        public M_DataAccuracySource currentM_DataAccuracySource { get; set; }



         #endregion

         DMServiceBasedEvaluation()
         {           
         }

         
        public void GetServiceBasedData(Action<bool> action)
         {
             CODES = new Queue<string>();
             CODES.Enqueue(Const.CODE_SS);
             CODES.Enqueue(Const.CODE_HW);
             CODES.Enqueue(Const.CODE_PS);
             CODES.Enqueue(Const.CODE_RP);
             CODES.Enqueue(Const.CODE_FRP);
             CODES.Enqueue(Const.CODE_BPP);
             CODES.Enqueue(Const.CODE_PA);
             LoadDataCount = CODES.Count;

             action_finish = action;
             GetListFromServer(CODES.Dequeue());  
         }
        /// <summary>
        ///从服务器中获取表单
        /// </summary>
        void GetListFromServer(string CODE)
        {
            try
            {               
                ReqGetFormList cmd = new ReqGetFormList(CODE, (obj) =>
                {
                    ReqGetFormList rf = obj as ReqGetFormList;
                    rf.ParseParam();
                    //rf.ContentOfJsonResult;
                    JFormBase fb = new JFormBase();
                    bool isSuccess = fb.ParseResult(rf.ContentOfJsonResult);
                    LoadDataCount--;
                    if (isSuccess)
                    {
                        Adapter(CODE, fb);
                        if (CODES.Count > 0)
                            GetListFromServer(CODES.Dequeue());
                    }else
                    {
                        string str = "店id为" + DMStoreTour.INSTANCE.CurrentMStore.shopId + "\n二级表带code为" + CODE + "获取失败";
                        MessageBox.Show(str);
                        Debug.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%店id为" + DMStoreTour.INSTANCE.CurrentMStore.shopId + "\n二级表带code为" + CODE + "获取失败");
                        if(action_finish != null)
                            action_finish(false);
                    }
                });
                cmd.SendHttpRequest();
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》GetListFromServer->Error\r\n" + ex.Message);
            }
           
        }

        #region 适配

        void Adapter(string CODE, JFormBase fb)
        {
            switch (CODE)
            {
                case Const.CODE_SS:
                    AdapterSS(fb);
                    //LoadDataCount--;
                    break;

                case Const.CODE_HW:
                    AdapterHW(fb);
                    //LoadDataCount--;
                    break;

                case Const.CODE_PS:
                    Adapter_PS(fb);
                    //LoadDataCount--;
                    break;

                case Const.CODE_RP:
                    Adapter_RP(fb);                   
                    //LoadDataCount--;
                    break;

                case Const.CODE_FRP:
                    Adapter_FRP(fb);
                    //LoadDataCount--;
                    break;

                case Const.CODE_BPP:
                    Adapter_BP(fb);
                    //LoadDataCount--;
                    break;

                case Const.CODE_PA:
                    Adapter_PA(fb);
                   // LoadDataCount--;
                    break;
            }

            if(LoadDataCount == 0)
            {
                if(action_finish != null)
                    action_finish(true);
            }
        }

        #region 适配适配5s及其安全
        
        /// <summary>
        /// 适配5s及其安全
        /// </summary>
        /// <param name="fb"></param>
        void AdapterSS(JFormBase fb)
        {
            try
            {
                currentFiveSAndSafesSource = new M_FiveSAndSafes_Source();
                currentFiveSAndSafesSource.Code = fb.Code;
                currentFiveSAndSafesSource.ID = fb.ID;
                currentFiveSAndSafesSource.ParentID = fb.ParentID;

                List<MFiveSAndSafes> lst = new List<MFiveSAndSafes>();
                List<JFormGroup> GroupList = fb.GroupList;
                for (int i = 0; i < GroupList.Count; i++)
                {
                    MFiveSAndSafes group = new MFiveSAndSafes();
                    group.GroupName = GroupList[i].Name;
                    group.ID = GroupList[i].ID;
                    group.ParentID = GroupList[i].ParentID;

                    //组里的单元项
                    List<object> lstObj = GroupList[i].ItemList;
                    for (int j = 0; j < lstObj.Count; j++)
                    {
                        JFormItemFirst first = (JFormItemFirst)lstObj[j];

                        MItem_FiveSAndSafe cell = new MItem_FiveSAndSafe(first.SerialNum, first.Title, IsPassOrNot(first.LastResult), IsPassOrNot(first.CurrentResult), true);
                        cell.ID = first.ID;
                        cell.ParentId = first.ParentId;
                        group.Add(cell);
                        if(cell.ID == null)
                        {
                            Debug.WriteLine("########5S############################");
                        }
                    }

                    lst.Add(group);

                }
                currentFiveSAndSafesSource.LstGroup = lst;
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》AdapterSS->Error\r\n" + ex.Message);
            }
            
        }

        #endregion


        #region 适配硬件

        /// <summary>
        /// 适配硬件
        /// </summary>
        /// <param name="fb"></param>
        void AdapterHW(JFormBase fb)
        {
            try
            {
                currentHardware = new M_Hardware_Source();
                currentHardware.Code = fb.Code;
                currentHardware.ID = fb.ID;
                currentHardware.ParentID = fb.ParentID;

                List<JFormGroup> GroupList = fb.GroupList;

                //非工具类型的组
                M_Hardware_NO_TOOL_Group _no_tool_group = null;
                //工具类型的组
                M_Hardware_TOOL_Group _tool_group = null;

                for (int i = 0; i < GroupList.Count; i++)
                {
                    //前四组是非工具类型的小组
                    if (i < 4)
                    {
                        _no_tool_group = new M_Hardware_NO_TOOL_Group();
                        JFormGroup group = GroupList[i];
                        AdapterNoTool(group, _no_tool_group, i);
                    }
                    else
                    {
                        _tool_group = new M_Hardware_TOOL_Group();
                        _tool_group._EvaluationGroupContent = GroupList[i].Name;
                        _tool_group.ID = GroupList[i].ID;
                        _tool_group.ParentID = GroupList[i].ParentID;
                        _tool_group._index = i;

                        //组里的单元项
                        List<object> lstObj = GroupList[i].ItemList;

                        for (int j = 0; j < lstObj.Count; j++)
                        {
                            JFormItemBase JFIB = (JFormItemBase)(lstObj[j]);

                            //快修工具
                            if (JFIB.ENUMItemTemplate == ENUM_FORM_ITEM_TEMPLATE.SECOND)
                            {
                                JFormItemSecond second = (JFormItemSecond)lstObj[j];
                                AdapterToolA(second, _tool_group);
                            }
                            //钣喷工具
                            else if (JFIB.ENUMItemTemplate == ENUM_FORM_ITEM_TEMPLATE.THIRD)
                            {
                                JFormItemThird third = (JFormItemThird)lstObj[j];
                                AdapterToolB(third, _tool_group);


                            }//机修工具类型
                            else if (JFIB.ENUMItemTemplate == ENUM_FORM_ITEM_TEMPLATE.FOURTH)
                            {

                                JFormItemFourth four = (JFormItemFourth)lstObj[j];
                                AdapterToolC(four, _tool_group);

                            }

                        }
                        currentHardware.LstHardware.Add(_tool_group);
                    }


                }

                currentHardware.InitGroupData();
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》AdapterHW->Error\r\n" + ex.Message);
            }
           
            
        }

        /// <summary>
        /// 适配硬件中的非工具类型
        /// </summary>
        void AdapterNoTool(JFormGroup group, M_Hardware_NO_TOOL_Group _no_tool_group, int i)
        {
            try
            {
                _no_tool_group._EvaluationGroupContent = group.Name;
                _no_tool_group.ID = group.ID;
                _no_tool_group.ParentID = group.ParentID;
                _no_tool_group._index = i;

                //组里的单元项
                List<object> lstObj = group.ItemList;
                for (int j = 0; j < lstObj.Count; j++)
                {
                    JFormItemFirst first = (JFormItemFirst)lstObj[j];
                    MItem_FiveSAndSafe cell = new MItem_FiveSAndSafe(first.SerialNum, first.Title, IsPassOrNot(first.LastResult), IsPassOrNot(first.CurrentResult), true);
                    cell.ID = first.ID;
                    cell.ParentId = first.ParentId;
                    cell.ShopID = first.ShopID;

                    _no_tool_group.LstItem.Add(cell);
                }
                currentHardware.LstHardware.Add(_no_tool_group);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》AdapterNoTool->Error\r\n" + ex.Message);
            }
           
        }

        /// <summary>
        /// 适配硬件中快修工具类型
        /// </summary>
        void AdapterToolA(JFormItemSecond second, M_Hardware_TOOL_Group _tool_group)
        {
            try
            {
                M_Hardware_TOOL_Level_Two_A _second = new M_Hardware_TOOL_Level_Two_A();
                //设置项里的名、id、序号
                _second._number = second.SerialNum;
                _second._toolGroupName = second.Title;
                _second.ID = second.ID;
                _second.ParentID = second.ParentId;

                //设置项里的组
                List<JFormItemContentSecond> ItemList = second.ItemList;
                for (int k = 0; k < ItemList.Count; k++)
                {
                    JFormItemContentSecond contentSecond = ItemList[k];
                    MItem_FiveSAndSafe cell = new MItem_FiveSAndSafe(contentSecond.Title, contentSecond.Detail, IsPassOrNot(contentSecond.LastResult), IsPassOrNot(contentSecond.CurrentResult), true);

                    cell.ID = contentSecond.ID;
                    cell.ParentId = contentSecond.ParentID;
                    if (cell.ID == null)
                    {
                        Debug.WriteLine("########AdapterToolA############################");
                    }
                    _second.Add(cell);
                }

                _tool_group.LstItem.Add(_second);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》AdapterToolA->Error\r\n" + ex.Message);
            }     
           
        }

        /// <summary>
        /// 适配硬件中钣喷工具类型
        /// </summary>
        void AdapterToolB(JFormItemThird third, M_Hardware_TOOL_Group _tool_group)
        {
            try
            {
                M_Hardware_TOOL_Level_Two_B _third = new M_Hardware_TOOL_Level_Two_B();
                //设置项里的名、id、序号
                _third._Number = third.SerialNum;
                _third._ToolGroupName = third.Title;
                _third.ID = third.ID;
                _third.ParentID = third.ParentId;

                //设置项里的组
                List<JFormItemContentThird> ItemList = third.ItemList;
                for (int k = 0; k < ItemList.Count; k++)
                {
                    JFormItemContentThird content = ItemList[k];

                    //设置最小项
                    List<string> Details = content.Detail;
                    MItem_Tool_A cell = new MItem_Tool_A(content.Title, Details[0], Details[1], Details[2],
                            Details[3], IsPassOrNot(content.LastResult), IsPassOrNot(content.CurrentResult), true);

                    cell.StrMachineName = content.Title;
                    cell.ID = content.ID;
                    cell.ParentId = content.ParentID;
                    _third.Add(cell);
                    if (cell.ID == null)
                    {
                        Debug.WriteLine("########AdapterToolB############################");
                    }

                }
                _tool_group.LstItem.Add(_third);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》AdapterToolB->Error\r\n" + ex.Message);
            }
            
            
        }

        /// <summary>
        /// 适配硬件中机修工具类型
        /// </summary>
        void AdapterToolC(JFormItemFourth four, M_Hardware_TOOL_Group _tool_group)
        {
            try
            {
                M_Hardware_TOOL_Level_Two_C _four = new M_Hardware_TOOL_Level_Two_C();
                //设置项里的名、id、序号
                _four._number = four.SerialNum;
                _four._toolGroupName = four.Title;
                _four.ID = four.ID;
                _four.ParentID = four.ParentId;

                //设置项里的组
                List<JFormItemContentFourth> ItemList = four.ItemList;
                for (int k = 0; k < ItemList.Count; k++)
                {
                    JFormItemContentFourth content = ItemList[k];

                    //设置最小项
                    //List<string> Details = content.Detail;---xiang
                    M_Tools_B cells = new M_Tools_B();
                    cells._deviceGroupName = content.Title;
                    for (int i1 = 0; i1 < content.Detail.Count; i1++)
                    {
                        MItem_Tool_B cell = new MItem_Tool_B(content.Detail[i1].Title, IsPassOrNot(content.Detail[i1].LastResult), IsPassOrNot(content.Detail[i1].CurrentResult), true); //需修改
                        cell.ID = content.Detail[i1].ID;
                        cell.ParentId = content.Detail[i1].ParentID;
                        cell.ID = content.Detail[i1].ID;
                        cell.ParentId = content.Detail[i1].ParentID;
                        cells.Add(cell);


                        if (string.IsNullOrEmpty(cell.ID) )
                        {
                            Debug.WriteLine("########AdapterToolC############################");
                        }


                    }
                    _four.Add(cells);
                }
                _tool_group.LstItem.Add(_four);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》AdapterToolC->Error\r\n" + ex.Message);
            }
            
        }
        #endregion


        #region 适配人员
        
        /// <summary>
        /// 适配人员
        /// </summary>
        /// <param name="fb"></param>
        void Adapter_PS(JFormBase fb)
        {
            try
            {
                currentPersonnel = new M_Personnel_SourceEX1();
                currentPersonnel.Code = fb.Code;
                currentPersonnel.ID = fb.ID;
                currentPersonnel.ParentID = fb.ParentID;

                List<JFormGroup> GroupList = fb.GroupList;


                for (int i = 0; i < GroupList.Count; i++)
                {
                    JFormGroup group = GroupList[i];


                    switch (i)
                    {
                        //第一项是人员配置类型
                        case 0:
                            AdapterPersonGroupA(group);
                            break;

                        //人员培训
                        case 1:
                            AdapterPersonGroupB(group);
                            break;

                        //人员能力评估
                        case 2:
                            AdapterPersonGroupC(group);
                            break;

                        case 3:
                        case 4:
                            AdapterPersonGroupB(group);
                            break;

                    }



                }
                currentPersonnel.InitFormData();
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》Adapter_PS->Error\r\n" + ex.Message);
            }
            
        }

        /// <summary>
        /// 人员表单中，适配第一组
        /// </summary>
        void AdapterPersonGroupA(JFormGroup group)
        {
            try
            {
                //组
                M_Personnel_Configuration_Group _configuration_Group = new M_Personnel_Configuration_Group();
                _configuration_Group._EvaluationGroupContent = group.Name;
                _configuration_Group.ID = group.ID;
                _configuration_Group.ParentID = group.ParentID;
                currentPersonnel.LstGroup.Add(_configuration_Group);

                //组成员list
                List<object> ItemList = group.ItemList;
                for (int j = 0; j < ItemList.Count; j++)
                {
                    JFormItemBase jfib = (JFormItemBase)ItemList[j];
                    //人员配置1-10项和11-23项的类型
                    if (jfib.ENUMItemTemplate == ENUM_FORM_ITEM_TEMPLATE.THIRD)
                    {
                        JFormItemThird third = (JFormItemThird)jfib;
                        AdapterPersonA(third, _configuration_Group);

                    }


                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("类DMServiceBasedEvaluation-》AdapterPersonGroupA->Error\r\n" + ex.Message);
            }
           
        }

        /// <summary>
        /// 人员表单中，第一组人员配置1-10项和11-23项的类型的类型
        /// </summary>
        void AdapterPersonA(JFormItemThird third, M_Personnel_Configuration_Group _configuration_Group)
        {
            try
            {
                

                if (third.ItemList[0].Detail.Count == 7)
                {

                    //第一组人员配置1-10项
                    MItem_personnel_A _personA = new MItem_personnel_A();
                    _personA._strNo = third.SerialNum;
                    _personA._strPost = third.Title;
                    _personA.ID = third.ID;
                    _personA.ParentId = third.ParentId;
                    _personA.ShopID = third.ShopID;
                    _personA.bIsEvaluationOfTour = true;
                    
                    _personA.bIsLastTimePass = IsPassOrNot(third.ItemList[0].LastResult);
                    //JFormItemContentThird
                    _personA.bIsSelfEvaluation = IsPassOrNot(third.ItemList[0].CurrentResult);

                    _personA._strEvaluation_of_count_0 = third.ItemList[0].Detail[0];
                    _personA._strEvaluation_of_count_1 = third.ItemList[0].Detail[1];
                    _personA._strEvaluation_of_count_2 = third.ItemList[0].Detail[2];
                    _personA._strEvaluation_of_count_3 = third.ItemList[0].Detail[3];
                    _personA._strEvaluation_of_count_4 = third.ItemList[0].Detail[4];
                    _personA._strEvaluation_of_count_5 = third.ItemList[0].Detail[5];
                    _personA._strEvaluation_of_count_6 = third.ItemList[0].Detail[6];

                    _configuration_Group.ListGroup.Add(_personA);
                    if (_personA.ID == null)
                    {
                        Debug.WriteLine("########AdapterPersonA############################");
                    }
                }
                else if (third.ItemList[0].Detail.Count == 1)
                {
                    //第一组人员配置11-23项
                    string content = third.ItemList[0].Detail[0];
                    MItem_personnel_B _personb = new MItem_personnel_B(third.SerialNum, third.Title,
                                       content, IsPassOrNot(third.ItemList[0].LastResult), IsPassOrNot(third.ItemList[0].CurrentResult), true);

                    _personb.ID = third.ID;
                    _personb.ParentId = third.ParentId;
                    _personb.ShopID = third.ShopID;
                    _configuration_Group.ListGroup.Add(_personb);
                    if (_personb.ID == null)
                    {
                        Debug.WriteLine("########AdapterPersonA############################");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("类DMServiceBasedEvaluation-》AdapterPersonA->Error\r\n" + ex.Message);
                
            }
            
           
        }
       

        /// <summary>
        /// 人员表单中，适配第二种类型的组（类似于人员培训的类型）
        /// </summary>
        /// <param name="group"></param>
        void AdapterPersonGroupB(JFormGroup group)
        {
            try
            {
                M_Personnel_Train_Group _train_group = new M_Personnel_Train_Group();
                //组
                _train_group._EvaluationGroupContent = group.Name;
                _train_group.ID = group.ID;
                _train_group.ParentID = group.ParentID;
                currentPersonnel.LstGroup.Add(_train_group);

                //组成员list
                List<object> ItemList = group.ItemList;
                for (int j = 0; j < ItemList.Count; j++)
                {
                    JFormItemBase jfib = (JFormItemBase)ItemList[j];
                    if (jfib.ENUMItemTemplate == ENUM_FORM_ITEM_TEMPLATE.FIRST)
                    {
                        JFormItemFirst first = jfib as JFormItemFirst;
                        if (jfib == null)
                        {
                            return;
                        }
                        MItem_FiveSAndSafe item = new MItem_FiveSAndSafe(first.SerialNum, first.Title, IsPassOrNot(first.LastResult), IsPassOrNot(first.CurrentResult), true);
                        item.ID = first.ID;
                        item.ParentId = first.ParentId;
                        item.ShopID = first.ShopID;
                        _train_group.ListGroup.Add(item);
                        if (item.ID == null)
                        {
                            Debug.WriteLine("########AdapterPersonGroupB############################");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("类DMServiceBasedEvaluation-》AdapterPersonGroupB->Error\r\n" + ex.Message);                
            }
            
        }

        /// <summary>
        /// 人员表单中，适配第三种类型的组（类似于人员能力评估）
        /// </summary>
        /// <param name="group"></param>
        void AdapterPersonGroupC(JFormGroup group)
        {
            try
            {
                M_Personnel_Evaluation_Group _evaluation_group = new M_Personnel_Evaluation_Group();
                //组
                _evaluation_group._EvaluationGroupContent = group.Name;
                _evaluation_group.ID = group.ID;
                _evaluation_group.ParentID = group.ParentID;
                currentPersonnel.LstGroup.Add(_evaluation_group);

                //组成员list
                List<object> ItemList = group.ItemList;
                for (int j = 0; j < ItemList.Count; j++)
                {
                    JFormItemBase jfib = (JFormItemBase)ItemList[j];
                    if (jfib.ENUMItemTemplate == ENUM_FORM_ITEM_TEMPLATE.FIRST)
                    {
                        JFormItemFirst first = (JFormItemFirst)jfib;
                        double lastScore = 0;
                        double selfScore = 0;
                        if (!string.IsNullOrWhiteSpace(first.LastResult) && !string.IsNullOrWhiteSpace(first.CurrentResult))
                        {
                            lastScore = double.Parse(first.LastResult);
                            selfScore = double.Parse(first.CurrentResult);
                        }
                        MItem_personnel_C item = new MItem_personnel_C(first.SerialNum, first.Title, lastScore, selfScore, 0);
                        item.ID = first.ID;
                        item.ParentId = first.ParentId;
                        item.ShopID = first.ShopID;
                        _evaluation_group.ListGroup.Add(item);
                        if (item.ID == null)
                        {
                            Debug.WriteLine("########AdapterPersonGroupC############################");
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》AdapterPersonGroupC->Error\r\n" + ex.Message); 
            }
           
        }
        #endregion

        #region 适配接待流程 、快修流程、BP流程、数据准确性
        /// <summary>
        /// 适配接待流程
        /// </summary>
        /// <param name="fb"></param>
        void Adapter_RP(JFormBase fb)
        {
            try
            {
                currentReceiveGuestsFlow = new M_ReceiveGuestsFlowSource();
                currentReceiveGuestsFlow.Code = fb.Code;
                currentReceiveGuestsFlow.ID = fb.ID;
                currentReceiveGuestsFlow.ParentID = fb.ParentID;
                currentReceiveGuestsFlow.ListGroup = Adapter_common_group(fb.GroupList); 
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》Adapter_RP->Error\r\n" + ex.Message); 
            }
           
        }

        /// <summary>
        /// 适配快修流程
        /// </summary>
        /// <param name="fb"></param>
        void Adapter_FRP(JFormBase fb)
        {
            try
            {
                currentQuickService = new M_QuickService();
                currentQuickService.Code = fb.Code;
                currentQuickService.ID = fb.ID;
                currentQuickService.ParentID = fb.ParentID;
                currentQuickService.ListGroup = Adapter_common_group(fb.GroupList);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》Adapter_FRP->Error\r\n" + ex.Message); 
            }
            
        }

        /// <summary>
        /// 适配BP流程
        /// </summary>
        /// <param name="fb"></param>
        void Adapter_BP(JFormBase fb)
        {
            try
            {
                currentBpFlow = new M_BpFlowSource();
                currentBpFlow.Code = fb.Code;
                currentBpFlow.ID = fb.ID;
                currentBpFlow.ParentID = fb.ParentID;
                currentBpFlow.ListGroup = Adapter_common_group(fb.GroupList);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》Adapter_BP->Error\r\n" + ex.Message); 
            }
           
        }

        /// <summary>
        /// 适配BP流程
        /// </summary>
        /// <param name="fb"></param>
        void Adapter_PA(JFormBase fb)
        {
            try
            {
                currentM_DataAccuracySource = new M_DataAccuracySource();
                currentM_DataAccuracySource.Code = fb.Code;
                currentM_DataAccuracySource.ID = fb.ID;
                currentM_DataAccuracySource.ParentID = fb.ParentID;
                currentM_DataAccuracySource.ListGroup = Adapter_common_group(fb.GroupList);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》Adapter_PA->Error\r\n" + ex.Message); 
            }
            
        }

        /// <summary>
        /// 适配正常格式的表单的组数据
        /// </summary>
        /// <param name="?"></param>
        List<M_Common_Groupcs> Adapter_common_group(List<JFormGroup> GroupList)
        {

            List<M_Common_Groupcs> ListGroup = new List<M_Common_Groupcs>();
            for (int i = 0; i < GroupList.Count; i++)
            {

                M_Common_Groupcs _common = new M_Common_Groupcs();
                _common._EvaluationGroupContent = GroupList[i].Name;
                _common.ID = GroupList[i].ID;
                _common.ParentID = GroupList[i].ParentID;

                //组里的单元项
                List<object> lstObj = GroupList[i].ItemList;
                for (int j = 0; j < lstObj.Count; j++)
                {
                    JFormItemFirst first = (JFormItemFirst)lstObj[j];
                    MItem_FiveSAndSafe cell = new MItem_FiveSAndSafe(first.SerialNum, first.Title, IsPassOrNot(first.LastResult), IsPassOrNot(first.CurrentResult), true);
                    cell.ID = first.ID;
                    cell.ParentId = first.ParentId;
                    cell.ShopID = first.ShopID;
                    _common.LstItem.Add(cell);
                    if (cell.ID == null)
                    {
                        Debug.WriteLine("########Adapter_common_group############################");
                    }

                }
                ListGroup.Add(_common);
            }

            return ListGroup;
        }
        #endregion

      

       bool IsPassOrNot(string scoreCode)
        {
            
            if (GlobalValue.PASS == scoreCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
 
    }

}
