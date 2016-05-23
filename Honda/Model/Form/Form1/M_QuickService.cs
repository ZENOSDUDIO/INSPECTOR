using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*服务基础评价 - 快修流程
     */
    [Serializable]
    public class M_QuickService : M_Common_Source
    {
        public M_QuickService()
            : base("快修流程")
        {
            _pageTotalScore = 30;
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
                        group._InspectionMethod = "1、班后抽查保养作业内容完整性（入职一年内机修人员）。\n2、确认保养确认单数据填写完整性 (记录无负荷/负荷电压、轮胎气压、胎纹深度、刹车片厚度)。";
                        group._EvaluationCriterion = "以上检查方法对应项目1项不合格得15分。2项不合格得0分";
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
                       
                        group._level_One_TourScore = GetGroupScore(fullScore, 15, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 15, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 15, failLastCount);        
                        break;                   
                }
            }
        }

       

        


    }
}
