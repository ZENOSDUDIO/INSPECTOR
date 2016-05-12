using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.IO;
using Microsoft.Win32;
using System.Windows.Ink;
using Honda.Globals;
using Honda.View.BaseView;

namespace Honda.View
{
    /// <summary>
    /// ExportFileMarkWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExportFileMarkWindow : BaseWindow
    {
        public ExportFileMarkWindow()
        {
            InitializeComponent();
        }

        public bool bbetterMark
        {
            get { return this.cbbetterMark.IsChecked == true; }
        }

        public bool bfeedBackMark
        {
            get { return this.cbfeedBackMark.IsChecked == true; }
        }

        public bool btourMark
        {
            get { return this.cbtourMark.IsChecked == true; }
        }

        public bool bbusinessMark
        {
            get { return this.cbbusinessMark.IsChecked == true; }
        }

        public bool bExcelMark
        {
            get { return this.cbExcel.IsChecked == true; }
        }

        public bool bPdfMark
        {
            get { return this.cbPdf.IsChecked == true; }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var msg = Validate();
            if (msg == _error_msg.NOTHING)
            {
                this.DialogResult = true;
                this.Close();
                return;
            }
            if (msg == _error_msg.PLS_CHOOSE_FILE_FORMAT)
            {
                MessageBox.Show("请选择输出的格式！");
            }
            if (msg == _error_msg.PLS_CHOOSE_FILE_CONTENT)
            {
                MessageBox.Show("请选择输出的内容！");
            }
        }

        private enum _error_msg
        {
            PLS_CHOOSE_FILE_CONTENT,
            PLS_CHOOSE_FILE_FORMAT,
            NOTHING
        }

        private _error_msg Validate()
        {
            if (!(bbetterMark || bfeedBackMark || btourMark || bbusinessMark) && (bExcelMark || bPdfMark))
                return _error_msg.PLS_CHOOSE_FILE_CONTENT;

            if (!(bExcelMark || bPdfMark) && (bbetterMark || bfeedBackMark || btourMark || bbusinessMark))
                return _error_msg.PLS_CHOOSE_FILE_FORMAT;

            return _error_msg.NOTHING;
        }
    }
}