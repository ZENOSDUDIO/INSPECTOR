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
    class ItemControl_Suggest_A : BaseItem_B
    {
        #region Var、 Load、construction Fun

        MItem_Suggest_PlusProject _item;

        Action _action_score;

        EvaluationSort _evaluationSort;

        Action _action_GoForem;

        #region UI Var
        /// <summary>
        /// 宽度的比例（可用宽度的分为22等份，columnRatio1占1份
        /// </summary>
        int columnRatio1 = 1;

        /// <summary>
        /// 宽度的比例（可用宽度的分为22等份，columnRatio2占2份
        /// </summary>
        int columnRatio2 = 2;

        /// <summary>
        /// 宽度的比例（可用宽度的分为22等份，columnRatio3占1份
        /// </summary>
        int columnRatio3 = 1;

        /// <summary>
        /// 宽度的比例（可用宽度的分为22等份，columnRatio4占8份
        /// </summary>
        int columnRatio4 = 8;

        /// <summary>
        /// 宽度的比例（可用宽度的分为22等份，columnRatio5占2份
        /// </summary>
        int columnRatio5 = 2;

        /// <summary>
        /// 宽度的比例（可用宽度的分为22等份，columnRatio6占2份
        /// </summary>
        int columnRatio6 = 2;

        /// <summary>
        /// 宽度的比例（可用宽度的分为22等份，columnRatio7占2份
        /// </summary>
        int columnRatio7 = 2;

        /// <summary>
        /// 宽度的比例（可用宽度的分为22等份，columnRatio8占2份
        /// </summary>
        int columnRatio8 = 2;

        /// <summary>
        /// 宽度的比例（可用宽度的分为22等份，columnRatio9占2份
        /// </summary>
        int columnRatio9 = 2;

        string strContent0;
        string strContent1;
        string strContent2;
        string strContent3;
        string strContent4;
        string strContent5;
        /// <summary>
        /// 巡回评价是否合格
        /// </summary>
        public bool m_bIsQualified;

        readonly string strRemarkImaUri = "/Assets/page_icons_compile.png";
        readonly string strQualifiedImaUri = "/Assets/page_icons1_1.png";
        readonly string strDisqualificationImaUri = "/Assets/page_icons1.png";

        TextBlock tbkContent0;
        TextBlock tbkContent1;
        TextBlock tbkContent2;
        TextBlock tbkContent3;
        TextBlock tbkContent4;
        TextBlock tbkContent5;
        TextBlock tbkGradeTitle;
        /// <summary>
        /// 评价的分数
        /// </summary>
        TextBlock tbkScore;

        Image imaQualified;
        Image imaDisqualification;
        Image imgRemark;

        #endregion 
       
        public ItemControl_Suggest_A(MItem_Suggest_PlusProject item)
            : base(9,item._evaluationSort)
        {
            strContent0 = item.strNo;
            strContent1 = item.strEvaluationItem;
            strContent2 = item._TotalScore.ToString();
            strContent3 = item.strInspectionItem;
            strContent4 = item._cellLastScore.ToString();
            strContent5 = item._cellSelfScore.ToString() ;
            m_bIsQualified = item.bIsEvaluationOfTour;
            _evaluationSort = item._evaluationSort;
            _item = item;

        }
        #endregion


        #region 布局，初始化控件
        
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
            SetTextBlokStyle(tbkContent0,strContent0,HorizontalAlignment.Center);

            tbkContent1 = new TextBlock();
            SetTextBlokStyle(tbkContent1, strContent1, HorizontalAlignment.Center);

            tbkContent2 = new TextBlock();
            SetTextBlokStyle(tbkContent2, strContent2, HorizontalAlignment.Center);

            tbkContent3 = new TextBlock();
            SetTextBlokStyle(tbkContent3, strContent3);

            tbkContent4 = new TextBlock();
            SetTextBlokStyle(tbkContent4, strContent4, HorizontalAlignment.Center);

            tbkContent5 = new TextBlock();
            SetTextBlokStyle(tbkContent5, strContent5, HorizontalAlignment.Center);

            //设置巡回评价图标
            imaQualified = new Image();
            imaDisqualification = new Image();
            if (_item.isEvaluate)
            {
                if (m_bIsQualified)
                {
                    SetImageStyle(imaQualified, strQualifiedImaUri);
                    SetImageStyle(imaDisqualification, strDisqualificationImaUri);
                }
                else
                {
                    SetImageStyle(imaQualified, strDisqualificationImaUri);
                    SetImageStyle(imaDisqualification, strQualifiedImaUri);
                }
            }
            else
            {
                SetImageStyle(imaQualified, strDisqualificationImaUri);
                SetImageStyle(imaDisqualification, strDisqualificationImaUri);
            }
           
            imgRemark = new Image();
            SetImageStyle(imgRemark, strRemarkImaUri);

            // 设置标题和分数的TextBlock
            SetGrade();
        }

        /// <summary>
        /// 设置标题和分数的TextBlock
        /// </summary>
        void SetGrade()
        {
            
            if (_evaluationSort == EvaluationSort.plusesEvaluation)
            {
                tbkGradeTitle = new TextBlock();
                tbkGradeTitle.Text = "评分表";
                tbkGradeTitle.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
                tbkGradeTitle.FontSize = 20;
                tbkGradeTitle.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                tbkGradeTitle.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                tbkGradeTitle.MouseUp += (o, e) =>
                {
                    if (_action_GoForem != null)
                    {
                        _action_GoForem();
                    }
                };

                tbkScore = new TextBlock();
                SetTextBlokStyle(tbkScore, _item._cellTourScore.ToString(), HorizontalAlignment.Right);

            }
        }

        protected override void AddControlIntoBorder()
        {
            listBorder[0].Child = tbkContent0;
            listBorder[1].Child = tbkContent1;
            listBorder[2].Child = tbkContent2;
            listBorder[3].Child = tbkContent3;
            listBorder[4].Child = tbkContent4;
            listBorder[5].Child = tbkContent5;
            if(_evaluationSort == EvaluationSort.normalEvaluation)
            {
                listBorder[6].Child = imaQualified;
                listBorder[7].Child = imaDisqualification;
            }else if(_evaluationSort == EvaluationSort.plusesEvaluation)
            {
                listBorder[6].Child = tbkScore;
                listBorder[7].Child = tbkGradeTitle;
            }
            
            listBorder[8].Child = imgRemark;
            SetBorderBackgroundHigh();
        }

        #endregion

        #region 设置事件

        /// <summary>
        /// 为控件设置时间
        /// </summary>
        protected override void SetControlEvent()
        {
            imgRemark.MouseUp += imgRemark_MouseUp;
            if(isDetail)
            {
                return;
            }
            imaQualified.MouseUp += imaQualified_MouseUp;
            imaDisqualification.MouseUp += imaDisqualification_MouseUp;
        }

        /// <summary>
        /// 当用户单击备注图片时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 当用户单击“不合格按钮时”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imaDisqualification_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            m_bIsQualified = false;
            SetImageStyle(imaQualified, strDisqualificationImaUri);
            SetImageStyle(imaDisqualification, strQualifiedImaUri);

            //设置分数
            _item.GetScore(m_bIsQualified);
            _action_score();
        }

        /// <summary>
        /// 当用户单击合格按钮的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imaQualified_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            m_bIsQualified = true;
            SetImageStyle(imaQualified, strQualifiedImaUri);
            SetImageStyle(imaDisqualification, strDisqualificationImaUri);

            //设置分数
            _item.GetScore(m_bIsQualified);
            _action_score();
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

        void SetBorderBackgroundHigh()
        {
        }

        /// <summary>
        /// 跳转到五星级仓库表格
        /// </summary>
        /// <param name="action"></param>
        public void SetGoForm(Action action)
        {
            _action_GoForem = action;
        }
    }
}
