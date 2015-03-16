Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"

import-module D:\saveenr\code\DataPS\bin\Debug\DataPS.dll

$files = Get-ChildItem -Path D:\saveenr\code\visioautomation\visioautomation_2010 -File -Recurse

$ext_size = @{}
$ext_count = @{}

foreach ($file in $files)
{
	$ext = [System.IO.Path]::GetExtension($file.Name).ToLower();

    if ($ext -eq "") { $ext = "<NONE>" } 

    if ($ext_size.ContainsKey($ext))
    {
        $ext_size[$ext] = $ext_size[$ext]  + $file.Length 
    }
    else
    {
            $ext_size[$ext] = $file.Length 
    }
    if ($ext_count.ContainsKey($ext))
    {
        $ext_count[$ext] = $ext_count[$ext]  + 1 
    }
    else
    {
            $ext_count[$ext] = 1 
    }
}

$dt1 = New-DataTable -ColumnNames Ext,Count
foreach ($ext in $ext_size.Keys)
{
    $dt1.Rows.Add( $ext, $ext_count[$ext] )
}

$dt2 = New-DataTable -ColumnNames Ext,Size
foreach ($ext in $ext_size.Keys)
{
    $dt2.Rows.Add( $ext, $ext_size[$ext] )
}

$chart1 = New-Chart -DataTable $dt1 -ChartType Bar -Width 1000 -Height ($dt1.Rows.Count * 30)
$chart2 = New-Chart -DataTable $dt2 -ChartType Bar -Width 1000 -Height ($dt2.Rows.Count * 30)

$doc = New-Document
$doc.Header("File Extensions: Count ")
$doc.Chart( $chart1 )
$doc.Header("File Extensions: Size ")
$doc.Chart( $chart2 )
$doc.Show()

