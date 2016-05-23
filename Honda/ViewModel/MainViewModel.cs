using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model;

namespace Honda.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private string _welcomeTitle = string.Empty;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get { return _welcomeTitle; }

            set
            {
                if (_welcomeTitle == value)
                {
                    return;
                }

                _welcomeTitle = value;
                RaisePropertyChanged(WelcomeTitlePropertyName);
            }
        }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = item.Title;
                });

            InitData();
        }


        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}

        #region Command

        public string StrCommandClose { get; set; }
        public string StrCommandMax { get; set; }

        public string StrCommandMin { get; set; }

        public string StrCommandNormal { get; set; }
        public string strCommandDragMove { get; set; }


        private void InitData()
        {
            StrCommandClose = GlobalValue.COMMAND_CLOSE_WINDOW;
            StrCommandMax = GlobalValue.COMMAND_MAX_WINDOW;
            StrCommandNormal = GlobalValue.COMMAND_NORMAL_WINDOW;
            strCommandDragMove = GlobalValue.COMMAND_DRAG_MOVE_WINDOW;
            StrCommandMin = GlobalValue.COMMAND_MIN_WINDOW;
        }


        public RelayCommand<string> WindowCommandAction
        {
            get
            {
                return
                    new RelayCommand<string>((msg) => { Messenger.Default.Send(msg, GlobalValue.COMMAND_MAIN_WINDOW); });
            }
        }

        #endregion
    }
}