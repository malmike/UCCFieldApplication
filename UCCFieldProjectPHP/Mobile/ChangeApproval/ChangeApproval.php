<?php
    include"../../ConnectDB/connect.php";
	include"../../ConnectDB/Sanitize.php";

	$Approval = clean($_POST['Approval']);

    if($Approval == "Accept")
    {
       $sql = "UPDATE `employeelogs` SET `Approval`=[value-6] WHERE `CheckID`="; 
    }else if($Approval == "Deny")
    {
        
    }

?>

