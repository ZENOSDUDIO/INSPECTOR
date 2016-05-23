using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model;
using Honda.View;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using Honda.Excel;

namespace Honda.ViewModel
{
    /*
     * 商务政策
     */
    public class ImproveVM : ViewModelBase
    {
        #region Var 、Construction Fun

        private string _topbartitle1 = "折叠";

        public string Topbartitle1
        {
            get { return _topbartitle1; }
            set
            {
                if (value != _topbartitle1)
                {
                    _topbartitle1 = value;
                    RaisePropertyChanged("Topbartitle1");
                }
            }
        }

        private string _topbartitle2 = "折叠";

        public string Topbartitle2
        {
            get { return _topbartitle2; }
            set
            {
                if (value != _topbartitle2)
                {
                    _topbartitle2 = value;
                    RaisePropertyChanged("Topbartitle2");
                }
            }
        }

        /// <summary>
        ///  是否显示列表
        /// </summary>
        private Visibility _bIsShowImproveList = Visibility.Visible;

        public Visibility bIsShowImproveList
        {
            get { return _bIsShowImproveList; }
            set
            {
                if (_bIsShowImproveList != value)
                {
                    _bIsShowImproveList = value;
                    RaisePropertyChanged("bIsShowImproveList");
                }
            }
        }

        /// <summary>
        ///  是否显示忽略列表
        /// </summary>
        private Visibility _bIsShowImproveIgnoreList = Visibility.Visible;

        public Visibility bIsShowImproveIgnoreList
        {
            get { return _bIsShowImproveIgnoreList; }
            set
            {
                if (_bIsShowImproveIgnoreList != value)
                {
                    _bIsShowImproveIgnoreList = value;
                    RaisePropertyChanged("bIsShowImproveIgnoreList");
                }
            }
        }


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
            private set
            {
                _lst = value;
                RaisePropertyChanged("ItemList");
            }
        }


        /// <summary>
        /// 改善计划列表(已忽略)
        /// </summary>
        private ObservableCollection<MImprove> _lstImprove = new ObservableCollection<MImprove>();

        /// <summary>
        /// 改善计划列表(已忽略)
        /// </summary>
        public ObservableCollection<MImprove> ItemListImprove
        {
            get { return _lstImprove; }
            private set
            {
                _lstImprove = value;
                RaisePropertyChanged("ItemListImprove");
            }
        }


        /// <summary>
        /// 商店名
        /// </summary>
        private string _storeName;

        public string StoreName
        {
            get
            {
                if (string.IsNullOrEmpty(_storeName))
                {
                    _storeName = "";
                }

                return _storeName;
            }
            set
            {
                if (_storeName != value)
                {
                    _storeName = value;
                    RaisePropertyChanged("StoreName");
                }
            }
        }


        public ImproveVM()
        {
        }

        #endregion

        public void InitStoreData()
        {
            if (DMStoreTour.INSTANCE.CurrentMStore != null)
            {
                StoreName = DMStoreTour.INSTANCE.CurrentMStore.StoreName;
            }
            ListSignature = new MSignature[4];

            if (File.Exists(DirectoryHelper.INSTANCE.ImprovePlanPath) || File.Exists(DirectoryHelper.INSTANCE.ImprovePlanIgnorePath))
            {
                this.LoadOffineData();
            }
            else
            {
                DMImprove.INSTANCE.GetImproveList((isSuccess, msg) =>
                {
                    if (isSuccess)
                    {
                        ItemList = DMImprove.INSTANCE.ItemList;
                    }
                });
            }
        }

        #region CMD

