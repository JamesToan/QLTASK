function reloadtrang() {
    window.location.reload();
}

function ChangeToSlug(str) {
    // Chuyển hết sang chữ thường
    str = str.toLowerCase();

    // xóa dấu
    str = str.replace(/(à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ)/g, 'a');
    str = str.replace(/(è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ)/g, 'e');
    str = str.replace(/(ì|í|ị|ỉ|ĩ)/g, 'i');
    str = str.replace(/(ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ)/g, 'o');
    str = str.replace(/(ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ)/g, 'u');
    str = str.replace(/(ỳ|ý|ỵ|ỷ|ỹ)/g, 'y');
    str = str.replace(/(đ)/g, 'd');

    // Xóa ký tự đặc biệt
    str = str.replace(/([^0-9a-z-\s])/g, '');

    // Xóa khoảng trắng thay bằng ký tự -
    str = str.replace(/(\s+)/g, '-');

    // xóa phần dự - ở đầu
    str = str.replace(/^-+/g, '');

    // xóa phần dư - ở cuối
    str = str.replace(/-+$/g, '');

    // return
    return str;
}

function ChangeToSlugSearch(str) {
    // Chuyển hết sang chữ thường
    str = str.toLowerCase().trim();

    // xóa dấu
    str = str.replace(/(à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ)/g, 'a');
    str = str.replace(/(è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ)/g, 'e');
    str = str.replace(/(ì|í|ị|ỉ|ĩ)/g, 'i');
    str = str.replace(/(ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ)/g, 'o');
    str = str.replace(/(ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ)/g, 'u');
    str = str.replace(/(ỳ|ý|ỵ|ỷ|ỹ)/g, 'y');
    str = str.replace(/(đ)/g, 'd');

    // Xóa ký tự đặc biệt
    str = str.replace(/([^0-9a-z-\s])/g, '');

    // Xóa khoảng trắng thay bằng ký tự -
    str = str.replace(/(\s+)/g, ' ');

    // xóa phần dự - ở đầu
    str = str.replace(/^-+/g, '');

    // xóa phần dư - ở cuối
    str = str.replace(/-+$/g, '');

    // return
    return str;
}

function ktanh(file) {
    chonfile = file;
    var fileIn = file[0];
    if (fileIn.files === undefined || fileIn.files.length == 0) {
        call_noti('Chưa chọn file ảnh!', 'error');
        chonfile.val(null);
        return false;
    } else {
        file = fileIn.files[0];
        type = file.type;
        size = file.size;

        if (size < filesizeup) {
            if (type == "image/jpg" || type == "image/jpeg" || type == "image/png" || type == "image/gif") {
                return true;
            } else {
                call_noti('Vui lòng chọn 1 file ảnh!', 'error');
                chonfile.val(null);
                return false;
            }
        } else {
            call_noti('Dung lượng file nhỏ hơn 3MB!', 'error');
            chonfile.val(null);
            return false;
        }
    }
}

function ktcv(file) {
    chonfile = file;
    var fileIn = file[0];
    if (fileIn.files === undefined || fileIn.files.length == 0) {
        if (typeof file.attr('required') === "undefined") {
            return true;
        } else {
            call_noti('Vui lòng upload file hồ sơ', 'error');
            chonfile.val(null);
            return false;
        }
    } else {
        file = fileIn.files[0];
        type = file.type;
        size = file.size;
        if (size < hssizeup) {
            if (type == "image/jpg" || type == "image/jpeg" || type == "image/png" || type == "image/gif" || type == "application/msword" || type == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || type == "application/pdf") {
                return true;
            } else {
                call_noti('File không đúng định dạng!', 'error');
                chonfile.val(null);
                return false;
            }
        } else {
            call_noti('Dung lượng file nhỏ hơn 3MB!', 'error');
            chonfile.val(null);
            return false;
        }
    }
}

function format1(n, currency) {

    return currency + n.toFixed(0).replace(/./g, function(c, i, a) {
        return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "." + c : c;
    });
}

