$(document).ready(function () {

    var btn = $("#SendMessage");

    btn.on("click",
        function() {
            alert("клік");
            console.log("click");
        });

    var info = $(".props li");
    info.on("click",
        function () {
            console.log("u clicked on" + $(this).text());
        });

    var $loginToggle = $("#loginToggle");
    var $popup = $(".popupform");
    $popup.hide();

    $loginToggle.on("click",function() {
        $popup.fadeToggle(500);
    })
});
