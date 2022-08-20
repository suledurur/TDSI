$(document).ready(function () {

    $("#follow-button").click(function () {
        if ($("#follow-button").text() == "Takip Et") {

            $("#follow-button").css("color", "#FFF");
            $("#follow-button").css("background-color", "Tomato");
            $("#follow-button").css("border-color", "Tomato");
            $("#follow-button").text("Takibi Bırak");

        } else {
            $("#follow-button").text("Takip Et");
            $("#follow-button").css("color", "#FFFFFF");
            $("#follow-button").css("background-color", "cornflowerblue");
            $("#follow-button").css("border-color", "cornflowerblue");
        }
    });
});