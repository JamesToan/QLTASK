Dropzone.autoDiscover = false;
const limitSkill = 6;

function refeshKN(data) {
    var i = 0;
    html = "";
    data.forEach(function(value) {
        if (value.thangTo == 0 || value.namTo == 0)
            tgTo = "hiện tại";
        else
            tgTo = "tháng " + value.thangTo + "/" + value.namTo;
        html += `<div class="col-sm-6 mb10">
                                        <div class="itemKN">
                                            <div class="toolBoxKN">
                                                <button class="btn btn-primary btn-block btn-xs btnKinhNghiem" type="button" data-id="${i}">
                                                    <i class="far fa-edit"></i>
                                                </button>
                                                <button class="btn btn-danger btn-block btn-xs btnDelKinhNghiem" type="button" data-id="${i}">
                                                    <i class="far fa-trash-alt"></i>
                                                </button>
                                            </div>
                                            <div class="mb5" style="font-weight: 500">${value.chucdanh}</div>
                                            <div class="mb5">${value.tencty}</div>
                                            <div style="font-size: 12px">Từ tháng ${value.thangFrom}/${value.namFrom} đến ${tgTo}
                                            </div>
                                        </div>
                                    </div>`;
        i++;
    });
    $('#divKN').fadeOut(100);
    $('#divKN').html(html);
    $('#divKN').fadeIn();
}

function refeshTD(data) {
    var i = 0;
    html = "";
    data.forEach(function(value) {
        html += `<div class="col-sm-6 mb10">
                                        <div class="itemTD">
											<input type="hidden" name="key[]" value="${i}">
											<div class="handle">
												<i class="far fa-arrows-alt"></i>
											</div>
                                            <div class="toolBoxTD">
                                                <button class="btn btn-primary btn-block btn-xs btnTrinhDo" type="button" data-id="${i}">
                                                    <i class="far fa-edit"></i>
                                                </button>
                                                <button class="btn btn-danger btn-block btn-xs btnDelTrinhDo" type="button" data-id="${i}">
                                                    <i class="far fa-trash-alt"></i>
                                                </button>
                                            </div>
                                            <div class="mb5" style="font-weight: 500">${value.bangcap}</div>
                                            <div class="mb5">${value.truong}</div>
                                            <div style="font-size: 12px">Từ tháng ${value.thangFrom}/${value.namFrom} đến tháng ${value.thangTo}/${value.namTo}
                                            </div>
                                        </div>
                                    </div>`;
        i++;
    });
    $('#divTD').fadeOut(100);
    $('#divTD').html(html);
    $('#divTD').fadeIn();
}

function refeshTK(data) {
    var i = 0;
    html = "";
    data.forEach(function(value) {
        html += `<div class="col-sm-6 mb10">
                    <div class="itemTK">
                        <div class="toolBoxTK">
                            <button class="btn btn-primary btn-block btn-xs btnThamKhao" type="button" data-id="${i}">
                                <i class="far fa-edit"></i>
                            </button>
                            <button class="btn btn-danger btn-block btn-xs btnDelThamKhao" type="button" data-id="${i}">
                                <i class="far fa-trash-alt"></i>
                            </button>
                        </div>
                        <div class="mb5" style="font-weight: 500">${value.hoten}</div>
                        <div class="mb5">Công ty: ${value.tencty}</div>
                        <div style="font-size: 12px">Vị trí: ${value.chucdanh}</div>
                    </div>
                </div>`;
        i++;
    });
    $('#divTK').fadeOut(100);
    $('#divTK').html(html);
    $('#divTK').fadeIn();
}

function refeshNN(data) {
    html = "";
    data.forEach(function(value) {
        html += `<div class="col-sm-6 mb10">
            <div class="itemNN">
                <div class="toolBoxNN">
                    <button class="btn btn-primary btn-block btn-xs btnNgoaiNgu" type="button" data-id="${value.ngoaingu_id}">
                        <i class="far fa-edit"></i>
                    </button>
                    <button class="btn btn-danger btn-block btn-xs btnDelNgoaiNgu" type="button" data-id="${value.ngoaingu_id}">
                        <i class="far fa-trash-alt"></i>
                    </button>
                </div>
                <div class="mb5"
                     style="font-weight: 500">${value.ngoaingu_ten}</div>
                <div style="font-size: 12px">
                    Nghe: ${value.ngoaingu_nghe} - Nói: ${value.ngoaingu_noi} - Đọc: ${value.ngoaingu_doc} - Viết: ${value.ngoaingu_viet}
                </div>
            </div>
        </div>`;
    });
    $('#divNN').fadeOut(100);
    $('#divNN').html(html);
    $('#divNN').fadeIn();
}

