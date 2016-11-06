<?php
function testMail(){
	$to = "***REMOVED***";
	$subject = "testing php mailer";
	$txt = 
	"this is a test of sending an email from the rainstorm server. \r\n
	this code will be used as part of a function to send contact requests from the applications users. \r\n
	Do not reply to this email.";
	$headers = "From: app@rainstorm.com.au" . "\r\n";

	mail($to,$subject,$txt,$headers);
}

function gatherDetails(){
	// this function will gather the sent details and format them for mailing
	$contactInfomation = $_REQUEST["contactInfomation"];
	$requestedDate = $_REQUEST["requestedDate"];
	$contactsName = $_REQUEST["contactsName"];
	$jobLocation = $_REQUEST["jobLocation"];
	$contactType = $_REQUEST["contactType"];
	$industryToContact = $_REQUEST["industryToContact"];
	
	$mailingAddress = getMailingAddress($industryToContact);
	$subject = getSubject();
	$body = getMailBody();
	$headers = getHeaders();	
}
// example of returned JSON
// {'errors':[{'errorType':'industry','errorMessage':'No industry option was sent'}],'success':0}
?>