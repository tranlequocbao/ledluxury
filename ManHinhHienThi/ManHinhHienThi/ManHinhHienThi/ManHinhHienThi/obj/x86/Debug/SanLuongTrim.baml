   M S B A M L   `   `   `  ����I  EManHinhHienThi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ManHinhHienThiManHinhHienThi  5   	   !    ManHinhHienThi.SanLuongTrim   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962a>Design1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   (localclr-namespace:ManHinhHienThi  5   	     � Title$   Nhập sản lượng Trim��5	   	   $	��720��6%   $
��1280��62    � WindowStyle$
 None=�6?    � 
ResizeMode$ NoResize=�6R    � WindowStartupLocation$ CenterScreen=�6h   .��5
      � 
������� � RowDefinitions 5   
   �� 5      $��45 �6   �� 5      $��* �6   �� 5      $��10 �6    � ColumnDefinitions 5   
   �� 5      $��10 �6   6,   �� 5      $��* �6   6+   �� 5      $��0 �6   6+   .+�5   
   � $��0��6   $��0��6   $��3��6,   
���  ��.+�5      �� 
�������$��30 0 0 0q�6)   $
��Left=�6;   $��Center=�6V   $��70��6q   $��30��6|   $��1q�6�    �� CornerRadius$ 5,5,5,5��6�   
�������._�5      �� -   5        $��lblTramNhap��5      $
��TRIM��6/    �� TextAlignment$ Center=�6;   $��Center=�6R   $
��Bold"�6m   $��18(�6   6�   5      �� 5      $3��-ỨNG DỤNG NHẬP TIẾN ĐỘ SẢN XUẤT��6   
�������$��20(�6Q   $
��Bold"�6_   $ Center=�6q   $��Center=�6�   $��Center=�6�   6�   � 5      $��Right=�6   $��80��6/   .+�5      �� -   5        $��btnMini��5   n   $��40��6   
���  ��$��40��66   $
��Left=�6B   
���  ��.��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ WindowMinimize=�6&   $��40��6<   $��40��6G   5      �� 5      -   5        $��btnShutdown��5   o   $��40��6   
���  ��$��40��66   $��Right=�6B   
���  ��.��5         $ Shutdown=�6&   $��40��66   $��40��6A   5!      5"      5#      ]� 5&   
   $��1��6   $��1��6!   .+�5'      �� 
�������$	��580��6,   $��10q�68   .+�5(      ]� $	��170��6   .+�5)      �� $ 10,10,10,10��6   $��0 0 0 0q�68   
���  ��
���  ��._�5*      �� $	��290��6%   .+�5+      �� $��SẢN XUẤT NGÀY��6(   
�������$	��200��6P   $��20(�6\   $ Center=�6j   $
��Bold"�6�   6�   ]� 5,      
���  ��$��10 30 10 10q�69   .+�5-   "   �� -   5        $��lblThucTeNgay��5-   ,   $	��000��6A   $	��125��6L   
�������$
��Bold"�6k   $��50(�6}   $ Center=�6�   $��Center=�6�   $��Center=�6�   6�   �� 5.   "   $��/��6,   
�������$
��Bold"�6H   $��50(�6Z   $ Center=�6h   $��Center=�6   $��Center=�6�   6�   �� 5/   "   -   5        $��lblKeHoachNgay��5/   ,   $	��000��6B   
�������$
��Bold"�6`   $��50(�6r   $ Center=�6�   $��Center=�6�   $��Center=�6�   6�   50      51      52      �� 53      
���  ��$��10 0 0 0q�6.   
���  ��$ 10,10,10,10��6R   ._�54      �� $	��290��6%   .+�55      �� $��SẢN XUẤT THÁNG��6(   
�������$	��200��6S   $��20(�6_   $ Center=�6m   $
��Bold"�6�   6�   ]� 56      
���  ��$��
0 30 10 10q�69   .+�57   "   �� -   5        $��lblThucTeThang��57   ,   $
��0000��6B   $	��130��6N   
�������$
��Bold"�6m   $��50(�6   $ Center=�6�   $��Center=�6�   $��Center=�6�   6�   �� 58   "   $��/��6,   
�������$
��Bold"�6H   $��50(�6Z   $ Center=�6h   $��Center=�6   $��Center=�6�   6�   �� 59   "   -   5        $��lblKeHoachThang��59   ,   $
��0000��6C   
�������$
��Bold"�6b   $��50(�6t   $ Center=�6�   $��Center=�6�   $��Center=�6�   6�   5:      5;      5<      5=      � 5>      $��20��6   6%   �� 5?      $��Nhập số khung:��6   $��40(�62   6B   � 5@      $��10��6   6%   �� 5A      $ 10,10,10,10��6   $��70��64   $��1q�6@   
���   �._�5B      �� -   5        $��
txtSoKhung��5B      
���   �$
��Left=�6\   $��Center=�6w   $	��580��6�   $��43(�6�   	 �� CharacterCasing$	 Upper=�6�   $
��Bold"�6�   
������ $��0q�6�   6  5C      � 5D      $��10��6   6%   ]� 5E      .+�5F      �� $�� Số khung:   ��6    $��30(�65   $ Center=�6C   $	��157��6Z   6i   �� 5G      -	   5        $��
lblSoKhung��5G       $�� ��62   $
��Bold"�6:   $��30(�6L   $
��Left=�6[   $ Center=�6v   6�   5H      ]� 5I      .+�5J      �� $�� Loại xe:      ��6    $��30(�67   $ Center=�6E   6_   �� 5K      -
   5        $��	lblLoaiXe��5K       $�� ��61   $
��Bold"�69   $��30(�6K   $
��Left=�6Y   $ Center=�6t   6�   5L      ]� 5M      .+�5N      �� $�� Số Lot:       ��6    $��30(�67   $ Center=�6E   6^   �� 5O      -   5        $��lblSoLot��5O       $�� ��60   $
��Bold"�68   $��30(�6J   $
��Left=�6X   $ Center=�6s   6�   5P      ]� 5Q      .+�5R      �� $�� Màu sơn:   ��6    $��30(�64   $ Center=�6B   6[   �� 5S      -   5        $��	lblMauSon��5S       $�� ��61   $
��Bold"�69   $��30(�6K   $
��Left=�6Y   $ Center=�6t   6�   5T      � 5U      $��30��6   6%   �� 5V      -   5        $��lblOK��5V      
��� � �$��90(�6<   $ Center=�6J   $
��Bold"�6a   6u   5W      �� 5Y      .+�5Z      � $��40��6   $��Right=�6#   $	��200��6?   $��	 0 0 10 0q�6K   .+�5[      �� 
�������._�5\      �� -   5        $��txtNgayThang��5\   $   $��
00/00/0000��68   $��20(�6J   
����  �$ Center=�6j   $��Right=�6�   $��5 10 0 0q�6�   $��Center=�6�   6�   5]      5^      � 5_      .+�5`      �� .��5a      �� -   5        
 ��Name$

 lvXe��5a   #   $��20(�61    � 	Resources 5b       �� 
TargetType �� Property �� Value��%n  '��       '�� �     �� 5c   "   " ��6(   .�5d   &   ��  � ��$ Center=�6S   �� 5e   &    � ��
 �  ��6C   �� 5f   &    � ��$ 30��6?   �� 5g   &    � ��$
 Bold"�6C   �� 5h   &    � ��
 �����6C   5i   #   �� 5j   "   " ��6(   .�5k   &   ��  � ��$ 1,0,1,1q�6H   6Z   �� 5l   &    � ��
 �����6X   5m   #   5n       �� View 5o      �� 5p   "   .P�5q   &   �� $��TT��66    �� Width$ 30��6B    �� DisplayMemberBinding 6M   �� *thutu+6v   �� 5r   &   $��
Số khung��66   $	 210��6H    6U   �� *	sokhung+6�   �� 5s   &   $��	Loại xe��66   $ 70��6G    6R   �� *loaixe+6|   �� 5t   &   $��	Màu sơn��66   $	 150��6G    6S   �� *mauson+6}   �� 5u   &   $��Thời gian nhập��66    6N   �� *thoigiannhap+6~   5v   #   5x      5y      5z      5{      5|      5}      5~      