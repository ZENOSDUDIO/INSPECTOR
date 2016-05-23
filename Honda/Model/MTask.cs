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
    public class MTask : INotifyPropertyChanged
    {
        private const string TASK_STATUS_OUT_DATE = "已过期";

        private const string TASK_STATUS_WILL_OUT_DATE = "即将过期";


        /// <summary>
        ///  是否显示已过期
        /// </summary>
        private Visibility bIsShowOutData = Visibility.Collapsed;

        public Visibility _bIsShowOutData
        {
            get { return bIsShowOutData; }
            set
            {
                if (bIsShowOutData != value)
                {
                    bIsShowOutData = value;
                    NotifyPropertyChanged("_bIsShowOutData");
                }
            }
        }

        /// <summary>
        ///  是否显示未过期
        /// </summary>
        private Visibility bIsShowWillOutData = Visibility.Collapsed;

        public Visibility _bIsShowWillOutData
        {
            get { return bIsShowWillOutData; }
            set
            {
                if (bIsShowWillOutData != value)
                {
                    bIsShowWillOutData = value;
                    NotifyPropertyChanged("_bIsShowWillOutData");
                }
            }
        }

        /// <summary>
        /// 登录账号，ID
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        public string TaskID { get; set; }

        private string _TaskName;

        /// <summary>
        /// 任务名
        /// </summary>
        public string TaskName
        {
            get { return _TaskName; }
            set
            {
                if (_TaskName != value)
                {
                    _TaskName = value;
                    NotifyPropertyChanged("TaskName");
                }
            }
        }

        /// <summary>
        /// 任务状态
        /// </summary>
        public ENUM_TASK enum_task { get; set; }


        private string _taskStatus;

        /// <summary>
        /// 任务状态
        /// </summary>
        public string TaskStatus
        {
            get { return _taskStatus; }
            set
            {
                if (_taskStatus != value)
                {
                    _taskStatus = value;
                    NotifyPropertyChanged("TaskStatus");
                }
            }
        }

        /// <summary>
        /// 任务开始时间
        /// </summary>
        public string TaskBeginTime { get; set; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        public string TaskEndTime { get; set; }

        public string _creatorId = "";

        /// <summary>
        /// 任务创建人ID
        /// </summary>
        public string creatorId
        {
            get { return _creatorId; }
            set
            {
                if (_creatorId != value)
                {
                    _creatorId = value;
                }
            }
        }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string taskType { get; set; }

        public MTaskType TaskType;

        /// <summary>
        /// 执行该评价任务的巡回员Id
        /// </summary>
        public string executorId { get; set; }

        /// <summary>
        /// 执行该任务的特约店ID
        /// </summary>
        public string shopId { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string taskDescription { get; set; }

        /// <summary>
        /// 店名字
        /// </summary>
        public string ShopName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TaskName">任务名</param>
        /// <param name="enum_task">任务状态</param>
        /// <param name="TaskBeginTime">任务开始时间</param>
        /// <param name="TaskEndTime">任务结束时间</param>
        public MTask(string TaskName, ENUM_TASK enum_task, string TaskBeginTime, string TaskEndTime)
        {
            this.TaskName = TaskName;
            this.enum_task = enum_task;
            this.TaskBeginTime = TaskBeginTime;
            this.TaskEndTime = TaskEndTime;
            setTaskStatusIcoAndForeground();
        }

        public MTask(string TaskName, ENUM_TASK enum_task)
        {
            this.TaskName = TaskName;
            this.enum_task = enum_task;
            setTaskStatusIcoAndForeground();
        }

        public MTask()
        {
        }

        /// <summary>
        /// 根据任务状态设置，任务清单的字体和颜色和图标
        /// </summary>
        private void setTaskStatusIcoAndForeground()
        {
            if (enum_task == ENUM_TASK.outDate)
            {
                TaskStatus = TASK_STATUS_OUT_DATE;

                _bIsShowOutData = Visibility.Visible;
                _bIsShowWillOutData = Visibility.Collapsed;
            }
            else if (enum_task == ENUM_TASK.willOutDate)
            {
                TaskStatus = TASK_STATUS_WILL_OUT_DATE;

                _bIsShowOutData = Visibility.Collapsed;
                _bIsShowWillOutData = Visibility.Visible;
            }
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

    public class MTaskType
    {
        public string taskTypeId { get; set; }
        public string taskTypeName { get; set; }
    }

    public enum ENUM_TASK
    {
        /// <summary>
        /// 已过期
        /// </summary>
        outDate = 0,

        /// <summary>
        /// 即将过期
        /// </summary>
        willOutDate = 1
    }
}