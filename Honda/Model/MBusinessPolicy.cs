using Honda.Model.Form;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Honda.Model
{
    /// <summary>
    /// 商务政策-非纯正零部件
    /// </summary>
    [Serializable]
    public class MNotPureComponent : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性ID（当数据为新增时可不传，当数据为修改需传ID）
        /// </summary>
        //public string attributeId { get; set; }
        private string _partName;

        /// <summary>
        /// 零件名称
        /// </summary>
        public string partName
        {
            get { return _partName; }

            set
            {
                if (_partName != value)
                {
                    _partName = value;
                    NotifyPropertyChanged("partName");
                }
            }
        }

        private string _partNo;

        /// <summary>
        /// 对应广本零件号
        /// </summary>
        public string partNo
        {
            get { return _partNo; }

            set
            {
                if (_partNo != value)
                {
                    _partNo = value;
                    NotifyPropertyChanged("partNo");
                }
            }
        }


        private string _price;

        /// <summary>
        /// 价格(保留2位小数)
        /// </summary>
        public string price
        {
            get { return _price; }

            set
            {
                _price = value;
                NotifyPropertyChanged("price");
            }
        }


        private string _partNum;

        /// <summary>
        /// 数量
        /// </summary>
        public string partNum
        {
            get { return _partNum; }

            set
            {
                if (_partNum != value)
                {
                    _partNum = value;
                    NotifyPropertyChanged("partNum");
                }
            }
        }

        /// <summary>
        /// 开始时间(用于显示在界面)    
        /// </summary>
        private string _startDateText;

        public string startDateText
        {
            get { return _startDateText; }
            set
            {
                if (_startDateText != value)
                {
                    _startDateText = value;
                    NotifyPropertyChanged("startDateText");
                }
            }
        }


        /// <summary>
        /// 开始时间(用于提交服务器)    
        /// </summary>
        public string startDate { get; set; }


        /// <summary>
        /// 结束时间(用于显示在界面上的时间格式)    
        /// </summary>
        private string _endDateText;

        public string endDateText
        {
            get { return _endDateText; }
            set
            {
                if (_endDateText != value)
                {
                    _endDateText = value;
                    NotifyPropertyChanged("endDateText");
                }
            }
        }

        /// <summary>
        /// 结束时间(用于提交服务器上的时间格式)    
        /// </summary>
        public string endDate { get; set; }


        private string _provider;

        /// <summary>
        /// 供应商    
        /// </summary>
        public string provider
        {
            get { return _provider; }

            set
            {
                if (_provider != value)
                {
                    _provider = value;
                    NotifyPropertyChanged("provider");
                }
            }
        }


        private MRemarks _remarks;

        /// <summary>
        /// 附件
        /// </summary>
        public MRemarks remarks
        {
            get
            {
                if (_remarks == null)
                {
                    _remarks = new MRemarks();
                }
                return _remarks;
            }
            set
            {
                if (_remarks != value)
                {
                    _remarks = value;
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
    [Serializable]
    public class MNotPureComponentClone
    {
        /// <summary>
        /// 属性ID（当数据为新增时可不传，当数据为修改需传ID）
        /// </summary>
        //public string attributeId { get; set; }
        private string _partName;

        /// <summary>
        /// 零件名称
        /// </summary>
        public string partName
        {
            get { return _partName; }

            set
            {
                if (_partName != value)
                {
                    _partName = value;
                }
            }
        }

        private string _partNo;

        /// <summary>
        /// 对应广本零件号
        /// </summary>
        public string partNo
        {
            get { return _partNo; }

            set
            {
                if (_partNo != value)
                {
                    _partNo = value;
                }
            }
        }


        private string _price;

        /// <summary>
        /// 价格(保留2位小数)
        /// </summary>
        public string price
        {
            get { return _price; }

            set
            {
                _price = value;
            }
        }


        private string _partNum;

        /// <summary>
        /// 数量
        /// </summary>
        public string partNum
        {
            get { return _partNum; }

            set
            {
                if (_partNum != value)
                {
                    _partNum = value;
                }
            }
        }

        /// <summary>
        /// 开始时间(用于显示在界面)    
        /// </summary>
        private string _startDateText;

        public string startDateText
        {
            get { return _startDateText; }
            set
            {
                if (_startDateText != value)
                {
                    _startDateText = value;
                }
            }
        }


        /// <summary>
        /// 开始时间(用于提交服务器)    
        /// </summary>
        public string startDate { get; set; }


        /// <summary>
        /// 结束时间(用于显示在界面上的时间格式)    
        /// </summary>
        private string _endDateText;

        public string endDateText
        {
            get { return _endDateText; }
            set
            {
                if (_endDateText != value)
                {
                    _endDateText = value;
                }
            }
        }

        /// <summary>
        /// 结束时间(用于提交服务器上的时间格式)    
        /// </summary>
        public string endDate { get; set; }


        private string _provider;

        /// <summary>
        /// 供应商    
        /// </summary>
        public string provider
        {
            get { return _provider; }

            set
            {
                if (_provider != value)
                {
                    _provider = value;
                }
            }
        }


        private MRemarks _remarks;

        /// <summary>
        /// 附件
        /// </summary>
        public MRemarks remarks
        {
            get
            {
                if (_remarks == null)
                {
                    _remarks = new MRemarks();
                }
                return _remarks;
            }
            set
            {
                if (_remarks != value)
                {
                    _remarks = value;
                }
            }
        }
    }
    /// <summary>
    /// 零部件对外销售属性详情1
    /// </summary>
    [Serializable]
    public class MComponentESDepartment : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性ID
        /// </summary>
        //public string attributeId { get; set; }
        private string _exportSaler;

        /// <summary>
        /// 外销商
        /// </summary>
        public string exportSaler
        {
            get { return _exportSaler; }
            set
            {
                if (_exportSaler != value)
                {
                    _exportSaler = value;
                    NotifyPropertyChanged("exportSaler");
                }
            }
        }


        private string _price;

        /// <summary>
        /// 价格
        /// </summary>
        public string price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    NotifyPropertyChanged("price");
                }
            }
        }

        private string _mainPart;

        /// <summary>
        /// 主要零件
        /// </summary>
        public string mainPart
        {
            get { return _mainPart; }
            set
            {
                if (_mainPart != value)
                {
                    _mainPart = value;
                    NotifyPropertyChanged("mainPart");
                }
            }
        }

        /// <summary>
        /// 零件号
        /// </summary>
        public string partNo { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        public string partName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string partNum { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string startDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endDate { get; set; }

        /// <summary>
        /// 外销对象
        /// </summary>
        public string exportObject { get; set; }


        private MRemarks _remrks;

        /// <summary>
        /// 附件
        /// </summary>
        public MRemarks remrks
        {
            get
            {
                if (_remrks == null)
                {
                    _remrks = new MRemarks();
                }
                return _remrks;
            }
            set
            {
                if (_remrks != value)
                {
                    _remrks = value;
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

    [Serializable]
    public class MComponentESDepartmentClone
    {
        /// <summary>
        /// 属性ID
        /// </summary>
        //public string attributeId { get; set; }
        private string _exportSaler;

        /// <summary>
        /// 外销商
        /// </summary>
        public string exportSaler
        {
            get { return _exportSaler; }
            set
            {
                if (_exportSaler != value)
                {
                    _exportSaler = value;
                }
            }
        }


        private string _price;

        /// <summary>
        /// 价格
        /// </summary>
        public string price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                }
            }
        }

        private string _mainPart;

        /// <summary>
        /// 主要零件
        /// </summary>
        public string mainPart
        {
            get { return _mainPart; }
            set
            {
                if (_mainPart != value)
                {
                    _mainPart = value;
                }
            }
        }

        /// <summary>
        /// 零件号
        /// </summary>
        public string partNo { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        public string partName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string partNum { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string startDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endDate { get; set; }

        /// <summary>
        /// 外销对象
        /// </summary>
        public string exportObject { get; set; }


        private MRemarks _remrks;

        /// <summary>
        /// 附件
        /// </summary>
        public MRemarks remrks
        {
            get
            {
                if (_remrks == null)
                {
                    _remrks = new MRemarks();
                }
                return _remrks;
            }
            set
            {
                if (_remrks != value)
                {
                    _remrks = value;
                }
            }
        }
    }
    /// <summary>
    /// 零部件对外销售属性详情2
    /// </summary>
    [Serializable]
    public class MComponentESDepartment2 : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性ID
        /// </summary>
        //public string attributeId { get; set; }
        private string _partNo;

        /// <summary>
        /// 零件号
        /// </summary>
        public string partNo
        {
            get { return _partNo; }
            set
            {
                if (_partNo != value)
                {
                    _partNo = value;
                    NotifyPropertyChanged("partNo");
                }
            }
        }


        private string _partName;

        /// <summary>
        /// 零件名称
        /// </summary>
        public string partName
        {
            get { return _partName; }
            set
            {
                if (_partName != value)
                {
                    _partName = value;
                    NotifyPropertyChanged("partName");
                }
            }
        }


        private string _partNum;

        /// <summary>
        /// 数量
        /// </summary>
        public string partNum
        {
            get { return _partNum; }
            set
            {
                if (_partNum != value)
                {
                    _partNum = value;
                    NotifyPropertyChanged("partNum");
                }
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string startDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endDate { get; set; }


        private string _exportObject;

        /// <summary>
        /// 外销对象
        /// </summary>
        public string exportObject
        {
            get { return _exportObject; }
            set
            {
                if (_exportObject != value)
                {
                    _exportObject = value;
                    NotifyPropertyChanged("exportObject");
                }
            }
        }


        private MRemarks _remrks;

        /// <summary>
        /// 附件
        /// </summary>
        public MRemarks remrks
        {
            get
            {
                if (_remrks == null)
                {
                    _remrks = new MRemarks();
                }
                return _remrks;
            }
            set
            {
                if (_remrks != value)
                {
                    _remrks = value;
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

    [Serializable]
    public class MComponentESDepartment2Clone
    {
        /// <summary>
        /// 属性ID
        /// </summary>
        //public string attributeId { get; set; }
        private string _partNo;

        /// <summary>
        /// 零件号
        /// </summary>
        public string partNo
        {
            get { return _partNo; }
            set
            {
                if (_partNo != value)
                {
                    _partNo = value;
                }
            }
        }


        private string _partName;

        /// <summary>
        /// 零件名称
        /// </summary>
        public string partName
        {
            get { return _partName; }
            set
            {
                if (_partName != value)
                {
                    _partName = value;
                }
            }
        }


        private string _partNum;

        /// <summary>
        /// 数量
        /// </summary>
        public string partNum
        {
            get { return _partNum; }
            set
            {
                if (_partNum != value)
                {
                    _partNum = value;
                }
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string startDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endDate { get; set; }


        private string _exportObject;

        /// <summary>
        /// 外销对象
        /// </summary>
        public string exportObject
        {
            get { return _exportObject; }
            set
            {
                if (_exportObject != value)
                {
                    _exportObject = value;
                }
            }
        }


        private MRemarks _remrks;

        /// <summary>
        /// 附件
        /// </summary>
        public MRemarks remrks
        {
            get
            {
                if (_remrks == null)
                {
                    _remrks = new MRemarks();
                }
                return _remrks;
            }
            set
            {
                if (_remrks != value)
                {
                    _remrks = value;
                }
            }
        }

    }

    /// <summary>
    /// 零部件价格执行
    /// </summary>
    [Serializable]
    public class MComponentPrice : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性ID
        /// </summary>
        public string attributeId { get; set; }

        private string _partNo;

        /// <summary>
        /// 零件号
        /// </summary>
        public string partNo
        {
            get { return _partNo; }
            set
            {
                if (_partNo != value)
                {
                    _partNo = value;
                    NotifyPropertyChanged("partNo");
                }
            }
        }

        private string _partName;

        /// <summary>
        /// 零件名称
        /// </summary>
        public string partName
        {
            get { return _partName; }
            set
            {
                if (_partName != value)
                {
                    _partName = value;
                    NotifyPropertyChanged("partName");
                }
            }
        }


        private string _salePrice;

        /// <summary>
        /// 销售价格(保留2位小数)
        /// </summary>
        public string salePrice
        {
            get { return _salePrice; }
            set
            {
                if (_salePrice != value)
                {
                    _salePrice = value;
                    NotifyPropertyChanged("salePrice");
                }
            }
        }


        private string _normalPrice;

        /// <summary>
        /// 标准价格(保留2位小数)
        /// </summary>
        public string normalPrice
        {
            get { return _normalPrice; }
            set
            {
                if (_normalPrice != value)
                {
                    _normalPrice = value;
                    NotifyPropertyChanged("normalPrice");
                }
            }
        }


        private string _spread;

        /// <summary>
        /// 差价(保留2位小数)
        /// </summary>
        public string spread
        {
            get { return _spread; }
            set
            {
                if (_spread != value)
                {
                    _spread = value;
                }
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        private string _partNum;

        /// <summary>
        /// 数量
        /// </summary>
        public string partNum
        {
            get { return _partNum; }
            set
            {
                if (_partNum != value)
                {
                    _partNum = value;
                    NotifyPropertyChanged("partNum");
                }
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string startDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endDate { get; set; }


        private MRemarks _remrks;

        /// <summary>
        /// 附件
        /// </summary>
        public MRemarks remrks
        {
            get
            {
                if (_remrks == null)
                {
                    _remrks = new MRemarks();
                }
                return _remrks;
            }
            set
            {
                if (_remrks != value)
                {
                    _remrks = value;
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
    [Serializable]
    public class MComponentPriceClone
    {
        /// <summary>
        /// 属性ID
        /// </summary>
        public string attributeId { get; set; }

        private string _partNo;

        /// <summary>
        /// 零件号
        /// </summary>
        public string partNo
        {
            get { return _partNo; }
            set
            {
                if (_partNo != value)
                {
                    _partNo = value;
                }
            }
        }

        private string _partName;

        /// <summary>
        /// 零件名称
        /// </summary>
        public string partName
        {
            get { return _partName; }
            set
            {
                if (_partName != value)
                {
                    _partName = value;
                }
            }
        }


        private string _salePrice;

        /// <summary>
        /// 销售价格(保留2位小数)
        /// </summary>
        public string salePrice
        {
            get { return _salePrice; }
            set
            {
                if (_salePrice != value)
                {
                    _salePrice = value;
                }
            }
        }


        private string _normalPrice;

        /// <summary>
        /// 标准价格(保留2位小数)
        /// </summary>
        public string normalPrice
        {
            get { return _normalPrice; }
            set
            {
                if (_normalPrice != value)
                {
                    _normalPrice = value;
                }
            }
        }


        private string _spread;

        /// <summary>
        /// 差价(保留2位小数)
        /// </summary>
        public string spread
        {
            get { return _spread; }
            set
            {
                if (_spread != value)
                {
                    _spread = value;
                }
            }
        }

        /// <summary>
        /// 数量
        /// </summary>
        private string _partNum;

        /// <summary>
        /// 数量
        /// </summary>
        public string partNum
        {
            get { return _partNum; }
            set
            {
                if (_partNum != value)
                {
                    _partNum = value;
                }
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string startDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string endDate { get; set; }


        private MRemarks _remrks;

        /// <summary>
        /// 附件
        /// </summary>
        public MRemarks remrks
        {
            get
            {
                if (_remrks == null)
                {
                    _remrks = new MRemarks();
                }
                return _remrks;
            }
            set
            {
                if (_remrks != value)
                {
                    _remrks = value;
                }
            }
        }


    }
}