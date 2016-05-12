using Honda.Model.Form.baseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 建议加分项 - 五星级仓库评价表
    /// </summary>
    [Serializable]
    public class M_Suggest_Warehouse_Source : MBaseSource
    {

        private List<M_Suggest_Warehouse_Group> _lstGroup;
        public List<M_Suggest_Warehouse_Group> LstGroup
        {
            get
            {
                if (_lstGroup == null)
                {
                    _lstGroup = new List<M_Suggest_Warehouse_Group>();
                }
                return _lstGroup;
            }

            set
            {
                _lstGroup = value;
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

                foreach (M_Suggest_Warehouse_Group _Group in LstGroup)
                {
                    sum += _Group._level_One_TourScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 评价表总分值
        /// </summary>
        public new double _pageSelfScore
        {
            get
            {
                double sum = 0;

                foreach (M_Suggest_Warehouse_Group _Group in LstGroup)
                {
                    sum += _Group._level_One_SelfScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 评价表总分值
        /// </summary>
        public new double _pageLastScore
        {
            get
            {
                double sum = 0;

                foreach (M_Suggest_Warehouse_Group _Group in LstGroup)
                {
                    sum += _Group._level_One_LastScore;
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
                foreach (M_Suggest_Warehouse_Group item in LstGroup)
                {
                    if (!item._isEvaluateOfGroups)
                    {
                        isEvaluate = false;
                        break;
                    }
                }
                return isEvaluate;
            }
        }

        public M_Suggest_Warehouse_Source()
        {
           
            _projectName = "五星级仓库评价表";
        }

        public void GetScore()
        {

        }

    }
}
