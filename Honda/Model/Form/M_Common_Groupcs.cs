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
    /// 通用的一个小组列表数据模型 
    /// </summary>
    [Serializable]
    public class M_Common_Groupcs : MItemBaseGroup, IGroup
    {
        private List<MItemNormal> _lstItem;

        public List<MItemNormal> LstItem
        {
            get
            {
                if (_lstItem == null)
                {
                    _lstItem = new List<MItemNormal>();
                }


                return _lstItem;
            }
            set { _lstItem = value; }
        }


        /// <summary>
        /// 表格中 ，组的种类
        /// </summary>
        public ItemGroupTyple itemGroupType { get; set; }


        /// <summary>
        /// 具体评价内容
        /// </summary>
        public string _EvaluationGroupContent { get; set; }

        /// <summary>
        /// 是否所有的项都评价了
        /// </summary>
        public bool _isEvaluateOfGroups
        {
            get
            {
                bool isEvaluate = true;
                foreach (MItemNormal item in LstItem)
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

        public M_Common_Groupcs()
        {
            itemGroupType = ItemGroupTyple.normalType;
        }

        /// <summary>
        /// 获取评价不通过的小项的数量
        /// </summary>
        public override int GetFailCount()
        {
            int failCount = 0;
            foreach (MItemNormal cell in _lstItem)
            {
                if (!cell.bIsEvaluationOfTour)
                {
                    failCount++;
                }
            }

            return failCount;
        }

        /// <summary>
        /// 获取评价表上次评价不通过的小项的数量
        /// </summary>
        public int GetFailLastCount()
        {
            int failCount = 0;
            foreach (MItemNormal cell in _lstItem)
            {
                if (!cell.bIsLastTimePass)
                {
                    failCount++;
                }
            }

            return failCount;
        }

        /// <summary>
        /// 获取评价表自评不通过的小项的数量
        /// </summary>
        public int GetFailSelftCount()
        {
            int failCount = 0;
            foreach (MItemNormal cell in _lstItem)
            {
                if (!cell.bIsSelfEvaluation)
                {
                    failCount++;
                }
            }

            return failCount;
        }
    }
}