var selectedTemplatesPage = {};
var templateHeaderDetails = {};
var Global_Selected_Template_BaseHTMLData = "";
var nameOfWordFile = "";
var templateComtent = "";
var markAsFavoriteString = "Mark as favorite";
var unmarkFavoriteString = "Unmark favorite";

selectedTemplatesPage.setPage = function (iTemplateWrapper) {
    templateHeaderDetails = iTemplateWrapper.templateHeader;

    if (Global_Selected_Template_BaseHTMLData === "") {
        selectedTemplatesPage.LoadTemplatePageRes(true);
    }
    else {
        selectedTemplatesPage.onPagedRecived();
    }
}

selectedTemplatesPage.LoadTemplatePageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/HTML/selectedTemplatePage.html",
        dataType: 'text',
        success: function (data) {
            Global_Selected_Template_BaseHTMLData = data;
            selectedTemplatesPage.onPagedRecived();
        },
        error: function () {
            alert("error Loading Template Page");
        }
    });
}

selectedTemplatesPage.onPagedRecived = function () {
    $("#MainAppWindow").html("");
    setTimeout(function () {
        $("#MainAppWindow").html(Global_Selected_Template_BaseHTMLData);
        Platform.GetTemplate(templateHeaderDetails.MCategoryName, templateHeaderDetails.TemplateHeaderName, selectedTemplatesPage.valueFromTopicSelected);
        let backBtn = document.querySelector("#backBtn");
        backBtn.onclick = selectedTemplatesPage.onBackBtnClicked;

        var selectedReplaceTemplate = document.getElementById("nameOfTemplate");
        selectedReplaceTemplate.innerText = selectedReplaceTemplate.innerText + " " + templateHeaderDetails.TemplateHeaderName;

        let addCommentBtn = document.querySelector("#InsertComment-btn");
        addCommentBtn.onclick = selectedTemplatesPage.onAddCommentBtnClicked;

        let deleteTemplateBtn = document.querySelector("#deleteTemplate-btn");
        deleteTemplateBtn.onclick = selectedTemplatesPage.onDeleteTemplateBtnClicked;

        let markFavoritBtn = document.querySelector("#markFavoritBtn");
        markFavoritBtn.onclick = selectedTemplatesPage.onMarkFavoritBtnClicked;

        let starsTiles = document.getElementsByName("rating");
        for (var i = 0; i < starsTiles.length; i++) {
            starsTiles[i].onclick = selectedTemplatesPage.onStarBtnClicked;
        }

        selectedTemplatesPage.AddCommentField = document.getElementById("message");
    }, 1);
}

selectedTemplatesPage.onMarkFavoritBtnClicked = function () {
    var markFavoriteTemplate = document.getElementById("markFavoritBtn");
    selectedTemplatesPage.isFavoriteTemplate = (markFavoriteTemplate.text === markAsFavoriteString) ? true : false;
    if (selectedTemplatesPage.isFavoriteTemplate) {
        Platform.MarkTemplateAsFavorite(templateHeaderDetails.MCategoryName, templateHeaderDetails.TemplateHeaderName, selectedTemplatesPage.onMarkFavoritBtnRes);
    }
    else {
        Platform.RemoveMarkTemplateAsFavorite(templateHeaderDetails.MCategoryName, templateHeaderDetails.TemplateHeaderName, selectedTemplatesPage.onMarkFavoritBtnRes);
    }
}

selectedTemplatesPage.onMarkFavoritBtnRes = function (iEvent) {
    var markFavoriteTemplate = document.getElementById("markFavoritBtn");
    if (selectedTemplatesPage.isFavoriteTemplate) {
        markFavoriteTemplate.text = unmarkFavoriteString;
    }
    else {
        markFavoriteTemplate.text = markAsFavoriteString;
    }
}

selectedTemplatesPage.onAddCommentBtnClicked = function (iEvent) {
    var finalCommentMessage = Global_User_Data.FirstName + " " + Global_User_Data.LastName + ": " + selectedTemplatesPage.AddCommentField.value;
    Platform.AddCommentToTemplate(templateHeaderDetails.MCategoryName, templateHeaderDetails.TemplateHeaderName, finalCommentMessage, selectedTemplatesPage.onAddCommentBtnReceived);
}

selectedTemplatesPage.onAddCommentBtnReceived = function (iServerResponce) {
    if (iServerResponce.StatusCode === 0) {
        selectedTemplatesPage.AddCommentField.value = "";
        selectedTemplatesPage.onPagedRecived();
    }
    else {
        alert(iServerResponce.RetObject);
    }
}

selectedTemplatesPage.onStarBtnClicked = function (iEvent) {
    var index = iEvent.target.value;
    Platform.RateTamplate(templateHeaderDetails.MCategoryName, templateHeaderDetails.TemplateHeaderName, index, selectedTemplatesPage.onStarBtnClickedRes);
}

selectedTemplatesPage.onBackBtnClicked = function () {
    templatesPage.setPage();
}

selectedTemplatesPage.valueFromTopicSelected = function (iServerResponce) {
    if (iServerResponce.StatusCode === 0) {
        selectedTemplatesPage.SelectedTemplateFromServer = iServerResponce.RetObject;
        let deleteTemplateBtn = document.querySelector("#deleteTemplate-btn");
        deleteTemplateBtn.style.visibility = (Global_User_Data.IsAdmin || Global_User_Data.Email === selectedTemplatesPage.SelectedTemplateFromServer.UserIdentity) ? "visible" : "hidden";
        selectedTemplatesPage.isFavoriteTemplate = (selectedTemplatesPage.isTemplateInFavoriteList(Global_User_Data.FavoriteTemplates)) ? true : false;
        selectedTemplatesPage.onMarkFavoritBtnRes();
        selectedTemplatesPage.buildTemplatePartOfPage(selectedTemplatesPage.SelectedTemplateFromServer);
        selectedTemplatesPage.buildCommentsList(selectedTemplatesPage.SelectedTemplateFromServer.Comments);
    }
}

