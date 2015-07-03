<?php /*error_reporting(E_ALL ^ E_NOTICE);*/?>

<?php

	$username = "root";
	$password = "";
	$hostname = "localhost";
	$db = "uccfield";

	$con = mysqli_connect($hostname, $username, $password, $db);
	
	try {
		$dbo = new PDO('mysql:host='.$hostname.';dbname='.$db, $username, $password);
		
	} catch (PDOException $e) {
		print "Error!: " . $e->getMessage() . "<br/>";
		die();
	}
?>
    