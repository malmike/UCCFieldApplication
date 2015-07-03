<?php
	include'../../ConnectDB/connect.php';
	include'../../ConnectDB/Sanitize.php';;
	
	$json = $_POST['UserID'];
	$pushURI = clean($_POST['PushURI']);
	
	//$json = '{"data":[{"EmployeeID":"EMP019","EmployeeName":"J","Department":"TNS","SupervisorID":"EMP012","SupervisorName":"Z"}]}';
	$elements = json_decode($json, true);
	
	foreach($elements['data'] as $item)
	{
		$Serial = $item['Serial'];
		$EmpFN = $item['EmpFn'];
		$FirstName = $item['FirstName'];
		$LastName = $item['LastName'];
		
		$sql = "INSERT INTO `registeredemployees`(`Serial`, `EmpFn`, `FirstName`, `LastName`, `PushURI`) VALUES 
		('$Serial','$EmpFN','$FirstName','$LastName', '$pushURI')";
		
		$row=$dbo->prepare($sql);
		$row->execute();
	}
	   
?>