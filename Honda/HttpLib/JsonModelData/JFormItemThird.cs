using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.HttpLib.JsonModelData
{
    /// <summary>
    /// 用于模板三中的组的单元项
    /// </summary>
    public class JFormItemContentThird
    {
        /// <summary>
        /// 如“硬件”-->"第17项"-->"第1项” 标题（移动式干磨系统）
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 如“硬件”-->"第17项"-->"第1项” 内容（一个数组 只有4组值如：2,2,3,5）横着排列
        /// </summary>
        public List<string> Detail { get; set; }

        /// <summary>
        /// 自己的id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentID { get; set; }

        public JFormItemContentThird()
        {
            Detail = new List<string>();
        }

        /// <summary>
        /// 上一次评价结果
        /// </summary>
        public string LastResult { get; set; }

        /// <summary>
        /// 特约店自评结果
        /// </summary>
        public string CurrentResult { get; set; }

        /// <summary>
        /// 上次评价是否通过(合格) by luyonghe 10/27 
        /// </summary>
        public bool bIsLastTimePass { get; set; }

        /// <summary>
        /// 自评是否通过(合格) by luyonghe 10/27 
        /// </summary>
        public bool bIsSelfEvaluation { get; set; }
    }

    /// <summary>
    /// 表单Item第3种模板
    /// 服务基础评价-->硬件-->钣喷工具
    /// 服务基础评价-->人员-->人员配置1--10项
    /// 零部件评价-->人员管理-->人员配置1--3项
    /// </summary>
    public class JFormItemThird : JFormItemBase
    {
        public JFormItemThird()
        {
            ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.THIRD;
        }

        private List<JFormItemContentThird> itemList = new List<JFormItemContentThird>();

        /// <summary>
        /// 该单元项所包含的拆分项<如“钣喷工具”所包含的7小项>
        /// 该小项包含两部分，一部分为标题，第二部分为详情(数组)
        /// </summary>
        public List<JFormItemContentThird> ItemList
        {
            get { return itemList; }
            private set { itemList = value; }
        }
    }
}