//$(document).ready(function() {
//    $('#dZUpload').dropzone({
//        url: URL_ROOT + "ung-vien/taikhoan/uploadanh",
//        maxFiles: maxfile,
//        thumbnailWidth: 100,
//        thumbnailHeight: 100,
//        addRemoveLinks: true,
//        dictRemoveFile: "x",
//        acceptedFiles: ".png, .jpg, .jpeg",
//        maxFilesize: 10, //MB
//        success: function(file, response) {
//            var kq = JSON.parse(response);
//            file.previewElement.querySelector("[data-dz-name]").innerHTML = kq.tenhinh;
//            file.previewElement.querySelector("[data-dz-thumbnail]").src = kq.url;
//            var html = '<input class="hidden" value="' + kq.id_hinh + '" data-img-id><input class="hidden" value="' + kq.tenhinh + '" name="hinhtmp[]">' + "<div class='divAva' title='Chọn làm hình đại diện'><input type='checkbox' name='hinhdaidien' value='" + kq.tenhinh + "' class='avatar'></div>";
//            file.previewElement.querySelector(".dz-filename").innerHTML = html + file.previewElement.querySelector(".dz-filename").innerHTML;
//        },
//        removedfile: function(file) {
//            id_hinh = file.previewElement.querySelector("[data-img-id]").value;
//            tenhinh = file.previewElement.querySelector("[data-dz-name]").innerHTML;
//            $.post(URL_ROOT + "ung-vien/taikhoan/xoaanh", {
//                'id_hinh': id_hinh,
//                'tenhinh': tenhinh
//            }, function(o) {
//                if (o.status == 1) {
//                    $(document).find(file.previewElement).remove();
//                }
//            }, 'JSON')
//        },
//        init: function() {
//            myDropzone = this;
//        },
//        maxfilesexceeded: function(file) {
//            alert('Upload tối đa ' + maxfile + ' file');
//            $(document).find(file.previewElement).remove();
//        }
//    });

//    $(document).on('click', '.avatar', function() {
//        $('.avatar').prop('checked', false);
//        $(this).prop('checked', true);
//    });

//    $('.dz-remove').on('click', function() {
//        parentEl = $(this).parent();
//        tenhinh = $(this).prev().children().find('span  ').html();
//        $.post(URL_ROOT + "ung-vien/taikhoan/xoaanh", {
//            'id_hinh': $(this).attr('data-idhinh'),
//            'tenhinh': tenhinh
//        }, function(o) {
//            if (o.status == 1) {
//                maxfile += 1;
//                parentEl.remove();
//                myDropzone.options.maxFiles = maxfile;
//            }
//        }, 'JSON');
//    });

//    $('.btnEditInfo').click(function() {
//        $('#modalGeneralHoSo .modal-content').html(`
//            <div class="lds-ellipsis" style="margin: 20px auto">
//                <div></div>
//                <div></div>
//                <div></div>
//                <div></div>
//            </div>
//        `);
//        $('#modalGeneralHoSo').modal('show');
//        $.post(URL_ROOT + 'ung-vien/taikhoan/editInfo', {}, function(r) {
//            $('#modalGeneralHoSo .modal-content').html(r);
//        });
//    });

//    $('.btnEditTQ').click(function() {
//        $('#modalGeneralHoSo .modal-content').html(`
//            <div class="lds-ellipsis" style="margin: 20px auto">
//                <div></div>
//                <div></div>
//                <div></div>
//                <div></div>
//            </div>
//        `);
//        $('#modalGeneralHoSo').modal('show');
//        $.post(URL_ROOT + 'ung-vien/taikhoan/editTQ', {}, function(r) {
//            if (r.length < 100) {
//                call_noti('Hoàn thành cập nhật thông tin cá nhân trước.', 'error');
//                $('#modalGeneralHoSo').modal('hide');
//            } else {
//                $('#modalGeneralHoSo .modal-content').html(r);
//            }
//        });
//    });

