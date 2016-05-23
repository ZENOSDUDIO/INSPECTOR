using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Honda.View.BaseView
{
    public class BaseWindow : Window
    {
        #region 设置窗口的owner

        public IntPtr ActiveWindowHandle; //定义活动窗体的句柄  

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetActiveWindow(); //获得父窗体句柄 

        #endregion

        public BaseWindow()
        {
        }

        #region 设置窗口的owner

        /// <summary>
        /// 设置窗口的owner
        /// </summary>
        public void SetOwner()
        {
            ActiveWindowHandle = GetActiveWindow(); //获取父窗体句柄  
            WindowInteropHelper helper = new WindowInteropHelper(this);
            helper.Owner = ActiveWindowHandle;
        }

        #endregion

        public ImageSource ReadImage(string imagePath)
        {
            string picturePath = imagePath;
            byte[] buffer = File.ReadAllBytes(picturePath);
            return new ImageSourceConverter().ConvertFrom(buffer) as BitmapSource;
        }
    }
}