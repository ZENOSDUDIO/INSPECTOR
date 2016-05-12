using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model
{
    [Serializable()]
    public class MUser
    {
        /// <summary>
        /// 是否自动登录
        /// </summary>
        public bool IsAutoLogin { get; set; }

        /// <summary>
        /// 是否保存账号
        /// </summary>
        public bool IsSaveAccount { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserAccount { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd { get; set; }

        public string UserName { get; set; }
    }

    [Serializable()]
    public class MBiReportInfo
    {
        public string BiReportURL { get; set; }
    }
}