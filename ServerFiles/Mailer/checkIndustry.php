<?PHP

function checkIndustry(){
	if(getMailingAddress() == null){
		return false;
	}
	else{
		return true;
	}
}
?>