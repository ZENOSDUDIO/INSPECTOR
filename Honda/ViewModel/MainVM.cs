using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model;
using Honda.Model.Form;
using Honda.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;

namespace Honda.ViewModel
{
    public class MainVM : ViewModelBase
    {
        public MainPage _thisPage { get; set; }

        /// <summary>
        /// 需修改
        /// </summary>
        private bool isFirstUse = true;

        public string dataFistPath = DirectoryHelper.APP_DATA_BASE_PATH + "\\" + "isFirstUse.data";
        private AppState appState;

        public bool IsFirstUser
        {
            get
            {
                appState = (AppState) SerialHelp.LoadFromBinaryFile(dataFistPath);
                if (appState == null)
                {
                    isFirstUse = true;
                }
                else
                {
                    isFirstUse = false;
                }
                return isFirstUse;
            }
            set
            {
                if (isFirstUse != value)
                {
                    isFirstUse = value;
                    if (appState == null)
                    {
                        appState = new AppState();
                    }
                    appState.isFirstUser = value;
                    SerialHelp.SerialObject(dataFistPath, appState);
                }
            }
        }

        private Queue<MStore> QueueStore;

        /*
        * 1、当所有请求都有返回数据时，页面在能操作。
        * 2、每当发一次请求时LoadDataCount加1，数据返回一次时，LoadDataCount减1，当LoadDataCount==0时，
        * 表示所有请求完成
        */
        private int LoadDataCount = 0;

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

        /// <summary>
        ///  第一次启动本应用程序的时候，引导页面
        /// </summary>
        private Visibility bIsShowFirst = Visibility.Collapsed;

        public Visibility _bIsShowFirst
        {
            get { return bIsShowFirst; }
            set
            {
                if (bIsShowFirst != value)
                {
                    bIsShowFirst = value;
                    RaisePropertyChanged("_bIsShowFirst");
                }
            }
        }


        /// <summary>
        ///  是否显示“正在提交数据“ 页面
        /// </summary>
        private Visibility bIsShowCommiting = Visibility.Collapsed;

        public Visibility _bIsShowCommiting
        {
            get { return bIsShowCommiting; }
            set
            {
                if (bIsShowCommiting != value)
                {
                    bIsShowCommiting = value;
                    RaisePropertyChanged("_bIsShowCommiting");
                }
            }
        }


        /// <summary>
        /// 导航
        /// </summary>
        private NavigationService _navigate;

        public MainVM()
        {
        }

        #region CMD

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

                    if (_navigate != null)
                    {
                        if (_navigate.CanGoBack)
                        {
                            _navigate.GoBack();
                        }
                    }
                });
            }
        }

        public RelayCommand LoadCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (!IsFirstUser)
                    {
                        Messenger.Default.Send("主页", GlobalValue.COMMAND_MAIN_PAGE);
                        // LoadStoreList();
                    }
                    else
                    {
                        _bIsShowFirst = Visibility.Visible;
                    }

                    //获取特约店是否加载离线数据的状态
                    if (SerialHelp.CheckFileExsists(DirectoryHelper.INSTANCE.STORE_NEED_LOAD_OFFLINE_DATA))
                        GlobalValue.Store_Need_Load_Offline_Data_State_Mgr =
                            (MStoreOfflineState)
                                SerialHelp.LoadFromBinaryFile(DirectoryHelper.INSTANCE.STORE_NEED_LOAD_OFFLINE_DATA);
                });
            }
        }


        public RelayCommand LoadFormListCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _bIsShowFirst = Visibility.Collapsed;
                    LoadStoreList();
                });
            }
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
                    //把店列表放入队列并开始加载所有特约店列表
                    CreateQueueStore();
                }
                else
                {
                    MessageBox.Show(msg);
                }
            });
        }

        private void CreateQueueStore()
        {
            //把店列表放入队列
            QueueStore = new Queue<MStore>();
            foreach (MStore store in DMStoreTour.INSTANCE.listStore)
            {
                QueueStore.Enqueue(store);
            }

            LoadForm();
        }

        public RelayCommand<string> NavigatedCommand
        {
            get
            {
                return new RelayCommand<string>((msg) => { Messenger.Default.Send(msg, GlobalValue.COMMAND_MAIN_PAGE); });
            }
        }

        /// <summary>
        /// 加载特约店列表
        /// </summary>
        private void LoadForm()
        {
            DMStoreTour.INSTANCE.CurrentMStore = QueueStore.Dequeue();

            LoadDataCount++;
            DMUnivesalEvaluate.INSTANCE.GetFormListFromServer((isSuccess) =>
            {
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
                //加载当前商店的列表之后，开始保存数据到本地
                DirectoryHelper.INSTANCE.CreateStoreFileDirectory(DMStoreTour.INSTANCE.CurrentMStore.shopId);
                DMPreview.INSTANCE.SaveCurrentSoteForm();
                if (QueueStore.Count != 0)
                {
                    LoadForm();
                }
                else
                {
                    _bIsShowLoading = Visibility.Collapsed;
                    IsFirstUser = false;
                    Messenger.Default.Send("日程管理", GlobalValue.COMMAND_MAIN_PAGE);
                }
            }
        }

        /// <summary>
        /// 保存当前列表到本地
        /// </summary>
        private void SaveForm()
        {
            try
            {
                //创建该特约店的保存的目录，并且设置当前特约店的保存目录
                DirectoryHelper.INSTANCE.CreateStoreFileDirectory(DMStoreTour.INSTANCE.CurrentMStore.shopId);

                //保存特约店评价列表
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("保存特约店数据错误\n" + ex.Message);
            }
        }

        #endregion
    }

    [Serializable]
    public class AppState
    {
        public bool isFirstUser;
    }
}