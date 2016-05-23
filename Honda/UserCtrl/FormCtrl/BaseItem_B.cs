using Honda.Globals;
using Honda.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Honda.UserCtrl
{
    public abstract class BaseItem_B : Grid
    {
        #region Var

        /// <summary>
        /// 是否是详情
        /// </summary>
        public bool isDetail;

        /// <summary>
        /// 需要划分成的列数
        /// </summary>
        private int CountColumn;

        public List<Border> listBorder;

        public double m_high = GlobalValue.FORM_ITEM_HIGH;

        /// <summary>
        /// 高度是否自动适应
        /// </summary>
        public bool bIsAutoHigh = false;

        /// <summary>
        /// 字体颜色
        /// </summary>
        public SolidColorBrush ContenForeground = (SolidColorBrush) Application.Current.Resources["FormForegroundBrush"];

        /// <summary>
        /// 字体颜色
        /// </summary>
        public SolidColorBrush ContenForeground1 =
            (SolidColorBrush) Application.Current.Resources["FormForegroundBrush"];


        /// <summary>
        /// 表格边框的颜色
        /// </summary>
        public SolidColorBrush borderBrush = (SolidColorBrush) Application.Current.Resources["FormBorderBrush1"];

        /// <summary>
        /// 表格的背景颜色
        /// </summary>
        public SolidColorBrush borderBackground = (SolidColorBrush) Application.Current.Resources["FormBackgroundBrush"];

        /// <summary>
        /// RED BRUSH
        /// </summary>
        public SolidColorBrush redBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        /// <summary>
        /// WHITE BRUSH
        /// </summary>
        public SolidColorBrush whiteBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="countColumn">Grid将要分成的列数</param>
        public BaseItem_B(int countColumn)
        {
            CountColumn = countColumn;
            this.Loaded += BaseItem_B_Loaded;
        }

        private void BaseItem_B_Loaded(object sender, RoutedEventArgs e)
        {
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            if (!bIsAutoHigh)
            {
                this.Height = m_high;
            }
            this.MaxHeight = 200;
            InitControl();
            SetControlEvent();
            InitBorder();
            SetGridColum();
        }

        #region 初始化Border

        /// <summary>
        /// 初始化Border
        /// </summary>
        private void InitBorder()
        {
            listBorder = new List<Border>();
            for (int i = 0; i < CountColumn; i++)
            {
                Border bd = new Border();
                bd.BorderThickness = new Thickness(1, 1, 1, 1);
                bd.BorderBrush = borderBrush;

                bd.Background = borderBackground;
                bd.SetValue(Grid.ColumnProperty, i);
                bd.SetValue(Grid.RowProperty, 0);
                listBorder.Add(bd);
            }

            AddControlIntoBorder();
        }

        #endregion

        #region 设置列数

        /// <summary>
        /// 设置列数
        /// </summary>
        private void SetGridColum()
        {
            for (int i = 0; i < CountColumn; i++)
            {
                VerticalAlignment = VerticalAlignment.Stretch;
                ColumnDefinition rd = new ColumnDefinition();
                rd.Width = SetColumnWith(i);
                ColumnDefinitions.Add(rd);
                Children.Add(listBorder[i]);
            }
        }

        #endregion

        /// <summary>
        /// 设置TextBlok控件的样式
        /// </summary>
        /// <param name="tbk">需要设置的控件</param>
        /// <param name="content">该控件的文本</param>
        /// <param name="horizontalAlignment">该控件水平对齐的方式</param>
        protected virtual void SetTextBlokStyle(TextBlock tbk, string content,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left)
        {
            tbk.VerticalAlignment = VerticalAlignment.Center;
            tbk.HorizontalAlignment = horizontalAlignment;
            tbk.Margin = new Thickness(10, 0, 10, 0);
            tbk.FontSize = 20;
            tbk.Foreground = ContenForeground;
            tbk.TextWrapping = TextWrapping.Wrap;
            tbk.Text = content;
            tbk.Margin = new Thickness(8);
        }

        /// <summary>
        /// 设置图片
        /// </summary>
        /// <param name="ima"></param>
        /// <param name="imaUri"></param>
        protected virtual void SetImageStyle(Image ima, string imaUri)
        {
            ima.VerticalAlignment = VerticalAlignment.Stretch;
            ima.HorizontalAlignment = HorizontalAlignment.Stretch;
            ima.Margin = new Thickness(0, 8, 0, 8);
            ima.Stretch = Stretch.None;
            ima.Source = new BitmapImage(new Uri(imaUri, UriKind.RelativeOrAbsolute));
        }

        protected void SetTextBoxStyle(TextBox tb, string content)
        {
            tb.Text = content;
            tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            tb.FontSize = 20;
            tb.Foreground = ContenForeground1;
            tb.Width = 150;
            tb.Margin = new Thickness(5, 0, 5, 0);
            tb.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        protected abstract void InitControl();

        /// <summary>
        /// 给控件设置事件
        /// </summary>
        protected abstract void SetControlEvent();

        /// <summary>
        /// 把控件放入Border内
        /// </summary>
        protected abstract void AddControlIntoBorder();

        /// <summary>
        /// 设置每一列的宽度
        /// </summary>
        /// <param name="iCount"></param>
        /// <returns></returns>
        protected abstract GridLength SetColumnWith(int iCount);
    }
}