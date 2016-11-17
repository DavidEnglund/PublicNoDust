<?PHP
// this will send back a json object that will contain all of the tables that need to be updated.
error_reporting(E_ALL);
ini_set('display_errors', 1);
// require all of the files that will get the tables
require_once("getDBMetaData.php");
require_once("getAreaType.php");
require_once("getSupplier.php");
require_once("getProductDescription.php");
require_once("getProductDuration.php");
require_once("getProduct.php");
require_once("getProductMatrix.php");

// now for an object to be turned into json when filled with data
class DBTables{
	public $DBMetaData;
	public $AreaType;
	Public $Supplier;
	public $Product;
	public $ProductDescription;
	public $ProductDuration;
	public $ProductMatrix;
}
// and now to actually get everything 
$dbTables = new DBTables;
$dbTables->DBMetaData = getDBMetaData();
$dbTables->AreaType = getAreaType();
$dbTables->Supplier = getSupplier();
$dbTables->Product = getProduct();
$dbTables->ProductDescription = getProductDescription();
$dbTables->ProductDuration = getProductDuration();
$dbTables->ProductMatrix = getProductMatrix();


?>