function tien(str) {
    var array = new Array();
    var arraystr = new Array();
    var x = str;
    x = x.replace(/[^0-9]/g, '');

    $j = 0;
    for ($i = x.length - 1; $i >= 0; $i--) {

        if ($j == 3) {
            arraystr.push('.');
            arraystr.push(x[$i]);
            $j = 0;
        } else {
            arraystr.push(x[$i]);
        }
        $j++;
    }
    temp = '';
    for ($i = arraystr.length - 1; $i >= 0; $i--) {
        temp = temp + arraystr[$i];
    }

    return temp;
}

function cuon(phantu, plus) {
    $('html, body').animate({
        scrollTop: (phantu.offset().top) - plus
    }, 1000)
}

function parseDate(str) {
    var mdy = str.split('/');
    return new Date(mdy[2], mdy[0] - 1, mdy[1]);
}

function daydiff(first, second) {
    return (second - first) / (1000 * 60 * 60 * 24)
}

function btnlinkload(elm, text, onlyIcon) {
    if (!text || text == "") {
        if (onlyIcon)
            text = '<i class="fas fa-spinner fa-spin"></i>';
        else
            text = 'Loading... <i class="fas fa-spinner fa-spin"></i>';
    }
    elm.attr("data-textLoading", elm.html());
    elm.html(text);
    elm.prop("disabled", true);
    elm.addClass("disabled");
}

function btnlinkthanhcong(elm, text) {
    if (text) {
        elm.html(text);
    } else {
        text2 = elm.attr("data-textLoading");
        if (text2) {
            elm.html(text2);
            elm.attr("data-textLoading", "");
        } else {
            elm.html("Submit");
        }
    }
    elm.prop("disabled", false);
    elm.removeClass("disabled");
}

function isset(key, array) {
    ret = false;
    array.forEach(function(entry) {
        if (entry == key) {
            ret = true;
        }
    });
    return ret;
}

function in_array(needle, haystack) {
    for (var key in haystack) {
        if (needle === haystack[key]) {
            return true;
        }
    }
    return false;
}

function ngayoutput(strDate, format) {
    if (!isNaN(strDate)) {
        strDate = strDate * 1000;
    } else {
        strDate = strDate.replace(" ", "T");
    }
    let date = new Date(strDate);
    if (!(date instanceof Date)) {
        return 'NaN';
    }
    let timezone = -(date.getTimezoneOffset() / 60);

    let year = date.getUTCFullYear();
    let yearShort = year % 100;
    let month = date.getUTCMonth() + 1;
    if (month < 10)
        month = "0" + month;
    let day = date.getUTCDate();
    if (day < 10)
        day = "0" + day;
    let hours = date.getUTCHours() + timezone;
    if (hours < 10)
        hours = "0" + hours;
    let minutes = date.getUTCMinutes();
    if (minutes < 10)
        minutes = "0" + minutes;
    let seconds = date.getUTCSeconds();
    if (seconds < 10)
        seconds = "0" + seconds;
    if (!format) {
        format = "d-m-Y H:i:s";
    }
    return format.replace("d", day).replace("m", month).replace("y", yearShort).replace("Y", year).replace("H", hours).replace("i", minutes).replace("s", seconds);
}

function phantrangajax(total_page, cur_page) {

    cur_page = parseInt(cur_page);
    total_page = parseInt(total_page);
    current_range = new Array();
    if (cur_page - 2 < 1)
        start = 1;
    else
        start = cur_page - 2;
    if (cur_page + 2 > total_page)
        end = total_page;
    else
        end = cur_page + 2;
    current_range[0] = start;
    current_range[1] = end;

    first_page = '';
    if (cur_page > 3)
        first_page += '<li  data-page="1" class="page page-item" ><a class="page-link">1</a></li>';
    if (cur_page >= 5)
        first_page += '<li class="page page-item"> <a class="page-link">...</a> <li>';

    last_page = '';
    if (cur_page <= (total_page - 4))
        last_page += '<li class="page page-item"> <a class="page-link">...</a> <li>';
    if (cur_page < (total_page - 2))
        last_page += '<li  data-page="' + total_page + '" class="page page-item" ><a class="page-link">' + total_page + '</a></li>';

    previous_page = '';
    if (cur_page > 1)
        previous_page = '<li data-page="' + (cur_page - 1) + '" class="page page-item" ><a class="page-link">' + "<" + '</a></li>';

    next_page = '';
    if (cur_page < total_page)
        next_page = '<li data-page="' + (cur_page + 1) + '" class="page page-item " ><a class="page-link">' + ">" + '</a></li>';

    page = new Array();
    for (x = current_range[0]; x <= current_range[1]; ++x) {
        active = '';
        if (x == cur_page)
            active = "active";
        var html = '<li data-page="' + x + '" class="page page-item ' + active + ' "><a class="page-link">' + x + '</a></li>';
        page.push(html);
    }
    if (total_page > 1) {
        return previous_page + first_page + page.join(" ") + last_page + next_page;
    } else
        return '';
}

