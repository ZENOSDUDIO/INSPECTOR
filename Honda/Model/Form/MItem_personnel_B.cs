using Honda.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 服务基础评价-->人员- 人员配置里的第二种表格的数据源
    /// </summary>
    [Serializable]
    public class MItem_personnel_B : MBaseItem, IPersonnelGroupConfiguration
    {
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
        /// 序号
        /// </summary>
        public string _strNo { get; set; }


        /// <summary>
        /// 岗位
        /// </summary>
        public string _strPost { set; get; }

        /// <summary>
        /// 岗位描述
        /// </summary>
        public string _strDescribe { set; get; }

       

        /// <summary>
        /// 是否做出评价
        /// </summary>
        public new bool isEvaluate { get; set; }
        public MItem_personnel_B(string strNo, string strPost, string strDescribe, bool IsLastTimePass, bool IsSelfEvaluation, bool IsEvaluationOfTour)
        {
            _strNo = strNo;
            _strPost = strPost;
            _strDescribe = strDescribe;
            bIsLastTimePass = IsLastTimePass;
            bIsSelfEvaluation = IsSelfEvaluation;
            bIsEvaluationOfTour = IsEvaluationOfTour;
            isEvaluate = true;

            enum_personel_group_configuration_form = _Enum_Personel_Group_Configuration_Form._postDescribe;

        }


    }
}
