using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model.Form;
using System.Collections.Generic;

namespace Honda.ViewModel
{
    /// <summary>
    /// 服务基础评价
    /// </summary>
    public class ServiceBasedEvaluationVM : ViewModelBase
    {

        /// <summary>
        /// 5S & 安全
        /// </summary>
        public M_FiveSAndSafes_Source _currentFiveSAndSafesSource;


        /// <summary>
        /// 硬件数据源
        /// </summary>
        public M_Hardware_Source _currentHardware { get; set; }

        /// <summary>
        /// 人员数据源
        /// </summary>
        public M_Personnel_Source _currentPersnnel { get; set; }

        /// <summary>
        /// 接待流程表格的数据源
        /// </summary>
        public M_Common_Source _currentReceiveGuestsFlow { get; set; }


        /// <summary>
        /// 快修流程表格的数据源
        /// </summary>
        public M_Common_Source _currentQuickService { get; set; }

        /// <summary>
        /// BP流程表格的数据源
        /// </summary>
        public M_Common_Source _currentBpFlow { get; set; }

        /// <summary>
        ///数据准确性表格的数据源
        /// </summary>
        public M_Common_Source _currentM_DataAccuracySource { get; set; }

        public ServiceBasedEvaluationVM()
        {
            
        }


        #region  Test
        void InitDataFiveSAndSafes()
        {
            _currentFiveSAndSafesSource = DMServiceBasedEvaluation.INSTANCE.currentFiveSAndSafesSource;
            _currentHardware = DMServiceBasedEvaluation.INSTANCE.currentHardware;
            _currentPersnnel = DMServiceBasedEvaluation.INSTANCE.currentPersonnel;
            _currentReceiveGuestsFlow = DMServiceBasedEvaluation.INSTANCE.currentReceiveGuestsFlow;
            _currentQuickService = DMServiceBasedEvaluation.INSTANCE.currentQuickService;
            _currentBpFlow = DMServiceBasedEvaluation.INSTANCE.currentBpFlow;
            _currentM_DataAccuracySource = DMServiceBasedEvaluation.INSTANCE.currentM_DataAccuracySource;


        }
        #endregion


        #region CMD
        public RelayCommand LoadedCommand
        {
            get
            {

                return new RelayCommand(() =>
                {
                    InitDataFiveSAndSafes();
                    Messenger.Default.Send("Sucess", GlobalValue._EVALUATE_LOAD_DATA);
                });

            }

        }
        #endregion
    }
}