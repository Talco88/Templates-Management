var templatesPage = {}
var Global_index_BaseHTMLData = "";

templatesPage.setPage = function () {
    templatesPage.categoryName = "";
    if (Global_index_BaseHTMLData === "") {
        templatesPage.LoadLobbyPageRes(true);
    }
    else {
        templatesPage.onPagedRecived();
    }
}

templatesPage.onPagedRecived = function () {
    $("#MainAppWindow").html(Global_index_BaseHTMLData);

    // all the btn registered here

    //let loginBtn = document.querySelector(".login-btn");
    //loginBtn.onclick = mainPage.onLoginClicked;

    //let signinBtn = document.querySelector(".signin-btn");
    //signinBtn.onclick = mainPage.onSignUpClicked;

    ////let logoutBtn = document.querySelector(".logout-btn");
    ////logoutBtn.onclick = mainPage.onLogOutClicked;

    //let birthdayBtn = document.querySelector("#birthdayBtn");
    //birthdayBtn.onclick = mainPage.onCategoryClicked;

    Platform.GetTopicsInCategory(templatesPage.onPagedResponce);

}

templatesPage.LoadLobbyPageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/HTML/templatesPage.html",
        dataType: 'text',
        success: function (data) {
            Global_index_BaseHTMLData = data;
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