   M S B A M L   `   `   `  ���� @  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  /clr-namespace:Honda.UserCtrlHonda.UserCtrl      Honda.View.StoreTourPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     3BaseP!clr-namespace:Honda.View.BaseView     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1UserCtrlclr-namespace:Honda.UserCtrl     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  k gGalaSoft.MvvmLight.Extras.WPF45, Version=4.2.30.23246, Culture=neutral, PublicKeyToken=0e27b6f8be9b2ef7-Custom http://www.galasoft.ch/mvvmlight    N� 	KeepAlive  .  N� Title$ StoreTourPage�� � DataContext ��  �� Path$ 	StoreTour�   Locator �� Source# [  .��� -    �Name$ 
LayoutRoot��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger  .  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadCommand+	 �� Mode$	 OneWay=�.+�(� 
 (� Stretch$

 Fill=�.���   PageBackgroundBrush#��[ $
��1920��$
��1080��.+�� $��0q� � ColumnDefinitions �� $	��330 ��� $��* �.+�� .+��� $��0,0,1,0q�  BlackBackgroundBrush#��[ ._��� -    ��Name$ listBox��  ListBoxItemStyle1#���  $��Hidden=�$��Hidden=�  ItemsPanelTemplate1#���  $ �� IsSynchronizedWithCurrentItem$ FalseY����� *	_lstStore+ �� SelectedIndex �� *SelectedStoreIndex+      	EventName$ SelectionChanged��.    �� *SelectedStoreCommand+$	 OneWay=�   CommandParameter �� *SelectedItem+ �� ElementName$ listBox��� $��0q����	#FFE9EFF4$��1��$��Stretch=�y��� *_bIsShowGroup+.+�� $	��600��$
��1464�� �� $��* ��� $��* ��� $��* � � RowDefinitions �� $��* ��� $��* �.+���   ItemborderBrush#���  $��1q�$��0q�   $ MouseUp��.    �� *NavigateToTourCommand+$	 OneWay=�._�� .+��   ItemBackgroundBrush1#���  y��� *_bIsShowTour_ToSubmit+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$0��*/Honda;component/Assets/Store/ToSubmit.bmp��$	��126��$��38��y��� *_bIsShowTourIma_ToSubmit+�� $��Center=�$
��Wrap=�$��巡回评价表��$��Center=�  ItemTextBlockStyle1#���  �� $
��Left=�$��待提交附件��$��Bottom=�$��	38,0,0,20q����	#FF3496F2$��30.667(�y��� *_bIsShowTourFile_ToSubmit+�   ItemBackgroundBrush2#���  y��� *_bIsShowTour_Submited+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$1��+/Honda;component/Assets/Store/Submitted.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$��巡回评价表��$��Center=� 	 ItemTextBlockStyle2#��� 	 �  
 ItemBackgroundBrush3#��� 
 y��� *_bIsShowTour_NotStart+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$0��*/Honda;component/Assets/Store/NotStart.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$��巡回评价表��$��Center=�  ItemTextBlockStyle3#���  �� #���  $��1q�$��1��$��0q�   $ MouseUp��.    �� *NavigateToImproveCommand+$	 OneWay=�._�� .+�� #���  y��� *_bIsShowImprove_ToSubmit+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$0��*/Honda;component/Assets/Store/ToSubmit.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$��改善计划��$��Center=�#���  � #���  y��� *_bIsShowImprove_Submited+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$1��+/Honda;component/Assets/Store/Submitted.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$��改善计划��$��Center=�#��� 	 � #��� 
 y��� *_bIsShowImprove_NotStart+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$0��*/Honda;component/Assets/Store/NotStart.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$��改善计划��$��Center=�#���  � #���  y��� *_bIsShowImproveCheck_ToSubmit+.+��� $
��Left=�$��	待审核��$��Bottom=�$��	38,0,0,20q����	#FF3496F2$��30.667(��� $��Center=�$
��Wrap=�$��改善计划��$��Center=�#���  � #���  y��� *_bIsShowImproveCheck_Finsih+.+��� $
��Left=�$��	已完成��$��Bottom=�$��	38,0,0,20q����	#FF3496F2$��30.667(��� $��Center=�$
��Wrap=�$��改善计划��$��Center=�#���  �� #���  $��1q�$��2��$��0q�   $ MouseUp��.    �� *#!NavigateToLightspotAndIdeaCommand+$	 OneWay=�._�� .+�� #���  y��� *_bIsShowLightspot_ToSubmit+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$0��*/Honda;component/Assets/Store/ToSubmit.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$!��工作亮点与意见需求��$��Center=�#���  � #���  y��� *_bIsShowLightspot_Submited+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$1��+/Honda;component/Assets/Store/Submitted.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$!��工作亮点与意见需求��$��Center=�#��� 	 � #��� 
 y��� *_bIsShowLightspot_NotStart+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$0��*/Honda;component/Assets/Store/NotStart.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$!��工作亮点与意见需求��$��Center=�#���  �� #���  $��1q�$��0q�$��1��   $ MouseUp��.    �� *NavigateToBusinessCommand+$	 OneWay=�._�� .+�� #���  y��� *_bIsShowBusiness_ToSubmit+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$0��*/Honda;component/Assets/Store/ToSubmit.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$��商务政策��$��Center=�#���  � #���  y��� *_bIsShowBusiness_Submited+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$1��+/Honda;component/Assets/Store/Submitted.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$��商务政策��$��Center=�#��� 	 � #��� 
 y��� *_bIsShowBusiness_NotStart+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$0��*/Honda;component/Assets/Store/NotStart.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$��商务政策��$��Center=�#���  �� #���  $��1q�$��1��$��0q�$��1��   $ MouseUp��.    �� *NavigateToRecordCommand+$	 OneWay=�._�� .+�� #���  .+��� $��Center=�$
��Wrap=�$��评价记录��$��Center=�#���  �� #���  $��1q�$��2��$��0q�$��1��   $ MouseUp��.    �� *NavigateToSummaryCommand+$	 OneWay=�._�� .+�� #��� 
 y��� *_bIsShowReport_ToStart+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$0��*/Honda;component/Assets/Store/NotStart.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$��巡回评价总结报告��$��Center=�#���  � #���  y��� *_bIsShowReport_ToSubmit+.+��� $
��Left=�$��Bottom=�$��	38,0,0,20q�$
��None=�$0��*/Honda;component/Assets/Store/ToSubmit.bmp��$	��126��$��38���� $��Center=�$
��Wrap=�$��巡回评价总结报告��$��Center=�#���  � -   $ gdTask��$��Center=�$	��620��$��Center=�$
��1105��$��0q�$��2��y��� *_bIsShowTaskPanel+.+��� #���  $��1q�$��0q�$
��Left=�$	��488��$	��300��$��Center=�   $ MouseUp��.    �� *ReleaseTaskCommand+$	 OneWay=�._�� .+�� #���  .+��� $��Center=�$
��Wrap=�$��发布评价任务��$��Center=�#���  �� #���  $��1q�$��0q�$��Center=�$	��488��$	��300��$��Right=�   $ MouseUp��.    �� *NavigateToRecordCommand+$	 OneWay=�._�� .+�� #���  .+��� $��Center=�$
��Wrap=�$��评价记录��$��Center=�#���  �� -   $��工作亮点测试��$��1��$
��Left=�$��1270,750,0,0q�$	��Top=�$	��115��$��54.24��$y�	Collapsed=�� $��2��$��2�����	#FFF7F1F1 K� ZIndex$ 1��y��� *_bIsShowLoading+.+�� -    �Name$ 	pbLoading��$��Center=�$��55��$��0q�$��Center=�$	��755�� � IsIndeterminate . �� $
��Left=�$��1385,520.86,0,0q�$
��Wrap=�$��正在加载数据...��$	��Top=�$��32(��� -   $��商务政策��$��1��$
��Left=�$��1425,750,0,0q�$	��Top=�$	��115��$��54.24��$y�	Collapsed=��� -   $��改善计划待审核��$
��Left=�$��1755,829,0,0q�$	��Top=�$	��115��$��54��$y�	Collapsed=��� -   $��改善计划��$
��Left=�$��1584,829,0,0q�$	��Top=�$	��115��$��54��$y�	Collapsed=��� -	   $��	评价表��$
��Left=�$��1352,890,0,0q�$	��Top=�$	��135��$��38��$y�	Collapsed=