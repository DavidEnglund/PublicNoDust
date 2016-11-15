<?PHP

function getSubject(){
	// returns an array with the quote details in this order: name,location,area type,size,time,contact details.
	$details = getDetails();
	$subject = "Contact Request: Please contact " .$details[0];
	return $subject;
}

?>