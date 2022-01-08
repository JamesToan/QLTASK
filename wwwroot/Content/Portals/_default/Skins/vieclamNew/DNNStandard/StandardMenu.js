$(document).ready(function() {

    function HoverOver() {
        $(this).addClass('hover');
    }

    function HoverOut() {
        $(this).removeClass('hover');
    }

    var config = {
        sensitivity: 2,
        interval: 200,
        over: HoverOver,
        timeout: 500,
        out: HoverOut
    };

    //$("#dnnMenu .topLevel > li.haschild").hoverIntent(config);

    $(".subLevel li.haschild").hover(HoverOver, HoverOut);

});


function setShowroomCity() {
  var city_id = $('.city-selector select option:selected').val();
  //$('.city-wrapper').css('display', 'none');
  $('.city_' + city_id).css('display', 'block');
}
$(document).ready(function () {
  $('.showroom-item').click(function () {
    $('.showroom-item').removeClass('active');
    $(this).addClass('active');
    var lat = $(this).attr('lat');
    var lon = $(this).attr('lon');

    $('.gllpLatitude').val(lat);
    $('.gllpLongitude').val(lon);
    $('.gllpUpdateButton').click();
  });
  $('.showroom-item').hover(
    function () {
      $(this).addClass('hover');
    },
    function () {
      $(this).removeClass('hover');
    }
  );

  setShowroomCity();
  $('.city-selector select').change(setShowroomCity);
})
