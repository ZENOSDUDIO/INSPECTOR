   M S B A M L   `   `   `  ���� @  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  /clr-namespace:Honda.UserCtrlHonda.UserCtrl      Honda.View.SummaryPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     3BaseP!clr-namespace:Honda.View.BaseView     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1UserCtrlclr-namespace:Honda.UserCtrl     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity    N� Title$  
Evaluation�� � DataContext �� *	Summary+   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger  .  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadedCommand+ �� Mode$ OneWay=�.��� -    �Name$ 
LayoutRoot��.+�(�  (� Stretch$
 Fill=�.���   PageBackgroundBrush#��[ $
��1920��$
��1080��	 � RowDefinitions	 �� $	��119 ��� .+�� .+�� $��0,0,0,1q�.+�� $��1��.+��� $��0,45,0,0q�
 �� Orientation$
 
Horizontal=�$
��Left=�$	��Top=�.+�"   Honda.UserCtrl.ImageButtonEx  -     Name$ btnBack��$��ImageButtonEx��$��Stretch=�  ImageButtonExStyle1#��[ $��Bottom=�$��25��$��0q�$��54,0,0,0q�$��46��   ImageSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png��   ImagePressSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png�� � Cursor$
 Hand��$y�	Collapsed=�� $��45��$��15,0,0,0q�.+��� $
��Wrap=�$��巡回评价总结报告��$
��Left=�$��Bottom=�  TextForegroundBlueBrush#��[ $��0,0,0,-7q�$��40(��� -    ��Name$ tbkStoreName��$
��Wrap=�$��32(�$��Bottom=�$��45,0,0,0q����	#FF060606�� $��StoreTourPage.xaml?�.�.��� ���� *	StoreName+� $��Right=�.+��� $	��Top=�$
 
Horizontal=�$��0,38,0,0q�.+��� -    ��Name$ 	btnCommit��$��提交��$	��200��$��32(�$��65��$��	0,0,100,0q�$��0q�
������� # ButtonBackgroundNormalBlueBrush#���  ���� *CommitCommand+� $��
15,0,100,0q�$��1��.+��� $��	0,0,0,100q�.+�� $	��100��.+�� -   $ 	gdBottom1��$��0q�  FormTopAndBottomBorderBrush#���  $	��100��$	��Top=� � ColumnDefinitions �� $��3* ��� $��3* ��� $��9* ��� $��3* ��� $��3* �.+�� $��
0,0,-1.5,0q�#���  .+��� 
�������  BorderStyle1#���  $��0,0,0,1q�._��� $��0q�$��序号��  FormHeaderTextStyle#��[ $��Center=�$��Center=�� $��0q�$��1��.+��� 
�������#���  $��1,0,0,1q�$��	1.5,0,0,0q�._��� -   $ tbkAllLastScore��$��
9,24,10,23q�$��	文件名��#��[ $��Center=�$��Center=�� $��0q�$��2��.+��� 
�������#���  $��1,0,0,1q�$��	1.5,0,0,0q�._��� -   $ 
tbfilepath��$��
9,24,10,23q�$��本地路径��#��[ $��Center=�$��Center=�� $��
-1.5,0,1,0q�$��3��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbDoc��$��0q�$��文档��#��[ $��Center=�$��Center=�� $��-3,0,0,0q�$��4��.+��� 
�������#���  $��1,0,0,1q�._��� -	   $ tbkOps��$��0q�$��操作��#��[ $��Center=�$��Center=��� $	��Top=�  ListBoxItemSummaryStyle#���  $��0q�$
��1805��$	��650��$��Hidden=�$��Hidden=� 	 ItemsPanelTemplate1#��� 	 ���� *listSummary+��Z� ��Z� � $	��100��.+��� $��+ 增加一条�����	#FFB2AAAA$��1q�$��32(����	#FFB2AAAA��Z� ���� *
AddSummary+