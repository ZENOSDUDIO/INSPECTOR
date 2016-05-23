using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model;
using Honda.Model.Form;
using Honda.UserCtrl;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Collections.Generic;
using Honda.Excel;

namespace Honda.View
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class PreviewPage : BasePage
    {
        #region Var、Constration Fun

        private PreviewVM _ViewModel;

        public PreviewPage()
        {
            InitializeComponent();

            _ViewModel = (PreviewVM)DataContext;
            ((PreviewVM)DataContext)._thisPage = this;

            Messenger.Default.Register<string>(this, GlobalValue._PREVIEW_LOAD_DATA, (msg) =>
            {
                LoadBaseUnivesalControl();
            });
        }

        #endregion

        #region 加载基础业务

        /// <summary>
        /// 加载基础业务模块表格
        /// </summary>
        private void LoadBaseUnivesalControl()
        {
            //if (DMPreview.INSTANCE.ListSignature != null)
            //{
            //    foreach (var sign in DMPreview.INSTANCE.ListSignature)
            //    {
            //        if (sign != null)
            //        {
            //            byte[] buffer = File.ReadAllBytes(sign.signatureFileName);

            //            switch (sign.Sign2String())
            //            {
            //                case "1":
            //                    imaValuator1.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            //                    break;
            //                case "2":
            //                    imaComponent.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            //                    break;
            //                case "3":
            //                    imaServe.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            //                    break;
            //                case "4":
            //                    ImaGeneral.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
            //                    break;
            //            }
            //        }
            //    }
            //}

            sp1.Children.Clear();

            if (_ViewModel == null || _ViewModel.ListEvaData == null) return;

            foreach (var item in _ViewModel.ListEvaMenu)
            {
                var itemPreview = new ItemPreviewPanel();

                //设置宽度比例
                itemPreview.SetWith(13, 64);

                itemPreview.m_groupName = item.EvaluateName;

                var data = _ViewModel.ListEvaData.SingleOrDefault(n => n._sourceIdentify == item.EvaluateCode);

                if (data == null) return;
                foreach (MItemBaseGroup group in data.ListGroup)
                {
                    LoadUnivesalForm(itemPreview, SetScore, (M_Common_Groupcs)group);
                }
                sp1.Children.Add(itemPreview);
            }
            SetScore();
        }

        private void SetScore()
        {
            double intTotal = 0;
            double intPass = 0;
            double intUnpass = 0;

            foreach (var item in DMUnivesalEvaluate.INSTANCE.DataBaseUniversal[DMStoreTour.INSTANCE.CurrentMStore.shopId])
            {
                intPass += item._pageTourScore;
                intTotal += item._pageTotalScore;
            }

            intUnpass = intTotal - intPass;

            tbkAll.Text = string.Format("合计：{0}项", intTotal);
            tbkPass.Text = string.Format("合格：{0}项", intPass);
            tbkUnpass.Text = string.Format("不合格：{0}项", intUnpass);
            tbkPassRate.Text = string.Format("合格率：{0}%", Math.Round(intPass * 100 / intTotal, 2));
            NotificationUpdateScore();
        }

        #endregion

        /// <summary>
        /// 加载一组表格的数据
        /// </summary>
        /// <param name="group">这一组表格的数据List</param>
        /// <param name="updateScore"> 回掉函数，当操作分数区域时更新代码</param>
        /// <param name="sp">需要添加表格的面板</param>
        private void LoadUnivesalForm(ItemPreviewPanel itemPreview, Action updateScore, M_Common_Groupcs group)
        {
            var itemGroup = new ItemPanel
            {
                m_groupName = group._EvaluationGroupContent,
                m_groupNo = group._index.ToString()
            };

            //设置宽度比例
            itemGroup.SetWith(4, 21, 39);

            foreach (MItemNormal item in group.LstItem)
            {
                var itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_PREVIEW, item) { IsHeighAuto = true };

                //刷新分数
                itemControl.UpdateScore(updateScore);

                itemGroup.AddItem(itemControl);


            }
            itemPreview.AddItem(itemGroup);
        }

        #region 签名

        ////List<bool> lstIsCompleteSignature = new List<bool>() { false, false, false, false, false };
        ///// <summary>
        ///// 评价员一
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gdValuator1_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    string FileName = null;
        //    if (DMPreview.INSTANCE.ListSignature[0] != null)
        //    {
        //        FileName = DMPreview.INSTANCE.ListSignature[0].signatureFileName;
        //    }
        //    else
        //    {
        //        FileName = DirectoryHelper.INSTANCE.Signature_produce_valuator1_path;
        //    }

        //    bool isSucceed = DoSignature(FileName, imaValuator1, 0);
        //    if (isSucceed)
        //    {
        //        MSignature signatrue = new MSignature();
        //        signatrue._signature_type = SIGNATURE_TYPE.valuator;
        //        signatrue.signatureFileName = FileName;
        //        DMPreview.INSTANCE.ListSignature[0] = signatrue;
        //    }
        //}

        //// 评价员二
        //private void gdValuator2_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    //string FileName = null;
        //    //if (DMPreview.INSTANCE.ListSignature[1] != null)
        //    //{
        //    //    FileName = DMPreview.INSTANCE.ListSignature[1].signatureFileName;
        //    //}
        //    //else
        //    //{
        //    //    FileName = DirectoryHelper.INSTANCE.Signature_produce_valuator2_path;
        //    //}
        //    //bool isSucceed = DoSignature(FileName, imaValuator2, 1);
        //    //if (isSucceed)
        //    //{
        //    //    MSignature signatrue = new MSignature();
        //    //    signatrue._signature_type = SIGNATURE_TYPE.valuator;
        //    //    signatrue.signatureFileName = FileName;
        //    //    DMPreview.INSTANCE.ListSignature[1] = signatrue;
        //    //}
        //}

        ///// <summary>
        ///// 零部件经理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gdComponent_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    string FileName = null;
        //    if (DMPreview.INSTANCE.ListSignature[1] != null)
        //    {
        //        FileName = DMPreview.INSTANCE.ListSignature[1].signatureFileName;
        //    }
        //    else
        //    {
        //        FileName = DirectoryHelper.INSTANCE.Signature_produce_componentManager_path;
        //    }
        //    bool isSucceed = DoSignature(FileName, imaComponent, 1);
        //    if (isSucceed)
        //    {
        //        MSignature signatrue = new MSignature();
        //        signatrue._signature_type = SIGNATURE_TYPE.componentManager;
        //        signatrue.signatureFileName = FileName;
        //        DMPreview.INSTANCE.ListSignature[1] = signatrue;
        //    }
        //}


        ///// <summary>
        ///// 服务经理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gdServe_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    string FileName = null;
        //    if (DMPreview.INSTANCE.ListSignature[2] != null)
        //    {
        //        FileName = DMPreview.INSTANCE.ListSignature[2].signatureFileName;
        //    }
        //    else
        //    {
        //        FileName = DirectoryHelper.INSTANCE.Signature_produce_servesManager_path;
        //    }
        //    bool isSucceed = DoSignature(FileName, imaServe, 2);
        //    if (isSucceed)
        //    {
        //        MSignature signatrue = new MSignature();
        //        signatrue._signature_type = SIGNATURE_TYPE.servesManager;
        //        signatrue.signatureFileName = FileName;
        //        DMPreview.INSTANCE.ListSignature[2] = signatrue;
        //    }
        //}


        ///// <summary>
        ///// 总经理
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gdGeneral_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    string FileName = null;
        //    if (DMPreview.INSTANCE.ListSignature[3] != null)
        //    {
        //        FileName = DMPreview.INSTANCE.ListSignature[3].signatureFileName;
        //    }
        //    else
        //    {
        //        FileName = DirectoryHelper.INSTANCE.Signature_produce_generalManager_path;
        //    }
        //    bool isSucceed = DoSignature(FileName, ImaGeneral, 3);
        //    if (isSucceed)
        //    {
        //        MSignature signatrue = new MSignature();
        //        signatrue._signature_type = SIGNATURE_TYPE.generalManager;
        //        signatrue.signatureFileName = FileName;
        //        DMPreview.INSTANCE.ListSignature[3] = signatrue;
        //    }
        //}


        ///// <summary>
        ///// 签名
        ///// </summary>
        //private bool DoSignature(string path, Image imaSignature, int index)
        //{
        //    SignatureWindow _signature = new SignatureWindow();
        //    _signature.pictruePath = path;
        //    _signature.ShowLocationPictrue();
        //    _signature.ShowDialog();

        //    if (_signature.IsComplate)
        //    {
        //        try
        //        {
        //            byte[] buffer = File.ReadAllBytes(path);
        //            imaSignature.Source = new ImageSourceConverter().ConvertFrom(buffer) as ImageSource;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("PreviewPage类中的\ngdValuator1_MouseUp()方法出错\n" + ex.Message);
        //            return false;
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        #endregion

        //打开提交评分表窗口窗口
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //if (DMPreview.INSTANCE.ListSignature != null)
            //{
            //    bool isFinishSignature = true;
            //    foreach (MSignature sign in DMPreview.INSTANCE.ListSignature)
            //    {
            //        if (sign == null)
            //        {
            //            isFinishSignature = false;
            //        }
            //    }
            //    if (!isFinishSignature)
            //    {
            //        System.Windows.MessageBox.Show("请完成签名后再提交！");
            //        return;
            //    }
            //}

            var uploadingWindow = new UploadingWindow();
            var showDialog = uploadingWindow.ShowDialog();
            if (showDialog != null && (bool)showDialog)
            {
                var navigate = NavigationService.GetNavigationService(this);
                while (navigate != null && navigate.CanGoBack)
                {
                    navigate.RemoveBackEntry();
                }

                var mainPage = new MainPage();
                if (navigate != null) navigate.Content = mainPage;
            }
        }

        /// <summary>
        /// 保存本地数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveOffineData();

            MessageBox.Show("保存成功！");
        }

        private static void SaveOffineData()
        {
            MFormStatu statu = new MFormStatu();
            statu.statu = "1";
            //服务基础评价
            SerialHelp.SerialObject(DirectoryHelper.INSTANCE.STORE_FORM_STATE, statu);
            DMPreview.INSTANCE.SaveCurrentSoteForm();

            //设置允许读取离线缓冲数据
            GlobalValue.Store_Need_Load_Offline_Data_State_Mgr.SetStoreNeedLoadOfflineDataState(
                DMStoreTour.INSTANCE.CurrentMStore, true);
            SerialHelp.SerialObject(DirectoryHelper.INSTANCE.STORE_NEED_LOAD_OFFLINE_DATA,
                GlobalValue.Store_Need_Load_Offline_Data_State_Mgr);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <author name="Zeno Peng" QQ="348866475"/>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //保存本地数据
            SaveOffineData();

            //构建Excel导出表格数据
            var tables = new List<Honda.Excel.ExcelModel.ExcelTable>();

            if (_ViewModel == null || _ViewModel.ListEvaMenu == null) return;

            foreach (var menu in _ViewModel.ListEvaMenu)
            {
                var table = new Honda.Excel.ExcelModel.ExcelTable
                {
                    TbHeads = new List<Excel.ExcelModel.DataHead> { 
                        new Excel.ExcelModel.DataHead { T1 = menu.EvaluateName},
                      new Honda.Excel.ExcelModel.DataHead {T1="序号", T2=menu.EvaluateHeadName,T3="序号",T4=menu.EvaluateHeadDesc,T11="确认"}
                    }
                };
                tables.Add(table);

                //构建Excel表格数据部分
                var data = _ViewModel.ListEvaData.First(n => n._sourceIdentify == menu.EvaluateCode);

                if (data == null) continue;
                var excelData = new List<Excel.ExcelModel.DataRow>();
                var excelGroup = new List<Excel.ExcelModel.DataGroup>();

                table.TbData = excelData;
                table.TbGroups = excelGroup;

                int rowindex = 0;
                foreach (var gp in data.ListGroup)
                {
                    //构建Excel合并单元格Group数据；表格第一列，第二列有按组合并单元格
                    var egp1 = new Excel.ExcelModel.DataGroup { startRowIndex = rowindex, startColIndex = 1 };
                    var egp2 = new Excel.ExcelModel.DataGroup { startRowIndex = rowindex, startColIndex = 2 };

                    foreach (var item in gp.LstItem)
                    {
                        var dr = new Excel.ExcelModel.DataRow
                        {
                            D1 = gp._index.ToString(),
                            D2 = gp._EvaluationGroupContent,
                            D3 = item.StrContent1,
                            D4 = item.StrContent2,
                            D11 = item.bIsEvaluationOfTour ? Excel.ExcelModel.ExcelValues.PassValue : Excel.ExcelModel.ExcelValues.UnpassValue
                        };
                        excelData.Add(dr);

                        rowindex++;
                    }

                    //记录组结尾坐标
                    egp1.endRowIndex = rowindex - 1;
                    egp1.endColIndex = 1;
                    egp2.endRowIndex = rowindex - 1;
                    egp2.endColIndex = 2;

                    excelGroup.Add(egp1);
                    excelGroup.Add(egp2);
                }
            }

            //调用并导出Excel
            var titles = new Dictionary<string, string>();
            titles.Add("shopname", DMStoreTour.INSTANCE.CurrentMStore.StoreName);

            Excel.ExcelModel.ExcelBag ebag = new Excel.ExcelModel.ExcelBag
            {
                WorkSheetTitle = titles,
                fileName = "客服现场检查表",
                tables = tables,
                workSheetName = "检查细项"
            };

            var excelHandle = new ExportEvaluationHandle(ebag);
            excelHandle.Work();

            if (excelHandle.IsError)
            {
                MessageBox.Show("导出过程中遇到错误，导出失败，具体错误信息请参考：" + excelHandle.Message);
            }
        }
    }
}