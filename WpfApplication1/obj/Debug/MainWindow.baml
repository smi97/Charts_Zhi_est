   M S B A M L   `   `   `  ����J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5   	        WpfApplication1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Y UDynamicDataDisplay, Version=0.3.0.0, Culture=neutral, PublicKeyToken=5b7d744a7263923f=d34http://research.microsoft.com/DynamicDataDisplay/1.0  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   )localclr-namespace:WpfApplication1  5   	     � Title$  
MainWindow��5	   	   $	��600��6   $	��800��6)    � SizeToContent$ WidthAndHeight=�65    � WindowStartupLocation$ CenterScreen=�6T    � WindowState$ 	Maximized=�6y   .��5
      �  � Cursor$ Cross��6   .+�5   
   8  2Microsoft.Research.DynamicDataDisplay.ChartPlotter  -   5        $��plotter��5      $��0,40,0,0q�5      
������� � 	Resources 5      ��%    5         HorizontalAxis 5      I  CMicrosoft.Research.DynamicDataDisplay.Charts.HorizontalDateTimeAxis  5      -   5        $��dateAxis��5   ,   3  -Microsoft.Research.DynamicDataDisplay.Plotter   Children. 5      2  ,Microsoft.Research.DynamicDataDisplay.Header  ��6   �� *
PlotHeader+=  7Microsoft.Research.DynamicDataDisplay.VerticalAxisTitle  5      $��Price��6#   ?  9Microsoft.Research.DynamicDataDisplay.HorizontalAxisTitle  5      $
��Date��6%   5        5   
   -   5        $��plotter1��5      $��20,340,0,30q�5      
������� 5        5       $y�Hidden=�6,      VerticalAxis 5"      ?  9Microsoft.Research.DynamicDataDisplay.Charts.VerticalAxis  5#      $y�Hidden=�6"   5%      �� 5'   
   -   5        $��ToBegin��5'      $
��Left=�5(      $��	10,10,0,0q�5)      $	��Top=�5*      $��75��5+      5,      �� 5.   
   -   5        $��ToEnd��5.      $
��Left=�5/      $��
100,10,0,0q�50      $	��Top=�51      $��75��52      53      �� 55   
   -   5        $��
Screenshot��55      $
��Left=�56      $��
275,10,0,0q�57      $	��Top=�58      $��75��59      5:      �� 5>   
   -   5        $��BarChartBox��5>      $��Barchart��6&   $
��Left=�5?      $��
200,14,0,0q�5@      $	��Top=�5A      $��75��5B      5E   	   5G      5I      