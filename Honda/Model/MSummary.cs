using Honda.Globals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Honda.Model
{
    /// <summary>
    /// 工作亮点与意见需求
    /// </summary>
    [Serializable]
    public class MSummary : INotifyPropertyChanged
    {
        #region 文档

        private string _docPath;

        /// <summary>
        /// 文档路径
        /// </summary>
        public string docPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_docPath))
                {
                    DocName = "";
                }
                else
                {
                    DocName = Path.GetFileName(_docPath);
                }

                return _docPath;
            }
            set
            {
                if (value != _docPath)
                {
                    _docPath = value;
                    if (string.IsNullOrWhiteSpace(_docPath))
                    {
                        DocName = "";
                    }
                    else
                    {
                        DocName = Path.GetFileName(_docPath);
                    }

                    NotifyPropertyChanged("docPath");
                }
            }
        }


        /// <summary>
        /// 文档名
        /// </summary>
        private string _docName;

        public string DocName
        {
            get
            {
                if (_docName == null)
                {
                    _docName = "";
                }
                ShowOrHidenCtrOfDoc();
                return _docName;
            }

            set
            {
                if (_docName != value)
                {
                    _docName = value;
                    ShowOrHidenCtrOfDoc();
                    NotifyPropertyChanged("DocName");
                }
            }
        }


        /// <summary>
        /// 是否显示文档项中的上传按钮
        /// </summary>
        private Visibility _IsShowDocUploadBtn = Visibility.Collapsed;

        public Visibility IsShowDocUploadBtn
        {
            get { return _IsShowDocUploadBtn; }
            set
            {
                if (_IsShowDocUploadBtn != value)
                {
                    _IsShowDocUploadBtn = value;
                    NotifyPropertyChanged("IsShowDocUploadBtn");
                }
            }
        }

        /// <summary>
        /// 是否显示文档项中的“未上传”TextBlock
        /// </summary>
        private Visibility _IsShowDocNotUpload = Visibility.Collapsed;

        public Visibility IsShowDocNotUpload
        {
            get { return _IsShowDocNotUpload; }
            set
            {
                if (_IsShowDocNotUpload != value)
                {
                    _IsShowDocNotUpload = value;
                    NotifyPropertyChanged("IsShowDocNotUpload");
                }
            }
        }

        /// <summary>
        /// 是否显示文档项中的“文档”
        /// </summary>
        private Visibility _IsShowDoc = Visibility.Collapsed;

        public Visibility IsShowDoc
        {
            get { return _IsShowDoc; }
            set
            {
                if (_IsShowDoc != value)
                {
                    _IsShowDoc = value;
                    NotifyPropertyChanged("IsShowDoc");
                }
            }
        }

        /// <summary>
        /// 根据条件隐藏或者显示文档项控件
        /// </summary>
        private void ShowOrHidenCtrOfDoc()
        {
            //是否显示“未上传”
            if (string.IsNullOrWhiteSpace(_docName))
            {
                IsShowDocNotUpload = Visibility.Visible;
            }
            else
            {
                IsShowDocNotUpload = Visibility.Collapsed;
            }

            //是否显示 “上传按钮”
            if (!string.IsNullOrWhiteSpace(_docName))
            {
                IsShowDocUploadBtn = Visibility.Collapsed;
            }
            else
            {
                IsShowDocUploadBtn = Visibility.Visible;
            }

            //是否显示文档名字
            if (string.IsNullOrWhiteSpace(_docName))
            {
                IsShowDoc = Visibility.Collapsed;
            }
            else
            {
                IsShowDoc = Visibility.Visible;
            }
        }

        #endregion

        public bool IsRemoved { get; set; }

        public string oldName { get; set; }

        private string _strNo;

        /// <summary>
        /// 序号
        /// </summary>
        public string strNo
        {
            get { return _strNo; }
            set
            {
                if (_strNo != value)
                {
                    _strNo = value;
                    NotifyPropertyChanged("strNo");
                }
            }
        }

        public MSummary()
        {
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