using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using Honda.Model;
using Newtonsoft.Json;
using Honda.HttpLib.JsonInputData;
using System.Windows;
using System.ComponentModel;
using System.Security.Cryptography;

namespace Honda.HttpLib
{
    /// <summary>
    /// http请求的方式<现有方法包含四种>
    /// <date>2014-2-27 xiang</date>
    /// </summary>
    public enum RequestType
    {
        GET,
        POST,
        PUT,
        DELETE
    }


    public class WebState
    {
        public HttpWebRequest AsyncRequest { get; set; }
        public HttpWebResponse AsyncResponse { get; set; }
    }

    public class HttpRequestHeadInfo
    {
        public static string BOUNDARY = "---------------HondaSurface";
        public static string CONTENT_TYPE_OF_STRING = "application/x-www-form-urlencoded";
        //public static string CONTENT_TYPE_OF_STRING = "application/json";
        //public static string CONTENT_TYPE_OF_STRING_AND_FILE = "multipart/form-data";
        public static string CONTENT_TYPE_OF_STRING_AND_FILE = "multipart/form-data" + ";boundary=" + BOUNDARY;
    }

    public abstract class HttpBase
    {
        private WebClient webClient = new WebClient();

        /// <summary>
        /// 成功返回码
        /// <author>ddliu</author>
        /// </summary>
        public const string SUCCESS_CODE = "0";

        #region var members

        /// <summary>
        /// 网络请求的回调
        /// </summary>
        public Action<Object> m_onHandHttpRequest;

        /// <summary>
        /// 服务器地址
        /// </summary>
        //protected readonly string m_strServerDomain = "http://10.36.64.192:";

        ////打包版本
        //public static string m_strServerDomain = "10.251.68.242";
        ////打包版本
        //public static string m_strServerPort = "80";

        ////调试版本
        //public static string m_strServerDomain = "10.48.64.51";
        ////调试版本
        //public static string m_strServerPort = "8087";
        public static string m_strServerDomain =
            Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_ServerHost);

        public static string m_strServerPort =
            Honda.Globals.Tools.GetConfigValue(Honda.Globals.CONFIG_SETTING.IMP_ServerPort);

        /// <summary>
        /// 接口地址<服务器地址 + 接口地址 = 请求地址>
        /// </summary>
        protected string m_strInterfaceUrl = "";

        /// <summary>
        /// 请求对象
        /// </summary>
        protected HttpWebRequest m_httpRequest;

        protected string m_strContentType = HttpRequestHeadInfo.CONTENT_TYPE_OF_STRING;

        /// <summary>
        /// 向上传递的错误消息
        /// </summary>
        public string m_strErrorMsg;

        /// <summary>
        /// 是否出现异常
        /// </summary>
        public bool m_bIsExistException = false;

        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool m_bIsSuccess = false;

        /// <summary>
        /// 请求方式<GET or POST>
        /// </summary>
        protected RequestType m_RequestType;

        /// <summary>
        /// 请求的URI<服务器地址 + 接口地址 + 请求的相关参数>
        /// </summary>
        protected string m_strRequestUrl;

        /// <summary>
        /// 请求所附带的数据<byte类型>
        /// </summary>
        protected byte[] m_byteRequestData;

        /// <summary>
        /// 请求所对应的响应的数据<byte类型>
        /// </summary>
        protected byte[] m_byteResponseData;

        /// <summary>
        /// 添加json文本的头
        /// <author>xiang</author>
        /// </summary>
        protected readonly string PROTOCAL_PARAM = "json=";


        /// <summary>
        /// 请求所对应的响应的数据<流类型>
        /// </summary>
        //protected Stream m_streamResponseStream;

        #endregion

