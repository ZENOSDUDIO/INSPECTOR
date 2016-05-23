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
    /// 整个硬件表格的数据源
    /// </summary>
    [Serializable]
    public class M_Hardware_Source : MBaseSource
    {

        private List<IHardware> _lstHardware;
        public List<IHardware> LstHardware 
        { 
            get
            {
                if(_lstHardware == null)
                {
                    _lstHardware = new List<IHardware>();
                }
                
                return _lstHardware;
            }

            set
            {
                _lstHardware = value;
                
            }
        }


        /// <summary>
        /// 表格的满分分数
        /// </summary>
        public double _totalScore { get; set; }

        /// <summary>
        /// 页面巡回评价的总分数
        /// </summary>
        public double _pageTourScore 
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < LstHardware.Count; i++)
                {
                    sum += LstHardware[i]._level_One_TourScore;
                }
                return sum;
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
                for (int i = 0; i < LstHardware.Count; i++)
                {
                    sum += LstHardware[i]._level_One_SelfScore;
                }
                return sum;
            }
        }

        /// <summary>
        /// 页面巡回评价上次评价的总分数
        /// </summary>
        public new double _pageLastScore
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < LstHardware.Count; i++)
                {
                    sum += LstHardware[i]._level_One_LastScore;
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
                foreach (IHardware item in LstHardware)
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

        public M_Hardware_Source()
        {

        }

        /// <summary>
        /// 初始化该二级表单中的数据：包括1、每个小组的检查方法，2、评分标准等，3、每个小组的总分 
        /// </summary>
        public void InitGroupData()
        {
            for(int i =0; i < _lstHardware.Count ; i++)
            {
                switch(i)
                {
                    case 0:
                        M_Hardware_NO_TOOL_Group group0 = _lstHardware[i] as M_Hardware_NO_TOOL_Group;
                        if (group0 == null)
                        {
                            return ;
                        }

                        group0._GroupTotalScore =15 ;
                        group0._EvaluationCriterion = "标准要求全合格得15分，有1项不合格得10分，有2项不合格得5分，3项及以上不合格得0分";
                        group0._InspectionMethod = "现场照片和标准照片对比（快修引导台，预约管理看板，前台辅助工具，旧件展示架），逐项对照和确认。";
                        break;

                    case 1:
                        M_Hardware_NO_TOOL_Group group1 = _lstHardware[i] as M_Hardware_NO_TOOL_Group;
                        if (group1 == null)
                        {
                            return;
                        }

                        group1._GroupTotalScore = 20;
                        group1._EvaluationCriterion = "标准要求1项不合格得15分，有2项不合格得10分，3项及以上不合格得0分";
                        group1._InspectionMethod = "1、5张各区域现场照片和运营手册管理标准对比。\n2、试用局域网。\n3、检查饮品和茶点的采购发票\n";
                        break;

                    case 2:
                        M_Hardware_NO_TOOL_Group group2 = _lstHardware[i] as M_Hardware_NO_TOOL_Group;
                        if (group2 == null)
                        {
                            return;
                        }

                        group2._GroupTotalScore = 20;
                        group2._EvaluationCriterion = "以上检查方法对应项目1项未达标得10分,2项未达标整体项目为0分";
                        group2._InspectionMethod = "1、现场各功能区照片。是否与标准一致\n2、重点检查烤漆房过滤棉的干净程度和底棉、顶棉、进\n3、检查是否标识机修完检工位";
                        break;

                    case 3:
                        M_Hardware_NO_TOOL_Group group3 = _lstHardware[i] as M_Hardware_NO_TOOL_Group;
                        if (group3 == null)
                        {
                            return;
                        }

                        group3._GroupTotalScore = 10;
                        group3._EvaluationCriterion = "标准要求1项不合格得5分，全合格得10分";
                        group3._InspectionMethod = "1、售后经理办公室和客服经理办公室管理看板照片，是否与标准一致";
                        break;

                    case 4:
                        M_Hardware_TOOL_Group group4 = _lstHardware[i] as M_Hardware_TOOL_Group;
                        if (group4 == null)
                        {
                            return;
                        }

                        group4._GroupTotalScore = 45;
                        group4._EvaluationCriterion = "得分按比例换算到对应项目得分";
                        group4._InspectionMethod = "使用工具评分表评分";
                        break;
                }
            }
        }


    }
}