selectedTemplatesPage.isTemplateInFavoriteList = function (favoriteListString) {
    var selectedTemplate = templateHeaderDetails.MCategoryName + ":" + templateHeaderDetails.TemplateHeaderName;
    return favoriteListString.includes(selectedTemplate);
}

selectedTemplatesPage.buildCommentsList = function (commentsList) {
    if (commentsList && commentsList !== "") {
        var lastCommentsField = document.getElementById("lastComments-message");
        var commentsArray = commentsList.split("|");
        for (var i = 0; i < commentsArray.length; i++) {
            if (commentsArray[i] && commentsArray[i] !== "") {
                if (i === 0) {
                    lastCommentsField.value = commentsArray[i];
                }
                else {
                    lastCommentsField.value = lastCommentsField.value + "\n" + commentsArray[i];
                }
            }
        }
        
        selectedTemplatesPage.AddCommentField.value = "";
    }
}

selectedTemplatesPage.buildTemplatePartOfPage = function (templateDetails) {
    var replacementDiv = document.getElementById("replaceTempateContent");
    var valuesArray = templateDetails.Values;
    var lengthArray = valuesArray.length;

    for (var i = 0; i < lengthArray; i++) {
        var propertyDiv = document.createElement('div');
        propertyDiv.className = "property-div-wrapper";

        var propertyNameDiv = document.createElement('div');
        propertyNameDiv.className = "property-name-text";
        propertyNameDiv.innerText = valuesArray[i].Name + ":";
        propertyDiv.appendChild(propertyNameDiv);

        var inputwrapperDiv = document.createElement('div');
        inputwrapperDiv.className = "property-input-wrapper-div";

        var inputField = document.createElement("INPUT");
        inputField.setAttribute("type", "text");
        inputField.setAttribute("id", i);
        inputField.setAttribute("placeholder", "Insert value");
        inputwrapperDiv.appendChild(inputField);

        propertyDiv.appendChild(inputwrapperDiv);

        replacementDiv.appendChild(propertyDiv);
    }

    var submitButton = document.createElement('button');
    submitButton.innerText = "Show values in template";
    submitButton.addEventListener("click", function () {
        for (var i = 0; i < valuesArray.length; i++) {
            valuesArray[i].Value = document.getElementById(i).value;
        }

        Platform.GenerateHTMLTemplateWithValues(templateDetails.HeaderName, templateDetails.CategoryName, valuesArray, selectedTemplatesPage.showTemplateContent);
    });

    replacementDiv.appendChild(submitButton);
}

selectedTemplatesPage.showTemplateContent = function (iServerResponce) {
    if (iServerResponce.StatusCode === 0) {
        templateComtent = iServerResponce.RetObject;
        
        $("#showTemplateContent").html(templateComtent);
        var showContenttDiv = document.getElementById("showTemplateContent");

        var propertyDiv = document.createElement('div');
        propertyDiv.innerText = "Insert name to the word document file:  ";

        var inputField = document.createElement("INPUT");
        inputField.setAttribute("type", "text");
        inputField.setAttribute("id", "FileNameText");
        inputField.setAttribute("placeholder", "Insert value");
        inputField.setAttribute("required", "");
        propertyDiv.appendChild(inputField);

        showContenttDiv.appendChild(propertyDiv);

        var wordButton = document.createElement('button');
        wordButton.innerText = "Open template in word";
        wordButton.addEventListener("click", function () {
            nameOfWordFile = document.getElementById("FileNameText").value;;
            var fileNameText = document.getElementById("FileNameText").value;
            if (fileNameText && fileNameText !== "") {
                Platform.OpenTemplateInWord(nameOfWordFile, templateComtent.toString(), selectedTemplatesPage.OpenTemplateInWordRes);
            }
            else {
                alert("You must give name to the word file!!!");
            }
        });

        showContenttDiv.appendChild(wordButton);
    }
    else
    {
        alert(iServerResponce.RetObject);
    }
}

selectedTemplatesPage.onBackSelected = function (iEvent) {
    templatesPage.setPage();
}

selectedTemplatesPage.OpenTemplateInWordRes = function (iContent) {
    var fileNameText = document.getElementById("FileNameText").value;
    if (fileNameText && fileNameText !== "")
    {
        $.ajax({
            url: "/" + iContent.RetObject,
            method: 'Get',
            xhrFields: {
                responseType: 'blob'
            },
            success: function (data) {
                var a = document.createElement('a');
                var url = window.URL.createObjectURL(data);
                a.href = url;
                var fileName = iContent.RetObject.slice(iContent.RetObject.lastIndexOf(nameOfWordFile), iContent.RetObject.length);
                a.download = fileName;
                a.click();
                window.URL.revokeObjectURL(url);
            }
        });
    }
    else
    {
        alert("You must give name to the word file!!!");
    }
}

selectedTemplatesPage.onDeleteTemplateBtnClicked = function() {
    Platform.DeleteTemplate(templateHeaderDetails.MCategoryName,
        templateHeaderDetails.TemplateHeaderName,
        selectedTemplatesPage.onDeleteResponce);
}

selectedTemplatesPage.onDeleteResponce = function(iServerResponse) {
    if (iServerResponse.StatusCode === 0) {
        selectedTemplatesPage.onBackBtnClicked();
    } else {
        let serverResponce = document.querySelector(".server-error-response");
        serverResponce.innerHTML = iServerResponse.RetObject;
    }
}

selectedTemplatesPage.onStarBtnClickedRes = function (iContent) {
    
}