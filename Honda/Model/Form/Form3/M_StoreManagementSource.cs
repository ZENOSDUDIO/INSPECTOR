using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*零部件评价 - 仓库管理
     */
    [Serializable]
    public class M_StoreManagementSource : M_Common_Source
    {
        public M_StoreManagementSource()
            : base("仓库管理")
        {
            _pageTotalScore = 40;
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
                        group._GroupTotalScore = 16;
                        group._InspectionMethod = "1、现场检查\n2、按照仓库星级评价表评分(低于700分为不合格)";
                        group._EvaluationCriterion = "有1项不合格得0分，全合格得16分";
                        break;

                    case 1:
                        group._GroupTotalScore = 12;
                        group._InspectionMethod = "1、检查入库单、出库单\n2、检查DMS例外出入库记录\n3、现场检查\n4、检查盘点报告，现场抽查20个零件(有帐物不符的亦属不合格)";
                        group._EvaluationCriterion = "有1项不合格得0分，全合格得12分";
                        break;

                    case 2:
                        group._GroupTotalScore = 12;
                        group._InspectionMethod = "1、现场检查、抽查货位及码放情况\n2、现场抽查10个零件拣货计算平均时间";
                        group._EvaluationCriterion = "有2项不合格得0分，1项不合格得6分，全合格得12分";
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
                       
                        group._level_One_TourScore = GetGroupScore2(fullScore, failCount);
                        group._level_One_SelfScore = GetGroupScore2(fullScore, failSelfCount);
                        group._level_One_LastScore = GetGroupScore2(fullScore, failLastCount);     
                        break;

                    case 1:
                       
                        group._level_One_TourScore = GetGroupScore2(fullScore, failCount);
                        group._level_One_SelfScore = GetGroupScore2(fullScore, failSelfCount);
                        group._level_One_LastScore = GetGroupScore2(fullScore, failLastCount);
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
