   M S B A M L   `   `   `  ���� @  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  /clr-namespace:Honda.UserCtrlHonda.UserCtrl  )    #Honda.View.WorkLightspotAndIdeaPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     3BaseP!clr-namespace:Honda.View.BaseView     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1UserCtrlclr-namespace:Honda.UserCtrl     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity    N� Title$  
Evaluation�� � DataContext �� *WorkLightspotAndIdea+   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger  .  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadedCommand+ �� Mode$ OneWay=�.��� -    �Name$ 
LayoutRoot��.+�(�  (� Stretch$
 Fill=�.���   PageBackgroundBrush#��[ $
��1920��$
��1080��	 � RowDefinitions	 �� $	��119 ��� .+�� .+�� $��0,0,0,1q�.+�� $��1��.+��� $��0,45,0,0q�
 �� Orientation$
 
Horizontal=�$
��Left=�$	��Top=�.+�"   Honda.UserCtrl.ImageButtonEx  -     Name$ btnBack��$��ImageButtonEx��$��Stretch=�  ImageButtonExStyle1#��[ $��Bottom=�$��25��$��0q�$��54,0,0,0q�$��46��   ImageSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png��   ImagePressSource$E ?/Honda;component/Assets/EvaluationOfTour/page_icons_retreat.png�� � Cursor$
 Hand��$y�	Collapsed=�� $��45��$��15,0,0,0q�.+��� $
��Wrap=�$!��工作亮点和意见需求��$
��Left=�$��Bottom=�  TextForegroundBlueBrush#��[ $��0,0,0,-7q�$��40(��� -    ��Name$ tbkStoreName��$
��Wrap=�$��32(�$��Bottom=�$��45,0,0,0q����	#FF060606�� $��StoreTourPage.xaml?�.�.��� ���� *	StoreName+� $��Right=�.+��� $	��Top=�$
 
Horizontal=�$��0,38,0,0q�.+��� -    ��Name$ 	btnCommit��$��提交��$	��200��$��32(�$��65��$��	0,0,100,0q�$��0q�
������� # ButtonBackgroundNormalBlueBrush#���  ���� *CommitCommand+� $��0,0,25,0q�$��1��.+��� $��	0,0,0,100q�.+�� $	��100��.+�� -   $ 	gdBottom1��$��0q�  FormTopAndBottomBorderBrush#���   � ColumnDefinitions �� $��1* ��� $��3* ��� $��3* ��� $��9* ��� $��3* ��� $��3* ��� $��3* ��� $��3* ��� $��2* �.+�� $��
0,0,-1.5,0q�#���  .+��� 
�������  BorderStyle1#���  $��0,0,0,1q�._��� $��0q�$��序号��  FormHeaderTextStyle#��[ $��Center=�$��Center=�� $��0q�$��1��.+��� 
�������#���  $��1,0,0,1q�$��	1.5,0,0,0q�._��� -   $ tbkAllLastScore��$��
9,24,10,23q�$��项目��#��[ $��Center=�$��Center=�� $��0q�$��2��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllSelfScore��$��
9,24,10,24q�$��类别��#��[ $��Center=�$��Center=�� $��0,0,1,0q�$��3��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore��$��0q�$��文字��#��[ $��Center=�$��Center=�� $��
-1,0,1.5,0q�$��4��.+��� 
�������#���  $��1,0,0,1q�._��� -	   $ tbkAllTourScore1��$��0q�$��图片��#��[ $��Center=�$��Center=�� $��
-1.5,0,1,0q�$��5��.+��� 
�������#���  $��1,0,0,1q�._��� -
   $ tbkAllTourScore2��$��0q�$��音频��#��[ $��Center=�$��Center=�� $��-1,0,2,0q�$��6��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore3��$��0q�$��视频��#��[ $��Center=�$��Center=�� $��-2,0,3,0q�$��7��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore4��$��0q�$��文档��#��[ $��Center=�$��Center=�� $��-3,0,0,0q�$��8��.+��� 
�������#���  $��1,0,0,1q�._��� -   $ tbkAllTourScore5��$��0q�$��操作��#��[ $��Center=�$��Center=��� $	��Top=�  ListBoxItemWorkLightStyle#���  $��0q�$	��650��$��Hidden=�$��Hidden=� 	 ItemsPanelTemplate1#��� 	 ���� *listWorkLightspot+��Z� ��Z� � $	��100��.+��� $��+ 增加一条�����	#FFB2AAAA$��1q�$��32(����	#FFB2AAAA��Z� ���� *AddWorkLightCommand+