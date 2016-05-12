using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Honda.HttpLib.JsonModelData
{
    /// <summary>
    /// 巡回员做出评价的类型（1、合格与否 2、具体的分值　３、五星级仓库连接）
    /// </summary>
    public enum ENUM_SCORE_TYPE
    {
        /// <summary>
        /// 具体的分数
        /// </summary>
        SCORE,

        /// <summary>
        /// 合格或不合格
        /// </summary>
        QUALIFIED_OR_NOT,

        /// <summary>
        /// 其他类型---现用来作为跳转标识 加分项第13项
        /// </summary>
        OTHER,

        /// <summary>
        /// 跳转到五星级仓库评价表
        /// </summary>
        FIVE_STAR_LINK
    }

    /// <summary>
    /// 表单里的组里面的单元项的模板
    /// </summary>
    public enum ENUM_FORM_ITEM_TEMPLATE
    {
        FIRST,
        SECOND,
        THIRD,
        FOURTH,
        FIFTH,
        SIXTH,
        SEVEN
    }

    /// <summary>
    /// 表单的单元项基类，用于描绘评价表中的具体一行数据
    /// <author>xiang</author>
    /// <date>2014-10-14</date>
    /// </summary>
    public class JFormItemBase
    {
        /// <summary>
        /// 显示在表单里的序号
        /// </summary>
        public string SerialNum { get; set; }

        /// <summary>
        /// 分值类型（1、合格与否 2、具体的分值）
        /// </summary>
        public ENUM_SCORE_TYPE EnumScoreType { get; set; }

        /// <summary>
        /// 分值类型（服务端返回值 0 或 1）
        /// </summary>
        public string ValueType
        {
            get { return ""; }
            set
            {
                if (value == "1") //单选框
                {
                    EnumScoreType = ENUM_SCORE_TYPE.QUALIFIED_OR_NOT;
                }
                else if (value == "2" || value == "4") //文本框，也就是由下拉框改过来的
                {
                    EnumScoreType = ENUM_SCORE_TYPE.SCORE;
                }
                else if (value == "3")
                {
                    EnumScoreType = ENUM_SCORE_TYPE.FIVE_STAR_LINK;
                }
                else
                {
                    EnumScoreType = ENUM_SCORE_TYPE.OTHER;
                }
            }
        }

        /*
        /// <summary>
        /// 显示的内容
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// 详情内容
        /// </summary>
        public string DetailDescribe { get; set; }
          
        */

        /// <summary>
        /// 用于显示的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 自身的ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 所属巡回店的ID
        /// </summary>
        public int ShopID { get; set; }

        /// <summary>
        /// 待定
        /// </summary>
        public string TaskID { get; set; }

        /// <summary>
        /// 模板类型（是属于1~~5号模板中的哪一项）
        /// </summary>
        public ENUM_FORM_ITEM_TEMPLATE ENUMItemTemplate { get; set; }

        /// <summary>
        /// 上一次评价结果
        /// </summary>
        public string LastResult { get; set; }

        /// <summary>
        /// 特约店自评结果
        /// </summary>
        public string CurrentResult { get; set; }

        /// <summary>
        /// 上次评价是否通过(合格) by luyonghe 10/27 
        /// </summary>
        public bool bIsLastTimePass { get; set; }

        /// <summary>
        /// 自评是否通过(合格) by luyonghe 10/27 
        /// </summary>
        public bool bIsSelfEvaluation { get; set; }
    }
}