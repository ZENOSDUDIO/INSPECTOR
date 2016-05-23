using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model;
using Honda.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
using Honda.Excel;

namespace Honda.ViewModel
{
    /*
     * 商务政策
     */
    public class BusinessPolicyVM : ViewModelBase
    {
        #region Var 、Construction Fun

        public MSignature[] ListSignature;

        /// <summary>
        /// 商务政策-非纯正零部件列表
        /// </summary>
        private ObservableCollection<MNotPureComponent> _listNotPureComponent;

        public ObservableCollection<MNotPureComponent> ListNotPureComponent
        {
            get
            {
                if (_listNotPureComponent == null)
                {
                    _listNotPureComponent = new ObservableCollection<MNotPureComponent>();
                }

                return _listNotPureComponent;
            }

            set
            {
                if (_listNotPureComponent != value)
                {
                    _listNotPureComponent = value;
                    RaisePropertyChanged("ListNotPureComponent");
                }
            }
        }


        /// <summary>
        /// 商务政策-零部件对外销售属性详情列表1
        /// </summary>
        private ObservableCollection<MComponentESDepartment> _listComponentESDepartment;

        /// <summary>
        /// 商务政策-零部件对外销售属性详情列表1
        /// </summary>
        public ObservableCollection<MComponentESDepartment> ListComponentESDepartment
        {
            get
            {
                if (_listComponentESDepartment == null)
                {
                    _listComponentESDepartment = new ObservableCollection<MComponentESDepartment>();
                }

                return _listComponentESDepartment;
            }
            set
            {
                if (_listComponentESDepartment != value)
                {
                    _listComponentESDepartment = value;
                }
                RaisePropertyChanged("ListComponentESDepartment");
            }
        }

        /// <summary>
        /// 商务政策-零部件对外销售属性详情列表2
        /// </summary>
        private ObservableCollection<MComponentESDepartment2> _listComponentESDepartment2;

        /// <summary>
        /// 商务政策-零部件对外销售属性详情列表2
        /// </summary>
        public ObservableCollection<MComponentESDepartment2> ListComponentESDepartment2
        {
            get
            {
                if (_listComponentESDepartment2 == null)
                {
                    _listComponentESDepartment2 = new ObservableCollection<MComponentESDepartment2>();
                }

                return _listComponentESDepartment2;
            }
            set
            {
                if (_listComponentESDepartment2 != value)
                {
                    _listComponentESDepartment2 = value;
                }
                RaisePropertyChanged("ListComponentESDepartment2");
            }
        }

        /// <summary>
        /// 证据
        /// </summary>
        private ObservableCollection<MFileData> _attachment;

        public ObservableCollection<MFileData> AttachmentList
        {
            get { return _attachment; }
            set
            {
                if (_attachment != value)
                {
                    _attachment = value;
                    RaisePropertyChanged("AttachmentList");
                }
            }
        }

        /// <summary>
        /// 商务政策-零部件价格执行列表
        /// </summary>
        private ObservableCollection<MComponentPrice> _listComponentPrice;

