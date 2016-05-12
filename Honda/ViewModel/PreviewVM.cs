using System.Collections.ObjectModel;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model;
using Honda.Model.Form;
using Honda.View;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Honda.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PreviewVM : ViewModelBase
    {
        #region Constraction、 Var

        public PreviewPage _thisPage { get; set; }

        private NavigationService _navigate;

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

        private string _signature_valuator1;

        public string Signature_valuator1
        {
            get
            {
                if (string.IsNullOrEmpty(_signature_valuator1))
                {
                    _signature_valuator1 = "";
                }
                return _signature_valuator1;
            }
            set
            {
                if (_signature_valuator1 != value)
                {
                    _signature_valuator1 = value;
                    RaisePropertyChanged("Signature_valuator1");
                }
            }
        }

        /// <summary>
        /// 评估二级表单数据
        /// </summary>
        private ObservableCollection<M_BaseUnivesalsSource> _listEvaData =
            new ObservableCollection<M_BaseUnivesalsSource>();

        public ObservableCollection<M_BaseUnivesalsSource> ListEvaData
        {
            get { return _listEvaData; }
            set
            {
                if (_listEvaData != value)
                {
                    _listEvaData = value;
                    RaisePropertyChanged("ListEvaData");
                }
            }
        }

        /// <summary>
        /// 评估一级菜单
        /// </summary>
        private ObservableCollection<MEvaluateMenu> _listMenu = new ObservableCollection<MEvaluateMenu>();

        public ObservableCollection<MEvaluateMenu> ListEvaMenu
        {
            get { return _listMenu; }
            set
            {
                if (_listMenu != value)
                {
                    _listMenu = value;
                    RaisePropertyChanged("ListEvaMenu");
                }
            }
        }

        public PreviewVM()
        {
        }

        #endregion

        private void InitData()
        {
            if (DMStoreTour.INSTANCE.CurrentMStore != null)
            {
                StoreName = DMStoreTour.INSTANCE.CurrentMStore.StoreName;
            }

            //一级菜单数据
            this.ListEvaMenu = DMUnivesalEvaluate.INSTANCE.ListUniversalMenu;

            //全部的二级表单数据
            if (DMStoreTour.INSTANCE.CurrentMStore != null)
                this.ListEvaData = DMUnivesalEvaluate.INSTANCE.DataBaseUniversal[DMStoreTour.INSTANCE.CurrentMStore.shopId];
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

        public RelayCommand LoadCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    InitData();
                    Messenger.Default.Send("Sucess", GlobalValue._PREVIEW_LOAD_DATA);
                });
            }
        }

        #endregion
    }
}