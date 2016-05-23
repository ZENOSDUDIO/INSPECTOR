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
    public class ImageButtonEx : Button
    {
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(
                "ImageSource",
                typeof (ImageSource),
                typeof (ImageButtonEx),
                null);

        public static readonly DependencyProperty ImagePressSourceProperty =
            DependencyProperty.Register(
                "ImagePressSource",
                typeof (ImageSource),
                typeof (ImageButtonEx),
                null);

        public static readonly DependencyProperty BackgroundPressProperty =
            DependencyProperty.Register(
                "BackgroundPress",
                typeof (SolidColorBrush),
                typeof (ImageButtonEx),
                null);

        public SolidColorBrush BackgroundPress
        {
            get { return (SolidColorBrush) GetValue(BackgroundPressProperty); }
            set { SetValue(BackgroundPressProperty, value); }
        }

        public ImageSource ImageSource
        {
            get { return (ImageSource) GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public ImageSource ImagePressSource
        {
            get { return (ImageSource) GetValue(ImagePressSourceProperty); }
            set { SetValue(ImagePressSourceProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}