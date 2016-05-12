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
    /// 服务基础评价- 人员列表中的 人员培训类型的组成员
    /// </summary>
    [Serializable]
    public class M_Personnel_Train_Group : MItemBaseGroup, IPersonnelGroup
    {
        private List<MItem_FiveSAndSafe> _listGroup;
        public List<MItem_FiveSAndSafe> ListGroup
        {
            get
            {
                if (_listGroup == null)
                {
                    _listGroup = new List<MItem_FiveSAndSafe>();
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
        public double _level_One_TourScore { get; set; }
        
        /// <summary>
        /// 是否所有的项都评价了
        /// </summary>
        public bool _isEvaluateOfGroups { get; set; }
       

        public M_Personnel_Train_Group()
        {
            enum_personel_group_form = _Enum_Personel_Group_Form._train;
            itemGroupType = ItemGroupTyple.personnelTypeB;
        }


        /// <summary>
        /// 该组不及格的数量
        /// </summary>
        public new int _failCount
        {
            get
            {
                return GetFailCount();
            }

            set
            {

            }
        }

        /// <summary>
        /// 该组上次不及格的数量
        /// </summary>
        public  int _failLastCount
        {
            get
            {
                return GetFailLastCount();
            }

            set
            {

            }
        }

        /// <summary>
        /// 该组上次不及格的数量
        /// </summary>
        public int _failSelfCount
        {
            get
            {
                return GetFailSelfCount();
            }

            set
            {

            }
        }


        /// <summary>
        /// 获取巡回店评价表该小组的不合格数
        /// </summary>
        public override int GetFailCount()
        {
            int failCount = 0;
            for (int i = 0; i < ListGroup.Count; i++)
            {
                if (!ListGroup[i].bIsEvaluationOfTour)
                {
                    failCount += 1;
                }
            }
            return failCount;
        }

        /// <summary>
        /// 获取巡回店评价表该小组的上次不合格数
        /// </summary>
        public  int GetFailLastCount()
        {
            int failCount = 0;
            for (int i = 0; i < ListGroup.Count; i++)
            {
                if (!ListGroup[i].bIsLastTimePass)
                {
                    failCount += 1;
                }
            }
            return failCount;
        }

        /// <summary>
        /// 获取巡回店评价表该小组的自评不合格数
        /// </summary>
        public int GetFailSelfCount()
        {
            int failCount = 0;
            for (int i = 0; i < ListGroup.Count; i++)
            {
                if (!ListGroup[i].bIsSelfEvaluation)
                {
                    failCount += 1;
                }
            }
            return failCount;
        }

    }
}
