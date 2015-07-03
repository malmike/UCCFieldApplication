<?php

	include'../../ConnectDB/Sanitize.php';
	include'../../ConnectDB/connect.php';
	include'pushNotifications.php';
	
	$employeeID = clean($_POST['UserID']);
	//$employeeID = "EMP001";
    
    $employeeName = clean($_POST['EmployeeName']);

	$supervisorID = clean($_POST['SupervisorID']);
	
	$approval = "Pending";
	
	$checkType = clean($_POST['CheckType']);
	
	if($checkType = 0)
	{
		$check = "CheckOut";
	}else if($checkType = 1)
	{
		$check = "CheckIn";
	}
	
	//$supervisorID = "EMP004";
	$location = clean($_POST['Location']);
    //$location = "MTN";
	$reason = clean($_POST['Reason']);
	
	date_default_timezone_set('Africa/Kampala');
	$checkTime = date('Y-m-d H:i:s a', time());
	
	$pushURI = "";
	
	$ins  = "INSERT INTO `employeelogs`(`EmpFn`, `SupervisorEmpFn`, `CheckTime`, `CheckType`, `Approval`, `Location`, `Reason`) VALUES 
		('$employeeID','$supervisorID','$checkTime','$checkType','$approval','$location','$reason')";
		
	$row = $dbo->prepare($ins);
	$row->execute();

    //Get the CheckID entry in the employeelogs table
    $sel  = "Select `CheckID` From `employeelogs` Where `EmpFn`= '$employeeID' ORDER BY `CheckID` DESC  LIMIT 1";		
	$row2 = $dbo->prepare($sel);
	$row2->execute();
    
    //Create an array of the values we have fetched from the table
    $result2=$row2->fetchAll(PDO::FETCH_ASSOC);
   
    $CheckID = "";
    if(count($result2) == 1) 
	{
		foreach($result2 as $item)
		{
			$CheckID = $item['CheckID'];
		}
	}
	
	$sql = "SELECT `PushURI` FROM `registeredemployees` WHERE `EmpFn` = '$supervisorID'";
	$row=$dbo->prepare($sql);
	$row->execute();

	$result=$row->fetchAll(PDO::FETCH_ASSOC);
	if(count($result) == 1) 
	{
		foreach($result as $item)
		{
			$pushURI = $item['PushURI'];
		}
	}


	if($pushURI != null || $pushURI != "")
	{
		pushNotification($pushURI, $employeeName, $CheckID, $location, $check);
	}
                         
?>