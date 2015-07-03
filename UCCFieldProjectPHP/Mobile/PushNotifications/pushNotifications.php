<?php
	function pushNotification($pushURI, $employeeName, $checkID, $location, $checkType)
	{
		//$pushURI = "http://s.notify.live.net/u/1/db3/H2QAAAB6ZOn67HEp_gXuz6XPxEXvq7PX8rlRvXO7YLs3TCRdk664tdfkmLBdgx-m0yud7DCfHFNQh_NYpeHXmqezRWTZxzPYs3ZN_3SbAwGuNTysuM_0Vfxz64EU3v60jZpVQHU/d2luZG93c3Bob25lZGVmYXVsdA/0pqGa5A7DkiEcxeWg4Asvw/k5NaOc2_-HB_uP-4UBRcR7p30XE";
		 // Create the toast message
		
		
		$toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" .
					"<wp:Notification xmlns:wp=\"WPNotification\">" .
					   "<wp:Toast>" .
						   "<wp:Text1>{$employeeName}</wp:Text1>" . 
						   "<wp:Text2>{$checkType}</wp:Text2>" .
                           "<wp:Param>/SupervisorPage.xaml?" . 
                                "employeeName={$employeeName}&amp;" . 
                                "checkID={$checkID}&amp;" . 
                                "location={$location}&amp;" . 
                                "checkType={$checkType}" . 
                           "</wp:Param>" .
					    "</wp:Toast>" .
					"</wp:Notification>";
///SupervisorPage.xaml?employeeName=."$employeeName".&employeeID=."$employeeID".&location=."$location."&reason=."$reason."&checkType=."$checkType."
		// Create request to send
		$r = curl_init();
		curl_setopt($r, CURLOPT_URL, $pushURI);
		curl_setopt($r, CURLOPT_RETURNTRANSFER, 1);
		curl_setopt($r, CURLOPT_POST, true);
		curl_setopt($r, CURLOPT_HEADER, true); 

		// add headers
		$httpHeaders=array('Content-type: text/xml; charset=utf-8', 'X-WindowsPhone-Target: toast',
						'Accept: application/*', 'X-NotificationClass: 2','Content-Length:'.strlen($toastMessage));
		curl_setopt($r, CURLOPT_HTTPHEADER, $httpHeaders);

		// add message
		curl_setopt($r, CURLOPT_POSTFIELDS, $toastMessage);

		// execute request
		$output = curl_exec($r);
		curl_close($r);
		echo "</br>".$pushURI."</br>";
		echo $employeeID."</br>";
		echo $location."</br>";
		echo $reason."</br>";
		echo $checkType."</br>";
      
	}
?>