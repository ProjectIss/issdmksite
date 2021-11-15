$(document).ready(function () {
  onlyNumber();
  onlyChar();

  $("#phone_no").change(function () {
    var phone_no = $(this).val();
    if (phone_no != "") {
      $("#read_only_phone_no").val(phone_no);
    }
  });

  $(".monthDOB").on("change", function () {
    var month = jQuery(this).val();
    monthChange(month);
  });

  dayDOBchange();

  $("input[type=radio][name=existing_member]").change(function () {
    $(".membership_div").hide();
    $(".dmk-frontal").hide();
    $("#dmk_frontal_type").val("").selectpicker("refresh");
    if (this.value == "Yes") {
      $(".membership_div").show();
    }
  });

  $("input[type=radio][name=dmk_frontal]").change(function () {
    $(".dmk-frontal").hide();
    $("#dmk_frontal_type").val("").selectpicker("refresh");
    if (this.value == "Yes") {
      $(".dmk-frontal").show();
    }
  });

  // $("#forgot_id").on("click", function (event) {
  //   $(".exist-member").hide();
  //   $("#reset_phone_no").val("");
  //   $(".forgot_div").show();
  // });

  // $("#back_btn").on("click", function (event) {
  //   $(".forgot_div").hide();
  //   $(".exist-member").show();
  //   $("#reset_phone_no").val("");
  // });

  // $("#new_reg").on("click", function (event) {
  //   $(".step1").hide();
  //   $(".mks_video").remove();
  //   $(".mks_video_m").remove();
  //   $(".step2").show();
  //   $("html, body").animate({
  //     scrollTop: $(".step2").offset().top - 150,
  //   });
  // });

  $("#state").change(function () {
    if (
      $("#state").val() == "Tamil Nadu" ||
      $("#state").val() == "Puducherry"
    ) {
      getDistrict();
    } else {
      $(".state_div").hide();
      $("#district").empty();
      $("#ac").val("");
      $("<option>").val("").text(lang.select_state).appendTo($("#district"));
      $("#district").selectpicker("refresh");
    }
  });

  $("#district").change(function () {
    if ($("#district").val() != "") {
      getAC();
    } else {
      $("#ac").empty();
      $("<option>").val("").text(lang.select_dist).appendTo($("#ac"));
      $("#ac").selectpicker("refresh");
    }
  });

  $("#upload_photo").each(function () {
    $this = $(this);
    $this.on("change", function () {
      var fsize = $this[0].files[0].size,
        ftype = $this[0].files[0].type,
        fname = $this[0].files[0].name,
        fextension = fname.substring(fname.lastIndexOf(".") + 1);
      validExtensions = ["jpg", "jpeg", "png"];

      if ($.inArray(fextension, validExtensions) == -1) {
        swal("Error", lang.file_type, "error");
        this.value = "";
        return false;
      } else {
        if (fsize > 3145728) {
          swal("Error", lang.file_size, "error");
          this.value = "";
          return false;
        }
        return true;
      }
    });
  });

  // Code for the Registration Validator
  var $regValidator = $("form[name='regForm']").validate({
    ignore: ":hidden",
    rules: {
      phone_no: {
        required: !0,
      },
      reset_phone_no: {
        required: !0,
      },
      existing_member: {
        required: !0,
      },
      otp: {
        required: !0,
      },
    },
    messages: {
      existing_member: {
        required: lang.existing_member_req,
      },
      membership_id: {
        required: lang.membership_id_req,
      },
      otp: {
        required: lang.otp_req,
      },
      phone_no: {
        required: lang.phone_no_req,
        maxlength: lang.phone_no_maxlength,
        minlength: lang.phone_no_minlength,
      },
      reset_phone_no: {
        required: lang.phone_no_req,
        maxlength: lang.phone_no_maxlength,
        minlength: lang.phone_no_minlength,
      },
    },
    errorPlacement: function (error, element) {
      if (element.is(":radio")) {
        error.appendTo(element.parents(".form-group"));
      } else if (element.is(":checkbox")) {
        error.appendTo(element.parents(".form-check"));
      } else {
        error.appendTo(element.parents(".form-group"));
      }
    },
  });
  jQuery.validator.addMethod("noSpace", function (value, element) {
    //Code used for blank space Validation
    return $.trim(value) != "";
  });

  var $validator = $("form[name='surveyForm']").validate({
    ignore: ":hidden",
    rules: {
      name: {
        required: !0,
        noSpace: true,
      },
      father_name: {
        required: !0,
      },
      gender: {
        required: !0,
      },
      district: {
        required: !0,
      },
      ac: {
        required: !0,
      },
      address: {
        required: !0,
      },
      pincode: {
        required: !0,
      },
      party_member: {
        required: !0,
      },
      party_name: {
        required: !0,
      },
      confirmation: {
        required: !0,
      },
      regMonth: {
        required: !0,
      },
      regDay: {
        required: !0,
      },
      regYear: {
        required: !0,
      },
    },
    groups: {
      dateOfBirth: "regMonth regDay regYear",
    },
    messages: {
      name: {
        required: lang.existing_member_req,
        noSpace: lang.existing_member_req,
      },
      father_name: {
        required: lang.existing_member_req,
      },
      gender: {
        required: lang.existing_member_req,
      },
      district: {
        required: lang.existing_member_req,
      },
      ac: {
        required: lang.existing_member_req,
      },
      address: {
        required: lang.existing_member_req,
      },
      pincode: {
        required: lang.existing_member_req,
      },
      party_member: {
        required: lang.existing_member_req,
      },
      party_name: {
        required: lang.existing_member_req,
      },
      confirmation: {
        required: lang.existing_member_req,
      },
      regMonth: {
        required: lang.select_dob,
      },
      regDay: {
        required: lang.select_dob,
      },
      regYear: {
        required: lang.select_dob,
      },
    },
    errorPlacement: function (error, element) {
      if (element.is(":radio")) {
        error.appendTo(element.parents(".form-group"));
      } else if (element.is(":checkbox")) {
        error.appendTo(element.parents(".form-check"));
      } else if (
        element.attr("name") == "regMonth" ||
        element.attr("name") == "regDay" ||
        element.attr("name") == "regYear"
      ) {
        error.appendTo(element.parents(".form-group"));
      } else if (element.attr("name") == "dob") {
        error.appendTo(element.parents(".form-group"));
      } else {
        // This is the default behavior
        error.appendTo(element.parents(".form-group"));
        // error.insertAfter(element);
      }
    },
  });

  var current_fs, next_fs, previous_fs; //fieldsets
  var opacity;

  $(".next").click(function (event) {
    event.preventDefault();
    var $valid = $("form[name='surveyForm']").valid();
    if (!$valid) {
      $validator.focusInvalid();
      return false;
    } else {
      current_fs = $(this).parent();
      next_fs = $(this).parent().next();

      //Add Class Active
      $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

      //show the next fieldset
      next_fs.show();
      //hide the current fieldset with style
      current_fs.animate(
        {
          opacity: 0,
        },
        {
          step: function (now) {
            // for making fielset appear animation
            opacity = 1 - now;

            current_fs.css({
              display: "none",
              position: "relative",
            });
            next_fs.css({
              opacity: opacity,
            });
          },
          duration: 600,
        }
      );
    }
  });

  $(".previous").click(function () {
    current_fs = $(this).parent();
    previous_fs = $(this).parent().prev();

    //Remove class active
    $("#progressbar li")
      .eq($("fieldset").index(current_fs))
      .removeClass("active");

    //show the previous fieldset
    previous_fs.show();

    //hide the current fieldset with style
    current_fs.animate(
      {
        opacity: 0,
      },
      {
        step: function (now) {
          // for making fielset appear animation
          opacity = 1 - now;

          current_fs.css({
            display: "none",
            position: "relative",
          });
          previous_fs.css({
            opacity: opacity,
          });
        },
        duration: 600,
      }
    );
  });

  $("#generateOTP").on("click", function () {
    var phone_no = $("#phone_no").val();
    if (phone_no.trim() != "") {
      ajaxindicatorstart("Please Wait..");
      $.ajax({
        url: "welcome/sendOTP",
        type: "post",
        data: $("#regForm").serialize(),
        dataType: "json",
        success: function (data) {
          ajaxindicatorstop();
          if (data.status == "OK") {
            disableResend();
            $(".otp-section").show();
          } else {
            swal("Error", data.message, "error");
          }
        },
        error: function () {
          ajaxindicatorstop();
        },
      });
    }
  });

  $("#verify_btn").on("click", function (event) {
    event.preventDefault();
    var $valid = $("form[name='regForm']").valid();
    if (!$valid) {
      $regValidator.focusInvalid();
      return false;
    } else {
      ajaxindicatorstart("Please Wait..");
      $.ajax({
        url: "welcome/verifyOTP",
        type: "post",
        data: $("#regForm").serialize(),
        dataType: "json",
        success: function (data) {
          ajaxindicatorstop();
          if (data.status == "OK") {
            if (data.is_exist == true) {
              window.location.href =
                data.lang == "ta" ? "thankyou" : "thankyou/en";
              return;
            }
            $(".otp-section").hide();
            $("#generateOTP").remove();
            $(".timer_div").hide();
            $("#begin_btn").removeAttr("disabled");
          } else {
            swal("Error", data.message, "error");
          }
        },
        error: function () {
          ajaxindicatorstop();
        },
      });
    }
  });

  $("#begin_btn").on("click", function (event) {
    event.preventDefault();
    var $valid = $("form[name='regForm']").valid();
    if (!$valid) {
      $regValidator.focusInvalid();
      return false;
    } else {
      $.ajax({
        url: "welcome/verifyOTP",
        type: "post",
        data: $("#regForm").serialize(),
        dataType: "json",
        success: function (data) {
          if (data.status == "OK") {
            if (data.is_exist == true) {
              window.location.href =
                data.lang == "ta" ? "thankyou" : "thankyou/en";
              return;
            }
            $(".step1").hide();
            $(".mks_video").remove();
            $(".mks_video_m").remove();
            $(".step2").show();
            $("html, body").animate({
              scrollTop: $(".step2").offset().top - 150,
            });
          } else {
            swal("Error", data.message, "error");
          }
        },
        error: function () {
          ajaxindicatorstop();
        },
      });
    }
  });

  // $("#reset_btn").on("click", function (event) {
  //   event.preventDefault();
  //   var $valid = $("form[name='regForm']").valid();
  //   if (!$valid) {
  //     $regValidator.focusInvalid();
  //     return false;
  //   } else {
  //     $.ajax({
  //       url: "welcome/resetMembershipId",
  //       type: "post",
  //       data: $("#regForm").serialize(),
  //       dataType: "json",
  //       success: function (data) {
  //         if (data.status == "OK") {
  //           window.location.href =
  //             data.lang == "ta" ? "thankyou" : "thankyou/en";
  //           return;
  //         } else {
  //           swal("Error", data.message, "error");
  //         }
  //       },
  //       error: function () {
  //         ajaxindicatorstop();
  //       },
  //     });
  //   }
  // });

  // $("#submit_btn").on("click", function (event) {
  //   event.preventDefault();
  //   var $valid = $("form[name='regForm']").valid();
  //   if (!$valid) {
  //     $regValidator.focusInvalid();
  //     return false;
  //   } else {
  //     ajaxindicatorstart("Please Wait..");
  //     $.ajax({
  //       url: "welcome/checkmemberexist",
  //       type: "post",
  //       data: $("#regForm").serialize(),
  //       dataType: "json",
  //       success: function (data) {
  //         ajaxindicatorstop();
  //         if (data.status == "OK") {
  //           if (data.is_exist == true && data.type == "new") {
  //             window.location.href =
  //               data.lang == "ta" ? "thankyou" : "thankyou/en";
  //             return;
  //           }
  //           $("#membership_id").val("");
  //           $(".step1").hide();
  //           $(".mks_video").remove();
  //           $(".mks_video_m").remove();
  //           $(".step2").show();
  //           $("html, body").animate({
  //             scrollTop: $(".step2").offset().top - 150,
  //           });
  //         } else {
  //           swal("Error", data.message, "error");
  //         }
  //       },
  //       error: function () {
  //         ajaxindicatorstop();
  //       },
  //     });
  //   }
  // });

  $(".btn-finish").click(function (event) {
    event.preventDefault();
    var $valid = $("form[name='surveyForm']").valid();
    if (!$valid) {
      $validator.focusInvalid();
      return false;
    } else {
      var form = $("form[name='surveyForm']")[0];
      var formData = new FormData(form);
      $("input[type=file]", form).each(function () {
        var files = $(this).prop("files");
        if (files != undefined && files.length <= 0) {
          formData.delete($(this).attr("name"));
        }
      });
      formData.append("phone_no", $("#phone_no").val());
      formData.append(
        "existing_member",
        $("input[name='existing_member']:checked").val()
      );
      formData.append("membership_id", $("#membership_id").val());
      formData.append("otp", $("#otp").val());
      formData.append("language", $("#language").val());
      formData.append("referral_code", $("#referral_code").val());
      formData.append("ac_no", $("#ac").find(":selected").data("params"));
      formData.append(
        "district_code",
        $("#district").find(":selected").data("params")
      );

      formData.append(
        "dmk_frontal",
        $("input[name='dmk_frontal']:checked").val()
      );
      formData.append("dmk_frontal_type", $("#dmk_frontal_type").val());
      window._mfq = window._mfq || [];
      window._mfq.push(["formSubmitAttempt", "#surveyForm"]);
      $.ajax({
        xhr: function () {
          var xhr = new window.XMLHttpRequest();
          xhr.upload.addEventListener(
            "progress",
            function (evt) {
              if (evt.lengthComputable) {
                var percentComplete = Math.round(
                  (evt.loaded / evt.total) * 100
                );
                $(".progress").show();
                $(".progress-bar").width(percentComplete + "%");
                var html = "";
                html +=
                  percentComplete == 100 ? "Submitted... " : "Submitting... ";
                $(".progress-bar").html(html + percentComplete + "%");
              }
            },
            false
          );
          return xhr;
        },
        url: "welcome/registration",
        type: "post",
        data: formData,
        contentType: false,
        cache: false,
        processData: false,
        beforeSend: function () {
          $(".progress-bar").width("0%");
          ajaxindicatorstart("Please Wait..");
        },
        success: function (res) {
          ajaxindicatorstop();
          data = JSON.parse(res);
          if (data.status == "OK") {
            window._mfq.push(["formSubmitSuccess", "#surveyForm"]);
            window.location.href =
              data.lang == "ta" ? "thankyou" : "thankyou/en";
          } else {
            swal("Error", data.message, "error");
            window._mfq.push(["formSubmitFailure", "#surveyForm"]);
          }
        },
        error: function () {
          ajaxindicatorstop();
          window._mfq.push(["formSubmitFailure", "#surveyForm"]);
        },
      });
    }
  });
});

