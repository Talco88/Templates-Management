var serverURL = "api/"; //"http://localhost:52530/api/base/";
var serverRequestQeue = [];

function testLogin() {
    var data = {};
    data.username = "Tal";
    LogInPlayer(data, testCallback);   
}

function testCallback() {
    alert("Good!");
}

function RegisterUser(iPlayerData, iResponseFunc) {
    var dataWrapper = { Data: iPlayerData };
    getDataFRomServer("User/RegisterNewUser", dataWrapper, iResponseFunc);
}

function LogInPlayer(iPlayerData, iResponseFunc) {
    var dataWrapper = { Data: iPlayerData };
    getDataFRomServer("User/Login", dataWrapper, iResponseFunc);
}

function IsLogIn(iResponseFunc) {
    var reqData = { Data: {} };
    getDataFRomServer(iResponseFunc, "User/IsLogin", reqData);
}

function LogOut(iResponseFunc) {
    var reqData = { Data: {} };
    getDataFRomServer(iResponseFunc, "User/Logout", reqData);
}

function getDataFRomServer(path, requestData, callback) {
    let url = serverURL + path;
    fetch(`${url}`, {
        method: 'POST',
        credentials: 'include',
        headers: { 'Content-Type': 'application/json' },
        body: (requestData) ? JSON.stringify(requestData) : ""
    })
        .then((response) => {
            var contentType = response.headers.get("content-type");
            if (contentType && contentType.indexOf("application/json") !== -1) {
                return response.json().then((json) => {
                    if (callback != null) {
                        alert(json.RetObject);
                        callback(json);
                    }
                }).catch(err => {
                    console.log(err);
                });
            } else {
                console.log("Oops, we haven't got JSON from server");
            }
        })
        .catch(err => console.log(err));
}

function HandleServerResponse(iResponse, iResponseFunction) {
    SendServerRequest.isLock = false;

    iResponseFunction(iResponse);
    SendServerRequest(); //recall for the send function to release the queue
}