        public RelayCommand LoadedCommand
        {
            get { return new RelayCommand(InitStoreData); }
        }
        protected void SaveOffineData()
        {
            if (ItemList.Count > 0)
            {
                var list = new List<MImproveClone>();
                foreach (var item in ItemList)
                {
                    list.Add(new MImproveClone
                    {
                        finishTime = item.finishTime,
                        bIsShowIgnore = item.bIsShowIgnore,
                        bIsShowRecover = item.bIsShowRecover,
                        description = item.description,
                        id = item.id,
                        isIgnore = item.isIgnore,
                        middName = item.middName,
                        ListPriority = item.ListPriority,
                        minName = item.minName,
                        pgrId = item.pgrId,
                        priority = item.priority,
                        responsiblePerson = item.responsiblePerson,
                        SelectPriority = item.SelectPriority,
                        smallName = item.smallName,
                        strNo = item.strNo
                    });
                }

                SerialHelp.SerialObject(DirectoryHelper.INSTANCE.ImprovePlanPath, list);
            }
            else
            {
                if (File.Exists(DirectoryHelper.INSTANCE.ImprovePlanPath))
                    File.Delete(DirectoryHelper.INSTANCE.ImprovePlanPath);
            }

            if (ItemListImprove.Count > 0)
            {
                var list = new List<MImproveClone>();
                foreach (var item in ItemListImprove)
                {
                    list.Add(new MImproveClone
                    {
                        finishTime = item.finishTime,
                        bIsShowIgnore = item.bIsShowIgnore,
                        bIsShowRecover = item.bIsShowRecover,
                        description = item.description,
                        id = item.id,
                        isIgnore = item.isIgnore,
                        middName = item.middName,
                        ListPriority = item.ListPriority,
                        minName = item.minName,
                        pgrId = item.pgrId,
                        priority = item.priority,
                        responsiblePerson = item.responsiblePerson,
                        SelectPriority = item.SelectPriority,
                        smallName = item.smallName,
                        strNo = item.strNo
                    });
                }
                SerialHelp.SerialObject(DirectoryHelper.INSTANCE.ImprovePlanIgnorePath, list);
            }
            else
            {
                if (File.Exists(DirectoryHelper.INSTANCE.ImprovePlanIgnorePath))
                    File.Delete(DirectoryHelper.INSTANCE.ImprovePlanIgnorePath);
            }

        }

