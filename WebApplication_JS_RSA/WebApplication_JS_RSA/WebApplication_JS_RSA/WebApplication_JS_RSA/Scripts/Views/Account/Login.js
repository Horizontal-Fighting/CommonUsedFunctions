var login = (
    function ($) {
        $(document).ready(
            function () {
                $('#LoginButton').click(function () {
                    var publicKey = $('#PublicKey').data("val");
                    var plainpassword = $('#passwordTextBox').val();
                    var AccountLoginURL = $('#AccountLoginURL').data("url");
                    var encryptedPassword;
                    
                    var formSelector = "#loginForm";
                    var form = $(formSelector);

                    form.validate();
                    var isFormValid = form.valid();

                    //encrypt password
                    if (plainpassword !== null && plainpassword !== "") {
                        var crypt = new JSEncrypt();
                        crypt.setPublicKey(publicKey);
                        encryptedPassword = crypt.encrypt(plainpassword);
                        console.log(encryptedPassword);
                        $('#passwordTextBox').val(encryptedPassword);
                    }
                    
                    debugger;
                    if (isFormValid) {
                        //blockUI
                        //showSpinner();
                        $.ajax({
                            type: "POST",
                            url: AccountLoginURL,
                            data: form.serialize(),
                            success: function (data, textStatus, jqXHR) {
                                if (data.RedirectUrl !== null) {
                                    window.location.href = data.RedirectUrl;
                                }
                                else {
                                    $('#errorMsg').text(data.ErrorMessage);
                                }
                            },
                            error: function (jqXhr, textStatus, errorThrown) {
                                console.log('error: ' + jqXhr.responseText);
                            },
                            complete: function (jqXHR, textStatus) {
                                //hideSpinner();
                            }
                        });
                    }
                });
            });
    }(jQuery));

