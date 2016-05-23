using Honda.Globals;
using Honda.Interface;
using Honda.Model.Form;
using Honda.View;
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
    public abstract class BaseItem_C : Grid
    {
        #region  Var 、construction和Load

        /// <summary>
        /// 是否是详情
        /// </summary>
        public bool isDetail;

        private Action _action_score;
        private Action _action_GoForem;

        /// <summary>
        /// 备注
        /// </summary>
        private MRemarks _rmarks;

        //是否已经评价了
        private bool isAlreadyEvaluate;

        /// <summary>
        /// 巡回评价是否合格
        /// </summary>
        public bool m_bIsQualified;

        /// <summary>
        /// 是否通过上次评价结果
        /// </summary>
        public bool m_bIsPassLastEvaluationResults;

        /// <summary>
        /// 是否通过特约店自评结果
        /// </summary>
        public bool m_bIsPassSinceEvaluationResult;

        #region 控件 & 布局所用 Var

        public Grid gdPanel1;
        public Grid gdPanel2;

        /// <summary>
        /// gdPanel1的水平宽度所占总长度的比重
        /// </summary>
        private double _dWidthRatioGdPanel1 = 27;

        /// <summary>
        /// gdPanel2的水平宽度所占总长度的比重
        /// </summary>
        private double _dWidthRatioGdPanel2 = 12;

        /// <summary>
        /// gdPanel2里子控件数量
        /// </summary>
        private static int iCountOfgdPanel2ChildControl = 2;

        /// <summary>
        /// gdPanel2的边框
        /// </summary>
        private List<Border> _lstBorder;


        /// <summary>
        /// gdPanel1的边框
        /// </summary>
        public List<Border> listBorder;

        /// <summary>
        /// gdPanel1需要划分成的列数
        /// </summary>
        private int CountColumn;

        /// <summary>
        /// 高度是否自动适应
        /// </summary>
        public bool bIsAutoHigh = false;

        public double m_high = GlobalValue.FORM_ITEM_HIGH;

        #endregion

        #region 颜色 Var

        /// <summary>
        /// 字体颜色   
        /// </summary>
        public SolidColorBrush ContenForeground = (SolidColorBrush)Application.Current.Resources["FormForegroundBrush"];

        /// <summary>
        /// 表格边框的颜色    
        /// </summary>
        public SolidColorBrush borderBrush = (SolidColorBrush)Application.Current.Resources["FormBorderBrush1"];

        /// <summary>
        /// 表格的背景颜色
        /// </summary>
        public SolidColorBrush borderBackground = (SolidColorBrush)Application.Current.Resources["FormBackgroundBrush"];

        /// <summary>
        /// 单元格红色背景颜色
        /// </summary>
        public SolidColorBrush redCellBackground = (SolidColorBrush)Application.Current.Resources["RedBackgroundBrush"];

        /// <summary>
        /// 表格的高亮背景颜色
        /// </summary>
        public SolidColorBrush borderHighBackground =
            (SolidColorBrush)Application.Current.Resources["FormBackgroundHighBrush"];

        #endregion

        #region  控件 & 地址 Var

        private readonly string strPassImaUri = "/Assets/page_icons1_1.png";
        private readonly string strCrossImaUri = "/Assets/page_icons1.png";

        private Image imgSinceEvaluationResult;
        private Image imgQualified;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="countColumn">Grid将要分成的列数</param>
        /// <param name="bIsPassLastEvaluationResults">上次评价是否合格true为合格，false为不合格</param>
        /// <param name="bIsPassSinceEvaluationResult">自评是否合格true为合格，false为不合格</param>
        /// <param name="bIsQualified">巡回评价是否合格</param>
        /// <param name="isAlreadyEvaluate">是否已经评价了</param>
        /// <param name="marks"></param>
        public BaseItem_C(int countColumn, bool bIsPassLastEvaluationResults,
            bool bIsPassSinceEvaluationResult, bool bIsQualified, bool isAlreadyEvaluate, MRemarks marks)
        {
            m_bIsPassLastEvaluationResults = bIsPassLastEvaluationResults;
            m_bIsPassSinceEvaluationResult = bIsPassSinceEvaluationResult;
            CountColumn = countColumn;
            _rmarks = marks;
            this.isAlreadyEvaluate = isAlreadyEvaluate;
            this.Loaded += BaseItem_A_Loaded;
            this.isDetail = false;
            m_bIsQualified = bIsQualified;
        }

        private void BaseItem_A_Loaded(object sender, RoutedEventArgs e)
        {
            _lstBorder = new List<Border>();

            if (!bIsAutoHigh)
            {
                this.Height = m_high;
                this.MaxHeight = 300;
            }
            else
            {
                this.MaxHeight = 300;
            }
            InitBaseGdPanel();
            InitgdPanel2();
            InitgdPanel1();
        }

        #endregion

        public virtual void SetPanel1AndPanel2Ratio(int ratio1, int ratio2)
        {
            _dWidthRatioGdPanel1 = ratio1;
            _dWidthRatioGdPanel2 = ratio2;
        }

        #region  设置布局

        /// <summary>
        /// 初始化设置总得布局
        /// </summary>
        private void InitBaseGdPanel()
        {
            gdPanel1 = new Grid();
            gdPanel1.SetValue(Grid.ColumnProperty, 0);
            gdPanel1.SetValue(Grid.RowProperty, 0);

            gdPanel2 = new Grid();
            gdPanel2.SetValue(Grid.ColumnProperty, 1);
            gdPanel2.SetValue(Grid.RowProperty, 0);

            SplitBaseGrid();
        }

        /// <summary>
        /// 把父级Grid分成两大列，第一列 放置gdPanel1，第二列放置gdPanel2
        /// </summary>
        private void SplitBaseGrid()
        {
            ColumnDefinition rd1 = new ColumnDefinition();
            rd1.Width = new GridLength(_dWidthRatioGdPanel1, GridUnitType.Star);
            ColumnDefinitions.Add(rd1);

            ColumnDefinition rd2 = new ColumnDefinition();
            rd2.Width = new GridLength(_dWidthRatioGdPanel2, GridUnitType.Star);
            ColumnDefinitions.Add(rd2);

            Children.Add(gdPanel1);
            Children.Add(gdPanel2);
        }

        #endregion

        #region 往gdPanel1面板上面添加控件、设置面板中边框样式、设置控件的样式

        private void InitgdPanel1()
        {
            InitControlOfPanel1();
            SetGridColum1();
            InitGdPanel1Border();
            AddControlIntoPanel1Border();
        }

        /// <summary>
        /// 设置gdPanel1面板列数
        /// </summary>
        private void SetGridColum1()
        {
            for (int i = 0; i < CountColumn; i++)
            {
                ColumnDefinition rd = new ColumnDefinition();
                rd.Width = SetGdPanel1ColumnWith(i);
                gdPanel1.ColumnDefinitions.Add(rd);
            }
        }


        /// <summary>
        /// 初始化gdPanel1面板上的Border
        /// </summary>
        private void InitGdPanel1Border()
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
                if (gdPanel1.Children.Contains(listBorder[i])) return;
                gdPanel1.Children.Add(listBorder[i]);
            }

            AddControlIntoPanel1Border();
        }

        /// <summary>
        /// 把控件放入gdPanel1面板的Border内
        /// </summary>
        protected abstract void AddControlIntoPanel1Border();

        /// <summary>
        /// 设置每一列的宽度
        /// </summary>
        /// <param name="iCount"></param>
        /// <returns></returns>
        protected abstract GridLength SetGdPanel1ColumnWith(int iCount);

        /// <summary>
        /// 设置gdPanel里的
        /// </summary>
        protected abstract void InitControlOfPanel1();

        #endregion

        #region 往gdPanel2面板上面添加控件、设置面板中边框样式、设置控件的样式和设置控件里相应的事件

        private void InitgdPanel2()
        {
            SetGridColum2();
            SetPanel2Border();
            InitControlOfPanel2();
            GdPanel2AddControl();
        }

        private void GdPanel2AddControl()
        {
            _lstBorder[0].Child = imgSinceEvaluationResult;
            SetRed(_lstBorder[0], m_bIsPassSinceEvaluationResult);
            _lstBorder[1].Child = imgQualified;
            SetRed(_lstBorder[1], m_bIsQualified);



        }

        /// <summary>
        /// 设置gdPanel2面板列数
        /// </summary>
        private void SetGridColum2()
        {
            for (int i = 0; i < iCountOfgdPanel2ChildControl; i++)
            {
                ColumnDefinition rd = new ColumnDefinition();
                rd.Width = new GridLength(1, GridUnitType.Star);
                gdPanel2.ColumnDefinitions.Add(rd);
            }
        }

        /// <summary>
        /// 设置gdPanel2的边框
        /// </summary>
        private void SetPanel2Border()
        {
            for (int i = 0; i < iCountOfgdPanel2ChildControl; i++)
            {
                Border bd = new Border();
                bd.BorderThickness = new Thickness(1, 1, 1, 1);
                bd.BorderBrush = borderBrush;
                bd.Background = borderBackground;

                bd.SetValue(Grid.ColumnProperty, i);
                bd.SetValue(Grid.RowProperty, 0);
                _lstBorder.Add(bd);
                if (gdPanel2.Children.Contains(_lstBorder[i])) return;
                gdPanel2.Children.Add(_lstBorder[i]);
            }
        }


        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControlOfPanel2()
        {
            //自评图标
            imgSinceEvaluationResult = new Image();
            SetImageStyle(imgSinceEvaluationResult, m_bIsPassSinceEvaluationResult ? strPassImaUri : strCrossImaUri);

            //设置巡回评的图标
            imgQualified = new Image();

            //如果已经评价了，就根据是否合格来给巡回评价设置图标
            if (isAlreadyEvaluate)
            {
                SetImageStyle(imgQualified, m_bIsQualified ? strPassImaUri : strCrossImaUri);
            }
            else
            {
                SetImageStyle(imgQualified, strCrossImaUri);
            }

        }

        /// <summary>
        /// 给控件设置事件
        /// </summary>
        private void imgRemark_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!isDetail)
            {
                RemarksWindows remakWindows = new RemarksWindows(_rmarks);
                remakWindows.ShowDialog();
            }
            else
            {
                RemarksDetailWindow remakDetail = new RemarksDetailWindow(_rmarks);
                remakDetail.ShowDialog();
            }
        }

        #endregion

        #region  设置公用控件的样式 TextBlok 和 Image

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
            tbk.Margin = new Thickness(10, 15, 10, 15);
            tbk.FontSize = 20;
            tbk.Foreground = ContenForeground;
            tbk.TextWrapping = TextWrapping.Wrap;
            tbk.Text = content;
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
            ima.Stretch = Stretch.None;
            ima.Source = new BitmapImage(new Uri(imaUri, UriKind.RelativeOrAbsolute));
        }

        #endregion

        /// <summary>
        /// 更新分数
        /// </summary>
        /// <param name="isQualified">合格为true，不合格为false</param>
        private void SetRed(Border bd, bool isQualified)
        {
            if (isQualified)
            {
                bd.Background = borderBackground;
            }
            else
            {
                bd.Background = redCellBackground;
            }

        }

        /// <summary>
        /// 更新分数
        /// </summary>
        /// <param name="action"></param>
        public void SetRefreshScore(Action action)
        {
            _action_score = action;
        }

        /// <summary>
        /// 设置跳转到五星级仓库表格
        /// </summary>
        /// <param name="action"></param>
        public void SetGoForm(Action action)
        {
            _action_GoForem = action;
        }

        /// <summary> 
        /// 改变巡回评价分数  
        /// </summary>
        /// <param name="isQualified">是否合格</param>
        protected abstract void ChangeScoreValue(bool isQualified);
    }
}