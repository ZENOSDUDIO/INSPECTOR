using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 五星级仓库
    /// </summary>
    [Serializable]
    public class M_Suggest_Warehouse_Level_Three : List<MItem_Suggest_Warehouse>
    {
        /// <summary>
        /// 上一次评价小组分数
        /// </summary>
        public double _level_Three_LastScore
        {
            get
            {
                double sum = 0;

                foreach (MItem_Suggest_Warehouse item in this)
                {
                    sum += item._cellLastScore;
                }
                return sum;
            }

        }

        /// <summary>
        /// 特约店自评结果小组分数
        /// </summary>
        public double _level_Three_SelfScore
        {
            get
            {
                double sum = 0;

                foreach (MItem_Suggest_Warehouse item in this)
                {
                    sum += item._cellSelfScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 巡回评价小组分数
        /// </summary>
        public double _level_Three_TourScore
        {
            get
            {
                double sum = 0;

                foreach (MItem_Suggest_Warehouse item in this)
                {
                    sum += item._cellTourScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 数据源所有的项是否都评价了
        /// </summary>
        public bool isEvaluateOflevel_Three
        {
            get
            {
                bool isEvaluate = true;
                foreach (MItem_Suggest_Warehouse item in this)
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

        public string _GroupName { get; set; }

        /// <summary>
        /// 商店id
        /// </summary>
        public string shopID { get; set; }

        /// <summary>
        /// 组的ID 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 组的父级ID 
        /// </summary>
        public string ParentID { get; set; }
    }
}
