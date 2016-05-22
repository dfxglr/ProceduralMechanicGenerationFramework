<?php

// Database connection info

$user = '';
$pass = '';

$host = '';

$db = '';

mysql_connect($host, $user, $pass);

mysql_select_db($db);

$whichDamnIDShouldWeChooseThisDependsOnTestSpecifics = 0;
$id = 0;


$query = "SELECT actorgenome,actorlocgenome,eventgenome,methodgenome FROM genomesets WHERE id='$id'";

$result = mysql_query($query);

// Should always be 1....
$rowsReturned = mysql_num_rows($result);

$genomes[0] = mysql_result($result, 0, "actorgenome");
$genomes[1] = mysql_result($result, 0, "actorlocgenome");
$genomes[2] = mysql_result($result, 0, "eventgenome");
$genomes[3] = mysql_result($result, 0, "methodgenome");


mysql_close();
?>
