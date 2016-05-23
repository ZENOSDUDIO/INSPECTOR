using Honda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.ViewModel
{
    /// <summary>
    /// 文件下载管理器
    /// <author>xiang</author>
    /// </summary>
    [Serializable]
    public class DMFileDownload
    {
        private static DMFileDownload _instance;
        private Queue<MFileData> queueFiles = new Queue<MFileData>();

        /// <summary>
        /// 下载的队列数
        /// </summary>
        public int DownQueueCount = 0;

        /// <summary>
        /// 下载文件数
        /// </summary>
        public int DownFileCount
        {
            get { return queueFiles.Count; }
        }


        private DMFileDownload()
        {
        }

        public static DMFileDownload INSTANCE
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DMFileDownload();
                }
                return _instance;
            }
        }

        private bool _isDownloading = false;

        public void AddTask(MFileData file)
        {
            queueFiles.Enqueue(file);
            if (!_isDownloading)
            {
                ExcuteTask();
            }
        }

        private void ExcuteTask()
        {
            if (queueFiles.Count > 0)
            {
                MFileData file = queueFiles.Dequeue();
                _isDownloading = true;
                file.DownloadFile((isSuccess) => { ExcuteTask(); });
            }
            else
            {
                _isDownloading = false;
            }
        }


        public void StopTaks()
        {
            queueFiles.Clear();
        }

        /// <summary>
        /// 广播<当前下载格式 + 是否需要关闭同步窗口>
        /// </summary>
        private Action<string, bool> _broadcast;

        /// <summary>
        /// 用于通知同步窗口当前正在下载的队列个数以及是否需要关闭同步窗口
        /// </summary>
        /// <param name="action"></param>
        public void LanuchBroadcast(Action<string, bool> action)
        {
            _broadcast = action;
        }
    }
}