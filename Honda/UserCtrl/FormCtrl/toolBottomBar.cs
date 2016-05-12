using Honda.Globals;
using Honda.Interface;
using Honda.Model.Form;
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
    public class toolBottomBar : BaseItem_B
    {
        private TextBlock tbkdeviceNumber1;
        private TextBlock tbkdeviceNumber2;
        private TextBlock tbkdeviceNumber3;
        private TextBlock tbkdeviceNumber4;

        /// <summary>
        /// 小于200台
        /// </summary>
        private string deviceNumber1;

        /// <summary>
        /// 200～400台
        /// </summary>
        private string deviceNumber2;

        /// <summary>
        /// 400～500台
        /// </summary>
        private string deviceNumber3;

        /// <summary>
        /// 大于500台
        /// </summary>
        private string deviceNumber4;


        public toolBottomBar(string _deviceNumber1 = "小于200台", string _deviceNumber2 = "200～400台",
            string _deviceNumber3 = "400～500台", string _deviceNumber4 = "大于500台")
            : base(6)
        {
            deviceNumber1 = _deviceNumber1;
            deviceNumber2 = _deviceNumber2;
            deviceNumber3 = _deviceNumber3;
            deviceNumber4 = _deviceNumber4;

            m_high = 80;
            borderBackground = new SolidColorBrush(Color.FromArgb(255, 228, 228, 228));
            borderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }


        protected override void InitControl()
        {
            tbkdeviceNumber1 = new TextBlock();
            SetTextBlokStyle(tbkdeviceNumber1, deviceNumber1, HorizontalAlignment.Center);

            tbkdeviceNumber2 = new TextBlock();
            SetTextBlokStyle(tbkdeviceNumber2, deviceNumber2, HorizontalAlignment.Center);

            tbkdeviceNumber3 = new TextBlock();
            SetTextBlokStyle(tbkdeviceNumber3, deviceNumber3, HorizontalAlignment.Center);


            tbkdeviceNumber4 = new TextBlock();
            SetTextBlokStyle(tbkdeviceNumber4, deviceNumber4, HorizontalAlignment.Center);
        }

        protected override void AddControlIntoBorder()
        {
            listBorder[1].Child = tbkdeviceNumber1;
            listBorder[2].Child = tbkdeviceNumber2;
            listBorder[3].Child = tbkdeviceNumber3;
            listBorder[4].Child = tbkdeviceNumber4;
        }

        #region 设置事件

        protected override void SetControlEvent()
        {
        }

        #endregion

        protected override GridLength SetColumnWith(int iCount)
        {
            double dbWith = 0;
            switch (iCount)
            {
                case 0:
                    dbWith = 11.0;
                    break;
                case 1:
                    dbWith = 2.5;
                    break;
                case 2:
                    dbWith = 2.5;
                    break;
                case 3:
                    dbWith = 2.5;
                    break;
                case 4:
                    dbWith = 2.5;
                    break;
                case 5:
                    dbWith = 15.0;
                    break;
            }
            return new GridLength(dbWith, GridUnitType.Star);
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}