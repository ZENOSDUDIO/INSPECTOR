   M S B A M L   `   `   `  ���� @  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  #    Honda.View.ScheduleManagePage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     3BaseP!clr-namespace:Honda.View.BaseView     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity    N� Title$  ScheduleManagePage�� � DataContext �� *ScheduleManage+   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger  .  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadedCommand+ �� Mode$ OneWay=�.��� .+�(� -    (� Stretch$
 Fill=�.��� $
��1080��$
��1920�����	#FFE9EFF4.+���  �� Orientation$ 
Horizontal=�.+�(  "System.Windows.Controls.WebBrowser  -   	  Name$
	 _web��$
��1590��� -   
 �Name$
 gdTask��
������� � RowDefinitions �� $	��125 ��� $��* �.+��� -    ��Name$ lbTask��$��0q�$��Hidden=�$��Hidden=�$��1��  ListBoxItemSchedule#���  ���� *
listTask+� $��0q�.+��� ���	#FFDEE3E6$��0,0,0,1q�._�� $	��124��.+��� $ 
Horizontal=�$��84��$	��Top=�.+��� $��24,0,0,0q�$��Center=�$��Center=�$
��None=�$/��)/Honda;component/Assets/MainPage/bg_4.png���� $
��Wrap=�$��未完成的任务清单��$��Center=�$��Center=�$��10,0,0,0q�$��30(����	#FF231815�� $ 
Horizontal=�$��40��$��0,60,0,0q�.+��� $
��Wrap=�$��已过期：��$��Center=�$
��Left=�$��20,0,0,0q�$��26.667(��� -    ��Name$ tbkHaveExpiredTotal��$
��Wrap=�$��Center=�$
��Left=�$��5,0,0,0q�$��26.667(����� *haveExpiredTotal+�� $
��Wrap=�$��即将过期：��$��Center=�$
��Left=�$��20,0,0,0q�$��26.667(��� -   $ tbkwillExpireTotal��$
��Wrap=�$��Center=�$
��Left=�$��5,0,0,0q�$��26.667(����� *willExpireTotal+