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
        saveTemplatetnBtn.onclick = createTemplatesPage.onSaveTemplatetnBtnTemplatetnBtn;
    }, 1);
}

createTemplatesPage.onSaveTemplatetnBtnTemplatetnBtn = function () {
    let templateContent = document.getElementById("message").value;
    let templateName = document.getElementById("templateName").value;
    if (templateName && templateName !== "") {
        let newTemplateContent = "{\"Template\": \"<p>" + templateContent + "</p>\", \"numberOfChanges\": 0}";
        Platform.CreateNewTemplate(newTemplateContent.toString(), templateName, createTemplatesPage.CategoryName, true, createTemplatesPage.onSaveTemplatetnBtnTemplatetnBtnResponce);
    }
    else {
        alert("You must enter name to the new template");
    }
}

createTemplatesPage.onSaveTemplatetnBtnTemplatetnBtnResponce = function (iServerResponce) {
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