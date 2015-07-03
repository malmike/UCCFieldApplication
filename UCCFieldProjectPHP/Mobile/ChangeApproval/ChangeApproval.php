<?php
    include"../../ConnectDB/connect.php";
	include"../../ConnectDB/Sanitize.php";

	$Limit = clean($_POST['Limit']);
    $Approval = clean($_POST['Approval']);
    $value = "";

    insertDB($Approval, clean($_POST['CheckID0']), $dbo); 

    for($x=0; $x<$Limit; $x++)
    {
        $Check = 'CheckID'.$x;
         insertDB($Approval, clean($_POST[$Check]), $dbo); 
         echo clean($POST[$Check]);
    }

    function insertDB($Approval, $CheckID,$dbo)
    {
        $sql = "UPDATE `employeelogs` SET `Approval`='$Approval' WHERE `CheckID`='$CheckID'"; 
        $row=$dbo->prepare($sql);
		$row->execute();
    }
    

?>

