using Honda.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{

    /// <summary>
    /// 机修工具类型的表格   
    /// </summary>
    [Serializable]
    public class M_Hardware_TOOL_Level_Two_C : List<M_Tools_B>, IHardwareTool
    {


        /// <summary>
        /// 组的ID 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 组的父级ID 
        /// </summary>
        public string ParentID { get; set; }

       public HardwareTool_Form_Typle _hardwareTool_Form_Typle { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string _number { get; set; }
        public string _toolGroupName;

        /// <summary>
        /// 上一次评价小组分数
        /// </summary>
        public double _level_Two_LastScore
        {
            get
            {
                double sum = 0;

                foreach (M_Tools_B tool in this)
                {
                    sum += tool._level_Three_LastScore;
                }
                return sum;
            }

        }

        /// <summary>
        /// 特约店自评结果小组分数
        /// </summary>
        public double _level_Two_SelfScore
        {
            get
            {
                double sum = 0;

                foreach (M_Tools_B tool in this)
                {
                    sum += tool._level_Three_SelfScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 巡回评价小组分数
        /// </summary>
        public double _level_Two_TourScore
        {
            get
            {
                double sum = 0;
                
                foreach (M_Tools_B tool in this)
                {
                    sum += tool._level_Three_TourScore;

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
                    if (!this[i]._isEvaluateOf_level_Three)
                    {
                        isEvaluate = false;
                        break;
                    }
                }

                return isEvaluate;
            }
        }

        /// <summary>
        /// 该项中的最小的项的数量
        /// </summary>
        public int cellCount
        {
            get
            {
                int sum = 0;
                foreach(M_Tools_B tool in this)
                {
                    sum += tool.Count;
                }
                return sum;

            }
            set
            {

            }
        }

        /// <summary>
        /// 该项中的最小的项合格的数量
        /// </summary>
        public int cellPassCount
        {
            get
            {
                int sum = 0;
                foreach (M_Tools_B tool in this)
                {
                    foreach(MItem_Tool_B cell in tool)
                    {
                        if(cell.bIsEvaluationOfTour)
                        {
                            sum++;
                        }
                    }
                }
                return sum;

            }
            set
            {

            }
        }

        /// <summary>
        /// 该项中的所有最小的项自评合格的数量
        /// </summary>
        public int cellSelfPassCount
        {
            get
            {
                int sum = 0;
                foreach (M_Tools_B tool in this)
                {
                    foreach (MItem_Tool_B cell in tool)
                    {
                        if (cell.bIsSelfEvaluation)
                        {
                            sum++;
                        }
                    }
                }
                return sum;

            }
            set
            {

            }
        }

        /// <summary>
        /// 该项中的所有最小的项上次评价合格的数量
        /// </summary>
        public int cellLastPassCount
        {
            get
            {
                int sum = 0;
                foreach (M_Tools_B tool in this)
                {
                    foreach (MItem_Tool_B cell in tool)
                    {
                        if (cell.bIsLastTimePass)
                        {
                            sum++;
                        }
                    }
                }
                return sum;

            }
            set
            {

            }
        }


        public M_Hardware_TOOL_Level_Two_C()
        {
            _hardwareTool_Form_Typle = HardwareTool_Form_Typle._TOOL_MACHINE;
            
        }


    }


}
