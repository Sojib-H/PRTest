$(document).ready(function () {
    var UserID = localStorage.getItem('UserID');
    if (UserID == null) {
        window.location.href = 'http://localhost:5148/';
    }
    getDepartment();
    getEmployee();
    function getDepartment() {
        $.ajax({
            url: "/Department/GetAllDepartment",
            method: "GET",
            success: function (data, textStatus, xhr) {
                var optionhtml1 = '';
                var optionhtml1 = '<option value="' +
                    0 + '">' + "--Select--" + '</option>';
                $("#ddlDepartment").append(optionhtml1);
                $.each(data, function (i) {
                    var optionhtml = '<option value="' +
                        data[i].departmentID + '">' + data[i].departmentName + '</option>';
                    $("#ddlDepartment").append(optionhtml);
                });
            },
            error: function (xhr, textStatus, errorThrown) {
                alert('Error in Operation');
            }
        });
    }
    function getEmployee() {
        var EmployeeData = {
            CreateBy: UserID,
        }

        $.ajax({
            url: "/Employee/GetEmployee",
            method: "POST",
            data: EmployeeData,
            success: function (data, textStatus, xhr) {
                //console.log(data);
            },
            error: function (xhr, textStatus, errorThrown) {
                alert('Error in Operation');
            }
        });
    }
    function validateEmail($email) {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        return emailReg.test($email);
    }

    $("#Mobile").keypress(function (e) {
        // 46 is a period
        if (e.which != 46 && e.which != 8 && (e.which < 48 || e.which > 57)) {
            return (false);
        }
    });
    function EmployeeValidation() {
        $('.form-control').css('border-color', '');
        $('.form-select').css('border-color', '');
        $('.validation').hide();
        if ($('#EmpName').val() == '' || $('#EmpName').val() == undefined) {
            $("#EmpName")
                .after("<p class='text-danger validation'>Name required</p>");
            $('#EmpName').css('border-color', 'red');
            $('#EmpName').focus();
            return false;
        }
        else if ($('#Mobile').val() == '' || $('#Mobile').val() == undefined) {
            $("#Mobile")
                .after("<p class='text-danger validation'>Mobile required</p>");
            $('#Mobile').css('border-color', 'red');
            $('#Mobile').focus();
            return false;
        } else if ($('#Email').val() == '' || $('#Email').val() == undefined) {
            $("#Email")
                .after("<p class='text-danger validation'>Email required</p>");
            $('#Email').css('border-color', 'red');
            $('#Email').focus();
            return false;
        } else if (!validateEmail($('#Email').val())) {
            $("#Email")
                .after("<p class='text-danger validation'>Enter valid email</p>");
            $('#Email').css('border-color', 'red');
            $('#Email').focus();
            return false;
        } else if ($('#ddlDepartment').val() == 0 || $('#ddlDepartment').val() == undefined) {
            $("#ddlDepartment")
                .after("<p class='text-danger validation'>Select department</p>");
            $('#ddlDepartment').css('border-color', 'red');
            $('#ddlDepartment').focus();
            return false;
        } else { return true; }
    }
    $('#AddEmployee').click(function () {
        if (EmployeeValidation() == true) {
            var EmployeeData = {
                EmpName: $('#EmpName').val(),
                Mobile: $('#Mobile').val(),
                Email: $('#Email').val(),
                DepartmentID: $('#ddlDepartment').val(),
                CreateBy: UserID,
            }
            $.ajax({
                url: "/Employee/AddEmployee",
                method: "POST",
                data: EmployeeData,
                success: function (data, textStatus, xhr) {
                    if (data == "Success") {
                        alert("Successfully data stored")
                    } else if (data == "Duplicate") {
                        alert("This email or mobile already in use");
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