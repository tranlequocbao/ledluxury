   M S B A M L   `   `   `  ����K  GTKAC Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TKAC_ApplicationTKAC_Application  5          TKAC_Application.frm1   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:TKAC_Application     5      b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962a>Design1http://materialdesigninxaml.net/winfx/xaml/themes  6;   
�������  � 	Resources  5	         	DarkBrush �� 
StartPoint �� EndPoint � Color � Offset  Clicked �� 
TargetType �� Property �� Value �� 
TargetType	 ��Name
 �� Color �� Orientation K� IsItemsHost  MenuItemControlTemplate1 �� CornerRadius J� SnapsToDevicePixels � ColumnDefinitions �� HeaderStringFormat �� ContentStringFormat �Name � AllowsTransparency �� RelativeSource �� Padding �� Opacity ��Name �� 
ResourceId �� TypeInTargetAssembly �� ClearTypeHint ��Name �� ElementName ��Name-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation � IsSharedSizeScope    TabNavigation! �� Triggers" P� Property# P� Value$ �� 
TargetName% P� 
SourceName��%�  &	        &	 �     'o� �    &	 6    7   7  �� 5
   
   $ 0.5,0&�60   $ 0.5,1&�6C   ��5      � � 5      $ #939393��6#   $	 0.0Q�63   � 5      $ #717171��6#   $
 0.05Q�63   � 5      $ #606060��6#   $	 1.0Q�63   5      5      �� 5   
   $ 0.5,0&�6.   $ 0.5,1&�6A   ��5      � � 5      $ #363636��6#   $	 0.0Q�63   � 5      $ #393939��6#   $	 1.0Q�63   5      5      �� 5   
   " o�6&   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5        �@J�SnapsToDevicePixels . 64   �� 5!       � ��
 �����6+   �� 5"       � �� 5#      �� 5$      " o�6&   .R�5%      �� $	 MainMenu��6!   ��5&      �� 5'   "   $
 	#FF535353��62   ._�5)      �� }�. $ 
Horizontal=�6=    . 6V   5*      5+      5-      5.      �� 50   
   " m�6;   .R�51      �� $	 templateRoot��6   
������ $ 3��53      $��1q�54      #��z��55       . ._�57      � $��Center=�6    58      �� 59      $
��Auto �6+   �� 5:      $
��Auto �6+   .+�5>      �� #��z��6'   #��z��6Z   $��1��6}   # z 6�   $��Header��6�   #��z��6�   ��. # z 6  � 5?      $ 
PART_Popup��6    . |�.  #��� -�6�   $��Right=�6  ��6]   �� *IsSubmenuOpen+ �� *TemplatedParent+.��5@      �� $	 SubMenuBorder��6!   
������ $��1q�6R   $ 2q�6f   ��5A      �� 5B   "   $
 	#FF535353��62   $	 0.7Q�6D   ._�5D      �� $ SubMenuScrollViewer��6+   ��6H   C� *�� $ MenuScrollViewer��" �+.��5E   "   � $ Enabled=�6'   .+�5F   &   �� $
��Left=�6-   $��0��6H   $	��Top=�6S   $��0��6k   .+�5G   *   �� $ 
OpaqueRect��64   ��6H   �� *
Background+$ SubMenuBorder����6   �� *ActualHeight+$ SubMenuBorder����6�   �� *ActualWidth+$ SubMenuBorder��5H   '   �� 5I   &   $ ItemsPresenter��65   $ Cycle=�6M    . # z 6�   $  Cycle=�6�   5J   #   5K      5L      5M      5N      5O      ! 5P      P� 5Q      "" �@m�IsSuspendingPopupAnimation# . . �5R      �� $$ 
PART_Popup��67    � ��$
 None=�6O   5S      P� 5U      " �@m�IsHighlighted# . . �5V      �� $$ templateRoot��63   8    � ��6M   �� 5W      $$ templateRoot��64    � �� �#2C2C2C6N   �� 5X      $$ templateRoot��68    � ��$ 1q�6R   6^   5Y      P� 5[      $% SubMenuScrollViewer��66   " � ��# .  . �5\      �� $$ 
OpaqueRect��63    6K   �� *VerticalOffset+$ SubMenuScrollViewer�� �@��Top�� 5]      $$ 
OpaqueRect��64    6L   �� *HorizontalOffset+$ SubMenuScrollViewer�� �@��Left5^      P� 5_      " �@J�IsKeyboardFocusWithin# . . �5`      �� $$ templateRoot��6   8   � ��6M   �� 5a       � ��$
 Test��6/   �� 5b       � �� �#2C2C2C6F   �� 5c       � ��$ 1q�68   6D   5d      5f      5g      .��5h      �  5i   
   �� 5j      $��50 �6   �� 5k      $��* �6   & � RowDefinitions& 5m   
   �� 5n      $��20 �6   6*   �� 5o      $��* �6   6)   .+�5r   
   � $��0��6   $��2��6   
�������6-   o� 5s   
   -   5        $��btnMenu��5s      $	��Top=�6   $��0��63   $��0��6C   $��2��6P   
������ $��Center=�6z   $	��Top=�6�   .@�5t      m� -   5        $��MnMain��5t      
������ $��40��6>   $��40��6J   $��Right=�6V   $��Center=�6r   #���  6�   ' � ToolTip$)' #Theo dõi tiến độ sản xuất��6�   ��5u      '  !MaterialDesignThemes.Wpf.PackIcon  5w      (   Kind$
( Menu=�6&   $��Right=�62   $��Center=�6N   
�������
������ $��70��6�   $��	-40 0 0 0q�6�   $��40��6�   5z      5{      ]� 5|   
   -   5        ) ]�Name$) dpShow��5|      $��1��6$   $��2��64   ��5}   
   �� 5~      * �� ImageSource$* LOGO.png��6   + j� Stretch$+ Uniform=�60   $	 0.3Q�6B   5�      5�      5�      