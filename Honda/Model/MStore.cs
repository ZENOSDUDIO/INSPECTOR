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
    /// <summary>
    /// 当前菜单完成状态 
    /// </summary>
    public enum ITEM_STATE
    {
        /// <summary>
        /// 未开始
        /// </summary>
        NOT_START = 0,

        /// <summary>
        /// 待提交/执行中
        /// </summary>
        TO_SUBMIT = 1,

        /// <summary>
        /// 待提交附件
        /// </summary>
        TO_SUBMIT_ACCESSORY = 2,

        /// <summary>
        /// 已过期
        /// </summary>
        OutDate = 3,

        /// <summary>
        /// 已提交/已完成
        /// </summary>
        SUBMITED = 4,

        /// <summary>
        /// 待审核
        /// </summary>
        CHECK_PENDING = 5,

        /// <summary>
        /// 已结束
        /// </summary>
        CHECK_PENDING_FINISH = 6,

        NONE = 7
    }

    /// <summary>
    /// 特约店的任务状态
    /// </summary>
    public enum Enum_ShopType
    {
        /// <summary>
        /// 自身管辖特约店
        /// </summary>
        selfStore = 0,

        /// <summary>
        /// 系长指派特约店
        /// </summary>
        assignmentStore = 1
    }


    [Serializable]
    public class MStore : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_storeName">店名</param>
        /// <param name="RECORD_STATE">评价记录状态</param>
        /// <param name="BUSEINESS_STATE">商务政策状态</param>
        /// <param name="LIGHT_SPOT_STATE">工作亮点与意见需求状态</param>
        /// <param name="TOUR_STATE">巡回评价状态</param>
        /// <param name="IMPROVE_STATE">改善计划状态</param>
        public MStore(string _storeName, ITEM_STATE RECORD_STATE, ITEM_STATE BUSEINESS_STATE,
            ITEM_STATE LIGHT_SPOT_STATE, ITEM_STATE TOUR_STATE, ITEM_STATE IMPROVE_STATE)
        {
            storeName = _storeName;
            _RECORD_STATE = RECORD_STATE;
            _BUSEINESS_STATE = BUSEINESS_STATE;
            _LIGHT_SPOT_STATE = LIGHT_SPOT_STATE;
            _TOUR_STATE = TOUR_STATE;
            _IMPROVE_STATE = IMPROVE_STATE;
        }

        public MStore()
        {
        }

        private string storeName;

        public string StoreName
        {
            get { return storeName; }
            set
            {
                if (storeName != value)
                {
                    storeName = value;
                }
            }
        }

        /// <summary>
        /// 用户设置选中样式
        /// </summary>
        private bool _isSelected = false;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// 评价表ID
        /// </summary>
        public string appriaseId { get; set; }

        /// <summary>
        /// 店id
        /// </summary>
        public string shopId { get; set; }

        /// <summary>
        /// 该店的任务状态  特约店类型(0-自身管辖特约店，1-系长指派特约店) 枚举类型
        /// </summary>
        //public Enum_ShopType enum_ShopType { get; set; }
        private string _strShopType = "0";

        /// <summary>
        /// 特约店类型(0-自身管辖特约店，1-系长指派特约店)
        /// </summary>
        public string strShopType
        {
            get { return _strShopType; }
            set
            {
                if (_strShopType != value)
                {
                    _strShopType = value;
                }
            }
        }

        /// <summary>
        /// 任务状态(0未开始，1执行中，2已过期，3已完成)
        /// </summary>
        public string taskStatus { get; set; }

        /// <summary>
        /// 评价记录状态
        /// </summary>
        public ITEM_STATE _RECORD_STATE { get; set; }

        /// <summary>
        /// 商务政策状态
        /// </summary>
        public ITEM_STATE _BUSEINESS_STATE { get; set; }

        /// <summary>
        /// 工作亮点与意见需求状态
        /// </summary>
        public ITEM_STATE _LIGHT_SPOT_STATE { get; set; }

        /// <summary>
        /// 巡回评价状态
        /// </summary>
        public ITEM_STATE _TOUR_STATE { get; set; }

        /// <summary>
        /// 改善计划状态
        /// </summary>
        public ITEM_STATE _IMPROVE_STATE { get; set; }

        /// <summary>
        /// 巡回评价总评报告
        /// </summary>
        public ITEM_STATE _OVERALL_RATING_REPORT { get; set; }


        /// <summary>
        /// 正常状态下的图片URI
        /// </summary>
        private readonly string strNormalImaUri = "/Assets/EvaluationOfTour/page_nav1.png";

        public string ImgNormalUri
        {
            get { return strNormalImaUri; }
        }


        /// <summary>
        ///  是否显示跨区
        /// </summary>
        private Visibility bIsShowCrossDistrict = Visibility.Collapsed;

        public Visibility _bIsShowCrossDistrict
        {
            get { return bIsShowCrossDistrict; }
            set
            {
                if (bIsShowCrossDistrict != value)
                {
                    bIsShowCrossDistrict = value;
                    NotifyPropertyChanged("_bIsShowCrossDistrict");
                }
            }
        }

        /// <summary>
        ///  是否显示不跨区
        /// </summary>
        private Visibility bIsShowNormal = Visibility.Collapsed;

        public Visibility _bIsShowNormal
        {
            get { return bIsShowNormal; }
            set
            {
                if (bIsShowNormal != value)
                {
                    bIsShowNormal = value;
                    NotifyPropertyChanged("_bIsShowNormal");
                }
            }
        }


        /// <summary>
        /// 选中状态下的图片URI
        /// </summary>
        private readonly string strPitchOnImaUri = "/Assets/EvaluationOfTour/page_nav1_1.png";

        public string ImgSelectedUri
        {
            get { return strPitchOnImaUri; }
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