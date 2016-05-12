using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honda.HttpLib.JsonInputData;
using Honda.Model;
using Honda.ViewModel;
using System.IO;
using Honda.Globals;
using System.Windows;

namespace Honda.HttpLib
{
    public class ReqUploadFormScore : HttpBase
    {
        /// <summary>
        /// 是否上传附件图片
        /// </summary>
        private bool _isUploadFile;

        private string _jsonTxt;

        /// <summary>
        /// 请求json 字符串
        /// </summary>
        private string _exJson;

        /// <summary>
        /// 入参，评价表的组得分和小项得分
        /// </summary>
        private EvaluationForUpload _eva;

        /// <summary>
        /// 签名
        /// </summary>
        private MSignature[] _ListSignature;

        private List<FileDataForUpload> _Files = new List<FileDataForUpload>();

        public ReqUploadFormScore(EvaluationForUpload eva, MSignature[] ListSignature, Action<object> callback,
            bool isUploadFile = false)
            : base(RequestType.POST, callback)
        {
            _isUploadFile = isUploadFile;
            _eva = eva;
            _ListSignature = ListSignature;
            List<FileDataForUpload> _Files = new List<FileDataForUpload>();
            //m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING_AND_FILE;
            m_strInterfaceUrl =
                Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqUploadFormScore);
                //"/imp/tourEvaluation/commitTourEvaluation";
            BuildParam();
        }

        public override void BuildParam()
        {
            //CreateTestData();
            //需要修改 将以下硬编码数据改为真实的数据
            try
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
                m_jsonWriter.WritePropertyName("appriaseId"); //评价表Id
                m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.appriaseId);

                //开始写“组得分”数据 为数组
                m_jsonWriter.WritePropertyName("grpData"); //二级表单数组
                m_jsonWriter.WriteStartArray();
                GroupDataForUpload grp;
                for (int i = 0; i < _eva.Groups.Count; i++)
                {
                    grp = _eva.Groups[i];
                    m_jsonWriter.WriteStartObject();
                    m_jsonWriter.WritePropertyName("Id"); //评价表数据编号
                    m_jsonWriter.WriteValue(grp.ID);
                    m_jsonWriter.WritePropertyName("Score"); //巡回员打出小项总分
                    m_jsonWriter.WriteValue(grp.Score);
                    m_jsonWriter.WriteEndObject();
                }
                m_jsonWriter.WriteEndArray();

                //开始写“小项得分”数据 为数组
                m_jsonWriter.WritePropertyName("itemData"); //二级表单数组
                m_jsonWriter.WriteStartArray();
                ItemDataForUpload item;
                for (int i = 0; i < _eva.Items.Count; i++)
                {
                    item = _eva.Items[i];

                    m_jsonWriter.WriteStartObject();
                    m_jsonWriter.WritePropertyName("Id"); //评价表数据编号
                    m_jsonWriter.WriteValue(item.ID);
                    if (item.ID == "836")
                    {
                        Debug.WriteLine("    ");
                    }
                    if (string.IsNullOrWhiteSpace(item.ID))
                    {
                        Debug.WriteLine("小项的ID为空===================");
                        Debug.WriteLine("");
                    }

                    // m_jsonWriter.WritePropertyName("grpId");
                    m_jsonWriter.WritePropertyName("parentId");
                    m_jsonWriter.WriteValue(item.GroupId);
                    if (string.IsNullOrEmpty(item.GroupId))
                    {
                        Debug.WriteLine("小项的组ID为空===================");
                    }
                    m_jsonWriter.WritePropertyName("Score");
                    m_jsonWriter.WriteValue(item.Score);
                    m_jsonWriter.WritePropertyName("remarkTourDes");
                    m_jsonWriter.WriteValue(item.Remark);

                    m_jsonWriter.WritePropertyName("fileNameGroup"); //二级表单数组
                    m_jsonWriter.WriteStartArray();
                    FileDataForUpload file;
                    for (int n = 0; n < item.Files.Count; n++)
                    {
                        file = item.Files[n];
                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("fileName");
                        m_jsonWriter.WriteValue(file.FileName);
                        m_jsonWriter.WritePropertyName("oldFileName");
                        m_jsonWriter.WriteValue(file.OldName);
                        m_jsonWriter.WriteEndObject();
                        if (_isUploadFile)
                        {
                            _Files.Add(file);
                        }
                    }
                    m_jsonWriter.WriteEndArray();
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
                //    switch (signature._signature_type)
                //    {
                //        case SIGNATURE_TYPE.valuator:
                //            m_jsonWriter.WriteValue("1");
                //            break;

                //        case SIGNATURE_TYPE.componentManager:
                //            m_jsonWriter.WriteValue("2");
                //            break;

                //        case SIGNATURE_TYPE.servesManager:
                //            m_jsonWriter.WriteValue("3");
                //            break;

                //        case SIGNATURE_TYPE.generalManager:
                //            m_jsonWriter.WriteValue("4");
                //            break;
                //    }

                //    m_jsonWriter.WritePropertyName("fileName"); //签名图片名字
                //    m_jsonWriter.WriteValue(uplodFile.FileName);

                //    m_jsonWriter.WritePropertyName("oldFileName"); //签名图片名字
                //    m_jsonWriter.WriteValue(uplodFile.OldName);

                //    m_jsonWriter.WriteEndObject();
                //}
                //m_jsonWriter.WriteEndArray();


                m_jsonWriter.WriteEndObject();
                _jsonTxt = m_strBuilderJson.ToString();
                _exJson = _jsonTxt;
                JObject jObj = JObject.Parse(_jsonTxt);
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


        private void CreateTestData()
        {
            _eva = new EvaluationForUpload();
            for (int i = 0; i < 8; i++)
            {
                GroupDataForUpload grp = new GroupDataForUpload();
                grp.ID = i.ToString();
                grp.Score = (i*new Random().Next(3, 10)).ToString();
                _eva.Groups.Add(grp);

                ItemDataForUpload item = new ItemDataForUpload();
                item.ID = (i*new Random().Next(10)).ToString();
                item.GroupId = (i*new Random().Next(5, 10)).ToString();
                item.Score = (i*new Random().Next(10)).ToString();
                item.Remark = "xxxxxxxxxxxxxxxx";
                int total = new Random().Next(1, 5);
                for (int n = 0; n < total; n++)
                {
                    FileDataForUpload file = new FileDataForUpload();
                    file.FileName = (n*new Random().Next(1, 10)).GetHashCode().ToString() + ".jpg";
                    file.OldName = (n*new Random().Next(3, 5)).GetHashCode().ToString() + ".jpg";
                    item.Files.Add(file);
                }
                _eva.Items.Add(item);
            }
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