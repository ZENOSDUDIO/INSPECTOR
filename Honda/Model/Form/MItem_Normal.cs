using Honda.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 服务基础评价-->5s&安全表中的类型
    /// </summary>
    [Serializable]
    public class MItemNormal : MBaseItem, IPersonnelGroupConfiguration
    {
        private _Enum_Personel_Group_Configuration_Form enum_personel_group_configuration_form =
            _Enum_Personel_Group_Configuration_Form.other;

        public _Enum_Personel_Group_Configuration_Form _enum_personel_group_configuration_form
        {
            get { return enum_personel_group_configuration_form; }
        }

        public MItemNormal(string Content1, string Content2, bool isLastTimePass, bool isSelfPass,
            bool bIsEvaluationPass)
        {
            StrContent1 = Content1;
            StrContent2 = Content2;
            bIsLastTimePass = isLastTimePass;
            bIsSelfEvaluation = isSelfPass;
            bIsEvaluationOfTour = bIsEvaluationPass;
        }


        public string StrContent1 { get; set; }

        public string StrContent2 { get; set; }
    }

    /// <summary>
    /// 5s及其安全
    /// </summary>
    [Serializable]
    public class MFiveSAndSafes : List<MItemNormal>
    {
        /// <summary>
        /// 组的ID 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 组的父级ID 
        /// </summary>
        public string ParentID { get; set; }


        public readonly String _code = "ss";

        /// <summary>
        /// 上一次评价小组分数
        /// </summary>
        public double _level_One_LastScore { get; set; }

        /// <summary>
        /// 特约店自评结果小组分数
        /// </summary>
        public double _level_One_SelfScore { get; set; }


        /// <summary>
        /// 巡回评价小组分数
        /// </summary>
        public double _level_One_TourScore { get; set; }


        /// <summary>
        /// 该小组的满分分数
        /// </summary>
        public double TotalScore { get; set; }

        /// <summary>
        /// 是否所有的项都评价了
        /// </summary>
        public bool _isEvaluateOfGroups
        {
            get
            {
                bool isEvaluate = true;
                foreach (MItemNormal item in this)
                {
                    if (!item.isEvaluate)
                    {
                        isEvaluate = false;
                        break;
                    }
                }
                return isEvaluate;
            }
        }

        public string GroupName { get; set; }
    }
}