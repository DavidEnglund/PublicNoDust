<?PHP
// this function formats an output string with the errors and a success into a valid JSON object
function handleOutput(){
	
	$errors = rtrim($errors, ",");
	$output = "{'errors':[" . $errors .  "],'success':" . $success . "}";
	echo $output;
}

?>