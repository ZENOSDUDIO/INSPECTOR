using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    

    public class MBaseItem
    {
        /// <summary>
        /// 上一次评价分数
        /// </summary>
        public double _cellLastScore { get; set; }

        /// <summary>
        /// 特约店自评结果分数
        /// </summary>
        public double _cellSelfScore { get; set; }

        /// <summary>
        /// 巡回评价分数
        /// </summary>
        public double _cellTourScore { get; set; }

        /// <summary>
        /// 合格时至少要大于或者等于的分数
        /// </summary>
        public double _qualifiedScore { get; set; }


        /// <summary>
        ///当评分的方式是单选时候，合格时的分数
        /// </summary>
        public double _RadioQualifiedScore;

        /// <summary>
        ///当评分的方式是单选时候，不合格时的分数
        /// </summary>
        public double _RadioDisqualifiedScore;

        /// <summary>
        /// 是否合格
        /// </summary>
        public bool isQualified { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public MRemarks remarks { get; set; }

        /// <summary>
        /// 是否做出评价
        /// </summary>
        public bool isEvaluate { get; set; }

        


        public MBaseItem()
        {
            isEvaluate = false;
            _qualifiedScore = _cellTourScore;
            _RadioDisqualifiedScore = 0;
        }

        /// <summary>
        /// 获取巡评价分数（单选方式）
        /// </summary>
        /// <param name="isQualified"></param>
        public void GetScore(bool isQualified)
        {
            isEvaluate = true;
            if(isQualified)
            {
                _cellTourScore = _RadioQualifiedScore;
                this.isQualified = true;
            }else
            {
                _cellTourScore = _RadioDisqualifiedScore;
                this.isQualified = false;
            }
        }

        /// <summary>
        /// 获取巡评价分数(直接填值方式)
        /// </summary>
        public void GetScore(double score)
        {
            isEvaluate = true;
            _cellTourScore = score;
            SetQualified(_cellTourScore);
        }

        /// <summary>
        /// 根据分数判断是否合格
        /// </summary>
        /// <returns></returns>
        private void SetQualified(double score)
        {
            if(score >= _qualifiedScore)
            {
                isQualified =  true;
            }
            else
            {
                isQualified =  false; 
            }
        }
        

    }
}
