<?PHP
// this code will get the industry that was sent to the php script and then give back the correct mailing address(rainstorm or sun hawk)
function getMailingAddress(){
	// check that the data was sent first
	if(isset($_REQUEST["industryToContact"])){
		// get the sent data and set the email address to return
		$industryToContact = $_REQUEST["industryToContact"];
		$rainstormEmail = "***REMOVED***";
		$sunhawkEmail = "***REMOVED***";
		
		// return the email address for the given industry
		if($industryToContact == "Rainstorm"){
			return $rainstormEmail;
		}
		if($industryToContact == "Sunhawk"){
			return $sunhawkEmail;
		}
		// all other options have failed to now to echo the error message which will appear inside a formatted JSON string
		echo ",{'errorType':'industry','errorMessage':'The requested industry does not exist'},";
		return null;
	}
	else
	{
		// no data was sent so now to echo the error for that to go inside the JSON
		echo "{'errorType':'industry','errorMessage':'No industry option was sent'},";
		return null;
	}
}

?>