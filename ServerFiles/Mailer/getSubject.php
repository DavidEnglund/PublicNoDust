<?PHP
// this will format the sent details for the quote into a formatted string
// to be used as the body of the email to be sent to the industry to inform them of a contact request
function getSubject(){

	// returns an array with the quote details in this order: name,location,area type,size,time,contact details.
	$details = getDetails();
	
	$bodyText = "Hi, \r\n Could you please contact " .$details[0]. " regarding their quote for the location " .$details[1]. " \r\n".
	"which is a " .$details[2]. " with a size of " .$details[3]. " \r\n ".
	$details[0]. " would like you to contact them at " .$details[4]. " via " .$details[5]. "\r\n".
	"Thank you";
	
	return $bodyText;
}
?>