<?PHP
// global connection variable
$databaseConnection;
// this function will create and maintain the database connection
function connectToDatabase(){
global $databaseConnection;
	// get all of the connection variable set-up
	$servername = "localhost";
	$database = "rainstorm_app";
	$username = "rainstorm_app";
	$password = "***REMOVED***";
	try {
		$databaseConnection = new PDO("mysql:host=$servername;dbname=$database", $username, $password);
		// set the PDO error mode to exception
		$databaseConnection->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
		
	}
	catch(PDOException $e){
		echo "Connection failed: " . $e->getMessage();
	}
}
?>