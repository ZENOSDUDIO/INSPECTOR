using Honda.Model;
using Honda.Model.Form;
using Honda.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Honda.UserCtrl
{
    /// <summary>
    /// 服务基础评价-->建议加分项-->加分项类型
    /// </summary>
    class ItemControl_Suggest_B : BaseItem_B
    {
        #region Var
        MItem_Suggest_Warehouse _item;
        Action _action_score; //回调，当打分的时候回调
        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio1占4份
        /// </summary>
        int columnRatio1 = 4;

        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio2占3份
        /// </summary>
        int columnRatio2 = 3;

        /// <summary>
        /// 宽度的比例（可用宽度的分为244等份，columnRatio3占4份
        /// </summary>
        int columnRatio3 = 4;

        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio4占3份
        /// </summary>
        int columnRatio4 = 3;

        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio5占12份
        /// </summary>
        int columnRatio5 = 12;

        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio6占4份
        /// </summary>
        int columnRatio6 = 4;

        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio7占4份
        /// </summary>
        int columnRatio7 = 4;

        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio8占4份
        /// </summary>
        int columnRatio8 = 4;

        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio9占4份
        /// </summary>
        int columnRatio9 = 4;

        string strSmallItem;
        string strSort;
        string strTarget;
        double _itemScore;
        string strStandardForEvaluation;
        double _cellLastScore;
        double _cellSelfScore;


        /// <summary>
        /// 巡回评价分数
        /// </summary>
        private double cellTourScore;

        public double _cellTourScore
        {
            get
            {
                return cellTourScore;
            }
        }

        readonly string strRemarkImaUri = "/Assets/page_icons_compile.png";


        TextBlock tbkContent0;
        TextBlock tbkContent1;
        TextBlock tbkContent2;
        TextBlock tbkContent3;
        TextBlock tbkContent4;
        TextBlock tbkContent5;
        TextBlock tbkContent6;
        TextBox tbTourScore;

        /// <summary>
        /// ComboBox边框的颜色    
        /// </summary>
        public SolidColorBrush ComboBorderBrush = (SolidColorBrush)Application.Current.Resources["ComboxBorderBrush"];

        Image imgRemark;

        #endregion


        public ItemControl_Suggest_B(MItem_Suggest_Warehouse item)
            : base(9)
        {
            strSmallItem = item.strSmallItem;
            strSort = item.strSort;
            strTarget = item.strTarget;
            _itemScore = item._itemScore;
            strStandardForEvaluation = item.strStandardForEvaluation;
            _cellLastScore = item._cellLastScore;
            _cellSelfScore = item._cellSelfScore;
            cellTourScore = item._cellTourScore;
            _item = item;
        }

        protected override GridLength SetColumnWith(int iCount)
        {
            double dbWith = 0;
            switch (iCount)
            {
                case 0:
                    dbWith = columnRatio1;
                    break;
                case 1:
                    dbWith = columnRatio2;
                    break;
                case 2:
                    dbWith = columnRatio3;
                    break;
                case 3:
                    dbWith = columnRatio4;
                    break;
                case 4:
                    dbWith = columnRatio5;
                    break;
                case 5:
                    dbWith = columnRatio6;
                    break;
                case 6:
                    dbWith = columnRatio7;
                    break;
                case 7:
                    dbWith = columnRatio8;
                    break;
                case 8:
                    dbWith = columnRatio9;
                    break;

            }
            return new GridLength(dbWith, GridUnitType.Star);
        }

        protected override void InitControl()
        {
            tbkContent0 = new TextBlock();
            SetTextBlokStyle(tbkContent0, strSmallItem, HorizontalAlignment.Center);

            tbkContent1 = new TextBlock();
            SetTextBlokStyle(tbkContent1, strSort, HorizontalAlignment.Center);

            tbkContent2 = new TextBlock();
            SetTextBlokStyle(tbkContent2, strTarget, HorizontalAlignment.Center);

            tbkContent3 = new TextBlock();
            SetTextBlokStyle(tbkContent3, _itemScore.ToString(), HorizontalAlignment.Center);

            tbkContent4 = new TextBlock();
            SetTextBlokStyle(tbkContent4, strStandardForEvaluation, HorizontalAlignment.Left);

            tbkContent5 = new TextBlock();
            SetTextBlokStyle(tbkContent5, _cellLastScore.ToString(), HorizontalAlignment.Center);

            tbkContent6 = new TextBlock();
            SetTextBlokStyle(tbkContent6, _cellSelfScore.ToString(), HorizontalAlignment.Center);


            //特约店评分
            tbTourScore = new TextBox();

            SetTextBoxStyle(tbTourScore, cellTourScore.ToString());

            if (_item.bIsTourScoreOutOfRange)
            {
                //背景设置Red
                tbTourScore.Background = redBrush;
            }

            if (isDetail)
            {
                tbTourScore.IsReadOnly = true;
            }
            else
            {
                tbTourScore.IsReadOnly = false;
            }

            tbTourScore.Width = 90;

            //备注
            imgRemark = new Image();
            SetImageStyle(imgRemark, strRemarkImaUri);


        }



        //往border中添加控件
        protected override void AddControlIntoBorder()
        {
            listBorder[0].Child = tbkContent0;
            listBorder[1].Child = tbkContent1;
            listBorder[2].Child = tbkContent2;
            listBorder[3].Child = tbkContent3;
            listBorder[4].Child = tbkContent4;
            listBorder[5].Child = tbkContent5;
            listBorder[6].Child = tbkContent6;
            listBorder[7].Child = tbTourScore;
            listBorder[8].Child = imgRemark;
            SetBorderBackgroundHigh();
        }

        #region 设置事件

        protected override void SetControlEvent()
        {

            imgRemark.MouseUp += imgRemark_MouseUp;
            if (isDetail)
            {
                return;
            }


            tbTourScore.PreviewMouseUp += tbTourScore_PreviewMouseUp;

        }

        void tbTourScore_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null) return;
            double TourScore = 0;
            if (tb.Text.Length > 0)
            {
                TourScore = double.Parse(tb.Text);
            }
            double oldTourScore = TourScore;


            CalculatorWindow calculatorWindow = new CalculatorWindow();
            calculatorWindow.SetActionNum((Num) =>
            {

                TourScore = double.Parse(Num);
                _item.GetScore(TourScore);
                tb.Text = TourScore.ToString();

                if (_action_score != null)
                {
                    _action_score();
                }

                if (TourScore < 0 || TourScore > _item._itemScore)
                {
                    tb.Background = redBrush;
                }
                else
                {
                    tb.Background = whiteBrush;
                }
            });

            if (!(bool)calculatorWindow.ShowDialog())
            {
                if (oldTourScore < 0 || oldTourScore > _item._itemScore)
                {
                    tb.Background = redBrush;
                }
                else
                {
                    tb.Background = whiteBrush;
                }

                _item.GetScore(oldTourScore);
                tb.Text = oldTourScore.ToString();
                if (_action_score != null)
                {
                    _action_score();
                }
            }
        }


        /// <summary>
        /// 更新分数
        /// </summary>
        /// <param name="action"></param>
        public void SetRefreshScore(Action action)
        {

            _action_score = action;
        }

        void imgRemark_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!isDetail)
            {
                RemarksWindows remakWindows = new RemarksWindows(_item.remarks);
                remakWindows.ShowDialog();
            }
            else
            {
                RemarksDetailWindow remakDetail = new RemarksDetailWindow(_item.remarks);
                remakDetail.ShowDialog();
            }
        }







        #endregion

        /// <summary>
        /// 设置背景高亮   
        /// </summary>
        void SetBorderBackgroundHigh()
        {

        }
    }
}
