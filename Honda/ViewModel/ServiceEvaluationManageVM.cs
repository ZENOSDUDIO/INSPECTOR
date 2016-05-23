using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model.Form;

namespace Honda.ViewModel
{
   /// <summary>
   /// 服务管理评价
   /// </summary>
    public class ServiceEvaluationManageVM : ViewModelBase
    {
        /// <summary>
        /// 满意度管理 数据源
        /// </summary>
        public M_Common_Source _currentSatisfactionManagement { get; set; }

        /// <summary>
        /// 客户维系管理 数据源
        /// </summary>
        public M_Common_Source _currentClientMange { get; set; }

        /// <summary>
        /// 来厂促进管理 数据源
        /// </summary>
        public M_Common_Source _currentMangement { get; set; }

        /// <summary>
        /// 重点业务 数据源
        /// </summary>
        public M_Common_Source _currentEmphasisBusiness { get; set; }

        /// <summary>
        /// Initializes a new instance of the ServiceEvaluationManageVM class.
        /// </summary>
        public ServiceEvaluationManageVM()
        {
        }

        #region CMD
        public RelayCommand LoadedCommand
        {
            get
            {

                return new RelayCommand(() =>
                {

                    _currentSatisfactionManagement = DMServiceEvaluationManage.INSTANCE.currentSatisfactionManagement;
                    _currentClientMange = DMServiceEvaluationManage.INSTANCE.currentClientMange;
                    _currentEmphasisBusiness = DMServiceEvaluationManage.INSTANCE.currentEmphasisBusiness;
                    _currentMangement = DMServiceEvaluationManage.INSTANCE.currentMangement;
                    Messenger.Default.Send("Sucess", GlobalValue._SERVICE_MANAGEMENT_LOAD_DATA);
                });

            }

        }
        #endregion


    }
}