   M S B A M L   `   `   `  ����@  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  5      /clr-namespace:Honda.UserCtrlHonda.UserCtrl  5          Honda.View.RecordPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      3BaseP!clr-namespace:Honda.View.BaseView     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1UserCtrlclr-namespace:Honda.UserCtrl     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5        N� Title$  
RecordPage��5      $��0,0,0,20q�6   .��5      (� -   5         (� Stretch$
 Fill=�5      .��5   
   �    PageBackgroundBrush#��[  6   $
��1920��6A   $
��1080��6N    � RowDefinitions 5      �� 5      $	��119 �6    �� 5      .+�5      � .+�5      �� $��0,45,0,0q�6    �� Orientation$ 
Horizontal=�6/   $
��Left=�6H   $	��Top=�5      .+�5      � $��45��6   $��15,0,0,0q�6'   .+�5      �� $
��Wrap=�6$   $��评价记录��68   $
��Left=�6D   $��Bottom=�5   $     TextForegroundBlueBrush#��[ 6?   $��0,0,0,-8q�5   $   $��40(�66   5      �� 5      $
��Wrap=�6    $��32(�64   $��Bottom=�6B   $��45,0,0,0q�6]   ���	#FF060606�� 5      $��StoreTourPage.xaml?�6$   .�6E   .�5      �� -   5         ��Name$ tbkStoreName��5   (   5       5!      5#      5$      � 5%      ���	#FFE9EFF4$��1��6*   .+�5&      �� $ 
Horizontal=�6   .+�5'      (  "System.Windows.Controls.WebBrowser  -   5          Name$
 _web��5'   !   $
��1920��6/   5(      5)      5*      5+      5,      