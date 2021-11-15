//   all ------------------
function initTowhub() {
  "use strict";
  //   loader ------------------
  $(".loader-wrap").fadeOut(300, function () {
    $("#main").animate(
      {
        opacity: "1",
      },
      600
    );
  });

  //   lightGallery------------------
  $(".image-popup").lightGallery({
    selector: "this",
    cssEasing: "cubic-bezier(0.25, 0, 0.25, 1)",
    download: false,
    counter: false,
  });
  var o = $(".lightgallery"),
    p = o.data("looped");
  o.lightGallery({
    selector: ".lightgallery a.popup-image",
    cssEasing: "cubic-bezier(0.25, 0, 0.25, 1)",
    download: false,
    loop: false,
    counter: false,
  });
  function initHiddenGal() {
    $(".dynamic-gal").on("click", function () {
      var dynamicgal = eval($(this).attr("data-dynamicPath"));
      $(this).lightGallery({
        dynamic: true,
        dynamicEl: dynamicgal,
        download: false,
        loop: false,
        counter: false,
      });
    });
  }
  initHiddenGal();
}
//   Init All ------------------
$(document).ready(function () {
  initTowhub();
});
