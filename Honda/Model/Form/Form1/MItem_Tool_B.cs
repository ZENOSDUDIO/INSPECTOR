using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 服务基础评价-->硬件表中的类型机修工具、其他工具中的类型
    /// </summary>
    [Serializable]
    public class MItem_Tool_B : MBaseItem
    {

        public MItem_Tool_B(string toolEquipment, bool bIsLastTimePass, bool bIsSelfEvaluation, bool bIsEvaluationOfTour)
        {
            _ToolEquipment = toolEquipment;
            this.bIsLastTimePass = bIsLastTimePass;
            this.bIsSelfEvaluation = bIsSelfEvaluation;
            this.bIsEvaluationOfTour = bIsEvaluationOfTour;
        }


       

        /// <summary>
        /// 工具设备
        /// </summary>
        public string _ToolEquipment { get; set; }
       
    }


     [Serializable]
    public class M_Tools_B : List<MItem_Tool_B>
    {
        /// <summary>
        /// 上一次评价小组分数
        /// </summary>
        public double _level_Three_LastScore
        {
            get
            {
                double sum = 0;

                foreach (MItem_Tool_B tool in this)
                {
                    sum += tool._cellLastScore;
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

                foreach (MItem_Tool_B tool in this)
                {
                    sum += tool._cellSelfScore;
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

                foreach (MItem_Tool_B tool in this)
                {
                    sum += tool._cellTourScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 是否该组所有的项都评价了
        /// </summary>
        public bool _isEvaluateOf_level_Three
        {
            get
            {
                bool isEvaluate = true;
                for (int i = 0; i < this.Count; i++)
                {
                    if (!this[i].isEvaluate)
                    {
                        isEvaluate = false;
                        break;
                    }
                }

                return isEvaluate;
            }
        }
        public string _deviceGroupName { get; set; }
    }

    



        
    
}
