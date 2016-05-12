using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model.Form;
using Honda.Model.Form.baseModel;
using Honda.Model;

namespace Honda.ViewModel
{
    /// <summary>
    /// 零部件评价
    /// </summary>
    public class UnivesalEvaluateVM : ViewModelBase
    {
        /// <summary>
        /// 评估二级表单数据
        /// </summary>
        public M_BaseUnivesalsSource CurrentBaseUniversal { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the ComponentEvaluateVM class.
        /// </summary>
        public UnivesalEvaluateVM()
        {
        }

        #region CMD

        public void SetEvaluateSourceByCode(string code)
        {
            var source = ListEvaData.FirstOrDefault(n => n._sourceIdentify == code);

            if (source != null) this.CurrentBaseUniversal = source;
        }

        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    //菜单数据
                    this.ListEvaMenu = DMUnivesalEvaluate.INSTANCE.ListUniversalMenu;

                    //当前表单数据
                    this.CurrentBaseUniversal = DMUnivesalEvaluate.INSTANCE.CurrentBaseUniversal;

                    //全部的表单数据
                    this.ListEvaData = DMUnivesalEvaluate.INSTANCE.DataBaseUniversal[DMStoreTour.INSTANCE.CurrentMStore.shopId];

                    Messenger.Default.Send("Sucess", GlobalValue._COMPONENT_LOAD_DATA);
                });
            }
        }

        #endregion
    }
}