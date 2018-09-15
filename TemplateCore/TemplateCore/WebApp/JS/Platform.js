var Platform = {};

Platform.serverURL = "http://localhost:52530/api/"; //"api/"; //

// User Controller API
Platform.RegisterUser = function (iFirstName, iLastName, iEmail, iPassword, iResponseFunc) {
     var dataWrapper = {
         Data: {
             FirstName: iFirstName,
             LastName: iLastName,
             Email: iEmail,
             Password: iPassword
         }
     };
    Platform.getDataFRomServer("User/RegisterNewUser", dataWrapper, iResponseFunc);
}

Platform.LogIn = function (iEmail, iPassword, iResponseFunc) {
    var dataWrapper = { 
        Data: {
            Email: iEmail,
            Password: iPassword
        } 
    };
    Platform.getDataFRomServer("User/Login", dataWrapper, iResponseFunc);
}

Platform.IsLogIn = function (iEmail, iResponseFunc) {
    var dataWrapper = {
        Data: {
            Email: iEmail
        }
    };
    Platform.getDataFRomServer("User/IsLogin", dataWrapper, iResponseFunc);
}

Platform.LogOut = function (iEmail, iResponseFunc) {
    var dataWrapper = { Data: { Email: iEmail } };
     Platform.getDataFRomServer("User/Logout", dataWrapper, iResponseFunc);
}

Platform.GetUserData = function (iEmail, iResponseFunc) {
    var dataWrapper = { Data: { Email: iEmail } };
    Platform.getDataFRomServer("User/GetUserData", dataWrapper, iResponseFunc);
}

// Template Controller API
Platform.GetTemplate = function(iTemplateName, iResponseFunc) {
    var dataWrapper = {
        Data: {
            templateName: iTemplateName
        } 
    };
    Platform.getDataFRomServer("Template/GetTemplate", dataWrapper, iResponseFunc);
}

Platform.SearchTemplate = function(iSearchKey, iResponseFunc) {
    var dataWrapper = {
        Data: {
        searchKey: iSearchKey
        } 
    };
    Platform.getDataFRomServer("Template/SearchTemplate", dataWrapper, iResponseFunc);
}

Platform.GetAllTopics = function (iResponseFunc) {
    var dataWrapper = {
        Data: { }
    };
    Platform.getDataFRomServer("Template/GetAllTopics", dataWrapper, iResponseFunc);
}

Platform.GetTopicsInCategory = function (iCategoryName, iResponseFunc) {
    var dataWrapper = {
        Data: {
            CategoryName: iCategoryName
        }
    };
    Platform.getDataFRomServer("Template/GetTopicsInCategory", dataWrapper, iResponseFunc);
}


Platform.getDataFRomServer = function(path, requestData, callback) {
    var url = Platform.serverURL + path;
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
                });
            } else {
                console.log("Oops, we haven't got JSON from server");
            }
        })
        .catch(err => console.log(err));
}