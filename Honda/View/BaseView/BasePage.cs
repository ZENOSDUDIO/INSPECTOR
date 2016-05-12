using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Honda.View.BaseView
{
    public class BasePage : Page
    {
        /// <summary>
        /// 用于通知EvaluationOfTourPage页面中总分数更新显示的
        /// </summary>
        public void NotificationUpdateScore()
        {
            Messenger.Default.Send("", GlobalValue._UPDATE_TOTAL_SCORE);
        }
    }
}