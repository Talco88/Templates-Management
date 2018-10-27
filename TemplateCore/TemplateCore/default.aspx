<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="TemplateCore._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%-- SCRIPTS --%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="WebApp/JS/Platform.js"></script>
    <script src="WebApp/JS/Utill.js"></script>
    <script src="WebApp/JS/MainPage.js"></script>
    <script src="WebApp/JS/SignIn.js"></script>
    <script src="WebApp/JS/templatesPage.js"></script>
    <script src="WebApp/JS/selectedTemplatePage.js"></script>

<%--     CSS --%>
    <link href="WebApp/CSS/creative.css" rel="stylesheet" />
    <link href="WebApp/CSS/creative.min.css" rel="stylesheet" />
    <link href="WebApp/CSS/LoadingPopup.css" rel="stylesheet" />
    <title>Template Core</title>
</head>
<body>
    <div id="loadingPopUp" class="popup-page hide-div"></div>
    <div id="MainAppWindow" class="app-main">
    </div>
    <script>
        mainPage.setPage();
    </script>
</body>
</html>
