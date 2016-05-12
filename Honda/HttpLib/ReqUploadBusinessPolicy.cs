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
     * 上传商务政策的接口  - by luyonghe 
     */

    public class ReqUploadBusinessPolicyList : HttpBase
    {
        private string _jsonTxt;
        private List<FileDataForUpload> _Files = new List<FileDataForUpload>();
        private ObservableCollection<MNotPureComponent> listNotPureComponent;
        private ObservableCollection<MComponentESDepartment> listComponentESDepartment1;
        private ObservableCollection<MComponentESDepartment2> listComponentESDepartment2;
        private ObservableCollection<MComponentPrice> ListComponentPrice;
        private MSignature[] ListSignature;

        /// <summary>
        /// 请求json 字符串
        /// </summary>
        private string _exJson;

        public ReqUploadBusinessPolicyList(ObservableCollection<MNotPureComponent> _listNotPureComponent,
            ObservableCollection<MComponentESDepartment> _listComponentESDepartment1,
            ObservableCollection<MComponentESDepartment2> _listComponentESDepartment2,
            ObservableCollection<MComponentPrice> _ListComponentPrice,
            MSignature[] _ListSignature, Action<object> callback)
            : base(RequestType.POST, callback)
        {
            listNotPureComponent = _listNotPureComponent;
            listComponentESDepartment1 = _listComponentESDepartment1;
            listComponentESDepartment2 = _listComponentESDepartment2;
            ListComponentPrice = _ListComponentPrice;
            ListSignature = _ListSignature;
            m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING_AND_FILE;

            //打包地址
            m_strInterfaceUrl =
                Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_IF_ReqUploadBusinessPolicy);
                // "/imp/tourEvaluation/addBusinessPolicy";
            // m_strInterfaceUrl = "/impInterface/tourEvaluation/queryWorkHighlightsAndIdea";tourEvaluation/addBusinessPolicy
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


                m_jsonWriter.WritePropertyName("data");
                m_jsonWriter.WriteStartArray();
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("shopId");
                m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.shopId);
                m_jsonWriter.WritePropertyName("appriaseId");
                m_jsonWriter.WriteValue(DMStoreTour.INSTANCE.CurrentMStore.appriaseId);

                //--------------签名数据-----------------------------
                //m_jsonWriter.WritePropertyName("signatureGroup");
                //m_jsonWriter.WriteStartArray();

                //foreach (MSignature signature in ListSignature)
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

                //    m_jsonWriter.WritePropertyName("signatureFileName"); //签名图片名字
                //    m_jsonWriter.WriteValue(uplodFile.FileName);

                //    m_jsonWriter.WritePropertyName("oldFileName"); //签名图片名字
                //    m_jsonWriter.WriteValue(uplodFile.OldName);

                //    m_jsonWriter.WriteEndObject();
                //}
                //m_jsonWriter.WriteEndArray();
                //^^^^^^^^^^^^^^^^^^^^^^^^^^签名数据^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^


                //-------------------------价格：零部件价格执行-------------------
                m_jsonWriter.WritePropertyName("price");
                m_jsonWriter.WriteStartArray();
                foreach (MComponentPrice _price in ListComponentPrice)
                {
                    m_jsonWriter.WriteStartObject();
                    m_jsonWriter.WritePropertyName("partName");
                    m_jsonWriter.WriteValue(_price.partName);
                    m_jsonWriter.WritePropertyName("partNo");
                    m_jsonWriter.WriteValue(_price.partNo);
                    m_jsonWriter.WritePropertyName("salePrice");
                    m_jsonWriter.WriteValue(_price.salePrice);
                    m_jsonWriter.WritePropertyName("normalPrice");
                    m_jsonWriter.WriteValue(_price.normalPrice);
                    m_jsonWriter.WritePropertyName("spread");
                    m_jsonWriter.WriteValue(_price.spread);
                    m_jsonWriter.WritePropertyName("partNum");
                    m_jsonWriter.WriteValue(_price.partNum);
                    m_jsonWriter.WritePropertyName("endDate");
                    m_jsonWriter.WriteValue(_price.endDate);
                    m_jsonWriter.WritePropertyName("startDate");
                    m_jsonWriter.WriteValue(_price.startDate);
                    //附件
                    m_jsonWriter.WritePropertyName("fileNameGroup");
                    m_jsonWriter.WriteStartArray();
                    foreach (string pathName in _price.remrks.ImagePathList)
                    {
                        string oldName = Path.GetFileName(pathName);
                        string fileName = Path.GetFileName(DirectoryHelper.INSTANCE.GetFileNewName(pathName));
                        FileDataForUpload file = new FileDataForUpload();
                        file.OldName = oldName;
                        file.FileName = fileName;
                        file.FilePath = pathName;
                        _Files.Add(file);

                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("fileName");
                        m_jsonWriter.WriteValue(fileName);
                        m_jsonWriter.WritePropertyName("oldFileName");
                        m_jsonWriter.WriteValue(oldName);
                        m_jsonWriter.WriteEndObject();
                    }
                    m_jsonWriter.WriteEndArray();
                    m_jsonWriter.WritePropertyName("description");
                    m_jsonWriter.WriteValue(_price.remrks.content);
                    m_jsonWriter.WriteEndObject();
                }
                m_jsonWriter.WriteEndArray();


                //^^^^^^^^^^^^^^^^^^^^^^^^^^价格：零部件价格执行^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^


                //-------------------------纯正性：非纯正零部件使用情况-------------------
                m_jsonWriter.WritePropertyName("part");
                m_jsonWriter.WriteStartArray();
                foreach (MNotPureComponent _pureComponent in listNotPureComponent)
                {
                    m_jsonWriter.WriteStartObject();
                    m_jsonWriter.WritePropertyName("partName");
                    m_jsonWriter.WriteValue(_pureComponent.partName);
                    m_jsonWriter.WritePropertyName("partNo");
                    m_jsonWriter.WriteValue(_pureComponent.partNo);
                    m_jsonWriter.WritePropertyName("partNum");
                    m_jsonWriter.WriteValue(_pureComponent.partNum);
                    m_jsonWriter.WritePropertyName("price");
                    m_jsonWriter.WriteValue(_pureComponent.price);
                    m_jsonWriter.WritePropertyName("endDate");
                    m_jsonWriter.WriteValue(_pureComponent.endDate);
                    m_jsonWriter.WritePropertyName("startDate");
                    m_jsonWriter.WriteValue(_pureComponent.startDate);
                    m_jsonWriter.WritePropertyName("provider");
                    m_jsonWriter.WriteValue(_pureComponent.provider);
                    //附件
                    m_jsonWriter.WritePropertyName("fileNameGroup");
                    m_jsonWriter.WriteStartArray();
                    foreach (string pathName in _pureComponent.remarks.ImagePathList)
                    {
                        string oldName = Path.GetFileName(pathName);
                        string fileName = Path.GetFileName(DirectoryHelper.INSTANCE.GetFileNewName(pathName));
                        FileDataForUpload file = new FileDataForUpload();
                        file.OldName = oldName;
                        file.FileName = fileName;
                        file.FilePath = pathName;
                        _Files.Add(file);

                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("fileName");
                        m_jsonWriter.WriteValue(fileName);
                        m_jsonWriter.WritePropertyName("oldFileName");
                        m_jsonWriter.WriteValue(oldName);
                        m_jsonWriter.WriteEndObject();
                    }
                    m_jsonWriter.WriteEndArray();
                    m_jsonWriter.WritePropertyName("description");
                    m_jsonWriter.WriteValue(_pureComponent.remarks.content);
                    m_jsonWriter.WriteEndObject();
                }
                m_jsonWriter.WriteEndArray();

                //^^^^^^^^^^^^^^^^^^^^^^^^^^纯正性：非纯正零部件使用情况^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^


                //-------------------------外销：零部件对外销售情况-------------------

                m_jsonWriter.WritePropertyName("sale");
                m_jsonWriter.WriteStartObject();
                m_jsonWriter.WritePropertyName("part1");
                m_jsonWriter.WriteStartArray();
                foreach (MComponentESDepartment _ESDepartment1 in listComponentESDepartment1)
                {
                    m_jsonWriter.WriteStartObject();

                    m_jsonWriter.WritePropertyName("exportSaler");
                    m_jsonWriter.WriteValue(_ESDepartment1.exportSaler);
                    m_jsonWriter.WritePropertyName("mainPart");
                    m_jsonWriter.WriteValue(_ESDepartment1.mainPart);
                    m_jsonWriter.WritePropertyName("price");
                    m_jsonWriter.WriteValue(_ESDepartment1.price);
                    m_jsonWriter.WritePropertyName("endDate");
                    m_jsonWriter.WriteValue(_ESDepartment1.endDate);
                    m_jsonWriter.WritePropertyName("startDate");
                    m_jsonWriter.WriteValue(_ESDepartment1.startDate);
                    //附件
                    m_jsonWriter.WritePropertyName("fileNameGroup");
                    m_jsonWriter.WriteStartArray();
                    foreach (string pathName in _ESDepartment1.remrks.ImagePathList)
                    {
                        string oldName = Path.GetFileName(pathName);
                        string fileName = Path.GetFileName(DirectoryHelper.INSTANCE.GetFileNewName(pathName));
                        FileDataForUpload file = new FileDataForUpload();
                        file.OldName = oldName;
                        file.FileName = fileName;
                        file.FilePath = pathName;
                        _Files.Add(file);

                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("fileName");
                        m_jsonWriter.WriteValue(fileName);
                        m_jsonWriter.WritePropertyName("oldFileName");
                        m_jsonWriter.WriteValue(oldName);
                        m_jsonWriter.WriteEndObject();
                    }
                    m_jsonWriter.WriteEndArray();
                    m_jsonWriter.WritePropertyName("description");
                    m_jsonWriter.WriteValue(_ESDepartment1.remrks.content);
                    m_jsonWriter.WriteEndObject();
                }
                m_jsonWriter.WriteEndArray();

                m_jsonWriter.WritePropertyName("part2");
                m_jsonWriter.WriteStartArray();
                foreach (MComponentESDepartment2 _ESDepartment2 in listComponentESDepartment2)
                {
                    m_jsonWriter.WriteStartObject();
                    m_jsonWriter.WritePropertyName("partNo");
                    m_jsonWriter.WriteValue(_ESDepartment2.partNo);
                    m_jsonWriter.WritePropertyName("partName");
                    m_jsonWriter.WriteValue(_ESDepartment2.partName);
                    m_jsonWriter.WritePropertyName("partNum");
                    m_jsonWriter.WriteValue(_ESDepartment2.partNum);
                    m_jsonWriter.WritePropertyName("endDate");
                    m_jsonWriter.WriteValue(_ESDepartment2.endDate);
                    m_jsonWriter.WritePropertyName("startDate");
                    m_jsonWriter.WriteValue(_ESDepartment2.startDate);
                    m_jsonWriter.WritePropertyName("exportObject");
                    m_jsonWriter.WriteValue(_ESDepartment2.exportObject);


                    //附件
                    m_jsonWriter.WritePropertyName("fileNameGroup");
                    m_jsonWriter.WriteStartArray();
                    foreach (string pathName in _ESDepartment2.remrks.ImagePathList)
                    {
                        string oldName = Path.GetFileName(pathName);
                        string fileName = Path.GetFileName(DirectoryHelper.INSTANCE.GetFileNewName(pathName));
                        FileDataForUpload file = new FileDataForUpload();
                        file.OldName = oldName;
                        file.FileName = fileName;
                        file.FilePath = pathName;
                        _Files.Add(file);

                        m_jsonWriter.WriteStartObject();
                        m_jsonWriter.WritePropertyName("fileName");
                        m_jsonWriter.WriteValue(fileName);
                        m_jsonWriter.WritePropertyName("oldFileName");
                        m_jsonWriter.WriteValue(oldName);
                        m_jsonWriter.WriteEndObject();
                    }
                    m_jsonWriter.WriteEndArray();
                    m_jsonWriter.WritePropertyName("description");
                    m_jsonWriter.WriteValue(_ESDepartment2.remrks.content);
                    m_jsonWriter.WriteEndObject();
                }
                m_jsonWriter.WriteEndArray();


                m_jsonWriter.WriteEndObject();
                //^^^^^^^^^^^^^^^^^^^^^^^^^^外销：零部件对外销售情况^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

                m_jsonWriter.WriteEndObject();
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

                if (code == SUCCESS_CODE)
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