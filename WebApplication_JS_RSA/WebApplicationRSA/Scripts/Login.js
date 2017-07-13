$(function () {
    // Execute when they click the button.
    $('#login').click(function () {

        // Create the encryption object.
        var crypt = new JSEncrypt();

        // If no public key is set then set it here...
        var publicKey = $('#PublicKey').text();
        var antiForgeryToken = $("input[name = '__RequestVerificationToken']").val();
        var password = $('#passwordTextBox').val();
        var userName = $('#userNameTextBox').val();
        var AccountLoginURL = $('#AccountLoginURL').val();
        var encryptedPassword;

        //debugger;

        crypt.setPublicKey(publicKey);

        encryptedPassword = crypt.encrypt(password);
        console.log(encryptedPassword);

        var LoginModel = {
            _RequestVerificationToken: antiForgeryToken, //Reading text box values using Jquery   
            email: userName,
            plainTextPassword: password,
            encryptedPassword: encryptedPassword,
            rememberMe: 'false'
        };
        LoginModel.__RequestVerificationToken = antiForgeryToken;

        //debugger;
        $.ajax({
            type: "POST",
            url: AccountLoginURL,
            data: LoginModel,
            success:  function (result) {
                console.log('succeed:' + result);
            },
            //contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            error: function (err) {
                alert("error - " + err);
            }
        });

        
    });


  

})