<?PHP


function gatherDetails(){
	// this function will gather the sent details and format them for mailing
	
	
	$mailingAddress = getMailingAddress();
	$subject = getSubject();
	$body = getMailBody();
	$headers = getHeaders();	
}

?>