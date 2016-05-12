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

namespace Honda.HttpLib
{
    /// <summary>
    /// 多表单上传测试
    /// <author>xiang</author>
    ///<date>2014/09/24</date>
    ///<summary>
    public class ReqTestMulityForm : HttpBase
    {
        /// <summary>
        /// 请求json 字符串
        /// </summary>
        private string _exJson;

        public ResponseObject m_response;
        public string _createId;
        private List<ItemDataForUpload> _ItemsData;
        private List<FileDataForUpload> _Files = new List<FileDataForUpload>();

        public ReqTestMulityForm(Action<Object> act, List<ItemDataForUpload> items)
            : base(RequestType.POST, act)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING_AND_FILE;
            //m_strInterfaceUrl = "/MultipartFormCommit_01/UploadServlet";
            m_strInterfaceUrl = Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqTestMulityForm);
                //"/imp/tourEvaluation/commitAppraiseAttr";
            _ItemsData = items;
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
            //需修改 填充真实数据--xiang
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
            m_jsonWriter.WritePropertyName("itemData");
            m_jsonWriter.WriteStartArray();
            for (int i = 0; i < _ItemsData.Count; i++)
            {
                if (_ItemsData[i].Files.Count <= 0)
                    continue;
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("id");
                m_jsonWriter.WriteValue(_ItemsData[i].ID);
                m_jsonWriter.WritePropertyName("grpId");
                m_jsonWriter.WriteValue(_ItemsData[i].GroupId);

                m_jsonWriter.WritePropertyName("fileInfo"); //二级表单数组
                m_jsonWriter.WriteStartArray();
                for (int n = 0; n < _ItemsData[i].Files.Count; n++)
                {
                    m_jsonWriter.WriteStartObject();
                    m_jsonWriter.WritePropertyName("fileName");
                    m_jsonWriter.WriteValue(_ItemsData[i].Files[n].FileName);
                    m_jsonWriter.WritePropertyName("oldFileName");
                    m_jsonWriter.WriteValue(_ItemsData[i].Files[n].OldName);
                    m_jsonWriter.WriteEndObject();
                    _Files.Add(_ItemsData[i].Files[n]);
                }
                m_jsonWriter.WriteEndArray();
                m_jsonWriter.WriteEndObject();
            }
            m_jsonWriter.WriteEndArray();
            m_jsonWriter.WriteEndObject();
            _exJson = m_strBuilderJson.ToString();
            JObject jobj = JObject.Parse(_exJson);
            ConvertPostDataToBytesData();
        }

        private void CreateTestData()
        {
            _ItemsData = new List<ItemDataForUpload>();
            ItemDataForUpload item;
            for (int i = 0; i < 10; i++)
            {
                item = new ItemDataForUpload();
                item.ID = new Random().Next(1, 100).ToString();
                item.GroupId = new Random().Next(1, 100).ToString();
                string name1 = "E:\\文本.txt";
                string name2 = "E:\\图片.png";
                string name3 = "E:\\文档.doc";
                FileDataForUpload file = new FileDataForUpload();
                file.OldName = "文本.txt";
                file.FilePath = name1;
                string newName = file.OldName + DateTime.Now.ToShortTimeString() + new Random().Next(3, 300).ToString() +
                                 ".txt";
                byte[] d = Encoding.UTF8.GetBytes(newName);
                file.FileName = Convert.ToBase64String(d);
                item.Files.Add(file);

                file = new FileDataForUpload();
                file.OldName = "图片.png";
                file.FilePath = name2;
                newName = file.OldName + DateTime.Now.ToShortTimeString() + new Random().Next(3, 300).ToString() +
                          ".png";
                d = Encoding.UTF8.GetBytes(newName);
                file.FileName = Convert.ToBase64String(d);
                item.Files.Add(file);

                file = new FileDataForUpload();
                file.OldName = "文档.doc";
                file.FilePath = name3;
                newName = file.OldName + DateTime.Now.ToShortTimeString() + new Random().Next(3, 300).ToString() +
                          ".doc";
                d = Encoding.UTF8.GetBytes(newName);
                file.FileName = Convert.ToBase64String(d);
                item.Files.Add(file);
                _ItemsData.Add(item);
            }
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
            m_response = new ResponseObject();
            string str = Encoding.UTF8.GetString(m_byteResponseData);
            return;
            try
            {
                var resultObject = JObject.Parse(str);
                var code = resultObject["code"];
                var msg = resultObject["msg"];
                var value = resultObject["value"];
                if (code != null)
                {
                    m_response.rcode = code.ToString();
                }
                if (msg != null)
                {
                    m_response.resultMsg = msg.ToString();
                }
                if (value != null)
                {
                    //var mcase = JsonConvert.DeserializeObject<MCase>(value.ToString());
                    //_createId = mcase.Id;
                }
                if (m_response.rcode == SUCCESS_CODE)
                {
                    m_bIsSuccess = true;
                }
                else
                {
                    m_bIsSuccess = false;
                    m_strErrorMsg = m_response.resultMsg;
                }
            }
            catch (System.Exception ex)
            {
                m_strErrorMsg = ex.Message;
                string errMsg = "请求参数：" + _exJson + "\r\n";
                errMsg += "返回数据：" + str + "\r\n";
                Debug.WriteLine("ReqAddOrUpdateCase", "解析数据失败：" + errMsg + "\r\n" + ex.Message);
            }
        }
    }
}