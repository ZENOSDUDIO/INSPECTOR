using Honda.Model;
using Honda.ViewModel;
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
     * 发布任务接口  - by luyonghe 2014/11/4    
     */

    public class ReqExportFile : HttpBase
    {
        private MExportFileMark _exportFileMark;
        private string _jsonTxt;

        public ReqExportFile(Action<object> callback, MExportFileMark exportMark)
            : base(RequestType.POST, callback)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;
            m_strInterfaceUrl = Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqExportFile);
                //"/imp/tourEvaluation/exportFile";
            _exportFileMark = exportMark;
            BuildParam();
        }

        public override void BuildParam()
        {
            try
            {
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("logId"); //巡回员ID
                m_jsonWriter.WriteValue(_exportFileMark.accountName);
                m_jsonWriter.WritePropertyName("md5String"); //md5（logID + token + timestamp）
                m_jsonWriter.WriteValue(GetMD5Str(_exportFileMark.accountName));
                m_jsonWriter.WritePropertyName("timestamp"); //时间戳
                m_jsonWriter.WriteValue(_timestamp);
                m_jsonWriter.WritePropertyName("appriaseId");
                m_jsonWriter.WriteValue(_exportFileMark.appriaseId);
                m_jsonWriter.WritePropertyName("shopId");
                m_jsonWriter.WriteValue(_exportFileMark.shopId);

                m_jsonWriter.WritePropertyName("feedBackMark");
                m_jsonWriter.WriteValue(_exportFileMark.feedBackMark == true ? 1 : 0);

                m_jsonWriter.WritePropertyName("betterMark");
                m_jsonWriter.WriteValue(_exportFileMark.betterMark == true ? 1 : 0);


                m_jsonWriter.WritePropertyName("tourMark");
                m_jsonWriter.WriteValue(_exportFileMark.tourMark == true ? 1 : 0);

                m_jsonWriter.WritePropertyName("businessMark");
                m_jsonWriter.WriteValue(_exportFileMark.businessMark == true ? 1 : 0);

                m_jsonWriter.WritePropertyName("excelMark");
                m_jsonWriter.WriteValue(_exportFileMark.excelMark == true ? 1 : 0);

                m_jsonWriter.WritePropertyName("pdfMark");
                m_jsonWriter.WriteValue(_exportFileMark.pdfMark == true ? 1 : 0);

                m_jsonWriter.WriteEndObject();

                _jsonTxt = m_strBuilderJson.ToString();
                string tsss = _jsonTxt;
                JObject jobj = JObject.Parse(tsss);
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

        public string msg;
        public string excelUrl;
        public string pdfUrl;

        public override void ParseParam()
        {
            try
            {
                string str = Encoding.UTF8.GetString(m_byteResponseData);

                JObject resultObject = JObject.Parse(str);
                string code = resultObject["code"].ToString();


                if (code == SUCCESS_CODE)
                {
                    m_bIsSuccess = true;

                    this.msg = resultObject["message"].ToString();
                    this.excelUrl = resultObject["excelUrl"].ToString();
                    this.pdfUrl = resultObject["pdfUrl"].ToString();
                }
                else
                {
                    m_bIsSuccess = false;
                    m_strErrorMsg = msg;
                }
            }
            catch (System.Exception ex)
            {
                string UriMessage = "请求地址： " + m_strRequestUrl + "\r\n";
                string requestMsg = "请求参数：" + _jsonTxt + "\r\n";
                string errMsg = UriMessage + requestMsg + m_strErrorMsg + "\r\n" + ex.Message;
                Debug.WriteLine(errMsg);
            }
        }
    }
}