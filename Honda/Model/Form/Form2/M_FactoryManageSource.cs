using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*服务管理评价 - 来厂促进管理
     */
    [Serializable]
    public class M_FactoryManageSource : M_Common_Source
    {
        public M_FactoryManageSource()
            : base("来厂促进管理")
        {
            _pageTotalScore = 80;
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
                        group._GroupTotalScore = 30;
                        group._InspectionMethod = "1、检查定期保养招揽录音2条，所有高级招揽录音项目各1条，确认招揽话术和措施\n2、检查特约店客服系统—分析决策—定期保养招揽，确认招揽成功率\n3、检查客服系统-分析决策-招揽报表-定期保养，确认招揽成功率。";
                        group._EvaluationCriterion = "以上检查方法对应项目1项不合格得20分，2项不合格得20分，3项不合格得0分。";
                        break;

                    case 1:
                        group._GroupTotalScore = 50;
                        group._InspectionMethod = "1、检查每月活动主题，全店培训照片，客户参与照片，宣传海报和现场布置照片\n2、检查每月活动达标情况的分析报告和应对措施";
                        group._EvaluationCriterion = "以上标准要求1项不合格得40分，2项不合格得30分，3项及以上不合格得0分。";
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
                       
                        group._level_One_TourScore = GetGroupScore1(fullScore, 20,20, failCount);
                        group._level_One_SelfScore = GetGroupScore1(fullScore, 20, 20, failSelfCount);
                        group._level_One_LastScore = GetGroupScore1(fullScore, 20, 20, failLastCount);   
                        break;

                    case 1:
                       
                        group._level_One_TourScore = GetGroupScore1(fullScore, 40, 30, failCount);
                        group._level_One_SelfScore = GetGroupScore1(fullScore, 40, 30, failSelfCount);
                        group._level_One_LastScore = GetGroupScore1(fullScore, 40, 30, failLastCount);
                        break;
                }
            }
        }

    }
}
