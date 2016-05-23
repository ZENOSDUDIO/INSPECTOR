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
    /// <summary>
    /// 工作亮点与意见需求
    /// </summary>
    public class DMWorkLightspotAndIdea
    {
        /// <summary>
        /// 工作亮点与意见需求列表
        /// </summary>
        public ObservableCollection<MWorkLightspot> listWorkLightspot;

        public static DMWorkLightspotAndIdea INSTANCE = new DMWorkLightspotAndIdea();

        private DMWorkLightspotAndIdea()
        {
        }


        /// <summary>
        /// 获取工作亮点与意见需求列表
        /// </summary>
        public void GetWorkLightspotAndIdeaList(string loginId, string shopId, Action<bool, string> action)
        {
            ReqGetWorkLightspotAndIdeaList _cmdGetWorkLightspotList = new ReqGetWorkLightspotAndIdeaList(loginId, shopId,
                (obj) =>
                {
                    ReqGetWorkLightspotAndIdeaList req = obj as ReqGetWorkLightspotAndIdeaList;
                    if (!req.m_bIsExistException)
                    {
                        req.ParseParam();
                        if (req.m_bIsSuccess)
                        {
                            if (req.lstWorkLightspot != null)
                                listWorkLightspot = req.lstWorkLightspot;
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

            _cmdGetWorkLightspotList.SendHttpRequest();
        }

        /// <summary>
        /// 提交工作亮点与意见需求列表
        /// </summary>
        public void CommitWorkLightspotList(string loginId, string shopId,
            ObservableCollection<MWorkLightspot> listWorkLightspot, Action<bool, string> action)
        {
            CommitWorkLightspotList _cmdWorkLightspotList = new CommitWorkLightspotList(loginId, shopId,
                listWorkLightspot, (obj) =>
                {
                    CommitWorkLightspotList req = obj as CommitWorkLightspotList;
                    if (!req.m_bIsExistException)
                    {
                        req.ParseParam();
                        if (req.m_bIsSuccess)
                        {
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

            //_cmdWorkLightspotList.SendHttpRequest();   
        }

        public void UploadWorkLightspotList(ObservableCollection<MWorkLightspot> listWorkLightspot, string remmovedIDs,
            Action<bool, string> action)
        {
            ReqUploadtWorkLightspotAndIdeaList _cmdUpload = new ReqUploadtWorkLightspotAndIdeaList(listWorkLightspot,
                remmovedIDs, (obj) =>
                {
                    ReqUploadtWorkLightspotAndIdeaList req = obj as ReqUploadtWorkLightspotAndIdeaList;
                    if (!req.m_bIsExistException)
                    {
                        req.ParseParam();
                        if (req.m_bIsSuccess)
                        {
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
            _cmdUpload.SendHttpRequest();
        }
    }
}