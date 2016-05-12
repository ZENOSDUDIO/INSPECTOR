using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using System.Windows;

namespace Honda.View
{
    /// <summary>
    /// Description for EvidenceWindows.
    /// </summary>
    public partial class EvidenceWindows : Window
    {
        /// <summary>
        /// Initializes a new instance of the EvidenceWindows class.
        /// </summary>
        public EvidenceWindows()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, GlobalValue.IMPROVE_CHECK_ClOSE_EVIDECE, msg => { this.Close(); });
        }
    }
}