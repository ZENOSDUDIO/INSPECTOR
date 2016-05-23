using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honda.Model.Form.baseModel;

namespace Honda.Model.Form
{
    /*零部件评价 - 基础业务
     */

    [Serializable]
    public class M_BaseUnivesalsSource : MBaseSource
    {
        public M_BaseUnivesalsSource()
            : base()
        {
        }

        /// <summary>
        /// 数据源标识
        /// 
        /// </summary>
        public string _sourceIdentify { get; set; }

        /// <summary>
        /// 初始化二级表单组的数据
        /// </summary>
        public override void InitFormData()
        {
            for (int i = 0; i < _listGroup.Count; i++)
            {
                var group = (M_Common_Groupcs) _listGroup[i];
                //switch(i)
                //{
                //    case 0:
                //        group._GroupTotalScore = 14;
                //        group._InspectionMethod = "1、统计各类采购单占比或抽查部分零件(含特殊订单)的采购单(或实操考核)\n2、抽查定金收取单据和零部件申购单\n3、检查BO零件跟踪管理表及零部件跟踪管理系统登陆时间\n4、实操检查“欠款通知”查询熟练程度";
                //        group._EvaluationCriterion = "有2项不合格得0分，有1项不合格得7分，全合格得14分";
                //        break;

                //    case 1:
                //        group._GroupTotalScore = 12;
                //        group._InspectionMethod = "1、分别抽查5个零件进行口头报价和书面报价\n2、检查预约零件跟踪管理表\n3、数据分析和现场检查(含外销记录检查)";
                //        group._EvaluationCriterion = "有2项不合格得0分，1项不合格得6分，全合格得12分";
                //        break;

                //    case 2:
                //        group._GroupTotalScore = 8;
                //        group._InspectionMethod = "1、抽查保修申请单\n2、检查保修信息记录表";
                //        group._EvaluationCriterion = "有1项不合格得0分，全合格得8分";
                //        break;

                //    case 3:
                //        group._GroupTotalScore = 10;
                //        group._InspectionMethod = "1、检查零部件手册\n2、查看EPC版本号。\n3、导出OA人员名单与现场人员逐一确认，现场登陆OA查看是否所有通知及信息都已阅读\n4、检查DMS出入库数据、例外、库存及货位信息是否一致\n5、检查到货时间反馈记录";
                //        group._EvaluationCriterion = "有2项不合格得0分，1项不合格得5分，全合格得10分";
                //        break;

                //    case 4:
                //        group._GroupTotalScore = 6;
                //        group._InspectionMethod = "1、展厅现场检查\n2、检查废机油罐是否打孔，LOGO及标签是否划掉";
                //        group._EvaluationCriterion = "有1项不合格得0分，全合格得6分";
                //        break;


                //}
            }
        }


        /// <summary>
        /// 开始打分
        /// </summary>
        public override void DoEvaluate()
        {
            for (int i = 0; i < _listGroup.Count; i++)
            {
                var group = (M_Common_Groupcs) _listGroup[i];
                double fullScore = group._GroupTotalScore;
                int failCount = group.GetFailCount();
                int failLastCount = group.GetFailLastCount();
                int failSelfCount = group.GetFailSelftCount();
                //switch (i)
                //{
                //    case 0:

                //        group._level_One_TourScore = GetGroupScore(fullScore, 7, failCount);
                //        group._level_One_SelfScore = GetGroupScore(fullScore, 7, failSelfCount);
                //        group._level_One_LastScore = GetGroupScore(fullScore, 7, failLastCount);  
                //        break;

                //    case 1:

                //        group._level_One_TourScore = GetGroupScore(fullScore, 6, failCount);
                //        group._level_One_SelfScore = GetGroupScore(fullScore, 6, failSelfCount);
                //        group._level_One_LastScore = GetGroupScore(fullScore, 6, failLastCount);
                //        break;

                //    case 2:
                //        group._level_One_TourScore = GetGroupScore2(fullScore, failCount);
                //        group._level_One_SelfScore = GetGroupScore2(fullScore, failSelfCount);
                //        group._level_One_LastScore = GetGroupScore2(fullScore, failLastCount);
                //        break;

                //    case 3:
                //        group._level_One_TourScore = GetGroupScore(fullScore, 5, failCount);
                //        group._level_One_SelfScore = GetGroupScore(fullScore, 5, failSelfCount);
                //        group._level_One_LastScore = GetGroupScore(fullScore, 5, failLastCount);
                //        break;

                //    case 4:
                //        group._level_One_TourScore = GetGroupScore2(fullScore, failCount);
                //        group._level_One_SelfScore = GetGroupScore2(fullScore, failSelfCount);
                //        group._level_One_LastScore = GetGroupScore2(fullScore, failLastCount);
                //        break;

                //}
            }
        }
    }
}