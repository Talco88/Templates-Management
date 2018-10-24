var templatesPage = {}
var Global_Template_BaseHTMLData = "";
var iCategoryNameGlobal = undefined;
var birthDayFields = ["Dear", "Date", "Age", "From"];

templatesPage.setPage = function (iCategoryName) {
    iCategoryNameGlobal = iCategoryName;
    templatesPage.categoryName = iCategoryName;
    if (Global_Template_BaseHTMLData === "") {
        templatesPage.LoadLobbyPageRes(true);
    }
    else {
        templatesPage.onPagedRecived();
    }
}

templatesPage.onPagedRecived = function () {
    $("#MainAppWindow").html(Global_Template_BaseHTMLData);
    $(".dynamic-category-name").html(templatesPage.categoryName);
    Platform.GetTopicsInCategory(templatesPage.categoryName, templatesPage.onPagedResponce);
}

templatesPage.LoadLobbyPageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/HTML/templatesPage.html",
        dataType: 'text',
        success: function (data) {
            Global_Template_BaseHTMLData = data;
            templatesPage.onPagedRecived();
        },
        error: function () {
            alert("error Loading Lobby Page");
        }
    });
}

templatesPage.onPagedResponce = function (iServerResponce) {
    if (iServerResponce.StatusCode === 0) {
        //console.log(iServerResponce.RetObject);
        categoryNames = iServerResponce.RetObject;
    }
    templatesPage.SetCategoryNames(categoryNames);
}

templatesPage.SetCategoryNames = function (iCategoryName) {
    let categoryTitleContainer = document.getElementById("category_name");
    if (iCategoryName != "" && iCategoryName != null && iCategoryName != undefined) {
        for (let i = 0; i < iCategoryName.length; i++){
            var topicDiv = document.createElement('div');
            topicDiv.className = 'template-property ' + iCategoryName[i];
            topicDiv.innerText = iCategoryName[i];
            topicDiv.onclick = templatesPage.onTopicSelected;
            categoryTitleContainer.appendChild(topicDiv);
        }
    }
}

templatesPage.onTopicSelected = function (iEvent) {
    var selectedTopicName = iEvent.target.className.substring(iEvent.target.classList[0].length + 1);
    var fields = getTemplateFieldsFromTopicName(selectedTopicName);
    var templateWrapper = {
        templateHeader:{
            MCategoryName: iCategoryNameGlobal,
            TemplateHeaderName: selectedTopicName
        },
        templatePlaceHolders: fields
    }

    selectedTemplatesPage.setPage(templateWrapper);
}

function getTemplateFieldsFromTopicName(iTopicName)
{
    let retVal;
    switch(iTopicName)
    {
        case "יום הולדת":
            {
                retVal = birthDayFields;
                break;
            }
        default:
            {
                break;
            }
    }

    return retVal;
}