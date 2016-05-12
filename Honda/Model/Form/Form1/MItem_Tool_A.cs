using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 服务基础评价-->硬件表中的类型钣喷工具中的类型
    /// </summary>
    [Serializable]
    public class MItem_Tool_A : MBaseItem
    {


        public MItem_Tool_A(string machineName, string countNumberScale1, string countNumberScale2,
            string countNumberScale3, string countNumberScale4, bool bIsLastTimePass, bool bIsSelfEvaluation, bool bIsEvaluationOfTour)
        {
            StrMachineName = machineName;
            strCountNumberScale1 = countNumberScale1;
            strCountNumberScale2 = countNumberScale2;
            strCountNumberScale3 = countNumberScale3;
            strCountNumberScale4 = countNumberScale4;
            this.bIsLastTimePass = bIsLastTimePass;
            this.bIsSelfEvaluation = bIsSelfEvaluation;
            this.bIsEvaluationOfTour = bIsEvaluationOfTour;
        }

       

        /// <summary>
        /// 工具名称
        /// </summary>
        public string StrMachineName { get; set; }

        /// <summary>
        /// 小于200台
        /// </summary>
        public string strCountNumberScale1 { get; set; }

        /// <summary>
        /// 200~400台
        /// </summary>
        public string strCountNumberScale2 { get; set; }

        /// <summary>
        /// 400~ 500台
        /// </summary>
        public string strCountNumberScale3 { get; set; }

        /// <summary>
        /// 大于 500台
        /// </summary>
        public string strCountNumberScale4 { get; set; }


    }


    




}
