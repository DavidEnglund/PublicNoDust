<?PHP
// this function will get the details and add them to the database to record that a contact request was sent
function sendContactRequestToDatabase(){
	// getting the details
	$details = getDetails();
	//setting up the SQL string
	 $sql = "INSERT INTO contactRequests (createdDate,name,jobLocation,areaType,areaSize,contactDate,contactDetails,jobDuration,contactType)
    VALUES (now(), '$details[0]', '$details[1]', '$details[2]', $details[3], '$details[4]', '$details[5]', $details[6], '$details[7]')";
	// try and execute the sql and catch the error if it fails
	try{
		$conn->exec($sql);
	}
	catch(PDOException $e)
    {
		$error .= "{'errorType':'sql','errorMessage':'" . $e->getMessage() . "'},";
    }
}

?>