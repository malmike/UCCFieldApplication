<?php error_reporting(E_ALL ^ E_NOTICE);?>
<?php

	session_start();
	
	include'includeParameters.php';


	$a = $_REQUEST['action'];
	$b = $_REQUEST['connect'];
	
	if(!$a && !$b && !$_SESSION['SESS_EMPLOYEE_ID'])
	{
		home($i);
		login();
	}else
	{
		if($_SESSION['SESS_EMPLOYEE_ID'])
		{
			header("location: LoggedIn/empindex.php");
		}

		switch($a)
		{
			case "login":
				header("location: index.php");
				break;

		}
			
		switch($b)
		{	
			case "login":
				autheticate($dbo);
				break;
				
				
		}			
	}
?>
