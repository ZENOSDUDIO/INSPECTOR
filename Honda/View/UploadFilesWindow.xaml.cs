using Honda.Globals;
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

    public partial class UploadFilesWindow : BaseWindow
    {
        public UploadFilesWindow()
        {
            InitializeComponent();
        }

        //提交附件
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(DirectoryHelper.INSTANCE.ALL_UPLOAD_FILE_DATA)) return;

            EvaluationForUpload eva =
                (EvaluationForUpload) SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.ALL_UPLOAD_FILE_DATA);
            DMPreview.INSTANCE.UploadAttachment(eva, (isSucceed, msg) =>
            {
                Dispatcher.InvokeAsync(() =>
                {
                    if (isSucceed)
                    {
                        MessageBox.Show("上传附件成功");
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("上传附件失败");
                    }
                });
            });
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}