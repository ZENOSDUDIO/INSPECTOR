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
    public class ItemPanel_Style2:Grid
    {

        #region Var

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为1564等份，columnRatio1占212份
        /// </summary>
        double columnRatio1 = 3;
        /// <summary>
        /// 两列宽度的比例（可用宽度的分为8等份，columnRatio2占1352份
        /// </summary>
        double columnRatio2 = 3;

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为8等份，columnRatio2占1352份
        /// </summary>
        double columnRatio3 = 30;

        /// <summary>
        /// 需要划分成的列数
        /// </summary>
        int CountColumn = 3;

        /// <summary>
        /// 字体颜色
        /// </summary>
        public SolidColorBrush ContenForeground =(SolidColorBrush)Application.Current.Resources["FormForegroundBrush"];

        /// <summary>
        /// 组名
        /// </summary>
         string m_groupName ;

        /// <summary>
        /// 组名
        /// </summary>
         string m_strNo;

        List<Border> lstBorder = new List<Border>();

        TextBlock tbkGroupName;
        TextBlock tbkNo;
        StackPanel panelContent;
        #endregion

        public ItemPanel_Style2(string groupName, string strNo)
        {
            m_groupName = groupName;
            m_strNo = strNo;
            panelContent = new StackPanel();
            panelContent.VerticalAlignment = VerticalAlignment.Stretch;
            panelContent.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.Loaded += ItemPanel_Style2＿Loaded;
        }

        void ItemPanel_Style2＿Loaded(object sender, RoutedEventArgs e)
        {
            tbkGroupName = new TextBlock();
            SetTextBlokStyle(tbkGroupName,m_groupName);
            tbkNo = new TextBlock();
            SetTextBlokStyle(tbkNo, m_strNo);
            InitBorder();
            SetGridColum();
        }


        #region 设置列数
        /// <summary>
        /// 设置列数
        /// </summary>
        void SetGridColum()
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

        GridLength SetColumnWith(int iCount)
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

        void InitBorder()
        {
            for (int i = 0; i < CountColumn; i++)
            {
                Border bd = new Border();
         
                if(i==2)
                {
                    bd.BorderThickness = new Thickness(1, 0, 1, 0);
                }else
                {
                    bd.BorderThickness = new Thickness(1, 1, 1, 1);
                }
                
                bd.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 228, 228, 228));
                bd.Background = (SolidColorBrush)Application.Current.Resources["FormBackgroundBrush"];
                bd.SetValue(Grid.ColumnProperty, i);
                bd.SetValue(Grid.RowProperty, 0);
                lstBorder.Add(bd);
            }

            lstBorder[0].Child = tbkNo;
            lstBorder[1].Child = tbkGroupName;
            lstBorder[2].Child = panelContent;
        }

        void SetTextBlokStyle( TextBlock tbk, string content)
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

        public void AddItem(StackPanel sp)
        {
            panelContent.Children.Add(sp);
        }


        /// <summary>
        /// 设置两列的宽度比例
        /// </summary>
        /// <param name="ratio1"></param>
        /// <param name="ratio2"></param>
        public void setColumnRatio(double ratio1, double ratio2, double ratio3)
        {
            columnRatio1 = ratio1;
            columnRatio2 = ratio2;
            columnRatio3 = ratio3;
        }
    }
}
