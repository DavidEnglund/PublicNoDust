<?PHP
// of course we need all of the connection details and setup
require_once("../connectToDatabase.php");
// this function will get the details and add them to the database to record that a contact request was sent
function getHelpLinks(){
	//setting up the SQL string
	 $sql = "SELECT * FROM helpLinks";
	 // start the return JSON and success boolean
	 $returnJSON = "{";
	 $success = false;	 
	// try and execute the sql and catch the error if it fails
	try{
		$returnJSON .= "'links':[";
		foreach($databaseConnection->query($sql) as $row) {
			$returnJSON .= "{'id':" $row['id'] . ",'link':'" . $row['link'] . "','title':'" . $row['title'] . "'},";
			$returnJSON = rtrim($returnJSON, ",");
			$returnJSON .= "],error:{},";
			$success = true;
		}	
	}
	catch(PDOException $e){
		$returnJSON .= "links:{},"
		$returnJSON .= "error:{'errorType':'sql','errorMessage':'" . $e->getMessage() . "'},";
		$success = false;
    }	
	// now to finish off the return and echo it 
	$returnJSON .= "'success':" . $success . "}";
}
?>