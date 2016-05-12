using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.IO;
using Microsoft.Win32;
using System.Windows.Ink;
using Honda.Globals;
using Honda.View.BaseView;

namespace Honda.View
{
    /// <summary>
    /// SignatureWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SignatureWindow : BaseWindow
    {
        #region var 、contraction 、 load Fun

        /// <summary>
        /// 是否完成了签名
        /// </summary>
        public bool IsComplate { get; set; }

        public string pictruePath { get; set; }

        /// <summary>
        /// 从本地中获取的图片
        /// </summary>
        public string locationPictruePath { get; set; }


        public SignatureWindow()
        {
            InitializeComponent();
            this.Loaded += SignatureWindow_Loaded;

            //设置窗口的owner
            SetOwner();
        }

        #endregion

        private void SignatureWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public void ShowLocationPictrue()
        {
            try
            {
                if (File.Exists(pictruePath))
                {
                    imaDra.Source = ReadImage(pictruePath);
                    gdImaSelect.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SignatureWindow类中的\nShowLocationPictrue()方法出错\n" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            locationPictruePath = null;
            this.inkCanv.Strokes.Clear();
            ShowHidenHint(Visibility.Visible);
            gdImaSelect.Visibility = System.Windows.Visibility.Collapsed;
            imaDra.Source = null;
        }

        /// <summary>
        /// 从本地中选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = "(*.bmp)|*.bmp";


            if ((bool) dlg.ShowDialog(this))
            {
                this.inkCanv.Strokes.Clear();

                try
                {
                    if (!dlg.FileName.ToLower().EndsWith(".bmp"))
                    {
                        MessageBox.Show("图片格式错误", Title);
                    }
                    else
                    {
                        byte[] buffer = File.ReadAllBytes(dlg.FileName);
                        imaDra.Source = new ImageSourceConverter().ConvertFrom(buffer) as BitmapSource;
                        gdImaSelect.Visibility = System.Windows.Visibility.Visible;
                        locationPictruePath = dlg.FileName;
                    }
                    ShowHidenHint(Visibility.Collapsed);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SignatureWindow类中的\nbtnSelectPicture_Click()方法出错\n" + ex.Message);
                }
            }
        }

        #region 当鼠标进入签名区的时候，提示模块隐藏，出签名区的时候根据情况显示或者隐藏

        private void inkCanv_MouseEnter(object sender, MouseEventArgs e)
        {
            ShowHidenHint(Visibility.Collapsed);
        }

        private void inkCanv_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.inkCanv.Strokes.Count <= 0)
                ShowHidenHint(Visibility.Visible);
        }

        /// <summary>
        /// 隐藏或者显示提示语
        /// </summary>
        /// <param name="vis"></param>
        private void ShowHidenHint(Visibility vis)
        {
            gdHint.Visibility = vis;
        }

        #endregion

        #region 保存图片

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (pictruePath == null) return;

            if (!Directory.Exists(System.IO.Path.GetDirectoryName(pictruePath)))
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(pictruePath));
            }
            if (this.inkCanv.Strokes.Count > 0)
            {
                SavePictrueFromApp();
            }
            else if (locationPictruePath != null)
            {
                SavePictrueFromLoaction();
            }
            else if (imaDra.Source != null)
            {
                MessageBox.Show("此签名图片已经保存");
                return;
            }
            else
            {
                MessageBox.Show("请完成签名后再按保存");
                return;
            }
            this.Close();
        }

        /// <summary>
        /// 当保存的是从本地选取的图片而不是从涂鸦板画出来的图片的时候
        /// </summary>
        private void SavePictrueFromLoaction()
        {
            if (locationPictruePath != null)
            {
                try
                {
                    File.Copy(locationPictruePath, pictruePath, true);
                    IsComplate = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SignatureWindow类中的\nSavePictrueFromLoaction()方法出错\n" + ex.Message);
                    IsComplate = false;
                }
            }
        }

        /// <summary>
        /// 当保存的是从涂鸦板画出来的图片的时候
        /// </summary>
        private void SavePictrueFromApp()
        {
            //InkManager sss = 
            try
            {
                using (FileStream file = new FileStream(pictruePath,
                    FileMode.Create, FileAccess.Write))
                {
                    int marg = int.Parse(this.inkCanv.Margin.Left.ToString());
                    RenderTargetBitmap rtb = new RenderTargetBitmap((int) this.inkCanv.ActualWidth - marg,
                        (int) this.inkCanv.ActualHeight - marg, 0, 0, PixelFormats.Default);
                    rtb.Render(this.inkCanv);

                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    encoder.Save(file);
                }

                IsComplate = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("SignatureWindow类中的\nSavePictrueFromApp()方法出错\n" + exc.Message);
                IsComplate = false;
            }
        }

        #endregion
    }
}