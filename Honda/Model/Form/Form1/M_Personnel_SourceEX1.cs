using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form.Form1
{
    /*
     * 服务基础评价-人员
     
     */
    [Serializable]
    public class M_Personnel_SourceEX1 : M_Personnel_Source
    {

        public M_Personnel_SourceEX1()
        {
            _pageTotalScore = 85;
        }

       

        /// <summary>
        /// 初始化二级表单组的数据
        /// </summary>
        public override void InitFormData()
        {
            for (int i = 0; i < _lstGroup.Count; i++)
            {                
                switch (i)
                {
                    case 0:

                        M_Personnel_Configuration_Group group = (M_Personnel_Configuration_Group)_lstGroup[i];
                        group._InspectionMethod = "使用人员配置评分表逐项对照检查";
                        group._EvaluationCriterion = "未按照标准配备该项得分为0分";
                        group._GroupTotalScore = 20;
                      
                        break;

                    case 1:
                        M_Personnel_Train_Group group1 = (M_Personnel_Train_Group)_lstGroup[i];
                         group1._InspectionMethod = "1、导出OA系统培训率数据和标准要求对比是否符合要求。\n2、确认主要岗位人员在OA系统中是否有以下培训记录。（技术主管获取DT资格/IDI获取RT及IDI培训资格/质检及班组长获取RT资格/客服经理参加并通过认证培训/服务经理参加并通过认证培训/保修鉴定员参加并通过认证（至少1人））";
                         group1._EvaluationCriterion = "以上检查方法对应项目1项不达标该项得0分，并要求特约店参加对应培训";
                         group1._GroupTotalScore = 20;
                        break;

                    case 2:
                        M_Personnel_Evaluation_Group group2 = (M_Personnel_Evaluation_Group)_lstGroup[i];
                        group2._InspectionMethod = "1、利用班后时间对SA、客服人员、机修人员、钣喷人员进行现场基础能力测试，总结时公示成绩";
                        group2._EvaluationCriterion = "平均成绩换算记录到该项得分";
                        group2._GroupTotalScore = 30;
                        break;

                    case 3:
                        M_Personnel_Train_Group group3 = (M_Personnel_Train_Group)_lstGroup[i];
                        group3._InspectionMethod = "1、查看特约店绩效管理体系文件\n2、查看每月指标达成情况分析报表\n3、员工访谈确认绩效和激励方案的合理性";
                        group3._EvaluationCriterion = "以上检查方法对应项目1项不合格得5分。2项不合格得0分";
                         group3._GroupTotalScore = 10;
                        break;

                    case 4:
                         M_Personnel_Train_Group group4 = (M_Personnel_Train_Group)_lstGroup[i];
                         group4._InspectionMethod = "参加特约店早会，拍全员合影确认（巡回员在场）";
                         group4._EvaluationCriterion = "现场发现2人未按要求着装该项为0分";
                         group4._GroupTotalScore = 5;
                        break;
                }
            }

        }


        /// <summary>
        /// 开始打分
        /// </summary>
        public override void DoEvaluate()
        {
            for (int i = 0; i < _lstGroup.Count; i++)
            {
                
                double fullScore = 0;
                int failCount = 0;

                switch (i)
                {
                    case 0:
                        M_Personnel_Configuration_Group group = (M_Personnel_Configuration_Group)_lstGroup[i];
                        fullScore = group._GroupTotalScore;
                        failCount = group._failCount;
                        group._level_One_TourScore = GetGroupScore2(fullScore, failCount);
                        group._level_One_LastScore = GetGroupScore2(fullScore, group._failLastCount);
                        group._level_One_SelfScore = GetGroupScore2(fullScore, group._failSelfCount);
                        break;

                    case 1:
                        M_Personnel_Train_Group group1 = (M_Personnel_Train_Group)_lstGroup[i];
                        fullScore = group1._GroupTotalScore;
                        failCount = group1._failCount;
                        group1._level_One_TourScore = GetGroupScore2(fullScore, failCount);
                        group1._level_One_SelfScore = GetGroupScore2(fullScore, group1._failSelfCount);
                        group1._level_One_LastScore = GetGroupScore2(fullScore, group1._failLastCount);
                        break;

                    case 2:
                        //该组是直接填写分数，当评价的分数大于30分的时候，该组分数是30分，当评价的分数小于0分的时候，该组分数是零分
                        M_Personnel_Evaluation_Group group2 = (M_Personnel_Evaluation_Group)_lstGroup[i];
                        if (group2._level_One_TourScore > 30 )
                        {
                            group2._level_One_TourScore = 30;
                        }
                        else if (group2._level_One_TourScore < 0)
                        {
                            group2._level_One_TourScore = 0;
                        }
                        
                        break;

                    case 3:

                        M_Personnel_Train_Group group3 = (M_Personnel_Train_Group)_lstGroup[i];
                        fullScore = group3._GroupTotalScore;
                        failCount = group3._failCount;
                        group3._level_One_TourScore = GetGroupScore(fullScore,5, failCount);
                        group3._level_One_SelfScore = GetGroupScore(fullScore, 5, group3._failSelfCount);
                        group3._level_One_LastScore = GetGroupScore(fullScore, 5, group3._failLastCount);
                        break;

                    case 4:

                        M_Personnel_Train_Group group4 = (M_Personnel_Train_Group)_lstGroup[i];
                        fullScore = group4._GroupTotalScore;
                        failCount = group4._failCount;
                        group4._level_One_TourScore = GetGroupScore2(fullScore, failCount);
                        group4._level_One_SelfScore = GetGroupScore2(fullScore, group4._failSelfCount);
                        group4._level_One_LastScore = GetGroupScore2(fullScore, group4._failLastCount);
                        break;
                }
            }
        }


    }
}
