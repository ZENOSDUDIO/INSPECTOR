using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Globals
{
    public class GlobalValue
    {
        /// <summary>
        /// <summary>
        /// 自动登录的时长间隔 用于检测连接状态
        /// </summary>
        public static int TIME_TICK_AUTO_LOGIN = 60000;

        //特约店自评相关
        public static int STORE_SELECTED_INDEX;
        public static bool NEED_REFRESH_STORE_TORE = false;

        public static Honda.Model.MStoreOfflineState Store_Need_Load_Offline_Data_State_Mgr =
            new Model.MStoreOfflineState();

        public const string _WORK_LIGHTS_SPOT = "1";
        public const string _WORK_LIGHTS_NEED = "2";
        public const string _WORK_LIGHTS_SUGGEST = "3";
        public const string _BUSINESS_POLICY_VALUATOR = "_BUSINESS_POLICY_VALUATOR";

        /// <summary>
        /// 显示第一个表格
        /// </summary>
        public const string FIRST_FORM_NAVIGATE = "FIRST_FORM_NAVIGATE";

        /// <summary>
        ///改善计划审核
        /// </summary>
        public const string IMPROVE_CHECK = "IMPROVE_CHECK";

        /// <summary>
        ///证据
        /// </summary>
        public const string IMPROVE_CHECK_ClOSE_EVIDECE = "CloseEvideceWindows";

        /// <summary>
        ///商务政策-零部件经理
        /// </summary>
        public const string _BUSINESS_POLICY_COMPONT_MANGER = "_COMPONT_MANGER";

        /// <summary>
        ///商务政策-总经理
        /// </summary>
        public const string _BUSINESS_POLICY_GENERAL_MANAGER = "_GENERAL_MANAGER";


        /// <summary>
        ///改善计划—评价员1
        /// </summary>
        public const string _IMPROVE_VALUATOR1 = "_VALUATOR1";

        /// <summary>
        ///改善计划—评价员2
        /// </summary>
        public const string _IMPROVE_VALUATOR2 = "_VALUATOR2";

        /// <summary>
        ///改善计划—零部件经理
        /// </summary>
        public const string _IMPROVE_COMPONT_MANGER = "_IMPROVE_COMPONT_MANGER";

        /// <summary>
        ///改善计划—服务经理
        /// </summary>
        public const string _IMPROVE_SERVE_MANGER = "_IMPROVE__SERVE_MANGER";

        /// <summary>
        ///改善计划—总经理
        /// </summary>
        public const string _IMPROVE_GENERAL_MANAGER = "_IMPROVE_GENERAL_MANAGER";

        /// <summary>
        /// 对小项评分，通过时需传入的字符串
        /// </summary>
        public const string PASS = "-1";

        /// <summary>
        /// 对小项评分，不通过通过时需传入的字符串
        /// </summary>
        public const string NO_PASS = "-2";

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

        //cmd
        public static readonly string COMMAND_LOGIN = "login";

        #endregion

        #region MainView

        // 对窗口的操作
        public const string COMMAND_MAIN_WINDOW = "mainWindow";

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public const string COMMAND_CLOSE_WINDOW = "closeWindow";

        /// <summary>
        /// 窗口最大化
        /// </summary>
        public const string COMMAND_MAX_WINDOW = "maxWindow";

        /// <summary>
        /// 窗口最小化
        /// </summary>
        public const string COMMAND_MIN_WINDOW = "minWindow";

        /// <summary>
        /// 窗口正常显示
        /// </summary>
        public const string COMMAND_NORMAL_WINDOW = "normalWindow";

        /// <summary>
        /// 拖拽窗口
        /// </summary>
        public const string COMMAND_DRAG_MOVE_WINDOW = "dragMoveWindow";

        #endregion

        /// <summary>
        /// 对MainPage主页窗口的操作
        /// </summary>
        public const string COMMAND_MAIN_PAGE = "mainPage";


        /// <summary>
        /// 二级导航（从一级导航到了巡回评价店列表窗口）
        /// </summary>
        public static readonly string COMMAND_STORE = "Store";

        #region  评价列表导航页主界面

        public static readonly string _NAVIGATE_TO = "NavigetToPage";

        /// <summary>
        /// 用于通知评价表中分数的变更
        /// </summary>
        public const string _UPDATE_TOTAL_SCORE = "_UPDATE_TOTAL_SCORE";

        #endregion

        #region 服务基础评价

        public const string _EVALUATE_LOAD_DATA = "loadData";

        #endregion

        #region 服务管理评价

        public const string _SERVICE_MANAGEMENT_LOAD_DATA = "managementLoadData";

        #endregion

        #region 零部件评价

        public const string _COMPONENT_LOAD_DATA = "componentLoadData";

        #endregion

        #region 建议加分项

        public const string _SUGGEST_PUSES_LOAD_DATA = "SuggestPlusesLoadData";

        #endregion

        #region 生成评价结果

        public const string _PREVIEW_LOAD_DATA = "PreviewLoadData";

        #endregion

        #region  各种表格的列的跨度比例 (未完待整理)

        //(未完待整理)
        /*
         * 1.巡回评价表（除了 建议加分项）的最小的一列表格的设计思路是，首先这表格首先分为两部分，第一部分需要动态获取数据，
         * 而第二部分不需要动态获取数据绘制，它包括：上次评价结果、特约店 、自评结果、合格（巡回评价）、不（巡回评价）、备注说明。
         * 
         * 
         * 2.
         */

        #endregion

        /// <summary>
        /// 表格宽度
        /// </summary>
        public static readonly double FORM_ITEM_HIGH = 75;

        /// <summary>
        /// 表格底部宽度
        /// </summary>
        public static readonly double FORM_BOTTOM_HIGH = 55;
    }


    /// <summary>
    /// 更新哪个表格的分数
    /// </summary>
    public enum ENUM_SCORE
    {
        BASE_SERVICES_FIVES, //5S & 安全 -- 服务基础评价
        BASE_SERVICES_HARDWARE, //硬件 -- 服务基础评价
        BASE_SERVICES_PEOPLE, //人员 -- 服务基础评价
        BASE_SERVICES_RECEPTION, //接待流程 -- 服务基础评价
        BASE_SERVICES_QUICK_SERVICE, //快修流程 -- 服务基础评价
        BASE_SERVICES_BP, //BP流程 -- 服务基础评价
        BASE_SERVICES_DATA_EXACT, //数据准确 -- 服务基础评价
        NONE
    }

    /// <summary>
    /// 最小单元行表格的种类
    /// </summary>
    public enum ItemStyle
    {
        /// <summary>
        /// 评分类型表格
        /// </summary>
        ITEM_STYLE_NORMAL,

        /// <summary>
        /// 预览类型表格
        /// </summary>
        ITEM_STYLE_PREVIEW
    }

    /// <summary>
    /// 表格中组的总类（主要用于生成列表中的）
    /// </summary>
    public enum ItemGroupTyple
    {
        /// <summary>
        /// 普通类型
        /// </summary>
        normalType = 0
    }


    /// <summary>
    /// 配置文件
    /// </summary>
    public enum CONFIG_SETTING
    {
        IMP_ServerHost,
        IMP_ServerPort,

        IMP_VIEW_STOREKPI_URL,


        IMP_IF_ReqCommitWorkLightspotList,
        IMP_IF_ReqExportFile,
        IMP_IF_ReqGetFormList,
        IMP_IF_ReqGetImproveCheckList,
        IMP_IF_ReqGetImproveList,
        IMP_IF_ReqGetStoreList,
        IMP_IF_ReqGetUnfinishedTask,

        IMP_IF_ReqGetWorkLightspotAndIdeaList,
        IMP_IF_ReqLogin,
        IMP_IF_ReqGetEvaluateMenu,
        IMP_IF_ReqReleaseTask,
        IMP_IF_ReqTestMulityForm,
        IMP_IF_ReqUploadBusinessPolicy,

        IMP_IF_ReqUploadFormScore,
        IMP_IF_ReqUploadImproves,
        IMP_IF_ReqUploadImprovesCheck,
        IMP_IF_ReqUploadSummary,
        IMP_IF_ReqUploadtWorkLightspotAndIdeaList,
        IMP_IF_ReqBIReportPath,
    }
}