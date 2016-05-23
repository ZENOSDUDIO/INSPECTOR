using Honda.Model;
using Honda.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Honda.UserCtrl
{
    /// <summary>
    /// 服务基础评价-->建议加分项表中的类型
    /// </summary>
    internal class ItemControlPreview : BaseItem_C
    {
        private MItemNormal _item;
        public string m_strContent1;

        public string m_strContent2;

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为8等份，columnRatio1占67份
        /// </summary>
        private int columnRatio1 = 4;

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为8等份，columnRatio2占580份
        /// </summary>
        private int columnRatio2 = 23;


        private TextBlock tbkContent0;
        private TextBlock tbkContent1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNo">第一列的内容</param>
        /// <param name="strCheckItemConten">第二列的内容</param>
        /// <param name="bIsQualified">巡回评价是否合格</param>
        /// <param name="bIsPassLastEvaluationResults">上次评价是否合格</param>
        /// <param name="bIsPassSinceEvaluationResult">自评是否合格</param>
        public ItemControlPreview(MItemNormal item)
            : base(
                2, item.bIsLastTimePass, item.bIsSelfEvaluation, item.bIsEvaluationOfTour, item.isEvaluate, item.remarks
                )
        {
            _item = item;
            m_strContent1 = item.StrContent1;
            m_strContent2 = item.StrContent2;
        }


        /// <summary>
        /// 前两列放置分别放置一个Border控件，用来显示边框， 而每个边框里面放置一个TextBlock
        /// </summary>
        protected override void AddControlIntoPanel1Border()
        {
            listBorder[0].Child = tbkContent0;
            listBorder[1].Child = tbkContent1;
            SetBorderBackgroundHigh();
        }

        /// <summary>
        /// 出事化控件
        /// </summary>
        protected override void InitControlOfPanel1()
        {
            tbkContent0 = new TextBlock();
            SetTextBlokStyle(tbkContent0, m_strContent1, HorizontalAlignment.Center);

            tbkContent1 = new TextBlock();
            SetTextBlokStyle(tbkContent1, m_strContent2);
        }

        /// <summary>
        /// 设置前两列的宽度（按比例设置）
        /// </summary>
        /// <param name="iCount"></param>
        /// <returns></returns>
        protected override GridLength SetGdPanel1ColumnWith(int iCount)
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
            }
            return new GridLength(dbWith, GridUnitType.Star);
        }


        /// <summary>
        /// 设置前两列高亮显示
        /// </summary>
        private void SetBorderBackgroundHigh()
        {
            if (!m_bIsPassLastEvaluationResults || !m_bIsPassSinceEvaluationResult)
            {
                listBorder[0].Background = borderHighBackground;
                listBorder[1].Background = borderHighBackground;
            }
        }

        /// <summary>
        /// 设置钱两列的宽度比例
        /// </summary>
        /// <param name="_columnRatio1"></param>
        /// <param name="_columnRatio2"></param>
        public void SetWith(int _columnRatio1, int _columnRatio2)
        {
            columnRatio1 = _columnRatio1;
            columnRatio2 = _columnRatio2;
        }

        protected override void ChangeScoreValue(bool isQualified)
        {
            _item.GetScore(isQualified);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}