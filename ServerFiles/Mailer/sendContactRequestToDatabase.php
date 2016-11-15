<?PHP
// this function will get the details and add them to the database to record that a contact request was sent
function sendContactRequestToDatabase(){
	connectToDatabase();
	global $errors;
	global $databaseConnection;
	// getting the details
	$details = getDetails();
	$industry = $_REQUEST["industryToContact"];
	//setting up the SQL string
	 $sql = "INSERT INTO contactRequests (createdDate,name,jobLocation,areaType,areaSize,contactDate,contactDetails,jobDuration,contactType,industry)
    VALUES (now(), '$details[0]', '$details[1]', '$details[2]', $details[3], '$details[4]', '$details[5]', $details[6], '$details[7]','$industry')";
	// try and execute the sql and catch the error if it fails
	try{
		$databaseConnection->exec($sql);
		return true;
	}
	catch(PDOException $e)
    {
		$errors .= "{'errorType':'sql','errorMessage':'" . $e->getMessage() . "'},";
		return false;
    }
}

?>