Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"

import-module D:\saveenr\code\DataPS\bin\Debug\DataPS.dll

$cmds = Get-Command -Module DataPS

$doc = New-Document
$doc.Header("DataPS Cmdlets")

foreach ($cmd in $cmds)
{
    $doc.Header($cmd)
	$help = Get-Help $cmd | Out-String 
    $doc.Code($help)
} 

$doc.Save( "D:\DataPS_Cmdlets.htm")


