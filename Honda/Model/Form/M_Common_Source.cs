using Honda.Model.Form.baseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 通用的一个表格数据源模型 
    /// </summary>
    [Serializable]
    public class M_Common_Source : MBaseSource
    {
        protected List<M_Common_Groupcs> _listGroup;
        public List<M_Common_Groupcs> ListGroup
        {
            get
            {
                if(_listGroup == null)
                {
                    _listGroup = new List<M_Common_Groupcs>();
                }
                DoEvaluate();
                return _listGroup;
            }

            set
            {
                _listGroup = value;
                InitFormData();
            }
        }




        /// <summary>
        /// 页面巡回评价自评的总分数
        /// </summary>
        public new double _pageSelfScore
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < ListGroup.Count; i++)
                {
                    sum += ListGroup[i]._level_One_SelfScore;
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
                for (int i = 0; i < ListGroup.Count; i++)
                {
                    sum += ListGroup[i]._level_One_LastScore;
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
                for (int i = 0; i < ListGroup.Count; i++)
                {
                    sum += ListGroup[i]._level_One_TourScore;
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
                foreach (M_Common_Groupcs item in ListGroup)
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

        public M_Common_Source(string projectName)
        {
            _projectName = projectName;
        }

       

        /// <summary>
        /// 初始化二级表单中列表的数据
        /// </summary>
        public virtual void InitFormData()
        {

        }

        /// <summary>
        /// 开始打分
        /// </summary>
        public virtual void DoEvaluate()
        {

        }
    }
}
