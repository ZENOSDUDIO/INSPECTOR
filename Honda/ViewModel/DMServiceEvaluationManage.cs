using Honda.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honda.HttpLib;
using Honda.HttpLib.JsonModelData;
using System.Threading;
using Honda.Globals;
using System.Diagnostics;
using System.Windows;

namespace Honda.ViewModel
{
    /// <summary>
    /// 服务管理评价 9/17
    /// </summary>
    public class DMServiceEvaluationManage
    {
        #region Var
        /*
         * 1、当所有请求都有返回数据时，页面在能操作。
         * 2、每当发一次请求时LoadDataCount加1，数据返回一次时，LoadDataCount减1，当LoadDataCount==0时，
         * 表示所有请求完成
         */
        int LoadDataCount = 0;

        Action<bool> action_finish;

        public static DMServiceEvaluationManage INSTANCE = new DMServiceEvaluationManage();

        private Queue<string> CODES;

        

        /// <summary>
        /// 满意度管理 数据源
        /// </summary>
        public M_SatisfactionManageSource currentSatisfactionManagement { get; set; }

        /// <summary>
        /// 客户维系管理 数据源
        /// </summary>
        public M_ClientMange currentClientMange { get; set; }

        /// <summary>
        /// 来厂促进管理 数据源
        /// </summary>
        public M_FactoryManageSource currentMangement { get; set; }

        /// <summary>
        /// 重点业务 数据源
        /// </summary>
        public M_EmphasisBusinessSource currentEmphasisBusiness { get; set; }

        #endregion


        DMServiceEvaluationManage()
        {
            
        }

        // by luyonghe 10/30
        public void GetFormListFromServer(Action<bool> action)
        {
            //满意度管理
            //GetSecondForm(CODE_SFM);
            ////Thread.Sleep(1000);
            ////客户维系管理
            //GetSecondForm(CODE_CTMM);
            ////Thread.Sleep(1000);
            ////来场促进管理
            //GetSecondForm(CODE_TP);
            ////Thread.Sleep(1000);
            ////重点业务
           // GetSecondForm(CODE_FB);
            ////Thread.Sleep(1000);

            CODES = new Queue<string>();
            CODES.Enqueue(Const.CODE_SFM);
            CODES.Enqueue(Const.CODE_CTMM);
            CODES.Enqueue(Const.CODE_TP);
            CODES.Enqueue(Const.CODE_FB);
            LoadDataCount = 4;
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
                    Thread.Sleep(100);
                    if(CODES.Count > 0)
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

        #region  适配
        void Adapter(string CODE, JFormBase fb)
        {
            switch (CODE)
            {
                case Const.CODE_SFM:
                    AdapterSFM(fb);
                   // LoadDataCount--;
                    break;

                case Const.CODE_CTMM:
                    AdapterCTMM(fb);
                    //LoadDataCount--;
                    break;

                case Const.CODE_TP:
                    AdapterTP(fb);
                    //LoadDataCount--;
                    break;

                case Const.CODE_FB:
                    Adapter_RP(fb);
                    //LoadDataCount--;
                    break;                
            }

            if (LoadDataCount == 0)
            {
                if(action_finish != null)
                    action_finish(true);
            }
        }

        /// <summary>
        /// 适配满意度管理
        /// </summary>
        void AdapterSFM(JFormBase fb)
        {
            currentSatisfactionManagement = new M_SatisfactionManageSource();
            currentSatisfactionManagement.Code = fb.Code;
            currentSatisfactionManagement.ID = fb.ID;
            currentSatisfactionManagement.ParentID = fb.ParentID;
            currentSatisfactionManagement.ListGroup = Adapter_common_group(fb.GroupList);
        }

        /// <summary>
        /// 适配客户维系管理
        /// </summary>
        void AdapterCTMM(JFormBase fb)
        {
            currentClientMange = new M_ClientMange();
            currentClientMange.Code = fb.Code;
            currentClientMange.ID = fb.ID;
            currentClientMange.ParentID = fb.ParentID;
            currentClientMange.ListGroup = Adapter_common_group(fb.GroupList);
        }

        /// <summary>
        /// 适配来场促进管理
        /// </summary>
        void AdapterTP(JFormBase fb)
        {
            currentMangement = new M_FactoryManageSource();
            currentMangement.Code = fb.Code;
            currentMangement.ID = fb.ID;
            currentMangement.ParentID = fb.ParentID;
            currentMangement.ListGroup = Adapter_common_group(fb.GroupList);
        }

        /// <summary>
        /// 适配重点业务
        /// </summary>
        void Adapter_RP(JFormBase fb)
        {
            currentEmphasisBusiness = new M_EmphasisBusinessSource();
            currentEmphasisBusiness.Code = fb.Code;
            currentEmphasisBusiness.ID = fb.ID;
            currentEmphasisBusiness.ParentID = fb.ParentID;
            currentEmphasisBusiness.ListGroup = Adapter_common_group(fb.GroupList);
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
