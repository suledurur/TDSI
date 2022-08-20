$(document).ready(function () {

    $("#softwares").click(function () {

        //alanları gizleyip açma
        $("#software-area").removeClass("hide");
        $("#account-settings-area").addClass("hide");
        $("#follows-area").addClass("hide");
        $("#favorites-area").addClass("hide");
        $("#branches-area").addClass("hide");
        //tab aktif etme
        $("#softwares-tab").addClass("active");
        $("#account-settings-tab").removeClass("active");
        $("#follows-tab").removeClass("active");
        $("#favorites-tab").removeClass("active");
        $("#branches-tab").removeClass("active");
    });

    $("#account-settings").click(function () {

        //alanları gizleyip açma
        $("#software-area").addClass("hide");
        $("#account-settings-area").removeClass("hide");
        $("#follows-area").addClass("hide");
        $("#favorites-area").addClass("hide");
        $("#branches-area").addClass("hide");
        //tab aktif etme
        $("#softwares-tab").removeClass("active");
        $("#account-settings-tab").addClass("active");
        $("#follows-tab").removeClass("active");
        $("#favorites-tab").removeClass("active");
        $("#branches-tab").removeClass("active");
    });

    $("#follows").click(function () {

        //alanları gizleyip açma
        $("#software-area").addClass("hide");
        $("#account-settings-area").addClass("hide");
        $("#follows-area").removeClass("hide");
        $("#favorites-area").addClass("hide");
        $("#branches-area").addClass("hide");
        //tab aktif etme
        $("#softwares-tab").removeClass("active");
        $("#account-settings-tab").removeClass("active");
        $("#follows-tab").addClass("active");
        $("#favorites-tab").removeClass("active");
        $("#branches-tab").removeClass("active");
    });

    $("#favorites").click(function () {

        //alanları gizleyip açma
        $("#software-area").addClass("hide");
        $("#account-settings-area").addClass("hide");
        $("#follows-area").addClass("hide");
        $("#favorites-area").removeClass("hide");
        $("#branches-area").addClass("hide");
        //tab aktif etme
        $("#softwares-tab").removeClass("active");
        $("#account-settings-tab").removeClass("active");
        $("#follows-tab").removeClass("active");
        $("#favorites-tab").addClass("active");
        $("#branches-tab").removeClass("active");
    });

    $("#branches").click(function () {
        //alanları gizleyip açma
        $("#software-area").addClass("hide");
        $("#account-settings-area").addClass("hide");
        $("#follows-area").addClass("hide");
        $("#favorites-area").addClass("hide");
        $("#branches-area").removeClass("hide");
        //tab aktif etme
        $("#softwares-tab").removeClass("active");
        $("#account-settings-tab").removeClass("active");
        $("#follows-tab").removeClass("active");
        $("#favorites-tab").removeClass("active");
        $("#branches-tab").addClass("active");
    });

    $('#account-settings-area').find('input, textarea').on('keyup blur focus', function (e) {

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

    var close = document.getElementsByClassName("closebtn");
    var i;

    for (i = 0; i < close.length; i++) {
        close[i].onclick = function () {
            var div = this.parentElement;
            div.style.opacity = "0";
            setTimeout(function () { div.style.display = "none"; }, 1000);
        }
    }
});

