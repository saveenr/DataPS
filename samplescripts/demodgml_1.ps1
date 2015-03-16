Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"

import-module DataPS
$d = New-DGMLDocument

$link_0 = $d.AddLink("VC0","MP1")
$link_1 = $d.AddLink("VC0","MP2")
$link_2 = $d.AddLink("MP1","vol0")
$link_3 = $d.AddLink("MP1","vol1")
$link_4 = $d.AddLink("MP1","vol2")
$link_5 = $d.AddLink("MP2","vol3")
$link_6 = $d.AddLink("MP2","vol4")

$d.Save("\demo_graph_1.dgml")

