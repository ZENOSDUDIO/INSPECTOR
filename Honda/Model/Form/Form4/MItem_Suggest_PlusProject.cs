using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    ///建议加分项 -->建议加分项的类型
    /// </summary>
    [Serializable]
    public class MItem_Suggest_PlusProject :MBaseItem
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="No">序号</param>
        /// <param name="EvaluationItem">评价项目</param>
        /// <param name="totalScore">分值</param>
        /// <param name="InspectionItem">检查项目</param>
        /// <param name="LastScore">特约店上次评价结果分数</param>
        /// <param name="SelfScore">特约店自评结果</param>
        /// <param name="isSqualified">巡回评价</param>
        public MItem_Suggest_PlusProject(string No, string EvaluationItem, double totalScore, string InspectionItem,
            double LastScore, double SelfScore, bool isSqualified)
        {
            strNo = No;
            strEvaluationItem = EvaluationItem;
            _TotalScore = totalScore;  
            strInspectionItem = InspectionItem;
            _cellLastScore = LastScore;
            _cellSelfScore = SelfScore;
            BIsSqualified = isSqualified;
        }

        public MItem_Suggest_PlusProject()
        {

        }

  
        public double _TotalScore { get; set; }
       

       
        /// <summary>
        /// 序号
        /// </summary>
        public string strNo { get; set; }

        /// <summary>
        /// 评价项目
        /// </summary>
        public string strEvaluationItem { get; set; }


        /// <summary>
        /// 检查项目
        /// </summary>
        public string strInspectionItem { get; set; }


       
        /// <summary>
        /// 巡回店自评是否合格
        /// </summary>
        public bool BIsSqualified { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public MRemarks _remarks { get; set; }


        /// <summary>
        /// 巡回评价分数
        /// </summary>
        public new double _cellTourScore 
        {
            get
            {
                if(bIsEvaluationOfTour)
                {
                    return _TotalScore;
                }else
                { 
                    return 0;
                }
            }
            set
            {
            }
        }


    }
}
