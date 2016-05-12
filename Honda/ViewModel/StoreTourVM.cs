using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model;
using Honda.Model.Form;
using Honda.UserCtrl;
using Honda.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Honda.ViewModel
{
    /// <summary>
    /// 导航到相应的页面
    /// </summary>
    public enum STORE_NAVIGATE
    {
        navigateRecord, //历史记录
        navigateBusinessPolicy, //商务政策
        navigatelightspotAndIdea, //工作亮点与意见需求
        navigateTour, //巡回评价表格
        navigateImprove, //改善计划
        navigateImproveCheck, //改善计划待审核
        navigateSummary, //巡回评价总结报表
        none
    };

    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class StoreTourVM : ViewModelBase
    {
        #region Var

        #region  Binding 是否显示

        /// <summary>
        ///  是否显示“正在加载数据“ 页面
        /// </summary>
        private Visibility bIsShowLoading = Visibility.Collapsed;

        public Visibility _bIsShowLoading
        {
            get { return bIsShowLoading; }
            set
            {
                if (bIsShowLoading != value)
                {
                    bIsShowLoading = value;
                    RaisePropertyChanged("_bIsShowLoading");
                }
            }
        }


        private int _storeIndex;

        public int SelectedStoreIndex
        {
            get
            {
                if (_storeIndex < 0)
                    _storeIndex = 0;
                return _storeIndex;
            }
            set
            {
                if (_storeIndex != value && value >= 0)
                {
                    _storeIndex = value;
                    GlobalValue.STORE_SELECTED_INDEX = value;
                }
            }
        }

        /// <summary>
        /// 是否显示 发布任务和历史记录餐单
        /// </summary>
        private Visibility bIsShowTaskPanel = Visibility.Collapsed;

        public Visibility _bIsShowTaskPanel
        {
            get { return bIsShowTaskPanel; }
            set
            {
                if (bIsShowTaskPanel != value)
                {
                    bIsShowTaskPanel = value;
                    RaisePropertyChanged("_bIsShowTaskPanel");
                }
            }
        }

        /// <summary>
        ///  是否显示StoreTourPage 页面右边的六个菜单
        /// </summary>
        private Visibility bIsShowGroup = Visibility.Collapsed;

        public Visibility _bIsShowGroup
        {
            get { return bIsShowGroup; }
            set
            {
                if (bIsShowGroup != value)
                {
                    bIsShowGroup = value;
                    RaisePropertyChanged("_bIsShowGroup");
                }
            }
        }


        /// <summary>
        ///  是否显示商务政策-待提交
        /// </summary>
        private Visibility bIsShowBusiness_ToSubmit = Visibility.Collapsed;

        public Visibility _bIsShowBusiness_ToSubmit
        {
            get { return bIsShowBusiness_ToSubmit; }
            set
            {
                if (bIsShowBusiness_ToSubmit != value)
                {
                    bIsShowBusiness_ToSubmit = value;
                    RaisePropertyChanged("_bIsShowBusiness_ToSubmit");
                }
            }
        }

        /// <summary>
        ///  是否显示商务政策-已提交
        /// </summary>
        private Visibility bIsShowBusiness_Submited = Visibility.Collapsed;

        public Visibility _bIsShowBusiness_Submited
        {
            get { return bIsShowBusiness_Submited; }
            set
            {
                if (bIsShowBusiness_Submited != value)
                {
                    bIsShowBusiness_Submited = value;
                    RaisePropertyChanged("_bIsShowBusiness_Submited");
                }
            }
        }

        /// <summary>
        ///  是否显示商务政策-未开始
        /// </summary>
        private Visibility bIsShowBusiness_NotStart = Visibility.Collapsed;

        public Visibility _bIsShowBusiness_NotStart
        {
            get { return bIsShowBusiness_NotStart; }
            set
            {
                if (bIsShowBusiness_NotStart != value)
                {
                    bIsShowBusiness_NotStart = value;
                    RaisePropertyChanged("_bIsShowBusiness_NotStart");
                }
            }
        }

        /// <summary>
        ///  是否显示改善计划审核-待提交
        /// </summary>
        private Visibility bIsShowImproveCheck_ToSubmit = Visibility.Collapsed;

        public Visibility _bIsShowImproveCheck_ToSubmit
        {
            get { return bIsShowImproveCheck_ToSubmit; }
            set
            {
                if (bIsShowImproveCheck_ToSubmit != value)
                {
                    bIsShowImproveCheck_ToSubmit = value;
                    RaisePropertyChanged("_bIsShowImproveCheck_ToSubmit");
                }
            }
        }

        /// <summary>
        ///  是否显示改善计划审核-已完成
        /// </summary>
        private Visibility bIsShowImproveCheck_Finsih = Visibility.Collapsed;

        public Visibility _bIsShowImproveCheck_Finsih
        {
            get { return bIsShowImproveCheck_Finsih; }
            set
            {
                if (bIsShowImproveCheck_Finsih != value)
                {
                    bIsShowImproveCheck_Finsih = value;
                    RaisePropertyChanged("_bIsShowImproveCheck_Finsih");
                }
            }
        }

        /// <summary>
        ///  是否显示改善计划-待提交
        /// </summary>
        private Visibility bIsShowImprove_ToSubmit = Visibility.Collapsed;

        public Visibility _bIsShowImprove_ToSubmit
        {
            get { return bIsShowImprove_ToSubmit; }
            set
            {
                if (bIsShowImprove_ToSubmit != value)
                {
                    bIsShowImprove_ToSubmit = value;
                    RaisePropertyChanged("_bIsShowImprove_ToSubmit");
                }
            }
        }

        /// <summary>
        ///  是否显示改善计划-已提交
        /// </summary>
        private Visibility bIsShowImprove_Submited = Visibility.Collapsed;

        public Visibility _bIsShowImprove_Submited
        {
            get { return bIsShowImprove_Submited; }
            set
            {
                if (bIsShowImprove_Submited != value)
                {
                    bIsShowImprove_Submited = value;
                    RaisePropertyChanged("_bIsShowImprove_Submited");
                }
            }
        }


        /// <summary>
        ///  是否显示改善计划-未开始
        /// </summary>
        private Visibility bIsShowImprove_NotStart = Visibility.Collapsed;

        public Visibility _bIsShowImprove_NotStart
        {
            get { return bIsShowImprove_NotStart; }
            set
            {
                if (bIsShowImprove_NotStart != value)
                {
                    bIsShowImprove_NotStart = value;
                    RaisePropertyChanged("_bIsShowImprove_NotStart");
                }
            }
        }


        /// <summary>
        ///  是否显示工作亮点与意见需求-待提交
        /// </summary>
        private Visibility bIsShowLightspot_ToSubmit = Visibility.Collapsed;

        public Visibility _bIsShowLightspot_ToSubmit
        {
            get { return bIsShowLightspot_ToSubmit; }
            set
            {
                if (bIsShowLightspot_ToSubmit != value)
                {
                    bIsShowLightspot_ToSubmit = value;
                    RaisePropertyChanged("_bIsShowLightspot_ToSubmit");
                }
            }
        }

        /// <summary>
        ///  是否显示工作亮点与意见需求-已提交
        /// </summary>
        private Visibility bIsShowLightspot_Submited = Visibility.Collapsed;

        public Visibility _bIsShowLightspot_Submited
        {
            get { return bIsShowLightspot_Submited; }
            set
            {
                if (bIsShowLightspot_Submited != value)
                {
                    bIsShowLightspot_Submited = value;
                    RaisePropertyChanged("_bIsShowLightspot_Submited");
                }
            }
        }


        /// <summary>
        ///  是否显示工作亮点与意见需求-未开始
        /// </summary>
        private Visibility bIsShowLightspot_NotStart = Visibility.Collapsed;

        public Visibility _bIsShowLightspot_NotStart
        {
            get { return bIsShowLightspot_NotStart; }
            set
            {
                if (bIsShowLightspot_NotStart != value)
                {
                    bIsShowLightspot_NotStart = value;
                    RaisePropertyChanged("_bIsShowLightspot_NotStart");
                }
            }
        }


        /// <summary>
        ///  是否显示巡回评价表-待提交
        /// </summary>
        private Visibility bIsShowTour_ToSubmit = Visibility.Collapsed;

        public Visibility _bIsShowTour_ToSubmit
        {
            get { return bIsShowTour_ToSubmit; }
            set
            {
                if (bIsShowTour_ToSubmit != value)
                {
                    bIsShowTour_ToSubmit = value;
                    RaisePropertyChanged("_bIsShowTour_ToSubmit");
                }
            }
        }

        /// <summary>
        ///  是否显示巡回评价表-待提交附件
        /// </summary>
        private Visibility bIsShowTourFile_ToSubmit = Visibility.Collapsed;

        public Visibility _bIsShowTourFile_ToSubmit
        {
            get { return bIsShowTourFile_ToSubmit; }
            set
            {
                if (bIsShowTourFile_ToSubmit != value)
                {
                    bIsShowTourFile_ToSubmit = value;
                    RaisePropertyChanged("_bIsShowTourFile_ToSubmit");
                }
            }
        }

        /// <summary>
        ///  是否显示巡回评价表-待提交图片
        /// </summary>
        private Visibility bIsShowTourIma_ToSubmit = Visibility.Collapsed;

        public Visibility _bIsShowTourIma_ToSubmit
        {
            get { return bIsShowTourIma_ToSubmit; }
            set
            {
                if (bIsShowTourIma_ToSubmit != value)
                {
                    bIsShowTourIma_ToSubmit = value;
                    RaisePropertyChanged("_bIsShowTourIma_ToSubmit");
                }
            }
        }

        /// <summary>
        ///  是否显示巡回评价表-已提交
        /// </summary>
        private Visibility bIsShowTour_Submited = Visibility.Collapsed;

        public Visibility _bIsShowTour_Submited
        {
            get { return bIsShowTour_Submited; }
            set
            {
                if (bIsShowTour_Submited != value)
                {
                    bIsShowTour_Submited = value;
                    RaisePropertyChanged("_bIsShowTour_Submited");
                }
            }
        }

        /// <summary>
        ///  是否显示巡回评价表-未开始
        /// </summary>
        private Visibility bIsShowTour_NotStart = Visibility.Collapsed;

        public Visibility _bIsShowTour_NotStart
        {
            get { return bIsShowTour_NotStart; }
            set
            {
                if (bIsShowTour_NotStart != value)
                {
                    bIsShowTour_NotStart = value;
                    RaisePropertyChanged("_bIsShowTour_NotStart");
                }
            }
        }

        /// <summary>
        ///  是否显示巡回评价表总结报告-未开始
        /// </summary>
        private Visibility bIsShowReport_ToStart = Visibility.Collapsed;

        public Visibility _bIsShowReport_ToStart
        {
            get { return bIsShowReport_ToStart; }
            set
            {
                if (bIsShowReport_ToStart != value)
                {
                    bIsShowReport_ToStart = value;
                    RaisePropertyChanged("_bIsShowReport_ToStart");
                }
            }
        }


        /// <summary>
        ///  是否显示巡回评价表总结报告-待提交
        /// </summary>
        private Visibility bIsShowReport_ToSubmit = Visibility.Collapsed;

        public Visibility _bIsShowReport_ToSubmit
        {
            get { return bIsShowReport_ToSubmit; }
            set
            {
                if (bIsShowReport_ToSubmit != value)
                {
                    bIsShowReport_ToSubmit = value;
                    RaisePropertyChanged("_bIsShowReport_ToSubmit");
                }
            }
        }

        /// <summary>
        ///  是否显示巡回评价表总结报告-已提交
        /// </summary>
        private Visibility bIsShowReport_Submited = Visibility.Collapsed;

        public Visibility _bIsShowReport_Submited
        {
            get { return bIsShowReport_Submited; }
            set
            {
                if (bIsShowReport_Submited != value)
                {
                    bIsShowReport_Submited = value;
                    RaisePropertyChanged("_bIsShowReport_Submited");
                }
            }
        }

        #endregion

        /// <summary>
        /// 当前的商店
        /// </summary>
        private MStore _currentMStore;

        private ObservableCollection<MStore> lstStore;

        /// <summary>
        /// 店列表 
        /// </summary>
        public ObservableCollection<MStore> _lstStore
        {
            get
            {
                if (lstStore == null)
                {
                    lstStore = new ObservableCollection<MStore>();
                }
                return lstStore;
            }
            set
            {
                lstStore = value;
                RaisePropertyChanged("_lstStore");
            }
        }


        public StoreTourPage _thisPage { get; set; }

        private NavigationService _navigate;

        /// <summary>
        /// 评价列表页面
        /// </summary>
        private EvaluationOfTourPage _evalutionPage;

        /// <summary>
        /// 工作亮点
        /// </summary>
        private WorkLightspotAndIdeaPage _workLightspotPage;

        /// <summary>
        /// 评价记录
        /// </summary>
        private RecordPage _recordPage;

        /// <summary>
        /// 商务政策
        /// </summary>
        private BusinessPolicyPage _businessPolicyPage;

        //改善计划待审核
        private ImproveCheckPage _improveCheckPage;

        //巡回评价总价报告
        private SummaryPage _summaryPage;

        //改善计划待审核
        private ImprovePage _improvePage;

        #endregion

        /// <summary>
        /// Initializes a new instance of the StoreTourVM class.
        /// </summary>
        public StoreTourVM()
        {
        }

        /// <summary>
        /// 加载店列表
        /// </summary>
        private void LoadStoreList()
        {
            _bIsShowLoading = Visibility.Visible;

            string loginId = DMUser.INSTANCE.CurrentMUser.UserAccount;
            DMStoreTour.INSTANCE.GetStoreList(loginId, (isSucess, msg) =>
            {
                if (isSucess)
                {
                    _lstStore = DMStoreTour.INSTANCE.listStore;
                }
                else
                {
                    _lstStore =
                        (ObservableCollection<MStore>)
                            SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.STORE_PATH_DATA);
                }

                //设置选中的特约店
                if (GlobalValue.STORE_SELECTED_INDEX < _lstStore.Count)
                {
                    _lstStore[GlobalValue.STORE_SELECTED_INDEX].IsSelected = true;
                    SelectedStoreIndex = GlobalValue.STORE_SELECTED_INDEX;
                }

                _bIsShowLoading = Visibility.Hidden;

                if (_lstStore.Count == 0 || !GlobalValue.NEED_REFRESH_STORE_TORE)
                    return;

                //加载特约店相关UI数据
                if (SelectedStoreIndex < _lstStore.Count)
                {
                    MStore store = _lstStore[SelectedStoreIndex];
                    if (store == null) return;
                    DMStoreTour.INSTANCE.CurrentMStore = store;

                    _currentMStore = store;
                    DirectoryHelper.INSTANCE.CreateStoreFileDirectory(DMStoreTour.INSTANCE.CurrentMStore.shopId);

                    if (DMUser.INSTANCE.isOnline)
                    {
                        LoadForm();
                    }
                    else
                    {
                        //加载离线数据
                        DMPreview.INSTANCE.LoadCurrentSoteForm();
                        SwitchPanelShowOrHiden();
                    }

                    //设置无需刷新特约店页面
                    GlobalValue.NEED_REFRESH_STORE_TORE = false;
                }
            });
        }

        #region 设置StoreTourPage 页右边面板的隐藏与显示

        /// <summary>
        /// 控制店列表页面右边六个按钮的状态的切换。
        /// </summary>
        private void SwitchPanelShowOrHiden()
        {
            SetAllPanelHiden();
            //商务政策
            switch (_currentMStore._BUSEINESS_STATE)
            {
                case ITEM_STATE.NOT_START:
                    _bIsShowBusiness_NotStart = Visibility.Visible;
                    break;
                case ITEM_STATE.SUBMITED:
                    _bIsShowBusiness_Submited = Visibility.Visible;
                    break;
                case ITEM_STATE.TO_SUBMIT:
                    _bIsShowBusiness_ToSubmit = Visibility.Visible;
                    break;
            }

            //改善计划
            switch (_currentMStore._IMPROVE_STATE)
            {
                case ITEM_STATE.NOT_START:
                    _bIsShowImprove_NotStart = Visibility.Visible;
                    break;
                case ITEM_STATE.SUBMITED:
                    _bIsShowImprove_Submited = Visibility.Visible;

                    break;
                case ITEM_STATE.TO_SUBMIT:
                    _bIsShowImprove_ToSubmit = Visibility.Visible;
                    break;

                case ITEM_STATE.CHECK_PENDING:
                    _bIsShowImproveCheck_ToSubmit = Visibility.Visible;
                    break;

                case ITEM_STATE.CHECK_PENDING_FINISH:
                    _bIsShowImproveCheck_Finsih = Visibility.Visible;
                    break;
            }

            //巡回评价列表
            switch (_currentMStore._TOUR_STATE)
            {
                case ITEM_STATE.NOT_START:
                    _bIsShowTour_NotStart = Visibility.Visible;
                    break;
                case ITEM_STATE.SUBMITED:
                    _bIsShowTour_Submited = Visibility.Visible;

                    break;
                case ITEM_STATE.TO_SUBMIT:
                    _bIsShowTour_ToSubmit = Visibility.Visible;
                    _bIsShowTourIma_ToSubmit = Visibility.Visible;
                    _bIsShowTourFile_ToSubmit = Visibility.Collapsed;

                    break;
                case ITEM_STATE.TO_SUBMIT_ACCESSORY:
                    _bIsShowTour_ToSubmit = Visibility.Visible;
                    _bIsShowTourIma_ToSubmit = Visibility.Collapsed;
                    _bIsShowTourFile_ToSubmit = Visibility.Visible;
                    break;
            }


            //工作亮点与意见需求
            switch (_currentMStore._LIGHT_SPOT_STATE)
            {
                case ITEM_STATE.NOT_START:
                    _bIsShowLightspot_NotStart = Visibility.Visible;
                    break;
                case ITEM_STATE.SUBMITED:
                    _bIsShowLightspot_Submited = Visibility.Visible;

                    break;
                case ITEM_STATE.TO_SUBMIT:
                    _bIsShowLightspot_ToSubmit = Visibility.Visible;
                    break;
            }


            //巡回评价表总结报告
            switch (_currentMStore._OVERALL_RATING_REPORT)
            {
                case ITEM_STATE.NOT_START:
                    _bIsShowReport_ToStart = Visibility.Visible;

                    break;
                case ITEM_STATE.TO_SUBMIT:
                    _bIsShowReport_ToSubmit = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// 让店列表页面右边的所有彩电都处于隐藏状态
        /// </summary>
        private void SetAllPanelHiden()
        {
            _bIsShowBusiness_NotStart = Visibility.Collapsed;
            _bIsShowBusiness_Submited = Visibility.Collapsed;
            _bIsShowBusiness_ToSubmit = Visibility.Collapsed;

            _bIsShowTour_NotStart = Visibility.Collapsed;
            _bIsShowTour_Submited = Visibility.Collapsed;
            _bIsShowTour_ToSubmit = Visibility.Collapsed;

            _bIsShowImprove_NotStart = Visibility.Collapsed;
            _bIsShowImprove_Submited = Visibility.Collapsed;
            _bIsShowImprove_ToSubmit = Visibility.Collapsed;
            _bIsShowImproveCheck_ToSubmit = Visibility.Collapsed;
            _bIsShowImproveCheck_Finsih = Visibility.Collapsed;

            _bIsShowLightspot_NotStart = Visibility.Collapsed;
            _bIsShowLightspot_Submited = Visibility.Collapsed;
            _bIsShowLightspot_ToSubmit = Visibility.Collapsed;


            _bIsShowReport_Submited = Visibility.Collapsed;
            _bIsShowReport_ToSubmit = Visibility.Collapsed;
        }

        #endregion

        #region Command

        /// <summary>
        /// 选择商店(店列表页面左边的ListBox)
        /// </summary>
        public RelayCommand<object> SelectedStoreCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    MStore store = obj as MStore;
                    if (store == null) return;
                    DMStoreTour.INSTANCE.CurrentMStore = store;

                    //设置特约店选中状态
                    foreach (var item in _lstStore)
                    {
                        item.IsSelected = false;
                    }
                    store.IsSelected = true;

                    _currentMStore = store;
                    DirectoryHelper.INSTANCE.CreateStoreFileDirectory(DMStoreTour.INSTANCE.CurrentMStore.shopId);

                    if (DMUser.INSTANCE.isOnline)
                    {
                        LoadForm();
                    }
                    else
                    {
                        //加载离线数据
                        DMPreview.INSTANCE.LoadCurrentSoteForm();
                        SwitchPanelShowOrHiden();
                    }
                });
            }
        }

        private List<string> lstFormFile;

        //private bool IsHaveSaveData()
        //{
        //    bool isCanLoadLocationFile = true;
        //    lstFormFile = new List<string>();
        //    //服务基础评价
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.FIVE_SAVE_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.HARDWARE_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.PERSONNEL_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.RECEIVE_GUESTS_FLOW_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.QUICK_SERVICE_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.BP_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.DATA_ACCURACY_LIST);

        //    //服务管理评价
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.SATISFACTION_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.CLIENT_MANAGE_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.FACTORYMANAGE_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.EMPHASIS_BUSINESS_LIST);

        //    //零部件评价
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.BASE_BUSINESS_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.MAR_MARKETING_BUSINESS_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.REPERTORYMANAGE_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.STORE_MANAGEMENT_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.PERSONNELEX_LIST);

        //    //建议加分项
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.SUGGEST_PLUS_PROJECT_LIST);
        //    lstFormFile.Add(DirectoryHelper.INSTANCE.SUGGEST_WAREHOUSE_LIST);

        //    foreach (string filePath in lstFormFile)
        //    {
        //        if (!File.Exists(filePath))
        //        {
        //            isCanLoadLocationFile = false;
        //            return isCanLoadLocationFile;
        //        }
        //    }

        //    return isCanLoadLocationFile;
        //}


        /// <summary>
        /// 导航到巡回评价表
        /// </summary>
        public RelayCommand NavigateToTourCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (!isCanNavigation(_currentMStore._TOUR_STATE)) return;
                    STORE_NAVIGATE navigateToPage = STORE_NAVIGATE.navigateTour;
                    if (_currentMStore._TOUR_STATE == ITEM_STATE.TO_SUBMIT_ACCESSORY)
                    {
                        UploadFilesWindow fileWindow = new UploadFilesWindow();
                        if ((bool)fileWindow.ShowDialog())
                        {
                            string loginId = DMUser.INSTANCE.CurrentMUser.UserAccount;
                            DMStoreTour.INSTANCE.GetStoreList(loginId, (isSucess, msg) =>
                            {
                                if (isSucess)
                                {
                                    _lstStore = DMStoreTour.INSTANCE.listStore;
                                    SerialHelp.SerialObject(DirectoryHelper.INSTANCE.STORE_PATH_DATA, _lstStore);
                                }
                            });
                        }
                    }
                    else
                    {
                        NavigateToPage(navigateToPage);
                    }
                });
            }
        }

        /// <summary>
        /// 导航到改善计划
        /// </summary>
        public RelayCommand NavigateToImproveCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (!isCanNavigation(_currentMStore._IMPROVE_STATE)) return;
                    STORE_NAVIGATE navigateToPage;
                    if (_currentMStore._IMPROVE_STATE == ITEM_STATE.CHECK_PENDING)
                    {
                        navigateToPage = STORE_NAVIGATE.navigateImproveCheck;
                    }
                    else
                    {
                        navigateToPage = STORE_NAVIGATE.navigateImprove;
                    }


                    NavigateToPage(navigateToPage);
                });
            }
        }


        /// <summary>
        /// 导航到商务政策
        /// </summary>
        public RelayCommand NavigateToBusinessCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (!isCanNavigation(_currentMStore._BUSEINESS_STATE)) return;
                    STORE_NAVIGATE navigateToPage = STORE_NAVIGATE.navigateBusinessPolicy;
                    NavigateToPage(navigateToPage);
                });
            }
        }

        /// <summary>
        /// 导航到工作亮点与意见需求
        /// </summary>
        public RelayCommand NavigateToLightspotAndIdeaCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (!isCanNavigation(_currentMStore._LIGHT_SPOT_STATE)) return;
                    STORE_NAVIGATE navigateToPage = STORE_NAVIGATE.navigatelightspotAndIdea;
                    NavigateToPage(navigateToPage);
                });
            }
        }


        /// <summary>
        /// 导航到评价记录
        /// </summary>
        public RelayCommand NavigateToRecordCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    STORE_NAVIGATE navigateToPage = STORE_NAVIGATE.navigateRecord;
                    NavigateToPage(navigateToPage);
                });
            }
        }


        /// <summary>
        /// 导航到巡回评价总结报表
        /// </summary>
        public RelayCommand NavigateToSummaryCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    STORE_NAVIGATE navigateToPage = STORE_NAVIGATE.navigateSummary;
                    NavigateToPage(navigateToPage);
                });
            }
        }

        private bool isCanNavigation(ITEM_STATE state)
        {
            bool isCan = false;
            if (state == ITEM_STATE.NOT_START)
            {
                isCan = false;
            }
            else if (state == ITEM_STATE.SUBMITED)
            {
                isCan = false;
            }
            else if (state == ITEM_STATE.CHECK_PENDING_FINISH)
            {
                isCan = false;
            }
            else
            {
                isCan = true;
            }
            return isCan;
        }


        /// <summary>
        /// 导航到相应的页面
        /// </summary>
        /// <param name="navigateName"></param>
        private void NavigateToPage(STORE_NAVIGATE nav)
        {
            if (_navigate == null)
            {
                _navigate = NavigationService.GetNavigationService(_thisPage);
            }

            if (_navigate == null)
            {
                return;
            }

            switch (nav)
            {
                case STORE_NAVIGATE.navigateBusinessPolicy:
                    if (_businessPolicyPage == null)
                    {
                        _businessPolicyPage = new BusinessPolicyPage();
                    }
                    _navigate.Content = _businessPolicyPage;
                    break;
                case STORE_NAVIGATE.navigateImprove:
                    if (_improvePage == null)
                    {
                        _improvePage = new ImprovePage();
                    }
                    _navigate.Content = _improvePage;
                    break;

                case STORE_NAVIGATE.navigatelightspotAndIdea: //导航到工作亮点与意见需求页面
                    if (_workLightspotPage == null)
                    {
                        _workLightspotPage = new WorkLightspotAndIdeaPage();
                    }
                    _navigate.Content = _workLightspotPage;
                    break;
                case STORE_NAVIGATE.navigateRecord:
                    _recordPage = new RecordPage();
                    _navigate.Content = _recordPage;
                   
                    break;
                case STORE_NAVIGATE.navigateTour: //巡回列表页面
                    if (_evalutionPage == null)
                    {
                        _evalutionPage = new EvaluationOfTourPage();
                    }
                    _navigate.Content = _evalutionPage;
                    break;

                case STORE_NAVIGATE.navigateImproveCheck: //巡回列表页面
                    if (_improveCheckPage == null)
                    {
                        _improveCheckPage = new ImproveCheckPage();
                    }
                    _navigate.Content = _improveCheckPage;
                    break;

                case STORE_NAVIGATE.navigateSummary: //巡回评价总结报告
                    if (_summaryPage == null)
                    {
                        _summaryPage = new SummaryPage();
                    }
                    _navigate.Content = _summaryPage;
                    break;
            }
        }


        /// <summary>
        /// 返回页面
        /// </summary>
        public RelayCommand GoBackCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (_navigate == null)
                    {
                        _navigate = NavigationService.GetNavigationService(_thisPage);
                    }

                    if (_navigate == null)
                    {
                        return;
                    }

                    if (_navigate.CanGoBack)
                    {
                        _navigate.GoBack();
                    }
                });
            }
        }

        /// <summary>
        /// 发布评价任务
        /// </summary>
        public RelayCommand ReleaseTaskCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (_currentMStore != null && _currentMStore.StoreName != null)
                    {
                        TaskWindow taskWindow = new TaskWindow(_currentMStore.StoreName);
                        if ((bool)taskWindow.ShowDialog())
                        {
                        }
                    }
                });
            }
        }


        public RelayCommand LoadCommand
        {
            get { return new RelayCommand(() => { LoadStoreList(); }); }
        }


        //是否成功获取所有的表格
        private bool isSucceedGetData = false;

        private void LoadForm()
        {
            _bIsShowLoading = Visibility.Visible;
            LoadDataCount++;
            DMUnivesalEvaluate.INSTANCE.GetFormListFromServer((isSucess) =>
            {
                if (isSucess)
                {
                    isSucceedGetData = true;
                }
                else
                {
                    isSucceedGetData = false;
                }

                LoadDataCount--;
                HideLoadingGrid();
            });
        }

        /// <summary>
        /// 隐藏正在加载的进度条
        /// </summary>
        private void HideLoadingGrid()
        {
            if (LoadDataCount == 0)
            {
                if (!isSucceedGetData)
                {
                    DMPreview.INSTANCE.LoadCurrentSoteForm();
                    Debug.WriteLine("店id为" + DMStoreTour.INSTANCE.CurrentMStore.shopId + "列表加载不成功");
                }

                if (GlobalValue.Store_Need_Load_Offline_Data_State_Mgr.GetStoreNeedLoadOfflineDataState(_currentMStore))
                {
                    DMPreview.INSTANCE.LoadCurrentSoteForm();
                }

                _bIsShowLoading = Visibility.Hidden;

                if (_currentMStore.taskStatus == "0")
                {
                    _bIsShowTaskPanel = Visibility.Visible;
                    _bIsShowGroup = Visibility.Collapsed;
                }
                else
                {
                    _bIsShowTaskPanel = Visibility.Collapsed;
                    _bIsShowGroup = Visibility.Visible;
                    SwitchPanelShowOrHiden();
                }
            }
        }

        /*
       * 1、当所有请求都有返回数据时，页面在能操作。
       * 2、每当发一次请求时LoadDataCount加1，数据返回一次时，LoadDataCount减1，当LoadDataCount==0时，
       * 表示所有请求完成
       */
        private int LoadDataCount = 0;

        #endregion
    }
}