using Honda.Globals;
using Honda.Interface;
using Honda.View;
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
    /// 生成页面表格的最小一项
    /// </summary>
    public partial class ProduceCell : UserControl
    {
        /// <summary>
        /// 表格中 ，组的种类
        /// </summary>
        public ItemGroupTyple itemGroupType { get; set; }

        //高亮的背景颜色
        private SolidColorBrush highlightBackground = new SolidColorBrush(Color.FromArgb(255, 255, 226, 230));
        //正常的背景颜色
        private SolidColorBrush normalBackground = null;

        //高亮的字体颜色
        private SolidColorBrush highlightForeground = new SolidColorBrush(Color.FromArgb(255, 255, 226, 230));
        //正常的字体颜色
        private SolidColorBrush normalForeground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

        /// <summary>
        /// 具体评价内容
        /// </summary>
        public string EvaluationContent { get; set; }

        /// <summary>
        /// 上次评分分数
        /// </summary>
        public string LastScore { get; set; }

        /// <summary>
        /// 自评分数
        /// </summary>
        public string SelfScore { get; set; }

        /// <summary>
        /// 巡回评价分数
        /// </summary>
        public string TourScore { get; set; }

        public IGroup dataSource;

        public ProduceCell()
        {
            InitializeComponent();
            this.Loaded += ProduceCell_Loaded;
        }

        private void ProduceCell_Loaded(object sender, RoutedEventArgs e)
        {
            itemGroupType = dataSource.itemGroupType;
            tbkEvaluationContent.Text = dataSource._EvaluationGroupContent;
            tbkLastScore.Text = dataSource._level_One_LastScore.ToString();
            tbkSelfScore.Text = dataSource._level_One_SelfScore.ToString();
            tbkTourScore.Text = dataSource._level_One_TourScore.ToString();
        }


        /// <summary>
        /// 显示或者隐藏底部边框（列表的最后一项的时候隐藏底部边框）
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowBottomBorder(bool isShow)
        {
            if (isShow)
            {
                bd_1.BorderThickness = new Thickness(0, 0, 0, 1);
            }
            else
            {
                bd_1.BorderThickness = new Thickness(0, 0, 0, 0);
            }
        }

        /// <summary>
        /// 设置高亮显示
        /// </summary>
        /// <param name="isHighlight">true为高亮（不是满分），false为正常显示（满分）</param>
        public void ShowHighlight(bool isHighlight)
        {
            if (isHighlight)
            {
                gd_1.Background = highlightBackground;
                tbkTourScore.Foreground = highlightForeground;
            }
            else
            {
                gd_1.Background = normalBackground;
                tbkTourScore.Foreground = normalForeground;
            }
        }

        private void btkCheck_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FormGroupDetail formGroupDetail = new FormGroupDetail(dataSource);
            formGroupDetail.ShowDialog();
        }
    }
}