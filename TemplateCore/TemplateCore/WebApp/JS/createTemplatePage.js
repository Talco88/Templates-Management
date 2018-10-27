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
        $(".dynamic-category-name").html(createTemplatesPage.categoryName);
    }, 1);
}

createTemplatesPage.LoadLobbyPageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/HTML/createTemplatesPage.html",
        dataType: 'text',
        success: function (data) {
            Global_Creat_Template_BaseHTMLData = data;
            createTemplatesPage.onPagedRecived();
        },
        error: function () {
            alert("error Loading Lobby Page");
        }
    });
}