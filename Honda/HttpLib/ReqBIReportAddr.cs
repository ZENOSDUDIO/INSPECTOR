using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honda.Model;
using Honda.HttpLib.JsonInputData;
using Honda.ViewModel;
using System.Collections.ObjectModel;
using Honda.Globals;
using System.IO;

namespace Honda.HttpLib
{
    /// <summary>
    ///获取嵌入BI页面地址
    /// <author></author>
    ///<date>2014/12/8</date>
    ///<summary>
    public class ReqBIReportAddr : HttpBase
    {
        /// <summary>
        /// 请求json 字符串
        /// </summary>
        private string _jsonTxt;

        public string Url;

        public ReqBIReportAddr(Action<Object> act)
            : base(RequestType.POST, act)
        {
            m_strInterfaceUrl = Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqBIReportPath);
                //"/imp/report/getReportPath";
            try
            {
                BuildParam();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }
        }

        /// <summary>
        /// 构建参数
        /// </summary>
        public override sealed void BuildParam()
        {
            m_jsonWriter.WriteStartObject();
            m_jsonWriter.WritePropertyName("logId"); //巡回员ID
            m_jsonWriter.WriteValue(DMUser.INSTANCE.CurrentMUser.UserAccount);
            m_jsonWriter.WritePropertyName("password");
            m_jsonWriter.WriteValue(DMUser.INSTANCE.CurrentMUser.UserPwd);
            m_jsonWriter.WriteEndObject();

            _jsonTxt = m_strBuilderJson.ToString();
            _jsonTxt = PROTOCAL_PARAM + _jsonTxt;
            m_byteRequestData = Encoding.UTF8.GetBytes(_jsonTxt);

            ConvertPostDataToBytesData();
        }

        /// <summary>
        /// 字符串转换成byte 数组
        /// </summary>
        protected override void ConvertPostDataToBytesData()
        {
        }

        /// <summary>
        /// 解析接口返回
        /// </summary>
        public override void ParseParam()
        {
            if (m_bIsExistException || !m_bIsSuccess)
            {
                m_bIsSuccess = false;
                return;
            }
            string str = Encoding.UTF8.GetString(m_byteResponseData);
            try
            {
                var resultObject = JObject.Parse(str);
                string code = resultObject["code"].ToString();
                string msg = resultObject["message"].ToString();

                if (code == "0")
                {
                    m_bIsSuccess = true;
                    Url = resultObject["url"].ToString();
                }
                else
                {
                    m_bIsSuccess = false;
                    m_strErrorMsg = msg;
                }
            }
            catch (System.Exception ex)
            {
                m_strErrorMsg = ex.Message;
                string errMsg = "请求参数：" + _jsonTxt + "\r\n";
                errMsg += "返回数据：" + str + "\r\n";
                Debug.WriteLine("ReqUploadImproves", "解析数据失败：" + errMsg + "\r\n" + ex.Message);
            }
        }
    }
}