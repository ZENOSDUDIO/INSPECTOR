using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 建议加分项-->五星级仓库
    /// </summary>
    [Serializable]
    public class MItem_Suggest_Warehouse : MBaseItem
    {
        public MItem_Suggest_Warehouse()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smallItem">小项</param>
        /// <param name="Sort">分类</param>
        /// <param name="target">细项/指标</param>
        /// <param name="itemScore">分值</param>
        /// <param name="standardForEvaluation">评分标准</param>
        /// <param name="cellLastScore">上一次评价结果</param>
        /// <param name="cellSelfScore">特约店自评结果</param>
        /// <param name="cellTourScore"></param>

        public MItem_Suggest_Warehouse(string smallItem, string Sort, string target, double itemScore, string standardForEvaluation, double cellLastScore, double cellSelfScore, double cellTourScore)
        {
            strSmallItem = smallItem;
            strSort = Sort;
            strTarget = target;
            _itemScore = itemScore;
            strStandardForEvaluation = standardForEvaluation;
            _cellLastScore = cellLastScore;
            _cellSelfScore = cellSelfScore;
            _cellTourScore = cellTourScore;
        }

        ///// <summary>
        ///// 上一次评价分数
        ///// </summary>
        //public new double _cellLastScore ;

        ///// <summary>
        ///// 特约店自评结果分数
        ///// </summary>
        //public new double _cellSelfScore ;

        ///// <summary>
        ///// 巡回评价分数
        ///// </summary>
        //public new double _cellTourScore = 0;

        public double _itemScore;

        /// <summary>
        /// 小项
        /// </summary>
        public string strSmallItem { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string strSort { get; set; }

        /// <summary>
        /// 细项/指标
        /// </summary>
        public string strTarget { get; set; }

        /// <summary>
        /// 评分标准
        /// </summary>
        public string strStandardForEvaluation { get; set; }

    }
}
