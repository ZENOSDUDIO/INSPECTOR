   M S B A M L   `   `   `  ����@  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  5      #    Honda.View.ScheduleManagePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     3BaseP!clr-namespace:Honda.View.BaseView     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5        N� Title$  ScheduleManagePage��5
       � DataContext 6    �� *ScheduleManage+   Locator �� Source# [  .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedCommand+ �� Mode$ OneWay=�5      .��5      � .+�5   
   (� -   5         (� Stretch$
 Fill=�5      .��5      � $
��1080��6   $
��1920��6!   ���	#FFE9EFF4.+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      (  "System.Windows.Controls.WebBrowser  -   5        	  Name$
	 _web��5   !   $
��1590��6/   � 5      -   5        
 �Name$
 gdTask��5      
������� � RowDefinitions 5      �� 5      $	��125 �6,   �� 5      $��* �6,   .+�5      �� -   5         ��Name$ lbTask��5   "   $��0q�62   $��Hidden=�5   "   $��Hidden=�5   "   $��1��6T     ListBoxItemSchedule#���  5   "   ��5   =   �� *
listTask+5    "   � 5!      $��0q�6   .+�5"      �� ���	#FFDEE3E6$��0,0,0,1q�6=   ._�5#   "   � $	��124��6'   .+�5$   &   �� $ 
Horizontal=�61   $��84��6J   $	��Top=�6V   .+�5%   *   �� $��24,0,0,0q�60   $��Center=�6B   $��Center=�5&   0   $
��None=�6K   $/��)/Honda;component/Assets/MainPage/bg_4.png��5'   0   �� 5(   *   $
��Wrap=�64   $��未完成的任务清单��6H   $��Center=�6X   $��Center=�5)   4   $��10,0,0,0q�6O   $��30(�6a   ���	#FF2318155*   4   5+   '   �� 5,   &   $ 
Horizontal=�61   $��40��6J   $��0,60,0,0q�6V   .+�5-   *   �� $
��Wrap=�64   $��已过期：��6H   $��Center=�6T   $
��Left=�5.   4   $��20,0,0,0q�6O   $��26.667(�6a   �� 5/   *   -   5         ��Name$ tbkHaveExpiredTotal��5/   4   $
��Wrap=�6Q   $��Center=�50   4   $
��Left=�6O   $��5,0,0,0q�51   4   $��26.667(�6E   ��6W   �� *haveExpiredTotal+�� 52   *   $
��Wrap=�64   $��即将过期：��6H   $��Center=�6U   $
��Left=�53   4   $��20,0,0,0q�6O   $��26.667(�6a   �� 54   *   -   5        $ tbkwillExpireTotal��54   4   $
��Wrap=�6P   $��Center=�55   4   $
��Left=�6O   $��5,0,0,0q�56   4   $��26.667(�6E   ��6W   �� *willExpireTotal+58   '   59   #   5:      5;      5<      5=      5>      5?      5A      5B      