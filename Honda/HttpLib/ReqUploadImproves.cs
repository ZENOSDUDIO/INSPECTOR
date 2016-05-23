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
    public class ReqUploadImproves : HttpBase
    {
        /// <summary>
        /// 请求json 字符串
        /// </summary>
        private string _exJson;

        public ResponseObject m_response;
        public string _createId;
        private ObservableCollection<MImprove> _ItemsData;

        private List<FileDataForUpload> _Files = new List<FileDataForUpload>();

        /// <summary>
        /// 签名
        /// </summary>
        private MSignature[] _ListSignature;

        public ReqUploadImproves(Action<Object> act, ObservableCollection<MImprove> items, MSignature[] ListSignature)
            : base(RequestType.POST, act)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING_AND_FILE;
            //m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;a
            //m_strInterfaceUrl = "/MultipartFormCommit_01/UploadServlet";
            m_strInterfaceUrl = Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqUploadImproves);
                //"/imp/tourEvaluation/betterPlan";
            _ItemsData = items;
            _ListSignature = ListSignature;
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
            //m_jsonWriter.WriteValue("DHEN04");//需修改，填写真实的id
            m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.shopId);
            m_jsonWriter.WritePropertyName("appriaseId"); //特约店有任务时就会返回appriaseId这个ID就是评价表ID
            m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.appriaseId);
            //m_jsonWriter.WriteValue("66494afc84f44fe289d66b0444baa884");//需修改填写真实的id
            m_jsonWriter.WritePropertyName("pgrId"); //从列表里的任意一项里面拿
            m_jsonWriter.WriteValue(_ItemsData[0].pgrId);
            m_jsonWriter.WritePropertyName("items");
            m_jsonWriter.WriteStartArray();
            for (int i = 0; i < _ItemsData.Count; i++)
            {
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("id");
                m_jsonWriter.WriteValue(_ItemsData[i].id);
                m_jsonWriter.WritePropertyName("priority");
                m_jsonWriter.WriteValue(_ItemsData[i].SelectPriority.priorityId);
                m_jsonWriter.WritePropertyName("description");
                m_jsonWriter.WriteValue(_ItemsData[i].description);
                m_jsonWriter.WritePropertyName("finishTime");
                m_jsonWriter.WriteValue(_ItemsData[i].finishTime);
                m_jsonWriter.WritePropertyName("responsiblePerson");
                m_jsonWriter.WriteValue(_ItemsData[i].responsiblePerson);
                m_jsonWriter.WritePropertyName("isIgnore");
                m_jsonWriter.WriteValue(_ItemsData[i].isIgnore);
                m_jsonWriter.WriteEndObject();
            }
            m_jsonWriter.WriteEndArray();
            //--------------签名数据-----------------------------
            //m_jsonWriter.WritePropertyName("signatureGroup");
            //m_jsonWriter.WriteStartArray();

            //foreach (MSignature signature in _ListSignature)
            //{
            //    FileDataForUpload uplodFile = new FileDataForUpload();
            //    uplodFile.FilePath = signature.signatureFileName;
            //    uplodFile.OldName = Path.GetFileName(signature.signatureFileName);
            //    string name = DirectoryHelper.INSTANCE.GetFileNewName(uplodFile.FilePath);
            //    string newName = Path.GetFileName(name);
            //    uplodFile.FileName = newName;
            //    _Files.Add(uplodFile);

            //    m_jsonWriter.WriteStartObject();
            //    m_jsonWriter.WritePropertyName("signatureType"); //签名类型
            //    m_jsonWriter.WriteValue(signature.Sign2String());

            //    m_jsonWriter.WritePropertyName("signatureFileName"); //签名图片名字
            //    m_jsonWriter.WriteValue(uplodFile.FileName);

            //    m_jsonWriter.WriteEndObject();
            //}
            //m_jsonWriter.WriteEndArray();

            m_jsonWriter.WriteEndObject();
            _exJson = m_strBuilderJson.ToString();
            JObject jobj = JObject.Parse(_exJson);
            //_exJson = PROTOCAL_PARAM + _exJson;
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