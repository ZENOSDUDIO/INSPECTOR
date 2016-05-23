using Honda.Globals;
using Honda.Interface;
using Honda.Model.Form.baseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 建议加分项 - 加分项
    /// </summary>
    [Serializable]
    public class M_Suggest_PlusProject_Source : MBaseSource, IGroup
    {
        private List<MItem_Suggest_PlusProject> _lstGroup;
        public List<MItem_Suggest_PlusProject> LstGroup
        {
            get
            {
                if (_lstGroup == null)
                {
                    _lstGroup = new List<MItem_Suggest_PlusProject>();
                }
                return _lstGroup;
            }

            set
            {
                _lstGroup = value;
            }
        }


        // <summary>
        /// 表格中 ，组的种类
        /// </summary>
        public ItemGroupTyple itemGroupType
        {
            get
            {
                return ItemGroupTyple.suggestType;
            }

            set
            {
                itemGroupType = value;
            }
        }

        /// <summary>
        /// 具体评价内容
        /// </summary>
        public string _EvaluationGroupContent
        {
            get
            {
                return _projectName;
            }
            set
            {
                _EvaluationGroupContent = value;
            }
        }

        /// <summary>
        /// 上次评价的分值
        /// </summary>
        public double _level_One_LastScore
        {
            get
            {
                return _pageLastScore;
            }
            set
            {
                
            }
        }

        /// <summary>
        /// 特约店自评的分值
        /// </summary>
        public double _level_One_SelfScore
        {
            get
            {
                return _pageSelfScore;
            }
            set
            {

            }
        }

        /// <summary>
        /// 巡回评价的分值
        /// </summary>
        public double _level_One_TourScore
        {
            get
            {
                return _pageTourScore;
            }
            set
            {

            }
        }

        /// <summary>
        /// 检查方法
        /// </summary>
        public string InspectionMethod { get; set; }

        /// <summary>
        /// 评价标准
        /// </summary>
        public string EvaluationCriterion { get; set; }



        /// <summary>
        /// 巡回评价的总分值
        /// </summary>
        public double _pageTourScore
        {
            get
            {
                double sum = 0;

                foreach (MItem_Suggest_PlusProject item in LstGroup)
                {
                    sum += item._cellTourScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 巡回评价的总分值
        /// </summary>
        public double _pageLastScore
        {
            get
            {
                double sum = 0;

                foreach (MItem_Suggest_PlusProject item in LstGroup)
                {
                    sum += item._cellLastScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 巡回评价的总分值
        /// </summary>
        public double _pageSelfScore
        {
            get
            {
                double sum = 0;

                foreach (MItem_Suggest_PlusProject item in LstGroup)
                {
                    sum += item._cellSelfScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 数据源所有的项是否都评价了
        /// </summary>
        public bool _isEvaluateOfSoure
        {
            get
            {
                bool isEvaluate = true;
                foreach (MItem_Suggest_PlusProject item in LstGroup)
                {
                    if (!item.isEvaluate)
                    {
                        isEvaluate = false;
                        break;
                    }
                }
                return isEvaluate;
            }
        }

        public M_Suggest_PlusProject_Source()
        {
            _pageTotalScore = 50;
            _projectName = "加分项";           
        }
    }
}
