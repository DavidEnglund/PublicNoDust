<?PHP
// this will format the sent details for the quote into a formatted string
// to be used as the body of the email to be sent to the industry to inform them of a contact request
function getBody(){

	// returns an array with the quote details in this order: name,location,area type,size,time,contact details,duration,contact type.
	$details = getDetails();
	
	// the contact type will be used to better format the return contact information
		$contactInformation = $details[5];
		if($details[7] == "phone"){
			$contactInformation = "ph: " .$details[5];
		}
		if($details[7] == "email"){
			$contactInformation = "<a href='mailto:" .$details[5]. "'>".$details[5]. "</a>" ;
		}
	
	
	// sets the body text using html 
	$bodyText = "<h1 style='font-size:1.2em;font-weight:bold;'>Contact Request</h1>" . 
	"<p>Hi, \r\n Could you please contact " .$details[0]. " regarding their quote for the location " .$details[1]. ".</p>\r\n".
	"<p>This is a " .$details[2]. " with a size of " .$details[3]. ". This job will last " .$details[6]. " days.</p> \r\n ".
	"<p>" .$details[0]. " would like you to contact them at " .$details[4]. " via " .$contactInformation. " .</p>\r\n".
	"<p>Thank you</p>";
	
	return $bodyText;
}
?>