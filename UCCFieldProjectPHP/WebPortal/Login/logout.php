<?php
    
    function deleteSession($dbo, $id)
    {

        $del = "DELETE FROM `sessions` WHERE `EmpFn`='$id'";
        $delete=$dbo->prepare($del);
        $delete->execute();

    }


	function logout($dbo)
	{
		//Start session
		session_start();

        $id = $_SESSION['SESS_EMPLOYEE_ID'];
        deleteSession($dbo, $id); 
        
		//Unset the variables stored in session
		unset($_SESSION['SESS_EMPLOYEE_ID']);
		unset($_SESSION['SESS_EMPLOYEE_NAME']);
		unset($_SESSION['SESS_DEPARTMERNT']);
		unset($_SESSION['SESS_SUPERVISOR_ID']);
		unset($_SESSION['SESS_SUPERVISOR_NAME']);
		
		header("location: ../index.php");


       
	}
?>