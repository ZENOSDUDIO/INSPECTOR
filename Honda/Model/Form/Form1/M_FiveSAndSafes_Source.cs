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

    
    /*
     表单 5S及&安全
     *总分：
     */
    [Serializable]
    public class M_FiveSAndSafes_Source : MBaseSource, IGroup
    {
        private List<MFiveSAndSafes> _lstGroup;
        public List<MFiveSAndSafes> LstGroup
        {
            get
            {
                if (_lstGroup == null)
                {
                    _lstGroup = new List<MFiveSAndSafes>();
                }

                for (int i = 0; i < _lstGroup.Count; i++)
                {
                    MFiveSAndSafes group = _lstGroup[i];
                    //设置评价后的分值
                    double groupScore = 0;
                    double groupSelf = 0;
                    double groupLast = 0;
                    for(int j =0 ; j <group.Count; j++)
                    {
                        MItem_FiveSAndSafe cell = group[j];
                        if(cell.bIsEvaluationOfTour)
                        {
                            groupScore += 2;
                        }

                        if(cell.bIsSelfEvaluation)
                        {
                            groupSelf += 2;
                        }

                        if(cell.bIsLastTimePass)
                        {
                            groupLast += 2;
                        }
                    }
                    group._level_One_TourScore = groupScore;
                    group._level_One_SelfScore = groupSelf;
                    group._level_One_LastScore = groupLast;
                }

                return _lstGroup;
            }

            set
            {
                _lstGroup = value;

                for (int i = 0; i < _lstGroup.Count; i++)
                {
                    MFiveSAndSafes group = _lstGroup[i];
                    //设置该小组的满分分值
                    group.TotalScore = group.Count * 2;
                }
            }
        }

        /// <summary>
        /// 表格中 ，组的种类
        /// </summary>
        public ItemGroupTyple itemGroupType
        {
            get
            {
                return ItemGroupTyple.fiveSType;
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
        /// 巡回评价的总分值
        /// </summary>
        public double _pageTourScore
        {
            get
            {
                double sum = 0;

                foreach (MFiveSAndSafes _fiveS in LstGroup)
                {
                    sum += _fiveS._level_One_TourScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 上次巡回评价的总分值 
        /// </summary>
        public new double _pageLastScore
        {
            get
            {
                double sum = 0;

                foreach (MFiveSAndSafes _fiveS in LstGroup)
                {
                    sum += _fiveS._level_One_LastScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 自评的总分值 
        /// </summary>
        public new double _pageSelfScore
        {
            get
            {
                double sum = 0;

                foreach (MFiveSAndSafes _fiveS in LstGroup)
                {
                    sum += _fiveS._level_One_SelfScore;
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
                foreach (MFiveSAndSafes _fiveS in LstGroup)
                {
                    if (!_fiveS._isEvaluateOfGroups)
                    {
                        isEvaluate = false;
                        break;
                    }
                }
                return isEvaluate;
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

        public M_FiveSAndSafes_Source()
        {
            _projectName = "1、5S及&安全";
            _pageTotalScore = 100;
            InspectionMethod = "按照5S检查标准检查";
            EvaluationCriterion = "按照评分表成绩得分";
        }
    }
}
