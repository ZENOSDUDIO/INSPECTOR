using Honda.Globals;
using Honda.Interface;
using Honda.Model;
using Honda.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Honda.UserCtrl
{
    /// <summary>
    /// 巡回评价表所划分的最小的菜单项行控件
    /// </summary>   
    public class ItemRowControl : StackPanel
    {
        #region Var 、Load 、Construction Fun

        private Action _action_score;
        private Action _action_goToPlane;

        /// <summary>
        /// 是否自动调整控件高度
        /// </summary>
        public bool IsHeighAuto { get; set; }

        private ItemStyle m_iStyle;

        public ItemStyle ItemStyle
        {
            get { return m_iStyle; }
        }


        //是否是详情
        private bool _isDetail = false;

        public bool IsDetail
        {
            get { return _isDetail; }
            set { _isDetail = value; }
        }

        /// <summary>
        /// 控件初始化时所需的数据
        /// </summary>
        private readonly object _objItem;

        private dynamic _mItemNormal;

        public ItemRowControl(ItemStyle iStyle, object item)
        {
            _objItem = item;
            m_iStyle = iStyle;
            this.Loaded += ItemRowControl_Loaded;
        }

        private void ItemRowControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (m_iStyle)
            {
                case ItemStyle.ITEM_STYLE_NORMAL:
                    InitControl();
                    break;

                case ItemStyle.ITEM_STYLE_PREVIEW:
                    InitPreviewControl();
                    break;
            }
        }

        #endregion

        #region 初始化控件

        /// <summary>
        /// 
        /// </summary>
        private void InitControl()
        {
            var normalItem = _objItem as MItemNormal;
            if (normalItem == null) return;
            _mItemNormal = new ItemControlNormal(normalItem);

            if (IsHeighAuto) _mItemNormal.bIsAutoHigh = IsHeighAuto;

            _mItemNormal.isDetail = IsDetail;
            (_mItemNormal as ItemControlNormal).SetRefreshScore(Update);
            _mItemNormal.SetGoForm(_action_goToPlane);
            this.Children.Add(_mItemNormal);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitPreviewControl()
        {
            var normalItem = _objItem as MItemNormal;
            if (normalItem == null) return;
            _mItemNormal = new ItemControlPreview(normalItem);

            if (IsHeighAuto) _mItemNormal.bIsAutoHigh = IsHeighAuto;

            _mItemNormal.isDetail = IsDetail;
            (_mItemNormal as ItemControlPreview).SetRefreshScore(Update);
            _mItemNormal.SetGoForm(_action_goToPlane);
            this.Children.Add(_mItemNormal);
        }

        public void SetWith()
        {
        }

        #endregion

        private void Update()
        {
            //更新整个面板的分数
            if (_action_score != null)
            {
                _action_score();
            }
        }

        public void UpdateScore(Action action)
        {
            _action_score = action;
        }

        public void GoToAntherPlane(Action action)
        {
            _action_goToPlane = action;
        }
    }
}