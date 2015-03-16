Set-StrictMode -Version 2
import-module CosmosPS

$data1=New-Object System.Data.Datatable 
$data1.Columns.Add("Category",[string]) 
$data1.Columns.Add("Value",[double]) 
$data1.Rows.Add("A",1) 
$data1.Rows.Add("B",3) 
$data1.Rows.Add("C",2) 

$data2=New-Object System.Data.Datatable 
$data2.Columns.Add("Category",[string]) 
$data2.Columns.Add("Value",[double]) 
$data2.Columns.Add("Series",[string]) 
$data2.Rows.Add("2009",10,"Team1") 
$data2.Rows.Add("2010",22,"Team1") 
$data2.Rows.Add("2011",29,"Team1") 
$data2.Rows.Add("2009",15,"Team2") 
$data2.Rows.Add("2010",18,"Team2") 
$data2.Rows.Add("2011",20,"Team2") 


$data3=New-Object System.Data.Datatable 
$data3.Columns.Add("Category",[string]) 
$data3.Columns.Add("Value",[double]) 
$data3.Columns.Add("Series",[string]) 
$data3.Rows.Add("Chrome",16535,"Chrome")
$data3.Rows.Add("Gecko(Firefox)",14025,"Firefox")
$data3.Rows.Add("IE9",3680,"InternetExplorer")
$data3.Rows.Add("IE8",2032,"InternetExplorer")
$data3.Rows.Add("Safari",1894,"Safari")
$data3.Rows.Add("Unknown",1145,"Unknown")
$data3.Rows.Add("Opera9",1109,"Opera")
$data3.Rows.Add("IE6",1084,"InternetExplorer")
$data3.Rows.Add("IE7",669,"InternetExplorer")
$data3.Rows.Add("IE",624,"InternetExplorer")
$data3.Rows.Add("Opera8",400,"Opera")
$data3.Rows.Add("KHTML",196,"Unknown")
$data3.Rows.Add("Opera7",55,"Opera")
$data3.Rows.Add("Opera1",27,"Opera")
$data3.Rows.Add("NS",20,"Unknown")
$data3.Rows.Add("Gecko",15,"Unknown")
$data3.Rows.Add("Opera6",7,"Opera")
$data3.Rows.Add("KHTML(Konqueror)",2,"Unknown")
$data3.Rows.Add("OperaM",1,"Opera")
$data3.Rows.Add("Opera3",1,"Opera")


$chart1 = New-Chart -DataTable $data1 -Width 600 -Height 800 -ChartType Bar -Name "Data1 Bar" 
$chart2 = New-Chart -DataTable $data1 -Width 600 -Height 800 -ChartType Column -Name "Data1 Column" 
$chart3 = New-Chart -DataTable $data1 -Width 600 -Height 800 -ChartType Pie -Name "Data1 Pie" 
$chart4 = New-Chart -DataTable $data1 -Width 600 -Height 800 -ChartType Doughnut -Name "Data1 Doughnut" 
$chart5 = New-Chart -DataTable $data1 -Width 600 -Height 800 -ChartType Line -Name "Data1 Line" 

$chart1m = New-Chart -DataTable $data2 -Width 600 -Height 800 -ChartType Bar -Name "Data2 Bar" -MultiSeries
$chart2m = New-Chart -DataTable $data2 -Width 600 -Height 400 -ChartType Column -Name "Amount Stored over Three Years" -MultiSeries
#$chart3m = New-Chart -DataTable $data2 -Width 600 -Height 800 -ChartType Pie -Name "Data2 Pie" -MultiSeries
#$chart4m = New-Chart -DataTable $data2 -Width 600 -Height 800 -ChartType Doughnut -Name "Data2 Doughnut" -MultiSeries
$chart5m = New-Chart -DataTable $data2 -Width 600 -Height 800 -ChartType Line -Name "Data2 Line" -MultiSeries

$d = New-Document
$d.Header("Chart Samples")
$d.Paragraph("A sample of the charts that can be created")

$d.Header("Single Series")
$d.Paragraph("All these charts use the DataTable below")
$d.Paragraph("The charts use the default styling")
$d.Table( $data1 )
$d.Chart( $chart1 )
$d.Chart( $chart2 )
$d.Chart( $chart3 )
$d.Chart( $chart4 )
$d.Chart( $chart5 )
$d.Header("Multi Series")
$d.Paragraph("All these charts use the DataTable below")
$d.Paragraph("The charts use the default styling")
$d.Table( $data2 )
$d.Chart( $chart1m )
$d.Chart( $chart2m )
#$d.Chart( $chart3m )
#$d.Chart( $chart4m )
$d.Chart( $chart5m )

$defstyle = New-ChartStylesheet
$defstyle_text = ($defstyle  | Format-List -Property *) | Out-String
$d.Code($defstyle_text)
#$d.Save( "drawcharts.htm" )
$d.Show( )
