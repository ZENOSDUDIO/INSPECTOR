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
    /// 硬件- 非工具类型的表格组
    /// </summary>
    [Serializable]
    public class M_Hardware_NO_TOOL_Group : MItemBaseGroup, IHardware
    {
        /// <summary>
        /// 非工具类型的表格组list
        /// </summary>
        private List<MItem_FiveSAndSafe> _lstItem;
        public List<MItem_FiveSAndSafe> LstItem
        {
            get
            {
                if (_lstItem == null)
                {
                    _lstItem = new List<MItem_FiveSAndSafe>();
                }

                return _lstItem;
            }
            set
            {
                _lstItem = value;
            }
        }

       

        /// <summary>
        /// 表格中 ，组的种类
        /// </summary>
        public ItemGroupTyple itemGroupType { get; set; }

        public Hardware_Form_Typle _Enum_hardware_Typle { get; set; }

        /// <summary>
        /// 具体评价内容
        /// </summary>
        public string _EvaluationGroupContent { get; set; }

        public string StrNo { get; set; }

        /// <summary>
        /// 上次评价分数
        /// </summary>
        public double _level_One_LastScore
        {
            get
            {
                return EvaluatingMethod(GetLastFailCount());
            }


            set
            {

            }
        }
       
        /// <summary>
        /// 特约店自评价分数
        /// </summary>
        public double _level_One_SelfScore
        {
            get
            {
                return EvaluatingMethod(GetSelfFailCount());
            }


            set
            {

            }
        }
       

        /// <summary>
        /// 巡回店评价分数
        /// </summary>
        public double _level_One_TourScore 
        {
            get
            {
                return EvaluatingMethod(_failCount);
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
                foreach (MItem_FiveSAndSafe item in LstItem)
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

        public M_Hardware_NO_TOOL_Group()
        {
            _Enum_hardware_Typle = Hardware_Form_Typle._NO_TOOL_STYLE;
            itemGroupType = ItemGroupTyple.HardwareNoToolType;


        }


        #region 评分

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
        /// 获取巡回店评价表该小组巡回评价的不合格数
        /// </summary>
        public override int GetFailCount()
        {
            int failCount = 0;
            for (int i = 0; i < LstItem.Count; i++)
            {
                if (!LstItem[i].bIsEvaluationOfTour)
                {
                    failCount += 1;
                }
            }
            return failCount;
        }

        /// <summary>
        /// 获取巡回店评价表该小组自评的不合格数
        /// </summary>
        public  int GetSelfFailCount()
        {
            int failCount = 0;
            for (int i = 0; i < LstItem.Count; i++)
            {
                if (!LstItem[i].bIsSelfEvaluation)
                {
                    failCount += 1;
                }
            }
            return failCount;
        }

        /// <summary>
        /// 获取巡回店评价表该小组上次评价的不合格数
        /// </summary>
        public int GetLastFailCount()
        {
            int failCount = 0;
            for (int i = 0; i < LstItem.Count; i++)
            {
                if (!LstItem[i].bIsLastTimePass)
                {
                    failCount += 1;
                }
            }
            return failCount;
        }

        /// <summary>
        /// 根据索引和该三级表格的不合格数，调用评分方法
        /// </summary>
        public double EvaluatingMethod(int _failCount)
        {

            double TourScore = 0;
            switch (_index)
            {
                case 0:

                    TourScore = EvaluatingScore1(_failCount);
                    break;

                case 1:
                    TourScore = EvaluatingScore2(_failCount);
                    break;

                case 2:
                    TourScore = EvaluatingScore3(_failCount);
                    break;

                case 3:
                    TourScore = EvaluatingScore4(_failCount);
                    break;
            }

            return TourScore;
        }

        /// <summary>
        /// 评分方法一
        /// </summary>
        double EvaluatingScore1(int _failCount_)
        {           
            double Score = 0;

            if (_failCount_ == 0)
            {
                Score = 15;
            }
            else if (_failCount_ == 1)
            {
                Score = 10;
            }
            else if (_failCount_ == 2)
            {
                Score = 5;
            }
            else if (_failCount_ >= 3)
            {
                Score = 0;
            }

            return Score;
        }

        /// <summary>
        /// 评分方法二
        /// </summary>
        double EvaluatingScore2(int _failCount_)
        {
            
            double Score = 0;

            if (_failCount_ == 0)
            {
                Score = 20;
            }
            else if (_failCount_ == 1)
            {
                Score = 15;
            }
            else if (_failCount_ == 2)
            {
                Score = 10;
            }
            else if (_failCount_ >= 3)
            {
                Score = 0;
            }
            return Score;
        }

        /// <summary>
        /// 评分方法三
        /// </summary>
        double EvaluatingScore3(int _failCount_)
        {
            
            double Score = 0;

            if (_failCount_ == 0)
            {
                Score = 20;
            }
            else if (_failCount_ == 1)
            {
                Score = 10;
            }
            else if (_failCount_ >= 2)
            {
                Score = 0;
            }

            return Score;
        }

        /// <summary>
        /// 评分方法四
        /// </summary>
        double EvaluatingScore4(int _failCount_)
        {
            
            double Score = 0;

            if ( _failCount_ == 0)
            {
                Score = 10;
            }
            else if ( _failCount_ == 1)
            {
                Score = 5;
            }
            else if (_failCount_ >= 2)
            {
                Score = 0;
            }

            return Score;
        }
        #endregion
       

    }
}
