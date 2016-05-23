using System.Windows;
using Honda.Globals;
using Honda.HttpLib;
using Honda.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.ViewModel
{
    public class DMUser
    {
        #region Var、constructor Fun

        private System.Timers.Timer loginTimer; //心跳登录的定时器 

        /// <summary>
        /// 当前用户
        /// </summary>
        public MUser CurrentMUser { get; set; }

        /// <summary>
        /// 本次登录是否成功
        /// </summary>
        public bool IsSucceedLogin { get; set; }

        public static DMUser INSTANCE = new DMUser();

        public bool isOnline { get; set; }

        private DMUser()
        {
            //Test
            isOnline = true;
            LoadUserInfo();
        }

        #endregion

        #region  保存用户名和密码

        /// <summary>
        /// 保存用户名和密码
        /// </summary>
        /// <param name="isAutoLogin">是否自动登录</param>
        /// <param name="isSaveAccount">是否保存账号</param>
        /// <param name="userAccount">账号</param>
        /// <param name="userPwd">密码</param>
        private void SaveUserInfo(bool isAutoLogin, bool isSaveAccount, string userAccount, string userPwd)
        {
            if (CurrentMUser == null)
            {
                CurrentMUser = new MUser();
            }

            //保存当前用户的信息
            CurrentMUser.UserAccount = userAccount;
            CurrentMUser.UserPwd = userPwd;
            CurrentMUser.IsAutoLogin = isAutoLogin;
            CurrentMUser.IsSaveAccount = isSaveAccount;

            //用于存储到本地的用户信息
            MUser uerInfo = new MUser();

            //如果勾选了记住用户用户,则保存用户名和密码,否则用户名和密码置空
            if (isSaveAccount)
            {
                uerInfo.UserAccount = userAccount;
                uerInfo.UserPwd = userPwd;
            }
            else
            {
                uerInfo.UserAccount = "";
                uerInfo.UserPwd = "";
            }
            uerInfo.IsAutoLogin = isAutoLogin;
            uerInfo.IsSaveAccount = isSaveAccount;
            string path = DirectoryHelper.INSTANCE.CUSTOMER_PATH;
            SerialHelp.SerialObject(path, uerInfo);
        }

        #endregion

        #region 读取用户名信息

        private void LoadUserInfo()
        {
            string customerPath = DirectoryHelper.INSTANCE.CUSTOMER_PATH;
            CurrentMUser = (MUser) SerialHelp.LoadFromBinaryFile(customerPath);
        }

        #endregion

        #region 登陆

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userAccount">用户名</param>
        /// <param name="userPwd">密码</param>
        /// <param name="isAutoLogin">是否自动登陆</param>
        /// <param name="isSaveAccount">是否保存密码</param>
        /// <param name="handleLoginResult">登录回调函数</param>
        public void Login(string userAccount, string userPwd, bool isAutoLogin, bool isSaveAccount,
            Action<bool, string> handleLoginResult)
        {
            ReqLogin _cmdLogin = new ReqLogin(userAccount, userPwd, (obj) =>
            {
                ReqLogin req = obj as ReqLogin;
                if (!req.m_bIsExistException)
                {
                    req.ParseParam();
                    if (req.m_bIsSuccess)
                    {
                        IsSucceedLogin = true;
                        SaveUserInfo(isAutoLogin, isSaveAccount, userAccount, userPwd);
                        CurrentMUser.UserName = req.userInfo.UserName;

                        //如果登陆成功则获取BI报表页面地址

                        var urlInfo =
                            SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.BI_REPORT_URL_INFO) as MBiReportInfo;
                        if (urlInfo == null || string.IsNullOrEmpty(urlInfo.BiReportURL))
                        {
                            this.GetBiReportAddr();
                        }

                        handleLoginResult(true, "操作成功！");
                    }
                    else
                    {
                        SaveUserInfo(false, isSaveAccount, userAccount, userPwd);
                        handleLoginResult(false, "登录失败,账号或密码不正确！");
                    }
                }
                else
                {
                    SaveUserInfo(false, isSaveAccount, userAccount, userPwd);
                    handleLoginResult(false, "登录失败,检查是否网络故障！");
                }
            });
            _cmdLogin.SendHttpRequest();
        }

        private void GetBiReportAddr()
        {
            ReqBIReportAddr cmdReqBiUrl = new ReqBIReportAddr((obj) =>
            {
                try
                {
                    ReqBIReportAddr req = obj as ReqBIReportAddr;

                    var urlInfo = new MBiReportInfo
                    {
                        BiReportURL = ""
                    };

                    if (!req.m_bIsExistException)
                    {
                        req.ParseParam();
                        if (req.m_bIsSuccess && !string.IsNullOrEmpty(req.Url))
                        {
                            urlInfo.BiReportURL = req.Url;
                        }

                        SerialHelp.SerialObject(DirectoryHelper.INSTANCE.BI_REPORT_URL_INFO, urlInfo);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
            cmdReqBiUrl.SendHttpRequest();
        }

        public void TickLogin()
        {
            loginTimer = new System.Timers.Timer();
            loginTimer.Interval = GlobalValue.TIME_TICK_AUTO_LOGIN;
            loginTimer.Elapsed += loginTimer_Tick;
            loginTimer.Start();
        }

        /// <summary>
        /// 自动登录心跳
        /// </summary>     string userAccount, string userPwd, bool isAutoLogin, bool isSaveAccount
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginTimer_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine("************心跳登录**************");
            Login(CurrentMUser.UserAccount, CurrentMUser.UserPwd, CurrentMUser.IsAutoLogin, CurrentMUser.IsSaveAccount,
                (isSucceed, msg) =>
                {
                    if (isSucceed)
                    {
                        isOnline = true;

                        Debug.WriteLine("心跳登录成功");
                    }
                    else
                    {
                        isOnline = false;
                        Debug.WriteLine("心跳登录失败");
                    }
                });
        }

        #endregion
    }
}