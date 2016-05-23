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
    /// 服务基础评价-->硬件钣喷工具类型
    /// </summary>
    class ItemControl_tool_A : BaseItem_A
    {
        MItem_Tool_A _item;

        /// <summary>
        /// 工具名
        /// </summary>
        private string m_strTooLName;

        /// <summary>
        /// 小于200台
        /// </summary>
        private string m_strCountNumberScale1;

        /// <summary>
        /// 200~400台
        /// </summary>
        private string m_strCountNumberScale2;

        /// <summary>
        /// 400~ 500台
        /// </summary>
        public string m_strCountNumberScale3;

        /// <summary>
        /// 大于 500台
        /// </summary>
        public string m_strCountNumberScale4;

        /// <summary>
        /// 5列宽度的比例（可用宽度的分为13等份，columnRatio1占1份
        /// </summary>
        double columnRatio1 = 5;

        /// <summary>
        /// 5列宽度的比例（可用宽度的分为13等份，columnRatio2占2份
        /// </summary>
        double columnRatio2 = 2.5;

        /// <summary>
        /// 5列宽度的比例（可用宽度的分为13等份，columnRatio3占2份
        /// </summary>
        double columnRatio3 = 2.5;

        /// <summary>
        /// 5列宽度的比例（可用宽度的分为13等份，columnRatio4占2份
        /// </summary>
        double columnRatio4 = 2.5;

        /// <summary>
        /// 5列宽度的比例（可用宽度的分为13等份，columnRatio5占2份
        /// </summary>
        double columnRatio5 = 2.5;


        TextBlock tbkContent0;
        TextBlock tbkContent1;
        TextBlock tbkContent2;
        TextBlock tbkContent3;
        TextBlock tbkContent4;


        public ItemControl_tool_A(MItem_Tool_A item)
            : base(5, item.bIsLastTimePass, item.bIsSelfEvaluation,item.bIsEvaluationOfTour, item.isEvaluate, item.remarks)
        {
            _item = item;
            m_strTooLName = item.StrMachineName;
            m_strCountNumberScale1 = item.strCountNumberScale1;
            m_strCountNumberScale2 = item.strCountNumberScale2;
            m_strCountNumberScale3 = item.strCountNumberScale3;
            m_strCountNumberScale4 = item.strCountNumberScale4;
        }

       

        protected override void AddControlIntoPanel1Border()
        {
            listBorder[0].Child = tbkContent0;
            listBorder[1].Child = tbkContent1;
            listBorder[2].Child = tbkContent2;
            listBorder[3].Child = tbkContent3;
            listBorder[4].Child = tbkContent4;
            SetBorderBackgroundHigh();
        }

        protected override void InitControlOfPanel1()
        {
            tbkContent0 = new TextBlock();
            SetTextBlokStyle(tbkContent0, m_strTooLName, HorizontalAlignment.Center);

            tbkContent1 = new TextBlock();
            SetTextBlokStyle(tbkContent1, m_strCountNumberScale1, HorizontalAlignment.Center);

            tbkContent2 = new TextBlock();
            SetTextBlokStyle(tbkContent2, m_strCountNumberScale2, HorizontalAlignment.Center);

            tbkContent3 = new TextBlock();
            SetTextBlokStyle(tbkContent3, m_strCountNumberScale3, HorizontalAlignment.Center);

            tbkContent4 = new TextBlock();
            SetTextBlokStyle(tbkContent4, m_strCountNumberScale4, HorizontalAlignment.Center);
        }

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


        void SetBorderBackgroundHigh()
        {
            if (!m_bIsPassLastEvaluationResults || !m_bIsPassSinceEvaluationResult)
            {
                listBorder[0].Background = borderHighBackground;
                listBorder[1].Background = borderHighBackground;
                listBorder[2].Background = borderHighBackground;
                listBorder[3].Background = borderHighBackground;
                listBorder[4].Background = borderHighBackground;
            }
        }


        protected override void ChangeScoreValue(bool isQualified)
        {
            _item.GetScore(isQualified);

        }

    }
}
