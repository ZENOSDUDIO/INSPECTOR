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
    /// 服务基础评价-->人员的类型B
    /// </summary>
    class ItemControl_personnel_B : BaseItem_A
    {
        #region Var

        MItem_personnel_B _item;
        /// <summary>
        /// 序号
        /// </summary>
         string _strNo;

        /// <summary>
        /// 岗位
        /// </summary>
         string _strPost;


         /// <summary>
         /// 岗位描述
         /// </summary>
         public string _strDescribe { set; get; }
      

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为18等份，columnRatio1占2份
        /// </summary>
        int columnRatio1=2;

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为19等份，columnRatio2占3份
        /// </summary>
        int columnRatio2 = 3;

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为19等份，columnRatio3占14份
        /// </summary>
        int columnRatio3 = 14;


        TextBlock tbkContent0;
        TextBlock tbkContent1;
        TextBlock tbkContent2;
       

        #endregion

        public ItemControl_personnel_B(MItem_personnel_B item)
            : base(3, item.bIsLastTimePass, item.bIsSelfEvaluation,item.bIsEvaluationOfTour,item.isEvaluate, item.remarks)
        {
            _item = item;
            _strDescribe = item._strDescribe;
            _strNo = item._strNo ;
            _strPost = item._strPost;     
        }

        #region UI 布局
        protected override void AddControlIntoPanel1Border()
        {
            listBorder[0].Child = tbkContent0;
            listBorder[1].Child = tbkContent1;
            listBorder[2].Child = tbkContent2;

            SetBorderHigh();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        protected override void InitControlOfPanel1()
        {
            tbkContent0 = new TextBlock();
            SetTextBlokStyle(tbkContent0, _strNo, HorizontalAlignment.Center);

            tbkContent1 = new TextBlock();
            SetTextBlokStyle(tbkContent1, _strPost, HorizontalAlignment.Center);

            tbkContent2 = new TextBlock();
            SetTextBlokStyle(tbkContent2, _strDescribe, HorizontalAlignment.Center);


        }

        /// <summary>
        /// 按比例分配各列的宽度
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

                case 2:
                    dbWith = columnRatio3;
                    break;


            }
            return new GridLength(dbWith, GridUnitType.Star);
        }
        #endregion
        

        #region Data Action
        /// <summary>
        /// 设置背景高亮
        /// </summary>
        void SetBorderHigh()
        {
            if (!m_bIsPassLastEvaluationResults || !m_bIsPassSinceEvaluationResult)
            {
                listBorder[0].Background = borderHighBackground;
                listBorder[1].Background = borderHighBackground;
                listBorder[2].Background = borderHighBackground;

            }
        }

        /// <summary>
        /// 分数改变时，改变数据源的分数
        /// </summary>
        /// <param name="TourScore"></param>
        protected override void ChangeScoreValue(bool isQualified)
        {
            _item.GetScore(isQualified);
        }
        #endregion
        

    }
}
