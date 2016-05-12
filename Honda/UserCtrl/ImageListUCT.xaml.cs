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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Honda.UserCtrl
{
    /// <summary>
    /// ImageListUCT.xaml 的交互逻辑
    /// </summary>
    public partial class ImageListUCT : UserControl
    {
        #region var 

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
                    btnAdd.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    btnAdd.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private List<string> imageList;

        public List<string> ImageList
        {
            get { return imageList; }
            set
            {
                imageList = value;
                loadImage();
            }
        }

        private Action addAction;

        #endregion

        public ImageListUCT()
        {
            InitializeComponent();
            _IsReadOnly = false;
        }

        #region 加载图片

        private void loadImage()
        {
            spImagelist.Children.Clear();
            if (imageList == null) return;
            for (int i = 0; i < imageList.Count; i++)
            {
                if (File.Exists(imageList[i]))
                {
                    ImageRemarkUCtrl imageUC = new ImageRemarkUCtrl();
                    string filePath = imageList[i];
                    imageUC.ImagePath = filePath;
                    Thickness margin = new Thickness(0, 0, 20, 0);
                    imageUC.Margin = margin;
                    spImagelist.Children.Add(imageUC);
                    if (!_isReadOnly)
                    {
                        imageUC.SetDeletAction((pictruePath) =>
                        {
                            if (imageList.Contains(pictruePath))
                            {
                                imageList.Remove(pictruePath);
                                loadImage();
                            }
                        });
                    }
                    imageUC._IsReadOnly = _IsReadOnly;


                    imageUC.MouseUp += (o, e) =>
                    {
                        if (imageList.Contains(filePath))
                        {
                            FileHelp.OpenFile(filePath);
                        }
                    };
                }
            }

            if (_isReadOnly)
            {
                return;
            }
            HideOrShowAddBtn();
        }

        /// <summary>
        /// 当有三张图片后就隐藏添加图片的按钮
        /// </summary>
        private void HideOrShowAddBtn()
        {
            if (imageList == null)
            {
                btnAdd.Visibility = System.Windows.Visibility.Visible;
            }
            else if (imageList.Count == 3)
            {
                btnAdd.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                btnAdd.Visibility = System.Windows.Visibility.Visible;
            }
        }

        #endregion

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (addAction != null)
            {
                addAction();
            }
        }

        public void SetAddAtion(Action action)
        {
            addAction = action;
        }
    }
}