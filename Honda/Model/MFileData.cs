using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Diagnostics;

namespace Honda.Model
{
    /// <summary>
    /// 文件对象《包括上传文件及下载文件》
    /// </summary>
    [Serializable]
    [DataContract]
    public class MFileData : INotifyPropertyChanged
    {
        private Action<bool> _OnHandDownloadCompleted;

        /// <summary>
        /// 下载文件 
        /// 如果为下载文件，则只需要传入一级类型《方案书、报价单、问题、建议意见》
        /// </summary>
        /// <param name="uri">网络路径</param>
        /// <param name="t">下载的文件类型</param>
        public MFileData(string uri, string fileName = "")
        {
            FileUriOnServer = uri;
            sourceFileName = fileName;
            FileName = fileName;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="path">文件在本机的路径</param>
        /// <param name="t">文件类型</param>
        public MFileData(string path)
        {
            FilePath = path;

            FileName = Path.GetFileName(path); //修改 xiang 不再重新生成文件名
            sourceFileName = FileName;
            //FileName = BuildFileName(FilePath, TypeSec);
            //FileName = Path.GetFileName(path);
        }

        private string _StrNo;

        /// <summary>
        /// 序号
        /// </summary>
        public string StrNo
        {
            get { return _StrNo; }
            set
            {
                if (_StrNo != value)
                {
                    _StrNo = value;
                    NotifyPropertyChanged("StrNo");
                }
            }
        }


        private string _Prompt = "下载";

        /// <summary>
        /// 序号
        /// </summary>
        public string Prompt
        {
            get { return _Prompt; }
            set
            {
                if (_Prompt != value)
                {
                    _Prompt = value;
                    NotifyPropertyChanged("Prompt");
                }
            }
        }


        /// <summary>
        /// 文件名
        /// </summary>
        private string fileName;

        /// <summary>
        /// 文件名
        /// </summary>
        [DataMember(Name = "name")]
        public string FileName
        {
            get { return fileName; }
            set
            {
                if (fileName != value)
                {
                    fileName = value;
                    NotifyPropertyChanged("FileName");
                }
            }
        }

        /// <summary>
        /// 源文件名
        /// </summary>
        public string sourceFileName;

        /// <summary>
        /// 完整路径，包含文件名
        /// </summary>
        public string FilePath { get; set; }


        /// <summary>
        /// 文件在服务器上的地址《网络地址》
        /// <author>xiang</author>
        /// </summary>
        private string fileUriOnServer;

        /// <summary>
        /// 文件在服务器上的地址《网络地址》
        /// <author>xiang</author>
        /// </summary>
        public string FileUriOnServer
        {
            get { return fileUriOnServer; }
            set { fileUriOnServer = value; }
        }

        /// <summary>
        /// 多表单提交 头信息 文件名<表示该表单内数据为文件数据，而不是指具体的文件名>
        /// <author>xiang</author>
        ///<date>2014-3-18</date>
        /// </summary>
        public static readonly string FileHeadName = "file";

        #region 下载文件到本地 by luyonghe

        /// <summary>
        /// 下载文件到本地
        /// </summary>
        /// <param name="downloadUri">下载网络路径</param>
        /// <param name="savePath">保存路径（包括文件名）</param>
        public void DownloadFile(Action<bool> action)
        {
            _OnHandDownloadCompleted = action;
            try
            {
                if (!File.Exists(FilePath))
                {
                    HttpWebRequest httpReq = (HttpWebRequest) HttpWebRequest.Create(FileUriOnServer);
                    using (HttpWebResponse httpResp = (HttpWebResponse) httpReq.GetResponse())
                    {
                        using (Stream responseStream = httpResp.GetResponseStream())
                        {
                            using (Stream fileStream = new FileStream(FilePath, System.IO.FileMode.Create))
                            {
                                long downloadedBytesCount = 0;
                                byte[] temp = new byte[1024];
                                int indeedCount = responseStream.Read(temp, 0, (int) temp.Length);

                                while (indeedCount > 0)
                                {
                                    downloadedBytesCount = indeedCount + downloadedBytesCount;
                                    System.Windows.Forms.Application.DoEvents();
                                    fileStream.Write(temp, 0, indeedCount);
                                    indeedCount = responseStream.Read(temp, 0, (int) temp.Length);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("文件已存在\r\n" + "网络路径: " + FileUriOnServer + "\r\n 本地路径： " + FilePath);
                }

                _OnHandDownloadCompleted(true);
            }
            catch (System.Exception e)
            {
                string logTitle = "下载文件异常";
                string logContent = "";
                logContent += "文件下载地址：";
                logContent += FileUriOnServer;
                logContent += "\r\n";
                logContent += "文件类型：";

                logContent += "\r\n文件名\r\n";
                logContent += FileName;
                logContent += "\r\n 异常信息:\r\n";
                logContent += e.Message;

                Debug.WriteLine("*******error******\r\n下载文件出错：\r\n" + e.Message);
                Debug.WriteLine("文件URI:\r\n" + FileUriOnServer);
                if (_OnHandDownloadCompleted != null)
                {
                    _OnHandDownloadCompleted(false);
                }
            }
        }


        /// <summary>
        /// 下载完成时触发该事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                _OnHandDownloadCompleted(false);
                //Log.PrintErrorLog("FileData", FilePath+"\r\nclient_DownloadFileCompleted\r\n" + e.Error.ToString());
                Debug.WriteLine(e.Error);
            }
            else
            {
                _OnHandDownloadCompleted(true);
                Debug.WriteLine(FilePath + "完成下载");
            }
        }

        #endregion

        public override string ToString()
        {
            string strInfo = "";
            strInfo += "********文件信息*********Begin\r\n";
            strInfo += "文件路径：" + FilePath + "\r\n";
            //strInfo += "文件类型：" + typeFirst.ToString() + "\r\n";
            strInfo += "********文件信息*********end\r\n";
            return strInfo;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 用于通知属性的改变
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}