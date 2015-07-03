<?php
	session_start();
	function emphome($dbo)
	{
		
?>
		<nav id="navigation">
			<div class="menu">
				<ul class="nav nav-tabs">
					<li><a href="?action=logout">LOGOUT</a></li>
					<li><a href="?action=employee">EMPLOYEE</a></li>
					<li><a href="?action=supervisor">SUPERVISOR</a></li>		
				</ul>
			</div>
		</nav>
		
		
		<label for="name">EMPLOYEE DETAILS</label> <br/> 
		EMPLOYEE ID: <?php echo $_SESSION['SESS_EMPLOYEE_ID']?> </br>
		EMPLOYEE NAME: <?php echo $_SESSION['SESS_EMPLOYEE_FNAME']?> <?php echo $_SESSION['SESS_EMPLOYEE_LNAME']?></br>
		SUPERVISOR NAME: <?php echo $_SESSION['SESS_SUPERVISOR_FNAME']?> <?php echo $_SESSION['SESS_SUPERVISOR_LNAME']?></br>
		
		 
<?php
	}
?>