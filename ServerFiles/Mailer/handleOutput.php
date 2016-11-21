<?PHP
// this function formats an output string with the errors and a success into a valid JSON object
function handleOutput(){
	global $errors;
	global $success;
	$errors = rtrim($errors, ",");
	$output = "{'errors':[" . $errors .  "],'success':" . (int)$success . "}";
	echo $output;
}

?>`