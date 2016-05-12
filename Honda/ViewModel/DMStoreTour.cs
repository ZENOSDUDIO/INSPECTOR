using Honda.Globals;
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
    public class DMStoreTour
    {
        public static DMStoreTour INSTANCE = new DMStoreTour();

        public ObservableCollection<MStore> listStore;

        public MStore CurrentMStore { get; set; }

        private DMStoreTour()
        {
        }


        /// <summary>
        /// 获取店列表
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="ation"></param>
        public void GetStoreList(string loginId, Action<bool, string> ation)
        {
            ReqGetStoreList _cmdGetStoreList = new ReqGetStoreList(loginId, (obj) =>
            {
                ReqGetStoreList req = obj as ReqGetStoreList;
                if (!req.m_bIsExistException)
                {
                    req.ParseParam();
                    if (req.m_bIsSuccess)
                    {
                        if (req.lstStore != null)
                            listStore = req.lstStore;
                        SerialHelp.SerialObject(DirectoryHelper.INSTANCE.STORE_PATH_DATA, listStore);
                        ation(true, "操作成功！");
                    }
                    else
                    {
                        ation(false, req.m_strErrorMsg);
                    }
                }
                else
                {
                    ation(false, req.m_strErrorMsg);
                }
            });

            _cmdGetStoreList.SendHttpRequest();
        }

        /// <summary>
        /// 发布任务
        /// </summary>
        public void ReleaseTask(MTask task, Action<bool, string> ation)
        {
            ReqReleaseTask _cmdReleaseTask = new ReqReleaseTask(task, (obj) =>
            {
                ReqReleaseTask req = obj as ReqReleaseTask;
                if (!req.m_bIsExistException)
                {
                    req.ParseParam();
                    if (req.m_bIsSuccess)
                    {
                        ation(true, "操作成功！");
                    }
                    else
                    {
                        ation(false, req.m_strErrorMsg);
                    }
                }
                else
                {
                    ation(false, req.m_strErrorMsg);
                }
            });

            _cmdReleaseTask.SendHttpRequest();
        }
    }
}