$(document).ready(function () {
    var WebUrl = 'http://localhost:5262/';

    //sign up page script
    function validateEmail($email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        return emailReg.test($email);
    }
    function SignUpValidation() {
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
                    if (data.ReturnMsg == "Success") {
                        localStorage.setItem('OTP', data.OTP);
                        window.location.href = 'http://localhost:5148/Otp'
                    }
                    else {
                        alert("Error");
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    alert('Error in Operation');
                }
            });
        }
    });

    //otp page script
    $('#BtnOtpSubmit').click(function () {
        $('.form-control').css('border-color', '');
        $('.validation').hide();
        if ($('#Otp').val() == '' || $('#Otp').val() == undefined) {
            $("#Otp")
                .after("<p class='text-danger validation'>Otp required</p>");
            $('#Otp').css('border-color', 'red');
            $('#Otp').focus();
            return;
        } else {
            if (localStorage.getItem('OTP') == $('#Otp').val()) {
                var data = {
                    Username: localStorage.getItem('Username'),
                    Password: localStorage.getItem('Password'),
                    Email: localStorage.getItem('Email')
                }
                $.ajax({
                    url: "/MVCMain/CreateUser",
                    method: "POST",
                    data: data,
                    success: function (data, textStatus, xhr) {
                        if (data == "Success") {
                            window.location.href = 'http://localhost:5148';
                            localStorage.clear();
                        } else if (data == "Duplicate") {
                            alert("This email is already in use");
                        }
                        else {
                            alert("Error");
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        alert('Error in Operation');
                    }
                });
            } else {
                alert("Enter correct OTP");
            }
        }
    });

    //sign in script

    function SignInValidation() {
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
        } else { return true; }
    }

    $('#BtnSignIn').click(function () {
        if (SignInValidation() == true) {
            var SignInData = {
                Username: $('#Username').val(),
                Password: $('#Password').val(),
            }

            //console.log(SignInData);
            //return;

            $.ajax({
                url: "/Authentication/Login",
                method: "POST",
                data: SignInData,
                success: function (data, textStatus, xhr) {
                    if (data == "Success") {
                        alert(1)
                    } else if (data == "Invalid") {
                        alert("Invalid username or password");
                    }
                    else {
                        alert("Error");
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    alert('Error in Operation');
                }
            });
        }
    });

});