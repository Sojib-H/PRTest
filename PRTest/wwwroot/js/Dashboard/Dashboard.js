$(document).ready(function () {
    var UserID = localStorage.getItem('UserID');
    if (UserID == null) {
        window.location.href = 'http://localhost:5148/';
    }

    $('#BtnSignOut').click(function () {
        localStorage.clear();
        window.location.href = 'http://localhost:5148/';
    });
    //alert("Home")
});