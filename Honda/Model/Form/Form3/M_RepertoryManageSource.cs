using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*零部件评价 - 库存管理
     */
    [Serializable]
    public class M_RepertoryManageSource : M_Common_Source
    {
        public M_RepertoryManageSource()
            : base("库存管理")
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
                        group._InspectionMethod = "1、检查每月的库存结构分析表";
                        group._EvaluationCriterion = "有2项不合格得0分，1项不合格得5分，全合格得10分";
                        break;

                    case 1:
                        group._GroupTotalScore = 25;
                        group._InspectionMethod = "1、检查缺货原因分析表\n2、各频度零件抽查3个ROQ基准(超过2个不合理该项不合格)";
                        group._EvaluationCriterion = "有2项不合格得0分，1项不合格得10分，全合格得25分";
                        break;

                    case 2:
                        group._GroupTotalScore = 15;
                        group._InspectionMethod = "1、检查申购审批单、奖惩文件及凭证\n2、检查滞销零件上传记录\n3、库存结构分析表的G频比例";
                        group._EvaluationCriterion = "有2项不合格得0分，1项不合格得7分，全合格得15分";
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
                       
                        group._level_One_TourScore = GetGroupScore(fullScore,10, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 10, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 10, failLastCount);
                        break;

                    case 2:
                        group._level_One_TourScore = GetGroupScore(fullScore, 7, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 7, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 7, failLastCount);
                        break;
                  
                }
            }
        }



    }
}