function tien_ngangon(int) {
    if (!isNaN(int)) {
        if (int > 1000000000)
            return (int / 1000000000) + " tỷ";
        else if (int > 1000000)
            return (int / 1000000) + " triệu";
        else
            return int;
    } else
        return 0;
}

function check_img($urlImg) {
    var http = new XMLHttpRequest();
    http.open('HEAD', URL_ROOT + $urlImg, false);
    http.send();
    if (http.status != 404)
        return $urlImg;
    else
        return "public/upload/images/noimage.png";
}

function getURL(id, slug, type, urlfulldomain) {
    url = urlfulldomain ? URL_ROOT : '';
    if (type === 'sp') {
        url += slug + '-' + id + '.html';
    } else if ($type === 'bv') {
        url += id + '/' + slug;
    }
    return url;
}

function autocompleteSearch(input, list, classText, classHide) {
    input.keyup(function() {
        inputText = ChangeToSlugSearch(input.val().trim());
        list.find(classText).each(function() {
            if (ChangeToSlugSearch($(this).html()).indexOf(inputText) == -1) {
                $(this).closest(classHide).hide();
            } else
                $(this).closest(classHide).show();
        });
    });
}

$(document).on('click', '.btnViewPass', function(e) {
    if ($('#' + $(this).attr('data-id')).attr('type') == 'text') {
        $('#' + $(this).attr('data-id')).attr('type', 'password');
        $(this).html('<i class="far fa-eye"></i>');
    } else {
        $('#' + $(this).attr('data-id')).attr('type', 'text');
        $(this).html('<i class="far fa-eye-slash"></i>');
    }
});

var validatePassword = function(inputPass, inputValid) {
    if (inputPass.val() != inputValid.val()) {
        inputValid[0].setCustomValidity("Mật khẩu không khớp");
    } else {
        inputValid[0].setCustomValidity('');
    }
};

$(".validPass").each(function() {
    var inputPass = $("#" + $(this).attr("data-id"));
    var inputValid = $(this);
    inputValid.keyup(function() {
        validatePassword(inputPass, inputValid);
    });
    inputPass.focusout(function() {
        validatePassword(inputPass, inputValid);
    });
});

// Y-m-d H:i:s
// second
function time_elapsed_string(input) {
    if (!isNaN(input)) {
        input = input * 1000;
    } else {
        input = input.replace(" ", "T");
    }
    let date = new Date(input);
    let current = new Date();
    if (!(date instanceof Date)) {
        return 'NaN';
    }
    var seconds = Math.floor((current.getTime() - date.getTime()) / 1000);

    var interval = seconds / 31536000;

    if (interval > 1) {
        return Math.floor(interval) + " năm trước";
    }
    interval = seconds / 2592000;
    if (interval > 1) {
        return Math.floor(interval) + " tháng trước";
    }
    interval = seconds / 86400;
    if (interval > 1) {
        return Math.floor(interval) + " ngày trước";
    }
    interval = seconds / 3600;
    if (interval > 1) {
        return Math.floor(interval) + " giờ trước";
    }
    interval = seconds / 60;
    if (interval > 1) {
        return Math.floor(interval) + " phút trước";
    }
    if (seconds > 5) {
        return Math.floor(seconds) + " giây trước";
    } else {
        return "Vài giây trước";
    }

}

