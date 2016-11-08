<?PHP
// this function get the headers for the email so we can set the users email as a reply and create html mails
function getHeaders(){
	// set the from as coming from the applications "address" (it does not yet exist)
	$headers = "From: app@rainstorm.com.au" . "\r\n";
	
	// check for email type request to set the reply-to 
	if(isset($_REQUEST["contactType"]){
		if($contactType == "email"){
			$headers .= "Reply-To: ". strip_tags($_REQUEST["contactType"]) . "\r\n";
		}
	}
	
	// additional information required to send pretty html mails
	$headers .= "MIME-Version: 1.0\r\n";
	$headers .= "Content-Type: text/html; charset=ISO-8859-1\r\n";
}

?>