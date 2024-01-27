$(document).ready(function () {
    var UserID = localStorage.getItem('UserID');
    if (UserID == null) {
        window.location.href = 'http://localhost:5148/';
    }

    //getDepartment();
    getEmployee();
    getCertificate();

    //$("#ddlDepartment").change(function () {
    //    var status = this.value;
    //    alert(status);
    //});
    function getDepartment() {
        $.ajax({
            url: "/Department/GetAllDepartment",
            method: "GET",
            success: function (data, textStatus, xhr) {
                //console.log(data);
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
                var optionhtml1 = '';
                var optionhtml1 = '<option value="' +
                    0 + '">' + "--Select--" + '</option>';
                $("#ddlEmployee").append(optionhtml1);
                $.each(data, function (i) {
                    var optionhtml = '<option value="' +
                        data[i].empID
                        + '">' + data[i].empName + '</option>';
                    $("#ddlEmployee").append(optionhtml);
                });
            },
            error: function (xhr, textStatus, errorThrown) {
                alert('Error in Operation');
            }
        });
    }
    function getCertificate() {
        $.ajax({
            url: "/Certificate/GetAllCertificateType",
            method: "GET",
            success: function (data, textStatus, xhr) {
                //console.log(data);
                var optionhtml1 = '';
                var optionhtml1 = '<option value="' +
                    0 + '">' + "--Select--" + '</option>';
                $("#ddlCertificate").append(optionhtml1);
                $.each(data, function (i) {
                    var optionhtml = '<option value="' +
                        data[i].certificateID + '">' + data[i].certificateType
                        + '</option>';
                    $("#ddlCertificate").append(optionhtml);
                });
            },
            error: function (xhr, textStatus, errorThrown) {
                alert('Error in Operation');
            }
        });
    }

    function CertificatePageValidation() {
        $('.form-select').css('border-color', '');
        $('.validation').hide();
        if ($('#ddlEmployee').val() == 0 || $('#ddlEmployee').val() == undefined) {
            $("#ddlEmployee")
                .after("<p class='text-danger validation'>Select employee</p>");
            $('#ddlEmployee').css('border-color', 'red');
            $('#ddlEmployee').focus();
            return false;
        }
        else if ($('#ddlCertificate').val() == 0 || $('#ddlCertificate').val() == undefined) {
            $("#ddlCertificate")
                .after("<p class='text-danger validation'>Select Certificate</p>");
            $('#ddlCertificate').css('border-color', 'red');
            $('#ddlCertificate').focus();
            return false;
        } else { return true; }
    }

    $('#BtnGenCertificate').click(function () {
        if (CertificatePageValidation() == true) {
            var CertificateID = $('#ddlCertificate').val();
            var EmpID = $('#ddlEmployee').val();
            if (CertificateID == 1) {
                window.open(
                    '/Reporting/NOCReport/' +
                    'EmpID=' + EmpID
                );

            } else {
                window.open(
                    '/Reporting/ReleaseReport/' +
                    'EmpID=' + EmpID
                );
            }
        }
    });

});