function monthChange(month, day = "") {
  var addOption = "";
  $(".dayDOB").empty();
  addOption = addOption + '<option value="">' + lang.day + "</option>";
  if (month != "") {
    if (month === "2") {
      dayCount = 29;
    }
    if (month === "4" || month === "6" || month === "9" || month === "11") {
      dayCount = 30;
    }
    if (
      month === "1" ||
      month === "3" ||
      month === "5" ||
      month === "7" ||
      month === "8" ||
      month === "10" ||
      month === "12"
    ) {
      dayCount = 31;
    }
    for (var i = 1; i <= dayCount; i++) {
      var valueselect = day == i ? "selected" : "";
      addOption =
        addOption +
        "<option value=" +
        i +
        " " +
        valueselect +
        ">" +
        i +
        "</option>";
    }
    $(".dayDOB").append(addOption);
  }
}

function dayDOBchange(y = "") {
  var addOptionYear = "";
  var year = 2003;
  for (var i = 1; i <= 100; i++) {
    year--;
    var valueselect = y == year ? "selected" : "";
    addOptionYear =
      addOptionYear +
      "<option value=" +
      year +
      " " +
      valueselect +
      ">" +
      year +
      "</option>";
  }
  $(".yearDOB").append(addOptionYear);
}

function getDistrict(district = "", ac = "") {
  ajaxindicatorstart("Please Wait..");
  var dataString = "state=" + $("#state").val();
  $.ajax({
    type: "POST",
    data: dataString,
    url: "welcome/getDistrict",
    cache: false,
    dataType: "json",
    success: function (html) {
      ajaxindicatorstop();
      $(".state_div").show();
      $("#district").empty();
      $("<option>")
        .val("")
        .text(lang.select_district)
        .attr("data-params", "0")
        .appendTo($("#district"));
      $.each(html, function (key, value) {
        var dist_name = lang.lang == "en" ? value.district : value.district_tn;
        $("<option>")
          .val(value.district)
          .text(dist_name)
          .attr("data-params", value.district_code)
          .appendTo($("#district"));
      });
      if (district != "") {
        $("#district option[value='" + district + "']").attr("selected", true);
        getAC(ac);
      }
      $("#district").selectpicker("refresh");
      $("#ac").empty();
      $("<option>").val("").text(lang.select_dist_first).appendTo($("#ac"));
      $("#ac").selectpicker("refresh");
    },
    error: function () {
      ajaxindicatorstop();
    },
  });
}

