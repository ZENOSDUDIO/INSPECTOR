using Honda.HttpLib;
using Honda.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.ViewModel
{
    /*
     * 日程管理DMScheduleMange
     */

    public class DMScheduleManage
    {
        public static DMScheduleManage INSTANCE = new DMScheduleManage();

        /// <summary>
        /// 任务清单
        /// </summary>
        public ObservableCollection<MTask> _listTask { get; set; }

        private DMScheduleManage()
        {
        }


        /// <summary>
        /// 获取任务清单列表
        /// </summary>
        public void GetTaskList(Action<bool, string> action)
        {
            ReqGetUnfinishedTask _cmd = new ReqGetUnfinishedTask(DMUser.INSTANCE.CurrentMUser.UserAccount, (obj) =>
            {
                ReqGetUnfinishedTask req = obj as ReqGetUnfinishedTask;
                if (!req.m_bIsExistException)
                {
                    req.ParseParam();
                    if (req.m_bIsSuccess)
                    {
                        _listTask = req.listTask;
                        haveExpiredTotal = req.haveExpiredTotal;
                        willExpireTotal = req.willExpireTotal;
                        action(true, "操作成功！");
                    }
                    else
                    {
                        action(false, req.m_strErrorMsg);
                    }
                }
                else
                {
                    action(false, req.m_strErrorMsg);
                }
            });
            _cmd.SendHttpRequest();
        }

        /// <summary>
        /// 已过期总数
        /// </summary>
        public string haveExpiredTotal { get; set; }

        /// <summary>
        /// 即将过期总数
        /// </summary>
        public string willExpireTotal { get; set; }
    }
}