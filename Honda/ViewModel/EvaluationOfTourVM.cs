using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model;
using Honda.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Honda.ViewModel
{
    public class EvaluationOfTourVM : ViewModelBase
    {
        #region Var 、Construction Fun

        public EvaluationOfTourPage _thisPage { get; set; }

        private NavigationService _navigate;

        /// <summary>
        /// 预览页面
        /// </summary>
        private PreviewPage _previewPage;

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


        private ObservableCollection<MPage> lstNavigationItem = new ObservableCollection<MPage>()
        {
            new MPage("服务基础评价"),
            new MPage("服务管理评价"),
            new MPage("零部件评价"),
            new MPage("建议加分项")
        };

        public ObservableCollection<MPage> _lstNavigationItem
        {
            get { return lstNavigationItem; }
        }

        public EvaluationOfTourVM()
        {
        }

        #endregion

        public void InitStoreData()
        {
            if (DMStoreTour.INSTANCE.CurrentMStore != null)
            {
                StoreName = DMStoreTour.INSTANCE.CurrentMStore.StoreName;
            }
            Messenger.Default.Send("表格", GlobalValue.FIRST_FORM_NAVIGATE);
        }

        #region CMD

        /// <summary>
        /// 检查当前是否能导航
        /// </summary>
        /// <returns></returns>
        private bool CheckNavigationServiceCanWork()
        {
            if (_navigate == null)
            {
                if (_thisPage != null)
                {
                    _navigate = NavigationService.GetNavigationService(_thisPage);
                }
            }

            if (_navigate == null)
            {
                return false;
            }
            return true;
        }

        public RelayCommand GoBackCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (CheckNavigationServiceCanWork())
                    {
                        if (_navigate.CanGoBack)
                        {
                            _navigate.GoBack();
                        }
                    }
                });
            }
        }


        public RelayCommand LoadedCommand
        {
            get { return new RelayCommand(() => { InitStoreData(); }); }
        }


        /// <summary>
        /// 导航到预览页面
        /// </summary>
        public RelayCommand NavigateToPreviewPage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (!CheckNavigationServiceCanWork())
                    {
                        return;
                    }

                    if (_previewPage == null)
                    {
                        _previewPage = new PreviewPage();
                    }
                    _navigate.Content = _previewPage;
                });
            }
        }

        /// <summary>
        /// 导航到巡回评价表界面
        /// </summary>
        public RelayCommand<object> SelectedPageCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    MPage item = obj as MPage;
                    if (item == null) return;
                    Messenger.Default.Send(item._pageName, GlobalValue._NAVIGATE_TO);
                });
            }
        }

        #endregion
    }
}