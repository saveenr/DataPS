Set-StrictMode -Version 2
$ErrorActionPreference = "Stop"

Import-Module DataPS
$d = new-DGMLDocument

$d.AddCategory("Contains",$true)

$node_a = $d.AddNode("A")
$node_b = $d.AddNode("B")
$node_b.Attributes.Group = [DataPS.DGML.GroupState]::Expanded
$node_c = $d.AddNode("C")

$link_a_b = $d.AddLink( $node_a, $node_b, $null )
$link_b_c = $d.AddLink( $node_b, $node_c, $null )
$link_b_c.Attributes.Category = "Contains"

$d.Save("\demo_graph_2.dgml")
