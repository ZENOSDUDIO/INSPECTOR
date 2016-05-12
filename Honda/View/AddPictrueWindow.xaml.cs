using Honda.Globals;
using Honda.Model.Form;
using Honda.View.BaseView;
using Honda.ViewModel;
using Microsoft.Win32;
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
    /// <summary>
    /// RemarksWindows.xaml 的交互逻辑
    /// </summary>
    public partial class AddPictrueWindow : BaseWindow
    {
        /// <summary>
        /// 选中的图片地址
        /// </summary>
        public string pictruePath;

        public AddPictrueWindow()
        {
            InitializeComponent();
            SetOwner();
        }

        #region Event

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        //打开本地选取相片
        private void AddLocationPictrue_Click(object sender, RoutedEventArgs e)
        {
            double left = this.Left;
            this.Left = -1800;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = "All files (*.*)|*.*|" + "(*.bmp)|*.bmp|" + "(*.jpeg)|*.jpeg|" + "(*.png)|*.png|" +
                         "(*.jpg)|*.jpg";

            if ((bool) dlg.ShowDialog(this))
            {
                try
                {
                    if (dlg.FileName.ToLower().EndsWith(".bmp") ||
                        dlg.FileName.ToLower().EndsWith(".jpg") ||
                        dlg.FileName.ToLower().EndsWith(".jpeg") ||
                        dlg.FileName.ToLower().EndsWith(".png"))
                    {
                        string path = dlg.FileName;
                        pictruePath = path;
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("图片格式错误", Title);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("PictrueWindows类中的\btnLocationPictrue_Click()方法出错\n" + ex.Message);
                }
            }

            this.Left = left;
        }


        //启动相机
        private void btnCamera_Click(object sender, RoutedEventArgs e)
        {
            double oldLeft = this.Left;
            this.Left = -1800;
            CameraWindow cameraWindow = new CameraWindow();
            if ((bool) cameraWindow.ShowDialog())
            {
                try
                {
                    byte[] CaptureData = cameraWindow.CaptureData;
                    string pictrueName = DirectoryHelper.INSTANCE.newPictrueName;
                    File.WriteAllBytes(pictrueName, CaptureData);
                    pictruePath = pictrueName;
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("btnCamera_Click图片保存失败\n" + ex.Message);
                }
            }
            this.Left = oldLeft;
        }
    }
}