using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*服务管理评价 - 重点业务管理
     */

    [Serializable]
    public class M_EmphasisBusinessSource : M_Common_Source
    {
        public M_EmphasisBusinessSource()
            : base("重点业务管理")
        {
            _pageTotalScore = 80;
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
                        group._GroupTotalScore = 30;
                        group._InspectionMethod = "1、DMS系统—促进—续保促进—日期，选择一年内有效保险信息和ABC客户数量比值\n2、DMS系统—公共信息代码—设定保险公司信息\n3、查看总结会议纪要和照片和战败客户报表及挽回对策。";
                        group._EvaluationCriterion = "以上检查方法对应项目1项不合格得20分.2项不合格得10分，3项不合格得0分。";
                        break;

                    case 1:
                        group._GroupTotalScore = 50;
                        group._InspectionMethod = "1、检查信息存档文件和宣贯签到表；\n2、检查近一个月的保修例会既要\n3、检查零部件仓库保修零部件的专用货架\n4、检查保修件存放是否整齐，抽查保修件是否有信息标识并且和车辆、故障对应\n5、检查近一个月存档的PDI档案，随机电话抽查3个客户是否收到PDI客户联";
                        group._EvaluationCriterion = "以上检查方法对应项目1项不合格得40分.2项不合格得30分，3项及以上不合格得0分。";
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
                       
                        group._level_One_TourScore = GetGroupScore1(fullScore, 20, 10, failCount);
                        group._level_One_SelfScore = GetGroupScore1(fullScore, 20, 10, failSelfCount);
                        group._level_One_LastScore = GetGroupScore1(fullScore, 20, 10, failLastCount);    
                        break;

                    case 1:
                       
                        group._level_One_TourScore = GetGroupScore1(fullScore, 40, 30, failCount);
                        group._level_One_SelfScore = GetGroupScore1(fullScore, 40, 30, failSelfCount);
                        group._level_One_LastScore = GetGroupScore1(fullScore, 40, 30, failLastCount);

                        break;
                }
            }
        }

    }
}
