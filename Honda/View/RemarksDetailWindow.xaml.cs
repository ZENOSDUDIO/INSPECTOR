using Honda.Globals;
using Honda.Model.Form;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Honda.View
{
    /// <summary>
    /// RemarksWindows.xaml 的交互逻辑
    /// </summary>
    public partial class RemarksDetailWindow : BaseWindow
    {
        /// <summary>
        /// 备注图片地址列表
        /// </summary>
        private List<string> lstpictrue;

        //备注内容
        private MRemarks remaks;

        public RemarksDetailWindow(MRemarks remaks)
        {
            InitializeComponent();
            SetOwner();

            this.remaks = remaks;
            if (remaks != null && remaks.ImagePathList != null)
            {
                lstpictrue = Tools.CloneList(remaks.ImagePathList);
            }
            else
            {
                lstpictrue = new List<string>();
            }

            imageListUCT._IsReadOnly = true;
            imageListUCT.ImageList = lstpictrue;
            this.Loaded += RemarksWindows_Loaded;
        }

        #region Event

        private void RemarksWindows_Loaded(object sender, RoutedEventArgs e)
        {
            if (remaks != null)
            {
                tbContent.Text = remaks.content;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}