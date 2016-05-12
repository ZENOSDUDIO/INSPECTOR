using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.HttpLib.JsonModelData
{

    public class JFormItemContentSixthItem
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        /// <summary>
        /// 小项
        /// </summary>
        public string SmallItem { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Classify { get; set; }
       
        public string Name { get; set; }

        public string Score { get; set; }

        public string ValueType { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 上一次评价结果
        /// </summary>
        public string LastResult { get; set; }
        /// <summary>
        /// 特约店自评结果
        /// </summary>
        public string CurrentResult { get; set; }
    }
    public class JFormItemContentSixth
    {
       
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string Title { get; set; }
        public List<JFormItemContentSixthItem> Detail { get; set; }
        public JFormItemContentSixth()
        {
            Detail = new List<JFormItemContentSixthItem>();
        }
    }
    /// <summary>
    ///  建议加分项---五星级仓库评价表
    /// </summary>
    class JFormItemSixth:JFormItemBase
    {
        public JFormItemSixth()
        {
            ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.SIXTH;
        }

        private List<JFormItemContentSixth> itemList = new List<JFormItemContentSixth>();

        public List<JFormItemContentSixth> ItemList
        {
            get { return itemList; }
            private set { itemList = value; }
        }
    }




    /// <summary>
    /// 第6号模板 类似人员表单的11---23项
    /// </summary>
    class JFormItemSix : JFormItemBase
    {
        public JFormItemSix()
        {
            ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.SIXTH;
        }
        /// <summary>
        /// 内容--xiang
        /// </summary>
        public string Content { get; set; }
    }


    /// <summary>
    /// 第7号模板，类似于人员中的1--10项
    /// </summary>
    class JFormItemSeven : JFormItemBase
    {
        public JFormItemSeven()
        {
            ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.SEVEN;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<string> Content { get; set; }
    }
}
