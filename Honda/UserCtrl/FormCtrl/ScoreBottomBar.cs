using Honda.Globals;
using Honda.Interface;
using Honda.Model.Form;
using Honda.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace Honda.UserCtrl
{
    public class ScoreBottomBar : BaseItem_B
    {
        /// <summary>
        /// 检查方法
        /// </summary>
        public string _InspectionMethod;

        /// <summary>
        /// 评分标准
        /// </summary>
        public string _EvaluationCriterion;

        /// <summary>
        /// 上次评分
        /// </summary>
        private double _cellLastScore;

        /// <summary>
        /// 特约店自评
        /// </summary>
        private double _cellSelfScore;

        /// <summary>
        /// 巡回评价
        /// </summary>
        private double _evaluationOfTourScore;

        #region UI Var

        //弹出窗口
        private Popup popup;
        private TextBlock tbkTitleSocre;
        private Image imaHelp;
        private TextBlock textHelp;
        private TextBlock tbkLastEvaluationScore;
        private TextBlock tbkselfEvaluationScore;
        private TextBlock tbkEvaluationOfTourScore;
        private StackPanel sp;
        private readonly string strHelpImaUri = "/Assets/HELPBUTTE.bmp";

        #endregion

        #region 宽度比例 Var

        /// <summary>
        /// 宽度的比例
        /// </summary>
        private double columnRatio1 = 25;

        /// <summary>
        /// 宽度的比例
        /// </summary>
        private double columnRatio2 = 3;

        /// <summary>
        /// 宽度的比例
        /// </summary>
        private double columnRatio3 = 3;

        /// <summary>
        /// 宽度的比例
        /// </summary>
        private double columnRatio4 = 6;

        /// <summary>
        /// 宽度的比例
        /// </summary>
        private double columnRatio5 = 3;

        #endregion

        public ScoreBottomBar(double lastScore, double SelfScore, double EvaluationOfTourScore, string inspectionMethod,
            string evaluationCriterion)
            : base(5)
        {
            m_high = GlobalValue.FORM_BOTTOM_HIGH;
            _cellLastScore = lastScore;
            _cellSelfScore = SelfScore;
            _evaluationOfTourScore = EvaluationOfTourScore;
            _InspectionMethod = inspectionMethod;
            _EvaluationCriterion = evaluationCriterion;

            borderBackground = new SolidColorBrush(Color.FromArgb(255, 228, 228, 228));
            borderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        public ScoreBottomBar()
            : base(5)
        {
            m_high = GlobalValue.FORM_BOTTOM_HIGH;
            borderBackground = new SolidColorBrush(Color.FromArgb(255, 228, 228, 228));
            borderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        /// <summary>
        /// 初始化控件的样式和内容
        /// </summary>
        protected override void InitControl()
        {
            tbkTitleSocre = new TextBlock();
            SetTextBlokStyle(tbkTitleSocre, "得分");

            tbkLastEvaluationScore = new TextBlock();
            SetTextBlokStyle(tbkLastEvaluationScore, _cellLastScore.ToString(), HorizontalAlignment.Center);

            tbkselfEvaluationScore = new TextBlock();
            SetTextBlokStyle(tbkselfEvaluationScore, _cellSelfScore.ToString(), HorizontalAlignment.Center);

            tbkEvaluationOfTourScore = new TextBlock();
            SetTextBlokStyle(tbkEvaluationOfTourScore, _evaluationOfTourScore.ToString(), HorizontalAlignment.Center);

            sp = new StackPanel();
            InitStackPanel(sp);

            tbkTitleSocre.Margin = new Thickness(250, 0, 0, 0);
            sp.Children.Add(tbkTitleSocre);


            InitTextHelpHint();
        }

        /// <summary>
        /// 表格中（检查标准与评价标准）说明的的样式设置
        /// </summary>
        private void InitTextHelpHint()
        {
            //TextBlock
            imaHelp = new Image();
            SetImageStyle(imaHelp, strHelpImaUri);
            imaHelp.Margin = new Thickness(70, 0, 0, 0);
            sp.Children.Add(imaHelp);
            //TextBox
            textHelp = new TextBlock();
            textHelp.TextDecorations = TextDecorations.Underline;
            textHelp.Text = "检查标准与评价标准";
            textHelp.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 153, 204));
            textHelp.FontSize = 22;
            textHelp.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            textHelp.Cursor = Cursors.Hand;
            sp.Children.Add(textHelp);

            //Popup
            popup = new Popup();
            popup.PlacementTarget = textHelp;
            popup.AllowsTransparency = true;
            popup.Placement = PlacementMode.Relative;
            popup.PopupAnimation = PopupAnimation.Scroll;
            popup.VerticalOffset = -200;
            popup.HorizontalOffset = 400;
            popup.StaysOpen = false;
            popup.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

            //popup - children
            StackPanel panel = new StackPanel();
            panel.Orientation = System.Windows.Controls.Orientation.Vertical;
            panel.HorizontalAlignment = HorizontalAlignment.Stretch;
            panel.VerticalAlignment = VerticalAlignment.Stretch;
            panel.Height = 460;
            panel.Width = 320;
            panel.Background = new SolidColorBrush(Color.FromArgb(255, 242, 242, 242));

            //检查方法
            TextBlock tbkCheckMethod = new TextBlock();
            tbkCheckMethod.Foreground = new SolidColorBrush(Color.FromArgb(255, 119, 119, 119));
            tbkCheckMethod.FontSize = 20;
            tbkCheckMethod.TextWrapping = TextWrapping.Wrap;
            tbkCheckMethod.Margin = new Thickness(15, 15, 15, 0);
            string temp = "检查方法：";
            tbkCheckMethod.Text = temp + _InspectionMethod;
            panel.Children.Add(tbkCheckMethod);

            //评分标准
            TextBlock tbkScaleOfmarks = new TextBlock();
            tbkScaleOfmarks.Foreground = new SolidColorBrush(Color.FromArgb(255, 119, 119, 119));
            tbkScaleOfmarks.FontSize = 20;
            tbkScaleOfmarks.TextWrapping = TextWrapping.Wrap;
            string temp1 = "评分标准：";
            tbkScaleOfmarks.Text = temp1 + _EvaluationCriterion;
            tbkScaleOfmarks.Margin = new Thickness(15, 40, 15, 0);
            panel.Children.Add(tbkScaleOfmarks);

            popup.Child = panel;
            this.Children.Add(popup);
        }

        /// <summary>
        /// 往边框中添加控件
        /// </summary>
        protected override void AddControlIntoBorder()
        {
            listBorder[0].Child = sp;
            listBorder[1].Child = tbkLastEvaluationScore;
            listBorder[2].Child = tbkselfEvaluationScore;
            listBorder[3].Child = tbkEvaluationOfTourScore;
        }


        private void InitStackPanel(StackPanel sp)
        {
            sp.VerticalAlignment = VerticalAlignment.Stretch;
            sp.HorizontalAlignment = HorizontalAlignment.Stretch;
            sp.Orientation = Orientation.Horizontal;
        }

        #region 设置事件

        protected override void SetControlEvent()
        {
            textHelp.MouseUp += (o, e) => { popup.IsOpen = true; };
        }

        #endregion

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
            }
            return new GridLength(dbWith, GridUnitType.Star);
        }

        /// <summary>
        /// 设置分数棒的宽度比例
        /// </summary>
        public void SetColumnScole(double columnRatio1, double columnRatio2, double columnRatio3, double columnRatio4,
            double columnRatio5)
        {
            this.columnRatio1 = columnRatio1;
            this.columnRatio2 = columnRatio2;
            this.columnRatio3 = columnRatio3;
            this.columnRatio4 = columnRatio4;
            this.columnRatio5 = columnRatio5;
        }


        /// <summary>
        /// 设置分数
        /// </summary>
        public void SetScore(double lastScore, double SelfScore, double EvaluationOfTourScore)
        {
            _cellLastScore = lastScore;
            _cellSelfScore = SelfScore;
            _evaluationOfTourScore = EvaluationOfTourScore;

            if (tbkLastEvaluationScore == null) return;
            tbkLastEvaluationScore.Text = _cellLastScore.ToString();

            if (tbkselfEvaluationScore == null) return;
            tbkselfEvaluationScore.Text = _cellSelfScore.ToString();

            if (tbkEvaluationOfTourScore == null) return;
            tbkEvaluationOfTourScore.Text = _evaluationOfTourScore.ToString();
        }
    }
}