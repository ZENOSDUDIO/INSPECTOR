using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Global
{
    public class GlobalValue
    {
        #region Login
        
        /// <summary>
        /// 用户名Key
        /// </summary>
        public static readonly string STR_KEY_OF_USERNAME = "USER_NAME";  
 
        /// <summary>
        /// 用户密码Key
        /// </summary>
        public static readonly string STR_KEY_OF_USERPWD = "USER_PWD";

        /// <summary>
        /// 是否自动登录Key
        /// </summary>
        public static readonly string BOOL_KEY_OF_IS_AUTO_LOGIN = "IS_AUTO_LOGIN";

        /// <summary>
        /// 是否保存账号Key
        /// </summary>
        public static readonly string BOOL_KEY_OF_IS_SAVE_ACCOUNT_MSG = "IS_SAVE_ACCOUNT";

        #endregion

        /// <summary>
        /// 系统缓存文件保存根目录
        /// </summary>
        private static string APP_DATA_BASE_PATH = @"C:\Users\Public\Documents";

        /// <summary>
        /// 系统缓存文件-客户基本信息
        /// </summary>
        public static string CUSTOMER_PATH = APP_DATA_BASE_PATH + @"\Honda\Customer\Customer.data";

        /// <summary>
        /// 每一行表格的种类
        /// </summary>
        public enum ItemStyle
        {
            /// <summary>
            /// 5s&安全表中的类型
            /// </summary>
            ITEM_STYLE_FIVES_AND_SAFE,
            /// <summary>
            /// 服务基础评价-->硬件表中的类型钣喷工具中的类型
            /// </summary>
            ITEM_STYLE_TOOL_A,
            /// <summary>
            /// 服务基础评价-->硬件表中的类型机修工具中的类型
            /// </summary>
            ITEM_STYLE_TOOL_B,
            /// <summary>
            /// 人员类型
            /// </summary>
            ITEM_STYLE_PERSONNEL,
            /// <summary>
            /// 建议加分项-->加分项
            /// </summary>
            ITEM_STYLE_SUGGEST_A,
            /// <summary>
            /// 建议加分项-->5星级仓库
            /// </summary>
            ITEM_STYLE_SUGGEST_B
        }
    }
}
