

function GetAppDate() {

    var date = $('#txtAppointmentDate').val().split("-");

    day = date[2];

    month = date[1];

    year = date[0];

    var appdate = (day + " / " + month + " / " + year)

    console.log(appdate)

    $("#txtAppDate").text(appdate);
}

function GetTimeSlotId() {


    var timeslotid = $('[id*="txtTime"]:checked').map(function () { return $(this).val().toString(); }).get().join(",");

    console.log(timeslotid);



    $.get("/Appointment/GetSlotById/?Id=" + timeslotid, function (data, status) {

        console.log(data);

        if (data.data == false) {
            /*  alert("Does not exist");*/
        } else {

            $("#txtId").val(data.data.id);
            $("#txtTimeSlotName").text(data.data.timeSlot);
        }

    });

}


function stkSelected() {

    document.getElementById('divPaybill').style.display = 'none';

    document.getElementById('divSTK').style.display = 'block';
}

function showInvoice() {

    document.getElementById('divmessage').style.display = 'none';

    document.getElementById('dvinvoice').style.display = 'block';
}


function payBillSelected() {

    document.getElementById('divSTK').style.display = 'none';

    document.getElementById('divPaybill').style.display = 'block';

    document.getElementById("errorMsg").style.display = "none";

}

function ShowLoader() {

    $("#loadMe").modal('show');
}

function HideLoader() {

    $("#loadMe").modal('hide');
}


function SendStkPush() {

    if ($('#txtPhoneNumber').val() == '') {
        $('#txtPhoneNumber').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Please enter mpesa Phone Number",
            showConfirmButton: true,
        });
        return false;
    }


    $("#loadMe").modal('show');

    var phoneNumber = document.getElementById("txtPhoneNumber").value;

    var link = "/Appointment/MpesaSTKPush?PhoneNumber=" + phoneNumber;

    console.log(link);

    $.ajax({

        type: "POST",

        url: link,


        success: function (response) {

            console.log(response);

            if (response.success == true) {

                $("#loadMe").modal('hide');

                document.getElementById("successMsg").style.display = "block";

                $("#showsuccess").text(response.responseText);

            }

            else {

                $("#loadMe").modal('hide');

                document.getElementById("errorMsg").style.display = "block";

                $("#showError").text(response.responseText);
            }



        },

        error: function (response) {
            alert("error!");
        },
        complete: function () {
            HideLoader();
        }
    })


}



