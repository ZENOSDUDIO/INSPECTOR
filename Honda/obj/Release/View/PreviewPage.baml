   M S B A M L   `   `   `  ���� @  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  /clr-namespace:Honda.UserCtrlHonda.UserCtrl      Honda.View.PreviewPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     3BaseP!clr-namespace:Honda.View.BaseView     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1UserCtrlclr-namespace:Honda.UserCtrl     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity    N� Title$  Preview�� � DataContext �� *	Preview+   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger  .  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadCommand+ �� Mode$ OneWay=�.��� .+�(�  (� Stretch$
 Fill=�.���   PageBackgroundBrush#��[ $
��1920��$
��1080��.+�� $��0,0,25,0q� � RowDefinitions �� $	��50* ��� $
��349* �.+��� -   	 ��Name$	 bdTop��$��0,0,0,1q�  BlackBackgroundBrush#��[ $��0q�._�� .+�� $��1��.+��� $��0,45,0,0q�
 �� Orientation$
 
Horizontal=�$
��Left=�$	��Top=�.+�"   Honda.UserCtrl.ImageButtonEx  $��ImageButtonEx��$��Stretch=�  ImageButtonExStyle3#��[ $��Bottom=�$��25��$��0q�$��54,0,0,0q�$��46��   ImageSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png��   ImagePressSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png�� � Cursor$
 Hand������ *GoBackCommand+� $��45��$��15,0,0,0q�.+��� $
��Wrap=�$��生成评价结果��$
��Left=�$��Center=�  TextForegroundBlueBrush#��[  " TextBlockEvaluationOfTourStyle#��[ $��0,0,0,-8q��� -    ��Name$ tbkStoreName��$
��Wrap=�$��32(�$��Bottom=�$��45,0,0,0q����	#FF060606�� $��StoreTourPage.xaml?�.�.��� ���� *	StoreName+�� $��Right=�$
 
Horizontal=�$	��Top=�$��0,45,0,0q�.+�  -     Name$ btnSave��"   Honda.UserCtrl.ImageButtonEx   Click$ btnSave_Click��$��保存本地��$��Stretch=�$��Bottom=�$��58��$��0,0,80,0q�  WhiteBackgroundBrush#��[  # ButtonBackgroundNormalBlueBrush#��[ $��0q�$��32(�$	��200�� " ButtonBackgroundPressBlueBrush   BackgroundPress# [   -   $ 	btnExport��$ btnExport_Click��$��导出评价记录��$��Stretch=�$��Bottom=�$��58��$��0,0,80,0q�#��[ #��[ $��0q�$��32(�$	��200��# [   -   $ 	btnSubmit��$ btnSubmit_Click��$��提交��$��Stretch=�$��Bottom=�$��58��$��0,0,40,0q�$	��200��#��[ #��[ $��32(�$��0q�# [ � -    �Name$ gdBottom��$��1�� � ColumnDefinitions �� $	��77* �.+�� -   $ gdleft��$��Stretch=� �� $��89 ��� $��* ��� $��90 �.+�� -   $ 
gdTopTitle��$��Stretch=� 	 FormTopAndBottomBorderBrush#��� 	  �� $	��13* ��� $��4* ��� $	��21* ��� $��4* ��� $	��35* �.+�� $��0q�.+��� 
������� 
 BorderStyle1#��� 
 $��0,0,0,1q�._��� $��0q�$��模块��  FormHeaderTextStyle#��[ $��Center=�$��Center=�� $��0q�$��1��.+��� 
�������#��� 
 $��1,0,0,1q�._��� $��0q�$��序号��#��[ $��Center=�$��Center=�� $��0q�$��2��.+��� 
�������#��� 
 $��1,0,0,1q�._��� $��0q�$��评价项目��#��[ $��Center=�$��Center=�� $��0q�$��3��.+��� 
�������#��� 
 $��1,0,0,1q�$��	1.3,0,0,0q�._��� $��0q�$��序号��#��[ $��Center=�$��Center=�� $��0q�$��4��.+��� 
�������#��� 
 $��1,0,0,1q�._��  �� $	��23* ��� $��6* ��� $��6* �.+�� $��0q�.+��� 
�������#��� 
 $��0q�$��
0.05,0,0,0q�._��� $��0q�$��检查项目��#��[ $��Center=�$��Center=�� $��0q�$��1��.+��� 
�������#��� 
 $��1,0,0,0q�._��� $��0q�$��特约店自评��#��[ $��Center=�$��Center=�� $��0q�$��2��.+��� 
�������#��� 
 $��1,0,0,0q�._��� $��0q�$��巡回评价��#��[ $��Center=�$��Center=��� $��1��$��Stretch=�.���   FormBackgroundBrush#���  .+���   PreviewFormBorder#���  $��1,1,1,0q�._��� -	   $	��sp1��$��Stretch=�� -
   $ 	gdBottom1��$��Stretch=�$��1,0,0,0q�$��2��#��� 	  �� $	��15* ��� $��8* ��� $��8* ��� $��8* �.+�� $��0q�#��� 	 .+��� 
�������#��� 
 $��0,0,0,1q�._��� -   $ tbkAll��$��0q�$��合计(项)��  FormBottomTextStyle1#��[ $��Center=�$��Center=�� $��0q�$��1��.+��� 
�������#��� 
 $��1,0,0,1q�._��� -   $ tbkPass��$��
9,24,10,23q�$��通过：30��  FormBottomScoreStyle#��[ $��Center=�$��Center=�� $��0q�$��2��.+��� 
�������#��� 
 $��1,0,0,1q�._��� -   $ 	tbkUnpass��$��
9,24,10,24q�$��不通过：30��#��[ $��Center=�$��Center=�� $��0q�$��3��.+��� 
�������#��� 
 $��1,0,0,1q�._��� -   $ tbkPassRate��$��0q�$��总共：120��#��[ $��Center=�$��Center=