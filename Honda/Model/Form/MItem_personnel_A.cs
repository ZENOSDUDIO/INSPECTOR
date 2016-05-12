using Honda.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 服务基础评价-->人员- 人员配置表格中的第一种类型
    /// </summary>
    [Serializable]
    public class MItem_personnel_A : MBaseItem, IPersonnelGroupConfiguration
    {
        #region Var


        private _Enum_Personel_Group_Configuration_Form enum_personel_group_configuration_form;
        /// <summary>
        /// 该model所属的类型
        /// </summary>
       public _Enum_Personel_Group_Configuration_Form _enum_personel_group_configuration_form
        {
           get
            {
                return enum_personel_group_configuration_form;
            }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public string _strNo { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string _strPost { set; get; }

        /// <summary>
        /// 评估标注和要求500台
        /// </summary>
        public string _strEvaluation_of_count_0 { get; set; }

        /// <summary>
        /// 评估标注和要求500-1000台
        /// </summary>
        public string _strEvaluation_of_count_1 { get; set; }

        /// <summary>
        /// 评估标注和要求1000-1500台
        /// </summary>
        public string _strEvaluation_of_count_2 { get; set; }

        /// <summary>
        /// 评估标注和要求1500-2000台
        /// </summary>
        public string _strEvaluation_of_count_3 { get; set; }

        /// <summary>
        /// 评估标注和要求2000-2500台
        /// </summary>
        public string _strEvaluation_of_count_4 { get; set; }

        /// <summary>
        /// 评估标注和要求2500-3000台
        /// </summary>
        public string _strEvaluation_of_count_5 { get; set; }

        /// <summary>
        /// 评估标注和要求3000-3500台
        /// </summary>
        public string _strEvaluation_of_count_6 { get; set; }

       

        /// <summary>
        /// 上一次评价分数
        /// </summary>
        public new double _cellLastScore { get; set; }

        /// <summary>
        /// 特约店自评结果分数
        /// </summary>
        public new double _cellSelfScore { get; set; }

        /// <summary>
        /// 巡回评价分数
        /// </summary>
        public new double _cellTourScore { get; set; }

        /// <summary>
        /// 是否做出评价
        /// </summary>
        public new bool isEvaluate { get; set; }
        #endregion

        public MItem_personnel_A()
        {
            enum_personel_group_configuration_form = _Enum_Personel_Group_Configuration_Form._machineDescribe;
            isEvaluate = true;
        }
       
    }

}
