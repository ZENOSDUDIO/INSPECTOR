using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.HttpLib.JsonModelData
{
    /// <summary>
    /// 表单Item第5种模板 参考“建议加分项--加分项表单”的所有项
    /// </summary>
    public class JFormItemFifth : JFormItemBase
    {
        public JFormItemFifth()
        {
            ENUMItemTemplate = ENUM_FORM_ITEM_TEMPLATE.FIFTH;
        
        }
        /// <summary>
        /// 内容  如加分项表单--第四列
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 分值  如加分项表单--第二列    
        /// </summary>
        public string Score { get; set; }

    }
}
