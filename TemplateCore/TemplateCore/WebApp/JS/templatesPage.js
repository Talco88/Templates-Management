var templatesPage = {}
var Global_Template_BaseHTMLData = "";
var CategoryNameGlobal = undefined;
var favoriteTemplateString = "My-Favorite-Templates";

templatesPage.setPage = function (iCategoryName) {
    if (iCategoryName) {
        CategoryNameGlobal = iCategoryName;
    }
    templatesPage.categoryName = CategoryNameGlobal;
    if (Global_Template_BaseHTMLData === "") {
        templatesPage.LoadLobbyPageRes(true);
    }
    else {
        templatesPage.onPagedRecived();
    }
}

templatesPage.onPagedRecived = function () {
    $("#MainAppWindow").html("");
    setTimeout(function () {
        $("#MainAppWindow").html(Global_Template_BaseHTMLData);
        $(".dynamic-category-name").html(templatesPage.categoryName);
        if (templatesPage.categoryName === favoriteTemplateString) {
            templatesPage.isFavoriteTemplates = true;
            Platform.GetAllFavoriteTemplates(Global_User_Data.Email, templatesPage.onPagedResponce);
        }
        else {
            templatesPage.isFavoriteTemplates = false;
            Platform.GetAllTemplatesInCategory(templatesPage.categoryName, templatesPage.onPagedResponce);
        }
    }, 1);
}

templatesPage.onCreateTemplatetnBtn = function () {
    createTemplatesPage.setPage(CategoryNameGlobal);
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
        categoryNames = iServerResponce.RetObject;
        let createTemplatetnBtn = document.querySelector("#createNewTemplate-btn");
        if(templatesPage.isFavoriteTemplates)
        {
            createTemplatetnBtn.style.visibility = 'hidden';
        }
        else
        {
            createTemplatetnBtn.onclick = templatesPage.onCreateTemplatetnBtn;
            createTemplatetnBtn.style.visibility = 'visible';
        }
        
    }
    templatesPage.SetCategoryNames(categoryNames);
}

templatesPage.SetCategoryNames = function (iCategoryNames) {
    //let startsTemplate = '<input type="radio" id="star5" name="rating" value="5" disabled = true/><label class="full" for="star5" title="Awesome - 5 stars"></label><input type="radio" id="star4" name="rating" value="4" disabled = true/><label class="full" for="star4" title="Pretty good - 4 stars"></label><input type="radio" id="star3" name="rating" value="3" disabled = true/><label class="full" for="star3" title="Meh - 3 stars"></label><input type="radio" id="star2" name="rating" value="2" disabled = true/><label class="full" for="star2" title="Kinda bad - 2 stars"></label><input type="radio" id="star1" name="rating" value="1" disabled = true/><label class="full" for="star1" title="Sucks big time - 1 star"></label>';

    let categoryTitleContainer = document.getElementById("category_name");
    if (iCategoryNames != "" && iCategoryNames != null && iCategoryNames != undefined) {
        for (let i = 0; i < iCategoryNames.length; i++) {
            var wrapperDiv = document.createElement('div');
            wrapperDiv.className = 'template-property-wrapper';

            var topicDiv = document.createElement('div');
            topicDiv.className = 'template-property ' + iCategoryNames[i].HeadName;
            topicDiv.innerText = iCategoryNames[i].HeadName;
            topicDiv.id = iCategoryNames[i].Category;
            topicDiv.onclick = templatesPage.onTopicSelected;
            wrapperDiv.appendChild(topicDiv);

            var rateDiv = document.createElement('div');
            rateDiv.className = "server-rating";
            rateDiv.innerText = "Rate:" + iCategoryNames[i].Rate.toString() + "/5 ⭐";
            wrapperDiv.appendChild(rateDiv);

            categoryTitleContainer.appendChild(wrapperDiv);
        }
    }
}

templatesPage.onTopicSelected = function (iEvent) {
    var selectedTopicName = iEvent.target.className.substring(iEvent.target.classList[0].length + 1);
    var templateWrapper = {
        templateHeader: {
            MCategoryName: iEvent.target.id,
            TemplateHeaderName: selectedTopicName
        }
    }

    selectedTemplatesPage.setPage(templateWrapper);
}