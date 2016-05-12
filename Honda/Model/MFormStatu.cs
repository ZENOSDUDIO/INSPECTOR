using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model
{
    [Serializable]
    public class MFormStatu
    {
        /// <summary>
        /// 0 表示已经提交不需要加载本地数据，1表示需要加载本地数据
        /// </summary>
        public string statu = "0";

        public bool isLoadData
        {
            get
            {
                if (statu == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}