   M S B A M L   `   `   `  ���� @  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  /clr-namespace:Honda.UserCtrlHonda.UserCtrl  #    Honda.View.BusinessPolicyPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     3BaseP!clr-namespace:Honda.View.BaseView     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1UserCtrlclr-namespace:Honda.UserCtrl     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  k gGalaSoft.MvvmLight.Extras.WPF45, Version=4.2.30.23246, Culture=neutral, PublicKeyToken=0e27b6f8be9b2ef7-Custom http://www.galasoft.ch/mvvmlight    N� Title$  
Evaluation�� � DataContext �� *BusinessPolicy+   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger  .  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadedCommand+ �� Mode$ OneWay=�.��� -    �Name$ 
LayoutRoot��.+�(�  (� Stretch$
 Fill=�.���   PageBackgroundBrush#��[ $
��1920��$
��1080��	 � RowDefinitions	 �� $	��119 ��� .+�� .+��� $��0,0,0,0q�  BlackBackgroundBrush#��[ ._�� .+�� $��1��.+��� $��0,45,0,0q�
 �� Orientation$
 
Horizontal=�$
��Left=�$	��Top=�.+�"   Honda.UserCtrl.ImageButtonEx  -     Name$ btnBack��$��ImageButtonEx��$��Stretch=�  ImageButtonExStyle1#��[ $��Bottom=�$��25��$��0q�$��54,0,0,0q�$��46��   ImageSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png��   ImagePressSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png�� � Cursor$
 Hand��$y�	Collapsed=�� $��45��$��15,0,0,0q�.+��� $
��Wrap=�$��商务政策��$
��Left=�$��Bottom=�  TextForegroundBlueBrush#��[ $��0,0,0,-8q�$��40(��� -    ��Name$ tbkStoreName��$
��Wrap=�$��32(�$��Bottom=�$��45,0,0,0q����	#FF060606�� $��StoreTourPage.xaml?�.�.��� ���� *	StoreName+� $��Right=�.+�� $��Right=�.+��� $	��Top=�$
 
Horizontal=�$��0,38,0,0q�.+��� -    ��Name$ 	btnExport��$��导出商务政策��$	��230��$��32(�$��65��$��	0,0,100,0q�$��0q�
������� # ButtonBackgroundNormalBlueBrush#���  ���� *ExportDataCommand+�� -   $ 	btnCommit��$��提交��$	��200��$��32(�$��65��$��	0,0,100,0q�$��0q�
�������#���  ���� *UploadDataCommand+� $��1�� � ColumnDefinitions �� $
��167* �.+�� 	 �� $
��101* ��� $
��860* �.+�'   !Honda.UserCtrl.UCTabNavigationBar  -     Name$ Tabbar��$��	30,0,30,8q�� -   $	 gd1��$��1��.+��� $��0,0,25,0q�.+�� $	��100��.+�� -   $ 	gdBottom1��$��0q�  FormTopAndBottomBorderBrush#���   �� $��4* ��� $��4* ��� $��3* ��� $��3* ��� $	��10* ��� $��4* ��� $��2* ��� $��2* �.+�� $��
0,0,-1.5,0q�#���  .+��� 
�������  BorderStyle1#���  $��0,0,0,1q�._��� $��0q�$��零件名称��  FormHeaderTextStyle#��[ $��Center=�$��Center=�� $��0q�$��1��.+��� 
�������#���  $��1,0,0,1q�$��	1.5,0,0,0q�._��� -	   $ tbkAllLastScore��$��
9,24,10,23q�$��对应本零件号��#��[ $��Center=�$��Center=�� $��0q�$��2��.+��� 
�������#���  $��1,0,0,1q�._��� -
   $ tbkAllSelfScore��$��
9,24,10,24q�$��价格��#��[ $��Center=�$��Center=�� $��0,0,1,0q�$��3��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore��$��0q�$��数量��#��[ $��Center=�$��Center=�� $��
-1,0,1.5,0q�$��4��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore1��$��0q�$��使用日期��#��[ $��Center=�$��Center=�� $��
-1.5,0,1,0q�$��5��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore2��$��0q�$��	供应商��#��[ $��Center=�$��Center=�� $��-1,0,2,0q�$��6��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore3��$��0q�$��备注��#��[ $��Center=�$��Center=�� $��-3,0,0,0q�$��8��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore5��$��0q�$��操作��#��[ $��Center=�$��Center=��� $	��Top=� 	 ListBoxItemBusinessesStyle1#��� 	 $��0q�$	��650��$��Hidden=�$��Hidden=� 
 ItemsPanelTemplate1#��� 
 ���� *ListNotPureComponent+��Z� ��Z� � $	��100��.+��� $��+ 增加一条�����	#FFB2AAAA$��1q�$��32(����	#FFB2AAAA��Z� ���� *AddNotPureComponentCommand+� -   $	 gd2��$��1��.+��� $��0,0,25,0q�.+�� $	��100��.+�� -   $ 	gdBottom2��$��0q�#���   �� $��8* ��� $��6* ��� $	��16* ��� $	��10* ��� $��3* ��� $��3* �.+�� $��0q�#���  .+��� 
�������#���  $��0,0,0,1q�._��� $��0q�$��	外销商��#��[ $��Center=�$��Center=�� $��0q�$��1��.+��� 
�������#���  $��1,0,0,1q�$��	1.5,0,0,0q�._��� -   $ tbkAllLastScore1��$��0,35.395,85.163,34.395q�$��金额��#��[ $��Right=�$��Center=�� $��0q�$��2��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllSelfScore1��$��
9,24,10,24q�$��日期��#��[ $��Center=�$��Center=�� $��0q�$��3��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore6��$��0q�$��主要零件��#��[ $��Center=�$��Center=�� $��0q�$��4��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore7��$��0q�$��备注��#��[ $��Center=�$��Center=�� $��0q�$��8��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore11��$��0q�$��操作��#��[ $��Center=�$��Center=��� $	��Top=�  ListBoxItemBusinessStyle2#���  $��0q�$	��200��$��Hidden=�$��Hidden=�#��� 
 ���� *ListComponentESDepartment+��Z� ��Z� � $	��100��.+��� $��+ 增加一条�����	#FFB2AAAA$��1q�$��32(����	#FFB2AAAA��Z� ���� *!AddComponentESDepartmentCommand+� $	��100��$��0,50,0,0q�.+�� -   $ 	gdBottom4��$��0q�#���   �� $��3* ��� $��3* ��� $��2* ��� $��6* ��� $��3* ��� $��* ��� $��* �.+�� $��0q�#���  .+��� 
�������#���  $��0,0,0,1q�._��� $��0q�$��	零件号��#��[ $��Center=�$��Center=�� $��0q�$��1��.+��� 
�������#���  $��1,0,0,1q�$��	1.5,0,0,0q�._��� -   $ tbkAllLastScore3��$��0,35.395,85.163,34.395q�$��零件名称��#��[ $��Right=�$��Center=�� $��0q�$��2��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllSelfScore3��$��
9,24,10,24q�$��数量��#��[ $��Center=�$��Center=�� $��0q�$��3��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore4��$��0q�$��使用日期��#��[ $��Center=�$��Center=�� $��0q�$��4��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore10��$��0q�$��外销对象��#��[ $��Center=�$��Center=�� $��0q�$��5��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore8��$��0q�$��备注��#��[ $��Center=�$��Center=�� $��0q�$��8��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore9��$��0q�$��操作��#��[ $��Center=�$��Center=��� $	��Top=�  ListBoxItemBusinessStyle2_1#���  $��0q�$	��200��$��Hidden=�$��Hidden=�#��� 
 ���� *ListComponentESDepartment2+��Z� ��Z� � $	��100��.+��� $��+ 增加一条�����	#FFB2AAAA$��1q�$��32(����	#FFB2AAAA��Z� ���� *" AddComponentESDepartmentCommand2+� -   $	 gd3��$��1��.+��� $��0,0,25,0q�.+�� $	��100��.+�� -   $ 	gdBottom3��$��0q�#���   �� $��3* ��� $��3* ��� $��2* ��� $��2* ��� $��2* ��� $��2* ��� $��8* ��� $��2* ��� $��2* �.+�� $��
0,0,-1.5,0q�#���  .+��� 
�������#���  $��0,0,0,1q�._��� $��0q�$��	零件号��#��[ $��Center=�$��Center=�� $��0q�$��1��.+��� 
�������#���  $��1,0,0,1q�$��	1.5,0,0,0q�._��� -    $ tbkAllLastScore2��$��
9,24,10,23q�$��零件名称��#��[ $��Center=�$��Center=�� $��0q�$��2��.+��� 
�������#���  $��1,0,0,1q�._��� -!   $ tbkAllSelfScore2��$��
9,24,10,24q�$��	销售价��#��[ $��Center=�$��Center=�� $��0,0,1,0q�$��3��.+��� 
�������#���  $��1,0,0,1q�._��� -"   $ tbkAllTourScore12��$��0q�$��	标准价��#��[ $��Center=�$��Center=�� $��
-1,0,1.5,0q�$��4��.+��� 
�������#���  $��1,0,0,1q�._��� -#   $ tbkAllTourScore13��$��0q�$��差价��#��[ $��Center=�$��Center=�� $��
-1.5,0,1,0q�$��5��.+��� 
�������#���  $��1,0,0,1q�._��� -$   $ tbkAllTourScore14��$��0q�$��数量��#��[ $��Center=�$��Center=�� $��-1,0,2,0q�$��6��.+��� 
�������#���  $��1,0,0,1q�._��� -%   $ tbkAllTourScore15��$��0q�$��销售时间��#��[ $��Center=�$��Center=�� $��-2,0,3,0q�$��7��.+��� 
�������#���  $��1,0,0,1q�._��� -&   $ tbkAllTourScore16��$��0q�$��备注��#��[ $��Center=�$��Center=�� $��-3,0,0,0q�$��8��.+��� 
�������#���  $��1,0,0,1q�._��� -'   $ tbkAllTourScore17��$��0q�$��操作��#��[ $��Center=�$��Center=��� $	��Top=�  ListBoxItemBusinessStyle3#���  $��0q�$	��650��$��Hidden=�$��Hidden=�#��� 
 ���� *ListComponentPrice+��Z� ��Z� � $	��100��.+��� $��+ 增加一条�����	#FFB2AAAA$��1q�$��32(����	#FFB2AAAA��Z� ���� *AddComponentPriceCommand+