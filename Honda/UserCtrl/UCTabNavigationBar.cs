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
    public class UCTabNavigationBar : Grid
    {
        /// <summary>
        /// 设置需要跳转到的面板的名字
        /// </summary>
        public string JumpPlaneName;

        private SolidColorBrush _Normalbackground =
            (SolidColorBrush) Application.Current.Resources["WhiteBackgroundBrush"];

        private SolidColorBrush _CheckBackground =
            (SolidColorBrush) Application.Current.Resources["ButtonBackgroundNormalBlueBrush"];

        private SolidColorBrush _CheckForeground =
            (SolidColorBrush) Application.Current.Resources["WhiteBackgroundBrush"];

        private SolidColorBrush _NormalForeground =
            (SolidColorBrush) Application.Current.Resources["ButtonBackgroundNormalBlueBrush"];

        private SolidColorBrush btnBorder =
            (SolidColorBrush) Application.Current.Resources["ButtonBackgroundNormalBlueBrush"];

        /// <summary>
        /// 需要显示或隐藏的Grid所对应的Button
        /// </summary>
        private List<Button> lstBtn;

        /// <summary>
        /// 需要显示或隐藏的Grid
        /// </summary>
        private List<Grid> lstGrid;


        private List<Grid> _lstGD;
        private Grid gdPanel;


        /// <summary>
        /// 整个控件的边框
        /// </summary>
        private Border _boreder;

        /// <summary>
        /// 整个控件的四个角的弧度半径
        /// </summary>
        private readonly double radius = 6;

        public UCTabNavigationBar()
        {
            lstBtn = new List<Button>();
            lstGrid = new List<Grid>();
            _lstGD = new List<Grid>();
            this.Loaded += UCTabNavigationBar_Loaded;
            InitGrid();
            InitBoreder();
        }

        private void UCTabNavigationBar_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < lstBtn.Count; i++)
            {
                lstBtn[i].BorderBrush = btnBorder;
                lstBtn[i].VerticalAlignment = VerticalAlignment.Stretch;
                lstBtn[i].HorizontalAlignment = HorizontalAlignment.Stretch;

                lstBtn[i].Margin = new Thickness(0, -1, 0, -1);

                if (i == 0 || i == (lstBtn.Count - 1))
                {
                    if (i == 0)
                    {
                        lstBtn[i].Style = (Style) Application.Current.Resources["ButtonStyle1"];
                        lstBtn[i].Margin = new Thickness(-2, -1, 0, -1);
                    }
                    else
                    {
                        lstBtn[i].Style = (Style) Application.Current.Resources["ButtonStyle2"];
                        lstBtn[i].Margin = new Thickness(0, -1, -1.5, -1);
                    }

                    lstBtn[i].SetValue(Grid.ColumnProperty, i);
                }
                else
                {
                    lstBtn[i].Style = (Style) Application.Current.Resources["ButtonStyle3"];
                }

                lstBtn[i].SetValue(Grid.ColumnProperty, i);
                lstBtn[i].SetValue(Grid.RowProperty, 0);
                if (gdPanel.Children.Contains(lstBtn[i])) return;
                gdPanel.Children.Add(lstBtn[i]);
            }


            if (lstBtn.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(JumpPlaneName))
                {
                    ShowPlane(JumpPlaneName);
                    JumpPlaneName = null;
                }
                else
                {
                    SetButtonHighlight(lstBtn[0]);

                    Grid gd = lstBtn[0].Tag as Grid;
                    if (gd == null)
                    {
                        return;
                    }

                    ShowGrid(gd, lstBtn[0].Content.ToString());
                }
            }
        }


        private void InitGrid()
        {
            gdPanel = new Grid();
            gdPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            gdPanel.VerticalAlignment = VerticalAlignment.Stretch;
        }

        private void InitBoreder()
        {
            _boreder = new Border();
            _boreder.Background = _Normalbackground;
            _boreder.BorderThickness = new Thickness(3, 3, 3, 3);
            _boreder.BorderBrush = _CheckBackground;
            _boreder.CornerRadius = new CornerRadius(radius);
            _boreder.HorizontalAlignment = HorizontalAlignment.Stretch;
            _boreder.VerticalAlignment = VerticalAlignment.Stretch;
            _boreder.Child = gdPanel;
            this.Children.Add(_boreder);
        }

        /// <summary>
        /// 添加要隐藏和显示的Grid 和对应的按钮名称
        /// </summary>
        /// <param name="panelName">对应的按钮名称</param>
        /// <param name="gd">需要隐藏显示的Grid</param>
        public void AddItem(string panelName, Grid gd)
        {
            Button btn = new Button();
            btn.Content = panelName;
            btn.FontSize = 25;
            btn.Tag = gd;
            btn.Click += btn_Click;
            lstBtn.Insert(lstBtn.Count, btn);
            lstGrid.Insert(lstGrid.Count, gd);
            ColumnDefinition col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            gdPanel.ColumnDefinitions.Add(col);
        }

        /// <summary>
        /// 跳转到其他面板
        /// </summary>
        /// <param name="planeName"></param>
        public void ShowPlane(string planeName)
        {
            string pannelname = string.Empty;

            Grid gd = null;
            for (int i = 0; i < lstBtn.Count; i++)
            {
                if (lstBtn[i].Content.ToString() == planeName)
                {
                    SetButtonHighlight(lstBtn[i]);
                    gd = lstBtn[i].Tag as Grid;

                    pannelname = lstBtn[i].Content.ToString();
                    break;
                }
            }

            if (gd != null)
            {
                ShowGrid(gd, pannelname);
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
            {
                return;
            }
            SetButtonHighlight(btn);

            Grid gd = btn.Tag as Grid;
            if (gd == null)
            {
                return;
            }

            ShowGrid(gd, btn.Content.ToString());
        }

        /// <summary>
        /// 设置按钮高亮显示
        /// </summary>
        /// <param name="btn"></param>
        private void SetButtonHighlight(Button btn)
        {
            for (int i = 0; i < lstBtn.Count; i++)
            {
                lstBtn[i].Background = _Normalbackground;
                lstBtn[i].Foreground = _NormalForeground;
            }

            btn.Background = _CheckBackground;
            btn.Foreground = _CheckForeground;
        }

        /// <summary>
        /// 显示对应的Grid，隐藏其他Grid
        /// </summary>
        /// <param name="gd"></param>
        /// <param name="panelname"></param>
        private void ShowGrid(Grid gd, string panelname)
        {
            for (int i = 0; i < lstGrid.Count; i++)
            {
                lstGrid[i].Visibility = Visibility.Collapsed;
            }

            gd.Visibility = Visibility.Visible;
            if (_actionName != null)
            {
                _actionName(panelname);
            }
        }

        private Action<string> _actionName;

        public void SetShowGridName(Action<string> action)
        {
            _actionName = action;
        }
    }
}