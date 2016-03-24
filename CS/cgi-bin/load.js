function setIP(ip){
	var result = window.external.setIP(ip);
	alert("Connect "+ip+":\n"+result);
}
