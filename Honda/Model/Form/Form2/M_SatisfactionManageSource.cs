using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*服务管理评价 - 满意度管理
     */

    [Serializable]
    public class M_SatisfactionManageSource : M_Common_Source
    {
        public M_SatisfactionManageSource()
            : base("满意度管理")
        {
            _pageTotalScore = 70;
        }

         
        /// <summary>
        /// 初始化二级表单组的数据
        /// </summary>
        public override void InitFormData()
        {
            for(int i = 0 ;i < _listGroup.Count; i++)
            {
                M_Common_Groupcs group = _listGroup[i];
                switch(i)
                {
                    case 0:
                        group._GroupTotalScore = 20;
                        group._InspectionMethod = "1、客服系统-分析决策-录音，导出三个回访录音检查话术\n2、查看近一个月的客户意见处理单，随机抽查10份与客服系统-意见处理信息比对是否一致，确保在系统中进行闭环管理（整理责任分析，处理意见，用户意见，总经理意见）";
                        group._EvaluationCriterion = "以上检查方法对应项目1项不合格得10分.全合格得20分。";
                        break;

                    case 1:
                        group._GroupTotalScore = 30;
                        group._InspectionMethod = "1、检查近一个月内外返单据档案\n2、检查近一个月完工检查单，每单都有作业班组、质检做好标识并签字，一车一单\n3、客服系统—分析决策—VOC月报中导出质量投诉，用YY分析法做VOC分析";
                        group._EvaluationCriterion = "以上检查方法对应项目1项不合格得20分.2项不合格得10分，全合格得30分。";
                        break;

                    case 2:
                        group._GroupTotalScore = 20;
                        group._InspectionMethod = "1、按时交车率(查近3个月，车间报表-接车员绩效-按时交车率统计)\n2、按时完工率（查近3个月，车间报表-维修组绩效-按时完工率统计)\n3、等待作业时间(查近3个月，统计报表-工时效率图-等待作业时间)\n4、等待完检时间(查近3个月，统计报表-工时效率图-等待完检时间)";
                        group._EvaluationCriterion = "以上标准要求1项不合格得15分.2项不合格得10分，3项及以上不合格得0分";
                        break;

                   
                }
            }
            
        }


        /// <summary>
        /// 开始打分
        /// </summary>
        public override void DoEvaluate()
        {
            for (int i = 0; i < _listGroup.Count; i++)
            {
                M_Common_Groupcs group = _listGroup[i];
                double fullScore = group._GroupTotalScore;
                int failCount = group.GetFailCount();
                int failLastCount = group.GetFailLastCount();
                int failSelfCount = group.GetFailSelftCount();

                switch (i)
                {
                    case 0:
                       
                        group._level_One_TourScore = GetGroupScore(fullScore, 10, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 10, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 10, failLastCount);    
                        break;

                    case 1:
                       
                        group._level_One_TourScore = GetGroupScore1(fullScore, 20,10, failCount);
                        group._level_One_SelfScore = GetGroupScore1(fullScore, 20, 10, failSelfCount);
                        group._level_One_LastScore = GetGroupScore1(fullScore, 20, 10, failLastCount);

                        break;

                    case 2:

                        group._level_One_TourScore = GetGroupScore1(fullScore, 15, 10, failCount);
                        group._level_One_SelfScore = GetGroupScore1(fullScore, 15, 10, failSelfCount);
                        group._level_One_LastScore = GetGroupScore1(fullScore, 15, 10, failLastCount);
                        break; 
                }
            }
        }

    }
}
