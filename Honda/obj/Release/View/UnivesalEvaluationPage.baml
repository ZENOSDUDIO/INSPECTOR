   M S B A M L   `   `   `  ���� @  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  /clr-namespace:Honda.UserCtrlHonda.UserCtrl  '    !Honda.View.UnivesalEvaluationPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     3BaseP!clr-namespace:Honda.View.BaseView     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1UserCtrlclr-namespace:Honda.UserCtrl     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  -   "   Honda.View.BaseView.BasePage   Name$  PageRoot�� N� 	KeepAlive . $��Stretch=�$��Stretch=� N� Title$ UniversalEvaluate�� � DataContext �� *UniversalEvaluate+   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger  .  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadedCommand+ �� Mode$ OneWay=�.���   FormBorderBrush#��[ .+�� -   	 �Name$	 gdUnvisalEvaluation��$��0,0,11,0q�#��[ 
 � RowDefinitions
 �� $	��76* ��� $
��645* �.+��    NavigationBarBackgroundBrush#��[ $��	0,0,-11,0q�.+�'   !Honda.UserCtrl.UCTabNavigationBar  -     Name$ 
UCMyTabBtn��$��69,15q�$��67��� $��11,0,0,0q�$��1��.+�� -   $		 gd1��$��0q�
 �� $
��571* ��� $��90 �.+�� -   $	 gdheader_normal��$	��Top=�$��89�� � ColumnDefinitions �� $	��67* ��� $
��312* ��� $��1352* �.+��� $��0,0,1,1q�$��0q�#��[   FormTopAndBottomBorderBrush#��[ ._��� $
��Wrap=�$��序号��$��Center=�$��Center=�  FormHeaderTextStyle#��[ � $��1��$��0q�.+��� #��[ $��1,0,1,1q�$��0q�$��3��._��� -   $��
tbHeadName��$
��Wrap=�$��评价项目��$��Center=�$��Center=�#��[ � $��2��$��0q� �� $
��647* ��� $
��705* �.+��  �� $	��67* ��� $
��580* �.+��� #��[ $��1,0,1,1q�$��0q�$��3��._��� $
��Wrap=�$��序号��$��Center=�$��Center=�$��0q�#��[ �� #��[ $��1,0,1,1q�$��0q�$��1��._��� -   $��
tbHeadDesc��$
��Wrap=�$��检查项目��$��Center=�$��Center=�$��0q�#��[ � $��1��
 �� $��* ��� $��* � �� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� $��1,0,1,1q�$��0q�#��[ #��[ $��2��._��� $
��Wrap=�$��上次评价结果��$��Center=�$��Center=�#��[ $
��10,0q��� $��1,0,1,1q�$��0q�#��[ #��[ $��2��$��1��._��� $
��Wrap=�$��特约店自评结果��$��Center=�$��Center=�#��[ $
��10,0q��� $��1,0,1,1q�$��0q�#��[ #��[ $��2��$��2��._��� $
��Wrap=�$��巡回评价��$��Center=�$��Center=�#��[ �� $��1,0,1,1q�$��0q�#��[ #��[ $��2��$��1��._��� $
��Wrap=�$��合格��$��Center=�$��Center=�#��[ �� $��1,0,1,1q�$��0q�#��[ #��[ $��3��$��1��._��� $
��Wrap=�$��	不合格��$��Center=�$��Center=�#��[ �� $��1,0,0,1q�$��0q�#��[ #��[ $��4��$��2��._��� $
��Wrap=�$��备注说明��$��Center=�$��Center=�#��[ �� $��0,89,0,0q� �� PanningMode$ VerticalOnly=�  ScrollViewerStyle#���  $
��Auto=�.���� -    ��Name$	 sp1��$��0q�� $��0q�$��1��$��90�� �� $	��15* ��� $��8* ��� $��8* ��� $��8* �.+��� $��0,1,1,0q�#��[ #��[ ._��� -	    ��Name$ tbkAll��$
��Wrap=�$��Center=�$��Center=�  FormBottomTextStyle1#��[ ��  � Language$ zh-cn�$� ���� $�本页合计（项）���� $��1,1,1,0q�#��[ $��1��#��[ ._��� -
   $ tbkPass��$
��Wrap=�$��通过：30��$��Center=�$��Center=�  RedBackgroundBrush#��[   FormBottomScoreStyle#��[ �� $��1,1,1,0q�#��[ $��2��#��[ ._��� -   $ 	tbkUnpass��$
��Wrap=�$��不通过：30��$��Center=�$��Center=�#��[ #��[ �� $��1,1,1,0q�#��[ $��3��#��[ ._��� -   $ tbkPassRate��$
��Wrap=�$��总共：130��$��Center=�$��Center=�#��[ #��[ 