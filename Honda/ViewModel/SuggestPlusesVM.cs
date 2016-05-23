using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model.Form;

namespace Honda.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SuggestPlusesVM : ViewModelBase
    {
        /// <summary>
        /// 加分项
        /// </summary>
        public M_Suggest_PlusProject_Source _currentPlusProject { get; set; }

        /// <summary>
        /// 五星级仓库
        /// </summary>
        public M_Suggest_Warehouse_Source _currentWarehouse { get; set; }

        public SuggestPlusesVM()
        {
        }

        #region CMD
        public RelayCommand LoadedCommand
        {
            get
            {

                return new RelayCommand(() =>
                {

                    _currentPlusProject = DMSuggestPluses.INSTANCE.currentPlusProject;
                    _currentWarehouse = DMSuggestPluses.INSTANCE.currentWarehouse;
                    Messenger.Default.Send("Sucess", GlobalValue._SUGGEST_PUSES_LOAD_DATA);
                });

            }

        }
        #endregion
    }
}