        public ObservableCollection<MComponentPrice> ListComponentPrice
        {
            get
            {
                if (_listComponentPrice == null)
                {
                    _listComponentPrice = new ObservableCollection<MComponentPrice>();
                }
                return _listComponentPrice;
            }
            set
            {
                if (_listComponentPrice != value)
                {
                    _listComponentPrice = value;
                    RaisePropertyChanged("ListComponentPrice");
                }
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

        public BusinessPolicyVM()
        {
        }

        #endregion

        public void InitStoreData()
        {
            if (DMStoreTour.INSTANCE.CurrentMStore != null)
            {
                StoreName = DMStoreTour.INSTANCE.CurrentMStore.StoreName;
            }
            //ListSignature = new MSignature[3];

            //加载离线数据
            this.LoadOffineData();
        }

        #region CMD

        public RelayCommand LoadedCommand
        {
            get { return new RelayCommand(() => { InitStoreData(); }); }
        }

        #region  商务政策-非纯正零部件

        /// <summary>
        /// 新增一条商务政策-非纯正零部件
        /// </summary>
        public RelayCommand AddNotPureComponentCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MNotPureComponent item = new MNotPureComponent();
                    ListNotPureComponent.Add(item);
                });
            }
        }

        /// <summary>
        /// 删除一条商务政策-非纯正零部件
        /// </summary>
        public RelayCommand<MNotPureComponent> DeletNotPureComponent
        {
            get
            {
                return new RelayCommand<MNotPureComponent>((notPureComponent) =>
                {
                    if (ListNotPureComponent.Contains(notPureComponent))
                    {
                        ListNotPureComponent.Remove(notPureComponent);
                    }
                });
            }
        }

        /// <summary>
        /// 打开备注窗口(非纯正零部件)
        /// </summary>
        public RelayCommand<MNotPureComponent> OpenRemarkWMCommod
        {
            get
            {
                return new RelayCommand<MNotPureComponent>((ComponentPrice) =>
                {
                    RemarksWindows remakWindows = new RemarksWindows(ComponentPrice.remarks);
                    remakWindows.ShowDialog();
                });
            }
        }


        public RelayCommand<DatePicker> NotPureStartDateTimeValueChangedCommod
        {
            get
            {
                return new RelayCommand<DatePicker>((dateTimeBegin) =>
                {
                    //需要转换成2014-01-0100: 00:00" 的时间格式 string.Format("{0:yyyy-MM-dd-HH: mm:ss}",dt);
                    try
                    {
                        if (dateTimeBegin.SelectedDate != null)
                        {
                            DateTime dt = (DateTime)dateTimeBegin.SelectedDate.Value;
                            string beginTime = string.Format("{0:yyyy-MM-dd 00:00:00}", dt);

                            MNotPureComponent notPureComponent = dateTimeBegin.Tag as MNotPureComponent;
                            if (notPureComponent != null)
                            {
                                notPureComponent.startDate = beginTime;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        public RelayCommand<DatePicker> NotPureEndDateTimeValueChangedCommod
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
                            DateTime dt = (DateTime)dateTimeEnd.SelectedDate.Value;
                            string beginTime = string.Format("{0:yyyy-MM-dd 00:00:00}", dt);

                            MNotPureComponent notPureComponent = dateTimeEnd.Tag as MNotPureComponent;
                            if (notPureComponent != null)
                            {
                                notPureComponent.endDate = beginTime;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        #endregion

        #region 外销：零部件对外销售情况

        //----------------------------------------------------------第一个表格-----------------------------
        /// <summary>
        /// 新增一条(外销：零部件对外销售情况)
        /// </summary>
        public RelayCommand AddComponentESDepartmentCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MComponentESDepartment item = new MComponentESDepartment();
                    ListComponentESDepartment.Add(item);
                });
            }
        }

        /// <summary>
        /// 删除一条(外销：零部件对外销售情况)
        /// </summary>
        public RelayCommand<MComponentESDepartment> DeletComponentESDepartment
        {
            get
            {
                return new RelayCommand<MComponentESDepartment>((ESDepartment) =>
                {
                    if (ListComponentESDepartment.Contains(ESDepartment))
                    {
                        ListComponentESDepartment.Remove(ESDepartment);
                    }
                });
            }
        }

        /// <summary>
        /// 打开备注窗口(外销：零部件对外销售情况第一个表格)
        /// </summary>
        public RelayCommand<MComponentESDepartment> OpenESDepartmentRemarkWMCommod1
        {
            get
            {
                return new RelayCommand<MComponentESDepartment>((ESDepartment) =>
                {
                    RemarksWindows remakWindows = new RemarksWindows(ESDepartment.remrks);
                    remakWindows.ShowDialog();
                });
            }
        }

        /// <summary>
        /// 外销：零部件对外销售情况第一个表格,结束时间选择器的值变化
        /// </summary>
        public RelayCommand<DatePicker> ESDepartmentEndDateTimeValueChangedCommod1
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
                            DateTime dt = (DateTime)dateTimeEnd.SelectedDate.Value;
                            string beginTime = string.Format("{0:yyyy-MM-dd 00:00:00}", dt);

                            MComponentESDepartment Component = dateTimeEnd.Tag as MComponentESDepartment;
                            if (Component != null)
                            {
                                Component.endDate = beginTime;
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
        /// 外销：零部件对外销售情况第一个表格,开始时间选择器的值变化
        /// </summary>
        public RelayCommand<DatePicker> ESDepartmentStartDateTimeValueChangedCommod1
        {
            get
            {
                return new RelayCommand<DatePicker>((dateTimeBegin) =>
                {
                    //需要转换成2014-01-0100: 00:00" 的时间格式 string.Format("{0:yyyy-MM-dd-HH: mm:ss}",dt);
                    try
                    {
                        if (dateTimeBegin.SelectedDate != null)
                        {
                            DateTime dt = (DateTime)dateTimeBegin.SelectedDate.Value;
                            string beginTime = string.Format("{0:yyyy-MM-dd 00:00:00}", dt);

                            MComponentESDepartment Component = dateTimeBegin.Tag as MComponentESDepartment;
                            if (Component != null)
                            {
                                Component.startDate = beginTime;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        //-----------------------------------------------------------------------第二个表格
        /// <summary>
        /// 新增一条(外销：零部件对外销售情况)2
        /// </summary>
        public RelayCommand AddComponentESDepartmentCommand2
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MComponentESDepartment2 item = new MComponentESDepartment2();
                    ListComponentESDepartment2.Add(item);
                });
            }
        }

        /// <summary>
        /// 删除一条(外销：零部件对外销售情况)2
        /// </summary>
        public RelayCommand<MComponentESDepartment2> DeletComponentESDepartment2
        {
            get
            {
                return new RelayCommand<MComponentESDepartment2>((ESDepartment) =>
                {
                    if (ListComponentESDepartment2.Contains(ESDepartment))
                    {
                        ListComponentESDepartment2.Remove(ESDepartment);
                    }
                });
            }
        }

        /// <summary>
        /// 打开备注窗口(外销：零部件对外销售情况第2个表格)
        /// </summary>
        public RelayCommand<MComponentESDepartment2> OpenESDepartmentRemarkWMCommod2
        {
            get
            {
                return new RelayCommand<MComponentESDepartment2>((ESDepartment) =>
                {
                    RemarksWindows remakWindows = new RemarksWindows(ESDepartment.remrks);
                    remakWindows.ShowDialog();
                });
            }
        }

        /// <summary>
        /// 外销：零部件对外销售情况第2个表格,结束时间选择器的值变化
        /// </summary>
        public RelayCommand<DatePicker> ESDepartmentEndDateTimeValueChangedCommod2
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
                            DateTime dt = (DateTime)dateTimeEnd.SelectedDate.Value;
                            string beginTime = string.Format("{0:yyyy-MM-dd 00:00:00}", dt);

                            MComponentESDepartment2 Component = dateTimeEnd.Tag as MComponentESDepartment2;
                            if (Component != null)
                            {
                                Component.endDate = beginTime;
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
        /// 外销：零部件对外销售情况第2个表格,开始时间选择器的值变化
        /// </summary>
        public RelayCommand<DatePicker> ESDepartmentStartDateTimeValueChangedCommod2
        {
            get
            {
                return new RelayCommand<DatePicker>((dateTimeBegin) =>
                {
                    //需要转换成2014-01-0100: 00:00" 的时间格式 string.Format("{0:yyyy-MM-dd-HH: mm:ss}",dt);
                    try
                    {
                        if (dateTimeBegin.SelectedDate != null)
                        {
                            DateTime dt = (DateTime)dateTimeBegin.SelectedDate.Value;
                            string beginTime = string.Format("{0:yyyy-MM-dd 00:00:00}", dt);

                            MComponentESDepartment2 Component = dateTimeBegin.Tag as MComponentESDepartment2;
                            if (Component != null)
                            {
                                Component.startDate = beginTime;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        #endregion

        #region  价格：零部件价格执行

        /// <summary>
        /// 新增一条(价格：零部件价格执行)
        /// </summary>
        public RelayCommand AddComponentPriceCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MComponentPrice item = new MComponentPrice();
                    ListComponentPrice.Add(item);
                });
            }
        }

        /// <summary>
        /// 删除一条(价格：零部件价格执行)
        /// </summary>
        public RelayCommand<MComponentPrice> DeletComponentPrice
        {
            get
            {
                return
                    new RelayCommand<MComponentPrice>((ComponentPrice) => { ListComponentPrice.Remove(ComponentPrice); });
            }
        }

        /// <summary>
        /// 打开备注窗口(价格：零部件价格执行)
        /// </summary>
        public RelayCommand<MComponentPrice> OpenComponentPriceWMCommod
        {
            get
            {
                return new RelayCommand<MComponentPrice>((ComponentPrice) =>
                {
                    RemarksWindows remakWindows = new RemarksWindows(ComponentPrice.remrks);
                    remakWindows.ShowDialog();
                });
            }
        }

        /// <summary>
        /// (价格：零部件价格执行)结束时间选择器的值变化
        /// </summary>
        public RelayCommand<DatePicker> ComponentPriceEndDateTimeValueChangedCommod
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
                            DateTime dt = (DateTime)dateTimeEnd.SelectedDate.Value;
                            string beginTime = string.Format("{0:yyyy-MM-dd 00:00:00}", dt);

                            MComponentPrice Component = dateTimeEnd.Tag as MComponentPrice;
                            if (Component != null)
                            {
                                Component.endDate = beginTime;
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
        /// (价格：零部件价格执行)开始时间选择器的值变化
        /// </summary>
        public RelayCommand<DatePicker> ComponentPriceStartDateTimeValueChangedCommod
        {
            get
            {
                return new RelayCommand<DatePicker>((dateTimeBegin) =>
                {
                    //需要转换成2014-01-0100: 00:00" 的时间格式 string.Format("{0:yyyy-MM-dd-HH: mm:ss}",dt);
                    try
                    {
                        if (dateTimeBegin.SelectedDate != null)
                        {
                            DateTime dt = (DateTime)dateTimeBegin.SelectedDate.Value;
                            string beginTime = string.Format("{0:yyyy-MM-dd 00:00:00}", dt);

                            MComponentPrice Component = dateTimeBegin.Tag as MComponentPrice;
                            if (Component != null)
                            {
                                Component.startDate = beginTime;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        #endregion
        protected void SaveOffineData()
        {
            if (ListNotPureComponent.Count > 0)
            {
                var list = new List<MNotPureComponentClone>();
                foreach (var item in ListNotPureComponent)
                {
                    list.Add(new MNotPureComponentClone
                    {
                        endDate = item.endDate,
                        endDateText = item.endDateText,
                        partName = item.partName,
                        partNo = item.partNo,
                        partNum = item.partNum,
                        price = item.price,
                        provider = item.provider,
                        remarks = item.remarks,
                        startDate = item.startDate,
                        startDateText = item.startDateText

                    });
                }

                SerialHelp.SerialObject(DirectoryHelper.INSTANCE.BusinessPolicyList1Path, list);
            }
            else
            {
                if (File.Exists(DirectoryHelper.INSTANCE.BusinessPolicyList1Path))
                    File.Delete(DirectoryHelper.INSTANCE.BusinessPolicyList1Path);
            }

            if (ListComponentESDepartment.Count > 0)
            {
                var list = new List<MComponentESDepartmentClone>();
                foreach (var item in ListComponentESDepartment)
                {
                    list.Add(new MComponentESDepartmentClone
                    {
                        endDate = item.endDate,
                        exportObject = item.exportObject,
                        exportSaler = item.exportSaler,
                        mainPart = item.mainPart,
                        remrks = item.remrks,
                        partName = item.partName,
                        partNo = item.partNo,
                        partNum = item.partNum,
                        price = item.price,
                        startDate = item.startDate,
                    });
                }
                SerialHelp.SerialObject(DirectoryHelper.INSTANCE.BusinessPolicyList2Path, list);
            }
            else
            {
                if (File.Exists(DirectoryHelper.INSTANCE.BusinessPolicyList2Path))
                    File.Delete(DirectoryHelper.INSTANCE.BusinessPolicyList2Path);
            }

            if (ListComponentESDepartment2.Count > 0)
            {
                var list = new List<MComponentESDepartment2Clone>();
                foreach (var item in ListComponentESDepartment2)
                {
                    list.Add(new MComponentESDepartment2Clone
                    {
                        endDate = item.endDate,
                        exportObject = item.exportObject,
                        remrks = item.remrks,
                        partName = item.partName,
                        partNo = item.partNo,
                        partNum = item.partNum,
                        startDate = item.startDate
                    });
                }
                SerialHelp.SerialObject(DirectoryHelper.INSTANCE.BusinessPolicyList2_1Path, list);
            }
            else
            {
                if (File.Exists(DirectoryHelper.INSTANCE.BusinessPolicyList2_1Path))
                    File.Delete(DirectoryHelper.INSTANCE.BusinessPolicyList2_1Path);
            }

            if (ListComponentPrice.Count > 0)
            {
                var list = new List<MComponentPriceClone>();
                foreach (var item in ListComponentPrice)
                {
                    list.Add(new MComponentPriceClone
                    {
                        endDate = item.endDate,
                        attributeId = item.attributeId,
                        normalPrice = item.normalPrice,
                        salePrice = item.salePrice,
                        spread = item.spread,
                        remrks = item.remrks,
                        partName = item.partName,
                        partNo = item.partNo,
                        partNum = item.partNum,
                        startDate = item.startDate
                    });
                }

                SerialHelp.SerialObject(DirectoryHelper.INSTANCE.BusinessPolicyList3Path, list);
            }
            else
            {
                if (File.Exists(DirectoryHelper.INSTANCE.BusinessPolicyList3Path))
                    File.Delete(DirectoryHelper.INSTANCE.BusinessPolicyList3Path);
            }
        }

        protected void LoadOffineData()
        {
            if (File.Exists(DirectoryHelper.INSTANCE.BusinessPolicyList1Path))
            {
                ListNotPureComponent = new ObservableCollection<MNotPureComponent>();

                var list = (List<MNotPureComponentClone>)SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.BusinessPolicyList1Path);

                if (list != null)
                {
                    foreach (var item in list)
                    {
                        ListNotPureComponent.Add(new MNotPureComponent
                        {
                            endDate = item.endDate,
                            endDateText = item.endDateText,
                            partName = item.partName,
                            partNo = item.partNo,
                            partNum = item.partNum,
                            price = item.price,
                            provider = item.provider,
                            remarks = item.remarks,
                            startDate = item.startDate,
                            startDateText = item.startDateText

                        });
                    }
                }

            }
            if (File.Exists(DirectoryHelper.INSTANCE.BusinessPolicyList2Path))
            {
                ListComponentESDepartment = new ObservableCollection<MComponentESDepartment>();

                var list = (List<MComponentESDepartmentClone>)SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.BusinessPolicyList2Path);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        ListComponentESDepartment.Add(new MComponentESDepartment
                        {
                            endDate = item.endDate,
                            exportObject = item.exportObject,
                            exportSaler = item.exportSaler,
                            mainPart = item.mainPart,
                            remrks = item.remrks,
                            partName = item.partName,
                            partNo = item.partNo,
                            partNum = item.partNum,
                            price = item.price,
                            startDate = item.startDate,
                        });
                    }
                }
            }
            if (File.Exists(DirectoryHelper.INSTANCE.BusinessPolicyList2_1Path))
            {
                ListComponentESDepartment2 = new ObservableCollection<MComponentESDepartment2>();

                var list = (List<MComponentESDepartment2Clone>)SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.BusinessPolicyList2_1Path);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        ListComponentESDepartment2.Add(new MComponentESDepartment2
                        {
                            endDate = item.endDate,
                            exportObject = item.exportObject,
                            remrks = item.remrks,
                            partName = item.partName,
                            partNo = item.partNo,
                            partNum = item.partNum,
                            startDate = item.startDate
                        });
                    }
                }
            }
            if (File.Exists(DirectoryHelper.INSTANCE.BusinessPolicyList3Path))
            {
                ListComponentPrice = new ObservableCollection<MComponentPrice>();

                var list = (List<MComponentPriceClone>)SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.BusinessPolicyList3Path);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        ListComponentPrice.Add(new MComponentPrice
                        {
                            endDate = item.endDate,
                            attributeId = item.attributeId,
                            normalPrice = item.normalPrice,
                            salePrice = item.salePrice,
                            spread = item.spread,
                            remrks = item.remrks,
                            partName = item.partName,
                            partNo = item.partNo,
                            partNum = item.partNum,
                            startDate = item.startDate
                        });
                    }
                }
            }
        }

        protected void DeleteOffineData()
        {
            Tools.DeleteDir(DirectoryHelper.INSTANCE.BusinessPolicyDir);
        }

        public RelayCommand ExportDataCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    foreach (MNotPureComponent notp in ListNotPureComponent)
                    {
                        if (string.IsNullOrWhiteSpace(notp.endDate) ||
                            string.IsNullOrWhiteSpace(notp.partName) ||
                            string.IsNullOrWhiteSpace(notp.partNo) ||
                            string.IsNullOrWhiteSpace(notp.partNum) ||
                            string.IsNullOrWhiteSpace(notp.price) ||
                            string.IsNullOrWhiteSpace(notp.provider) ||
                            string.IsNullOrWhiteSpace(notp.startDate)
                            )
                        {
                            System.Windows.MessageBox.Show("（商务政策-非纯正零部件）请输入完整，再导出");
                            return;
                        }

                        if (!isNumberic(notp.price) || !isNumberic(notp.partNum))
                        {
                            System.Windows.MessageBox.Show("（商务政策-非纯正零部件）数量和价格只能输入数字");
                            return;
                        }
                    }

                    foreach (MComponentESDepartment2 notp in ListComponentESDepartment2)
                    {
                        if (string.IsNullOrWhiteSpace(notp.endDate) ||
                            string.IsNullOrWhiteSpace(notp.partName) ||
                            string.IsNullOrWhiteSpace(notp.partNo) ||
                            string.IsNullOrWhiteSpace(notp.partNum) ||
                            string.IsNullOrWhiteSpace(notp.exportObject) ||
                            string.IsNullOrWhiteSpace(notp.startDate)
                            )
                        {
                            System.Windows.MessageBox.Show("（ 零部件对外销售属性详2）请输入完整，再导出");
                            return;
                        }

                        if (!isNumberic(notp.partNum))
                        {
                            System.Windows.MessageBox.Show("（ 零部件对外销售属性详情2）数量和价格只能输入数字");
                            return;
                        }
                    }

                    foreach (MComponentESDepartment notp in ListComponentESDepartment)
                    {
                        if (string.IsNullOrWhiteSpace(notp.endDate) ||
                            string.IsNullOrWhiteSpace(notp.exportSaler) ||
                            string.IsNullOrWhiteSpace(notp.price) ||
                            string.IsNullOrWhiteSpace(notp.startDate) ||
                            string.IsNullOrWhiteSpace(notp.endDate) ||
                            string.IsNullOrWhiteSpace(notp.mainPart)
                            )
                        {
                            System.Windows.MessageBox.Show("（ 零部件对外销售属性详情1）请输入完整，再导出");
                            return;
                        }

                        if (!isNumberic(notp.price) || !isNumberic(notp.partNum))
                        {
                            System.Windows.MessageBox.Show("（ 零部件对外销售属性详情1）数量和价格只能输入数字");
                            return;
                        }
                    }

                    foreach (MComponentPrice notp in ListComponentPrice)
                    {
                        if (string.IsNullOrWhiteSpace(notp.endDate) ||
                            string.IsNullOrWhiteSpace(notp.startDate) ||
                            string.IsNullOrWhiteSpace(notp.partName) ||
                            string.IsNullOrWhiteSpace(notp.partNo) ||
                            string.IsNullOrWhiteSpace(notp.normalPrice) ||
                            string.IsNullOrWhiteSpace(notp.salePrice) ||
                            string.IsNullOrWhiteSpace(notp.spread) ||
                            string.IsNullOrWhiteSpace(notp.partNum)
                            )
                        {
                            System.Windows.MessageBox.Show("(价格：零部件价格执行)请输入完整，再导出");
                            return;
                        }

                        if (!isNumberic(notp.partNum) ||
                            !isNumberic(notp.normalPrice) ||
                            !isNumberic(notp.spread) ||
                            !isNumberic(notp.salePrice)
                            )
                        {
                            System.Windows.MessageBox.Show("(价格：零部件价格执行)数量和价格只能输入数字");
                            return;
                        }
                    }

                    //保存本地数据
                    this.SaveOffineData();

                    //Excel導出邏輯
                    var tables = new List<Honda.Excel.ExcelModel.ExcelTable>();

                    //第一个表格数据 ListNotPureComponent
                    if (ListNotPureComponent != null)
                    {

                        var table = new Honda.Excel.ExcelModel.ExcelTable();
                        tables.Add(table);

                        //构建Excel表格数据部分
                        var excelData = new List<Excel.ExcelModel.DataRow>();
                        var excelGroup = new List<Excel.ExcelModel.DataGroup>();

                        table.TbData = excelData;
                        table.TbGroups = excelGroup;

                        foreach (var item in ListNotPureComponent)
                        {
                            var sdate = DateTime.Parse(item.startDate).ToString("MM/dd/yyyy");
                            var edate = DateTime.Parse(item.endDate).ToString("MM/dd/yyyy");

                            var dr = new Excel.ExcelModel.DataRow
                            {
                                D1 = item.partName,
                                D4 = item.partNo,
                                D6 = item.price,
                                D7 = item.partNum,
                                D8 = string.Format("{0}~{1}", sdate, edate),
                                D10 = item.provider,
                                D13 = item.remarks.content
                            };
                            excelData.Add(dr);
                        }
                    }

                    //第二个表格数据 ListComponentESDepartment
                    if (ListComponentESDepartment != null)
                    {

                        var table = new Honda.Excel.ExcelModel.ExcelTable();
                        tables.Add(table);

                        //构建Excel表格数据部分
                        var excelData = new List<Excel.ExcelModel.DataRow>();
                        var excelGroup = new List<Excel.ExcelModel.DataGroup>();

                        table.TbData = excelData;
                        table.TbGroups = excelGroup;

                        foreach (var item in ListComponentESDepartment)
                        {
                            var sdate = DateTime.Parse(item.startDate).ToString("MM/dd/yyyy");
                            var edate = DateTime.Parse(item.endDate).ToString("MM/dd/yyyy");

                            var dr = new Excel.ExcelModel.DataRow
                            {
                                D1 = item.exportSaler,
                                D4 = item.price,
                                D5 = string.Format("{0}~{1}", sdate, edate),
                                D7 = item.mainPart,
                                D13 = item.remrks.content
                            };
                            excelData.Add(dr);
                        }
                    }
                    //第三个表格数据 ListComponentESDepartment2
                    if (ListComponentESDepartment2 != null)
                    {

                        var table = new Honda.Excel.ExcelModel.ExcelTable();
                        tables.Add(table);

                        //构建Excel表格数据部分
                        var excelData = new List<Excel.ExcelModel.DataRow>();
                        var excelGroup = new List<Excel.ExcelModel.DataGroup>();

                        table.TbData = excelData;
                        table.TbGroups = excelGroup;

                        foreach (var item in ListComponentESDepartment2)
                        {
                            var sdate = DateTime.Parse(item.startDate).ToString("MM/dd/yyyy");
                            var edate = DateTime.Parse(item.endDate).ToString("MM/dd/yyyy");

                            var dr = new Excel.ExcelModel.DataRow
                            {
                                D1 = item.partNo,
                                D3 = item.partName,
                                D6 = item.partNum,
                                D7 = string.Format("{0}~{1}", sdate, edate),
                                D9 = item.exportObject,
                                D13 = item.remrks.content
                            };
                            excelData.Add(dr);
                        }
                    }
                    //第四个表格数据 ListComponentPrice
                    if (ListComponentPrice != null)
                    {

                        var table = new Honda.Excel.ExcelModel.ExcelTable();
                        tables.Add(table);

                        //构建Excel表格数据部分
                        var excelData = new List<Excel.ExcelModel.DataRow>();
                        var excelGroup = new List<Excel.ExcelModel.DataGroup>();

                        table.TbData = excelData;
                        table.TbGroups = excelGroup;


                        foreach (var item in ListComponentPrice)
                        {
                            var sdate = DateTime.Parse(item.startDate).ToString("MM/dd/yyyy");
                            var edate = DateTime.Parse(item.endDate).ToString("MM/dd/yyyy");

                            var dr = new Excel.ExcelModel.DataRow
                            {
                                D1 = item.partNo,
                                D3 = item.partName,
                                D6 = item.salePrice,
                                D7 = item.normalPrice,
                                D8 = item.spread,
                                D9 = item.partNum,
                                D10 = string.Format("{0}~{1}", sdate, edate),
                                D13 = item.remrks.content
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
                        fileName = "巡回评价商务政策表",
                        tables = tables,
                        workSheetName = "3-商务政策"
                    };

                    var excelHandle = new ExportBusinessHandle(ebag);
                    excelHandle.Work();


                });
            }
        }
        public RelayCommand UploadDataCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    //if (ListSignature != null)
                    //{
                    //    bool isFinishSignature = true;
                    //    foreach (MSignature sign in ListSignature)
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

                    foreach (MNotPureComponent notp in ListNotPureComponent)
                    {
                        if (string.IsNullOrWhiteSpace(notp.endDate) ||
                            string.IsNullOrWhiteSpace(notp.partName) ||
                            string.IsNullOrWhiteSpace(notp.partNo) ||
                            string.IsNullOrWhiteSpace(notp.partNum) ||
                            string.IsNullOrWhiteSpace(notp.price) ||
                            string.IsNullOrWhiteSpace(notp.provider) ||
                            string.IsNullOrWhiteSpace(notp.startDate)
                            )
                        {
                            System.Windows.MessageBox.Show("（商务政策-非纯正零部件）请输入完整，再提交");
                            return;
                        }

                        if (!isNumberic(notp.price) || !isNumberic(notp.partNum))
                        {
                            System.Windows.MessageBox.Show("（商务政策-非纯正零部件）数量和价格只能输入数字");
                            return;
                        }
                    }

                    foreach (MComponentESDepartment2 notp in ListComponentESDepartment2)
                    {
                        if (string.IsNullOrWhiteSpace(notp.endDate) ||
                            string.IsNullOrWhiteSpace(notp.partName) ||
                            string.IsNullOrWhiteSpace(notp.partNo) ||
                            string.IsNullOrWhiteSpace(notp.partNum) ||
                            string.IsNullOrWhiteSpace(notp.exportObject) ||
                            string.IsNullOrWhiteSpace(notp.startDate)
                            )
                        {
                            System.Windows.MessageBox.Show("（ 零部件对外销售属性详2）请输入完整，再提交");
                            return;
                        }

                        if (!isNumberic(notp.partNum))
                        {
                            System.Windows.MessageBox.Show("（ 零部件对外销售属性详情2）数量和价格只能输入数字");
                            return;
                        }
                    }

                    foreach (MComponentESDepartment notp in ListComponentESDepartment)
                    {
                        if (string.IsNullOrWhiteSpace(notp.endDate) ||
                            string.IsNullOrWhiteSpace(notp.exportSaler) ||
                            string.IsNullOrWhiteSpace(notp.price) ||
                            string.IsNullOrWhiteSpace(notp.startDate) ||
                            string.IsNullOrWhiteSpace(notp.endDate) ||
                            string.IsNullOrWhiteSpace(notp.mainPart)
                            )
                        {
                            System.Windows.MessageBox.Show("（ 零部件对外销售属性详情1）请输入完整，再提交");
                            return;
                        }

                        if (!isNumberic(notp.price) || !isNumberic(notp.partNum))
                        {
                            System.Windows.MessageBox.Show("（ 零部件对外销售属性详情1）数量和价格只能输入数字");
                            return;
                        }
                    }

                    foreach (MComponentPrice notp in ListComponentPrice)
                    {
                        if (string.IsNullOrWhiteSpace(notp.endDate) ||
                            string.IsNullOrWhiteSpace(notp.startDate) ||
                            string.IsNullOrWhiteSpace(notp.partName) ||
                            string.IsNullOrWhiteSpace(notp.partNo) ||
                            string.IsNullOrWhiteSpace(notp.normalPrice) ||
                            string.IsNullOrWhiteSpace(notp.salePrice) ||
                            string.IsNullOrWhiteSpace(notp.spread) ||
                            string.IsNullOrWhiteSpace(notp.partNum)
                            )
                        {
                            System.Windows.MessageBox.Show("(价格：零部件价格执行)请输入完整，再提交");
                            return;
                        }

                        if (!isNumberic(notp.partNum) ||
                            !isNumberic(notp.normalPrice) ||
                            !isNumberic(notp.spread) ||
                            !isNumberic(notp.salePrice)
                            )
                        {
                            System.Windows.MessageBox.Show("(价格：零部件价格执行)数量和价格只能输入数字");
                            return;
                        }
                    }
                    DMBusinessPolicy.INSTANCE.UpLoadBusinessPolicy(ListNotPureComponent, ListComponentESDepartment,
                        ListComponentESDepartment2,
                        ListComponentPrice, ListSignature, (msg, isSucceed) =>
                        {
                            if (isSucceed)
                            {
                                System.Windows.MessageBox.Show("上传成功");
                                //刷新巡回评价管理页面命令
                                GlobalValue.NEED_REFRESH_STORE_TORE = true;

                                Messenger.Default.Send("巡回评价管理", GlobalValue.COMMAND_MAIN_PAGE);

                                //清除离线数据
                                this.DeleteOffineData();
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("上传失败");
                            }
                        });
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

                    bool isSucceed = DoSignature(FileName, GlobalValue._BUSINESS_POLICY_VALUATOR);
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

                    bool isSucceed = DoSignature(FileName, GlobalValue._BUSINESS_POLICY_COMPONT_MANGER);
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

        public RelayCommand OpenSignatureGeneralWMCommand
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
                        FileName = DirectoryHelper.INSTANCE.Signature_produce_generalManager_path;
                    }

                    bool isSucceed = DoSignature(FileName, GlobalValue._BUSINESS_POLICY_GENERAL_MANAGER);
                    if (isSucceed)
                    {
                        MSignature signatrue = new MSignature();
                        signatrue._signature_type = SIGNATURE_TYPE.generalManager;
                        signatrue.signatureFileName = FileName;
                        ListSignature[2] = signatrue;
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


        public RelayCommand<string> InPutCommand
        {
            get
            {
                return new RelayCommand<string>((strNo) =>
                {
                    //if (!isNumberic(strNo))
                    //{
                    //    System.Windows.MessageBox.Show("请输入数字");
                    //}
                });
            }
        }

        public bool isNumberic(string _string)
        {
            if (string.IsNullOrEmpty(_string))
                return true;
            foreach (char c in _string)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        #endregion
    }
}