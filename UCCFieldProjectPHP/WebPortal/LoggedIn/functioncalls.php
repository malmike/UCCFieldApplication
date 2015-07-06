<?php error_reporting(E_ALL ^ E_NOTICE);?>
<?php
	session_start();
	
	include'includeParameters.php';
	
	$a = $_REQUEST['action'];
	$c = $_REQUEST['connect'];
	
	if(!$a & !$c)
	{
		emphome($dbo);
		employeePage();
		
	}else{
		emphome($dbo);
		switch($a)
		{		
			
			case "logout":
				logout($dbo);
				break;
			case "employee":
				employeePage();
				break;
			case"supervisor":
                supervisorPage();
                break;
		}
		
		switch($c)
		{
			case "checkIn":
				sendNotification($dbo);
				break;
		}
		
	}
//employeePage();
?>