        /// <summary>
        /// 设置请求的URI
        /// </summary>
        /// <param name="strUrl"></param>
        protected void SetUrl(string strUrl)
        {
            m_strRequestUrl = strUrl;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="RType">发送请求的方式</param>
        /// <param name="strUrl">请求的URI</param>
        /// <param name="callBack">回调</param>
        protected HttpBase(RequestType RType, string strUrl, Action<Object> callBack)
        {
            InitJsonTools();
            m_onHandHttpRequest = callBack;
            this.m_RequestType = RType;
            this.m_strRequestUrl = strUrl;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="RType">发送请求的方式</param>
        /// <param name="callBack">回调</param>
        protected HttpBase(RequestType RType, Action<Object> callBack)
        {
            InitJsonTools();
            m_RequestType = RType;
            m_onHandHttpRequest = callBack;
        }

        public HttpBase(RequestType requestType, Action<ReqTestMulityForm> act)
        {
            // TODO: Complete member initialization
            this.requestType = requestType;
            this.act = act;
        }

        /// <summary>
        /// 请求结果<byte类型>
        /// </summary>
        public byte[] ResponseData
        {
            private set { }
            get { return m_byteResponseData; }
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        public void SendHttpRequest()
        {
            if (string.IsNullOrEmpty(m_strRequestUrl))
                //m_strRequestUrl = m_strServerDomain + m_strServerPort + m_strInterfaceUrl;
                m_strRequestUrl = "http://" + m_strServerDomain + ":" + m_strServerPort + m_strInterfaceUrl;

            Uri url = new Uri(m_strRequestUrl, UriKind.Absolute);
            m_httpRequest = (HttpWebRequest) WebRequest.Create(url);
            m_httpRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
            m_httpRequest.Accept = "application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            m_httpRequest.ContentType = m_strContentType;
            m_httpRequest.Timeout = 100000; //50秒

            if (m_RequestType == RequestType.GET)
            {
                GetData();
            }
            else if (m_RequestType == RequestType.POST)
            {
                PostData();
            }

            #region 打印日志

            string reqInfo = "";
            reqInfo += "请求地址:" + m_strRequestUrl + "\r\n";
            reqInfo += "接口：" + this.ToString() + "\r\n";
            //reqInfo += "请求参数：\r\n";
            //reqInfo += HttpUtility.UrlDecode(Encoding.UTF8.GetString(m_byteRequestData));
            //Log.PrintDebugLog("网络请求", reqInfo);
            Debug.WriteLine(reqInfo);

            #endregion
        }

        #region Post data

        /// <summary>
        /// 以POST方式发送请求
        /// </summary>
        protected void PostData()
        {
            try
            {
                m_httpRequest.Method = "POST";
                if (m_byteRequestData != null && m_byteRequestData.Length != 0)
                    m_httpRequest.ContentLength = m_byteRequestData.Length;
                WebState state = new WebState();
                state.AsyncRequest = m_httpRequest;
                m_httpRequest.BeginGetRequestStream(new AsyncCallback(RequestReady), state);
            }
            catch (System.Exception ex)
            {
                m_bIsSuccess = false;
                m_bIsExistException = true;
                m_onHandHttpRequest(this);
                //Log.PrintErrorLog(this.ToString(), "\r\nPostData()异常\r\n" + ex.Message);
            }
        }

        protected void RequestReady(IAsyncResult asyncResult)
        {
            WebState state = asyncResult.AsyncState as WebState;
            HttpWebRequest request = state.AsyncRequest;
            try
            {
                if (m_byteRequestData != null && m_byteRequestData.Length > 0)
                {
                    Stream postStream = request.EndGetRequestStream(asyncResult);
                    postStream.Write(m_byteRequestData, 0, m_byteRequestData.Length);
                    postStream.Close();
                    postStream.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                m_bIsSuccess = false;
                m_bIsExistException = true;
                Debug.WriteLine("**********exception**********\r\nRequestReady异常\r\n" + ex.Message.ToString());
                m_onHandHttpRequest(this);
                return;
            }

            request.BeginGetResponse(RespCallback, state);
        }

        #endregion

        #region Get data

        /// <summary>
        /// 以GET方式发送请求
        /// </summary>
        protected void GetData()
        {
            m_httpRequest.Method = "GET";
            WebState state = new WebState();
            state.AsyncRequest = m_httpRequest;
            m_httpRequest.BeginGetResponse(RespCallback, state);
        }

        #endregion

        /// <summary>
        /// 请求所对应的响应<异步>
        /// </summary>
        /// <param name="asynchronousResult"></param>
        private void RespCallback(IAsyncResult asynchronousResult)
        {
            WebState State = (WebState) asynchronousResult.AsyncState;
            HttpWebRequest request = (HttpWebRequest) State.AsyncRequest;
            bool HaveRes = m_httpRequest.HaveResponse;
            try
            {
                State.AsyncResponse = (HttpWebResponse) request.EndGetResponse(asynchronousResult);
                if (State.AsyncResponse.StatusCode != HttpStatusCode.OK)
                {
                    //如果返回的状态不是200 则接口调用失败
                    Debug.WriteLine("接口调用失败");
                }

                //WebHeaderCollection wc = State.AsyncResponse.Headers;
                //string[] keys = wc.AllKeys;

                //var response = State.AsyncRequest.GetResponse();
                //var responseStream =response.GetResponseStream();
                var responseStream = State.AsyncResponse.GetResponseStream();
                //-------------------------
                MemoryStream memoryStream = new MemoryStream();
                const int bufferLength = 1024;
                int actual;
                byte[] buffer = new byte[bufferLength];
                while ((actual = responseStream.Read(buffer, 0, bufferLength)) > 0)
                {
                    memoryStream.Write(buffer, 0, actual);
                }
                memoryStream.Position = 0;
                int len = Convert.ToInt32(memoryStream.Length);
                m_byteResponseData = new Byte[len];
                memoryStream.Read(m_byteResponseData, 0, len);
                string strRet = Encoding.UTF8.GetString(m_byteResponseData);
                State.AsyncResponse.Close();
                responseStream.Dispose();
                m_bIsSuccess = true;

                #region 打印日志

                string strOutInf = "";
                strOutInf += "请求地址：\r\n";
                strOutInf += m_strRequestUrl;
                //strOutInf += "\r\n请求参数:\r\n";
                //strOutInf += HttpUtility.UrlDecode(Encoding.UTF8.GetString(m_byteRequestData));
                strOutInf += "\r\n响应结果:\r\n";
                strOutInf += "数据字长:" + m_byteResponseData.Length.ToString();
                strOutInf += "\r\n内容:\r\n";
                //strOutInf += Encoding.UTF8.GetString(m_byteResponseData);
                //Debug.WriteLine(strOutInf);
                //Log.PrintDebugLog("服务器响应内容", strOutInf);

                #endregion

                m_bIsExistException = false;
                m_onHandHttpRequest(this);
            }
            catch (System.Exception ex)
            {
                m_bIsExistException = true;
                m_bIsSuccess = false;
                m_strErrorMsg = ex.Message.ToString();
                Debug.WriteLine("HttpBase>>>>Exception-------\r\n" + ex.Message.ToString());
                m_onHandHttpRequest(this);

                #region 打印日志

                string logContent = "";
                logContent += "请求地址：";
                logContent += m_strRequestUrl;
                logContent += "\r\n";
                //logContent += "请求参数\r\n";
                //logContent += Encoding.UTF8.GetString(m_byteRequestData);
                logContent += "\r\n 异常信息:\r\n";
                logContent += ex.Message;
                //Log.PrintErrorLog(this.ToString() + ":" + "RespCallback", logContent, ErrorLevel.HIGH_LEVEL);

                #endregion
            }
        }

        /// <summary>
        /// 装箱 <封装请求所对应的数据>
        /// 组装格式根据接口定义<json、xml...>
        /// </summary>
        public abstract void BuildParam();

        #region tools 写文件对象 & 解析文件对象

        protected void WriteJsonAndFileData(string jsonTxt, List<FileDataForUpload> files)
        {
            try
            {
                var memStream = new MemoryStream();
                string boundary = HttpRequestHeadInfo.BOUNDARY; // ---------------mindray
                // 边界符
                byte[] beginBoundary = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");
                // 最后的结束符
                byte[] endBoundary = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                memStream.Write(beginBoundary, 0, beginBoundary.Length);

                // 写入json的Key
                var stringKeyHeader = "Content-Disposition: form-data; name=\"{0}\"" +
                                      "\r\n\r\n{1}";
                var txtContent = string.Format(stringKeyHeader, "json", jsonTxt);
                var byteDataOfTxt = Encoding.UTF8.GetBytes(txtContent);
                memStream.Write(byteDataOfTxt, 0, byteDataOfTxt.Length);

                if (files.Count == 0)
                {
                    string strRegion = "\r\n--" + boundary + "\r\n";
                    var byteRegion = Encoding.UTF8.GetBytes(strRegion);
                    memStream.Write(byteRegion, 0, byteRegion.Length);
                    const string filePartHeader =
                        "Content-Disposition: form-data;name=\"{0}\";filename=\"{1}\"\r\n" +
                        "Content-Type: application/octet-stream\r\n\r\n";
                    var header = string.Format(filePartHeader, MFileData.FileHeadName, "");
                    var headerbytes = Encoding.UTF8.GetBytes(header);
                    memStream.Write(headerbytes, 0, headerbytes.Length);
                }
                else
                {
                    foreach (FileDataForUpload file in files)
                    {
                        string strRegion = "\r\n--" + boundary + "\r\n";
                        var byteRegion = Encoding.UTF8.GetBytes(strRegion);
                        memStream.Write(byteRegion, 0, byteRegion.Length);
                        const string filePartHeader =
                            "Content-Disposition: form-data;name=\"{0}\";filename=\"{1}\"\r\n" +
                            "Content-Type: application/octet-stream\r\n\r\n";
                        var header = string.Format(filePartHeader, "file", file.FileName);
                        var headerbytes = Encoding.UTF8.GetBytes(header);
                        memStream.Write(headerbytes, 0, headerbytes.Length);
                        var fileStream = new FileStream(file.FilePath, FileMode.Open, FileAccess.Read);
                        var buffer = new byte[1024];
                        int bytesRead; // =0
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            memStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }

                memStream.Write(endBoundary, 0, endBoundary.Length);
                memStream.Position = 0;
                m_byteRequestData = new byte[memStream.Length];
                memStream.Read(m_byteRequestData, 0, m_byteRequestData.Length);
                memStream.Close();
                //string str = Encoding.UTF8.GetString(m_byteRequestData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("方法错误WriteJsonAndFileData()\n" + ex.Message);
            }
        }

        /// <summary>
        /// 解析单个的文件信息
        /// <author>xiang</author>
        /// <date>2014-4-24</date>
        /// </summary>
        /// <param name="objFile"></param>
        /// <returns></returns>
        //protected MFileData ParseFileItem(JObject objFile)
        //{
        //    try
        //    {
        //        string fileName = string.Empty;
        //        string fileUri = string.Empty;
        //        if (null != objFile.Property("sourceName"))
        //            fileName = objFile["sourceName"].ToString();
        //        if (null != objFile.Property("fullFilePath"))
        //            fileUri = objFile["fullFilePath"].ToString();
        //        MFileData fileDataItem; //= new FileData(fileUri, FileTypeFirstDegree.PROBLEM_DESCRIBE, fileName);
        //        return fileDataItem;
        //    }
        //    catch (System.Exception ex)
        //    {

        //        Debug.WriteLine("####################################");
        //        Debug.WriteLine(this.ToString() + "解析filedata出错 ParseFileItem 参数名称不统一");
        //        Debug.WriteLine("####################################");
        //        return null;
        //        //MessageBox
        //    }

        //}

        #endregion

        /// <summary>
        /// 拆箱
        /// 将请求的结果进行拆解，生成相应的数据对象
        /// </summary>
        public abstract void ParseParam();

        /// <summary>
        /// 将UI model传下来的数据转换成byte[]data
        /// </summary>
        protected abstract void ConvertPostDataToBytesData();

        #region json model

        //protected string m_strRequestJsonData;

        protected StringBuilder m_strBuilderJson = null;
        protected StringWriter m_sw = null;
        protected JsonTextWriter m_jsonWriter = null;
        private RequestType requestType;
        private Action<ReqTestMulityForm> act;

        protected void InitJsonTools()
        {
            m_strBuilderJson = new StringBuilder();
            m_sw = new StringWriter(m_strBuilderJson);
            m_jsonWriter = new JsonTextWriter(m_sw);
        }

        /// <summary>
        /// json中的公共参数
        /// <date>2014-2-27xiang</date>
        /// </summary>
        protected void WriteCommonParamOfRequest()
        {
            //如果有公共的参数需要填充，则调用此函数进行填充
            m_jsonWriter.WritePropertyName("os"); //系统类型
            m_jsonWriter.WriteValue(2);

            m_jsonWriter.WritePropertyName("osVersion"); //系统版本号
            m_jsonWriter.WriteValue("7.5");

            m_jsonWriter.WritePropertyName("appVersion"); //应用版本号
            m_jsonWriter.WriteValue("1.0");
        }

        #endregion

        #region 读取&接口修改地址

        //static string path = Application.StartupPath + "//" + "UriInfo.xml";
        //public static void GetUriInfo()
        //{
        //    XmlDocument doc = new XmlDocument();
        //    try
        //    {
        //        if (!File.Exists(path))
        //        {
        //            return;
        //        }
        //        doc.Load(path);
        //        if (doc != null)
        //        {
        //            XmlNode _DomainIP = doc.SelectSingleNode("Uri/UriInfo/UriPath");
        //            XmlNode _Port = doc.SelectSingleNode("Uri/UriInfo/UriPoint");
        //            if (_DomainIP != null && !String.IsNullOrWhiteSpace(_DomainIP.InnerText))
        //                m_strServerDomain = _DomainIP.InnerText;

        //            if (_Port != null && !String.IsNullOrWhiteSpace(_Port.InnerText))
        //                m_strServerPort = _Port.InnerText;


        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine("获取地址失败" + ex.Message);
        //        //Log.PrintErrorLog("HttpBase", "获取地址失败\r\nGetUriInfo()\r\n" + ex.Message);
        //    }

        //}
        //public static bool SaveUriInfo()
        //{
        //    try
        //    {
        //        XmlDocument doc = new XmlDocument();
        //        doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));
        //        XmlElement xmlElem = doc.CreateElement("Uri");
        //        XmlElement xmlElemUriInfo = doc.CreateElement("UriInfo");
        //        doc.AppendChild(xmlElem);
        //        doc.ChildNodes.Item(1).AppendChild(xmlElemUriInfo);

        //        XmlElement xmlElemUri = doc.CreateElement("UriPath");
        //        XmlText xmlTextUserUri = doc.CreateTextNode(m_strServerDomain);
        //        xmlElemUri.AppendChild(xmlTextUserUri);

        //        XmlElement xmlPoint = doc.CreateElement("UriPoint");
        //        XmlText xmlTextPoint = doc.CreateTextNode(m_strServerPort);
        //        xmlPoint.AppendChild(xmlTextPoint);

        //        doc.ChildNodes.Item(1).ChildNodes[0].AppendChild(xmlElemUri);
        //        doc.ChildNodes.Item(1).ChildNodes[0].AppendChild(xmlPoint);
        //        doc.Save(path);
        //        return true;
        //    }
        //    catch (System.Exception ex)
        //    {

        //        Debug.WriteLine("保存接口地址失败" + ex.Message);
        //        //Log.PrintErrorLog("HttpBase", "保存接口地址失败\r\nSaveUriInfo()\r\n" + ex.Message);
        //    }
        //    return false;
        //}

        #endregion

        #region HTTP下载文件

        public void DownloadFile(string resourcePath, string destinationDir, string filename,
            Action<bool, string> action)
        {
            var url = "http://" + m_strServerDomain + ":" + m_strServerPort + "/imp";

            Uri resourceURL = new Uri(string.Format("{0}/{1}", url, resourcePath));

            try
            {
                webClient.DownloadFile(resourceURL, destinationDir);
            }
            catch (Exception ex)
            {
                action(false, string.Format("文件：{0} 下载失败；错误原因：{1}", filename, ex.Message));
            }
        }

        #endregion

        #region MD5

        protected string _timestamp { get; private set; }
        private const string token = "HONDA";

        public string GetMD5Str(string logid)
        {
            lock (this)
            {
                _timestamp = GetTimestamp();
            }
            var key = string.Format("{0}{1}{2}", logid, token, _timestamp);

            string pwd = "";

            //实例化一个md5对像
            MD5 md5 = MD5.Create();

            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(key));

            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }

            return pwd;
        }

        public string GetTimestamp()
        {
            return DateTime.Now.Ticks.ToString();
        }

        #endregion
    }

    public class ResponseObject
    {
        public bool isSuccess { get; set; }
        public string rcode { get; set; }

        public string resultMsg { get; set; }
    }
}