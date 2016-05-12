using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honda.Model;
using Honda.ViewModel;

namespace Honda.HttpLib
{
    /**
     * 根据二级代码获取各个评价表
     * 二级代码：
     * 5S & 安全：SS       硬件：HW           人员：PS           接待流程：RP   
     * 快修流程：FRP       BP流程：BPP        数据准确性:PA      满意度管理：SFM    
     * 客户维系管理：CTMM  来厂促进管理：TP    重点业务:FB        基础业务：BB  
     * 营销管理：MM        库存管理：IM       仓库管理：WHM       人员管理：PM
     * 加分项：PLUSES      五星级仓库评价表：FSW
     */

    /// <summary>
    /// 获取评价表接口
    /// <author>xiang</author>
    ///<date>2014/09/24</date>
    ///<summary>
    public class ReqGetFormList : HttpBase
    {
        private string _jsonTxt;
        private string _code;

        /// <summary>
        /// 服务端返回的结果 json格式
        /// </summary>
        public string ContentOfJsonResult { get; set; }

        public ReqGetFormList(string code, Action<Object> act)
            : base(RequestType.POST, act)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;

            //打包版本
            m_strInterfaceUrl = Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqGetFormList);
                //"/imp/tourEvaluation/querySelfEvaluation";
            //调试版本
            //m_strInterfaceUrl = "/impInterface/tourEvaluation/querySelfEvaluation";

            _code = code;
            BuildParam();
        }

        /// <summary>
        /// 构建参数
        /// </summary>
        public override void BuildParam()
        {
            /*
             * {
                    "timestamp": "",//时间戳
                    "md5String": "",//md5（logId + token + timestamp）
                    "logId": "",  //巡回员登录ID
                    "data": [
                        {
                            "taskId": "12345",//任务ID
                            "shopId": "123",//特约店ID
                            "code": "SS"//评价项表单二级代码
                        }
                    ]
                }
             */

            m_jsonWriter.WriteStartObject();
            m_jsonWriter.WritePropertyName("logId"); //巡回员ID DMUser
            // m_jsonWriter.WriteValue("");
            m_jsonWriter.WriteValue(DMUser.INSTANCE.CurrentMUser.UserAccount);
            m_jsonWriter.WritePropertyName("md5String"); //md5（logID + token + timestamp）
            m_jsonWriter.WriteValue(GetMD5Str(DMUser.INSTANCE.CurrentMUser.UserAccount));
            m_jsonWriter.WritePropertyName("timestamp"); //时间戳
            m_jsonWriter.WriteValue(_timestamp);
            m_jsonWriter.WritePropertyName("shopId"); //巡回店id
            //m_jsonWriter.WriteValue("shop01");
            m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.shopId);
            m_jsonWriter.WritePropertyName("data");
            m_jsonWriter.WriteStartArray();
            m_jsonWriter.WriteStartObject();
            m_jsonWriter.WritePropertyName("appriaseId"); //评价表ID
            m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.appriaseId); //需要修改 还原为真实数据
            m_jsonWriter.WritePropertyName("shopId"); //特约店ID
            //m_jsonWriter.WriteValue("123");//需要修改 还原为真实数据
            m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.shopId);
            m_jsonWriter.WritePropertyName("code"); //二级代码
            m_jsonWriter.WriteValue(_code);
            m_jsonWriter.WriteEndObject();
            m_jsonWriter.WriteEndArray();
            m_jsonWriter.WriteEndObject();
            _jsonTxt = m_strBuilderJson.ToString();
            //HttpUtility d;
            _jsonTxt = PROTOCAL_PARAM + _jsonTxt;
            m_byteRequestData = Encoding.UTF8.GetBytes(_jsonTxt);
            //ConvertPostDataToBytesData();
        }

        /// <summary>
        /// 字符串转换成byte 数组
        /// </summary>
        protected override void ConvertPostDataToBytesData()
        {
            //WriteJsonAndFileData(_exJson, m_fileData);
        }

        /// <summary>
        /// 解析接口返回
        /// </summary>
        public override void ParseParam()
        {
            string str = Encoding.UTF8.GetString(m_byteResponseData);
            ContentOfJsonResult = Encoding.UTF8.GetString(m_byteResponseData);

            try
            {
                var resultObject = JObject.Parse(str);
                var code = resultObject["code"];
                var msg = resultObject["msg"];
                var value = resultObject["value"];
            }
            catch (System.Exception ex)
            {
                //string errMsg = "请求参数：" + _caseJson + "\r\n";
                //errMsg += "返回数据：" + str + "\r\n";
                //Log.PrintErrorLog("ReqAddOrUpdateCase", "解析数据失败：" + errMsg+"\r\n" + ex.Message);
            }
        }
    }
}