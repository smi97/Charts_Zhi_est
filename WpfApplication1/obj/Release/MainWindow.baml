   M S B A M L   `   `   `  ���� J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1       WpfApplication1.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Y UDynamicDataDisplay, Version=0.3.0.0, Culture=neutral, PublicKeyToken=5b7d744a7263923f=d34http://research.microsoft.com/DynamicDataDisplay/1.0  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  )localclr-namespace:WpfApplication1    � Title$  Charts��$	��600��$	��800�� � SizeToContent$ Manual=� � WindowState$ 	Maximized=�.���  � Cursor$ Cross��.+�8  2Microsoft.Research.DynamicDataDisplay.ChartPlotter  -   $��plotter��$��0,40,0,0q�
������� � 	Resources ��%       HorizontalAxis I  CMicrosoft.Research.DynamicDataDisplay.Charts.HorizontalDateTimeAxis  -   $��dateAxis��3  -Microsoft.Research.DynamicDataDisplay.Plotter   Children. 2  ,Microsoft.Research.DynamicDataDisplay.Header  ���� *
PlotHeader+=  7Microsoft.Research.DynamicDataDisplay.VerticalAxisTitle  $��Price��?  9Microsoft.Research.DynamicDataDisplay.HorizontalAxisTitle  $
��Date��  -   $��plotter1��$��20,340,0,30q�
�������   $y�Hidden=�   VerticalAxis ?  9Microsoft.Research.DynamicDataDisplay.Charts.VerticalAxis  $y�Hidden=��� -   $��ToBegin��$
��Left=�$��	10,10,0,0q�$	��Top=�$��75���� -   $��ToEnd��$
��Left=�$��
100,10,0,0q�$	��Top=�$��75���� -   $��
Screenshot��$
��Left=�$��
275,10,0,0q�$	��Top=�$��75���� -   $��BarChartBox��$��Barchart��$
��Left=�$��
200,14,0,0q�$	��Top=�$��75��