function getAC(ac = "") {
  var dataString = "district=" + $("#district").val();
  $.ajax({
    type: "POST",
    data: dataString,
    url: "welcome/getAC",
    cache: false,
    dataType: "json",
    success: function (html) {
      $("#ac").empty();
      $("<option>")
        .val("")
        .attr("data-params", "0")
        .text(lang.select_ac)
        .appendTo($("#ac"));
      $.each(html, function (key, value) {
        var ac_name = lang.lang == "en" ? value.ac_name : value.ac_name_tn;
        $("<option>")
          .val(value.ac_name)
          .text(ac_name)
          .attr("data-params", value.ac_no)
          .appendTo($("#ac"));
      });
      $("<option>")
        .val("Other")
        .text(lang.other_ac)
        .attr("data-params", "0")
        .appendTo($("#ac"));
      if (ac != "") {
        $("#ac option[value='" + ac + "']").attr("selected", true);
      }
      $("#ac").selectpicker("refresh");
    },
  });
}

function onlyNumber() {
  $(".integer").on("input", function (event) {
    this.value = this.value.replace(/[^0-9]/g, "");
  });
}

function onlyChar() {
  $(".nameinput").on("input", function (event) {
    this.value = this.value.replace(/[^A-Za-z\s]/g, "");
  });
}

