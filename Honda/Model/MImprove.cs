using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Honda.Model
{
    /*
     * 改善计划
     */
    [Serializable]
    public class MImprove : INotifyPropertyChanged
    {
        /// <summary>
        /// 改善计划ID
        /// </summary>
        public string id;

        /// <summary>
        /// 客户端只做保存 提交的时候需要用到此字段。
        /// </summary>
        public string pgrId;

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
                    NotifyPropertyChanged("priority");
                }
            }
        }


        private ObservableCollection<MPriority> _listPriority;

        public ObservableCollection<MPriority> ListPriority
        {
            get
            {
                if (_listPriority == null)
                {
                    _listPriority = new ObservableCollection<MPriority>
                    {
                        new MPriority() {priorityName = "高", priorityId = "1"},
                        new MPriority() {priorityName = "中", priorityId = "2"},
                        new MPriority() {priorityName = "低", priorityId = "3"}
                    };
                }
                return _listPriority;
            }
            set
            {
                if (_listPriority != value)
                {
                    _listPriority = value;
                    NotifyPropertyChanged("ListPriority");
                }
            }
        }

        private MPriority _SelectPriority;

        /// <summary>
        /// 当前选中的项目
        /// </summary>
        public MPriority SelectPriority
        {
            get { return _SelectPriority; }
            set
            {
                if (_SelectPriority != value)
                {
                    _SelectPriority = value;


                    NotifyPropertyChanged("SelectPriority");
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

        private string _finishTime;

        /// <summary>
        /// 完成时间
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
        ///  "张三"//负责人
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

        public string isIgnore = "1"; //是否忽略（(1、忽略2、恢复)）


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///  是否显示忽略按钮
        /// </summary>
        private Visibility _bIsShowIgnore = Visibility.Visible;

        public Visibility bIsShowIgnore
        {
            get { return _bIsShowIgnore; }
            set
            {
                if (_bIsShowIgnore != value)
                {
                    _bIsShowIgnore = value;
                    NotifyPropertyChanged("bIsShowIgnore");
                }
            }
        }

        /// <summary>
        ///  是否显示恢复按钮
        /// </summary>
        private Visibility _bIsShowRecover = Visibility.Collapsed;

        public Visibility bIsShowRecover
        {
            get { return _bIsShowRecover; }
            set
            {
                if (_bIsShowRecover != value)
                {
                    _bIsShowRecover = value;
                    NotifyPropertyChanged("bIsShowRecover");
                }
            }
        }

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

    [Serializable]
    public class MImproveClone
    {
        /// <summary>
        /// 改善计划ID
        /// </summary>
        public string id;

        /// <summary>
        /// 客户端只做保存 提交的时候需要用到此字段。
        /// </summary>
        public string pgrId;

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
                }
            }
        }


        private ObservableCollection<MPriority> _listPriority;

        public ObservableCollection<MPriority> ListPriority
        {
            get
            {
                if (_listPriority == null)
                {
                    _listPriority = new ObservableCollection<MPriority>
                    {
                        new MPriority() {priorityName = "高", priorityId = "1"},
                        new MPriority() {priorityName = "中", priorityId = "2"},
                        new MPriority() {priorityName = "低", priorityId = "3"}
                    };
                }
                return _listPriority;
            }
            set
            {
                if (_listPriority != value)
                {
                    _listPriority = value;
                }
            }
        }

        private MPriority _SelectPriority;

        /// <summary>
        /// 当前选中的项目
        /// </summary>
        public MPriority SelectPriority
        {
            get { return _SelectPriority; }
            set
            {
                if (_SelectPriority != value)
                {
                    _SelectPriority = value;
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
                }
            }
        }

        private string _finishTime;

        /// <summary>
        /// 完成时间
        /// </summary>
        public string finishTime
        {
            get { return _finishTime; }
            set
            {
                if (value != _finishTime)
                {
                    _finishTime = value;
                }
            }
        }

        private string _responsiblePerson;

        /// <summary>
        ///  "张三"//负责人
        /// </summary>
        public string responsiblePerson
        {
            get { return _responsiblePerson; }
            set
            {
                if (value != _responsiblePerson)
                {
                    _responsiblePerson = value;
                }
            }
        }

        public string isIgnore = "1"; //是否忽略（(1、忽略2、恢复)）



        /// <summary>
        ///  是否显示忽略按钮
        /// </summary>
        private Visibility _bIsShowIgnore = Visibility.Visible;

        public Visibility bIsShowIgnore
        {
            get { return _bIsShowIgnore; }
            set
            {
                if (_bIsShowIgnore != value)
                {
                    _bIsShowIgnore = value;
                }
            }
        }

        /// <summary>
        ///  是否显示恢复按钮
        /// </summary>
        private Visibility _bIsShowRecover = Visibility.Collapsed;

        public Visibility bIsShowRecover
        {
            get { return _bIsShowRecover; }
            set
            {
                if (_bIsShowRecover != value)
                {
                    _bIsShowRecover = value;
                }
            }
        }

    }
    //级别
    [Serializable]
    public class MPriority
    {
        public string priorityId { get; set; }
        public string priorityName { get; set; }
    }
}