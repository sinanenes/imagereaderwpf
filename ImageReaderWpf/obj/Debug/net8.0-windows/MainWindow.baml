   M S B A M L   `   `   `  ����I  EImageReaderWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ImageReaderWpfImageReaderWpf  5   	       ImageReaderWpf.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ImageReaderWpf     5   	     � Title$  
MainWindow��5   	   $
��Auto��6   $	��800��6*   .��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   
���   �$��1q�62   ._�5      �� -   5         ��Name$ 
CameraFeed��5      $��Uniform=�6(   5      �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5      �� -   5        $��Open Camera��5      $��5q�6J   �� 5      -   5        $��Capture Image��5      $��5q�6N   5      �� 5   
   $��2��6   
���   �$��1q�62   $��5q�6F   ._�5       �� -   5        $ CapturedImage��5       $��Uniform=�6+   5!      �� 5$   
   $��3��6   $ 
Horizontal=�6"   $��Center=�6;   .+�5%      �� -   5        $��OcrRead��5%      6'   5&      �� 5(   
   $��4��6   
�������$��Center=�69   $��Center=�6T   .+�5)      �� -   5         ��Name$ OcrText��5)      $��24(�6)   $
��Wrap=�67   5*      �� 5-   
   $��5��6   $ 
Horizontal=�6"   $��Center=�6;   .+�5.      �� -   5        $��Approve��5.      $��5q�6C   �� 5/      -   5        $
��Deny��5/      $��5q�6=   50      51      52      