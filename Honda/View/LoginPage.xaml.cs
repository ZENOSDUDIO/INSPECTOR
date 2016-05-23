using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.IO;
using Honda.HttpLib;
using Honda.Model;

namespace Honda.View
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            InitializeComponent();
            ((LoginVM) DataContext).loginPage = this;
        }

        private void btnHttpTest_Click(object sender, RoutedEventArgs e)
        {
            //VHttpTest vt = new VHttpTest();
            ////mainIndexPage = new MainIndexPage();
            //NavigationService ns = NavigationService.GetNavigationService(this);
            //ns.Content = vt;
            TestGetWorkLightList();
            return;
            TestGetImproveList();
            return;
            TestMulityFormUpload();
        }

        private void TestGetWorkLightList()
        {
            DMStoreTour.INSTANCE.CurrentMStore.appriaseId = "523012655de2410f860243b02d6152d4";
            DMStoreTour.INSTANCE.CurrentMStore.shopId = "DHEN04";
            //ReqGetWorkLightList cmd = new ReqGetWorkLightList(null);
            //cmd.SendHttpRequest();
        }

        private void TestGetImproveList()
        {
            DMImprove.INSTANCE.GetImproveCheckList((isSucceed, msg) => { });
        }


        private void TextBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            Tools.OpenKeyBoard();
        }

        private void TextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            Tools.CloseKeyBoard();
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Tools.OpenKeyBoard();
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Tools.CloseKeyBoard();
        }


        //test ExamplePage
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExamplePage page = new ExamplePage();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Content = page;
            return;

            DMImprove.INSTANCE.UploadImproveCheck(null);
            return;
            //NavigationService _navigate;
            //_navigate = NavigationService.GetNavigationService(this);

            //BusinessPolicyPage page = new BusinessPolicyPage();
            //_navigate.Content = page;
            //return;

            //TestMulityFormUpload();
            //return;
            //服务基础评价
            //DMServiceBasedEvaluation.INSTANCE.GetServiceBasedData(null);
            //服务管理评价
            //DMServiceEvaluationManage.INSTANCE.GetFormListFromServer(null);
            //零部件评价
            DMUnivesalEvaluate.INSTANCE.GetFormListFromServer(null);
            //建议加分项
            //DMSuggestPluses.INSTANCE.GetFormListFromServer(null);


            //testWindow _wd = new testWindow();
            //_wd.ShowDialog();
            return;


            ReqUploadFormScore _cmdUploadFormScore = new ReqUploadFormScore(null, DMPreview.INSTANCE.ListSignature,
                (obj) =>
                {
                    ReqUploadFormScore req = obj as ReqUploadFormScore;
                    if (!req.m_bIsExistException)
                    {
                        req.ParseParam();
                        if (req.m_bIsSuccess)
                        {
                            MessageBox.Show("上传分数成功");
                        }
                        else
                        {
                            MessageBox.Show(req.m_strErrorMsg);
                        }
                    }
                    else
                    {
                        MessageBox.Show(req.m_strErrorMsg);
                    }
                });

            _cmdUploadFormScore.SendHttpRequest();
        }

        private void TestMulityFormUpload()
        {
            //FileStream fs1 = File.Open("E:\\文本.txt",FileMode.Open);
            //FileStream fs2 = File.Open("E:\\图片.png", FileMode.Open);
            //FileStream fs3 = File.Open("E:\\文档.doc", FileMode.Open);
            //List<MFileData> files = new List<MFileData>();
            //files.Add(new MFileData("E:\\文本.txt"));
            //files.Add(new MFileData("E:\\图片.png"));
            //files.Add(new MFileData("E:\\文档.doc"));
            ReqTestMulityForm cmd = null;
            cmd = new ReqTestMulityForm((obje) => { cmd.ParseParam(); }, null);
            cmd.SendHttpRequest();
        }

        private bool isEnter = false;

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            sp_wd_btn.Visibility = Visibility.Visible;
            isEnter = true;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            delayTime();
        }

        private async Task delayTime()
        {
            isEnter = false;
            await Task.Delay(1000);
            if (isEnter)
            {
                return;
            }
            sp_wd_btn.Visibility = Visibility.Collapsed;
        }
    }


    public class TestObser : IObserver<int>
    {
        #region IObserver<int> 成员

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(int value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}