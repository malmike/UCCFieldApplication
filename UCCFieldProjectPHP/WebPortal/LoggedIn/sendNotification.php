<?php
	function sendNotification($dbo)
	{
		if ( isset( $_POST['CheckIn'] ) ) 
		{
			$check = "CheckIn";
			choose($dbo,  $_SESSION['SESS_EMPLOYEE_FNAME']." ".$_SESSION['SESS_EMPLOYEE_LNAME'], 
                $_SESSION['SESS_SUPERVISOR_ID'], $_SESSION['SESS_EMPLOYEE_ID'], 
                clean($_POST['location']), clean($_POST['reason']), $check);
		}
		if ( isset( $_POST['CheckOut'] ) )			
		{
			$check = "CheckOut";
			choose($dbo, $_SESSION['SESS_EMPLOYEE_FNAME']." ".$_SESSION['SESS_EMPLOYEE_LNAME'],
                $_SESSION['SESS_SUPERVISOR_ID'], $_SESSION['SESS_EMPLOYEE_ID'],
                clean($_POST['location']), clean($_POST['reason']), $check);

		}
	}
	
	function choose($dbo, $employeeName, $supervisorID, $employeeID, $location, $reason, $check)
	{
        //Insert values into the employeelog table
        date_default_timezone_set('Africa/Kampala');
		$checkTime = date('Y-m-d H:i:s a', time());
		if($check == "CheckIn")$checkType = 1;
		if($check == "CheckOut")$checkType = 0;
		
		$ins  = "INSERT INTO `employeelogs`(`EmpFn`, `SupervisorEmpFn`, `CheckTime`, `CheckType`, `Approval`, `Location`, `Reason`) VALUES 
		('$employeeID','$supervisorID','$checkTime','$checkType', 'Pending','$location','$reason')";
		
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


		$PushURI;
		$sql = "SELECT `PushURI` FROM `registeredemployees` WHERE `EmpFn`='$supervisorID'";
		$row = $dbo->prepare($sql);
		$row->execute();
		$result=$row->fetchAll(PDO::FETCH_ASSOC);
		
		if(count($result) == 1) 
		{
			foreach($result as $item)
			{
				$PushURI = $item['PushURI'];
			}
		}

        


		//$main = array('data'=>$result);
		//echo json_encode($main)."</br>";
		//echo "http://s.notify.live.net/u/1/db3/H2QAAAAQYTaHopWR02gURmbdSxvdApitIGYovgAk1HpWv7SlZp9Cz-3HiNoEtmuk_stTykeH1IPfGu1E_pIRJppf_XvwmNL2apX0OGPU-m7RixS9ioGARsCiMXxOa8kfOyhe3j0/d2luZG93c3Bob25lZGVmYXVsdA/0pqGa5A7DkiEcxeWg4Asvw/C44fBCUWnQfh-kXwsIpo9TpFS9o";
		//echo "</br>".$PushURI."</br>";
		if($PushURI != null || $PushURI != "")
		{
			include"../../Mobile/PushNotifications/pushNotifications.php";
			pushNotification($PushURI, $employeeName, $CheckID, $location, $check);
			//echo $PushURI;
		}
		
		
	}
?>