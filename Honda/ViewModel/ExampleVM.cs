using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Honda.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace Honda.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ExampleVM : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the ExampleVM class.
        /// </summary>
        public ExampleVM()
        {
            _Test = "欢迎使用";
        }

        public string _Test { get; set; }

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
    }
}