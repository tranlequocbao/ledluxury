   M S B A M L   `   `   `  ����K  GTKAC Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TKAC_ApplicationTKAC_Application  5   	   !    TKAC_Application.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962a>Design1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   *localclr-namespace:TKAC_Application  5   	     � Title$  
MainWindow��5	   	   $	��720��6   $
��1280��6)    � WindowStyle$
 None=�66    � WindowStartupLocation$ CenterScreen=�6I    � 
ResizeMode$ NoResize=�6n   .��5      �� $��1q�6   
�������._�5   
   �  � RowDefinitions 5      �� 5      $��40 �6    �� 5      $��* �6    .+�5      � -   5        
���  ��.+�5      �� $8��2PHẦN MỀM QUẢN TRỊ SẢN XUẤT - THACO KIA��6   $��14(�6J   $��Tahoma)�6X   $
��Bold"�6l   $��Center=�6~   $��Center=�6�   
�������6�   �� 5      -   5         ��Name$ btnClose��5      
������ $��0q�6\   
������ $��40��6�   $��40��6�   $��Right=�6�   $��Center=�6�    � Cursor$
 Hand��6�   ��5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property � ��	 �� Value	 5   "   �� 5   &   
 �� 
TargetType"
 ��66   .R�5   *   �� #��z��61   
���   �$��0q�6o   ._�5   .   �� $��Center=�6?   $��Center=�6\   5   +   5   '   5      5      .��5!      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Shutdown=�6&   
�������$��30��6I   $��30��6U   $��Center=�6`   $��Center=�6}   5"      �� 5#      -   5        $ 
btnMaximun��5#      
������ $��0q�6a   
������ $��40��6�   $��40��6�   $��Right=�6�   $��Center=�6�   $��0,0,40,0q�6�   $
 Hand��6�   ��5$      �� 5%      " ��6    .�5&      ��  � ��	 5'   "   �� 5(   &   "
 ��66   .R�5)   *   �� #��z��61   
���   �$��0q�6o   ._�5*   .   �� $��Center=�6?   $��Center=�6\   5+   +   5,   '   5.      5/      .��51        -   5        $��	piMaximun��51   &   $ WindowMaximize=�67   
�������$��30��6`   $��30��6l   $��Center=�6w   $��Center=�6�   52      �� 53      -   5        $ 
btnMinumum��53      
������ $��0q�6a   
������ $��40��6�   $��40��6�   $��Right=�6�   $��Center=�6�   $��0,0,80,0q�6�   $
 Hand��6�   .��54        $ WindowMinimize=�6&   
�������$��30��6O   $��30��6[   $��Center=�6f   $��Center=�6�   ��55      �� 56      " ��6    .�57      ��  � ��	 58   "   �� 59   &   "
 ��66   .R�5:   *   �� #��z��61   
���   �$��0q�6o   ._�5;   .   �� $��Center=�6?   $��Center=�6\   5<   +   5=   '   5?      5@      5B      5C      ]� 5D      -   5         ]�Name$ dpShow��5D      $��1��6(   ��5E      �� 5F       �� ImageSource$ LOGO.png��6!    j� Stretch$ Uniform=�68    �� Opacity$	 0.3Q�6J   5H      5I      5J      5K      