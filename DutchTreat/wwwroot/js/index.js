$(document).ready(function () {

    var btn = $("#send");

    if ($('#messageSentAlert').text().length != 0) {
        console.log("I'm in!");
        $('#messageSentAlert').fadeIn();
    }

    var info = $(".props li");
    info.on("click",
        function () {
            console.log("u clicked on" + $(this).text());
        });

    var $loginToggle = $("#loginToggle");
    var $popup = $(".popupform");
    $popup.hide();

    $loginToggle.on("click", function () {
        $popup.fadeToggle(500);
    })
});
