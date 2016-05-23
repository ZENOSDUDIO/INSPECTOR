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
    /// 上传改善计划
    /// <author>xiang</author>
    ///<date>2014/12/8</date>
    ///<summary>
    public class ReqUploadSummary : HttpBase
    {
        /// <summary>
        /// 请求json 字符串
        /// </summary>
        private string _exJson;

        public ResponseObject m_response;
        public string _createId;
        private ObservableCollection<MSummary> _listSummary;

        private List<FileDataForUpload> _Files = new List<FileDataForUpload>();

        public ReqUploadSummary(ObservableCollection<MSummary> listSummary, Action<Object> act)
            : base(RequestType.POST, act)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING_AND_FILE;
            m_strInterfaceUrl = Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqUploadSummary);
                //"/imp/tourEvaluation/uploadTourReport";
            _listSummary = listSummary;
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
        public override void BuildParam()
        {
            m_jsonWriter.WriteStartObject();
            m_jsonWriter.WritePropertyName("logId"); //巡回员ID
            m_jsonWriter.WriteValue(DMUser.INSTANCE.CurrentMUser.UserAccount); //需要修改
            m_jsonWriter.WritePropertyName("md5String"); //md5（logID + token + timestamp）
            m_jsonWriter.WriteValue(GetMD5Str(DMUser.INSTANCE.CurrentMUser.UserAccount));
            m_jsonWriter.WritePropertyName("timestamp"); //时间戳
            m_jsonWriter.WriteValue(_timestamp);

            m_jsonWriter.WritePropertyName("shopId"); //商店Id
            m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.shopId);
            m_jsonWriter.WritePropertyName("appriaseId"); //特约店有任务时就会返回appriaseId这个ID就是评价表ID
            m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.appriaseId);

            //--------------总结报表文件PPT-----------------------------
            m_jsonWriter.WritePropertyName("summaryfiles");
            m_jsonWriter.WriteStartArray();
            foreach (var summ in _listSummary)
            {
                FileDataForUpload uplodFile = new FileDataForUpload();
                uplodFile.FilePath = summ.docPath;
                uplodFile.OldName = summ.oldName;
                uplodFile.FileName = summ.DocName;

                _Files.Add(uplodFile);

                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("filename"); //巡回总结报表名称
                m_jsonWriter.WriteValue(uplodFile.FileName);
                m_jsonWriter.WriteEndObject();
            }

            m_jsonWriter.WriteEndArray();
            m_jsonWriter.WriteEndObject();
            _exJson = m_strBuilderJson.ToString();
            JObject jobj = JObject.Parse(_exJson);

            m_byteRequestData = Encoding.UTF8.GetBytes(_exJson);
            ConvertPostDataToBytesData();
        }

        /// <summary>
        /// 字符串转换成byte 数组
        /// </summary>
        protected override void ConvertPostDataToBytesData()
        {
            WriteJsonAndFileData(_exJson, _Files);
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
                string errMsg = "请求参数：" + _exJson + "\r\n";
                errMsg += "返回数据：" + str + "\r\n";
                Debug.WriteLine("ReqUploadImproves", "解析数据失败：" + errMsg + "\r\n" + ex.Message);
            }
        }
    }
}