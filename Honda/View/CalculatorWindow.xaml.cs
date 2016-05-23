using Honda.View.BaseView;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Honda.View
{
    /// <summary>
    /// CalculatorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CalculatorWindow : BaseWindow
    {
        private string Number;
        public Action<string> actionNum;

        public CalculatorWindow()
        {
            InitializeComponent();
            SetOwner();
        }

        public void SetActionNum(Action<string> action)
        {
            actionNum = action;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            //string temNum = (Button) 
            Button btn = sender as Button;
            if (btn == null)
            {
                return;
            }
            string temNum = (string) btn.Tag;
            Number += temNum;

            if (actionNum != null && Number != null && Number.Length > 0)
            {
                actionNum(Number);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (Number != null && Number.Length > 0)
            {
                if (Number.Length == 1)
                {
                    Number = "0";
                }
                else
                {
                    Number = Number.Remove(Number.Length - 1);
                }


                if (actionNum != null)
                {
                    actionNum(Number);
                }
            }
        }
    }
}