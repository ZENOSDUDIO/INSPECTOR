using Honda.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honda.HttpLib;
using Honda.HttpLib.JsonModelData;
using System.Threading;
using System.Diagnostics;
using Honda.Model.Form.Form3;
using Honda.Interface;
using Honda.Globals;
using System.Windows;


namespace Honda.ViewModel
{
    /// <summary>
    /// 零部件评价
    /// </summary>
    public class DMComponentEvaluate
    {
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

        public static DMComponentEvaluate INSTANCE = new DMComponentEvaluate();

        private Queue<string> CODES;

        

        /// <summary>
        /// 基础业务数据源
        /// </summary>
        public M_BaseBusinessSource currentBaseBusiness { get; set; }

        /// <summary>
        /// 营销管理
        /// </summary>
        public M_MarMarketingManageSource currentMarMarketingManage { get; set; }

        /// <summary>
        /// 库存管理
        /// </summary>
        public M_RepertoryManageSource currentRepertoryManage { get; set; }

        /// <summary>
        /// 仓库管理
        /// </summary>
        public M_StoreManagementSource currentStoreManagement { get; set; }

        /// <summary>
        /// 人员表格的数据源
        /// </summary>
        public M_Personnel_SourceEX2 currentPersonnel { get; set; }

        DMComponentEvaluate()
        {
            
        }
        public void GetFormListFromServer(Action<bool> action)
        {
            CODES = new Queue<string>();
            CODES.Enqueue(Const.CODE_BB);
            CODES.Enqueue(Const.CODE_MM);
            CODES.Enqueue(Const.CODE_IM);
            CODES.Enqueue(Const.CODE_WHM);
            CODES.Enqueue(Const.CODE_PM);
            LoadDataCount = CODES.Count;

            action_finish = action;
           GetSecondForm(CODES.Dequeue());
        }

        /// <summary>
        /// 根据二级表单代码获取二级表单
        /// </summary>
        /// <param name="code"></param>
        private void GetSecondForm(string code)
        {
            ReqGetFormList cmd = new ReqGetFormList(code, (obj) =>
            {
                ReqGetFormList rf = obj as ReqGetFormList;
                rf.ParseParam();
                //rf.ContentOfJsonResult;
                JFormBase fb = new JFormBase();
                if(rf.ContentOfJsonResult.Contains(Const.CODE_PM))
                {
                    Debug.WriteLine("renyuanbiaodan");
                }
                bool isSuccess = fb.ParseResult(rf.ContentOfJsonResult);
                LoadDataCount--;
                if (isSuccess)
                {
                    Adapter(code, fb);
                    Thread.Sleep(200);
                    if (CODES.Count > 0)
                        GetSecondForm(CODES.Dequeue());
                }
                else
                {
                    string str = " 店id为" + DMStoreTour.INSTANCE.CurrentMStore.shopId + "\n二级表带code为" + code + "获取失败";
                    MessageBox.Show(str);
                    Debug.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%店id为" + DMStoreTour.INSTANCE.CurrentMStore.shopId + "\n二级表带code为" + code + "获取失败");
                    if (action_finish != null)
                        action_finish(false);
                }
            });
            cmd.SendHttpRequest();
        }

        #region 适配

        void Adapter(string CODE, JFormBase fb)
        {
            switch (CODE)
            {
                case Honda.Globals.Const.CODE_BB:
                    Adapter_BB(fb);
                   // LoadDataCount--;
                    break;

                case Const.CODE_MM:
                    Adapter_MM(fb);
                   // LoadDataCount--;
                    break;

                case Const.CODE_IM:
                    Adapter_IM(fb);
                    //LoadDataCount--;
                    break;

                case Const.CODE_WHM:
                    Adapter_WHM(fb);
                   // LoadDataCount--;
                    break;

                case Const.CODE_PM:
                    Adapter_PS(fb);
                   // LoadDataCount--;
                    break;

              
            }

            if (LoadDataCount == 0)
            {
                if (action_finish != null)
                    action_finish(true);
            }
        }

        


       


        #region 适配人员

