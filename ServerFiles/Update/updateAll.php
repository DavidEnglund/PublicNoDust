<?PHP
error_reporting(E_ALL);
ini_set('display_errors', 1);

// of course we need all of the connection details and setup
require_once("../connectToDatabase.php");
connectToDatabase();
global $databaseConnection;
$sql = array("SELECT * FROM ProductMatrix" ,"SELECT * FROM Product");
$statement=$databaseConnection->prepare($sql[0]);
$statement+=$databaseConnection->prepare($sql[1]);

$statement->execute();
$results=$statement->fetchAll(PDO::FETCH_ASSOC);
// this is just an echo to see what gets send back when I make a json out of the different database returns.
$jsona=json_encode($results);
$jsons=json_encode($statement);
echo "<h1>json statement</h1>";
echo $jsons;
echo "<h1>json fetch assoc</h1>";
echo $jsona;

?>