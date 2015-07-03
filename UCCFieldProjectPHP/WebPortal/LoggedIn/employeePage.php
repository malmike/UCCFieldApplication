<?php
	function employeePage()
	{
				
		?>
		<form role="form" method="post" action="?connect=checkIn"> 
			<h3>EMPLOYEE CHECK IN OR OUT</h3>
			<div class="form-group"> 
				<label for="name">ENTER LOCATION</label> 
				<input type="text" class="form-control" name="location" id="location" placeholder = "Enter the location"> 
			</div>
			
			<div class="form-group"> 
				<label for="name">ENTER REASON</label> 
				<textarea class="form-control" name="reason" id="reasom" placeholder = "Enter Reason"> </textarea>
			</div> 
			<button type="submit" name="CheckIn" id="CheckIn" class="btn btn-default">CHECKIN</button> 
			<button type="submit" name="CheckOut" id="CheckOut" class="btn btn-default">CHECKOUT</button> 

		</form>	
		<?php
	}
?>
