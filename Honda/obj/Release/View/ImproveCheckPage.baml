   M S B A M L   `   `   `  ���� @  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  /clr-namespace:Honda.UserCtrlHonda.UserCtrl  !    Honda.View.ImproveCheckPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     3BaseP!clr-namespace:Honda.View.BaseView     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1UserCtrlclr-namespace:Honda.UserCtrl     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity    N� Title$  
Evaluation�� � DataContext �� *ImproveCheck+   Locator �� Source# [  .��� -    �Name$ 
LayoutRoot��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger  .  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadCommand+ �� Mode$ OneWay=�.+�(�  (� Stretch$
 Fill=�.���   PageBackgroundBrush#��[ $
��1920��$
��1080��	 � RowDefinitions	 �� $	��119 ��� .+�� .+�� $��0,0,0,1q�.+�� $��1��.+��� $��0,45,0,0q�
 �� Orientation$
 
Horizontal=�$
��Left=�$	��Top=�.+�"   Honda.UserCtrl.ImageButtonEx  -     Name$ btnBack��$��ImageButtonEx��$��Stretch=�  ImageButtonExStyle1#��[ $��Bottom=�$��25��$��0q�$��54,0,0,0q�$��46��   ImageSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png��   ImagePressSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png�� � Cursor$
 Hand��$y�	Collapsed=�� $��45��$��15,0,0,0q�.+��� $
��Wrap=�$��改善计划审核��$
��Left=�$��Bottom=�  TextForegroundBlueBrush#��[ $��0,0,0,-7q�$��40(��� -    ��Name$ tbkStoreName��$
��Wrap=�$��32(�$��Bottom=�$��45,0,0,0q����	#FF060606�� $��StoreTourPage.xaml?�.�.��� ���� *	StoreName+� $��Right=�.+��� $	��Top=�$
 
Horizontal=�$��0,38,0,0q�� $��15,0,0,0q�$��1��$
��1860��.+��� $��0q�.+�� $	��100��.+�� -   $ 	gdBottom1��$��0q�  FormTopAndBottomBorderBrush#���  $
��1860�� � ColumnDefinitions �� $��3* ��� $��6* ��� $��6* ��� $	��10* ��� $��4* ��� $	��10* ��� $	��10* ��� $	��10* ��� $��6* ��� $��6* ��� $��6* ��� $	��10* �.+�� $��
0,0,-1.5,0q�#���  .+��� 
�������  BorderStyle1#���  $��0,0,0,1q�._��� $��0q�$��序号��  FormHeaderTextStyle#��[ $��Center=�$��Center=�� $��0q�$��1��$��3��.+��� 
�������#���  $��1,0,0,1q�$��	1.5,0,0,0q�._��� -   $ tbkAllLastScore��$��
9,24,10,23q�$��评价不合格项��#��[ $��Center=�$��Center=�� $��0q�$��4��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllSelfScore��$��
9,24,10,24q�$��	优先级��#��[ $��Center=�$��Center=�� $��0,0,1,0q�$��5��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore��$��0q�$��问题描述��#��[ $��Center=�$��Center=�� $��
-1,0,1.5,0q�$��6��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore1��$��0q�$��改善措施��#��[ $��Center=�$��Center=�� $��
-1.5,0,1,0q�$��7��.+��� 
�������#���  $��1,0,0,1q�._��� -	   $ tbkAllTourScore2��$��0q�$��截止时间��#��[ $��Center=�$��Center=�� $��-1,0,2,0q�$��8��.+��� 
�������#���  $��1,0,0,1q�._��� -
   $ tbkAllTourScore3��$��0q�$��	责任人��#��[ $��Center=�$��Center=�� $��-2,0,3,0q�$��9��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore4��$��0q�$��状态��#��[ $��Center=�$��Center=�� $��-2,0,3,0q�$��10��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore10��$��0q�$��证据��#��[ $��Center=�$��Center=�� $��125.276,0,0,0q�$��10��$��2��.+��� 
�������#���  $��1,0,0,1q�#���  ._��� -   $ tbkAllTourScore5��$��0q�$��操作��#��[ $��Center=�$��Center=��� -    ��Name$ scrollViewer1��$��0q�$��Hidden=� �� PanningMode$ VerticalOnly=�$	��860��.���� $	��Top=�  ImproveCheckItemStyle#���  $��0q�$
��1860��$��Hidden=�$��Hidden=�  ItemsPanelTemplate1#���  $	��850������ *listImproveCheck+��Z� ��Z� �� -    ��Name$ 	btnCommit��$��提交��$	��200��$��32(�$��65��$��
0,42,99,12q�$��0q�
������� #	 ButtonBackgroundNormalBlueBrush#��� 	 $��Right=����� *UploadCommand+