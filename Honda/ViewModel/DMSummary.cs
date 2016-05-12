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
    /// 巡回总结报告
    /// </summary>
    public class DMSummary
    {
        /// <summary>
        /// 巡回总结报告列表
        /// </summary>
        public ObservableCollection<MSummary> summary;

        public static DMSummary INSTANCE = new DMSummary();

        private DMSummary()
        {
        }

        public void UploadSummary(ObservableCollection<MSummary> summary, Action<bool, string> action)
        {
            ReqUploadSummary _cmdUpload = new ReqUploadSummary(summary, (obj) =>
            {
                ReqUploadSummary req = obj as ReqUploadSummary;
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