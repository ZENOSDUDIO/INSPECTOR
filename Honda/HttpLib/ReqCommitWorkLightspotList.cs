using Honda.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.HttpLib
{
    /*    
     * 提交工作亮点与意见需求列表接口  - by luyonghe 2014/11/18  
     */

    public class CommitWorkLightspotList : HttpBase
    {
        private string _loginId; //巡回员登录id
        private string _shopId; //店id
        private string _jsonTxt;


        public CommitWorkLightspotList(string tourId, string shopId,
            ObservableCollection<MWorkLightspot> listWorkLightspot, Action<object> callback)
            : base(RequestType.POST, callback)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;

            //打包地址
            m_strInterfaceUrl =
                Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqCommitWorkLightspotList);
                // "/imp/tourEvaluation/addFeedBack";
            //m_strInterfaceUrl = "/impInterface/tourEvaluation/addFeedBack";

            _loginId = tourId;
            _shopId = shopId;

            BuildParam();
        }

        public override void BuildParam() //需修改
        {
            try
            {
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("logId"); //巡回员ID
                m_jsonWriter.WriteValue(_loginId);
                m_jsonWriter.WritePropertyName("md5String"); //md5（logID + token + timestamp）
                m_jsonWriter.WriteValue(GetMD5Str(_loginId));
                m_jsonWriter.WritePropertyName("timestamp"); //时间戳
                m_jsonWriter.WriteValue(_timestamp);

                m_jsonWriter.WritePropertyName("data");
                m_jsonWriter.WriteStartArray();
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("shopId ");
                m_jsonWriter.WriteValue(_shopId);
                m_jsonWriter.WriteEndObject();
                m_jsonWriter.WriteEndArray();

                m_jsonWriter.WriteEndObject();

                _jsonTxt = m_strBuilderJson.ToString();
                _jsonTxt = PROTOCAL_PARAM + _jsonTxt;
                m_byteRequestData = Encoding.UTF8.GetBytes(_jsonTxt);
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                Debug.WriteLine(err);
            }
        }

        protected override void ConvertPostDataToBytesData()
        {
        }

        public override void ParseParam() //需修改
        {
            string str = Encoding.UTF8.GetString(m_byteResponseData);
            try
            {
                JObject resultObject = JObject.Parse(str);
                string code = resultObject["code"].ToString();
                string msg = resultObject["message"].ToString();
                var result = resultObject["result"];

                if (code == SUCCESS_CODE)
                {
                    m_bIsSuccess = true;
                }
                else
                {
                    m_bIsSuccess = false;
                    m_strErrorMsg = msg;
                }
            }
            catch (System.Exception ex)
            {
                m_bIsSuccess = false;
                string UriMessage = "请求地址： " + m_strRequestUrl + "\r\n";
                string requestMsg = "请求参数：" + _jsonTxt + "\r\n";
                string errMsg = UriMessage + requestMsg + m_strErrorMsg + "\r\n" + ex.Message;
                Debug.WriteLine(errMsg);
            }
        }
    }
}