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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Honda.UserCtrl
{
    /// <summary>
    /// ImageUCtrl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageUCtrl : UserControl
    {
        /// <summary>
        /// 当选择图片时为true，不选择时候为false
        /// </summary>
        private Action<bool, string> chekImaAction;

        private bool isCanCheck = false;

        public bool isCheck { get; set; }

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
                    BitmapImage bitmap = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                    image.Source = bitmap;
                }
            }
        }

        public ImageUCtrl()
        {
            InitializeComponent();
        }

        //设置回调，当选中状态发生改变时，回调
        public void SetCheckAction(Action<bool, string> action)
        {
            chekImaAction = action;
            this.Loaded += ImageUCtrl_Loaded;
        }


        private void ImageUCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            if (isCheck)
            {
                chekIma.IsChecked = isCheck;
            }
        }

        /// <summary>
        /// 外部该类发送的通知(当外部已经包含有超多三张图片的时候，该控件就置为不可选中状态)
        /// </summary>
        /// <param name="count"></param>
        public void Notificatied(int count)
        {
            if (count >= 3)
            {
                isCanCheck = false;
            }
            else
            {
                isCanCheck = true;
            }
        }

        #region Event

        private void chekIma_Checked(object sender, RoutedEventArgs e)
        {
            if (!isCanCheck)
            {
                chekIma.IsChecked = false;
                MessageBox.Show("只能选择三张图片");
                return;
            }

            if (chekImaAction != null)
            {
                chekImaAction(true, ImagePath);
            }
        }

        private void chekIma_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chekImaAction != null)
            {
                chekImaAction(false, ImagePath);
            }
        }

        #endregion
    }
}