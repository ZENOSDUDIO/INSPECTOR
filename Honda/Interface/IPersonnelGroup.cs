using Honda.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Interface
{
    /// <summary>
    /// 服务基础评价- 人员 表格分为三大组的类型，
    /// </summary>
    [Serializable]
    public enum _Enum_Personel_Group_Form
    {
        /// <summary>
        /// 人员配置类型
        /// </summary>
        _configuration = 0,

        /// <summary>
        /// 人员训练类型
        /// </summary>
        _train = 1,

        /// <summary>
        /// 人员能力评估类型
        /// </summary>
        _evaluation = 2
    }

    public interface IPersonnelGroup : IGroup
    {
        _Enum_Personel_Group_Form _enum_personel_group_form { get; }

        /// <summary>
        /// 是否所有的项都评价了
        /// </summary>
        bool _isEvaluateOfGroups { get; }
    }
}