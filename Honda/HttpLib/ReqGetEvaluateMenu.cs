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
using System.Collections.ObjectModel;

namespace Honda.HttpLib
{
    /*    
     * 登录接口  - by luyonghe 2014/11/4    
     */

    public class ReqGetEvaluateMenu : HttpBase
    {
        private string _jsonTxt;

        public ObservableCollection<MEvaluateMenu> _lstEvulateMenu = new ObservableCollection<MEvaluateMenu>();

        public ReqGetEvaluateMenu(Action<object> callback)
            : base(RequestType.POST, callback)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;

            //打包地址
            m_strInterfaceUrl =
                Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqGetEvaluateMenu);
                //"/imp/tourEvaluation/getSelfEvaluationList";

            BuildParam();
        }

        public override void BuildParam()
        {
            try
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
                m_jsonWriter.WriteEndObject();
                m_jsonWriter.WriteEndArray();
                m_jsonWriter.WriteEndObject();
                _jsonTxt = m_strBuilderJson.ToString();
                //HttpUtility d;
                _jsonTxt = PROTOCAL_PARAM + _jsonTxt;
                m_byteRequestData = Encoding.UTF8.GetBytes(_jsonTxt);
                //ConvertPostDataToBytesData();
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
                    JArray dataList = JArray.Parse(result.ToString());

                    MEvaluateMenu evaluateMenu;
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        JObject jobj = JObject.Parse(dataList[i].ToString());
                        evaluateMenu = new MEvaluateMenu
                        {
                            EvaluateCode = jobj["code"].ToString(),
                            EvaluateName = jobj["name"].ToString(),
                            EvaluateHeadName = jobj["headname"].ToString(),
                            EvaluateHeadDesc = jobj["headdesc"].ToString()
                        };
                        _lstEvulateMenu.Add(evaluateMenu);
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