<?php
	include"../../ConnectDB/connect.php";
	include"../../ConnectDB/Sanitize.php";
	
	$id = clean($_POST['CheckID']);	

    	
	$sql = "SELECT `Reason` FROM `employeelogs` WHERE `CheckID`='$id'";
    //$sql = "SELECT `Reason` FROM `employeelogs` WHERE `EmpFn`='UCC-PF-114' AND `Location`='UCC'";
	
	$row=$dbo->prepare($sql);
	$row->execute();
	$result=$row->fetchAll(PDO::FETCH_ASSOC);

	$main = array('data'=>$result);
	echo json_encode($main);  

?>

