function ajaxFunction() {
    var ajaxRequest;
    try {
        // Opera 8.0+, Firefox, Safari
        ajaxRequest = new XMLHttpRequest();
    } catch (e) {
        // Internet Explorer Browsers
        try {
            ajaxRequest = new ActiveXObject("Msxml2.XMLHTTP");
        } catch (e) {
            try {
                ajaxRequest = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e) {
                // Something went wrong
                alert("Your browser does not support this technology!");
                return false;
            }
        }
    }
}

function configureOurRequest() {
    ajaxFunction();

       ajaxRequest.onreadystatechange = updateRequests;

    var url = "includeParameters.php";
       ajaxRequest.open("GET",url,true);
       ajaxRequest.send();

}

function UpdateRequests() {

    if (ajaxRequest.readystate == 4 && ajaxRequest.status == 200) { 

    }

}