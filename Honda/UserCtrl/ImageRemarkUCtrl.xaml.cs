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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Honda.UserCtrl
{
    /// <summary>
    /// ImageRemarkUCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageRemarkUCtrl : UserControl
    {
        private Action<string> action;

        private bool _isReadOnly = false;
        //是否是只读
        public bool _IsReadOnly
        {
            get { return _isReadOnly; }

            set
            {
                _isReadOnly = value;
                if (_isReadOnly)
                {
                    gdClose.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    gdClose.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// 图片保存路径
        /// </summary>
        private string imagePath;

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    image.Source = ReadImage(imagePath);
                }
            }
        }

        public ImageRemarkUCtrl()
        {
            InitializeComponent();
            _IsReadOnly = false;
        }

        public ImageSource ReadImage(string imagePath)
        {
            string picturePath = imagePath;
            byte[] buffer = File.ReadAllBytes(picturePath);
            return new ImageSourceConverter().ConvertFrom(buffer) as BitmapSource;
        }

        /// <summary>
        /// 删除该图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemovePictrue_MouseUp(object sender, MouseButtonEventArgs e)
        {
            image.Source = null;
            this.Visibility = Visibility.Collapsed;

            if (action != null)
            {
                action(ImagePath);
            }
        }

        public void SetDeletAction(Action<string> action)
        {
            this.action = action;
        }
    }
}