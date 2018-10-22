var signIn = {};
var Global_singIn_BaseHTMLData = "";

signIn.setPage = function () {
    if (Global_singIn_BaseHTMLData === "") {
        signIn.LoadLobbyPageRes(true);
    }
    else {
        signIn.onPagedRecived();
    }
}

signIn.onPagedRecived = function () {
    $("#MainAppWindow").html(Global_singIn_BaseHTMLData);

    // all the btn registered here

    let registerBtn = document.querySelector("#register-btn");
    registerBtn.onclick = signIn.onRegisterClicked;

}

signIn.onRegisterClicked = function () {
    // do this when btn is clicked
    let fname = document.querySelector('#fname');
    let lname = document.querySelector('#lname');
    let email = document.querySelector('#email');
    let pass = document.querySelector('#password');
    Platform.RegisterUser(fname.value, lname.value, email.value, pass.value, signIn.onLoginResponce);
}
signIn.onLoginResponce = function (iData) {
    signIn.nevigateFromSignUpPage(iData);
}

signIn.nevigateFromSignUpPage = function (iServerReturn) {
    if (iServerReturn.Status != "OK") {
        //mainPage.setPage(); // Go to Main page
    }
    if (iServerReturn.StatusCode != 0) {
        //mainPage.setPage(); // Go to Main page
    }
    else {
        mainPage.setPage(); // Go again to Main page
    }
}

signIn.LoadLobbyPageRes = function (isSet) {
    $.ajax({
        url: "/WebApp/HTML/signIn.html",
        dataType: 'text',
        success: function (data) {
            Global_singIn_BaseHTMLData = data;
            signIn.onPagedRecived();
        },
        error: function () {
            alert("error Loading Lobby Page");
        }
    });
}

function validationUserInput() {
    //  var name = document.ContactForm.Name;
    var email = document.singnupForm.email;
    var gender = document.getElementsByName("gender");
    var password = document.singnupForm.password;
    var confirm = document.singnupForm.confirm;

    if (email.value.indexOf("@", 0) < 0 || email.value.indexOf(".", 0) < 0) {
        alert("Please enter a valid e-mail address.");
        email.focus();
        return false;
    }
    var okayGender = false;
    var okayHobbies = false;
    for (var i = 0, l = gender.length; i < l; i++) {
        if (gender[i].checked) {
            okayGender = true;
            break;
        }
    }
    if (!okayGender) {
        alert("gender please");
        return false;
    }
    if (password.value != confirm.value) {
        password.focus;
        confirm.focus;
        alert("password and confirm must be equal");
        return false
    }
    if (name.value == "") {
        window.alert("Please enter your name.");
        name.focus();
        return false;
    }
}