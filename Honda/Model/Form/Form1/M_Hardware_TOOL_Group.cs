using Honda.Globals;
using Honda.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    //硬件- 工具类型的表格样式
    [Serializable]
    public class M_Hardware_TOOL_Group :MItemBaseGroup ,IHardware
    {
        private List<IHardwareTool> _lstItem;
        public List<IHardwareTool> LstItem
        {
            get
            {
                if (_lstItem == null)
                {
                    _lstItem = new List<IHardwareTool>();
                }

                return _lstItem;
            }
            set
            {
                _lstItem = value;
            }
        }

       

        /// <summary>
        /// 表格中 ，组的种类
        /// </summary>
        public ItemGroupTyple itemGroupType { get; set; }

        /// <summary>
        /// 硬件页面中所属的表格类型
        /// </summary>
        public Hardware_Form_Typle _Enum_hardware_Typle { get; set; }

        public M_Hardware_TOOL_Group()
        {
            _Enum_hardware_Typle = Hardware_Form_Typle._TOOL_STYLE;
            itemGroupType = ItemGroupTyple.HardwareToolType;
        }

        /// <summary>
        /// 上次评价的分值
        /// </summary>
        public double _level_One_LastScore
        {
            get
            {
                return EvaluatingMethod(GetAllCellLastPassCount());
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
                return EvaluatingMethod(GetAllCellSelfPassCount());
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
                return EvaluatingMethod(GetAllCellPassCount());
            }
            set
            {

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
                foreach (IHardwareTool tool in LstItem)
                {
                    if(!tool._isEvaluateOf_level_Two)
                    {
                        isEvaluate = false;
                        break;
                    }
                }
                return isEvaluate;
            }
        }

        /// <summary>
        /// 具体评价内容(组名)
        /// </summary>
        public string _EvaluationGroupContent { get; set; }


        #region 评分

       
        /// <summary>
        /// 硬件表格中工具组的评分方法
        /// </summary>
        /// <returns>该组的得分</returns>
        public double EvaluatingMethod(int CountPass)
        {
            //该组的得分
            double Score = 0;
            
            //计算出该组的所有小项的数量
            int CoutCell = GetAllCellCount();
            
            //该组的    每一个最小项的分数 = 该组的总分/该组最小项的数量
            double cellScore = _GroupTotalScore / CoutCell;

            //该组的得分等于  = 及格的数量 X 每一个最小项的分数            
            Score = CountPass * cellScore;
            Score = Math.Round(Score, 2);


            return Score;
        }

        /// <summary>
        /// 计算出该组的所有最小项的数量
        /// </summary>
        /// <returns>所有最小项的数量</returns>
        int GetAllCellCount()
        {
            int coutCell = 0;
            foreach (IHardwareTool _tool_group in _lstItem)
            {
                switch (_tool_group._hardwareTool_Form_Typle)
                {
                    case HardwareTool_Form_Typle._TOOL_QUICK:
                        M_Hardware_TOOL_Level_Two_A _tool_level_two_a = (M_Hardware_TOOL_Level_Two_A)_tool_group;
                        coutCell += _tool_level_two_a.Count;
                        break;

                    case HardwareTool_Form_Typle._TOOL_SHEET:
                        M_Hardware_TOOL_Level_Two_B _tool_level_two_b = (M_Hardware_TOOL_Level_Two_B)_tool_group;
                        coutCell += _tool_level_two_b.Count;

                        break;

                    case HardwareTool_Form_Typle._TOOL_MACHINE:
                        M_Hardware_TOOL_Level_Two_C _tool_level_two_c = (M_Hardware_TOOL_Level_Two_C)_tool_group;
                        coutCell += _tool_level_two_c.cellCount;

                        break;
                }
            }
            return coutCell;
        }

        /// <summary>
        /// 计算出该组的所有及格的最小项的数量
        /// </summary>
        /// <returns>及格的数量</returns>
        int GetAllCellPassCount()
        {
            int countPass = 0;
            foreach (IHardwareTool _tool_group in _lstItem)
            {
                switch (_tool_group._hardwareTool_Form_Typle)
                {
                    case HardwareTool_Form_Typle._TOOL_QUICK:
                        M_Hardware_TOOL_Level_Two_A _tool_level_two_a = (M_Hardware_TOOL_Level_Two_A)_tool_group;
                        foreach (MItem_FiveSAndSafe cell1 in _tool_level_two_a)
                        {
                            if(cell1.bIsEvaluationOfTour)
                            {
                                countPass++;
                            }
                        }
                        
                        break;

                    case HardwareTool_Form_Typle._TOOL_SHEET:
                        M_Hardware_TOOL_Level_Two_B _tool_level_two_b = (M_Hardware_TOOL_Level_Two_B)_tool_group;
                        foreach (MItem_Tool_A cell2 in _tool_level_two_b)
                        {
                            if (cell2.bIsEvaluationOfTour)
                            {
                                countPass++;
                            }
                        }

                        break;

                    case HardwareTool_Form_Typle._TOOL_MACHINE:
                        M_Hardware_TOOL_Level_Two_C _tool_level_two_c = (M_Hardware_TOOL_Level_Two_C)_tool_group;
                        countPass += _tool_level_two_c.cellPassCount;

                        break;
                }
            }

            return countPass;
        }

        /// <summary>
        /// 计算出该组的所有自评及格的最小项的数量
        /// </summary>
        /// <returns>及格的数量</returns>
        int GetAllCellSelfPassCount()
        {
            int countPass = 0;
            foreach (IHardwareTool _tool_group in _lstItem)
            {
                switch (_tool_group._hardwareTool_Form_Typle)
                {
                    case HardwareTool_Form_Typle._TOOL_QUICK:
                        M_Hardware_TOOL_Level_Two_A _tool_level_two_a = (M_Hardware_TOOL_Level_Two_A)_tool_group;
                        foreach (MItem_FiveSAndSafe cell1 in _tool_level_two_a)
                        {
                            if (cell1.bIsSelfEvaluation)
                            {
                                countPass++;
                            }
                        }

                        break;

                    case HardwareTool_Form_Typle._TOOL_SHEET:
                        M_Hardware_TOOL_Level_Two_B _tool_level_two_b = (M_Hardware_TOOL_Level_Two_B)_tool_group;
                        foreach (MItem_Tool_A cell2 in _tool_level_two_b)
                        {
                            if (cell2.bIsSelfEvaluation)
                            {
                                countPass++;
                            }
                        }

                        break;

                    case HardwareTool_Form_Typle._TOOL_MACHINE:
                        M_Hardware_TOOL_Level_Two_C _tool_level_two_c = (M_Hardware_TOOL_Level_Two_C)_tool_group;
                        countPass += _tool_level_two_c.cellSelfPassCount;

                        break;
                }
            }

            return countPass;
        }

        /// <summary>
        /// 计算出该组的所有自评及格的最小项的数量
        /// </summary>
        /// <returns>及格的数量</returns>
        int GetAllCellLastPassCount()
        {
            int countPass = 0;
            foreach (IHardwareTool _tool_group in _lstItem)
            {
                switch (_tool_group._hardwareTool_Form_Typle)
                {
                    case HardwareTool_Form_Typle._TOOL_QUICK:
                        M_Hardware_TOOL_Level_Two_A _tool_level_two_a = (M_Hardware_TOOL_Level_Two_A)_tool_group;
                        foreach (MItem_FiveSAndSafe cell1 in _tool_level_two_a)
                        {
                            if (cell1.bIsLastTimePass)
                            {
                                countPass++;
                            }
                        }

                        break;

                    case HardwareTool_Form_Typle._TOOL_SHEET:
                        M_Hardware_TOOL_Level_Two_B _tool_level_two_b = (M_Hardware_TOOL_Level_Two_B)_tool_group;
                        foreach (MItem_Tool_A cell2 in _tool_level_two_b)
                        {
                            if (cell2.bIsLastTimePass)
                            {
                                countPass++;
                            }
                        }

                        break;

                    case HardwareTool_Form_Typle._TOOL_MACHINE:
                        M_Hardware_TOOL_Level_Two_C _tool_level_two_c = (M_Hardware_TOOL_Level_Two_C)_tool_group;
                        countPass += _tool_level_two_c.cellLastPassCount;

                        break;
                }
            }

            return countPass;
        }

        #endregion
    }
}
