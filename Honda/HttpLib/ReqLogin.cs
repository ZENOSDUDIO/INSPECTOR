using Honda.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.HttpLib
{
    /*    
     * 登录接口  - by luyonghe 2014/11/4    
     */

    public class ReqLogin : HttpBase
    {
        private string _logId; //巡回员ID
        private string _pwd; //巡回员密码
        private string _jsonTxt;

        public MUser userInfo;

        public ReqLogin(string logId, string pwd, Action<object> callback)
            : base(RequestType.POST, callback)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;

            //打包地址
            m_strInterfaceUrl = Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqLogin);
                //"/imp/authentication/login";
            //m_strInterfaceUrl = "/impInterface/authentication/login";

            _logId = logId;
            _pwd = pwd;
            BuildParam();
        }

        public override void BuildParam()
        {
            try
            {
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("logId"); //巡回员ID
                m_jsonWriter.WriteValue(_logId);
                m_jsonWriter.WritePropertyName("password"); //巡回员密码
                m_jsonWriter.WriteValue(_pwd);
                m_jsonWriter.WritePropertyName("md5String"); //md5（logID + token + timestamp）
                m_jsonWriter.WriteValue(GetMD5Str(_logId));
                m_jsonWriter.WritePropertyName("timestamp"); //时间戳
                m_jsonWriter.WriteValue(_timestamp);
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

        public override void ParseParam()
        {
            string str = Encoding.UTF8.GetString(m_byteResponseData);

            try
            {
                JObject resultObject = JObject.Parse(str);
                string code = resultObject["code"].ToString();
                string msg = resultObject["message"].ToString();
                var result = resultObject["result"];

                if (code == "0")
                {
                    m_bIsSuccess = true;
                }
                else
                {
                    m_bIsSuccess = false;
                    m_strErrorMsg = msg;
                }

                if (m_bIsSuccess)
                {
                    userInfo = new MUser();
                    JArray dataList = JArray.Parse(result.ToString());
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        JObject jobj = JObject.Parse(dataList[i].ToString());
                        userInfo.UserName = jobj["name"].ToString();
                        userInfo.UserAccount = jobj["logId"].ToString();
                    }
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