//    $(document).on('click', '.btnKinhNghiem', function() {
//        let el = $(this);
//        $('#modalGeneralHoSo .modal-content').html(`
//            <div class="lds-ellipsis" style="margin: 20px auto">
//                <div></div>
//                <div></div>
//                <div></div>
//                <div></div>
//            </div>
//        `);
//        $('#modalGeneralHoSo').modal('show');
//        id = $(this).attr('data-id');
//        $.post(URL_ROOT + 'ung-vien/taikhoan/editKinhNghiem', {
//            'id': id
//        }, function(r) {
//            if (r.length < 100) {
//                call_noti('Hoàn thành cập nhật thông tin học vấn bằng cấp trước.', 'error');
//                $('#modalGeneralHoSo').modal('hide');
//            } else {
//                $('#modalGeneralHoSo .modal-content').html(r);
//            }
//        });
//    });

//    $(document).on('click', '.btnDelKinhNghiem', function() {
//        elm = $(this);
//        swal({
//                title: 'Bạn có chắc muốn xóa kinh nghiệm này?',
//                text: '',
//                type: 'warning',
//                showCancelButton: true,
//                confirmButtonColor: '#DD6B55',
//                confirmButtonText: 'OK',
//                closeOnConfirm: true
//            },
//            function() {
//                $('.btnDelKinhNghiem').prop('disabled', true);
//                let id = elm.attr('data-id');
//                $.post(URL_ROOT + 'ung-vien/taikhoan/xoaKinhNghiem', {
//                    'key': id
//                }, function(r) {
//                    if (r.status == 1) {
//                        refeshKN(r.data);
//                        updateDiem(r.diem);
//                        if (r.data.length == 0) {
//                            $("#kinhnghiem").find(".fa-check-circle").removeClass("fa-check-circle").addClass('fa-exclamation-circle').css({
//                                color: "red"
//                            });
//                        }
//                    } else {
//                        swal({
//                                title: 'Error',
//                                text: r.message + '\nVui lòng liên hệ quản trị viên để biết thêm.',
//                                type: 'error',
//                                showCancelButton: false,
//                                confirmButtonColor: '#DD6B55',
//                                confirmButtonText: 'OK',
//                                closeOnConfirm: true
//                            },
//                            function() {});
//                    }
//                    $('.btnDelKinhNghiem').prop('disabled', false);
//                }, 'JSON');
//            });
//    });

//    $(document).on('click', '.btnTrinhDo', function() {
//        let el = $(this);
//        $('#modalGeneralHoSo .modal-content').html(`
//            <div class="lds-ellipsis" style="margin: 20px auto">
//                <div></div>
//                <div></div>
//                <div></div>
//                <div></div>
//            </div>
//        `);
//        $('#modalGeneralHoSo').modal('show');
//        id = $(this).attr('data-id');
//        $.post(URL_ROOT + 'ung-vien/taikhoan/editTrinhDo', {
//            'id': id
//        }, function(r) {
//            if (r.length < 100) {
//                call_noti('Hoàn thành cập nhật thông tin hồ sơ trước.', 'error');
//                $('#modalGeneralHoSo').modal('hide');
//            } else {
//                $('#modalGeneralHoSo .modal-content').html(r);
//            }
//        });
//    });

//    $(document).on('click', '.btnDelTrinhDo', function() {
//        elm = $(this);
//        swal({
//                title: 'Bạn có chắc muốn xóa thông tin này?',
//                text: '',
//                type: 'warning',
//                showCancelButton: true,
//                confirmButtonColor: '#DD6B55',
//                confirmButtonText: 'OK',
//                closeOnConfirm: true
//            },
//            function() {
//                $('.btnDelTrinhDo').prop('disabled', true);
//                id = elm.attr('data-id');
//                $.post(URL_ROOT + 'ung-vien/taikhoan/xoaTrinhDo', {
//                    'key': id
//                }, function(r) {
//                    if (r.status == 1) {
//                        refeshTD(r.data);
//                        updateDiem(r.diem);
//                        if (r.data.length == 0) {
//                            $("#bangcap").find(".fa-check-circle").removeClass("fa-check-circle").addClass('fa-exclamation-circle').css({
//                                color: "red"
//                            });
//                        }
//                    } else {
//                        swal({
//                                title: 'Error',
//                                text: r.message + '\nVui lòng liên hệ quản trị viên để biết thêm.',
//                                type: 'error',
//                                showCancelButton: false,
//                                confirmButtonColor: '#DD6B55',
//                                confirmButtonText: 'OK',
//                                closeOnConfirm: true
//                            },
//                            function() {});
//                    }
//                    $('.btnDelTrinhDo').prop('disabled', false);
//                }, 'JSON');
//            });
//    });

