$(document).ready(function () {
    var UserID = localStorage.getItem('UserID');
    if (UserID == null) {
        window.location.href = 'http://localhost:5148/';
    }
    getEmployee();
    var reader = '';


    $('#FileUpload').change(function () {
        var file = document.getElementById("FileUpload").files[0];
        reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = function () {
            var url = reader.result;
            //get index to 1nd comma
            var str = url.split(',').slice(0, 1).join(',') + ','
            // We use slice for this (data:image/png;base64,) drop
            document.getElementById("uploadImageValue").value = url.slice(str.length)
        }
    })

    function CertificateInfoPageValidation() {
        $('.form-control').css('border-color', '');
        $('.form-select').css('border-color', '');
        $('.validation').hide();
        if ($('#ddlEmployee').val() == 0 || $('#ddlEmployee').val() == undefined) {
            $("#ddlEmployee")
                .after("<p class='text-danger validation'>Select employee</p>");
            $('#ddlEmployee').css('border-color', 'red');
            $('#ddlEmployee').focus();
            return false;
        } else if ($('#uploadImageValue').val() == "" || $('#uploadImageValue').val() == undefined) {
            $("#FileUpload")
                .after("<p class='text-danger validation'>PDF required</p>");
            $('#FileUpload').css('border-color', 'red');
            $('#FileUpload').focus();
            return false;
        } else { return true; }
    }

    $('#BtnCertificateInfo').click(function () {
        if (CertificateInfoPageValidation() == true) {
            var CertificateInfoData = {
                EmpID: $('#ddlEmployee').val(),
                Certificate: $('#uploadImageValue').val(),
            }
            $.ajax({
                url: "/Certificate/AddEmployeeCertificate",
                method: "POST",
                data: CertificateInfoData,
                success: function (data, textStatus, xhr) {
                    if (data == "Success") {
                        alert("Data saved successfully");
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

    function getEmployee() {
        var EmployeeData = {
            CreateBy: UserID,
        }

        $.ajax({
            url: "/Employee/GetEmployee",
            method: "POST",
            data: EmployeeData,
            success: function (data, textStatus, xhr) {
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



    //$('#FileUpload').change(function (event) {
    //    reader = new FileReader();
    //    reader.readAsDataURL(event.target.files[0]);
    //    reader.onload = function () {
    //        return reader.result;
    //    }
    //})
});