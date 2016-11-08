<?PHP
// all of the required files to be included
require_once("getDetails.php");
require_once("checkDetails.php");
require_once("getMailingAddress.php");
require_once("checkIndustry.php");
require_once("getSubject.php");
require_once("getBody.php");
require_once("getHeaders.php");
require_once("handleOutput.php");


//global error string and success boolean
$errors = "";
$success = false;

// now to run the request contact function and then to handle outputting errors
requestContact();
handleOutput();

// this will run the validation and formatting. If it passes run the send email function with the validated and formatted input
function requestContact(){
	if(checkDetails() && checkIndustry()){
		
		$address = getMailingAddress();
		$subject = getSubject();
		$body = getBody();
		$headers = getHeaders();
		
		// check that it sends. If it does not add to the errors message, if it does set success to true
		if(!mail($address,$subject,$body,$headers)){
			$errors .= error_get_last();
		}
		else{
			$success = true;
		}
	}
}





?>