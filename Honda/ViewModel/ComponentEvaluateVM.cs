using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model.Form;

namespace Honda.ViewModel
{
    /// <summary>
    /// 零部件评价
    /// </summary>
    public class ComponentEvaluateVM : ViewModelBase
    {

        /// <summary>
        /// 基础业务数据源
        /// </summary>
        public M_Common_Source _currentBaseBusiness { get; set; }

        /// <summary>
        /// 营销管理
        /// </summary>
        public M_MarMarketingManageSource _currentMarketingManage { get; set; }

        /// <summary>
        /// 库存管理
        /// </summary>
        public M_Common_Source _currentRepertoryManage { get; set; }

        /// <summary>
        /// 仓库管理
        /// </summary>
        public M_Common_Source _currentStoreManagement { get; set; }

        /// <summary>
        /// 人员数据源
        /// </summary>
        public M_Personnel_Source _currentPersnnel { get; set; }

        /// <summary>
        /// Initializes a new instance of the ComponentEvaluateVM class.
        /// </summary>
        public ComponentEvaluateVM()
        {
        }

        #region CMD
        public RelayCommand LoadedCommand
        {
            get
            {

                return new RelayCommand(() =>
                {

                    _currentBaseBusiness = DMComponentEvaluate.INSTANCE.currentBaseBusiness;
                    _currentMarketingManage = DMComponentEvaluate.INSTANCE.currentMarMarketingManage;
                    _currentRepertoryManage = DMComponentEvaluate.INSTANCE.currentRepertoryManage;
                    _currentStoreManagement = DMComponentEvaluate.INSTANCE.currentStoreManagement;
                    _currentPersnnel = DMComponentEvaluate.INSTANCE.currentPersonnel;

                    Messenger.Default.Send("Sucess", GlobalValue._COMPONENT_LOAD_DATA);
                });

            }

        }
        #endregion
    }
}