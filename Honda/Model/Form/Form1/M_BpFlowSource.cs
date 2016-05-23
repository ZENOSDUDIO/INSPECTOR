using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*服务基础评价 - BP流程
     */
    [Serializable]
    public class M_BpFlowSource : M_Common_Source
    {
        public M_BpFlowSource()
            : base("BP流程")
        {
            _pageTotalScore = 20;
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
                        group._InspectionMethod = "现场确认3台在修车辆的羽状边，底漆，抛光方法";
                        group._EvaluationCriterion = "标准要求中3项不合格得0分，2项不合格得10分，1项不合格得15分，全合格得20分";
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

                        group._level_One_TourScore = GetGroupScore1(fullScore, 15, 10, failCount);
                        group._level_One_SelfScore = GetGroupScore1(fullScore, 15, 10, failSelfCount);
                        group._level_One_LastScore = GetGroupScore1(fullScore, 15, 10, failLastCount);   
                        break;
                    
                }
            }
        }

    }
}
