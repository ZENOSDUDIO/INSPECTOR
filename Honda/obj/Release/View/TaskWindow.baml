   M S B A M L   `   `   `  ���� ^  ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration  [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms @ <Honda, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null9!clr-namespace:Honda.View.BaseViewHonda.View.BaseView /clr-namespace:Honda.UserCtrlHonda.UserCtrl    Honda.View.TaskWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      _wfiOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration      Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms      3BaseV!clr-namespace:Honda.View.BaseView      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     1UserCtrlclr-namespace:Honda.UserCtrl     :xctk)http://schemas.xceed.com/wpf/xaml/toolkit       � Title$  
TaskWindow��$	��670��$	��685�� � WindowStyle$
 None=� � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�.���� $��1q����	#FFD3DADD$��0,0,-5,0q�._�� ���	#FFE8E9EA$��0,0,4,-1q�.+��� .+�� .+��� $��0,0,0,1q����	#FFD3DADD._�� $��55�����	#FF5296F2.+��� $
��Left=�$
��Wrap=�$��创建任务��$��Center=�$��29.33(�$��	274,4,0,3q��� -    ��Name$ 	btnCancel��$��取消��$��Right=�$��0,585,172,-584q�$��Center=�$	��128��$��54�����	#FF5296F2���	#FFFDFBFB$��32(���Z� �� -   $ btnSave��$��保存��$��Right=�$��0,585,322,-584q�$��Center=�$	��128��$��54�����	#FF5296F2���	#FFFFFDFD$��32(���Z� � $��80��.+�� $��80�� � ColumnDefinitions �� $
��142* ��� $
��441* �.+��� $
��Left=�$
��Wrap=�$��*��$��Center=�$��23(����	#FFF92802$��36,29.68,0,29.68q�$��40.64��$��14.563���� $
��Left=�$
��Wrap=�$��任务名称：��$��Center=�$��23(����	#FF332826$��
55,10,0,10q�$��2��$	��129���� -    ��Name$ 
tbTaskName��$��1��$
��Left=�$
��0,19q� �� TextWrapping$
 Wrap=�$��Center=�$	��441��$��23(�$��42��� $��80��.+��  �� $
��142* ��� $
��441* �.+��� $
��Left=�$
��Wrap=�$��*��$��Center=�$��23(����	#FFF92802$��	36,3,0,37q�$��40��$��15���� $
��Left=�$
��Wrap=�$��任务名称：��$��Center=�$��23(����	#FF332826$��
55,-6,0,26q�$��2��$	��154���� -    ��Name$ comboTaskType��$��1��$
��Left=�$��0,6,0,38q�$��Center=�$	��442��$��23(�	 �� 
IsReadOnly	 . 
 �� SelectedIndex$
 0��� $��80��.+�� $��
0,-25,0,25q� �� $
��142* ��� $
��441* �.+��� $
��Left=�$
��Wrap=�$��*��$��Center=�$��23(����	#FFF92802$��
36,27,0,13q�$��40��$��15���� $
��Left=�$
��Wrap=�$��任务时间：��$��Center=�$��23(����	#FF332826$��
55,28,0,22q�$��2��$	��124��(� $
��Left=�$��	0,33,0,22q�$	��281��$��1��.���  �� $��2* ��� $��5* ��� $��2* �.+�(  "System.Windows.Controls.DatePicker  -     Name$ startTimePicker��$
��Left=�$	��Top=�$��23(�$��3��$	��213��$��40��$��	0,-1,-1,0q��� $��Center=�$	��至��$��Bottom=�$��23(�$��221,0,-55,4q�$��38��$��2�� J� RenderTransformOrigin$ 
5.44,0.698&�  -   $ EndTimePicker��$��2��$
��Left=�$	��Top=�$��23(�$��264.333,-3,-256,0q�$	��203��� $��80��.+��  �� $
��142* ��� $
��441* �.+��� $
��Left=�$
��Wrap=�$��*��$��Center=�$��23(����	#FFF92802$��
36,18,0,22q�$��40��$��15���� $
��Left=�$
��Wrap=�$��执行对象：��$��Center=�$��23(����	#FF332826$��	54,4,0,16q�$��2��$	��140���� -    ��Name$ tbkStoreName��$
��Left=�$��T��$
��Wrap=�$��Center=�$��1��$��29.333(�$��	0,12,0,28q�� $	��200�� �� $	��242 ��� .+��� $
��Left=�$
��Wrap=�$��任务描述：��$��Center=�$��23(����	#FF332826$��49,63,0,106q��� -   $ 	tbContent��$
 Wrap=�$��174,-2,0,12q�$��25(����	#FFF7FAF7���	#FFECEDED���	#FF777777$
��Left=�$	��440��$��2�� �� SelectionBrush Z� 