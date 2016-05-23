using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Honda.Model
{
    [Serializable]
    public class MEvaluateMenu : INotifyPropertyChanged
    {
        public MEvaluateMenu()
        {
        }

        private string evaluateName;

        public string EvaluateName
        {
            get { return evaluateName; }
            set
            {
                if (evaluateName != value)
                {
                    evaluateName = value;
                }
            }
        }

        private string evaluateCode;

        public string EvaluateCode
        {
            get { return evaluateCode; }
            set
            {
                if (evaluateCode != value)
                {
                    evaluateCode = value;
                }
            }
        }


        private string evaluateHeadName;

        public string EvaluateHeadName
        {
            get { return evaluateHeadName; }
            set
            {
                if (evaluateHeadName != value)
                {
                    evaluateHeadName = value;
                }
            }
        }


        private string evaluateHeadDesc;

        public string EvaluateHeadDesc
        {
            get { return evaluateHeadDesc; }
            set
            {
                if (evaluateHeadDesc != value)
                {
                    evaluateHeadDesc = value;
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 用于通知属性的改变
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}