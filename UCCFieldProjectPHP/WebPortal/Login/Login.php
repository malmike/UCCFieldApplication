<?php 
	function login()
	{
		session_start();
		if($_SESSION['ERRMSG_ARR'][0] != "") 
		{
			$id = $_SESSION['ERRMSG_ARR'][0];
		}else
		{
			$id = "Enter_User_ID";
		}
		
?>		

		<form role="form" method="post" action="functioncalls.php?connect=login"> 
			<h3>LOGIN <small><?php if($_SESSION['LOGIN_FAILED'] != '') echo $_SESSION['LOGIN_FAILED'];?></small></h3>
			<div class="form-group"> 
				<label for="name">EMPLOYEE ID</label> 
				<input type="text" class="form-control" name="id" id="id" placeholder = <?php echo $id ?>> 
			</div> <div class="form-group"> 
			
			<button type="submit" class="btn btn-default">SUBMIT</button> 
		</form>
		

<?php
		//clear $_SESSION['ERRMSG_ARR'] 
		$_SESSION['ERRMSG_ARR'] = '';
		$_SESSION['LOGIN_FAILED'] = '';
	}
?>


<?php
	function autheticate($dbo)
	{
		//Start session
		session_start();
		
		//Array to store validation errors
		$errmsg_arr = array();
		
		//Validation error flag
		$errflag = false;
		
		//Sanitize the POST values
		$id = clean($_POST['id']);
		
		//Input Validations
		if($id == '') 
		{
			$errmsg_arr[0] = 'Login_ID_Missing';
			$errflag = true;
		}

		
		//If there are input validations, redirect back to the login form
		if($errflag) 
		{
			$_SESSION['ERRMSG_ARR'] = $errmsg_arr;
			session_write_close();
			header("location: index.php");
			exit();
		}
		
		//Create query
		$sql = "SELECT `Serial`,`EmpFn`,`IsSupervisor`,`FirstName`,`LastName` FROM `employees` WHERE `EmployeeStatusID`='A' AND `EmpFn`='$id'";
		$row = $dbo->prepare($sql);
		$row->execute();
		$result=$row->fetchAll(PDO::FETCH_ASSOC);
		
		//Check whether the query was successful or not
	
		if(count($result) == 1) {
			//Login Successful
			session_regenerate_id();
			foreach($result as $item)
			{
				$_SESSION['SESS_EMPLOYEE_SERIAL'] = $item['Serial'];
				$_SESSION['SESS_EMPLOYEE_ID'] = $item['EmpFn'];
				$_SESSION['SESS_EMPLOYEE_FNAME'] = $item['FirstName'];
				$_SESSION['SESS_EMPLOYEE_LNAME'] = $item['LastName'];
				$_SESSION['SESS_ISSUPERVISOR'] = $item['IsSupervisor'];

				
				echo $_SESSION['SESS_EMPLOYEE_ID'];
			}
				
			$id = clean($_SESSION['SESS_EMPLOYEE_ID']);
			$sql = "SELECT `Serial`, `EmpFn`, `FirstName`, `LastName` FROM `employees` WHERE `Serial`=
					(SELECT `Acting` FROM `employees` WHERE `Serial`=
					(SELECT `Supervisor` FROM `employees` WHERE `EmpFn`='$id'))";
					
			$row = $dbo->prepare($sql);
			//$row = mysqli_prepare($con, $sql);
			$row->execute();
			//mysqli_stmt_execute($row);

			$result=$row->fetchAll(PDO::FETCH_ASSOC);
			//$result = mysqli_fetch_all($row, MYSQLI_ASSOC);
			
			foreach($result as $value )
			{
				$_SESSION['SESS_SUPERVISOR_ID'] = $value['EmpFn'];
				$_SESSION['SESS_SUPERVISOR_FNAME'] = $value['FirstName'];
				$_SESSION['SESS_SUPERVISOR_LNAME'] = $value['LastName'];
			}	
			session_write_close();
			header("location: index.php");
			exit();
		}else {
			//Login failed
			$_SESSION['LOGIN_FAILED'] = 'User ID does not exist';
			header("location: index.php");
			exit();
		}
		
			
	}
?>