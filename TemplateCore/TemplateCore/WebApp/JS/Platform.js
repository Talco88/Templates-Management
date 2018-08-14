let baseServerUrl = "http://localhost:52530/";

function requestFromServer(functionName, ReqBody, Callback) {
    $.post(baseServerUrl,
        {
            name: functionName,
            body: ReqBody
        },
        function (data, status) {
            alert("Data: " + data + "\nStatus: " + status);
        });
}