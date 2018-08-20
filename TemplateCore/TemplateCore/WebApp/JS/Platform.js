var serverURL = "api/base/"; //"http://localhost:52530/api/base/";
var serverRequestQeue = [];

function RegisterPlayer(iPlayerData, iResponseFunc) {
    var dataWrapper = { Data: iPlayerData };
    SendServerRequest(iResponseFunc, "User", dataWrapper);
}

function testLogin() {
    var data = {};
    data.username = "Tal";
    LogInPlayer(data, testCallback);
}

function testCallback() {
    alert("Good!");
}

function LogInPlayer(iPlayerData, iResponseFunc) {
    var dataWrapper = { Data: iPlayerData };
    getDataFRomServer("Login", dataWrapper, iResponseFunc);
}

function IsLogIn(iResponseFunc) {
    var reqData = { Data: {} };
    SendServerRequest(iResponseFunc, "IsLogin", reqData);
}

function LogOut(iResponseFunc) {
    var reqData = { Data: {} };
    SendServerRequest(iResponseFunc, "Logout", reqData);
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
                        callback(json);
                    }
                }).catch(err => {
                    console.log(err);
                    let i = 0;
                    i++;
                });
            } else {
                console.log("Oops, we haven't got JSON from server");
            }
        })
        .catch(err => console.log(err)); //Promise.reject(err));
}

function SendServerRequest(iReturnFunction, iReqUrl, iRequestParams) {
    // check if isLock is define (for the first load)
    if (SendServerRequest.isLock == undefined) {
        // It has not... perform the initialization
        SendServerRequest.isLock = false;
    }

    // if iReqUrl is define, adding new entry to the qeue
    if (iReqUrl != undefined && iReqUrl != null && iReqUrl.length > 0) {
        var dataWrapper = "Data=" + JSON.stringify(iRequestParams);
        var requestData = { ReturnFunction: iReturnFunction, ReqUrl: iReqUrl, RequestParams: dataWrapper };
        serverRequestQeue.push(requestData);
    }

    if (!SendServerRequest.isLock && serverRequestQeue.length > 0) {
        var requestInfo = serverRequestQeue.shift();

        $.ajax({
            type: "POST",
            dataType: 'json',
            url: serverURL + requestInfo.ReqUrl,
            context: document.body,
            data: requestInfo.RequestParams,
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            success: function (response) {
                HandleServerResponse(response, requestInfo.ReturnFunction);
            }
        });
    }
}

function HandleServerResponse(iResponse, iResponseFunction) {
    SendServerRequest.isLock = false;

    iResponseFunction(iResponse);
    SendServerRequest(); //recall for the send function to release the queue
}