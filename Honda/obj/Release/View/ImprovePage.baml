   M S B A M L   `   `   `  ���� @  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  /clr-namespace:Honda.UserCtrlHonda.UserCtrl      Honda.View.ImprovePage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     3BaseP!clr-namespace:Honda.View.BaseView     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1UserCtrlclr-namespace:Honda.UserCtrl     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  k gGalaSoft.MvvmLight.Extras.WPF45, Version=4.2.30.23246, Culture=neutral, PublicKeyToken=0e27b6f8be9b2ef7-Custom http://www.galasoft.ch/mvvmlight    N� Title$  
Evaluation�� � DataContext �� *	Improve+   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger  .  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadedCommand+ �� Mode$ OneWay=�.��� -    �Name$ 
LayoutRoot��.+�(�  (� Stretch$
 Fill=�.���   PageBackgroundBrush#��[ $
��1920��$
��1080��	 � RowDefinitions	 �� $	��119 ��� .+�� .+��� $��0,0,0,0q�  BlackBackgroundBrush#��[ ._�� .+�� $��1��.+��� $��0,45,0,0q�
 �� Orientation$
 
Horizontal=�$
��Left=�$	��Top=�.+�"   Honda.UserCtrl.ImageButtonEx  -     Name$ btnBack��$��ImageButtonEx��$��Stretch=�  ImageButtonExStyle1#��[ $��Bottom=�$��25��$��0q�$��54,0,0,0q�$��46��   ImageSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png��   ImagePressSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png�� � Cursor$
 Hand��$y�	Collapsed=�� $��45��$��15,0,0,0q�.+��� $
��Wrap=�$��改善计划��$
��Left=�$��Bottom=�  TextForegroundBlueBrush#��[ $��0,0,0,-8q�$��40(��� -    ��Name$ tbkStoreName��$
��Wrap=�$��32(�$��Bottom=�$��45,0,0,0q����	#FF060606�� $��StoreTourPage.xaml?�.�.��� ���� *	StoreName+� $��Right=�.+�� $��Right=�.+��� $	��Top=�$
 
Horizontal=�$��0,38,0,0q�.+��� -    ��Name$ 	btnExport��$��导出改善记录��$	��230��$��32(�$��65��$��	0,0,100,0q�$��0q�
������� # ButtonBackgroundNormalBlueBrush#���  ���� *ExportDataCommand+�� -   $ 	btnCommit��$��提交��$	��200��$��32(�$��65��$��	0,0,100,0q�$��0q�
�������#���  ���� *UploadDataCommand+� $��1�� � ColumnDefinitions �� $
��167* �.+�� .+�� -   $	 gd2��$��1��$��0q�$
��Left=�.+�� $	��Top=�$��50�����	#FF707473$
��1858��      	EventName$ MouseUp��.    �� *IsShowList1Command+$ OneWay=�.+��� $
��Left=�$
��Wrap=�$-��'有效的项目（生成改善计划）��$��Center=�$��29.333(����	#FFFBF9F9�� $��Right=�$
��Wrap=�$��Center=�$��29.333(����	#FFFBF9F9$��0,6.373,20,6.373q����� *Topbartitle1+�� $��0,50,0,0q� �� PanningMode$ VerticalOnly=�.���� $��0q�$��Disabled=�$
��1858��.+�� $	��100��.+�� -   $ 	gdBottom2��$��0q�  FormTopAndBottomBorderBrush#���   �� $��3* ��� $��4* ��� $��4* ��� $	��16* ��� $��4* ��� $	��16* ��� $	��16* ��� $��5* ��� $��4* �.+�� $��0q�#���  .+��� 
�������  BorderStyle1#���  $��0,0,0,1q�._��� $��0q�$��序号��  FormHeaderTextStyle#��[ $��Center=�$��Center=�� $��0q�$��1��$��3��.+��� 
�������#���  $��1,0,0,1q�$��	1.5,0,0,0q�._��� -   $ tbkAllLastScore1��$��0q�$��评价不合格项��#��[ $��Center=�$��Center=�� $��0q�$��4��.+��� 
�������#���  $��1,0,0,1q�._��� -	   $ tbkAllSelfScore1��$��
9,24,10,24q�$��	优先级��#��[ $��Center=�$��Center=�� $��0q�$��5��.+��� 
�������#���  $��1,0,0,1q�._��� -
   $ tbkAllTourScore6��$��0q�$��问题描述��#��[ $��Center=�$��Center=�� $��0q�$��6��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore7��$��0q�$��完成时间��#��[ $��Center=�$��Center=�� $��0q�$��7��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore00��$��0q�$��	责任人��#��[ $��Center=�$��Center=�� $��0q�$��8��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore11��$��0q�$��操作��#��[ $��Center=�$��Center=��� $	��Top=� 	 ImplroveItemStyle#��� 	 $��0q�$��Hidden=�$��Disabled=���.  ���� *
ItemList+��Z� ��Z� y��� *bIsShowImproveList+� $	��Top=�$��50�����	#FF707473   $ MouseUp��.    �� *IsShowList2Command+$ OneWay=�.+��� $
��Left=�$
��Wrap=�$6��0已忽略的项目（不会生成改善计划）��$��Center=�$��29.333(����	#FFFBF9F9�� $��Right=�$
��Wrap=�$��Center=�$��29.333(����	#FFFBF9F9$��0,0,20,0q����� *Topbartitle2+� $	��100��.+�� -   $ 	gdBottom4��$��0q�#���   �� $��3* ��� $��4* ��� $��4* ��� $	��16* ��� $��4* ��� $	��16* ��� $	��16* ��� $��5* ��� $��4* �.+�� $��0q�#���  .+��� 
�������#���  $��0,0,0,0q�._��� $��0q�$��序号��#��[ $��Center=�$��Center=�� $��0q�$��1��$��3��.+��� 
�������#���  $��1,0,0,1q�$��	1.5,0,0,0q�._��� -   $ tbkAllLastScore21��$��0q�$��评价不合格项��#��[ $��Center=�$��Center=�� $��0q�$��4��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllSelfScore21��$��
9,24,10,24q�$��	优先级��#��[ $��Center=�$��Center=�� $��0q�$��5��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore26��$��0q�$��问题描述��#��[ $��Center=�$��Center=�� $��0q�$��6��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore27��$��0q�$��完成时间��#��[ $��Center=�$��Center=�� $��0q�$��7��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore20��$��0q�$��	责任人��#��[ $��Center=�$��Center=�� $��0q�$��8��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore21��$��0q�$��操作��#��[ $��Center=�$��Center=��� $	��Top=�#��� 	 $��0q�$��Hidden=�$��Hidden=� 
 ItemsPanelTemplate1#��� 
 ���� *ItemListImprove+��Z� ��Z� y��� *bIsShowImproveIgnoreList+