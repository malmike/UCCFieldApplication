<?php
	function logout()
	{
		//Start session
		session_start();
		
		//Unset the variables stored in session
		unset($_SESSION['SESS_EMPLOYEE_ID']);
		unset($_SESSION['SESS_EMPLOYEE_NAME']);
		unset($_SESSION['SESS_DEPARTMERNT']);
		unset($_SESSION['SESS_SUPERVISOR_ID']);
		unset($_SESSION['SESS_SUPERVISOR_NAME']);
		
		header("location: ../index.php");
	}
?>