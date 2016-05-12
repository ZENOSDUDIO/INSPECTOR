using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    [Serializable]
    public class MItemBaseGroup
    {
        /// <summary>
        /// 该小组所属的索引
        /// </summary>
        public int _index { get; set; }

        /// <summary>
        /// 该小组不及格项的总数
        /// </summary>
        public int _failCount { get; set; }

        /// <summary>
        /// 该组的总分
        /// </summary>
        public double _GroupTotalScore { get; set; }

        /// <summary>
        /// 检查方法
        /// </summary>
        public string _InspectionMethod { get; set; }

        /// <summary>
        /// 评价标准
        /// </summary>
        public string _EvaluationCriterion { get; set; }

        /// <summary>
        /// 获取巡回店评价表该小组的不合格数
        /// </summary>
        public virtual int GetFailCount()
        {
            return _failCount;
        }

        /// <summary>
        /// 组的ID 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 组的父级ID 
        /// </summary>
        public string ParentID { get; set; }


        /// <summary>
        /// 上次评价的分值
        /// </summary>
        public double _level_One_LastScore { get; set; }


        /// <summary>
        /// 特约店自评的分值
        /// </summary>
        public double _level_One_SelfScore { get; set; }


        /// <summary>
        /// 巡回评价的分值
        /// </summary>
        public double _level_One_TourScore { get; set; }


        /// <summary>
        /// 具体评价内容
        /// </summary>
        public string _EvaluationGroupContent { get; set; }

        /// <summary>
        /// 是否所有的项都评价了
        /// </summary>
        public bool _isEvaluateOfGroups { get; set; }
    }
}