using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model;
using Honda.View;
using System;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Honda.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LoginVM : ViewModelBase
    {
        #region Var、constructor Fun

        public LoginPage loginPage { get; set; }

        private MainPage mainPage;


        private string strUserAccount;

        /// <summary>
        /// 用户名
        /// </summary>
        public string StrUserAccount
        {
            get { return strUserAccount; }
            set
            {
                if (value != strUserAccount)
                {
                    strUserAccount = value;
                    RaisePropertyChanged("StrUserAccount");
                }
            }
        }

        private string strPwd;

        /// <summary>
        /// 用户密码
        /// </summary>
        public string StrPwd
        {
            get { return strPwd; }
            set
            {
                if (value != strPwd)
                {
                    strPwd = value;
                    RaisePropertyChanged("StrPwd");
                }
            }
        }

        private bool bIsSaveAccount;

        /// <summary>
        /// 是否自动登录
        /// </summary>
        public bool BIsSaveAccount
        {
            get { return bIsSaveAccount; }
            set
            {
                if (value != bIsSaveAccount)
                {
                    bIsSaveAccount = value;
                    RaisePropertyChanged("BIsSaveAccount");
                }
            }
        }

        private bool bIsAutoLogin;

        public bool BIsAutoLogin
        {
            get { return bIsAutoLogin; }
            set
            {
                if (value != bIsAutoLogin)
                {
                    bIsAutoLogin = value;
                    RaisePropertyChanged("BIsAutoLogin");
                }
            }
        }

        public LoginVM()
        {
        }

        #endregion

        #region 获取登陆信息

        /// <summary>
        /// 获取登陆信息
        /// </summary>
        private void LoadUserInfo()
        {
            MUser CurrentUser = DMUser.INSTANCE.CurrentMUser;
            if (CurrentUser != null)
            {
                BIsAutoLogin = CurrentUser.IsAutoLogin;
                BIsSaveAccount = CurrentUser.IsSaveAccount;
                StrUserAccount = CurrentUser.UserAccount;
                StrPwd = CurrentUser.UserPwd;
            }
        }

        #endregion

        #region Command  登录 loaded 导航到主页

        /// <summary>
        /// 登录
        /// </summary>
        public RelayCommand LoginCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (string.IsNullOrWhiteSpace(StrUserAccount) || string.IsNullOrWhiteSpace(StrPwd))
                    {
                        MessageBox.Show("用户名和密码不能为空，请填写后再进行登录！");
                        return;
                    }
                    Login();
                });
            }
        }


        /// <summary>
        /// loaded 事件
        /// </summary>
        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    LoadUserInfo();
                    //本次成功登录后，再次返回次登陆页面的时候就不能，根据BIsAutoLogin直接跳入主页了
                    if (DMUser.INSTANCE.IsSucceedLogin) return;
                    if (BIsAutoLogin)
                    {
                        NavigateTomainPage();
                        DMUser.INSTANCE.IsSucceedLogin = true;
                    }
                });
            }
        }

        /// <summary>
        /// 是否正在登陆
        /// </summary>
        private bool isLogining = false;

        /// <summary>
        /// 登录客户端
        /// </summary>
        private void Login()
        {
            if (isLogining)
            {
                return;
            }
            isLogining = true;
            DMUser.INSTANCE.Login(StrUserAccount, StrPwd, BIsAutoLogin, BIsSaveAccount, (IsSucceed, msg) =>
            {
                loginPage.Dispatcher.InvokeAsync(() =>
                {
                    isLogining = false;
                    if (IsSucceed)
                    {
                        DMUser.INSTANCE.TickLogin();
                        NavigateTomainPage();
                    }
                    else
                    {
                        MessageBox.Show(msg);
                    }
                });
            });
        }

        /// <summary>
        /// 导航到主页
        /// </summary>
        private void NavigateTomainPage()
        {
            if (mainPage == null)
            {
                mainPage = new MainPage();
            }

            NavigationService ns = NavigationService.GetNavigationService(loginPage);
            ns.Content = mainPage;
        }

        #endregion
    }
}