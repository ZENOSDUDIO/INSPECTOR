using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /// <summary>
    /// 备注说明
    /// </summary>
    [Serializable]
    public class MRemarks
    {
        /// <summary>
        /// 备注图片地址列表
        /// </summary>
        public List<string> ImagePathList { get; set; }

        public string content { get; set; }

        public MRemarks()
        {
            if (ImagePathList == null)
            {
                ImagePathList = new List<string>();
            }
        }
    }
}