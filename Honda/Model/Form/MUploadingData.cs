using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.Model.Form
{
    /*
     * 用于上传到生成评价结果的数据
     */

    public class MUploadingData
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string logId { get; set; }

        public string shopId { get; set; }

        public List<Data> Datas { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 评价表数据编号
        /// </summary>
        public string appProld { get; set; }

        /// <summary>
        /// 巡回评价表分数
        /// </summary>
        public string tourScore { get; set; }

        public List<AppraiseDetail> apprailseDetails { get; set; }
    }

    public class AppraiseDetail
    {
    }
}