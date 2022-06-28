

var UserLoginValudation = function () {

    //To capture the form data

    var formData = new FormData();

    var formcontroldata = $("#formlogin").serializeArray();

    $.each(formcontroldata, function (i, field) {

        formData.append(field.name, field.value);

    });

    $("#btnLogin").val("Please wait..");

    //To make the ajax request

    $.ajax({

        url: $("#formlogin").attr('action'),

        type: $("#formlogin").attr('method'),

        data: formData,

        contentType: false, // Not to set any content header

        processData: false, // Not to process data

        dataType: 'json',

        cache: false,

        success: function (result) {

            if (result.status == 0) {

                $('#divShowMessage').show();

            } else {

                var role = result.role;

                if (role == "Admin") {

                    window.location = "@Url.Action("Index","Dashboard", new {area = "Admin"})";
                }
                else {
                    alert("No environment for you")

                    location.reload();
                }

            }

            $("#btnLogin").val("Login");

            $("#divmessage").html(result.message);

        }

    });

};
