<?PHP
// global connection variable
$databaseConnection;
// this function will create and maintain the database connection
function connectToDatabase(){
	

	
	$servername = "localhost";
	$database = "rainstorm_app";
	$username = "rainstorm_app";
	$password = "***REMOVED***";
	
	try {
		$databaseConnection = new PDO("mysql:host=$servername;dbname=$database", $username, $password);
		// set the PDO error mode to exception
		$databaseConnection->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
		echo "Connected successfully"; 
	}
	catch(PDOException $e){
		echo "Connection failed: " . $e->getMessage();
	}
}

?>