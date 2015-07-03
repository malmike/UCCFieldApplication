<?php
	include"../../ConnectDB/connect.php";
	include"../../ConnectDB/Sanitize.php";

	$id = clean($_POST['UserID']);
	$sql = "SELECT `Serial`, `EmpFn`, `FirstName`, `LastName` FROM `employees` WHERE `Serial`=
			(SELECT `Acting` FROM `employees` WHERE `Serial`=
			(SELECT `Supervisor` FROM `employees` WHERE `EmpFn`='$id'))";
			
	$row = $dbo->prepare($sql);
	//$row = mysqli_prepare($con, $sql);
	$row->execute();
	//mysqli_stmt_execute($row);

	$result=$row->fetchAll(PDO::FETCH_ASSOC);
	//$result = mysqli_fetch_all($row, MYSQLI_ASSOC);

	$main = array('data'=>$result);
	echo json_encode($main);

?>