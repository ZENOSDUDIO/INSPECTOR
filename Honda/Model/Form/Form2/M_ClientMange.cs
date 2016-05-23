using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*
     * 服务管理评价 - 客户维系管理
     */
    [Serializable]
    public class M_ClientMange : M_Common_Source
    {
        public M_ClientMange()
            : base("客户维系管理")
        {
            _pageTotalScore = 60;
        }

        /// <summary>
        /// 初始化二级表单组的数据
        /// </summary>
        public override void InitFormData()
        {
            for (int i = 0; i < _listGroup.Count; i++)
            {
                M_Common_Groupcs group = _listGroup[i];
                switch (i)
                {
                    case 0:
                        group._GroupTotalScore = 10;
                        group._InspectionMethod = "1、检查每月保养周期数据（DMS-高级查询-客户信息-保养周期数据），是否针对保养间隔里程长和周期长的客户有针对性的招揽活动\n2、检查特约店客户分析报告，提供招揽对象清单，检查三个以上招揽录音";
                        group._EvaluationCriterion = "以上检查方法对应项目1项未达标得5分.2项未达标整体项目为0分。";
                        break;

                    case 1:
                        group._GroupTotalScore = 30;
                        group._InspectionMethod = "1、检查特约店全年活动计划书面文件，确认计划落实情况\n2、检查会员章程和会员活动方案\n3、检查30天/1000KM关怀对象清单和方案，清单和DMS系统比对\n4、检查关怀短信发送模板";
                        group._EvaluationCriterion = "以上标准要求一项未达标得20分.2项不合格得10分，3项不合格得0分。";
                        break;

                    case 2:
                        group._GroupTotalScore = 20;
                        group._InspectionMethod = "1、检查客服系统-高级招揽-半年流失客户招揽记录\n2、抽查客服系统-分析决策-录音，导出三个以上半年流失客户录音，检查措施及话术";
                        group._EvaluationCriterion = "以上标准要求1项不合格得10分，2项不合格得0分";
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

                        group._level_One_TourScore = GetGroupScore1(fullScore, 20, 10, failCount);
                        group._level_One_SelfScore = GetGroupScore1(fullScore, 20, 10, failSelfCount);
                        group._level_One_LastScore = GetGroupScore1(fullScore, 20, 10, failLastCount);

                        break;

                    case 2:

                        group._level_One_TourScore = GetGroupScore(fullScore, 10, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 10, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 10, failLastCount);

                        break;
                }
            }
        }
    }
}
