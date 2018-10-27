var selectedTemplatesPage = {};
var templateHeaderDetails = {};
var Global_Template_BaseHTMLData = "";
var nameOfWordFile = "";

selectedTemplatesPage.setPage = function (iTemplateWrapper) {
    templateHeaderDetails = iTemplateWrapper.templateHeader;

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
        var valuesArray = iServerResponce.RetObject.Values;
        var lengthArray = valuesArray.length;

        for (var i = 0; i < lengthArray; i++)
        {
            var propertyDiv = document.createElement('div');
            propertyDiv.innerText = valuesArray[i].Name + ":  ";
        
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
        document.getElementById("showTemplateContent").innerText = iServerResponce.RetObject + "\n";
        var showContenttDiv = document.getElementById("showTemplateContent");

        var propertyDiv = document.createElement('div');
        propertyDiv.innerText = "File name:  ";

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
            Platform.OpenTemplateInWord(nameOfWordFile, "<p  align=\"right\" style=\"font-size:20px; color:green;\">אני <b>ושני </b>גדולים.</p><p align=\"left\" style=\"font-size:16px; color:blueViolet;\"><b>This text</b> is <b>bold or.</b></p><p align=\"left\" style=\"font-size:12px; color:blue; \">YESSSSSSS\nYOOOOOOOOOHOOOOOOOO</p>", selectedTemplatesPage.OpenTemplateInWordRes);
        });

        showContenttDiv.appendChild(wordButton);
    }
    else
    {
        alert(iServerResponce.RetObject);
    }
}

selectedTemplatesPage.OpenTemplateInWordRes = function (iContent) {
    var fileNameText = document.getElementById("FileNameText").value;
    console.log("fileNameText", fileNameText);
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