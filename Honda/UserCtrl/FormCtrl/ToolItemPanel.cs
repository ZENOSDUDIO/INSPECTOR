using Honda.Globals;
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
    public class ToolItemPanel : Grid
    {
        /// <summary>
        /// 要划分成的等份
        /// </summary>
        private int CountEqualParts = 20;

        /// <summary>
        /// 需要划分成的列数
        /// </summary>
        private int CountColumn = 3;


        /// <summary>
        /// 字体颜色
        /// </summary>
        public SolidColorBrush ContenForeground = new SolidColorBrush(Color.FromArgb(255, 000, 000, 000));

        /// <summary>
        /// 组名
        /// </summary>
        public string m_groupName;

        /// <summary>
        /// 序号号
        /// </summary>
        public string m_strOrderNum;

        private List<Border> lstBorder = new List<Border>();

        private TextBlock tbkGroupName;
        private TextBlock tbkOrderNum;
        private StackPanel panelContent;

        public ToolItemPanel(string _groupName, string OrderNum)
        {
            m_strOrderNum = OrderNum;
            panelContent = new StackPanel();
            panelContent.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            panelContent.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            m_groupName = _groupName;
            this.Loaded += ToolItemPanel_Loaded;
        }

        private void ToolItemPanel_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            tbkGroupName = new TextBlock();
            tbkOrderNum = new TextBlock();
            InitTextBlok(tbkGroupName, m_groupName);
            InitTextBlok(tbkOrderNum, m_strOrderNum);
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
                Children.Add(lstBorder[i]);
            }
        }

        private GridLength SetColumnWith(int iCount)
        {
            double dbWith = 0;
            switch (iCount)
            {
                case 0:
                    dbWith = (this.Width/CountEqualParts)*1.25;
                    break;
                case 1:
                    dbWith = (this.Width/CountEqualParts)*1.25;
                    break;
                case 2:
                    dbWith = (this.Width/CountEqualParts)*15.5;
                    break;
            }
            return new GridLength(dbWith);
        }

        #endregion

        private void InitBorder()
        {
            for (int i = 0; i < CountColumn; i++)
            {
                Border bd = new Border();
                if (i == 0)
                {
                    bd.BorderThickness = new Thickness(1, 1, 1, 1);
                    bd.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 228, 228, 228));
                }

                bd.SetValue(Grid.ColumnProperty, i);
                bd.SetValue(Grid.RowProperty, 0);
                lstBorder.Add(bd);
            }

            lstBorder[0].Child = tbkOrderNum;
            lstBorder[1].Child = tbkGroupName;
            lstBorder[2].Child = panelContent;
        }

        private void InitTextBlok(TextBlock tbk, string Content)
        {
            tbk.VerticalAlignment = VerticalAlignment.Center;
            tbk.HorizontalAlignment = HorizontalAlignment.Center;
            tbk.Foreground = ContenForeground;
            tbk.FontSize = 20;
            tbk.Margin = new Thickness(15, 0, 15, 0);
            tbk.TextWrapping = TextWrapping.Wrap;
            tbk.Text = Content;
        }

        public void AddItem(Grid gd)
        {
            panelContent.Children.Add(gd);
        }
    }
}