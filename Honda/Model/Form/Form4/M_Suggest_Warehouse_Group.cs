using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 五星级仓库组数据源 
    /// </summary>
    [Serializable]
    public class M_Suggest_Warehouse_Group : MItemBaseGroup
    {

        private List<M_Suggest_Warehouse_Level_Two> _listGroup;
        public List<M_Suggest_Warehouse_Level_Two> ListGroup
        {
            get
            {
                if (_listGroup == null)
                {
                    _listGroup = new List<M_Suggest_Warehouse_Level_Two>();
                }
                return _listGroup;
            }

            set
            {
                _listGroup = value;
            }
        }


        /// <summary>
        /// 上次评价的分值
        /// </summary>
        public double _level_One_LastScore
        {
            get
            {
                double sum = 0;
                foreach (M_Suggest_Warehouse_Level_Two _wareHouse in ListGroup)
                {
                    sum += _wareHouse._level_Two_LastScore;
                }

                return sum;
            }
        }


        /// <summary>
        /// 特约店自评的分值
        /// </summary>
        public double _level_One_SelfScore
        {
            get
            {
                double sum = 0;
                foreach (M_Suggest_Warehouse_Level_Two _wareHouse in ListGroup)
                {
                    sum += _wareHouse._level_Two_SelfScore;
                }

                return sum;
            }
        }


        /// <summary>
        /// 巡回评价的分值
        /// </summary>
        public double _level_One_TourScore
        {
            get
            {
                double sum = 0;
                foreach (M_Suggest_Warehouse_Level_Two _wareHouse in ListGroup)
                {
                    sum += _wareHouse._level_Two_TourScore;
                }

                return sum;
            }
        }

        /// <summary>
        /// 是否所有的项都评价了
        /// </summary>
        public bool _isEvaluateOfGroups
        {
            get
            {
                bool isEvaluate = true;
                foreach (M_Suggest_Warehouse_Level_Two item in ListGroup)
                {
                    if (!item.isEvaluateOflevel_Two)
                    {
                        isEvaluate = false;
                        break;
                    }
                }
                return isEvaluate;
            }
        }

        /// <summary>
        /// 具体评价内容
        /// </summary>
        public string _EvaluationGroupContent { get; set; }
    }

}
