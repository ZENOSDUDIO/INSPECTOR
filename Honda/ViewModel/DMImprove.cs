using Honda.Globals;
using Honda.HttpLib;
using Honda.HttpLib.JsonInputData;
using Honda.Interface;
using Honda.Model;
using Honda.Model.Form;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Honda.ViewModel
{
    /// <summary>
    ///  改善计划 管理
    ///  <author>xiang</author>
    ///  <date>2014-12-8</date>
    /// </summary>
    public class DMImprove
    {
        public static DMImprove INSTANCE = new DMImprove();

        public MSignature[] ListSignature;

        /// <summary>
        /// 改善计划列表
        /// </summary>
        private ObservableCollection<MImprove> _lst = new ObservableCollection<MImprove>();

        /// <summary>
        /// 改善计划列表
        /// </summary>
        public ObservableCollection<MImprove> ItemList
        {
            get { return _lst; }
            set { _lst = value; }
        }

        /// <summary>
        /// 改善计划审核列表
        /// </summary>
        private ObservableCollection<MImproveCheck> _lstCheck = new ObservableCollection<MImproveCheck>();

        /// <summary>
        /// 改善计划审核列表
        /// </summary>
        public ObservableCollection<MImproveCheck> ItemCheckList
        {
            get { return _lstCheck; }
            set { _lstCheck = value; }
        }

        private DMImprove()
        {
            ListSignature = new MSignature[5];
        }

        #region 获取改善计划列表

        private ReqGetImproveList cmd;

        public void GetImproveList(Action<bool, string> action)
        {
            cmd = new ReqGetImproveList((obj) =>
            {
                //ReqGetImproveList dd = obj as ReqGetImproveList;
                if (cmd.m_bIsSuccess)
                {
                    cmd.ParseParam();
                    ItemList = cmd.Items;
                    action(true, "succees");
                }
                else
                {
                    //提示获取失败
                    action(false, "succees");
                }
            });
            cmd.SendHttpRequest();
        }

        #endregion

        #region 上传改善计划

        private ReqUploadImproves cmdUpload;

        public void UploadImprove(Action<bool, string> action)
        {
            cmdUpload = new ReqUploadImproves((obj) =>
            {
                cmdUpload.ParseParam();
                if (cmdUpload.m_bIsSuccess)
                {
                    action(true, "操作成功！");
                }
                else
                {
                    action(false, cmdUpload.m_strErrorMsg);
                }
            }, ItemList, ListSignature);
            cmdUpload.SendHttpRequest();
        }

        //void CreateSignFile()
        //{
        //    for (int i = 0; i < ItemList.Count; i++)
        //    {
        //        ItemList[i].priority = new Random().Next(1, 4).ToString();
        //        ItemList[i].description = "XXXXXXXXXXXXXXX";
        //        ItemList[i].finishTime = "2014-12-08 17:23:34";
        //        ItemList[i].responsiblePerson = "aaaaaaaaa";
        //        ItemList[i].isIgnore = "2";
        //    }
        //    MSignature obj = new MSignature();
        //    obj._signature_type = SIGNATURE_TYPE.valuator;
        //    obj.signatureFileName = "1.bmp";
        //    ListSignature[0] = obj;
        //    obj._signature_type = SIGNATURE_TYPE.valuator;
        //    obj.signatureFileName = "2.bmp";
        //    ListSignature[1] = obj;
        //    obj._signature_type = SIGNATURE_TYPE.componentManager;
        //    obj.signatureFileName = "3.bmp";
        //    ListSignature[2] = obj;
        //    obj._signature_type = SIGNATURE_TYPE.servesManager;
        //    obj.signatureFileName = "4.bmp";
        //    ListSignature[3] = obj;
        //    obj._signature_type = SIGNATURE_TYPE.generalManager;
        //    obj.signatureFileName = "5.bmp";
        //    ListSignature[4] = obj;


        //}

        #endregion

        #region 获取改善计划审核列表

        private ReqGetImproveCheckList cmdGetCheckList;

        public void GetImproveCheckList(Action<bool, string> action)
        {
            cmdGetCheckList = new ReqGetImproveCheckList((obj) =>
            {
                //ReqGetImproveList dd = obj as ReqGetImproveList;
                if (cmdGetCheckList.m_bIsSuccess)
                {
                    cmdGetCheckList.ParseParam();
                    ItemCheckList = cmdGetCheckList.Items;
                    action(true, "succees");
                }
                else
                {
                    //提示获取失败
                    action(false, "succees");
                }
            });
            cmdGetCheckList.SendHttpRequest();
        }

        #endregion

        #region 上传改善计划审核列表

        private ReqUploadImprovesCheck cmdUploadCheckList;

        public void UploadImproveCheck(Action<bool, string> action)
        {
            //需修改 去掉下面的测试数据

            cmdUploadCheckList = new ReqUploadImprovesCheck((obj) =>
            {
                cmdUploadCheckList.ParseParam();
                if (cmdUploadCheckList.m_bIsSuccess)
                {
                    action(true, "操作成功！");
                }
                else
                {
                    action(false, cmdUpload.m_strErrorMsg);
                }
            }, ItemCheckList);
            cmdUploadCheckList.SendHttpRequest();
        }

        #endregion

        #region 获取导出接口文件

        private ReqExportFile cmdExportFile;

        public void GetExportFile(MExportFileMark mexportMark, Action<bool, MExportFileMark, string, string> action)
        {
            cmdExportFile = new ReqExportFile((obj) =>
            {
                List<string> list = new List<string>();

                cmdExportFile.ParseParam();
                if (cmdExportFile.m_bIsSuccess)
                {
                    //获取需要下载的文件地址
                    mexportMark.excelURL = cmdExportFile.excelUrl;
                    mexportMark.pdfURL = cmdExportFile.pdfUrl;

                    //创建相应文件夹
                    var excelfilename = GetFilename(mexportMark.excelURL);
                    var excelPath = string.Format(@"{0}\{1}", DirectoryHelper.APP_DATA_BASE_PATH,
                        mexportMark.excelURL.Replace("/", @"\").Replace("impUploadFile", ""));
                    Tools.MakeSureDirectory(excelPath);
                    mexportMark.excelPath = excelPath;

                    var pdffilename = GetFilename(mexportMark.pdfURL);
                    var pdfPath = string.Format(@"{0}\{1}", DirectoryHelper.APP_DATA_BASE_PATH,
                        mexportMark.pdfURL.Replace("/", @"\").Replace("impUploadFile", ""));
                    Tools.MakeSureDirectory(pdfPath);
                    mexportMark.pdfPath = pdfPath;

                    //下载文件
                    if (!string.IsNullOrEmpty(mexportMark.excelURL))
                    {
                        cmdExportFile.DownloadFile(cmdExportFile.excelUrl, excelPath, excelfilename,
                            (result, msg) => { action(result, mexportMark, excelfilename, msg); });
                    }
                    if (!string.IsNullOrEmpty(mexportMark.pdfURL))
                    {
                        cmdExportFile.DownloadFile(cmdExportFile.pdfUrl, pdfPath, pdffilename,
                            (result, msg) => { action(result, mexportMark, pdffilename, msg); });
                    }
                }
                else
                {
                    list.Add(cmdExportFile.m_strErrorMsg);
                    action(false, mexportMark, "DOWNLOAD_INTERFACE_ERROR", cmdExportFile.m_strErrorMsg);
                }
            }, mexportMark);
            cmdExportFile.SendHttpRequest();
        }

        private static string GetFilename(string URL)
        {
            return URL.Substring(URL.LastIndexOf("/") + 1, URL.Length - URL.LastIndexOf("/") - 1);
        }

        #endregion
    }
}