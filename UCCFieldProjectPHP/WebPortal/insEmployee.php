<?php
	function insEmployee($item)
	{

		$EmpFn = $item['EmpFn'];
		$Serial = $item['Serial'];
		$FirstName = $item['FirstName'];
		$LastName = $item['LastName'];
		$PushURI = "null";
		
		
		$sql = "INSERT INTO `registeredemployees`(`Serial`, `EmpFn`, `FirstName`, `LastName`, `PushURI`) VALUES 
		('$Serial','$EmpFN','$FirstName','$LastName', '$pushURI')";
		
		$row=$dbo->prepare($sql);
		$row->execute();
	}
?>