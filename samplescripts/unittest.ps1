
Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"
 
$dataps_dll = "D:\saveenr\code\DataPS\bin\Debug\DataPS.dll"

$path = split-path $myinvocation.mycommand.path 
$dataps_dll = Join-Path $path "..\bin\debug\DataPS.DLL"
$dataps_dll = Resolve-Path $dataps_dll

if (!(Test-Path $dataps_dll))
{
    Write-Host "Cannot find $dataps_dll"
    break
}

Write-Host "Importing $dataps_dll"
import-module $dataps_dll
Add-Type -Path $dataps_dll

$dest_path = Join-Path ([Environment]::GetFolderPath("MyDocuments")) "DataPSUnitTestResults"
if (!(test-path $dest_path))
{
    New-Item -Path $dest_path -ItemType Directory
}
else
{
    foreach ($child in (Get-ChildItem $dest_path))
    {
        Remove-Item $child.FullName -Recurse -Force
    }
}


# Create a New DatatTable
$dt1 = New-Object System.Data.Datatable 
$dt1.Columns.Add("Category",[string]) | Out-null
$dt1.Columns.Add("Value",[double]) | Out-null
$dt1.Columns.Add("Series",[string]) | Out-null
$dt1.Rows.Add("Chrome",16535,"Chrome") | Out-null
$dt1.Rows.Add("Gecko(Firefox)",14025,"Firefox") | Out-null
$dt1.Rows.Add("IE9",3680,"InternetExplorer") | Out-null
$dt1.Rows.Add("IE8",2032,"InternetExplorer") | Out-null
$dt1.Rows.Add("Safari",1894,"Safari") | Out-null
$dt1.Rows.Add("Unknown",1145,"Unknown") | Out-null
$dt1.Rows.Add("Opera9",1109,"Opera") | Out-null
$dt1.Rows.Add("IE6",1084,"InternetExplorer") | Out-null
$dt1.Rows.Add("IE7",669,"InternetExplorer") | Out-null
$dt1.Rows.Add("IE",624,"InternetExplorer") | Out-null
$dt1.Rows.Add("Opera8",400,"Opera") | Out-null
$dt1.Rows.Add("KHTML",196,"Unknown") | Out-null
$dt1.Rows.Add("Opera7",55,"Opera") | Out-null
$dt1.Rows.Add("Opera1",27,"Opera") | Out-null
$dt1.Rows.Add("NS",20,"Unknown") | Out-null
$dt1.Rows.Add("Gecko",15,"Unknown") | Out-null 
$dt1.Rows.Add("Opera6",7,"Opera") | Out-null
$dt1.Rows.Add("KHTML(Konqueror)",2,"Unknown")  | Out-null
$dt1.Rows.Add("OperaM",1,"Opera") | Out-null
$dt1.Rows.Add("Opera3",1,"Opera") | Out-null


$fn_data1 = (Join-Path $dest_path "table1.csv")
$fn_data1 = (Join-Path $dest_path "table2.csv")

Export-DataTableToDelimitedText -DataTable $dt1 -Filename $fn_data1  
$dt2 = Import-DataTableFromDelimitedText -Filename $fn_data1  -HeadersInFirstRow -Verbose

Export-DataTableToDelimitedText -DataTable $dt1 -Filename (Join-Path $dest_path "table1_roundtrip.csv")
Export-DataTableToDelimitedText -DataTable $dt1 -Filename (Join-Path $dest_path "table1_roundtrip.tsv") -Delimiter `t


$dt3 = Copy-DataTable $dt1
$dt3.Columns.Add( "Link", [DataPS.Link] )

$doc = New-Document

foreach ($row in $dt3.Rows)
{
	$l = $doc.CreateLink($row["Category"],"https://www.google.com/q=" + $row["Category"])
	$row["Link"] = $l
}

$dt3 | export-csv ((Join-Path $dest_path "table1_with_links.csv"))  -notypeinformation


$tn = $doc.Table($dt1)
$tn.TableSettings.Columns[0].Width=600
foreach ($col in $tn.TableSettings.Columns)
{
	Write-Host $col.Width
}
$doc.Save( (Join-Path $dest_path "doc1.htm") )


