   M S B A M L   `   `   `  ����@  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView  5           Honda.View.SignatureWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      3BaseV!clr-namespace:Honda.View.BaseView     5        � Title$  SignatureWindow��5      $	��615��6   $	��725��6*    � WindowStartupLocation$ CenterScreen=�66    � WindowStyle$
 None=�6[    � AllowsTransparency . .��5      � .+�5
   
   � $��0q�6   ���	#FFE8E9EA.+�5      � $��0q�6   .+�5      �� $��0q�6   .+�5      � $��55��6   ���	#FF5296F2.+�5      �� $��Center=�6$   $
��Wrap=�6A   $��评价人员签名��6U   $��Center=�5   $   $��29.333(�6?   5      � 5      $	��100��6   $��25,0,0,0q�6(   .+�5      ��  �� Orientation$ 
Horizontal=�6%   .+�5      �� $
��Left=�6(   $
��Wrap=�6C   $!��请在签名区内签名：��6W   $��Center=�5   (   $��0q�6C   $��26.667(�6N      BlackBackgroundBrush#��[  5   (   �� 5      -   5         ��Name$ btnClear��5   %   $��清除内容��67   $��Stretch=�6F   $��20,0,0,0q�6d   $��Center=�5   %   ���	#FFF9F4F4
�������$��32(�5   %   $	��150��63   ���	#FF5296F26V   �� 5      -   5        $ btnSelectPicture��5   %   $��选择图片��6?   $��Stretch=�6N   $��50,0,0,0q�5   %   $��Center=�67   ���	#FFFDF9F9���	#FFFDFDFD$��32(�5   <   $	��150��6J   ���	#FF5296F25   %   5      5      � 5       -   5         �Name$ gdCanvas��5       $	��306��6-   $
��25,0q�6:    K� ZIndex$ 1��6H   .+�5!      � -   5        $ gdImaSelect��5!      $ 2��64   $��0q�6E   $y�	Collapsed=�6P     SignatureBackgroundBrush#��[ 5"      .+�5#      �� -   5         ��Name$ imaDra��5#   $   $��0q�64   $
��Fill=�6?   5$      � 5&      -   5        $ gdHint��5&      $��0q�6/   ���	#FFF5EFEF.+�5'      �� $��Center=�6(   $��0q�6E   $
��Wrap=�6P   $��	签名区��6d   $��Center=�5(   (     TextBlockSignatureStyle#��[ 6C   ���	#FF9C92925)   (   5*      �� 5+      -   5        	 ��Name$	 inkCanv��5+   J   $��0q�6$   $
��Auto��6/   $
��Auto��6<   $ 1��6[   
 J� Opacity$
 1Q�6l    �� 
Background �	#FFFDFBFB5-   $   5.      � 5/      $	��140��6    � ColumnDefinitions 50      �� 51      $
��362* �6/   �� 52      $
��363* �6/   .+�54      �� -   5        $ 	btnCancel��54   !   $��取消��64   $
��Left=�6A   $��30,0,0,0q�6\   $��Center=�55   !   ���	#FFFDFBFB���	#FFFBF9F9$��32(�56   !   $��1��6/   $	��150��6?   ���	#FF5296F257   !   �� 58      -	   5        $ btnSave��58   !   $��保存��62   $��Right=�6?   $��0,0,30,0q�6[   $��Center=�59   !   ���	#FFFDFBFB���	#FFFDFBFB$��32(�5:   !   $	��150��6/   ���	#FF5296F26Q   5;      5<      5=      5>      5?      5@      