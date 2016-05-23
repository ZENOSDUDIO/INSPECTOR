using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 服务基础评价-->人员- 人员能力评估的类型的数据源
    /// </summary>
    [Serializable]
    public class MItem_personnel_C : MBaseItem
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string _strNo { get; set; }

        /// <summary>
        /// 岗位描述
        /// </summary>
        public string _strDescribe { set; get; }


        /// <summary>
        /// 是否做出评价
        /// </summary>
        public new bool isEvaluate { get; set; }

        public MItem_personnel_C(string strNo,string strDescribe, double cellLastScore, double cellSelfScore, double cellTourScore)
        {
            _strNo = strNo;
            _strDescribe = strDescribe;
            _cellLastScore = cellLastScore;
            _cellSelfScore = cellSelfScore;
            _cellTourScore = cellTourScore;
        }


    }
}
