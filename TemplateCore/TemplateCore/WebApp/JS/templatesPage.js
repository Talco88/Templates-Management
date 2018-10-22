var templatesPage = {}
var Global_Template_BaseHTMLData = "";

templatesPage.setPage = function (iCategoryName) {
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
    Platform.GetTopicsInCategory(templatesPage.onPagedResponce);

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
    let categoryName = "";
    if (iServerResponce.StatusCode === 0) {
        categoryName = iServerResponce.RetObject.FirstName;
    }
    templatesPage.SetCategoryName(categoryName);
}

templatesPage.SetCategoryName = function (iCategoryName) {
    if (iCategoryName != "") {
        templatesPage.categoryName = iCategoryName;
    }
    document.getElementById("category_name").textContent = iCategoryName;
}