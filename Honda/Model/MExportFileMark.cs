using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Honda.Model
{
    /*
     * 用于日程管理中的任务清单的Model
     * 9/28
     */

    [Serializable]
    public class MExportFileMark : INotifyPropertyChanged
    {
        private bool _feedBackMark;

        /// <summary>
        /// 
        /// </summary>
        public bool feedBackMark
        {
            get { return _feedBackMark; }
            set
            {
                if (_feedBackMark != value)
                {
                    _feedBackMark = value;
                    NotifyPropertyChanged("feedBackMark");
                }
            }
        }


        private bool _betterMark;

        /// <summary>
        /// 
        /// </summary>
        public bool betterMark
        {
            get { return _betterMark; }
            set
            {
                if (_betterMark != value)
                {
                    _betterMark = value;
                    NotifyPropertyChanged("betterMark");
                }
            }
        }

        private bool _tourMark;

        /// <summary>
        /// 
        /// </summary>
        public bool tourMark
        {
            get { return _tourMark; }
            set
            {
                if (_tourMark != value)
                {
                    _tourMark = value;
                    NotifyPropertyChanged("tourMark");
                }
            }
        }


        private bool _businessMark;

        /// <summary>
        /// 
        /// </summary>
        public bool businessMark
        {
            get { return _businessMark; }
            set
            {
                if (_businessMark != value)
                {
                    _businessMark = value;
                    NotifyPropertyChanged("businessMark");
                }
            }
        }

        private bool _excelMark;

        /// <summary>
        /// 
        /// </summary>
        public bool excelMark
        {
            get { return _excelMark; }
            set
            {
                if (_excelMark != value)
                {
                    _excelMark = value;
                    NotifyPropertyChanged("excelMark");
                }
            }
        }


        private bool _pdfMark;

        /// <summary>
        /// 
        /// </summary>
        public bool pdfMark
        {
            get { return _pdfMark; }
            set
            {
                if (_pdfMark != value)
                {
                    _pdfMark = value;
                    NotifyPropertyChanged("pdfMark");
                }
            }
        }

        /// <summary>
        /// 执行该任务的特约店ID
        /// </summary>
        public string shopId { get; set; }

        public string serialpath { get; set; }

        /// <summary>
        /// log ID
        /// </summary>
        public string accountName { get; set; }

        public string appriaseId { get; set; }

        public string excelURL { get; set; }
        public string excelPath { get; set; }

        public string pdfURL { get; set; }
        public string pdfPath { get; set; }

        public bool bIsDownloaded { get; set; }

        /// <summary>
        /// 店名字
        /// </summary>
        public string ShopName { get; set; }

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