//    $(document).on('click', '.btnThamKhao', function() {
//        let elm = $(this);
//        $('#modalGeneralHoSo .modal-content').html(`
//            <div class="lds-ellipsis" style="margin: 20px auto">
//                <div></div>
//                <div></div>
//                <div></div>
//                <div></div>
//            </div>
//        `);
//        $('#modalGeneralHoSo').modal('show');
//        id = $(this).attr('data-id');
//        $.post(URL_ROOT + 'ung-vien/taikhoan/editThamKhao', {
//            'id': id
//        }, function(r) {
//            $('#modalGeneralHoSo .modal-content').html(r);
//        });
//    });

//    $(document).on('click', '.btnDelThamKhao', function() {
//        elm = $(this);
//        swal({
//                title: 'Bạn có chắc muốn xóa thông tin này?',
//                text: '',
//                type: 'warning',
//                showCancelButton: true,
//                confirmButtonColor: '#DD6B55',
//                confirmButtonText: 'OK',
//                closeOnConfirm: true
//            },
//            function() {
//                $('.btnDelThamKhao').prop('disabled', true);
//                id = elm.attr('data-id');
//                $.post(URL_ROOT + 'ung-vien/taikhoan/xoaThamKhao', {
//                    'key': id
//                }, function(r) {
//                    if (r.status == 1) {
//                        refeshTK(r.data);
//                        if (r.data.length == 0) {
//                            $("#thamkhao").find(".fa-check-circle").removeClass("fa-check-circle").addClass('fa-exclamation-circle').css({
//                                color: "red"
//                            });
//                        }
//                    } else {
//                        swal({
//                                title: 'Error',
//                                text: r.message + '\nVui lòng liên hệ quản trị viên để biết thêm.',
//                                type: 'error',
//                                showCancelButton: false,
//                                confirmButtonColor: '#DD6B55',
//                                confirmButtonText: 'OK',
//                                closeOnConfirm: true
//                            },
//                            function() {});
//                    }
//                    $('.btnDelThamKhao').prop('disabled', false);
//                }, 'JSON');
//            });
//    });

//    $(document).on('click', '.btnNgoaiNgu', function() {
//        let eml = $(this);
//        $('#modalGeneralHoSo .modal-content').html(`
//            <div class="lds-ellipsis" style="margin: 20px auto">
//                <div></div>
//                <div></div>
//                <div></div>
//                <div></div>
//            </div>
//        `);
//        $('#modalGeneralHoSo').modal('show');
//        id = $(this).attr('data-id');
//        $.post(URL_ROOT + 'ung-vien/taikhoan/editNgoaiNgu', {
//            'id': id
//        }, function(r) {
//            $('#modalGeneralHoSo .modal-content').html(r);
//        });
//    });

//    $(document).on('click', '.btnDelNgoaiNgu', function() {
//        elm = $(this);
//        swal({
//                title: 'Bạn có chắc muốn xóa ngoại ngữ này?',
//                text: '',
//                type: 'warning',
//                showCancelButton: true,
//                confirmButtonColor: '#DD6B55',
//                confirmButtonText: 'OK',
//                closeOnConfirm: true
//            },
//            function() {
//                $('.btnDelNgoaiNgu').prop('disabled', true);
//                id = elm.attr('data-id');
//                $.post(URL_ROOT + 'ung-vien/taikhoan/xoaNgoaiNgu', {
//                    'ngoaingu_id': id
//                }, function(r) {
//                    if (r.status == 1) {
//                        refeshNN(r.data);
//                        updateDiem(r.diem);
//                        if (r.data.length == 0) {
//                            $("#ngoaingu").find(".fa-check-circle").removeClass("fa-check-circle").addClass('fa-exclamation-circle').css({
//                                color: "red"
//                            });
//                        }
//                    } else {
//                        swal({
//                                title: 'Error',
//                                text: r.message + '\nVui lòng liên hệ quản trị viên để biết thêm.',
//                                type: 'error',
//                                showCancelButton: false,
//                                confirmButtonColor: '#DD6B55',
//                                confirmButtonText: 'OK',
//                                closeOnConfirm: true
//                            },
//                            function() {});
//                    }
//                    $('.btnDelNgoaiNgu').prop('disabled', false);
//                }, 'JSON');
//            });
//    });

