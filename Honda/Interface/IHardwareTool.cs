using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Interface
{
    /// <summary>
    /// 工具类型的表格分为：快修工具类型、钣喷工具类型、机修工具类型
    /// </summary>
    [Serializable]
    public enum HardwareTool_Form_Typle
    {
        _TOOL_QUICK = 0, //快修工具类型
        _TOOL_SHEET = 1, //钣喷工具类型
        _TOOL_MACHINE = 2 //机修工具类型
    }

    public interface IHardwareTool
    {
        /// <summary>
        /// 是否该组所有的项都评价了
        /// </summary>
        bool _isEvaluateOf_level_Two { get; }

        HardwareTool_Form_Typle _hardwareTool_Form_Typle { get; }

        /// <summary>
        /// 上次评价的分值
        /// </summary>
        double _level_Two_LastScore { get; }

        /// <summary>
        /// 特约店自评的分值
        /// </summary>
        double _level_Two_SelfScore { get; }

        /// <summary>
        /// 巡回评价的分值
        /// </summary>
        double _level_Two_TourScore { get; }
    }
}