using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honda.Model;

namespace Honda.HttpLib.JsonInputData
{
    /// <summary>
    /// 组的得分 用于上传评分
    /// </summary>
    [Serializable]
    public class GroupDataForUpload
    {
        public string ID { get; set; }
        public string Score { get; set; }
    }

    /// <summary>
    /// 小项中的附件描述，用于上传评分
    /// </summary>
    [Serializable]
    public class FileDataForUpload
    {
        public string FileName { get; set; }
        public string OldName { get; set; }
        public string FilePath { get; set; }
    }

    /// <summary>
    /// 小项的得分，用于上传评分
    /// </summary>
    [Serializable]
    public class ItemDataForUpload
    {
        public string GroupId { get; set; }

        public string ID { get; set; }
        public string Score { get; set; }
        public string Remark { get; set; }

        public List<FileDataForUpload> Files = new List<FileDataForUpload>();
    }

    /// <summary>
    /// 提交评分 分两部分，第一部分为组得分，第二部分为小项得分
    /// </summary>
    [Serializable]
    public class EvaluationForUpload
    {
        public List<GroupDataForUpload> Groups = new List<GroupDataForUpload>();

        public List<ItemDataForUpload> Items = new List<ItemDataForUpload>();
    }
}