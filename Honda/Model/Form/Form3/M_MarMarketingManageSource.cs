using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*零部件评价 - 营销管理
     */
    [Serializable]
    public class M_MarMarketingManageSource : M_Common_Source
    {
        public M_MarMarketingManageSource()
            : base("营销管理")
        {
            _pageTotalScore = 75;
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
                        group._GroupTotalScore = 20;
                        group._InspectionMethod = "1、与总经理面谈\n2、检查工作计划与营销活动资料(图片)\n3、出库数统计计算";
                        group._EvaluationCriterion = "有2项不合格得0分，1项不合格得10分，全合格得20分";
                        break;

                    case 1:
                        group._GroupTotalScore = 40;
                        group._InspectionMethod = "1、检查活动方案、店头布置(照片)、优惠措施(结算凭证)等\n2、检查内部激励公示文件及发放凭证\n3、检查培训记录、图片、签到等\n4、检查日报、周报、活动总结资料等";
                        group._EvaluationCriterion = "有3项不合格得0分，2项不合格得10分，1项不合格得20分，全合格得40分";
                        break;

                    case 2:
                        group._GroupTotalScore = 15;
                        group._InspectionMethod = "1、现场检查";
                        group._EvaluationCriterion = "有1项不合格得0分，全合格得15分";
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
                       
                        group._level_One_TourScore = GetGroupScore(fullScore, 10, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 10, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 10, failLastCount);  
                        break;

                    case 1:
                       
                        group._level_One_TourScore = GetGroupScore1(fullScore, 20, 10, failCount);
                        group._level_One_SelfScore = GetGroupScore1(fullScore, 20, 10, failSelfCount);
                        group._level_One_LastScore = GetGroupScore1(fullScore, 20, 10, failLastCount);

                        break;

                    case 2:
                        group._level_One_TourScore = GetGroupScore2(fullScore, failCount);
                        group._level_One_SelfScore = GetGroupScore2(fullScore, failSelfCount);
                        group._level_One_LastScore = GetGroupScore2(fullScore, failLastCount);
                        break;                 
                }
            }
        }




    }
}
