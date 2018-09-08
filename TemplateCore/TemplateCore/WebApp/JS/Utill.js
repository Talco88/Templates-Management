var Utill = {};

Util.BuildFromFromTemplate = function (iContainerDiv, iTemplate) {
    
    for (var i = 0; i < iTemplate.Propertys.length; i++) {
        var propertyDiv = document.createElement('div');
        propertyDiv.className = 'template-property';
        propertyDiv.innerText = iTemplate.Propertys[i].Name;

        iContainerDiv.appendChild(propertyDiv);
    }
    
    return iContainerDiv;
}

Utill.LoadPage = function (iPageName) {

}