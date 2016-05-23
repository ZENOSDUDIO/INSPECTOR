using Honda.Globals;
using Honda.Model;
using Honda.Model.Form;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Honda.View
{
    /// <summary>
    /// RemarksWindows.xaml 的交互逻辑
    /// </summary>
    public partial class TaskWindow : BaseWindow
    {
        public ObservableCollection<MTaskType> TaskTypeList { get; set; }
        //任务
        public MTask _task;

        private List<string> _lstTask;

        public TaskWindow(string storeName)
        {
            InitializeComponent();
            SetOwner();
            _task = new MTask();
            _task.ShopName = storeName;
            _task.Account = DMUser.INSTANCE.CurrentMUser.UserAccount;
            _task.executorId = DMUser.INSTANCE.CurrentMUser.UserAccount;
            _task.shopId = DMStoreTour.INSTANCE.CurrentMStore.shopId;
            tbkStoreName.Text = storeName;
            InitComboBoxData();
        }


        private void InitComboBoxData()
        {
            _lstTask = new List<string>()
            {
                "特约店自评"
            };
            _task.taskType = "1"; //特约店自评
            comboTaskType.ItemsSource = _lstTask;
            comboTaskType.SelectedIndex = 0;
            comboTaskType.SelectionChanged += (s, e) =>
            {
                //任务的类型
                string taskType = (string) comboTaskType.SelectedItem;
                ComboBox com = s as ComboBox;
                if (com == null)
                {
                    return;
                }
                string typeName = com.SelectedItem as string;
                switch (typeName)
                {
                    case "其它任务":
                        _task.taskType = "0";
                        break;
                    case "特约店自评":
                        _task.taskType = "1";
                        break;
                    case "巡回评价":
                        _task.taskType = "2";
                        break;
                }
            };
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _task.TaskName = tbTaskName.Text;
            _task.taskDescription = tbContent.Text;
            if (string.IsNullOrWhiteSpace(_task.taskType) ||
                string.IsNullOrWhiteSpace(_task.TaskName) ||
                string.IsNullOrWhiteSpace(_task.TaskBeginTime) ||
                string.IsNullOrWhiteSpace(_task.TaskEndTime) ||
                string.IsNullOrWhiteSpace(_task.taskDescription)
                )
            {
                MessageBox.Show("请填写完整任务内容！");
            }
            else
            {
                ReleaseTask();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //发布任务
        private void ReleaseTask()
        {
            DMStoreTour.INSTANCE.ReleaseTask(_task, (isSucceed, msg) =>
            {
                Dispatcher.InvokeAsync(() =>
                {
                    if (isSucceed)
                    {
                        MessageBox.Show("发布任务成功！");
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show(msg);
                    }
                });
            });
        }


        //private void comboTaskType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox com = sender as ComboBox;
        //    if(com == null)
        //    {
        //        return;
        //    }
        //     string typeName = com.SelectedItem as string;
        //    switch(typeName)
        //    {
        //        case "其它任务":
        //            _task.taskType = "0";
        //            break;
        //        case "特约店自评":
        //            _task.taskType = "1";
        //            break;
        //        case "巡回评价":
        //            _task.taskType = "2";
        //            break;
        //    }
        //    //_task.taskType
        //}

        private void startTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime dt = (DateTime) startTimePicker.SelectedDate;
            string beginTime = string.Format("{0:yyyy-MM-dd}", dt);
            _task.TaskBeginTime = beginTime;
        }

        private void EndTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime dt = (DateTime) EndTimePicker.SelectedDate;
            string EndTime = string.Format("{0:yyyy-MM-dd}", dt);
            _task.TaskEndTime = EndTime;
        }
    }
}