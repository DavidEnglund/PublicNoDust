<?PHP
// of course we need all of the connection details and set-up
require_once("../connectToDatabase.php");
// then the code will connect to the database, run the prepared sql and json encode and output results
connectToDatabase();
global $databaseConnection;
$sql = "SELECT * FROM ProductMatrix";
$statement=$databaseConnection->prepare($sql);

$statement->execute();
$results=$statement->fetchAll(PDO::FETCH_ASSOC);
// this is just an echo to see what gets send back when I make a json out of the different database returns.
$json=json_encode($results,JSON_NUMERIC_CHECK);
// output the product matrix array acquired form the database table
echo $json;

?>