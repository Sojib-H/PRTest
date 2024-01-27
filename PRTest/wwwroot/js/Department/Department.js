$(document).ready(function () {
    var UserID = localStorage.getItem('UserID');
    if (UserID == null) {
        window.location.href = 'http://localhost:5148/';
    }

    alert("Department");
    $.ajax({
        url: "/Department/GetAllDepartment",
        method: "GET",
        success: function (data, textStatus, xhr) {
            console.log(data);
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error in Operation');
        }
    });

});