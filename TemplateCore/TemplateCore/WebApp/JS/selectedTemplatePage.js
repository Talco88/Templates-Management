var selectedTemplatesPage = {};
var templateHeaderDetails = {};
var templatePlaceHolders = [];
var Global_Template_BaseHTMLData = "";

selectedTemplatesPage.setPage = function (iTemplateWrapper) {
    templateHeaderDetails = iTemplateWrapper.templateHeader;
    templatePlaceHolders = iTemplateWrapper.templatePlaceHolders;

    if (Global_Template_BaseHTMLData === "") {
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
            Global_Template_BaseHTMLData = data;
            selectedTemplatesPage.onPagedRecived();
        },
        error: function () {
            alert("error Loading Template Page");
        }
    });
}

selectedTemplatesPage.onPagedRecived = function () {
    $("#MainAppWindow").html(Global_Template_BaseHTMLData);

    Platform.GetTemplate(templateHeaderDetails.MCategoryName, templateHeaderDetails.TemplateHeaderName, selectedTemplatesPage.valueFromTopicSelected);
}

selectedTemplatesPage.valueFromTopicSelected = function (iServerResponce) {
    if (iServerResponce.StatusCode === 0) {
        var replacementDiv = document.getElementById("replaceTempateContent");
        for (var i = 0; i < templatePlaceHolders.length; i++)
        {
            var propertyDiv = document.createElement('div');
            propertyDiv.innerText = templatePlaceHolders[i] + ":  ";
        
            var inputField = document.createElement("INPUT");
            inputField.setAttribute("type", "text");
            inputField.setAttribute("id", i);
            inputField.setAttribute("placeholder", "Insert value");
            propertyDiv.appendChild(inputField);
        
            replacementDiv.appendChild(propertyDiv);
        }

        var submitButton = document.createElement('button');
        submitButton.innerText = "Show values in template";
        submitButton.addEventListener("click", function () {
            var valuesArray = iServerResponce.RetObject.Values;
            for (var i = 0; i < valuesArray.length; i++) {
                valuesArray[i].Value = document.getElementById(i).value;
            }

            Platform.GenerateHTMLTemplateWithValues(iServerResponce.RetObject.HeaderName, iServerResponce.RetObject.CategoryName, valuesArray, selectedTemplatesPage.showTemplateContent);
        });

        replacementDiv.appendChild(submitButton);
    }
}

selectedTemplatesPage.showTemplateContent = function (iServerResponce) {
    if (iServerResponce.StatusCode === 0) {
        document.getElementById("showTemplateContent").innerText = iServerResponce.RetObject;
    }
    else
    {
        alert(iServerResponce.RetObject);
    }
}