function isDate(strDate) {
    if (strDate instanceof Date) {
        return true;
    } else {
        if (!isNaN(strDate)) {
            strDate = strDate * 1000;
        } else {
            strDate = strDate.replace(" ", "T");
        }
        let date = new Date(strDate);
        if (date instanceof Date) {
            return true
        } else {
            return false;
        }
    }
}

// lấy path từ url hiện tại
function getPathUrl() {
    let origin = window.location.protocol + '//' + window.location.host;
    let pathname = window.location.href.substr(origin.length);
    return pathname;
}

// luu meta goc
const metaOrigin = {
    title: $("title").text(),
    desc: $("meta[name='description']").attr("content"),
    image: $("meta[name='image']").attr("content"),
    path_url: getPathUrl()
}

// meta là object chứa các thông tin meta data
function pushGTAG(meta) {
    $("title").text(meta.title);
    $("meta[property='og:title']").attr("content", meta.title);
    $("meta[name='description']").attr("content", meta.desc);
    $("meta[property='og:description']").attr("content", meta.desc);
    $("meta[name='image']").attr("content", meta.image);
    $("meta[property='og:image']").attr("content", meta.image);
    if (meta.path_url != '') {
        if (meta.path_url.indexOf(URL_ROOT) == 0) {
            meta.path_url = meta.path_url.substring(URL_ROOT.length, meta.path_url.length);
        }

        if (meta.path_url[0] != '/') {
            meta.path_url = '/' + meta.path_url;
        }
        gtag('config', GTAG, {
            'page_path': meta.path_url
        });
    }
    if (_pushState) {
        window.history.pushState("", meta.title, meta.path_url);
    } else {
        _pushState = true;
    }
    if (meta.path_url.indexOf(URL_ROOT) == 0)
        URLNOW = meta.path_url;
    else if (meta.path_url.indexOf("/") == 0)
        URLNOW = window.location.protocol + '//' + window.location.host + meta.path_url;
    else
        URLNOW = window.location.protocol + '//' + window.location.host + "/" + meta.path_url;
}

function resetGtag(elm) {
    elm.removeAttribute("onclick");
    pushGTAG(metaOrigin);
}

function existAttr(elm, attrName) {
    let attr = elm.attr(attrName);
    return (typeof attr !== typeof undefined && attr !== false);
}

function popupCenter(url, title, w, h, toolbar, status) {
    // Fixes dual-screen position                         Most browsers      Firefox
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : window.screenX;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : window.screenY;

    var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    var systemZoom = width / window.screen.availWidth;
    var left = (width - w) / 2 / systemZoom + dualScreenLeft;
    var top = (height - h) / 2 / systemZoom + dualScreenTop;
    var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w / systemZoom + ', height=' + h / systemZoom + ', top=' + top + ', left=' + left + ', toolbar=' + toolbar + ', status=' + status);

    // Puts focus on the newWindow
    if (window.focus) newWindow.focus();
}

function getExtension(str) {
    if (str && str != '') {
        let arr = str.split('.');
        let len = arr.length;
        return arr[len - 1];
    }
    return '';
}

function getIconFile(extFile) {
    let extFileCls = '';
    extFile = extFile.toLowerCase();
    if (extFile == 'jpg' || extFile == 'png' || extFile == 'jpeg') {
        extFileCls = 'fa-file-image';
    } else if (extFile == 'pdf') {
        extFileCls = 'fa-file-pdf';
    } else if (extFile == 'docx' || extFile == 'doc') {
        extFileCls = 'fa-file-word';
    }
    return extFileCls;
}

renderCaptcha = function(elm) {
    if (typeof grecaptcha !== 'undefined') {
        let id = elm.attr('id');
        let formElm = elm.closest("form");
        grecaptcha.render(id, {
            "sitekey": CAPTCHASITE,
            "callback": function(token) {
                formElm.find(".g-recaptcha-response").val(token);
                if (!formElm[0].checkValidity()) {
                    formElm.find(":invalid").first().focus();
                    formElm[0].reportValidity();
                    formElm.find(":invalid").first()[0].scrollIntoView({
                        behavior: "smooth"
                    });
                } else {
                    formElm.submit();
                }
            }
        });
    }
};

