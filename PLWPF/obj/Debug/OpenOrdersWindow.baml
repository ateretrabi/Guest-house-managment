   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5   	       PLWPF.OpenOrdersWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:PLWPF     5   	     � Title$  OpenOrdersWindow��5   	   $	��450��6"   $	��800��6/    � Icon$ images/בית.ico��6;    � 	Resources 5	         orderTemplate �� DataType � RowDefinitions � ColumnDefinitions �� Path��%Z  &	        �� 5
   
   $ local:Order��6-   .R�5      �  5      �� 5      $
��Auto �6$    5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   .+�5      �� $��0��6   $�� Guest Name: ��5      �� 5      $��1��6   ��5      �� $ 	GuestName��� 5      $��2��6   $�� Entry Date: ��5       �� 5!      $��3��6   ��5"      �� $ 	EntryDate��� 5#      $��4��6   $�� Release Date:��5$      �� 5%      $��5��6   ��5&      �� $ ReleaseDate��� 5'      $��6��6   $�� NumberOf Guests:��5(      �� 5)      $��7��6   ��5*      �� $ NumOfGuests�5-      5/      50      .��51      � ���	#FFE9E4C9.+�52   
   �� -   5         ��Name$ ordersCB��52      #��[  6%   $��91,61,85.333,306.667q�6S   $��53��6q   $��Yu Gothic UI Semibold)�6}   $��16(�6�   6�   �� 53   
   -   5         ��Name$ inviteButton��53      $��Send an invitation��6'   $
��Left=�6D   $��
91,294,0,0q�6_   $	��Top=�6s   $	��264��6�   $��64��6�   $��Yu Gothic UI Semibold)�6�   $��20(�6�   ��54      �� 55      	 �� EndPoint$	 0.5,1&�6&   
 �� 
StartPoint$
 0.5,0&�67   .��56      �  � Color$ White��6#    � Offset$ 0Q�61   � 57      $ 	#FFD0AE27��6#   $ 1Q�65   58      5:      �� 5;   
   -   5        $ closeDealButton��5;      $��Close a deal��6*   $
��Left=�6A   $��444,294,0,0q�6\   $	��Top=�6q   $	��264��6�   $��64��6�   $��Yu Gothic UI Semibold)�6�   $��20(�6�   ��5<      �� 5=      $	 0.5,1&�6&   $
 0.5,0&�67   .��5>      � $ White��6#   $ 0Q�61   � 5?      $ 	#FFD0AE27��6#   $ 1Q�65   5@      5B      5D      5E      