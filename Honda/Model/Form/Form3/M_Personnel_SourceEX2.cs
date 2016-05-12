using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form.Form3
{
    /*
     * 零部件评价-人员
     
     */
    [Serializable]
    public class M_Personnel_SourceEX2 : M_Personnel_Source
    {

        public M_Personnel_SourceEX2()
        {
            _pageTotalScore = 35;
        }

       

        /// <summary>
        /// 初始化二级表单组的数据
        /// </summary>
        public override void InitFormData()
        {
            for (int i = 0; i < _lstGroup.Count; i++)
            {
                M_Personnel_Train_Group _Train_Group = null;
                switch (i)
                {
                    case 0:

                        M_Personnel_Configuration_Group group = (M_Personnel_Configuration_Group)_lstGroup[i];
                        group._InspectionMethod = "1、导出OA人员名单与现场人员逐一确认";
                        group._EvaluationCriterion = "有1项不合格得0分，全合格得12分";
                        group._GroupTotalScore = 12;
                      
                        break;

                    case 1:
                         _Train_Group = (M_Personnel_Train_Group)_lstGroup[i];
                         _Train_Group._InspectionMethod = "1、核实培训、认证记录";
                         _Train_Group._EvaluationCriterion = "有1项不合格得0分，全合格得12分";
                         _Train_Group._GroupTotalScore = 12;
                        break;

                    case 2:
                        _Train_Group = (M_Personnel_Train_Group)_lstGroup[i];
                        _Train_Group._InspectionMethod = "1、现场检查，绩效考核文件，员工调查\n2、问答";
                        _Train_Group._EvaluationCriterion = "有1项不合格得0分，全合格得11分";
                        _Train_Group._GroupTotalScore = 11;
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
                        int failSelftCount = group._failSelfCount;
                        int failLastCount = group._failLastCount;
                        group._level_One_TourScore = GetGroupScore2(fullScore, failCount);
                        group._level_One_SelfScore = GetGroupScore2(fullScore, failSelftCount);
                        group._level_One_LastScore = GetGroupScore2(fullScore, failLastCount);
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
                        M_Personnel_Train_Group group2 = (M_Personnel_Train_Group)_lstGroup[i];
                        fullScore = group2._GroupTotalScore;
                        failCount = group2._failCount;
                        group2._level_One_TourScore = GetGroupScore2(fullScore, failCount);
                        group2._level_One_SelfScore = GetGroupScore2(fullScore, group2._failSelfCount);
                        group2._level_One_LastScore = GetGroupScore2(fullScore, group2._failLastCount);
                        break;       
                }


            }
        }


    }
}
