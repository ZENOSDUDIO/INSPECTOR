using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Interface
{
    /// <summary>
    /// 服务基础评价- 人员 表格中的配置类型的这一组表格又分为两种基本的表格样式，
    /// </summary>
    [Serializable]
    public enum _Enum_Personel_Group_Configuration_Form
    {
        /// <summary>
        /// 描述机器台式类型
        /// </summary>
        _machineDescribe = 0,

        /// <summary>
        /// 岗位描述
        /// </summary>
        _postDescribe = 1,
        other = 2
    }


    public interface IPersonnelGroupConfiguration
    {
        _Enum_Personel_Group_Configuration_Form _enum_personel_group_configuration_form { get; }

        /// <summary>
        /// 上一次评价分数
        /// </summary>
        double _cellLastScore { get; set; }

        /// <summary>
        /// 特约店自评结果分数
        /// </summary>
        double _cellSelfScore { get; set; }

        /// <summary>
        /// 巡回评价分数
        /// </summary>
        double _cellTourScore { get; set; }

        /// <summary>
        /// 是否做出评价
        /// </summary>
        bool isEvaluate { get; set; }

        /// <summary>
        /// 巡回店评价是否通过
        /// </summary>
        bool bIsEvaluationOfTour { get; set; }

        /// <summary>
        /// 上次评价是否通过
        /// </summary>
        bool bIsLastTimePass { get; set; }

        /// <summary>
        /// 自评是否通过
        /// </summary>
        bool bIsSelfEvaluation { get; set; }
    }
}