        /// <summary>
        /// 适配人员
        /// </summary>
        /// <param name="fb"></param>
        void Adapter_PS(JFormBase fb)
        {
            try
            {
                currentPersonnel = new M_Personnel_SourceEX2();
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

                        //人员培训
                        case 2:
                             AdapterPersonGroupB(group);;
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

                    }else if(jfib.ENUMItemTemplate == ENUM_FORM_ITEM_TEMPLATE.FIRST)
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
                        _configuration_Group.ListGroup.Add(item);
                        if (item.ID == null)
                        {
                            Debug.WriteLine("########AdapterPersonGroupA############################");
                        }

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
                bool bIsLastTimePass = IsPassOrNot(third.ItemList[0].LastResult);   //上次评价结果  需修改
                bool bIsSelfEvaluation = IsPassOrNot(third.ItemList[0].CurrentResult); //自评结果      需修改

                if (third.ItemList[0].Detail.Count == 7)
                {

                    //第一组人员配置1-10项
                    MItem_personnel_A _personA = new MItem_personnel_A();
                    _personA._strNo = third.SerialNum;
                    _personA._strPost = third.Title;
                    _personA.ID = third.ID;
                    _personA.ParentId = third.ParentId;
                    _personA.ShopID = third.ShopID;
                    //需要修改 luyonghe
                    _personA.bIsLastTimePass = bIsLastTimePass;
                    _personA.bIsSelfEvaluation = bIsSelfEvaluation;
                    _personA.bIsEvaluationOfTour = true;
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
                        Debug.WriteLine("########AdapterPersonGroupA############################");
                    }
                }
                else if (third.ItemList[0].Detail.Count == 1)
                {
                    //第一组人员配置11-23项
                    string content = third.ItemList[0].Detail[0];
                    MItem_personnel_B _personb = new MItem_personnel_B(third.SerialNum, third.Title,
                                       content, bIsLastTimePass, bIsSelfEvaluation, true);
                    _personb.ID = third.ID;
                    _personb.ParentId = third.ParentId;
                    _personb.ShopID = third.ShopID;
                    _configuration_Group.ListGroup.Add(_personb);
                    if (_personb.ID == null)
                    {
                        Debug.WriteLine("########AdapterPersonGroupA############################");
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
                    }

                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》AdapterPersonGroupC->Error\r\n" + ex.Message);
            }

        }
        #endregion


        #region 适配基础业务 、营销管理、BP仓库管理、人员管理
        /// <summary>
        /// 基础业务
        /// </summary>
        /// <param name="fb"></param>
        void Adapter_BB(JFormBase fb)
        {
            try
            {
                currentBaseBusiness = new M_BaseBusinessSource();
                currentBaseBusiness.Code = fb.Code;
                currentBaseBusiness.ID = fb.ID;
                currentBaseBusiness.ParentID = fb.ParentID;
                currentBaseBusiness.ListGroup = Adapter_common_group(fb.GroupList);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMComponentEvaluate-》Adapter_BB->Error\r\n" + ex.Message);
            }

        }

        /// <summary>
        /// 营销管理
        /// </summary>
        void Adapter_MM(JFormBase fb)
        {
            try
            {
                currentMarMarketingManage = new M_MarMarketingManageSource();
                currentMarMarketingManage.Code = fb.Code;
                currentMarMarketingManage.ID = fb.ID;
                currentMarMarketingManage.ParentID = fb.ParentID;
                currentMarMarketingManage.ListGroup = Adapter_common_group(fb.GroupList);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMComponentEvaluate-》Adapter_MM->Error\r\n" + ex.Message);
            }
            
        }

        /// <summary>
        /// 库存管理
        /// </summary>
        /// <param name="fb"></param>
        void Adapter_IM(JFormBase fb)
        {           
            try
            {
                currentRepertoryManage = new M_RepertoryManageSource();
                currentRepertoryManage.Code = fb.Code;
                currentRepertoryManage.ID = fb.ID;
                currentRepertoryManage.ParentID = fb.ParentID;
                currentRepertoryManage.ListGroup = Adapter_common_group(fb.GroupList);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMComponentEvaluate-》Adapter_IM->Error\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 仓库管理
        /// </summary>
        /// <param name="fb"></param>
        void Adapter_WHM(JFormBase fb)
        {

            try
            {
                currentStoreManagement = new M_StoreManagementSource();
                currentStoreManagement.Code = fb.Code;
                currentStoreManagement.ID = fb.ID;
                currentStoreManagement.ParentID = fb.ParentID;
                currentStoreManagement.ListGroup = Adapter_common_group(fb.GroupList);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMComponentEvaluate-》Adapter_WHM->Error\r\n" + ex.Message);
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
                    if (first.EnumScoreType == ENUM_SCORE_TYPE.FIVE_STAR_LINK)
                    {
                        cell._evaluationSort = EvaluationSort.storeEvaluation;
                    }
                    cell.ID = first.ID;
                    cell.ParentId = first.ParentId;
                    cell.ShopID = first.ShopID;
                    _common.LstItem.Add(cell);

                }
                ListGroup.Add(_common);
            }

            return ListGroup;
        }
        #endregion


        //判断是否是合格或是不合格
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
