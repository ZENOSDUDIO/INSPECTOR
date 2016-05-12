using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.HttpLib.JsonModelData
{
   
    /// <summary>
    /// 用于模板二中的组的单元项
    /// </summary>
    public class JFormItemContentSecond
    {
        /// <summary>
        /// 如“硬件”-->"第16项"-->"第1项” 标题（快修工具）
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 如“硬件”-->"第16项"-->"第1项” 内容（2台/工位，含风炮扳手、胎压表各1个）
        /// </summary>
        public string Detail { get; set; }

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
    /// 表单Item第2种模板 参考“硬件”的第16项“快修工具”
    /// </summary>
    public class JFormItemSecond : JFormItemBase
    {
        public JFormItemSecond()
        {
            ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.SECOND;
        }

        private List<JFormItemContentSecond> itemList = new List<JFormItemContentSecond>();

        /// <summary>
        /// 该单元项所包含的拆分项<如“快修工具”所包含的7小项>
        /// 该小项包含两部分，一部分为标题，第二部分为详情
        /// </summary>
        public List<JFormItemContentSecond> ItemList
        {
            get { return itemList; }
            private set { itemList = value; }
        }

    }
}
