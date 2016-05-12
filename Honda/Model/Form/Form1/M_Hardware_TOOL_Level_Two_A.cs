using Honda.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 快修工具小组表格
    /// </summary>
    [Serializable]
    public class M_Hardware_TOOL_Level_Two_A : List<MItem_FiveSAndSafe>, IHardwareTool
    {
        /// <summary>
        /// 组的ID 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 组的父级ID 
        /// </summary>
        public string ParentID { get; set; }



        /// <summary>
        /// 序号
        /// </summary>
        public string _number { get; set; }
        public string _toolGroupName { get; set; }

        public HardwareTool_Form_Typle _hardwareTool_Form_Typle { get; set; }

        /// <summary>
        /// 上次评价的分值
        /// </summary>
        public double _level_Two_LastScore
        {
            get
            {
                double sum = 0;
                for(int i = 0; i < this.Count; i++)
                {
                    sum += this[i]._cellLastScore;
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
                    sum += this[i]._cellSelfScore;
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
                    sum += this[i]._cellTourScore;
                }

                return sum;
            }
        }

        /// <summary>
        /// 是否该组所有的项都评价了
        /// </summary>
        public bool _isEvaluateOf_level_Two
        {
            get
            {
                bool isEvaluate = true;
                for (int i = 0; i < this.Count; i++)
                {
                    if(!this[i].isEvaluate)
                    {
                        isEvaluate = false;
                        break;
                    }
                }

                return isEvaluate;
            }
        }

        public M_Hardware_TOOL_Level_Two_A()
        {
            _hardwareTool_Form_Typle = HardwareTool_Form_Typle._TOOL_QUICK;
        }

    }
}
