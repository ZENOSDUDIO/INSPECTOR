using Honda.Globals;
using Honda.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{

    /// <summary>
    /// 服务基础评价- 人员列表中的 能力评估的组成员List<MItem_personnel_C>
    /// </summary>
    [Serializable]
    public class M_Personnel_Evaluation_Group : MItemBaseGroup, IPersonnelGroup
    {

        private List<MItem_personnel_C> _listGroup;
        public List<MItem_personnel_C> ListGroup
        {
            get
            {
                if (_listGroup == null)
                {
                    _listGroup = new List<MItem_personnel_C>();
                }
                return _listGroup;
            }

            set
            {
                _listGroup = value;
            }
        }


      

        /// <summary>
        /// 表格中 ，组的种类
        /// </summary>
        public ItemGroupTyple itemGroupType { get; set; }


        private _Enum_Personel_Group_Form enum_personel_group_form;
        public _Enum_Personel_Group_Form _enum_personel_group_form
        {
            get
            {
                return enum_personel_group_form;
            }
        }

        /// <summary>
        /// 具体评价内容
        /// </summary>
        public string _EvaluationGroupContent { get; set; }

        /// <summary>
        /// 上次评价的分值
        /// </summary>
        public double _level_One_LastScore { get; set; }
        

        /// <summary>
        /// 特约店自评的分值
        /// </summary>
        public double _level_One_SelfScore { get; set; }
        
 

        /// <summary>
        /// 巡回评价的分值
        /// </summary>
        public double _level_One_TourScore 
        {
            get
            {
                double sum = 0;
                foreach(MItem_personnel_C cell in ListGroup)
                {
                    sum += cell._cellTourScore;
                }

                return sum;
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
                foreach (IPersonnelGroupConfiguration item in ListGroup)
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


        public M_Personnel_Evaluation_Group()
        {
            enum_personel_group_form = _Enum_Personel_Group_Form._evaluation;
            itemGroupType = ItemGroupTyple.personnelTypeC;
        }


    }
}
