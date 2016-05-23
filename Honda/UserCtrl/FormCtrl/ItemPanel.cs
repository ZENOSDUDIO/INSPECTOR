using Honda.Globals;
using Honda.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Honda.UserCtrl
{
    public class ItemPanel : Grid
    {
        #region Var

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为1564等份，columnRatio1占67份
        /// </summary>
        private int columnRatio1 = 67;

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为1564等份，columnRatio1占212份
        /// </summary>
        private int columnRatio2 = 312;

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为8等份，columnRatio2占1352份
        /// </summary>
        private int columnRatio3 = 1352;

        /// <summary>
        /// 需要划分成的列数
        /// </summary>
        private int CountColumn = 3;

        /// <summary>
        /// 字体颜色
        /// </summary>
        public SolidColorBrush ContenForeground = (SolidColorBrush)Application.Current.Resources["FormForegroundBrush"];

        /// <summary>
        /// 组名
        /// </summary>
        public string m_groupName;

        /// <summary>
        /// 组序号
        /// </summary>
        public string m_groupNo;

        private List<Border> lstBorder = new List<Border>();

        private TextBlock tbkGroupName;
        private TextBlock tbkGroupNo;

        /// <summary>
        /// 用于存放组名和分数
        /// </summary>
        private StackPanel panelGroup;

        /// <summary>
        /// 序号
        /// </summary>
        private StackPanel panelNo;

        private StackPanel panelContent;

        #endregion

        public ItemPanel()
        {
            panelContent = new StackPanel();
            panelContent.VerticalAlignment = VerticalAlignment.Stretch;
            panelContent.HorizontalAlignment = HorizontalAlignment.Stretch;

            panelGroup = new StackPanel();
            panelGroup.VerticalAlignment = VerticalAlignment.Center;
            panelGroup.HorizontalAlignment = HorizontalAlignment.Stretch;

            panelNo = new StackPanel();
            panelNo.VerticalAlignment = VerticalAlignment.Center;
            panelNo.HorizontalAlignment = HorizontalAlignment.Stretch;

            this.Loaded += ItemPanel_Loaded;
        }

        private void ItemPanel_Loaded(object sender, RoutedEventArgs e)
        {
            InitTextBlok();
            InitBorder();
            SetGridColum();
        }

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
                if (Children.Contains(lstBorder[i])) return;
                Children.Add(lstBorder[i]);
            }
        }

        /// <summary>
        /// 设置钱两列的宽度比例
        /// </summary>
        /// <param name="_columnRatio1"></param>
        /// <param name="_columnRatio2"></param>
        public void SetWith(int _columnRatio1, int _columnRatio2, int _columnRatio3)
        {
            columnRatio1 = _columnRatio1;
            columnRatio2 = _columnRatio2;
            columnRatio3 = _columnRatio3;
        }

        private GridLength SetColumnWith(int iCount)
        {
            double dbWith = 0;
            switch (iCount)
            {
                case 0:
                    dbWith = columnRatio1;
                    break;
                case 1:
                    dbWith = columnRatio2;
                    break;
                case 2:
                    dbWith = columnRatio3;
                    break;
            }
            return new GridLength(dbWith, GridUnitType.Star);
        }

        #endregion

        private void InitBorder()
        {
            for (int i = 0; i < CountColumn; i++)
            {
                Border bd = new Border();

                bd.BorderThickness = new Thickness(1, 1, 1, 1);

                bd.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 228, 228, 228));
                bd.Background = (SolidColorBrush)Application.Current.Resources["FormBackgroundBrush"];
                bd.SetValue(Grid.ColumnProperty, i);
                bd.SetValue(Grid.RowProperty, 0);
                lstBorder.Add(bd);
            }

            lstBorder[0].Child = panelNo;
            lstBorder[1].Child = panelGroup;
            lstBorder[2].Child = panelContent;
        }

        private void InitTextBlok()
        {
            tbkGroupName = new TextBlock();
            SetTextBlock(tbkGroupName, m_groupName);

            tbkGroupNo = new TextBlock();
            SetTextBlock(tbkGroupNo, m_groupNo);


            panelGroup.Children.Add(tbkGroupName);
            panelNo.Children.Add(tbkGroupNo);
        }

        private void SetTextBlock(TextBlock tbk, string content)
        {
            tbk.VerticalAlignment = VerticalAlignment.Center;
            tbk.HorizontalAlignment = HorizontalAlignment.Center;
            tbk.Foreground = ContenForeground;
            tbk.FontSize = 20;
            tbk.Margin = new Thickness(15, 0, 15, 0);
            tbk.TextWrapping = TextWrapping.Wrap;
            tbk.Text = content;
        }

        public void AddItem(Grid gd)
        {
            panelContent.Children.Add(gd);
        }

        public void AddItem(ItemRowControl sp)
        {
            panelContent.Children.Add(sp);
        }
    }
}