using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model
{
    /// <summary>
    /// 改善计划审核
    /// </summary>
    [Serializable]
    public class MImproveCheck : INotifyPropertyChanged
    {
        /// <summary>
        /// ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// middleid  
        /// </summary>
        public string middleItemId { get; set; }

        /// <summary>
        /// minItemID
        /// </summary>
        public string minItemID { get; set; }

        /// <summary>
        /// smallItemID
        /// </summary>
        public string smallItemID { get; set; }


        /// <summary>
        /// 细项名称 第四列
        /// </summary>
        public string minName { get; set; }

        /// <summary>
        /// 小项名称 第三列
        /// </summary>
        public string smallName { get; set; }

        /// <summary>
        /// 中项名称 第二列
        /// </summary>
        public string middName { get; set; }

        /// <summary>
        /// 中项名称 第一列
        /// </summary>
        public string strNo { get; set; }

        /// <summary>
        /// 优先级(1：高，2：中，3：低)
        /// </summary>
        public string IntPriority
        {
            set
            {
                switch (value)
                {
                    case "1":
                        priority = "高";
                        break;
                    case "2":
                        priority = "中";
                        break;
                    case "3":
                        priority = "低";
                        break;
                }
            }
        }

        private string _priority;

        /// <summary>
        /// 优先级(1：高，2：中，3：低)
        /// </summary>
        public string priority
        {
            get { return _priority; }
            set
            {
                if (value != _priority)
                {
                    _priority = value;
                    if (_priority == "1")
                    {
                        priorityName = "高";
                    }
                    else if (_priority == "2")
                    {
                        priorityName = "中";
                    }
                    else if (_priority == "3")
                    {
                        priorityName = "低";
                    }
                    NotifyPropertyChanged("priority");
                }
            }
        }

        private string _priorityName;

        /// <summary>
        /// 优先级(1：高，2：中，3：低)
        /// </summary>
        public string priorityName
        {
            get { return _priorityName; }
            set
            {
                if (value != _priorityName)
                {
                    _priorityName = value;
                    NotifyPropertyChanged("priorityName");
                }
            }
        }

        private string _description;

        /// <summary>
        /// 问题描述
        /// </summary>
        public string description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    NotifyPropertyChanged("description");
                }
            }
        }

        private string _mprovementMeasure;

        /// <summary>
        /// 改善措施
        /// </summary>
        public string mprovementMeasure
        {
            get { return _mprovementMeasure; }
            set
            {
                if (value != _mprovementMeasure)
                {
                    _mprovementMeasure = value;
                    NotifyPropertyChanged("mprovementMeasure");
                }
            }
        }

        private string _finishTime;

        /// <summary>
        /// 截止时间
        /// </summary>
        public string finishTime
        {
            get { return _finishTime; }
            set
            {
                if (value != _finishTime)
                {
                    _finishTime = value;
                    NotifyPropertyChanged("finishTime");
                }
            }
        }

        private string _responsiblePerson;

        /// <summary>
        ///  责任人
        /// </summary>
        public string responsiblePerson
        {
            get { return _responsiblePerson; }
            set
            {
                if (value != _responsiblePerson)
                {
                    _responsiblePerson = value;
                    NotifyPropertyChanged("responsiblePerson");
                }
            }
        }

        /// <summary>
        /// 状态
        /// 总共有五种  2待审核、 3审核、4不通过、1提交、0未提交
        /// 只有“待审核”的状态时，UI后面的操作区域才可用
        /// </summary>
        private string _status = "2";

        public string status
        {
            get { return _status; }
            set
            {
                _status = value;
                switch (_status)
                {
                    case "0":
                        statusName = "未提交";
                        break;
                    case "1":
                        statusName = "提交";
                        break;
                    case "2":
                        statusName = "待审核";
                        break;
                    case "3":
                        statusName = "审核";
                        break;
                    case "4":
                        statusName = "不通过";
                        break;
                }
            }
        }

        /// <summary>
        /// 状态
        /// 总共有五种  2待审核、 3审核、4不通过、1提交、0未提交
        /// 只有“待审核”的状态时，UI后面的操作区域才可用
        /// </summary>
        private string _statusName = "待审核";

        public string statusName
        {
            get { return _statusName; }
            set
            {
                if (_statusName != value)
                {
                    _statusName = value;
                    NotifyPropertyChanged("statusName");
                }
            }
        }

        /// <summary>
        /// 证据
        /// </summary>
        public ObservableCollection<MFileData> Attachment = new ObservableCollection<MFileData>();


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