//    $('#frmHD').submit(function() {
//        let checkAtleast = false;
//        let textEmpty = false;
//        $('#divKyNang input[type="checkbox"]').each(function() {
//            if ($(this).is(":checked")) {
//                checkAtleast = true;
//            }
//        });

//        $("#kncm #row-kncm").each(function() {
//            let elm = $(this);
//            let checkbox = elm.find('input[name="kynangchuyenmon[check][]"]');
//            checkbox.each(function(idex, item) {
//                let textbox = $(item).closest('.checkbox').find('input[name="kynangchuyenmon[text][]"]').val();
//                if ($(item).is(":checked")) {
//                    if (textbox.replace(/\s/g, '') == '') {
//                        checkAtleast = true;
//                        textEmpty = true;
//                    }
//                }
//            });
//        });

//        if (!checkAtleast) {
//            call_noti('Vui lòng chọn ít nhất một kỹ năng', 'warning');
//            return false;
//        }

//        if (textEmpty) {
//            alert('Vui nhập tên kỹ năng chuyên môn');
//            return false;
//        }

//        elm = $('#frmHD .btnSub');
//        // btnlinkload(elm);
//        $.post(URL_ROOT + "ung-vien/taikhoan/updateHD", $(this).serializeArray(), function(r) {
//            // btnlinkthanhcong(elm, 'Lưu');
//            if (r.status == 1) {
//                $("#kncm #row-kncm").find('input[name="kynangchuyenmon[check][]"]').each(function() {
//                    let elmCheckBox = $(this);
//                    let textbox = elmCheckBox.closest('label').next().val();
//                    if (!elmCheckBox.is(':checked') || (elmCheckBox.is(':checked') && textbox.replace(/\s/g, '') == '')) {
//                        elmCheckBox.closest('.checkbox.skill-input').remove();
//                    }
//                });
//                call_noti('Lưu thông tin thành công', 'success');
//                elm.closest(".panel").find(".fas").removeClass("fa-exclamation-circle").addClass('fa-check-circle').css('color', '#0099cc');
//                updateDiem(r.diem);
//            } else
//                call_noti(r.message, 'error');
//        }, 'JSON');
//        return false;
//    });

//    $('#divKyNang .rating span').click(function(e) {
//        e.preventDefault();
//        let value = $(this).attr('data-value');
//        let divRating = $(this).parent();
//        // xử lý check nếu checkbox chưa checked
//        let labelChecbox = divRating.prev();
//        let elmInputCheckbox = labelChecbox.find('input[type="checkbox"]');
//        if (!elmInputCheckbox.is(':checked')) {
//            elmInputCheckbox.prop('checked', true);
//            labelChecbox.addClass('checked');
//            elmInputCheckbox.change();
//        }
//        divRating.find('span').removeClass('selected');
//        divRating.prev().find('input[name="rating[]"]').val(value);
//        if (elmInputCheckbox.is(':checked')) {
//            divRating.find('span').each(function() {
//                let value2 = $(this).attr('data-value');
//                if (value2 <= value) {
//                    $(this).addClass('selected');
//                }
//            });
//        }
//        if ($('#divKyNang input[type="checkbox"]:checked').length > limitSkill) {
//            alert("Chọn tối đa " + limitSkill + " kỹ năng");
//            $(this).prop('checked', false);
//            divStar.removeAttr('name');
//            divRating.find('span').removeClass('selected');
//            return false;
//        }
//    });

