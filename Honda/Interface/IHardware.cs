using Honda.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Interface
{
    [Serializable]
    /// <summary>
    /// 把表格分为工具类型，和非工具类型
    /// </summary>
    public enum Hardware_Form_Typle
    {
        _NO_TOOL_STYLE = 0, //非工具类型
        _TOOL_STYLE = 1 //工具类型
    }

    public interface IHardware : IGroup
    {
        /// <summary>
        /// 是否该组所有的项都评价了
        /// </summary>
        bool _isEvaluateOfGroups { get; }


        /// <summary>
        /// 硬件页面中所属的表格类型
        /// </summary>
        Hardware_Form_Typle _Enum_hardware_Typle { get; set; }
    }
}