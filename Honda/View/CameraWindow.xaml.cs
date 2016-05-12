using AForge.Video;
using AForge.Video.DirectShow;
using Honda.Globals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace Honda.View
{
    /// <summary>
    /// CameraWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CameraWindow : Window
    {
        //定义一个用于传到UI照片数据的属性
        public byte[] CaptureData { get; set; }

        //拍照
        private BitmapSource ImageCapture;

        // 设定初始视频设备
        private FilterInfoCollection videoDevices;

        /// <summary>
        /// 当前启动的相机
        /// </summary>
        private int indexCurrentCamera = 0;

        public CameraWindow()
        {
            InitializeComponent();
            this.Loaded += CameraWindow_Loaded;
        }

        private void CameraWindow_Loaded(object sender, RoutedEventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                // 默认设备
                sourcePlayer.VideoSource = new VideoCaptureDevice(videoDevices[indexCurrentCamera].MonikerString);
                sourcePlayer.Start();
            }
            else
            {
                MessageBox.Show("您的电脑没有检测到摄像头");
                this.Close();
            }
        }


        //保存相片
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ImageCapture != null)
            {
                RenderTargetBitmap bmp = new RenderTargetBitmap((int) imaShow.Width, (int) imaShow.Height, 96, 96,
                    PixelFormats.Default);
                imaShow.Measure(imaShow.RenderSize);
                imaShow.Arrange(new Rect(imaShow.RenderSize));
                bmp.Render(imaShow);
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    CaptureData = ms.ToArray(); //将形成的照片留传递给属性
                    ms.Dispose();
                }
            }

            if (CaptureData == null || CaptureData.Length <= 0)
            {
                MessageBox.Show("请拍照后再点击保存");
                return;
            }
            else
            {
                if (sourcePlayer.IsRunning)
                {
                    sourcePlayer.SignalToStop();
                    sourcePlayer.WaitForStop();
                }
                this.DialogResult = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (sourcePlayer.IsRunning)
            {
                sourcePlayer.SignalToStop();
                sourcePlayer.WaitForStop();
                this.Close();
            }
            else
            {
                ImageCapture = null;
                imaShow.Source = null;
                VideoWFH.Visibility = System.Windows.Visibility.Collapsed;
                sourcePlayer.Start();
            }
        }

        //拍照
        private void btnTakePhotos_Click(object sender, RoutedEventArgs e)
        {
            ImageCapture = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                sourcePlayer.GetCurrentVideoFrame().GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            imaShow.Source = ImageCapture;
            VideoWFH.Visibility = System.Windows.Visibility.Collapsed;
        }

        #region 切换摄像机镜头

        //切换摄像机镜头
        private void btnSwitchCamera_Click(object sender, RoutedEventArgs e)
        {
            if (sourcePlayer.IsRunning)
            {
                sourcePlayer.SignalToStop();
                sourcePlayer.WaitForStop();
            }

            if (videoDevices.Count == 2)
            {
                if (indexCurrentCamera == 0)
                {
                    indexCurrentCamera = 1;
                }
                else
                {
                    indexCurrentCamera = 0;
                }
                sourcePlayer.VideoSource = new VideoCaptureDevice(videoDevices[1].MonikerString);
                sourcePlayer.Start();
            }
        }

        /// <summary>
        /// 显示拍照和转换按钮
        /// </summary>
        /// <param name="isShow"></param>
        private void ShowButton(bool isShow)
        {
            if (isShow && sourcePlayer.IsRunning)
            {
                btnSwitchCamera.Visibility = System.Windows.Visibility.Visible;
                btnTakePhotos.Visibility = System.Windows.Visibility.Visible;
                if (videoDevices.Count >= 2)
                {
                    btnSwitchCamera.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    btnSwitchCamera.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            else
            {
                btnSwitchCamera.Visibility = System.Windows.Visibility.Collapsed;
                btnTakePhotos.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        #endregion
    }
}