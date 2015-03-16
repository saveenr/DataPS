Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"

import-module D:\saveenr\code\DataPS\bin\Debug\DataPS.dll
$applog = Get-EventLog -LogName Application
$data = $applog | group Source | Sort-Object Count 
$data = $data | Select-Object -Property Name, Count


$dt = $data | Out-DataTable
$chart1 = New-Chart -DataTable $dt -ChartType Bar -Width 1000 -Height ($dt.Rows.Count * 30)

$doc = New-Document
$doc.Header("Application Event Log by Source")
$doc.Chart( $chart1 )
$doc.Show()