CaptchaCallback = function() {
    $(".g-recaptcha").each(function() {
        // captcha v3
        renderCaptcha($(this));
    });
};

call_noti = function(msg, type, time, position) {
    if (typeof time === 'undefined' || isNaN(time))
        time = 3000;
    toastr.options.timeOut = time;
    toastr.options.extendedTimeOut = time;
    if (position)
        toastr.options.positionClass = position;
    else if ($(window).width() < 765)
        toastr.options.positionClass = "toast-bottom-full-width";
    toastr[type](msg);
};

function delay(callback, ms) {
    let timer = 0;
    return function() {
        let context = this;
        clearTimeout(timer);
        timer = setTimeout(function() {
            callback.apply(context);
        }, ms || 0);
    };
}

function detectIos() {
    return [
            'iPad Simulator',
            'iPhone Simulator',
            'iPod Simulator',
            'iPad',
            'iPhone',
            'iPod'
        ].includes(navigator.platform)
        // iPad on iOS 13 detection
        ||
        (navigator.userAgent.includes("Mac") && "ontouchend" in document);
}

function setCookie(name, value, days) {
    var expires;
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    } else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

$(".speechText").click(function() {
    try {
        let recording;
        let elmInput = $("#" + $(this).attr('data-input'));
        let oldPlaceHolder = elmInput.attr("placeholder");
        let actionClick = $("#" + $(this).attr('data-click'));
        let elm = $(this);

        var SpeechRecognition = SpeechRecognition || webkitSpeechRecognition;

        let recognition = new SpeechRecognition();
        if (!detectIos()) {
            var SpeechGrammarList = SpeechGrammarList || webkitSpeechGrammarList;
            let grammar = '#JSGF V1.0;'
            let speechRecognitionList = new SpeechGrammarList();
            speechRecognitionList.addFromString(grammar, 1);
            recognition.grammars = speechRecognitionList;
        }
        recognition.lang = 'vi-VN';
        recognition.interimResults = false;

        recognition.onresult = function(event) {
            let lastResult = event.results.length - 1;
            let content = event.results[lastResult][0].transcript;
            elmInput.val(content);
            if (actionClick) {
                actionClick.click();
            }
        };

        recognition.onspeechend = function() {
            recognition.stop();
            elmInput.attr("placeholder", oldPlaceHolder);
            clearInterval(recording);
            elm.removeClass("recording");
        };

        recognition.onend = function() {
            elmInput.attr("placeholder", oldPlaceHolder);
            clearInterval(recording);
            elm.removeClass("recording");
        }

        recognition.onerror = function(event) {
            console.log('Error occurred in recognition: ' + event.error);
        }
        recognition.start();
        elm.addClass("recording");
        let index = 1;
        recording = setInterval(function() {
            let text = "Đang lắng nghe ";
            if (index == 1) {
                text += ".";
            } else if (index == 2) {
                text += "..";
            } else if (index == 3) {
                text += "...";
            }
            index++;
            if (index > 3)
                index = 1;
            elmInput.attr("placeholder", text);
        }, 500);

    } catch (err) {
        alert(err.message);
    }
})

$(document).on("keyup", ".onlyNum", function() {
    let x = $(this).val();
    x = x.replace(/[^0-9]/g, '');
    $(this).val(x);
});