        protected void LoadOffineData()
        {
            if (File.Exists(DirectoryHelper.INSTANCE.ImprovePlanPath))
            {
                ItemList = new ObservableCollection<MImprove>();

                var list = (List<MImproveClone>)SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.ImprovePlanPath);

                if (list != null)
                {
                    foreach (var item in list)
                    {
                        ItemList.Add(new MImprove
                        {
                            finishTime = item.finishTime,
                            bIsShowIgnore = item.bIsShowIgnore,
                            bIsShowRecover = item.bIsShowRecover,
                            description = item.description,
                            id = item.id,
                            isIgnore = item.isIgnore,
                            middName = item.middName,
                            ListPriority = item.ListPriority,
                            minName = item.minName,
                            pgrId = item.pgrId,
                            priority = item.priority,
                            responsiblePerson = item.responsiblePerson,
                            SelectPriority = item.SelectPriority,
                            smallName = item.smallName,
                            strNo = item.strNo
                        });
                    }
                }

            }
            if (File.Exists(DirectoryHelper.INSTANCE.ImprovePlanIgnorePath))
            {
                ItemListImprove = new ObservableCollection<MImprove>();

                var list = (List<MImproveClone>)SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.ImprovePlanIgnorePath);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        ItemListImprove.Add(new MImprove
                        {
                            finishTime = item.finishTime,
                            bIsShowIgnore = item.bIsShowIgnore,
                            bIsShowRecover = item.bIsShowRecover,
                            description = item.description,
                            id = item.id,
                            isIgnore = item.isIgnore,
                            middName = item.middName,
                            ListPriority = item.ListPriority,
                            minName = item.minName,
                            pgrId = item.pgrId,
                            priority = item.priority,
                            responsiblePerson = item.responsiblePerson,
                            SelectPriority = item.SelectPriority,
                            smallName = item.smallName,
                            strNo = item.strNo
                        });
                    }
                }
            }
        }

        protected void DeleteOffineData()
        {
            Tools.DeleteDir(DirectoryHelper.INSTANCE.ImprovePlanDir);
        }

        public RelayCommand ExportDataCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    foreach (MImprove improve in ItemList)
                    {
                        if (string.IsNullOrWhiteSpace(improve.description) ||
                            string.IsNullOrWhiteSpace(improve.finishTime) ||
                            string.IsNullOrWhiteSpace(improve.responsiblePerson) ||
                            (improve.SelectPriority == null) ||
                            string.IsNullOrWhiteSpace(improve.SelectPriority.priorityId))
                        {
                            System.Windows.MessageBox.Show("未忽略项：序号为" + improve.strNo + "优先级，问题描述，完成时间，责任人,不能为空");
                            return;
                        }

                    }

                    foreach (MImprove improve in ItemListImprove)
                    {
                        if (string.IsNullOrWhiteSpace(improve.description) ||
                            string.IsNullOrWhiteSpace(improve.finishTime) ||
                            string.IsNullOrWhiteSpace(improve.responsiblePerson) ||
                            (improve.SelectPriority == null) ||
                            string.IsNullOrWhiteSpace(improve.SelectPriority.priorityId))
                        {
                            System.Windows.MessageBox.Show("忽略项：序号为" + improve.strNo + "优先级，问题描述，完成时间，责任人,不能为空");
                            return;
                        }

                    }

                    //保存本地数据
                    this.SaveOffineData();

                    //Excel導出邏輯
                    var tables = new List<Honda.Excel.ExcelModel.ExcelTable>();

                    //第一个表格数据 
                    if (ItemList != null)
                    {
                        var table = new Honda.Excel.ExcelModel.ExcelTable();
                        tables.Add(table);

                        //构建Excel表格数据部分
                        var excelData = new List<Excel.ExcelModel.DataRow>();
                        var excelGroup = new List<Excel.ExcelModel.DataGroup>();

                        table.TbData = excelData;
                        table.TbGroups = excelGroup;

                        foreach (var item in ItemList)
                        {
                            var dr = new Excel.ExcelModel.DataRow
                            {
                                D1 = item.strNo,
                                D2 = item.middName,
                                D3 = item.smallName,
                                D4 = item.minName,
                                D7 = item.SelectPriority.priorityName,
                                D8 = item.description,
                                D13 = item.finishTime,
                                D14 = item.responsiblePerson
                            };
                            excelData.Add(dr);
                        }
                    }

                    //第二个表格数据 
                    if (ItemListImprove != null)
                    {

                        var table = new Honda.Excel.ExcelModel.ExcelTable();
                        tables.Add(table);

                        //构建Excel表格数据部分
                        var excelData = new List<Excel.ExcelModel.DataRow>();
                        var excelGroup = new List<Excel.ExcelModel.DataGroup>();

                        table.TbData = excelData;
                        table.TbGroups = excelGroup;

                        foreach (var item in ItemListImprove)
                        {

                            var dr = new Excel.ExcelModel.DataRow
                            {
                                D1 = item.strNo,
                                D2 = item.middName,
                                D3 = item.smallName,
                                D4 = item.minName,
                                D7 = item.SelectPriority.priorityName,
                                D8 = item.description,
                                D13 = item.finishTime,
                                D14 = item.responsiblePerson
                            };
                            excelData.Add(dr);
                        }
                    }

                    //调用并导出Excel
                    var titles = new Dictionary<string, string>();
                    titles.Add("shopname", DMStoreTour.INSTANCE.CurrentMStore.StoreName);
                    titles.Add("shopcode", DMStoreTour.INSTANCE.CurrentMStore.shopId);

                    Excel.ExcelModel.ExcelBag ebag = new Excel.ExcelModel.ExcelBag
                    {
                        WorkSheetTitle = titles,
                        fileName = "改善计划检查表",
                        tables = tables,
                        workSheetName = "改善计划"
                    };

                    var excelHandle = new ExportImproveHandle(ebag);
                    excelHandle.Work();


                });
            }
        }
        public RelayCommand OpenSignatureValuatorWMCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    string FileName = null;
                    if (ListSignature[0] != null)
                    {
                        FileName = ListSignature[0].signatureFileName;
                    }
                    else
                    {
                        FileName = DirectoryHelper.INSTANCE.Signature_produce_valuator1_path;
                    }

                    bool isSucceed = DoSignature(FileName, GlobalValue._IMPROVE_VALUATOR1);
                    if (isSucceed)
                    {
                        MSignature signatrue = new MSignature();
                        signatrue._signature_type = SIGNATURE_TYPE.valuator;
                        signatrue.signatureFileName = FileName;
                        ListSignature[0] = signatrue;
                    }
                });
            }
        }

        //public RelayCommand OpenSignatureValuatorWMCommand2
        //{
        //    get
        //    {

        //        return new RelayCommand(() =>
        //        {
        //            string FileName = null;
        //            if (ListSignature[1] != null)
        //            {
        //                FileName = ListSignature[1].signatureFileName;
        //            }
        //            else
        //            {
        //                FileName = DirectoryHelper.INSTANCE.Signature_produce_valuator2_path;
        //            }

        //            bool isSucceed = DoSignature(FileName, GlobalValue._IMPROVE_VALUATOR2);
        //            if (isSucceed)
        //            {
        //                MSignature signatrue = new MSignature();
        //                signatrue._signature_type = SIGNATURE_TYPE.valuator;
        //                signatrue.signatureFileName = FileName;
        //                ListSignature[1] = signatrue;
        //            }

        //        });

        //    }

        //}

        public RelayCommand OpenSignatureComponentWMCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    string FileName = null;
                    if (ListSignature[1] != null)
                    {
                        FileName = ListSignature[1].signatureFileName;
                    }
                    else
                    {
                        FileName = DirectoryHelper.INSTANCE.Signature_produce_componentManager_path;
                    }

                    bool isSucceed = DoSignature(FileName, GlobalValue._IMPROVE_COMPONT_MANGER);
                    if (isSucceed)
                    {
                        MSignature signatrue = new MSignature();
                        signatrue._signature_type = SIGNATURE_TYPE.componentManager;
                        signatrue.signatureFileName = FileName;
                        ListSignature[1] = signatrue;
                    }
                });
            }
        }

        public RelayCommand OpenSignatureServeWMCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    string FileName = null;
                    if (ListSignature[2] != null)
                    {
                        FileName = ListSignature[2].signatureFileName;
                    }
                    else
                    {
                        FileName = DirectoryHelper.INSTANCE.Signature_produce_servesManager_path;
                    }

                    bool isSucceed = DoSignature(FileName, GlobalValue._IMPROVE_SERVE_MANGER);
                    if (isSucceed)
                    {
                        MSignature signatrue = new MSignature();
                        signatrue._signature_type = SIGNATURE_TYPE.servesManager;
                        signatrue.signatureFileName = FileName;
                        ListSignature[2] = signatrue;
                    }
                });
            }
        }

        public RelayCommand OpenSignatureGeneralWMCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    string FileName = null;
                    if (ListSignature[3] != null)
                    {
                        FileName = ListSignature[3].signatureFileName;
                    }
                    else
                    {
                        FileName = DirectoryHelper.INSTANCE.Signature_produce_generalManager_path;
                    }

                    bool isSucceed = DoSignature(FileName, GlobalValue._IMPROVE_GENERAL_MANAGER);
                    if (isSucceed)
                    {
                        MSignature signatrue = new MSignature();
                        signatrue._signature_type = SIGNATURE_TYPE.generalManager;
                        signatrue.signatureFileName = FileName;
                        ListSignature[3] = signatrue;
                    }
                });
            }
        }

        public RelayCommand<DatePicker> DateTimeValueChangedCommod
        {
            get
            {
                return new RelayCommand<DatePicker>((dateTimeEnd) =>
                {
                    //需要转换成2014-01-0100: 00:00" 的时间格式 string.Format("{0:yyyy-MM-dd-HH: mm:ss}",dt);
                    try
                    {
                        if (dateTimeEnd.SelectedDate != null)
                        {
                            DateTime dt = dateTimeEnd.SelectedDate.Value;
                            string finishTime = string.Format("{0:yyyy-MM-dd}", dt);
                            MImprove improve = dateTimeEnd.Tag as MImprove;
                            if (improve != null)
                            {
                                improve.finishTime = finishTime;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        /// <summary>
        /// 签名
        /// </summary>
        private bool DoSignature(string path, string token)
        {
            SignatureWindow _signature = new SignatureWindow();
            _signature.pictruePath = path;
            _signature.ShowLocationPictrue();
            _signature.ShowDialog();

            if (_signature.IsComplate)
            {
                try
                {
                    Messenger.Default.Send(path, token);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("BusinessPolicyVM类中的DoSignature()方法出错\n" + ex.Message);
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        public RelayCommand UploadDataCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ObservableCollection<MImprove> List = new ObservableCollection<MImprove>();
                    foreach (MImprove improve in ItemList)
                    {
                        if (string.IsNullOrWhiteSpace(improve.description) ||
                            string.IsNullOrWhiteSpace(improve.finishTime) ||
                            string.IsNullOrWhiteSpace(improve.responsiblePerson) ||
                            (improve.SelectPriority == null) ||
                            string.IsNullOrWhiteSpace(improve.SelectPriority.priorityId))
                        {
                            System.Windows.MessageBox.Show("未忽略项：序号为" + improve.strNo + "优先级，问题描述，完成时间，责任人,不能为空");
                            return;
                        }

                        List.Add(improve);
                    }

                    foreach (MImprove improve in ItemListImprove)
                    {
                        if (string.IsNullOrWhiteSpace(improve.description) ||
                            string.IsNullOrWhiteSpace(improve.finishTime) ||
                            string.IsNullOrWhiteSpace(improve.responsiblePerson) ||
                            (improve.SelectPriority == null) ||
                            string.IsNullOrWhiteSpace(improve.SelectPriority.priorityId))
                        {
                            System.Windows.MessageBox.Show("忽略项：序号为" + improve.strNo + "优先级，问题描述，完成时间，责任人,不能为空");
                            return;
                        }

                        List.Add(improve);
                    }

                    if (List.Count == 0)
                    {
                        System.Windows.MessageBox.Show("系统检测无改善记录，无法提交。");
                        return;
                    }

                    ExportFileMarkWindow export = new ExportFileMarkWindow();

                    if ((bool)export.ShowDialog() && export.DialogResult == true)
                    {
                        DMImprove.INSTANCE.ItemList = List;

                        DMImprove.INSTANCE.UploadImprove((isSucceed, msg) =>
                        {
                            if (isSucceed)
                            {
                                System.Windows.MessageBox.Show("提交成功");

                                //刷新巡回评价管理页面命令
                                GlobalValue.NEED_REFRESH_STORE_TORE = true;

                                Messenger.Default.Send("巡回评价管理", GlobalValue.COMMAND_MAIN_PAGE);

                                this.DeleteOffineData();
                            }
                            else
                            {
                                System.Windows.MessageBox.Show(msg);
                            }
                        });

                        //////下载报表文件/////
                        MExportFileMark mark = new MExportFileMark
                        {
                            accountName = DMUser.INSTANCE.CurrentMUser.UserAccount,
                            appriaseId = DMStoreTour.INSTANCE.CurrentMStore.appriaseId,
                            shopId = DMStoreTour.INSTANCE.CurrentMStore.shopId,
                            betterMark = export.bbetterMark,
                            businessMark = export.bbusinessMark,
                            feedBackMark = export.bfeedBackMark,
                            tourMark = export.btourMark,
                            excelMark = export.bExcelMark,
                            pdfMark = export.bPdfMark
                        };
                        DMImprove.INSTANCE.GetExportFile(mark, (result, objmark, filename, msg) =>
                        {
                            if (!(bool)result)
                            {
                                var fileDir = DirectoryHelper.DOWNLOAD_ERROR_DIRECTORY;
                                if (!Directory.Exists(Path.GetDirectoryName(fileDir)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(fileDir));
                                }

                                var serialpath = string.Format("{0}/{1}_{2}.Data",
                                    DMStoreTour.INSTANCE.CurrentMStore.shopId, fileDir, filename);

                                var emark = objmark as MExportFileMark;
                                if (emark != null)
                                {
                                    emark.serialpath = serialpath;
                                    objmark = emark;
                                }

                                //TO-DO 下载有误，序列化对象保存到本地
                                SerialHelp.SerialObject(serialpath, objmark);
                            }
                        });
                    }
                });
            }
        }

        /// <summary>
        /// 忽略
        /// </summary>
        public RelayCommand<MImprove> IgnoreImprove
        {
            get
            {
                return new RelayCommand<MImprove>((improve) =>
                {
                    if (ItemList.Contains(improve))
                    {
                        improve.isIgnore = "2";
                        ItemList.Remove(improve);
                        improve.bIsShowIgnore = Visibility.Collapsed;
                        improve.bIsShowRecover = Visibility.Visible;
                        ItemListImprove.Add(improve);
                    }
                });
            }
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public RelayCommand<MImprove> RecoverImproveCommand
        {
            get
            {
                return new RelayCommand<MImprove>((improve) =>
                {
                    if (ItemListImprove.Contains(improve))
                    {
                        improve.isIgnore = "1";
                        ItemListImprove.Remove(improve);
                        improve.bIsShowIgnore = Visibility.Visible;
                        improve.bIsShowRecover = Visibility.Collapsed;
                        ItemList.Add(improve);
                    }
                });
            }
        }

        private bool isHiden1 = true;

        /// <summary>
        /// 显示或者隐藏第一个列表
        /// </summary>
        public RelayCommand IsShowList1Command
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (isHiden1)
                    {
                        bIsShowImproveList = Visibility.Collapsed;
                        Topbartitle1 = "显示";
                        isHiden1 = false;
                    }
                    else
                    {
                        bIsShowImproveList = Visibility.Visible;
                        Topbartitle1 = "折叠";
                        isHiden1 = true;
                    }
                });
            }
        }


        private bool isHiden2 = true;

        /// <summary>
        /// 显示或者隐藏第2个列表
        /// </summary>
        public RelayCommand IsShowList2Command
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (isHiden2)
                    {
                        bIsShowImproveIgnoreList = Visibility.Collapsed;
                        Topbartitle2 = "显示";
                        isHiden2 = false;
                    }
                    else
                    {
                        bIsShowImproveIgnoreList = Visibility.Visible;
                        Topbartitle2 = "折叠";
                        isHiden2 = true;
                    }
                });
            }
        }

        #endregion
    }
}