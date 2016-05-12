using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*服务基础评价 - 接待流程
     */
    [Serializable]
    public class M_ReceiveGuestsFlowSource : M_Common_Source
    {
        public M_ReceiveGuestsFlowSource()
            : base("接待流程")
        {
            _pageTotalScore = 50;
        }

         
        /// <summary>
        /// 初始化二级表单组的数据
        /// </summary>
        public override void InitFormData()
        {
            for(int i = 0 ;i < _listGroup.Count; i++)
            {
                M_Common_Groupcs group = _listGroup[i];
                switch(i)
                {
                    case 0:
                        group._GroupTotalScore = 10;
                        group._InspectionMethod = "1、现场检查预约管理看板中客户信息、快速服务单是否提前填写作业内容。\n2、检查预约管理界面是否有超时显示。";
                        group._EvaluationCriterion = "以上检查方法对应项目1项不合格得5分。2项不合格得0分";
                        break;

                    case 1:
                        group._GroupTotalScore = 8;
                        group._InspectionMethod = "通过调取接车区录像方式确认。";
                        group._EvaluationCriterion = "以上标准要求1项不合格得4分。2项不合格得0分";
                        break;

                    case 2:
                        group._GroupTotalScore = 8;
                        group._InspectionMethod = "1、抽查一周的快速服务单，检查SA填写是否清晰完整填写客户内容。\n2、检查客户签名是否齐全，手机号码是否记录。";
                        group._EvaluationCriterion = "以上检查方法对应项目1项不合格得4分。2项不合格得0分";
                        break;

                    case 3:
                        group._GroupTotalScore = 6;
                        group._InspectionMethod = "1、估价单维修类型是否和作业内容匹配，成组录入是否有深化养护。\n2、是否有客户签名，是否有解说的标识。";
                        group._EvaluationCriterion = "以上检查方法对应项目1项不合格得3分。2项不合格得0分";
                        break;

                    case 4:
                        group._GroupTotalScore = 8;
                        group._InspectionMethod = "1、通过调取录像或现场跟车检查\n2、检查车历卡客户联系方式是否和问诊表保持一致";
                        group._EvaluationCriterion = "标准要求1项不合格得4分。2项不合格得0分";
                        break;

                    case 5:
                        group._GroupTotalScore = 10;
                        group._InspectionMethod = "检查监控录像或者现场跟车确认";
                        group._EvaluationCriterion = "标准要求1项不合格得5分。2项不合格得0分";
                        break;
                }
            }
            
        }


        /// <summary>
        /// 开始打分
        /// </summary>
        public override void DoEvaluate()
        {
            for (int i = 0; i < _listGroup.Count; i++)
            {
                M_Common_Groupcs group = _listGroup[i];
                double fullScore = group._GroupTotalScore;
                int failCount = group.GetFailCount();
                int failLastCount = group.GetFailLastCount();
                int failSelfCount = group.GetFailSelftCount();

                switch (i)
                {
                    case 0:
                       
                        group._level_One_TourScore = GetGroupScore(fullScore, 5, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 5, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 5, failLastCount);
                        break;

                    case 1:
                       
                        group._level_One_TourScore = GetGroupScore(fullScore, 4, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 4, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 4, failLastCount);

                        break;

                    case 2:
                        group._level_One_TourScore = GetGroupScore(fullScore, 4, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 4, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 4, failLastCount);
                        break;

                    case 3:
                        group._level_One_TourScore = GetGroupScore(fullScore, 3, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 3, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 3, failLastCount);
                        break;

                    case 4:
                        group._level_One_TourScore = GetGroupScore(fullScore, 4, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 4, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 4, failLastCount);
                        break;

                    case 5:
                        group._level_One_TourScore = GetGroupScore(fullScore, 5, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 5, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 5, failLastCount);
                        break;
                }
            }
        }

    }
}