//    $('#kncm').on("click", ".rating span", function(e) {
//        value = $(this).attr('data-value');
//        divRating = $(this).parent();
//        // xử lý check nếu checkbox chưa checked
//        labelChecbox = divRating.prev().prev();
//        elmInputCheckbox = labelChecbox.find('input[type="checkbox"]');
//        if (!elmInputCheckbox.is(':checked')) {
//            elmInputCheckbox.prop('checked', true);
//            labelChecbox.addClass('checked');
//            elmInputCheckbox.change();
//        }
//        divRating.find('span').removeClass('selected');
//        divRating.prev().prev().find('input[name="kynangchuyenmon[rating][]"]').val(value);
//        divRating.find('span').each(function() {
//            value2 = $(this).attr('data-value');
//            if (value2 <= value) {
//                $(this).addClass('selected');
//            }
//        });
//        e.preventDefault();
//    });

//    $('#divKyNang input[type="checkbox"]').change(function() {
//        let divStar = $(this).next();
//        let divRating = $(this).parent().next();
//        if ($(this).is(":checked")) {
//            divStar.attr('name', 'rating[]');
//            divStar.val(1);
//            divRating.find('span').removeClass('selected');
//            divRating.find('span').last().addClass('selected');
//        } else {
//            divStar.removeAttr('name');
//            divRating.find('span').removeClass('selected');
//        }
//        if ($('#divKyNang input[type="checkbox"]:checked').length > limitSkill) {
//            alert("Chọn tối đa " + limitSkill + " kỹ năng");
//            $(this).prop('checked', false);
//            divStar.removeAttr('name');
//            divRating.find('span').removeClass('selected');
//            return false;
//        }
//    });

//    $('#kncm').on('change', 'input[type="checkbox"]', function() {
//        let divStar = $(this).closest('label').next().next();
//        let labelCheckbox = $(this).closest('label').next();
//        let divRating = labelCheckbox.next();
//        if ($(this).is(":checked")) {
//            labelCheckbox.prev().find('.num-rating').attr('name', 'kynangchuyenmon[rating][]');
//            divStar.val(1);
//            divRating.find('span').removeClass('selected');
//            divRating.find('span').last().addClass('selected');
//            labelCheckbox.prev().addClass('checked');
//            labelCheckbox.attr('name', 'kynangchuyenmon[text][]');
//        } else {
//            labelCheckbox.prev().removeClass('checked');
//            divStar.removeAttr('name');
//            divRating.find('span').removeClass('selected');
//            labelCheckbox.removeAttr('name');
//            labelCheckbox.prev().find('.num-rating').removeAttr('name');
//        }
//    });

//    $('#ungvien_hienthihs').click(function() {
//        elm = $(this);
//        hienthi = $(this).is(":checked") ? 1 : 0;
//        $.post(URL_ROOT + 'ung-vien/taikhoan/updateHienThi', {
//            'value': hienthi
//        }, function(r) {
//            if (r.status == 1) {
//                if (r.state == 0) {
//                    call_noti('Hồ sơ của bạn đã được ẩn', 'success');
//                } else {
//                    call_noti('Hồ sơ của bạn đã được hiển thị', 'success');
//                }
//            } else {
//                call_noti(r.message, 'error');
//                if (hienthi == 1) {
//                    elm.prop('checked', false);
//                } else {
//                    elm.prop('checked', true);
//                }
//            }
//        }, 'JSON');
//    });

//    $("#addMoreSkill").click(function() {
//        let html = `
//   <div class="checkbox skill-input">
//      <label checked>
//          <input type="checkbox" value="1" name="kynangchuyenmon[check][]" checked>
//          <input type="hidden" value="3" name="kynangchuyenmon[rating][]" class="num-rating">
//      </label>
//      <input class="form-control" type="text" name="kynangchuyenmon[text][]" minlength="5">
//      <div class="rating">
//         <span data-value="5">
//         ☆
//         </span>
//         <span data-value="4">
//         ☆
//         </span>
//         <span data-value="3" class="selected">
//         ☆
//         </span>
//         <span data-value="2" class="selected">
//         ☆
//         </span>
//         <span data-value="1" class="selected">
//         ☆
//         </span>
//      </div>
//   </div>`;
//        $("#row-kncm").append(html);
//    });

//    $('.expand-kynang').click(function(e) {
//        let elm = $(this).attr('data-target');
//        $(elm).find('.hidden-item').slideToggle();
//        if ($(this).hasClass('active')) {
//            $(this).removeClass('active');
//            $(this).find('.expanded-divider_noborder').text('Xem thêm');
//        } else {
//            $(this).addClass('active');
//            $(this).find('.expanded-divider_noborder').text('Rút gọn');
//        }
//    });

//});
