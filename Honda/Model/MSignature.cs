using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model
{
    /// <summary>
    /// 签名类型
    /// </summary>
    public enum SIGNATURE_TYPE
    {
        /// <summary>
        /// 评价人员
        /// </summary>
        valuator = 1,

        /// <summary>
        /// 零部件经理
        /// </summary>
        componentManager = 2,

        /// <summary>
        /// 服务经理
        /// </summary>
        servesManager = 3,

        /// <summary>
        /// 总经理
        /// </summary>
        generalManager = 4
    }

    [Serializable]
    public class MSignature
    {
        /// <summary>
        /// 签名图片名称
        /// </summary>
        public string signatureFileName { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public SIGNATURE_TYPE _signature_type { get; set; }

        /// <summary>
        /// 返回签名的字符串 xiang
        /// </summary>
        /// <returns></returns>
        public string Sign2String()
        {
            string ret = "1";
            switch (_signature_type)
            {
                case SIGNATURE_TYPE.valuator:
                    ret = "1";
                    break;

                case SIGNATURE_TYPE.componentManager:
                    ret = "2";
                    break;

                case SIGNATURE_TYPE.servesManager:
                    ret = "3";
                    break;

                case SIGNATURE_TYPE.generalManager:
                    ret = "4";
                    break;
            }
            return ret;
        }
    }
}