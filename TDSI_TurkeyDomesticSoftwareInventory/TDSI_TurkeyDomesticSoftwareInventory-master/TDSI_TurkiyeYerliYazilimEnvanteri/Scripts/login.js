$(document).ready(function () {

    $('.form').find('input, textarea').on('keyup blur focus', function (e) {

        var $this = $(this),
            label = $this.prev('label');

        if (e.type === 'keyup') {
            if ($this.val() === '') {
                label.removeClass('active highlight');
            } else {
                label.addClass('active highlight');
            }
        } else if (e.type === 'blur') {
            if ($this.val() === '') {
                label.removeClass('active highlight');
            } else {
                label.removeClass('highlight');
            }
        } else if (e.type === 'focus') {

            if ($this.val() === '') {
                label.removeClass('highlight');
            }
            else if ($this.val() !== '') {
                label.addClass('highlight');
            }
        }

    });

    $('.tab a').on('click', function (e) {

        e.preventDefault();

        $(this).parent().addClass('active');
        $(this).parent().siblings().removeClass('active');

        target = $(this).attr('href');

        $('.tab-content > div').not(target).hide();

        $(target).fadeIn(600);

    });

    $("#individualTab").click(function () {
        $("#individualLogin").html('<div><textarea name="individualLogin" style = "display:none">1</textarea ></div>');
        $("#companyLogin").html('<div><textarea name="companyLogin" style = "display:none"></textarea ></div>');
    });

    $("#companyTab").click(function () {
        $("#individualLogin").html('<div><textarea name="companyLogin" style = "display:none"></textarea ></div>');
        $("#companyLogin").html('<div><textarea name="companyLogin" style = "display:none">1</textarea ></div>');
    });

    $("#frgtPwBtn").click(function () {
        $("#forgotPw").removeClass("divInactive");
        $("#login").addClass("divInactive");
    });
    $("#goToLogin").click(function () {
        $("#login").removeClass("divInactive");
        $("#forgotPw").addClass("divInactive");
    });
});
//şifre göster (giriş yap-göz)
function togglePW() {
    document.querySelector('.eye').classList.toggle('slash');
    var password = document.querySelector('[name=password]');
    if (password.getAttribute('type') === 'password') {
        password.setAttribute('type', 'text');
    } else {
        password.setAttribute('type', 'password');
    }
}

////şifre ve şifre(tekrar) kontrolü (bireysel)
//window.onload = function () {
//    document.getElementById("password").onchange = validatePassword;
//    document.getElementById("confirmpw").onchange = validatePassword;
//}
//function validatePasswordInd() {
//    var pass2 = document.getElementById("confirmpw").value;
//    var pass1 = document.getElementById("password").value;
//    if (pass1 != pass2)
//        document.getElementById("confirmpw").setCustomValidity("Şifreler eşleşmiyor!");
//    else
//        document.getElementById("confirmpw").setCustomValidity('');
//    //empty string means no validation error
//}

////şifre ve şifre(tekrar) kontrolü (kurumsal)
//window.onload = function () {
//    document.getElementById("passwordComp").onchange = validatePassword;
//    document.getElementById("confirmComp").onchange = validatePassword;
//}
//function validatePasswordCom() {
//    var pass2 = document.getElementById("confirmComp").value;
//    var pass1 = document.getElementById("passwordComp").value;
//    if (pass1 != pass2)
//        document.getElementById("confirmComp").setCustomValidity("Şifreler eşleşmiyor!");
//    else
//        document.getElementById("confirmComp").setCustomValidity('');
//    //empty string means no validation error
//}


//şifre kuralları(bireysel)

$(document).ready(function () {
    var validateInput = $('input.validate');

    function validateInputs() {
        var password = $('#password').val(),
            confirmpw = $('#confirmpw').val(),
            all_pass = true;

        var uppercase = password.match(/[A-Z]/),
            number = password.match(/[0-9]/);

        if (password.length < 8) {
            $('.password_length').removeClass('complete');
            all_pass = false;
        } else $('.password_length').addClass('complete');

        if (uppercase) $('.password_uppercase').addClass('complete');
        else {
            $('.password_uppercase').removeClass('complete');
            all_pass = false;
        }

        if (number) $('.password_number').addClass('complete');
        else {
            $('.password_number').removeClass('complete');
            all_pass = false
        }


        if (password == confirmpw && password != '') $('.password_match').addClass('complete');
        else {
            $('.password_match').removeClass('complete')
            all_pass = false;
        }
    }
    validateInput.each(validateInputs).on('keyup', validateInputs);
});


//şifre kuralları(kurumsal)

$(document).ready(function () {
    var validateInputComp = $('input.validate');

    function validateInputs() {
        var passwordComp = $('#passwordComp').val(),
            confirmComp = $('#confirmComp').val(),
            all_pass = true;

        var uppercase = password.match(/[A-Z]/),
            number = passwordC.match(/[0-9]/);

        if (password.length < 8) {
            $('.password_length_comp').removeClass('complete');
            all_pass = false;
        } else $('.password_length_comp').addClass('complete');

        if (uppercase) $('.password_uppercase_comp').addClass('complete');
        else {
            $('.password_uppercase_comp').removeClass('complete');
            all_pass = false;
        }

        if (number) $('.password_number_comp').addClass('complete');
        else {
            $('.password_number_comp').removeClass('complete');
            all_pass = false
        }


        if (passwordComp == confirmComp && passwordComp != '') $('.password_match_comp').addClass('complete');
        else {
            $('.password_match_comp').removeClass('complete')
            all_pass = false;
        }
    }
    validateInputComp.each(validateInputs).on('keyup', validateInputs);
});