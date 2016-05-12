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
    /// 服务基础评价-->人员的类型
    /// </summary>
    class ItemControl_personnel_A : BaseItem_A
    {
        #region Var  constructor、Load

        MItem_personnel_A _item;

        /// <summary>
        /// 序号
        /// </summary>
        public string m_strNo;

        /// <summary>
        /// 岗位
        /// </summary>
        public string m_strPost;

        /// <summary>
        /// 评估标注和要求500台
        /// </summary>
        public string m_strEvaluation_of_count_0;

        /// <summary>
        /// 评估标注和要求500-1000台
        /// </summary>
        public string m_strEvaluation_of_count_1;

        /// <summary>
        /// 评估标注和要求1000-1500台
        /// </summary>
        public string m_strEvaluation_of_count_2;

        /// <summary>
        /// 评估标注和要求1500-2000台
        /// </summary>
        public string m_strEvaluation_of_count_3;

        /// <summary>
        /// 评估标注和要求2000-2500台
        /// </summary>
        public string m_strEvaluation_of_count_4;

        /// <summary>
        /// 评估标注和要求2500-3000台
        /// </summary>
        public string m_strEvaluation_of_count_5;

        /// <summary>
        /// 评估标注和要求3000-3500台
        /// </summary>
        public string m_strEvaluation_of_count_6;

      

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为18等份，columnRatio1占2份
        /// </summary>
        int columnRatio1=2;

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为18等份，columnRatio2占3份
        /// </summary>
        int columnRatio2 = 3;

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为18等份，columnRatio3占2份
        /// </summary>
        int columnRatio3 = 2;

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为18等份，columnRatio4占2份
        /// </summary>
        int columnRatio4 = 2;

        int columnRatio5 = 2;

        int columnRatio6 = 2;

        int columnRatio7 = 2;

        int columnRatio8 = 2;

        int columnRatio9 = 2;


        TextBlock tbkContent0;
        TextBlock tbkContent1;
        TextBlock tbkContent2;
        TextBlock tbkContent3;
        TextBlock tbkContent4;
        TextBlock tbkContent5;
        TextBlock tbkContent6;
        TextBlock tbkContent7;
        TextBlock tbkContent8;

        public ItemControl_personnel_A(MItem_personnel_A item)
            : base(9, item.bIsLastTimePass, item.bIsSelfEvaluation,item.bIsEvaluationOfTour, item.isEvaluate,  item.remarks)
        {

            m_strEvaluation_of_count_0 = item._strEvaluation_of_count_0;
            m_strEvaluation_of_count_1 = item._strEvaluation_of_count_1;
            m_strEvaluation_of_count_2 = item._strEvaluation_of_count_2;
            m_strEvaluation_of_count_3 = item._strEvaluation_of_count_3;
            m_strEvaluation_of_count_4 = item._strEvaluation_of_count_4;
            m_strEvaluation_of_count_5 = item._strEvaluation_of_count_5;
            m_strEvaluation_of_count_6 = item._strEvaluation_of_count_6;
            m_strNo = item._strNo;
            m_strPost = item._strPost;
            _item = item;
        }    

        #endregion


        #region 布局，添加子控件 设置需要亮显示的列
       

        protected override void AddControlIntoPanel1Border()
        {
            listBorder[0].Child = tbkContent0;
            listBorder[1].Child = tbkContent1;
            listBorder[2].Child = tbkContent2;
            listBorder[3].Child = tbkContent3;
            listBorder[4].Child = tbkContent4;
            listBorder[5].Child = tbkContent5;
            listBorder[6].Child = tbkContent6;
            listBorder[7].Child = tbkContent7;
            listBorder[8].Child = tbkContent8;    
            SetBorderHigh();
        }

        protected override void InitControlOfPanel1()
        {
            tbkContent0 = new TextBlock();
            SetTextBlokStyle(tbkContent0, m_strNo, HorizontalAlignment.Center);

            tbkContent1 = new TextBlock();
            SetTextBlokStyle(tbkContent1, m_strPost, HorizontalAlignment.Center);

            tbkContent2 = new TextBlock();
            SetTextBlokStyle(tbkContent2, m_strEvaluation_of_count_0, HorizontalAlignment.Center);

            tbkContent3 = new TextBlock();
            SetTextBlokStyle(tbkContent3, m_strEvaluation_of_count_1, HorizontalAlignment.Center);

            tbkContent4 = new TextBlock();
            SetTextBlokStyle(tbkContent4, m_strEvaluation_of_count_2, HorizontalAlignment.Center);

            tbkContent5 = new TextBlock();
            SetTextBlokStyle(tbkContent5, m_strEvaluation_of_count_3, HorizontalAlignment.Center);

            tbkContent6 = new TextBlock();
            SetTextBlokStyle(tbkContent6, m_strEvaluation_of_count_4, HorizontalAlignment.Center);

            tbkContent7 = new TextBlock();
            SetTextBlokStyle(tbkContent7, m_strEvaluation_of_count_5, HorizontalAlignment.Center);

            tbkContent8 = new TextBlock();
            SetTextBlokStyle(tbkContent8, m_strEvaluation_of_count_6, HorizontalAlignment.Center);
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

        void SetBorderHigh()
        {
            if ( !m_bIsPassLastEvaluationResults || !m_bIsPassSinceEvaluationResult)
            {
                listBorder[0].Background = borderHighBackground;
                listBorder[1].Background = borderHighBackground;
                listBorder[2].Background = borderHighBackground;
                listBorder[3].Background = borderHighBackground;
                listBorder[4].Background = borderHighBackground;
                listBorder[5].Background = borderHighBackground;
                listBorder[6].Background = borderHighBackground;
                listBorder[7].Background = borderHighBackground;
                listBorder[8].Background = borderHighBackground;
            }           
        }
        #endregion


        protected override void ChangeScoreValue(bool isQualified)
        {
            _item.GetScore(isQualified);
        }

    }
}
