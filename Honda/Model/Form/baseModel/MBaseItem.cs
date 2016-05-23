using Honda.HttpLib.JsonModelData;
using Honda.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*看低保图
     * 大部分表格的“巡回评价”评分项都有直接选择合格或是不合格，
     * 而 零部件评价-》仓库管理-》硬件环境和 建议加分项-》五星级仓库 中的“巡回评价”
     * 没有直接的评分功能，是根据其他表格的评分来获取分数，
     */

    [Serializable]
    public class MBaseItem
    {
        /// <summary>
        /// 自身的ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 所属巡回店的ID
        /// </summary>
        public int ShopID { get; set; }

        /// <summary>
        /// 待定
        /// </summary>
        public string TaskID { get; set; }

        /// <summary>
        /// 模板类型（是属于1~~5号模板中的哪一项）
        /// </summary>
        public ENUM_FORM_ITEM_TEMPLATE ENUMItemTemplate { get; set; }


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
        /// 上次评价是否通过
        /// </summary>
        public bool bIsLastTimePass { get; set; }

        /// <summary>
        /// 判断巡回评价分数是否超出范围
        /// </summary>
        public bool bIsTourScoreOutOfRange { get; set; }

        /// <summary>
        /// 自评是否通过
        /// </summary>
        public bool bIsSelfEvaluation { get; set; }


        /// <summary>
        /// 巡回店评价是否通过
        /// </summary>
        private bool _isEvaluationOfTour;


        /// <summary>
        /// 巡回店评价是否通过
        /// </summary>
        public bool bIsEvaluationOfTour
        {
            get
            {
                //if (_evaluationSort == EvaluationSort.storeEvaluation || _evaluationSort == EvaluationSort.plusesEvaluation)
                //{
                //    _qualifiedScore = 700;
                //    if (DMSuggestPluses.INSTANCE.currentWarehouse != null)
                //    {
                //        _cellTourScore = DMSuggestPluses.INSTANCE.currentWarehouse._pageTourScore;
                //    }

                //    GetScore(_cellTourScore);
                //}

                return _isEvaluationOfTour;
            }
            set { _isEvaluationOfTour = value; }
        }


        private MRemarks _remark;


        /// <summary>
        /// 备注
        /// </summary>
        public MRemarks remarks
        {
            get
            {
                if (_remark == null)
                {
                    _remark = new MRemarks();
                }
                return _remark;
            }
            set
            {
                if (value != _remark)
                {
                    _remark = value;
                }
            }
        }


        /// <summary>
        /// 是否做出评价
        /// </summary>
        public bool isEvaluate { get; set; }


        public MBaseItem()
        {
            //_isEvaluationOfTour = true;
            isEvaluate = true;
        }

        /// <summary>
        /// 获取巡评价分数（单选方式）
        /// </summary>
        /// <param name="isQualified"></param>
        public void GetScore(bool isQualified)
        {
            isEvaluate = true;
            if (isQualified)
            {
                _isEvaluationOfTour = true;
            }
            else
            {
                _isEvaluationOfTour = false;
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
        /// 根据分数判断是否合格，如果是单选情况不用判断，如果是填值方式需要判断
        /// </summary>
        /// <returns></returns>
        private void SetQualified(double score)
        {
            if (score >= _qualifiedScore)
            {
                _isEvaluationOfTour = true;
            }
            else
            {
                _isEvaluationOfTour = false;
            }
        }
    }
}