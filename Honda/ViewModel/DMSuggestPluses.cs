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
using Honda.Globals;
using System.Windows;

namespace Honda.ViewModel
{
    /// <summary>
    /// 建议加分项   2014/9/18
    /// </summary>
    public class DMSuggestPluses
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

        public static DMSuggestPluses INSTANCE = new DMSuggestPluses();
        private Queue<string> CODES;

        
        /// <summary>
        /// 加分项
        /// </summary>
        public M_Suggest_PlusProject_Source currentPlusProject { get; set; }

        /// <summary>
        /// 五星级仓库
        /// </summary>
        public M_Suggest_Warehouse_Source currentWarehouse { get; set; }

        public 
        DMSuggestPluses()
        {
            
           
        }
        public void GetFormListFromServer(Action<bool> action)
        {
            CODES = new Queue<string>();
            CODES.Enqueue(Const.CODE_PLUSES);
            CODES.Enqueue(Const.CODE_FSW);
            LoadDataCount = 2;

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
                case Const.CODE_PLUSES:
                    Adapter_PLUSES(fb);
                    //LoadDataCount--;
                    break;

                case Const.CODE_FSW:
                    Adapter_FSW(fb);
                    //LoadDataCount--;
                    break;
            }

            if (LoadDataCount == 0)
            {
                if (action_finish != null)
                    action_finish(true);
            }
        }

        /// <summary>
        /// 建议加分项
        /// </summary>
        /// <param name="fb"></param>
        void Adapter_PLUSES(JFormBase fb)
        {
            try
            {
                currentPlusProject = new M_Suggest_PlusProject_Source();
                currentPlusProject.Code = fb.Code;
                currentPlusProject.ID = fb.ID;
                currentPlusProject.ParentID = fb.ParentID;
                currentPlusProject._projectName = fb.Name;
                currentPlusProject.LstGroup = Adapter_LstGroup_PLUSES(fb.GroupList);

            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》Adapter_RP->Error\r\n" + ex.Message);
            }

        }

        /// <summary>
        /// 适配建议加分项的表单的组数据 JFormItemFifth
        /// </summary>
        /// <param name="GroupList"></param>
        /// <returns></returns>
        List<MItem_Suggest_PlusProject> Adapter_LstGroup_PLUSES(List<JFormGroup> GroupList)
        {
            List<MItem_Suggest_PlusProject> ListGroup = new List<MItem_Suggest_PlusProject>();
            for (int i = 0; i < GroupList.Count; i++)
            {
                MItem_Suggest_PlusProject PlusProject = new MItem_Suggest_PlusProject();               
                PlusProject.strEvaluationItem = GroupList[i].Name;
                PlusProject.ID = GroupList[i].ID;
                PlusProject.ParentId = GroupList[i].ParentID;
                PlusProject.bIsEvaluationOfTour = true;

                //组里的单元项
                List<object> lstObj = GroupList[i].ItemList;
                JFormItemFifth Fifth = (JFormItemFifth)lstObj[0];
                PlusProject.strNo = Fifth.SerialNum;
                PlusProject.ShopID = Fifth.ShopID;
                PlusProject.strInspectionItem = Fifth.Content;

                if (Fifth.EnumScoreType == ENUM_SCORE_TYPE.FIVE_STAR_LINK)
                {
                    PlusProject._evaluationSort = EvaluationSort.plusesEvaluation;
                }
                

                //设置分数
                double lastScore = 0;  //上次评价分数
                double selfScore = 0;  //自评分数 !string.IsNullOrWhiteSpace(first.CurrentResult)
                double totalScore = 0; //该项的满分分值
                if (!string.IsNullOrWhiteSpace(Fifth.Score))
                {
                    totalScore = double.Parse(Fifth.Score);
                }

                if (!string.IsNullOrWhiteSpace(Fifth.LastResult) )
                {
                    
                    if(GlobalValue.PASS == Fifth.LastResult)
                    {
                        lastScore = totalScore;
                    }else if(GlobalValue.NO_PASS == Fifth.LastResult)
                    {
                        lastScore = 0;
                    }else
                    {
                        lastScore = double.Parse(Fifth.LastResult);
                    }
                    
                }

                if (!string.IsNullOrWhiteSpace(Fifth.CurrentResult))
                {
                    
                    if(GlobalValue.PASS == Fifth.CurrentResult)
                    {
                        selfScore = totalScore;
                    }else if(GlobalValue.NO_PASS == Fifth.CurrentResult)
                    {
                        selfScore = 0;
                    }else
                    {
                        selfScore = double.Parse(Fifth.CurrentResult);
                    }
                }

               


                PlusProject._cellSelfScore = selfScore;
                PlusProject._cellLastScore = lastScore;
                PlusProject._TotalScore = totalScore;

                ListGroup.Add(PlusProject);
                if (PlusProject.ID == null)
                {
                    Debug.WriteLine("########Adapter_LstGroup_PLUSES############################");
                }
            }
            return ListGroup;
        }

       
        /// <summary>
        /// 五星级仓库
        /// </summary>
        /// <param name="fb"></param>
        void Adapter_FSW(JFormBase fb)
        {
            try
            {
                currentWarehouse = new M_Suggest_Warehouse_Source();
                currentWarehouse.Code = fb.Code;
                currentWarehouse.ID = fb.ID;
                currentWarehouse.ParentID = fb.ParentID;
                currentWarehouse.LstGroup = Adapter_LstGroup_FSW(fb.GroupList);
            }
            catch (Exception ex)
            {

                Debug.WriteLine("类DMServiceBasedEvaluation-》Adapter_RP->Error\r\n" + ex.Message);
            }

        }

        List<M_Suggest_Warehouse_Group> Adapter_LstGroup_FSW(List<JFormGroup> GroupList)
        {
            int SS = 0;
            List<M_Suggest_Warehouse_Group> ListGroup = new List<M_Suggest_Warehouse_Group>();

            //组成员list
            for (int i = 0; i < GroupList.Count; i++)
            {
                M_Suggest_Warehouse_Group group = new M_Suggest_Warehouse_Group();
                group._EvaluationGroupContent = GroupList[i].Name;
                group.ID = GroupList[i].ID;
                group.ParentID = GroupList[i].ParentID;

                //组里的单元项
                List<object> lstObj = GroupList[i].ItemList;
                for (int j = 0; j < lstObj.Count; j++)
                {
                    JFormItemSixth six = (JFormItemSixth)lstObj[j];
                    M_Suggest_Warehouse_Level_Two _Level_two = new M_Suggest_Warehouse_Level_Two();
                    group.ListGroup.Add(_Level_two);
                    _Level_two._GroupName = six.Title;
                    _Level_two.ID = six.ID;
                    _Level_two.ParentID = six.ParentId;
                    _Level_two.shopID = six.ShopID.ToString();
                    
                    foreach(JFormItemContentSixth sixConten in six.ItemList)
                    {
                        M_Suggest_Warehouse_Level_Three level_three = new M_Suggest_Warehouse_Level_Three();
                        _Level_two.Add(level_three);
                        level_three.ID = sixConten.ID;
                        level_three.ParentID = sixConten.ParentID;
                        level_three._GroupName = sixConten.Title;
                        
                        foreach( JFormItemContentSixthItem sixDetail in sixConten.Detail)
                        { 
                            MItem_Suggest_Warehouse cell = new MItem_Suggest_Warehouse();
                            level_three.Add(cell);
                            cell.ID = sixDetail.ID;

                            cell.ParentId = sixDetail.ParentID;

                            //cell._itemScore = double.Parse(sixDetail.Score);
                            cell.strSmallItem = sixDetail.SmallItem;
                            cell.strSort = sixDetail.Classify;
                            cell.strTarget = sixDetail.Name;
                            cell.strStandardForEvaluation = sixDetail.Content;

                            //总分值
                            double totalScore = 0;
                            //上次评分值
                            double lastScore = 0;
                            //自评
                            double selfScore = 0;
                            if (!string.IsNullOrWhiteSpace(sixDetail.Score))
                            {
                                totalScore = double.Parse(sixDetail.Score);
                                cell._itemScore = double.Parse(sixDetail.Score);
                            }

                            if (!string.IsNullOrWhiteSpace(sixDetail.LastResult))
                            {
                                lastScore = double.Parse(sixDetail.LastResult);
                                if (GlobalValue.PASS == lastScore.ToString())
                                {
                                    cell._cellLastScore = totalScore;
                                }else if(GlobalValue.NO_PASS == lastScore.ToString())
                                {
                                    cell._cellLastScore = 0;
                                }else
                                {
                                    cell._cellLastScore = lastScore;
                                }

                            }

                            if (!string.IsNullOrWhiteSpace(sixDetail.CurrentResult))
                            {
                                selfScore = double.Parse(sixDetail.CurrentResult);

                            }
                            
                            cell._cellSelfScore = selfScore;
                            cell._itemScore = totalScore;
                            SS += 1;
                            if (cell.ID == null)
                            {
                                Debug.WriteLine("########Adapter_LstGroup_PLUSES############################");
                            }

                        }
                        
                    }     

                }
                
                ListGroup.Add(group);
            }

            int sss = SS;
            return ListGroup;
        }
        #endregion


    }
}
