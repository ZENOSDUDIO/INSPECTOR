using System;
using System.Collections.Generic;
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
using AForge.Video.DirectShow;

namespace Honda
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private FilterInfoCollection videoDevices;

        public Window1()
        {
            InitializeComponent();
            Loaded += Window1_Loaded;
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count > 0)
            {
                AFGMyCamera.VideoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                AFGMyCamera.Start();
                if (videoDevices.Count > 1)
                {
                    AFGMyCamera2.VideoSource = new VideoCaptureDevice(videoDevices[1].MonikerString);
                    AFGMyCamera2.Start();
                }
            }
        }
    }
}