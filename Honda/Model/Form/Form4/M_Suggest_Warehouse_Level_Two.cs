using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 五星级仓库评价表小组
    /// </summary>
    [Serializable]
    public class M_Suggest_Warehouse_Level_Two : List<M_Suggest_Warehouse_Level_Three>
    {
        public string _GroupName { get; set; }

        /// <summary>
        /// 上次评价的分值
        /// </summary>
        public double _level_Two_LastScore
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < this.Count; i++)
                {
                    sum += this[i]._level_Three_LastScore;
                }

                return sum;
            }
        }


        /// <summary>
        /// 特约店自评的分值
        /// </summary>
        public double _level_Two_SelfScore
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < this.Count; i++)
                {
                    sum += this[i]._level_Three_SelfScore;
                }

                return sum;
            }
        }


        /// <summary>
        /// 巡回评价的分值
        /// </summary>
        public double _level_Two_TourScore
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < this.Count; i++)
                {
                    sum += this[i]._level_Three_TourScore;
                }

                return sum;
            }
        }

        /// <summary>
        /// 数据源所有的项是否都评价了
        /// </summary>
        public bool isEvaluateOflevel_Two 
        {
            get
            {
                bool isEvaluate = true;
                foreach (M_Suggest_Warehouse_Level_Three item in this)
                {
                    if (!item.isEvaluateOflevel_Three)
                    {
                        isEvaluate = false;
                        break;
                    }
                }
                return isEvaluate;
            }
        }

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
