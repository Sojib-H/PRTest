$(document).ready(function () {
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
    //sign up save 
    $('#BtnSignUp').click(function () {
        if (SignUpValidation() == true) {

            var SignUpData = {
                Username: $('#Username').val(),
                Password: $('#Password').val(),
                Email: $('#Email').val()
            }

            $.ajax({
                url: "/Authentication/SignUp",
                method: "POST",
                data: SignUpData,
                success: function (data, textStatus, xhr) {
                    if (data == "Success") {
                        window.location.href = 'http://localhost:5148/'
                    } else if (data == "Duplicate") {
                        alert("This email or username already in use");
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

            $.ajax({
                url: "/Authentication/Login",
                method: "POST",
                data: SignInData,
                success: function (data, textStatus, xhr) {
                    //console.log(data);
                    if (data.returnMsg == "Success") {
                        window.location.href = 'http://localhost:5148/otp';
                        localStorage.setItem("Email", data.email);
                    } else if (data.returnMsg == "Invalid") {
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
            var OTPData = {
                OTP: $('#Otp').val(),
                Email: localStorage.getItem("Email"),
            }
            $.ajax({
                url: "/Authentication/CheckOTP",
                method: "POST",
                data: OTPData,
                success: function (data, textStatus, xhr) {
                    console.log(data);
                    if (data.returnMsg == "Success") {
                        localStorage.clear();
                        localStorage.setItem("UserID", data.otp);
                        window.location.href = 'http://localhost:5148/home';
                    } else if (data.returnMsg == "Invalid") {
                        alert("Incorrect OTP");
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

    $('#BtnGoBack').click(function () {
        localStorage.clear();
        window.location.href = 'http://localhost:5148/';
    });

    $("#Otp").keypress(function (e) {
        // 46 is a period
        if (e.which != 46 && e.which != 8 && (e.which < 48 || e.which > 57)) {
            return (false);
        }
    });
});