function disableResend() {
  $("#generateOTP").attr("disabled", true);
  timer(120);
  $(".timer_div").show();
}

let timerOn = true;
function timer(remaining) {
  var m = Math.floor(remaining / 60);
  var s = remaining % 60;

  m = m < 10 ? "0" + m : m;
  s = s < 10 ? "0" + s : s;
  document.getElementById("timer").innerHTML = m + ":" + s;
  remaining -= 1;

  if (remaining >= 0 && timerOn) {
    setTimeout(function () {
      timer(remaining);
    }, 1000);
    return;
  }

  if (!timerOn) {
    // Do validate stuff here
    return;
  }
  $(".timer_div").hide();
  $("#generateOTP").removeAttr("disabled");
}

//loader
function ajaxindicatorstart(text) {
  $("#resultLoading").remove();
  if (jQuery(".content").find("#resultLoading").attr("id") != "resultLoading") {
    jQuery(".content").append(
      '<div id="resultLoading" style="display:none"><div><img src="assets/images/ajax-modal-loading.gif"><div>' +
        text +
        '</div></div><div class="bg"></div></div>'
    );
  }

  jQuery("#resultLoading").css({
    width: "100%",
    height: "100%",
    position: "fixed",
    "z-index": "10000000",
    top: "0",
    left: "0",
    right: "0",
    bottom: "0",
    margin: "auto",
  });

  jQuery("#resultLoading .bg").css({
    background: "#000000",
    opacity: "0.7",
    width: "100%",
    height: "100%",
    position: "absolute",
    top: "0",
  });

  jQuery("#resultLoading>div:first").css({
    width: "250px",
    height: "75px",
    "text-align": "center",
    position: "fixed",
    top: "0",
    left: "0",
    right: "0",
    bottom: "0",
    margin: "auto",
    "font-size": "16px",
    "z-index": "10",
    color: "#ffffff",
  });

  jQuery("#resultLoading .bg").height("100%");
  jQuery("#resultLoading").fadeIn("slow");
  jQuery(".container-contact100").css("cursor", "wait");
}

function ajaxindicatorstop() {
  jQuery("#resultLoading .bg").height("100%");
  jQuery("#resultLoading").fadeOut("slow");
  jQuery(".container-contact100").css("cursor", "default");
}
//end loader
