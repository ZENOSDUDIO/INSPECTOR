using Honda.Globals;
using Honda.HttpLib.JsonInputData;
using Honda.Model;
using Honda.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.HttpLib
{
    /*    
     * 上传工作亮点与意见需求列表接口  - by luyonghe 2014/11/18  
     */

    public class ReqUploadtWorkLightspotAndIdeaList : HttpBase
    {
        private string _jsonTxt;

        private ObservableCollection<MWorkLightspot> _lstWorkLightspot;
        private string _strRemovedIDs;
        private List<FileDataForUpload> _Files = new List<FileDataForUpload>();

        /// <summary>
        /// 请求json 字符串
        /// </summary>
        private string _exJson;

        public ReqUploadtWorkLightspotAndIdeaList(ObservableCollection<MWorkLightspot> lstWorkLightspot,
            string removedIDs, Action<object> callback)
            : base(RequestType.POST, callback)
        {
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING_AND_FILE;
            //打包地址
            m_strInterfaceUrl =
                Honda.Globals.Tools.GetConfigValue(
                    Honda.Globals.CONFIG_SETTING.IMP_IF_ReqUploadtWorkLightspotAndIdeaList);
                //"/imp/tourEvaluation/addFeedBack";
            _lstWorkLightspot = lstWorkLightspot;
            _strRemovedIDs = removedIDs;

            BuildParam();
        }

        public override void BuildParam()
        {
            try
            {
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("logId"); //巡回员ID
                m_jsonWriter.WriteValue(DMUser.INSTANCE.CurrentMUser.UserAccount);
                m_jsonWriter.WritePropertyName("md5String"); //md5（logID + token + timestamp）
                m_jsonWriter.WriteValue(GetMD5Str(DMUser.INSTANCE.CurrentMUser.UserAccount));
                m_jsonWriter.WritePropertyName("timestamp"); //时间戳
                m_jsonWriter.WriteValue(_timestamp);

                m_jsonWriter.WritePropertyName("shopId");
                m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.shopId);
                m_jsonWriter.WritePropertyName("appriaseId");
                m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.appriaseId);
                m_jsonWriter.WritePropertyName("removedIds");
                m_jsonWriter.WriteValue(_strRemovedIDs);
                m_jsonWriter.WritePropertyName("data");
                m_jsonWriter.WriteStartArray();

                foreach (MWorkLightspot work in _lstWorkLightspot)
                {
                    m_jsonWriter.WriteStartObject();
                    m_jsonWriter.WritePropertyName("id");
                    m_jsonWriter.WriteValue(work.ID == null ? "" : work.ID);
                    m_jsonWriter.WritePropertyName("projectId");
                    m_jsonWriter.WriteValue(work.projectId);
                    m_jsonWriter.WritePropertyName("categoryId");
                    m_jsonWriter.WriteValue(work.categoryId);
                    m_jsonWriter.WritePropertyName("contentDes");
                    m_jsonWriter.WriteValue(work.ContentDes);

                    //需要上传的图片
                    m_jsonWriter.WritePropertyName("picture");
                    m_jsonWriter.WriteStartArray();
                    if (!string.IsNullOrWhiteSpace(work.ImaName0))
                    {
                        _Files.Add(GetUplodFile(work.ImaName0, work.ImaPath0));
                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("pictureName");
                        m_jsonWriter.WriteValue(work.ImaName0);
                        m_jsonWriter.WriteEndObject();
                    }

                    if (!string.IsNullOrWhiteSpace(work.ImaName1))
                    {
                        _Files.Add(GetUplodFile(work.ImaName1, work.ImaPath1));
                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("pictureName");
                        m_jsonWriter.WriteValue(work.ImaName1);
                        m_jsonWriter.WriteEndObject();
                    }

                    if (!string.IsNullOrWhiteSpace(work.ImaName2))
                    {
                        _Files.Add(GetUplodFile(work.ImaName2, work.ImaPath2));
                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("pictureName");
                        m_jsonWriter.WriteValue(work.ImaName2);
                        m_jsonWriter.WriteEndObject();
                    }
                    m_jsonWriter.WriteEndArray();


                    //需要上传的音频
                    m_jsonWriter.WritePropertyName("audio");
                    m_jsonWriter.WriteStartArray();
                    if (!string.IsNullOrWhiteSpace(work.AudioName))
                    {
                        _Files.Add(GetUplodFile(work.AudioName, work.audioPath));
                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("audioName");
                        m_jsonWriter.WriteValue(work.AudioName);
                        m_jsonWriter.WriteEndObject();
                    }
                    m_jsonWriter.WriteEndArray();


                    //需要上传的视频
                    m_jsonWriter.WritePropertyName("video");
                    m_jsonWriter.WriteStartArray();

                    if (!string.IsNullOrWhiteSpace(work.VideoName))
                    {
                        _Files.Add(GetUplodFile(work.VideoName, work.videoPath));
                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("videoName");
                        m_jsonWriter.WriteValue(work.VideoName);
                        m_jsonWriter.WriteEndObject();
                    }

                    m_jsonWriter.WriteEndArray();

                    //需要上传的文档
                    m_jsonWriter.WritePropertyName("textFile");
                    m_jsonWriter.WriteStartArray();
                    if (!string.IsNullOrWhiteSpace(work.DocName0))
                    {
                        _Files.Add(GetUplodFile(work.DocName0, work.docPath0));
                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("textFileName");
                        m_jsonWriter.WriteValue(work.DocName0);
                        m_jsonWriter.WriteEndObject();
                    }

                    if (!string.IsNullOrWhiteSpace(work.DocName1))
                    {
                        _Files.Add(GetUplodFile(work.DocName1, work.docPath1));
                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("textFileName");
                        m_jsonWriter.WriteValue(work.DocName1);
                        m_jsonWriter.WriteEndObject();
                    }

                    m_jsonWriter.WriteEndArray();

                    m_jsonWriter.WriteEndObject();
                }
                m_jsonWriter.WriteEndArray();
                m_jsonWriter.WriteEndObject();

                _jsonTxt = m_strBuilderJson.ToString();
                _exJson = _jsonTxt;
                _jsonTxt = PROTOCAL_PARAM + _jsonTxt;
                m_byteRequestData = Encoding.UTF8.GetBytes(_jsonTxt);
                ConvertPostDataToBytesData();
            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
                Debug.WriteLine(err);
            }
        }

        private FileDataForUpload GetUplodFile(string fileName, string filePath)
        {
            FileDataForUpload file = new FileDataForUpload();
            file.OldName = fileName;
            file.FilePath = filePath;
            file.FileName = fileName;
            //file.FileName = Path.GetFileName(DirectoryHelper.INSTANCE.GetFileNewName(filePath));
            return file;
        }

        protected override void ConvertPostDataToBytesData()
        {
            WriteJsonAndFileData(_exJson, _Files);
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

                if (code == SUCCESS_CODE || code == "0")
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