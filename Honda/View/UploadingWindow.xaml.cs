using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.HttpLib;
using Honda.HttpLib.JsonInputData;
using Honda.Model;
using Honda.Model.Form;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.Collections.Generic;
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
    /*
     * 提交评价表/评价表附件   -by luyonghe
     */

    public partial class UploadingWindow : BaseWindow
    {
        public UploadingWindow()
        {
            InitializeComponent();
        }

        //提交评分表
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if ((bool) cbAccessory.IsChecked) //评分表和提交附件一起提交
            {
                UploadFormScore(true);
            }
            else //只提交评分表
            {
                UploadFormScore();
            }
        }

        /// <summary>
        /// 上传评价表
        /// </summary>
        private void UploadFormScore(bool isFile = false)
        {
            btnSubmit.IsEnabled = false;
            DMPreview.INSTANCE.UploadFormScore((isSucceed, msg) =>
            {
                Dispatcher.InvokeAsync(() =>
                {
                    if (isSucceed)
                    {
                        MessageBox.Show("评分表上传成功");
                        //DMStoreTour.INSTANCE.CurrentMStore._IMPROVE_STATE;

                        //刷新巡回评价管理页面命令
                        GlobalValue.NEED_REFRESH_STORE_TORE = true;

                        Messenger.Default.Send("巡回评价管理", GlobalValue.COMMAND_MAIN_PAGE);

                        if (File.Exists(DirectoryHelper.INSTANCE.STORE_FORM_STATE))
                        {
                            File.Delete(DirectoryHelper.INSTANCE.STORE_FORM_STATE);
                        }
                        this.DialogResult = true;

                        //设置不允许读取离线缓冲数据
                        GlobalValue.Store_Need_Load_Offline_Data_State_Mgr.SetStoreNeedLoadOfflineDataState(
                            DMStoreTour.INSTANCE.CurrentMStore, false);
                        SerialHelp.SerialObject(DirectoryHelper.INSTANCE.STORE_NEED_LOAD_OFFLINE_DATA,
                            GlobalValue.Store_Need_Load_Offline_Data_State_Mgr);
                    }
                    else
                    {
                        MessageBox.Show(msg);
                    }
                    btnSubmit.IsEnabled = true;
                });
            }, isFile);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}