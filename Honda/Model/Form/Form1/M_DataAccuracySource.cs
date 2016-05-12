using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*服务基础评价 - 数据准确性
     */
    [Serializable]
    public class M_DataAccuracySource : M_Common_Source
    {
        public M_DataAccuracySource()
            : base("数据准确性")
        {
            _pageTotalScore = 65;
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
                        group._GroupTotalScore = 15;
                        group._InspectionMethod = "1、索取特约店人员名册和现场OA导出的人员信息比对，确认OA系统人员信息的准确性\n2、现场登录OA操作查看是否所有培训通知已阅读\n3、登录OA系统 查看培训提交记录";
                        group._EvaluationCriterion = "发现人员信息不一致该项不得分，并要求特约店现场整改";
                        break;

                    case 1:
                        group._GroupTotalScore = 20;
                        group._InspectionMethod = "1、定期保养界面-反馈信息（包含他店保养、里程未到、车已转卖、路边店保养等字段）\n2、数据同步界面，检查客服人员操作方法是否正确";
                        group._EvaluationCriterion = "以上检查方法对应项目1项不合格得10分，全合格得20分";
                        break;

                    case 2:
                        group._GroupTotalScore = 30;
                        group._InspectionMethod = "1、利用DMS系统高级查询导出C类客户清单，查看相应信息是否完整。\n2、查看DMS系统—项目管理—成组作业实绩管理\n3、查看DMS系统—即时查询—车辆状态。确认非会计打印状态车辆是否是无用的单据。";
                        group._EvaluationCriterion = "以上检查方法对应项目1项不合格得20分，2项及以上不合格得0分";
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
                       
                        group._level_One_TourScore = GetGroupScore2(fullScore, failCount);
                        group._level_One_SelfScore = GetGroupScore2(fullScore, failSelfCount);
                        group._level_One_LastScore = GetGroupScore2(fullScore, failLastCount);   
                        break;

                    case 1:
                       
                        group._level_One_TourScore = GetGroupScore(fullScore, 10, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 10, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 10, failLastCount);
                        break;

                    case 2:
                        group._level_One_TourScore = GetGroupScore(fullScore, 20, failCount);
                        group._level_One_SelfScore = GetGroupScore(fullScore, 20, failSelfCount);
                        group._level_One_LastScore = GetGroupScore(fullScore, 20, failLastCount);
                        break;

                  
                }
            }
        }

    }
}
