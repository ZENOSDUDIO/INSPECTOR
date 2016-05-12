using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Model;
using Honda.View;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Honda.ViewModel
{
    /// <summary>
    /// 日程管理ViewModel
    /// </summary>
    public class ScheduleManageVM : ViewModelBase
    {
        public ScheduleManagePage thisPage;

        /// <summary>
        /// 任务清单  RaisePropertyChanged("_bIsShowGroup");
        /// </summary>
        private ObservableCollection<MTask> _listTask = new ObservableCollection<MTask>();

        public ObservableCollection<MTask> listTask
        {
            get { return _listTask; }
            set
            {
                if (_listTask != value)
                {
                    _listTask = value;
                    RaisePropertyChanged("listTask");
                }
            }
        }

        /// <summary>
        /// 已过期总数
        /// </summary>
        private string _haveExpiredTotal;

        /// <summary>
        /// 已过期总数
        /// </summary>
        public string haveExpiredTotal
        {
            get { return _haveExpiredTotal; }
            set
            {
                if (_haveExpiredTotal != value)
                {
                    _haveExpiredTotal = value;
                    RaisePropertyChanged("haveExpiredTotal");
                }
            }
        }

        private string _willExpireTotal;

        /// <summary>
        /// 即将过期总数
        /// </summary>
        public string willExpireTotal
        {
            get { return _willExpireTotal; }
            set
            {
                if (_willExpireTotal != value)
                {
                    _willExpireTotal = value;
                    RaisePropertyChanged("willExpireTotal");
                }
            }
        }

        public ScheduleManageVM()
        {
        }

        #region cmd

        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        DMScheduleManage.INSTANCE.GetTaskList((isSuccess, msg) =>
                        {
                            if (isSuccess)
                            {
                                listTask = DMScheduleManage.INSTANCE._listTask;
                                haveExpiredTotal = DMScheduleManage.INSTANCE.haveExpiredTotal;
                                willExpireTotal = DMScheduleManage.INSTANCE.willExpireTotal;
                            }
                        });
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
        }

        public RelayCommand<string> ViewTaskCommand
        {
            get
            {
                return new RelayCommand<string>((taskID) =>
                {
                    try
                    {
                        thisPage._web.InvokeScript("ShowTask", taskID);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
        }

        #endregion
    }
}