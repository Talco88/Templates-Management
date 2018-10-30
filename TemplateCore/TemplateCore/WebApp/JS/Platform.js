var Platform = {};

Platform.serverURL = "../api/"; //"http://localhost:52530/api/"

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

Platform.IsLogIn = function (iResponseFunc) {
    var dataWrapper = {
        Data: { }
    };
    Platform.getDataFRomServer("User/IsLogin", dataWrapper, iResponseFunc);
}

Platform.LogOut = function (iEmail, iResponseFunc) {
    var dataWrapper = { Data: { Email: iEmail } };
     Platform.getDataFRomServer("User/Logout", dataWrapper, iResponseFunc);
}

Platform.LogoutCurrentUser = function (iResponseFunc) {
    var dataWrapper = { Data: {  } };
    Platform.getDataFRomServer("User/LogoutCurrentUser", dataWrapper, iResponseFunc);
}

Platform.GetUserData = function (iEmail, iResponseFunc) {
    var dataWrapper = { Data: { Email: iEmail } };
    Platform.getDataFRomServer("User/GetUserData", dataWrapper, iResponseFunc);
}

Platform.GetLoggedInUserData = function (iResponseFunc) {
    var dataWrapper = { Data: {  } };
    Platform.getDataFRomServer("User/GetLoggedInUserData", dataWrapper, iResponseFunc);
}

// Template Controller API
Platform.GetTemplate = function(iCategoryName, iTemplateName, iResponseFunc) {
    var dataWrapper = {
        Data: {
            CategoryName: iCategoryName,
            HeaderName: iTemplateName
        } 
    };
    Platform.getDataFRomServer("Template/GetTemplate", dataWrapper, iResponseFunc);
}

Platform.GetTemplateDetails = function (iCategoryName, iTemplateName, iResponseFunc) {
    var dataWrapper = {
        Data: {
            CategoryName: iCategoryName,
            HeaderName: iTemplateName
        }
    };
    Platform.getDataFRomServer("Template/GetTemplateDetails", dataWrapper, iResponseFunc);
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

Platform.GetAllFavoriteTemplates = function (iUserEmail, iResponseFunc) {
    var dataWrapper = {
        Data: {
            UserEmail: iUserEmail
        }
    };
    Platform.getDataFRomServer("Template/GetAllFavoriteTemplates", dataWrapper, iResponseFunc);
}

Platform.GetAllTemplatesInCategory = function (iCategoryName, iResponseFunc) {
    var dataWrapper = {
        Data: {
            CategoryName: iCategoryName
        }
    };
    Platform.getDataFRomServer("Template/GetAllTemplatesInCategory", dataWrapper, iResponseFunc);
}

Platform.RateTamplate = function (iCategoryName, iTemplateName, iRateNumber, iResponseFunc) {
    var dataWrapper = {
        Data: {
            CategoryName: iCategoryName,
            TemplateName: iTemplateName,
            RateNumber: iRateNumber
        }
    };
    Platform.getDataFRomServer("Template/RateTamplate", dataWrapper, iResponseFunc);
}

Platform.AddCommentToTemplate = function (iCategoryName, iTemplateName, iComment, iResponseFunc) {
    var dataWrapper = {
        Data: {
            CategoryName: iCategoryName,
            TemplateName: iTemplateName,
            Comment: iComment
        }
    };
    Platform.getDataFRomServer("Template/AddCommentToTemplate", dataWrapper, iResponseFunc);
}

Platform.SetSharedTemplate = function (iCategoryName, iTemplateName, iIsShared, iResponseFunc) {
    var dataWrapper = {
        Data: {
            CategoryName: iCategoryName,
            TemplateName: iTemplateName,
            IsShared: iIsShared
        }
    };
    Platform.getDataFRomServer("Template/SetSharedTemplate", dataWrapper, iResponseFunc);
}

Platform.DeleteTemplate = function (iCategoryName, iTemplateName, iResponseFunc) {
    var dataWrapper = {
        Data: {
            CategoryName: iCategoryName,
            TemplateName: iTemplateName
        }
    };
    Platform.getDataFRomServer("Template/DeleteTemplate", dataWrapper, iResponseFunc);
}

Platform.MarkTemplateAsFavorite = function (iCategoryName, iTemplateName, iResponseFunc) {
    var dataWrapper = {
        Data: {
            CategoryName: iCategoryName,
            TemplateName: iTemplateName
        }
    };
    Platform.getDataFRomServer("Template/MarkTemplateAsFavorite", dataWrapper, iResponseFunc);
}

Platform.RemoveMarkTemplateAsFavorite = function (iCategoryName, iTemplateName, iResponseFunc) {
    var dataWrapper = {
        Data: {
            CategoryName: iCategoryName,
            TemplateName: iTemplateName
        }
    };
    Platform.getDataFRomServer("Template/RemoveMarkTemplateAsFavorite", dataWrapper, iResponseFunc);
}

Platform.GenerateHTMLTemplateWithValues = function (iHeaderName, iCategoryName, iValues, iResponseFunc) {
    var dataWrapper = {
        Data: {
            Template: {
                HeaderName: iHeaderName,
                CategoryName: iCategoryName,
                Values: iValues
            }
        }
    };
    Platform.getDataFRomServer("Template/GenerateHTMLTemplateWithValues", dataWrapper, iResponseFunc);
}

Platform.OpenTemplateInWord = function (iFileName, iContent, iResponseFunc) {
    var dataWrapper = {
        Data: {
            Content: iContent,
            FileName: iFileName
        }
    };
    Platform.getDataFRomServer("Template/OpenTemplateInWord", dataWrapper, iResponseFunc);
}

Platform.CreateNewTemplate = function (iData, iTemplateName, iCategory, iIsShared, iResponseFunc) {
    var dataWrapper = {
        Data: {
            Data: iData,
            TemplateName: iTemplateName,
            Category: iCategory,
            IsShared: iIsShared
        }
    };
    Platform.getDataFRomServer("Template/CreateNewTemplate", dataWrapper, iResponseFunc);
}

Platform.UpdateHeaderInTopic = function (iCategoryName, iOldHeaderName, iNewHeaderName, iResponseFunc) {
    var dataWrapper = {
        Data: {
            CategoryName: iCategoryName,
            OldHeaderName: iOldHeaderName,
            NewHeaderName: iNewHeaderName,
            IsShared: iIsShared
        }
    };
    Platform.getDataFRomServer("Template/UpdateHeaderInTopic", dataWrapper, iResponseFunc);
}

Platform.getDataFRomServer = function(path, requestData, callback) {
    var url = Platform.serverURL + path;
    Platform.ShowLoading();
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
                        Platform.HideLoading();
                    }
                }).catch(err => {
                    console.log(err);
                    Platform.HideLoading();
                });
            } else {
                console.log("Oops, we haven't got JSON from server");
                Platform.HideLoading();
            }
            Platform.HideLoading();
        })
        .catch(err => console.log(err));
}

Platform.ShowLoading = function () {
    let popupDiv = document.querySelector(".popup-page")
    popupDiv.classList.add("show-div");
    popupDiv.classList.remove("hide-div");
}

Platform.HideLoading = function () {
    let popupDiv = document.querySelector(".popup-page")
    popupDiv.classList.add("hide-div");
    popupDiv.classList.remove("show-div");
}