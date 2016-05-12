using Honda.Globals;
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
using System.Windows.Input;
using System.Windows.Media;


namespace Honda.UserCtrl
{
    /// <summary>
    /// 服务基础评价-->人员的类型C
    /// </summary>
    class ItemControl_personnel_C : BaseItem_B
    {
        Action _action_score;
        MItem_personnel_C _item;

        #region UI Var
        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio1占4份
        /// </summary>
        double columnRatio1 = 2;

        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio2占3份
        /// </summary>
        double columnRatio2 = 17;

        /// <summary>
        /// 宽度的比例（可用宽度的分为244等份，columnRatio3占4份
        /// </summary>
        double columnRatio3 = 2.375;

        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio4占3份
        /// </summary>
        double columnRatio4 = 2.375;

        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio5占12份
        /// </summary>
        double columnRatio5 = 4.75;

        /// <summary>
        /// 宽度的比例（可用宽度的分为44等份，columnRatio6占4份
        /// </summary>
        double columnRatio6 = 2.375;

        string _strNo;
        string _strDescribe;
        string _strLastScore;
        string _strSelfScore;
        string _strTourScore;



        readonly string strRemarkImaUri = "/Assets/page_icons_compile.png";
        TextBlock tbkNo;
        TextBlock tbkDescribe;
        TextBlock tbkLastScore;
        TextBlock tbkSelfScore;
        TextBox tbTourScore;
        Image imgRemark;
        #endregion 
       
        public ItemControl_personnel_C(MItem_personnel_C item)
            : base(6)
        {
            _item = item;
            _strNo = item._strNo;
            _strDescribe = item._strDescribe;
            _strLastScore = item._cellLastScore.ToString();
            _strSelfScore = item._cellSelfScore.ToString();
            _strTourScore = item._cellTourScore.ToString();

        }

        #region UI 布局，初始化
        
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
             
            }
            return new GridLength(dbWith, GridUnitType.Star);
        }

        protected override void InitControl()
        {
            tbkNo = new TextBlock();
            SetTextBlokStyle(tbkNo,_strNo,HorizontalAlignment.Center);

            tbkDescribe = new TextBlock();
            SetTextBlokStyle(tbkDescribe, _strDescribe, HorizontalAlignment.Center);

            tbkLastScore = new TextBlock();
            SetTextBlokStyle(tbkLastScore, _strLastScore, HorizontalAlignment.Center);

            tbkSelfScore = new TextBlock();
            SetTextBlokStyle(tbkSelfScore, _strSelfScore, HorizontalAlignment.Center);

            //特约店评分
            tbTourScore = new TextBox();
            SetTextBoxStyle(tbTourScore,_strTourScore);
            if(isDetail)
            {
                tbTourScore.IsReadOnly = true;
            }else
            {
                tbTourScore.IsReadOnly = false;
            }

            imgRemark = new Image();
            SetImageStyle(imgRemark, strRemarkImaUri);

            
        }      
        

        protected override void AddControlIntoBorder()
        {
            listBorder[0].Child = tbkNo;
            listBorder[1].Child = tbkDescribe;
            listBorder[2].Child = tbkLastScore;
            listBorder[3].Child = tbkSelfScore;
            listBorder[4].Child = tbTourScore;
            listBorder[5].Child = imgRemark;
            SetBorderBackgroundHigh();
        }
        #endregion


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
            });

            if (!(bool)calculatorWindow.ShowDialog())
            {
                _item.GetScore(oldTourScore);
                tb.Text = oldTourScore.ToString();
                if (_action_score != null)
                {
                    _action_score();
                }
            }
        }
       

        void imgRemark_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(!isDetail)
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

        /// <summary>
        /// 更新分数
        /// </summary>
        /// <param name="action"></param>
        public void SetRefreshScore(Action action)
        {

            _action_score = action;
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
