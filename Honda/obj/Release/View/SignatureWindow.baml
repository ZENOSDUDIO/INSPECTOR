   M S B A M L   `   `   `  ���� @  <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView       Honda.View.SignatureWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     3BaseV!clr-namespace:Honda.View.BaseView       � Title$  SignatureWindow��$	��615��$	��725�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency . .��� .+�� $��0q����	#FFE8E9EA.+�� $��0q�.+��� $��0q�.+�� $��55�����	#FF5296F2.+��� $��Center=�$
��Wrap=�$��评价人员签名��$��Center=�$��29.333(�� $	��100��$��25,0,0,0q�.+���  �� Orientation$ 
Horizontal=�.+��� $
��Left=�$
��Wrap=�$!��请在签名区内签名：��$��Center=�$��0q�$��26.667(�   BlackBackgroundBrush#��[  �� -    ��Name$ btnClear��$��清除内容��$��Stretch=�$��20,0,0,0q�$��Center=����	#FFF9F4F4
�������$��32(�$	��150�����	#FF5296F2�� -   $ btnSelectPicture��$��选择图片��$��Stretch=�$��50,0,0,0q�$��Center=����	#FFFDF9F9���	#FFFDFDFD$��32(�$	��150�����	#FF5296F2� -    �Name$ gdCanvas��$	��306��$
��25,0q� K� ZIndex$ 1��.+�� -   $ gdImaSelect��$ 2��$��0q�$y�	Collapsed=�  SignatureBackgroundBrush#��[ .+��� -    ��Name$ imaDra��$��0q�$
��Fill=�� -   $ gdHint��$��0q����	#FFF5EFEF.+��� $��Center=�$��0q�$
��Wrap=�$��	签名区��$��Center=�  TextBlockSignatureStyle#��[ ���	#FF9C9292�� -   	 ��Name$	 inkCanv��$��0q�$
��Auto��$
��Auto��$ 1��
 J� Opacity$
 1Q� �� 
Background �	#FFFDFBFB� $	��140�� � ColumnDefinitions �� $
��362* ��� $
��363* �.+��� -   $ 	btnCancel��$��取消��$
��Left=�$��30,0,0,0q�$��Center=����	#FFFDFBFB���	#FFFBF9F9$��32(�$��1��$	��150�����	#FF5296F2�� -	   $ btnSave��$��保存��$��Right=�$��0,0,30,0q�$��Center=����	#FFFDFBFB���	#FFFDFBFB$��32(�$	��150�����	#FF5296F2