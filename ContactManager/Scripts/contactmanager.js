$(document).ready(function () {

    $("#add-new-contact").unbind().on("click", function () {
        var url = "/Home/AddContact";
        window.location.href = url;
    });

    $(".edit-contact").unbind().on("click", function () {
        var contactID = $(this).closest("tr.contact-holder").find(".contact-id").text();
        contactID = parseInt(contactID);
        var url = "/Home/EditContact/" + contactID;
        window.location.href = url;
    });

    $(".delete-contact").unbind().on("click", function () {
        var contactID = $(this).closest("tr.contact-holder").find(".contact-id").text();
        var confirmDelete = confirm("Are you sure you want to Delete this record?");
        contactID = parseInt(contactID);

        if (confirmDelete) {
            DeleteContact(contactID);
        }
    });


    $("#add-contact").unbind().on("click", function () {
        var validate = ValidateContactValues();
        if (validate.isValidContact) {
            var model = validate.model;
            AddContact(model);
        }
    });

    $("#update-contact").unbind().on("click", function () {
        var validate = ValidateContactValues();
        var contactID = parseInt($("#contactID").text());
        validate.model.ID = contactID;

        if (validate.isValidContact) {
            var model = validate.model;
            UpdateContact(model);
        }
    });

    $(".close-error-block").unbind().on("click", function () {
        $(this).closest(".main-error-div").hide();
    });

    /*********Validation Calss************/
    var ValidateContactValues = function () {
        $(".error-div").hide();
        var isValidContact = true;
        var firstName = $("#firstname").val();
        var lastName = $("#lastname").val();
        var email = $("#email").val();
        var phonenumber = $("#phonenumber").val();
        var status = $("#status").val();

        if (!firstName) {
            ShowErrorMessage("First Name is required");
            isValidContact = false;
        } else if (!lastName) {
            ShowErrorMessage("Last Name is required");
            isValidContact = false;
        } else if (!email) {
            ShowErrorMessage("Email is required");
            isValidContact = false;
        } else if (!phonenumber) {
            ShowErrorMessage("Phone Number is required");
            isValidContact = false;
        }

        if (isValidContact) {
            //validate correct email format
            var isValidEmail = ValidateEmail(email);
            if (!isValidEmail) {
                ShowErrorMessage("Please enter valid Email");
                isValidContact = false;
            }
        }

        if (isValidContact) {
            //validate pone number is 10 digit
            var isValidPhoneNumber = ValidatePhoneNumber(phonenumber);

            if (!isValidPhoneNumber) {
                ShowErrorMessage("Please enter valid 10 digit Phone Number");
                isValidContact = false;
            }
        }

        return { isValidContact: isValidContact, model: { FirstName: firstName, LastName: lastName, Email: email, PhoneNumber: phonenumber, ContactStatus: status } };
    }

    var ValidateEmail = function (email) {
        var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return filter.test(email);
    }

    var ValidatePhoneNumber = function (phonenumber) {
        var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
        return phonenumber.match(phoneno);
    }

    var ShowErrorMessage = function (message) {
        $(".main-error-div .error-msg").html(message);
        $(".main-error-div").show();
    }

    /*****API Calls**********/
    var AddContact = function (model) {
        $.ajax({
            url: "/Contacts/AddContact",
            type: "POST",
            data: model,
            success: function (response) {
                if (response.success) {
                    var url = "/Home/Index";
                    window.location.href = url;
                } else {
                    ShowErrorMessage(response.message);
                }
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR.responseText);
                ShowErrorMessage("Adding contacts failed, please try again");
            }
        });
    }

    var DeleteContact = function (id) {
        $.ajax({
            url: "/Contacts/DeleteContact?Id=" + id,
            type: "DELETE",
            success: function (response) {
                if (response.success) {
                    var url = "/Home/Index";
                    window.location.href = url;
                } else {
                    ShowErrorMessage(response.message);
                }
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR.responseText);
                ShowErrorMessage("Deleting contacts failed, please try again");
            }
        });
    }


    var UpdateContact = function (model) {
        $.ajax({
            url: "/Contacts/UpdateContact",
            type: "PATCH",
            data: model,
            success: function (response) {
                if (response.success) {
                    var url = "/Home/Index";
                    window.location.href = url;
                } else {
                    ShowErrorMessage(response.message);
                }
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR.responseText);
                ShowErrorMessage("Updating contacts failed, please try again");
            }
        });
    }
});