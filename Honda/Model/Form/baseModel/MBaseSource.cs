using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form.baseModel
{
    [Serializable]
    public class MBaseSource
    {
        /// <summary>
        /// 表单的ID 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 表单的父级ID 
        /// </summary>
        public string ParentID { get; set; }


        /// <summary>
        /// 项目名称
        /// </summary>
        public string _projectName { get; set; }

        /// <summary>
        /// 评价的满分分值
        /// </summary>
        public double _pageTotalScore
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < ListGroup.Count; i++)
                {
                    sum += ListGroup[i].LstItem.Count();
                }
                return sum;
            }
        }

        protected List<M_Common_Groupcs> _listGroup;

        public List<M_Common_Groupcs> ListGroup
        {
            get
            {
                if (_listGroup == null)
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
        public double _pageSelfScore
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < ListGroup.Count; i++)
                {
                    sum += ListGroup[i].LstItem.Count(n => n.bIsSelfEvaluation == true);
                }
                return sum;
            }
        }

        /// <summary>
        /// 上次评价的总分值 
        /// </summary>
        public double _pageLastScore
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < ListGroup.Count; i++)
                {
                    sum += ListGroup[i].LstItem.Count(n => n.bIsLastTimePass == true);
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
                    sum += ListGroup[i].LstItem.Count(n => n.bIsEvaluationOfTour == true);
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
                foreach (MItemBaseGroup item in ListGroup)
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

        /// <summary>
        /// 一项不合格得failoneScore分，两项不合格得0分，全部合格得fullScore分
        /// </summary>
        /// <param name="fullScore">满分的分数</param>
        /// <param name="failoneScore">一项不合格所得的分数</param>
        /// <returns></returns>
        protected double GetGroupScore(double fullScore, double failoneScore, int failCount)
        {
            double score = 0;
            if (failCount == 0)
            {
                score = fullScore;
            }
            else if (failCount == 1)
            {
                score = failoneScore;
            }
            else if (failCount >= 2)
            {
                score = 0;
            }

            return score;
        }


        /// <summary>
        /// 一项不合格得failoneScore1分，两项不合格得分failoneScore2分，三项不合格得0分，全合格得fullScore分
        /// </summary>
        /// <param name="fullScore">满分的分数</param>
        /// <param name="failoneScore">一项不合格所得的分数</param>
        /// <returns></returns>
        protected double GetGroupScore1(double fullScore, double failoneScore1, double failoneScore2, int failCount)
        {
            double score = 0;
            if (failCount == 0)
            {
                score = fullScore;
            }
            else if (failCount == 1)
            {
                score = failoneScore1;
            }
            else if (failCount == 2)
            {
                score = failoneScore2;
            }
            else if (failCount >= 3)
            {
                score = 0;
            }

            return score;
        }

        /// <summary>
        /// 一项不合格得0分，全部合格得fullScore分
        /// </summary>
        /// <param name="fullScore">满分的分数</param>        
        /// <returns></returns>
        protected double GetGroupScore2(double fullScore, int failCount)
        {
            double score = 0;
            if (failCount == 0)
            {
                score = fullScore;
            }
            else if (failCount >= 1)
            {
                score = 0;
            }


            return score;
        }
    }
}