function SubmitAppointment() {


    if ($('#txtTransactionNumber').val() == '') {
        $('#txtTransactionNumber').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Mpesa Transaction Number is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtFirstName').val() == '') {
        $('#txtFirstName').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "First Name is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtLastName').val() == '') {
        $('#txtLastName').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Last Name is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtEmail').val() == '') {
        $('#txtEmail').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Email is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtPhoneNumber').val() == '') {
        $('#txtPhoneNumber').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Phone Number is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    var data = $("#frmSubAppointment").serialize();

    $.ajax({

        type: "POST",

        url: "/Appointment/Create/",

        data: data,   

        success: function (response) {

            if (response.success) {

                var appointmentId = response.responseText;

                console.log(appointmentId);

                setTimeout(function () { window.location = "/Appointment/Receipt/" + appointmentId; }, 5);

                //swal({

                //    position: 'top-end',

                //    type: "success",

                //    title: response.responseText,

                //    showConfirmButton: false,

                //}), setTimeout(function () { window.location = "/Appointment/Invoice/"; }, 3000);

            } else {

                swal({

                    position: 'top-end',

                    type: "error",

                    title: response.responseText,

                    showConfirmButton: true,

                    timer: 5000,
                });


            }
        },

        error: function (response) {
            alert("error!");
        },
        complete: function () {
            HideLoader();
        }
    })

}









//wizard
var currentTab = 0;

document.addEventListener("DOMContentLoaded", function (event) {


    showTab(currentTab);

});

function showTab(n) {
    var x = document.getElementsByClassName("tab");
    x[n].style.display = "block";
    if (n == 0) {
        document.getElementById("prevBtn").style.display = "none";
    } else {
        document.getElementById("prevBtn").style.display = "inline";
    }
    if (n == (x.length - 1)) {
        document.getElementById("nextBtn").innerHTML = "Submit";


    } else {
        document.getElementById("nextBtn").innerHTML = "Next";
    }
    fixStepIndicator(n)
}

function nextPrev(n) {
    var x = document.getElementsByClassName("tab");
    if (n == 1 && !validateForm()) return false;
    x[currentTab].style.display = "none";
    currentTab = currentTab + n;
    if (currentTab >= x.length) {
        // document.getElementById("frmSubAppointment").submit();
        // return false;
        //alert("sdf");
        document.getElementById("nextprevious").style.display = "none";

        document.getElementById("all-steps").style.display = "none";

        document.getElementById("register").style.display = "none";

        document.getElementById("text-message").style.display = "none";

        SubmitAppointment();
    }
    showTab(currentTab);
}

function validateForm() {
    var x, y, i, valid = true;
    x = document.getElementsByClassName("tab");
    y = x[currentTab].getElementsByTagName("input");
    for (i = 0; i < y.length; i++) {
        if (y[i].value == "") {
            y[i].className += " invalid";
            valid = false;
        }
    }
    if (valid) { document.getElementsByClassName("step")[currentTab].className += " finish"; }
    return valid;
}

function fixStepIndicator(n) {
    var i, x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) { x[i].className = x[i].className.replace(" active", ""); }
    x[n].className += " active";
}


// the selector will match all input controls of type :checkbox
// and attach a click event handler
$("input:checkbox").on('click', function () {
    // in the handler, 'this' refers to the box clicked on
    var $box = $(this);
    if ($box.is(":checked")) {
        // the name of the box is retrieved using the .attr() method
        // as it is assumed and expected to be immutable
        var group = "input:checkbox[name='" + $box.attr("name") + "']";
        // the checked state of the group/box on the other hand will change
        // and the current value is retrieved using .prop() method
        $(group).prop("checked", false);
        $box.prop("checked", true);
    } else {
        $box.prop("checked", false);
    }
});



$(document).ready(function () {

    var ckbox = $("input[name='Time']");
    var chkId = '';
    $('input').on('click', function () {

        if (ckbox.is(':checked')) {
            $("input[name='Time']:checked").each(function () {
                chkId = $(this).val() + ",";
                chkId = chkId.slice(0, -1);
            });

            //alert($(this).val()); // return all values of checkboxes checked
            //alert(chkId); // return value of checkbox checked
            console.log(chkId);
        }
    });

});


function ApproveAppointment(e) {

    var id = e;

    swal(
        {
            title: "Your are about to approve this appointment, are you sure?",

            //text: "Deac!",

            type: "success",

            showCancelButton: true,

            confirmButtonColor: "##62b76e",

            confirmButtonText: "Yes, Approve!",

            closeOnConfirm: false
        },

        function () {

            $.ajax({

                type: "GET",

                url: "/Admin/Appointments/ApproveAppoinment/" + id,

                success: function (response) {

                    if (response.success) {

                        swal({

                            position: 'top-end',

                            type: "success",

                            title: response.responseText,

                            showConfirmButton: false,

                        });
                        setTimeout(function () { location.reload(); }, 3000);

                    }

                    else {
                        swal({
                            position: 'top-end',
                            type: "error",
                            title: response.responseText,
                            showConfirmButton: true,
                            timer: 5000,
                        });

                    }

                },
                error: function (response) {

                    console.log(response);
                    swal({
                        position: 'top-end',
                        type: "error",
                        title: "Server error ,kindly contact the admin for assistance",
                        showConfirmButton: false,
                        timer: 5000,
                    });

                }

            })

        });
}