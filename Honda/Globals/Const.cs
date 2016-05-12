using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Globals
{
    public class Const
    {
        /// <summary>
        /// 服务基础评价
        /// </summary>
        public const string CODE_FIRST_SBE = "SBE";

        /// <summary>
        /// 服务管理评价
        /// </summary>
        public const string CODE_FIRST_SME = "SME";

        /// <summary>
        /// 零部件评价
        /// </summary>
        public const string CODE_FIRST_SPE = "SPE";

        /// <summary>
        ///建议加分项
        /// </summary>
        public const string CODE_FIRST_BIE = "BIE";

        #region 服务基础评价二级表单编码

        /// <summary>
        /// 5S及其安全二级代码
        /// </summary>
        public const string CODE_SS = "SS";

        /// <summary>
        /// 硬件二级代码
        /// </summary>
        public const string CODE_HW = "HW";

        /// <summary>
        /// 人员二级代码
        /// </summary>
        public const string CODE_PS = "PS";

        /// <summary>
        /// 接待流程二级代码
        /// </summary>
        public const string CODE_RP = "RP";

        /// <summary>
        /// 快修流程二级代码
        /// </summary>
        public const string CODE_FRP = "FRP";

        /// <summary>
        /// BP流程二级代码
        /// </summary>
        public const string CODE_BPP = "BPP";

        /// <summary>
        /// 数据准确性二级代码
        /// </summary>
        public const string CODE_PA = "PA";

        #endregion

        #region 服务管理评价二级表单编码

        /// <summary>
        /// 服务管理评价--满意度管理
        /// </summary>
        public const string CODE_SFM = "SFM";

        /// <summary>
        /// 服务管理评价--客户维系管理
        /// </summary>
        public const string CODE_CTMM = "CTMM";

        /// <summary>
        /// 服务管理评价--来场促进管理
        /// </summary>
        public const string CODE_TP = "TP";

        /// <summary>
        /// 服务管理评价--重点业务
        /// </summary>
        public const string CODE_FB = "FB";

        #endregion

        #region 零部件评价二级表单编码

        /// <summary>
        /// 零部件评价--基础业务
        /// </summary>
        public const string CODE_BB = "BB";

        /// <summary>
        /// 零部件评价--营销管理
        /// </summary>
        public const string CODE_MM = "MM";

        /// <summary>
        /// 零部件评价--库存管理
        /// </summary>
        public const string CODE_IM = "IM";

        /// <summary>
        /// 服务管理评价--仓库管理
        /// </summary>
        public const string CODE_WHM = "WHM";

        /// <summary>
        /// 零部件评价--人员管理
        /// </summary>
        public const string CODE_PM = "PM";

        #endregion

        #region 建议加分项二级表单

        /// <summary>
        /// 建议加分项--加分项
        /// </summary>
        public const string CODE_PLUSES = "PLUSES";

        /// <summary>
        /// 零部件评价--五星级仓库评价表
        /// </summary>
        public const string CODE_FSW = "FSW";

        #endregion
    }
}