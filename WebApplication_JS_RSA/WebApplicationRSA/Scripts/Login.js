$(function () {
    $('#login').click(function () {
        var crypt = new JSEncrypt();

        var publicKey = $('#PublicKey').text();
        var antiForgeryToken = $("input[name = '__RequestVerificationToken']").val();
        var password = $('#passwordTextBox').val();
        var userName = $('#userNameTextBox').val();
        var AccountLoginURL = $('#AccountLoginURL').val();
        var encryptedPassword;

        crypt.setPublicKey(publicKey);

        encryptedPassword = crypt.encrypt(password);
        console.log(encryptedPassword);

        //data to be transported
        var LoginModel = {
            _RequestVerificationToken: antiForgeryToken, //Reading text box values using Jquery   
            email: userName,
            plainTextPassword: password,
            encryptedPassword: encryptedPassword,
            rememberMe: 'false'
        };
        //add antiForgeryToken
        LoginModel.__RequestVerificationToken = antiForgeryToken;

        $.ajax({
            type: "POST",
            url: AccountLoginURL,
            data: LoginModel,
            success:  function (result) {
                console.log('succeed:' + result);
            },
            dataType: 'json',
            error: function (err) {
                alert("error - " + err);
            }
        });

    });


  

})