using Honda.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Interface
{
    public interface IGroup
    {
        /// <summary>
        /// 表格中 ，组的种类
        /// </summary>
        ItemGroupTyple itemGroupType { get; set; }

        /// <summary>
        /// 具体评价内容
        /// </summary>
        string _EvaluationGroupContent { get; set; }

        /// <summary>
        /// 上次评价的分值
        /// </summary>
        double _level_One_LastScore { get; set; }

        /// <summary>
        /// 特约店自评的分值
        /// </summary>
        double _level_One_SelfScore { get; set; }

        /// <summary>
        /// 巡回评价的分值
        /// </summary>
        double _level_One_TourScore { get; set; }
    }
}