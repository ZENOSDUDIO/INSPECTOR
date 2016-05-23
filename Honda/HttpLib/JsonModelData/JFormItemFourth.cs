using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.HttpLib.JsonModelData
{

    public class JFormItemContentFourthDetail
    {
        /// <summary>
        /// 自己的id
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// 上一次评价结果
        /// </summary>
        public string LastResult { get; set; }
        /// <summary>
        /// 特约店自评结果
        /// </summary>
        public string CurrentResult { get; set; }
       
        public string Title { get; set; }
    }
     /// <summary>
    /// 用于模板四中的组的单元项
    /// </summary>
    public class JFormItemContentFourth
    {
        /// <summary>
        /// 如“硬件”-->"第18项"-->"第1项” 标题（工具设备）
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 如“硬件”-->"第18项"-->"第1项” 内容（一个数组）竖着排列
        /// </summary>
        public List<JFormItemContentFourthDetail> Detail { get; set; }

        /// <summary>
        /// 自己的id
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 上一次评价结果
        /// </summary>
        public string LastResult { get; set; }
        /// <summary>
        /// 特约店自评结果
        /// </summary>
        public string CurrentResult { get; set; }

        public JFormItemContentFourth()
        {
            Detail = new List<JFormItemContentFourthDetail>();
        }
    }
    /// <summary>
    /// 表单Item第4种模板 参考“硬件”的第18项“机修工具”
    /// </summary>
    public class JFormItemFourth : JFormItemBase
    {
        public JFormItemFourth()
        {
            ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.FOURTH;
        }

       

        private List<JFormItemContentFourth> itemList = new List<JFormItemContentFourth>();

        /// <summary>
        /// 该单元项所包含的拆分项<如“钣喷工具”所包含的7小项>
        /// 该小项包含两部分，一部分为标题，第二部分为详情(数组)
        /// </summary>
        public List<JFormItemContentFourth> ItemList
        {
            get { return itemList; }
            private set { itemList = value; }
        }
    }
    
}
