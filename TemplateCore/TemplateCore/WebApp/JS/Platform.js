﻿var Platform = {};

Platform.serverURL = "api/"; //"http://localhost:52530/api/base/";
Platform.serverRequestQeue = [];

 Platform.RegisterUser = function(iPlayerData, iResponseFunc) {
    var dataWrapper = { Data: iPlayerData };
    getDataFRomServer("User/RegisterNewUser", dataWrapper, iResponseFunc);
}

Platform.LogInPlayer = function(iPlayerData, iResponseFunc) {
    var dataWrapper = { Data: iPlayerData };
    getDataFRomServer("User/Login", dataWrapper, iResponseFunc);
}

Platform.IsLogIn = function(iResponseFunc) {
    var reqData = { Data: {} };
    getDataFRomServer(iResponseFunc, "User/IsLogin", reqData);
}

 Platform.LogOut = function(iResponseFunc) {
    var reqData = { Data: {} };
    getDataFRomServer(iResponseFunc, "User/Logout", reqData);
}

Platform.GetTemplate = function(iTemplateName, iResponseFunc) {
    var reqData = { 
        Data: {
            templateName: iTemplateName
        } 
    };
    getDataFRomServer(iResponseFunc, "Template/GetTemplate", reqData);
}

Platform.SearchTemplate = function(iSearchKey, iResponseFunc) {
    var reqData = { 
        Data: {
        searchKey: iSearchKey
        } 
    };
    getDataFRomServer(iResponseFunc, "Template/SearchTemplate", reqData);
}

Platform.getDataFRomServer = function(path, requestData, callback) {
    var url = serverURL + path;
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

 Platform.HandleServerResponse = function(iResponse, iResponseFunction) {
    SendServerRequest.isLock = false;

    iResponseFunction(iResponse);
    SendServerRequest(); //recall for the send function to release the queue
}