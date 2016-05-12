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
    /// 服务基础评价-->硬件机修工具类型
    /// </summary>
    class ItemControl_tool_B : BaseItem_A
    {
        MItem_Tool_B _item;

       

        /// <summary>
        /// 两列宽度的比例（可用宽度的分为8等份，columnRatio1占1份
        /// </summary>
        int columnRatio1=1;
  
        /// <summary>
        /// 工具的内容描述
        /// </summary>
        public string m_strContent;


        TextBlock tbkContent;




        public ItemControl_tool_B(MItem_Tool_B item)
            : base(1, item.bIsLastTimePass, item.bIsSelfEvaluation, item.bIsEvaluationOfTour, item.isEvaluate, item.remarks)
        {
            m_strContent = item._ToolEquipment;
            _item = item;
        }

 


        protected override void AddControlIntoPanel1Border()
        {
            listBorder[0].Child = tbkContent;
            SetBorderBackgroundHigh();
        }

        protected override void InitControlOfPanel1()
        {
            tbkContent = new TextBlock();
            SetTextBlokStyle(tbkContent, m_strContent, HorizontalAlignment.Center);

         
        }

        protected override GridLength SetGdPanel1ColumnWith(int iCount)
        {
            double dbWith = 0;
            switch (iCount)
            {
                case 0:
                    dbWith = columnRatio1;
                    break;
              
            }
            return new GridLength(dbWith, GridUnitType.Star);
        }

        
        void SetBorderBackgroundHigh()
        {
            if ( !m_bIsPassLastEvaluationResults || !m_bIsPassSinceEvaluationResult)
            {
                listBorder[0].Background = borderHighBackground;
            }           
        }

        protected override void ChangeScoreValue(bool isQualified)
        {
            _item.GetScore(isQualified);
        }


    }
}
