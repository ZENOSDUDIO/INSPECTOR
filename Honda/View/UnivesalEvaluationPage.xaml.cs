using GalaSoft.MvvmLight.Messaging;
using Honda.Globals;
using Honda.Model.Form;
using Honda.UserCtrl;
using Honda.View.BaseView;
using Honda.ViewModel;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Honda.View
{
    /// <summary>
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class UnivesalEvaluationPage : BasePage
    {
        #region Var、Constration Fun

        private UnivesalEvaluateVM _ViewModel;

        /// <summary>
        /// 防止构造函数加载完一次控件后，Load函数再次加载第二次
        /// </summary>
        private bool _isFirstInitForm=false;

        public UnivesalEvaluationPage()
        {
            InitializeComponent();

            _ViewModel = (UnivesalEvaluateVM) DataContext;

            Messenger.Default.Register<string>(this, GlobalValue._COMPONENT_LOAD_DATA, msg => LoadBaseUnivesalControl());
        }

        #endregion

        #region

        /// <summary>
        ///
        /// </summary>
        private void InitPanel()
        {
            if (!_isFirstInitForm)
            {
                _isFirstInitForm = true;
                foreach (var item in _ViewModel.ListEvaMenu)
                {
                    UCMyTabBtn.AddItem(item.EvaluateName, gd1);
                }

                //根据所点击的按钮，返回所要显示的Grid的名字
                UCMyTabBtn.SetShowGridName((panelname) =>
                {
                    //根据名称查找code
                    var obj = this._ViewModel.ListEvaMenu.SingleOrDefault(n => n.EvaluateName == panelname);
                    if (obj != null)
                    {
                        //根据CODE设置二级表单数据源
                        var code = obj.EvaluateCode;

                        _ViewModel.SetEvaluateSourceByCode(code);

                        //表头信息
                        this.tbHeadName.Text = obj.EvaluateHeadName;
                        this.tbHeadDesc.Text = obj.EvaluateHeadName;

                        //显示表格
                        LoadBaseUnivesalControl();
                    }
                });
            }
        }

     
        #endregion

        #region 加载基础业务

        /// <summary>
        /// 加载基础业务模块表格
        /// </summary>
        private void LoadBaseUnivesalControl()
        {
            InitPanel();

            if (_ViewModel == null || _ViewModel.CurrentBaseUniversal == null) return;

            
            sp1.Children.Clear();

            foreach (MItemBaseGroup _Group in _ViewModel.CurrentBaseUniversal.ListGroup)
            {
                SetLoadForm(_Group, SetBaseUnivesalScore, sp1);
            }

            SetBaseUnivesalScore();
        }

        private void SetBaseUnivesalScore()
        {
            var intTotal = _ViewModel.CurrentBaseUniversal._pageTotalScore;
            var intPass = _ViewModel.CurrentBaseUniversal._pageTourScore;
            var intUnpass = intTotal - intPass;


            tbkAll.Text = string.Format("本页合计：{0}项", intTotal);
            tbkPass.Text = string.Format("合格：{0}项", intPass);
            tbkUnpass.Text = string.Format("不合格：{0}项", intUnpass);
            tbkPassRate.Text = string.Format("合格率：{0}%", Math.Round(intPass * 100 / intTotal, 2));

           var obj= _ViewModel.ListEvaData.SingleOrDefault(
                n => n._sourceIdentify == _ViewModel.CurrentBaseUniversal._sourceIdentify);

            obj = _ViewModel.CurrentBaseUniversal;
            var pagedata = DMUnivesalEvaluate.INSTANCE.DataBaseUniversal[DMStoreTour.INSTANCE.CurrentMStore.shopId]=_ViewModel.ListEvaData;

            NotificationUpdateScore();
        }

        #endregion

        private void SetLoadForm(MItemBaseGroup _Group, Action updateScore, StackPanel sp)
        {
            LoadUnivesalForm((M_Common_Groupcs) _Group, updateScore, sp);
        }

        /// <summary>
        /// 加载一组表格的数据
        /// </summary>
        /// <param name="_Group">这一组表格的数据List</param>
        /// <param name="updateScore"> 回掉函数，当操作分数区域时更新代码</param>
        /// <param name="sp">需要添加表格的面板</param>
        private void LoadUnivesalForm(M_Common_Groupcs _Group, Action updateScore, StackPanel sp)
        {
            ItemPanel itemGroup = new ItemPanel();
            itemGroup.m_groupName = _Group._EvaluationGroupContent;
            itemGroup.m_groupNo = _Group._index.ToString();

            foreach (MItemNormal _item in _Group.LstItem)
            {
                ItemRowControl itemControl = new ItemRowControl(ItemStyle.ITEM_STYLE_NORMAL, _item);
                itemControl.IsHeighAuto = true;

                //刷新分数
                itemControl.UpdateScore(updateScore);

                itemGroup.AddItem(itemControl);
            }

            sp.Children.Add(itemGroup);
        }
    }
}