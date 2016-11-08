<?PHP

function getDetails(){
	// all of the details that should be sent
	$contactInfomation = "";
	$requestedDate = "";
	$contactsName = "";
	$jobLocation = "";
	$contactType = "";
	$areaSize = "";
	$areaType = "";
	$jobDuration = "";
	// now to check that they are all set 
	if(isset($_REQUEST["contactInfomation"]){
		$contactInfomation = $_REQUEST["contactInfomation"];
	}
	else{
		$errors .= "{'errorType':'details','errorMessage':'No contact information was sent'},";
		return false;
	}
	// we will need to check and format the date to be better readable in the email
	if(isset($_REQUEST["requestedDate"]){
		$sentDate = date_parse($_REQUEST["requestedDate"]);
		if($sentDate['errors'].length ==0){
			$createdDate = date_create($_REQUEST["requestedDate"]);
			$requestedDate = date_format($createdDate,"on the d/m/Y at H:i");
		}
		else{
			$errors .= "{'errorType':'details','errorMessage':'contact date was invalid'},";
		return false;
		}
		
		
		$requestedDate = $_REQUEST["requestedDate"];
		
	}
	else{
		$errors .= "{'errorType':'details','errorMessage':'No contact date was sent'},";
		return false;
	}
	if(isset($_REQUEST["contactsName"]){
		$contactsName = $_REQUEST["contactsName"];
	}
	else{
		$errors .= "{'errorType':'details','errorMessage':'No contact name was sent'},";
		return false;
	}
	if(isset($_REQUEST["jobLocation"]){
		$jobLocation = $_REQUEST["jobLocation"];
	}
	else{
		$errors .= "{'errorType':'details','errorMessage':'No job location was sent'},";
		$jobLocation = "unknown location";
	}

	if(isset($_REQUEST["contactType"]){
		$contactType = $_REQUEST["contactType"];
	}
	else{
		$errors .= "{'errorType':'details','errorMessage':'No contact type was sent'},";
		$contactType = "unknown";
	}
	if(isset($_REQUEST["areaSize"]){
		$areaSize = $_REQUEST["areaSize"];
	}
	else{
		$errors .= "{'errorType':'details','errorMessage':'No area size was sent'},";
		$areaSize = 0;
	}
	if(isset($_REQUEST["areaType"]){
		$areaType = $_REQUEST["areaType"];
	}
	else{
		$errors .= "{'errorType':'details','errorMessage':'No area type was sent'},";
		$areaType = "unknown area";
	}
	if(isset($_REQUEST["jobDuration"]){
		$jobDuration = $_REQUEST["jobDuration"];
	}
	else{
		$errors .= "{'errorType':'details','errorMessage':'No job duration was sent'},";
		$jobDuration = 0;
	}
	
	
	$details = ($contactsName,$jobLocation,$areaType,$areaSize,$requestedDate,$contactInfomation,$jobDuration,$contactType);
	return $details;
	
}

?>
	
	