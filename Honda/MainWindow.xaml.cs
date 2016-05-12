using System.IO;
using System.Windows;
using System.Windows.Input;
using Honda.ViewModel;
using Honda.View;
using System.Windows.Navigation;
using System;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Excel;
using System.Collections.Generic;

namespace Honda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        private bool _allowDirectNavigation = false;
        private NavigatingCancelEventArgs _navArgs = null;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += MainWindow_KeyDown;

            this.SizeChanged += MainWindow_SizeChanged;

            Closing += (s, e) => ViewModelLocator.Cleanup();

            Messenger.Default.Register<string>(this, GlobalValue.COMMAND_MAIN_WINDOW,
                SwitchWindowAction);
        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualHeight > SystemParameters.WorkArea.Height || this.ActualWidth > SystemParameters.WorkArea.Width)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                this.WindowStyle = WindowStyle.None;
                this.Hide();
                this.Show();
            }
        }
        void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.WindowState = WindowState.Minimized;
        }

        private void SwitchWindowAction(string strAction)
        {
            switch (strAction)
            {
                case GlobalValue.COMMAND_MAX_WINDOW:
                    if (this.WindowState != WindowState.Maximized)
                    {
                        this.WindowState = WindowState.Maximized;
                    }
                    break;

                case GlobalValue.COMMAND_MIN_WINDOW:
                    this.WindowState = WindowState.Minimized;

                    break;

                case GlobalValue.COMMAND_NORMAL_WINDOW:
                    if (this.WindowState != WindowState.Normal)
                    {
                        this.WindowState = WindowState.Normal;
                    }

                    break;
                case GlobalValue.COMMAND_DRAG_MOVE_WINDOW:
                    this.DragMove();
                    break;
                case GlobalValue.COMMAND_CLOSE_WINDOW:
                    //保持用户信息
                    SerialHelp.SerialObject(DirectoryHelper.INSTANCE.CUSTOMER_PATH, DMUser.INSTANCE.CurrentMUser);

                    //清除BiReportURL缓存
                    if (File.Exists(DirectoryHelper.INSTANCE.BI_REPORT_URL_INFO))
                    {
                        File.Delete(DirectoryHelper.INSTANCE.BI_REPORT_URL_INFO);
                    }

                    CloseWindow();
                    break;
            }
        }

        /// <summary>
        ///                --待完善
        /// </summary>
        private void CloseWindow()
        {
            MessageBoxResult result = MessageBox.Show("是否退出程序？", "退出程序", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        #region Navegate 动画

        private void NavigationWindow_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (Content != null && !_allowDirectNavigation)
            {
                e.Cancel = true;
                _navArgs = e;
                this.IsHitTestVisible = false;
                DoubleAnimation da = new DoubleAnimation(0.3d, new Duration(TimeSpan.FromMilliseconds(100)));
                da.Completed += FadeOutCompleted;
                this.BeginAnimation(OpacityProperty, da);
            }
            _allowDirectNavigation = false;
        }

        private void FadeOutCompleted(object sender, EventArgs e)
        {
            var animationClock = sender as AnimationClock;
            if (animationClock != null) animationClock.Completed -= FadeOutCompleted;

            this.IsHitTestVisible = true;

            _allowDirectNavigation = true;
            switch (_navArgs.NavigationMode)
            {
                case NavigationMode.New:
                    if (_navArgs.Uri == null)
                    {
                        NavigationService.Navigate(_navArgs.Content);
                    }
                    else
                    {
                        NavigationService.Navigate(_navArgs.Uri);
                    }
                    break;
                case NavigationMode.Back:
                    NavigationService.GoBack();
                    break;

                case NavigationMode.Forward:
                    NavigationService.GoForward();
                    break;
                case NavigationMode.Refresh:
                    NavigationService.Refresh();
                    break;
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate()
                {
                    DoubleAnimation da = new DoubleAnimation(1.0d, new Duration(TimeSpan.FromMilliseconds(100)));
                    this.BeginAnimation(OpacityProperty, da);
                });
        }

        #endregion
    }
}