using Honda.Interface;
using Honda.Model.Form.baseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 人员整个列表的数据源MBaseSource
    /// </summary>
    [Serializable]
    public class M_Personnel_Source : MBaseSource
    {
        #region Var
        protected List<IPersonnelGroup> _lstGroup;
        public List<IPersonnelGroup> LstGroup
        {
            get
            {
                if (_lstGroup == null)
                {
                    _lstGroup = new List<IPersonnelGroup>();
                }
                DoEvaluate();
                return _lstGroup;
            }

            set
            {
                _lstGroup = value;
                InitFormData();
            }
        }

        /// <summary>
        /// 自评的总分值 
        /// </summary>
        public new double _pageSelfScore
        {
            get
            {
                double sum = 0;
                for(int i = 0; i <LstGroup.Count; i++)
                {
                    sum += LstGroup[i]._level_One_SelfScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 上次评价的总分值 
        /// </summary>
        public new double _pageLastScore
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < LstGroup.Count; i++)
                {
                    sum += LstGroup[i]._level_One_LastScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 巡回评价的总分值
        /// </summary>
        public double _pageTourScore
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < LstGroup.Count; i++)
                {
                    sum += LstGroup[i]._level_One_TourScore;
                }
                return sum;
            }
        }



        /// <summary>
        /// 数据源所有的项是否都评价了
        /// </summary>
        public bool _isEvaluateOfSoure
        {
            get
            {
                bool isEvaluate = true;
                foreach (IPersonnelGroup item in LstGroup)
                {
                    if (!item._isEvaluateOfGroups)
                    {
                        isEvaluate = false;
                        break;
                    }
                }
                return isEvaluate;
            }
        }
        #endregion
        

        public M_Personnel_Source()
        {            
        }


        /// <summary>
        /// 初始化二级表单中列表的数据
        /// </summary>
        public virtual void InitFormData()
        {

        }

        /// <summary>
        /// 执行打分
        /// </summary>
        public virtual void DoEvaluate()
        {

        }


       
    }
}
