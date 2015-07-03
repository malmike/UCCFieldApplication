<?php
	include"../../ConnectDB/connect.php";
	include"../../ConnectDB/Sanitize.php";
	
	$id = clean($_POST['UserID']);
	//$id = 'EMP001';
	//$sql = "SELECT * FROM `employees` WHERE `EmployeeID` = '$id'";
	
	$sql = "SELECT `Serial`,`EmpFn`,`IsSupervisor`,`FirstName`,`LastName` FROM `employees` WHERE `EmployeeStatusID`='A' AND `EmpFn`='$id'";
	
	$row=$dbo->prepare($sql);
	//$row = mysqli_prepare($con, $sql);
	$row->execute();
	//mysqli_stmt_execute($row);

	$result=$row->fetchAll(PDO::FETCH_ASSOC);
	//$result = mysqli_fetch_all($row, MYSQLI_ASSOC);

	$main = array('data'=>$result);
	echo json_encode($main);   

	
?>