$(document).ready(function() {
    $(".btnFollow").click(function() {
        if (usrID > 0) {
            if (usrType == 5) {
                let elm = $(this);
                let idntd = elm.attr("data-id");
                let typeF = elm.attr("data-type");
                if (typeF == "nofollow") {
                    $.post(URL_ROOT + "ung-vien/tuyendung/followComp", {
                        "ntd_id": idntd,
                        "type": typeF
                    }, function(r) {
                        if (r.status == 1) {
                            call_noti("Đã theo dõi!", "success", 3000);
                            elm.removeClass("btn-outline-primary");
                            elm.addClass("btn-primary");
                            elm.attr("data-type", "followed");
                            elm.html(`<i class="fas fa-signal-stream" data-toggle="tooltip"
                                       title="Theo dõi công ty để nhận thông báo tuyển dụng mới nhất"></i> 
                                       &nbsp;Đã theo dõi`);
                        } else {
                            alert(r.message);
                        }
                    }, "JSON");
                } else {
                    swal({
                            title: 'Bạn có chắc muốn hủy theo dõi?',
                            text: 'Theo dõi công ty để nhận thông báo tuyển dụng sớm nhất.',
                            type: 'info',
                            showCancelButton: true,
                            cancelButtonText: 'Hủy',
                            confirmButtonColor: '#DD6B55',
                            confirmButtonText: 'Theo dõi',
                            closeOnConfirm: true
                        },
                        function(isConfirm) {
                            if (!isConfirm) {
                                $.post(URL_ROOT + "ung-vien/tuyendung/followComp", {
                                    "ntd_id": idntd,
                                    "type": typeF
                                }, function(r) {
                                    if (r.status == 1) {
                                        call_noti("Đã hủy theo dõi!", "success", 3000);
                                        elm.addClass("btn-outline-primary");
                                        elm.removeClass("btn-primary");
                                        elm.attr("data-type", "nofollow");
                                        elm.html(`<i class="fal fa-info-circle" data-toggle="tooltip"
                                                     title="Theo dõi công ty để nhận thông báo tuyển dụng mới nhất"></i>
                                                     &nbsp;Theo dõi`);
                                    } else {
                                        alert(r.message);
                                    }
                                }, "JSON");
                            }
                        });
                }
            } else {
                alert("Đăng nhập ứng viên để thực hiện chức năng này!");
            }
        } else {
            call_noti("Vui lòng đăng nhập ứng viên để thực hiện chức năng này", "error", 3000);
            showModalLoginUv();
        }
    });

   

    //if (usrType == 5) {
    //    let currentDate = new Date();
    //    const today = currentDate.getDate();
    //    const currentMonth = currentDate.getMonth() + 1;
    //    const currentYear = currentDate.getFullYear();
    //    let strCurrentDate = today.toString() + currentMonth.toString() + currentYear.toString();
    //    if (typeof(Storage) !== "undefined") {
    //        let dataStorage = localStorage.getItem('refreshCV');
    //        if (dataStorage) {
    //            let dataJson = JSON.parse(dataStorage);
    //            if (dataJson.date != strCurrentDate || (usrID && usrID != dataJson.id)) {
    //                getLocalStorage(true);
    //            }
    //        } else {
    //            getLocalStorage(true);
    //        }
    //    } else {
    //        document.write("Sorry! Your browser no't support Web Storage.");
    //    }

    //    function getLocalStorage(isShow) {
    //        if (usrID) {
    //            const item = {
    //                id: usrID,
    //                date: strCurrentDate,
    //            }
    //            localStorage.setItem("refreshCV", JSON.stringify(item));
    //            if (isShow) {
    //                $.post(URL_ROOT + "api/taikhoan/getLocalStorage", {}, function(res) {
    //                    if (res.status == 1) {
    //                        if (res.isShow == 1) {
    //                            call_noti('Hôm nay bạn chưa làm mới hồ sơ, bạn có muốn làm mới hồ sơ lên đầu trang không<br><button class="btn btn-success confirm-refresh" style="margin-right: 10px;">Làm mới</button><button class="btn btn-secondary">Hủy</button>', 'info', 30000);
    //                        }
    //                    } else {
    //                        call_noti("Đã xảy ra lỗi vui lòng thử lại")
    //                    }
    //                }, "JSON");
    //            }
    //        }
    //    }
    //}

    $(document).on('click', ".confirm-refresh", function() {
        $.post(URL_ROOT + "ung-vien/taikhoan/refeshCV", {
            accept: 1
        }, function(res) {
            if (res.status == 1) {
                call_noti(res.message, 'success');
            } else {
                call_noti(res.message, 'error');
            }
        }, "JSON");
    });
});
