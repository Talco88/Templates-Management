var createTemplatesPage = {};
var Global_Creat_Template_BaseHTMLData = "";

createTemplatesPage.setPage = function (iCategoryName) {
    if (iCategoryName) {
        createTemplatesPage.CategoryName = iCategoryName;
    }

    if (Global_Creat_Template_BaseHTMLData === "") {
        createTemplatesPage.LoadLobbyPageRes(true);
    }
    else {
        createTemplatesPage.onPagedRecived();
    }
}

createTemplatesPage.onPagedRecived = function () {
    $("#MainAppWindow").html("");
    setTimeout(function () {
        $("#MainAppWindow").html(Global_Creat_Template_BaseHTMLData);
        $(".dynamic-category-name").html(createTemplatesPage.CategoryName);

        let saveTemplatetnBtn = document.querySelector("#registerTemplate-btn");
        saveTemplatetnBtn.onclick = createTemplatesPage.onSaveTemplatetnBtn;

        let placeHolderBtn = document.querySelector("#InsertPlaceHolder-btn");
        placeHolderBtn.onclick = createTemplatesPage.onPlaceHolderBtnTemplate;
    }, 1);
}

createTemplatesPage.onPlaceHolderBtnTemplate = function () {
    let templateContent = document.getElementById("message");
    let templateContentValue = templateContent.value;
    let placeHolderNameValue = document.getElementById("placeHolderName").value;
    templateContentValue = templateContentValue + " $" + placeHolderNameValue + " ";
    templateContent.value = templateContentValue;
}

createTemplatesPage.onSaveTemplatetnBtn = function () {
    let templateContent = document.getElementById("message").value;
    let templateContentAfterReplace = templateContent.split("\n").join("</br>");
    console.log("templateContentAfterReplace: ", templateContentAfterReplace);
    let templateName = document.getElementById("templateName").value;
    if (templateName && templateName !== "") {
        let newTemplateContent = "{\"Template\": \"<p>" + templateContentAfterReplace + "</p>\", \"numberOfChanges\": 0}";
        Platform.CreateNewTemplate(newTemplateContent.toString(), templateName, createTemplatesPage.CategoryName, true, createTemplatesPage.onSaveTemplatetnBtnResponce);
    }
    else {
        alert("You must enter name to the new template");
    }
}

createTemplatesPage.onSaveTemplatetnBtnResponce = function (iServerResponce) {
    if (iServerResponce.StatusCode === 0) {
        templatesPage.setPage();
    }
    else {
        alert("Failed to save the new Template\n" + iServerResponce.RetObject);
    }
}

createTemplatesPage.LoadLobbyPageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/HTML/createTemplatePage.html",
        dataType: 'text',
        success: function (data) {
            Global_Creat_Template_BaseHTMLData = data;
            createTemplatesPage.onPagedRecived();
        },
        error: function () {
            alert("error Loading create Template Page");
        }
    });
}