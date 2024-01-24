$(document).ready(function () {
    var WebUrl = 'http://localhost:5262/';
    $('#BtnSignUp').click(function () {
        if (SignUpValidation() == true) {
            localStorage.setItem('Username', $('#Username').val());
            localStorage.setItem('Password', $('#Password').val());
            localStorage.setItem('Email', $('#Email').val());

            //Make Ajax request with the contentType = false, and procesDate = false
            var ajaxRequest = $.ajax({
                type: "Get",
                url: WebUrl + "api/OTP/GenerateOTP/" + $('#Email').val(),
                contentType: 'json',
                success: function (data, textStatus, xhr) {
                    if (data == "Success") {
                        
                    }
                    else {
                        alert("Error");
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    alert('Error in Operation');
                }
            });

            //ajaxRequest.done(function (xhr, textStatus) {
            //    alert('Data Update successfully');
            //});
            //console.log(RegistrationDataArray);
        }
    });

    function validateEmail($email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        return emailReg.test($email);
    }
    function SignUpValidation() {
        $('.form-control').css('border-color', '');
        $('.validation').hide();
        $('.form-control').css('border-color', '');
        $('.validation').hide();
        if ($('#Username').val() == '' || $('#Username').val() == undefined) {
            $("#Username")
                .after("<p class='text-danger validation'>Username required</p>");
            $('#Username').css('border-color', 'red');
            $('#Username').focus();
            return false;
        }
        else if ($('#Password').val() == '' || $('#Password').val() == undefined) {
            $("#Password")
                .after("<p class='text-danger validation'>Password required</p>");
            $('#Password').css('border-color', 'red');
            $('#Password').focus();
            return false;
        }
        else if ($('#Email').val() == '' || $('#Email').val() == undefined) {
            $("#Email")
                .after("<p class='text-danger validation'>Email required</p>");
            $('#Email').css('border-color', 'red');
            $('#Email').focus();
            return false;
        }
        else if (!validateEmail($('#Email').val())) {
            $("#Email")
                .after("<p class='text-danger validation'>Enter valid email</p>");
            $('#Email').css('border-color', 'red');
            $('#Email').focus();
            return false;
        } else { return true; }
    }







});