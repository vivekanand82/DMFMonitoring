$(document).on("keypress", ".clsnumbers", function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        $("#clsnumbers").html("Digits Only").show().fadeOut("slow");
        return false;
    }
});
$(document).on("keypress", ".clsnumeric", function (event) {
    if (event.which != 8 && event.which != 0 && (event.which != 46 || $(control).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
$(document).on("keypress", ".clsletters", function (e) {
    var inputValue = e.which;
    // allow letters and whitespaces only.
    if (!(inputValue >= 65 && inputValue <= 122) && (inputValue != 32 && inputValue != 0)) {
        e.preventDefault();
    }
});
$(document).on("keypress", ".clsalphanumeric", function (event) {
    //$(".clsalphanumeric").keypress(function (event) {
    var regex = new RegExp("^[a-zA-Z0-9 . ; : - ? /']+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        //return false;
    }
});

$(document).on("keypress", ".only-alphanumerics", function (event) {
    //$(".clsalphanumeric").keypress(function (event) {
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        //return false;
    }
});

$(document).on("keypress", ".remove-spaces", function (event) {
    var key = event.which;
    if (key==32)
        event.preventDefault();
});

function mobileValid(_Mobile) {
    var regMobile1 = new RegExp("^[0-9]{10}$");
    var regMobile2 = new RegExp("^[5-9]{1}[0-9]{8}$");
    return regMobile1.test(_Mobile) || regMobile2.test(_Mobile);
}
function emailValid(_email) {
    var regEmail = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;// /^[_\.0-9a-zA-Z-]+@([0-9a-zA-Z][0-9a-zA-Z-]+\.)+[a-zA-Z]{2,6}$/i;
    return regEmail.test(_email);
}
function nameValid(_str) {
    _str = _str.replace("/", "").replace(".", "");
    var reg = /^[a-zA-Z ]+$/;
    return reg.test(_str);
}
function pinCodeValid(_number) {
    var reg = new RegExp("^[1-9]{1}[0-9]{5}$");
    return reg.test(_number);
}
function isPositiveInteger(n) {

    return 0 === n % (!isNaN(parseInt(n)) && 0 <= ~ ~n);
}
function isDate(value) {
    try {
        //Change the below values to determine which format of date you wish to check. It is set to dd/mm/yyyy by default.
        var DayIndex = 0;
        var MonthIndex = 1;
        var YearIndex = 2;

        value = value.replace(/-/g, "/").replace(/\./g, "/");
        var SplitValue = value.split("/");
        var OK = true;
        if (!(SplitValue[DayIndex].length == 1 || SplitValue[DayIndex].length == 2)) {
            OK = false;
        }
        if (OK && !(SplitValue[MonthIndex].length == 1 || SplitValue[MonthIndex].length == 2)) {
            OK = false;
        }
        if (OK && SplitValue[YearIndex].length != 4) {
            OK = false;
        }
        if (OK) {
            var Day = parseInt(SplitValue[DayIndex], 10);
            var Month = parseInt(SplitValue[MonthIndex], 10);
            var Year = parseInt(SplitValue[YearIndex], 10);

            if (OK = (Year > 1900)) {
                if (OK = (Month <= 12 && Month > 0)) {
                    var LeapYear = (((Year % 4) == 0) && ((Year % 100) != 0) || ((Year % 400) == 0));
                    if (Month == 2) {
                        OK = LeapYear ? Day <= 29 : Day <= 28;
                    }
                    else {
                        if ((Month == 4) || (Month == 6) || (Month == 9) || (Month == 11)) {
                            OK = (Day > 0 && Day <= 30);
                        }
                        else {
                            OK = (Day > 0 && Day <= 31);
                        }
                    }
                }
            }
        }
        return OK;
    }
    catch (e) {
        return false;
    }
}

//Added by Madhu Gone
function validatedate(value) {

    try {
        //Change the below values to determine which format of date you wish to check. It is set to dd/mm/yyyy by default.
        var DayIndex = 0;
        var MonthIndex = 1;
        var YearIndex = 2;

        //alert(value);

        // value = value.replace(/-/g, "/").replace(/\./g, "/");
        // alert(value);
        var SplitValue = value.split("-");
        var OK = true;
        if (!(SplitValue[DayIndex].length == 1 || SplitValue[DayIndex].length == 2)) {
            OK = false;
        }
        if (OK && !(SplitValue[MonthIndex].length == 1 || SplitValue[MonthIndex].length == 2)) {
            OK = false;
        }
        if (OK && SplitValue[YearIndex].length != 4) {
            OK = false;
        }
        if (OK) {
            var Day = parseInt(SplitValue[DayIndex], 10);
            var Month = parseInt(SplitValue[MonthIndex], 10);
            var Year = parseInt(SplitValue[YearIndex], 10);

            if (OK = (Year > 1900)) {
                if (OK = (Month <= 12 && Month > 0)) {
                    var LeapYear = (((Year % 4) == 0) && ((Year % 100) != 0) || ((Year % 400) == 0));
                    if (Month == 2) {
                        OK = LeapYear ? Day <= 29 : Day <= 28;
                    }
                    else {
                        if ((Month == 4) || (Month == 6) || (Month == 9) || (Month == 11)) {
                            OK = (Day > 0 && Day <= 30);
                        }
                        else {
                            OK = (Day > 0 && Day <= 31);
                        }
                    }
                }
            }
        }
        return OK;
    }
    catch (e) {
        return false;
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8)
        return false;
    return true;
}
function isTextKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode

    if (charCode > 31 && (charCode < 48 || charCode > 57) || charCode == 8)
        return true;
    return false;
}
function isValidName(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode

    if (charCode > 31 && (charCode < 48 || charCode > 57) || charCode == 8 || charCode == 191 || charCode == 190)
        return true;
    return false;
}
function AllowDeleteAndBackSpace(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode

    if (charCode == 8 || charCode == 127)
        return true;
    return false;

}

function onlyAlphabets(e) {
    try {

        var charCode = (e.which) ? e.which : e.keyCode

        if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 8 || charCode == 9 || charCode == 32 || charCode == 37 || charCode == 39 || charCode == 46))
            return true;
        else
            return false;
    }
    catch (err) {
        alert(err.Description);
    }

}

function isAlphaNumericKey(e) {
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}

function DecimalValidate(event, control) {
    $(control).val($(control).val().replace(/[^0-9\.]/g, ''));
    if (event.which != 8 && event.which != 0 && (event.which != 46 || $(control).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
}

function AddRowWithFieldsInTableBody(table_element) {
    //alert("in function");
    var IfSelect2Enabled = false;
    $(table_element).find('select').each(function () {
        if ($(this).data('select2')) {
            $(this).select2('destroy');
            IfSelect2Enabled = true;
        }
    });
    //$('select').select2('destroy');
    var _fields_panel = table_element.find("tbody tr").first();
    //_fields_panel = 
    table_element.find("tbody").append('<tr>' + _fields_panel.html() + '</tr>');

    if (table_element.find("tbody tr").length > 1 && table_element.find(".RemoveCurrentRow").length > 0) {
        table_element.find(".RemoveCurrentRow").show();
    }
    ResetFieldValues(table_element.find("tbody tr:last"));
    SetFieldNameAndID(table_element);

    //if (IfSelect2Enabled) {
    //    $(table_element).find('.custom-select').select2({
    //        selectOnClose: true
    //    });
    //}    
}
function RemoveRowWithFieldsFromTableBody(current_element, table_element) {
    current_element.closest("tr").remove();
    if (table_element.find("tbody tr").length == 1 && table_element.find(".RemoveCurrentRow").length > 0) {
        table_element.find(".RemoveCurrentRow").hide();
    }
    SetFieldNameAndID(table_element);
}

function SetFieldNameAndID(table_element) {
    var i = 1;
    var mn = 1;
    var rows = table_element.find("tbody tr");
    rows.each(function () {
        var td = $(this).find('td');
        td.each(function () {
            var element_name = "";
            var element;
            var IsDDL = false;
            if ($(this).find('select').length > 0) {
                element = $(this).find('select');
                element_name = element.attr("name").split('_')[0];
                IsDDL = true;
            } else if ($(this).find('input').length > 0) {
                element = $(this).find('input');
                element_name = element.attr("name").split('_')[0];
            } else if ($(this).find('textarea').length > 0) {
                element = $(this).find('textarea');
                element_name = element.attr("name").split('_')[0];
            } else if ($(this).find('a').length > 0) {
                element = $(this).find('a');
                element_name = element.attr("id").split('_')[0];
            }
            else if ($(this).find('label').length > 0) {
                element = $(this).find('label');
                element_name = element.attr("id").split('_')[0];
            }

            if (element_name.length > 0) {
                element.attr("name", element_name + "_" + i);
                element.attr("id", element_name + "_" + i);

                if (IsDDL && ($('#' + element_name + "_" + i).val() == null || $('#' + element_name + "_" + i).val() == "")) {
                    $('#' + element_name + "_" + i + ' option').eq(0).prop('selected', true);
                }
                //if (element.attr("message")) {
                //    var message = element.attr("message");
                //    element.attr("message", message.replace("{row}", "" + i));
                //}
            }
        });
        i++;
    });
    //$('select').select2();
}

//function SetFieldNameAndID(table_element) {
//    var i = 1;
//    var rows = table_element.find("tbody tr");
//    rows.each(function () {
//        var td = $(this).find('td');
//        td.each(function () {
//            var element_name = "";
//            var element;
//            if ($(this).find('select').length > 0) {
//                element = $(this).find('select');
//                element_name = element.attr("name").split('_')[0];
//            } else if ($(this).find('input').length > 0) {
//                element = $(this).find('input');
//                element_name = element.attr("name").split('_')[0];
//            } else if ($(this).find('textarea').length > 0) {
//                element = $(this).find('textarea');
//                element_name = element.attr("name").split('_')[0];
//            } else if ($(this).find('a').length > 0) {
//                element = $(this).find('a');
//                element_name = element.attr("id").split('_')[0];
//            }

//            if (element_name.length > 0) {
//                element.attr("name", element_name + "_" + i);
//                element.attr("id", element_name + "_" + i);

//                //if (element.attr("message")) {
//                //    var message = element.attr("message");
//                //    element.attr("message", message.replace("{row}", "" + i));
//                //}
//            }
//        });
//        i++;
//    });
//    //$('select').select2();
//}


function SetFieldNameAndIDNew(table_element) {
    var i = 1;
    var rows = table_element.find("tbody tr");
    rows.each(function () {
        var td = $(this).find('td');
        td.each(function () {
            var element_name = "";
            var element;
            if ($(this).find('select').length > 0) {
                element = $(this).find('select');
                element_name = element.attr("name").split('_')[0];
            } else if ($(this).find('input').length > 0) {
                element = $(this).find('input');
                element_name = element.attr("name").split('_')[0];
            } else if ($(this).find('textarea').length > 0) {
                element = $(this).find('textarea');
                element_name = element.attr("name").split('_')[0];
            } else if ($(this).find('a').length > 0) {
                element = $(this).find('a');
                element_name = element.attr("id").split('_')[0];
            }

            if (element_name.length > 0) {
                element.attr("name", element_name + "_" + i);
                element.attr("id", element_name + "_" + i);

                //if (element.attr("message")) {
                //    var message = element.attr("message");
                //    element.attr("message", message.replace("{row}", "" + i));
                //}
            }
        });
        i++;
    });
    //$('select').select2();
}
function AddRowWithFieldsInTableBodyNew(table_element) {
    //alert("in function");
    $('select').each(function () {
        if ($(this).data('select2')) {
            $(this).select2('destroy');
        }
    });
    //$('select').select2('destroy');
    var _fields_panel = table_element.find("tbody tr").first();
    //_fields_panel = 
    table_element.find("tbody").append('<tr>' + _fields_panel.html() + '</tr>');

    if (table_element.find("tbody tr").length > 1 && table_element.find(".RemoveCurrentRow").length > 0) {
        table_element.find(".RemoveCurrentRow").show();
    }
    ResetFieldValues(table_element.find("tbody tr:last"));
    SetFieldNameAndIDNew(table_element);

}

function ResetFieldValues(rows) {
    var i = 1;
    var td = rows.find("td");
    td.each(function () {
        var element;
        if ($(this).find('select').length > 0) {
            element = $(this).find('select');
            element.val("");
        } else if ($(this).find('input').length > 0) {
            element = $(this).find('input');
            if (element.attr("type") != "checkbox") {
                element.val("");
            }
        } else if ($(this).find('textarea').length > 0) {
            element = $(this).find('textarea');
            element.val("");
        }
        else if ($(this).find('a').length > 0) {
            element = $(this).find('a');
            element.val("");
        }
    });
}

//$(document).find("input[type=file]").change(function () {
//    // Get uploaded file extension  
//    var extension = $(this).val().split('.').pop().toLowerCase();
//    // Create array with the files extensions that we wish to upload  
//    var validFileExtensions = ['jpg', 'jpeg', 'png', 'pdf', 'kmz', 'kml','dwg'];
//    //Check file extension in the array.if -1 that means the file extension is not in the list.  
//    if ($.inArray(extension, validFileExtensions) == -1) {
//        alert("कृपया 'jpg', 'jpeg','png', 'pdf', 'kmz', 'kml','dwg' प्रकार की फाइल अपलोड करें और फाइल के नाम से डॉट (.) हटाएँ");
//        // Clear fileuload control selected file  
//        $(this).replaceWith($(this).val('').clone(true));
//        //Disable Submit Button  
//        //$('#Apply').prop('disabled', true);
//    }
//    var thisfile = $(this).attr('filesize');
//    var thisattr = $(this).attr('loipermitsize');
//    var MAX_FILE_SIZE = 0;
//    var alertmsg ="";
//    if (thisfile == "10") {
//        MAX_FILE_SIZE = 10 * 1024 * 1024; // 10MB
//        alertmsg = "कृपया फाइल की साइज 10 MB  तक अपलोड करें";
//    }
//    if (thisfile == "5") {
//        MAX_FILE_SIZE = 5 * 1024 * 1024; // 10MB
//        alertmsg = "कृपया फाइल की साइज 5 MB  तक अपलोड करें";
//    }
//    else if (thisattr == "3") {
//        MAX_FILE_SIZE = 3 * 1024 * 1024; // 3MB
//        alertmsg = "कृपया फाइल की साइज 3 MB  तक अपलोड करें";
//    }
//    else {
//        MAX_FILE_SIZE = 1 * 1024 * 1024; // 1MB
//        alertmsg = "कृपया फाइल की साइज 1 MB  तक अपलोड करें";
//    }
//    var fileSize = this.files[0].size;
//    if (fileSize > MAX_FILE_SIZE) {
//        alert(alertmsg);
//        $(this).replaceWith($(this).val('').clone(true));
//    } 
//});
$('.zoomBtn').click(function () {
    var id = $(this).attr("id");
    var thishasclass = $('#iconi_' + id).hasClass('fa fa-times');
    $(".clsicon").removeClass('fa fa-times');
    $(".clsicon").addClass('fa fa-ellipsis-h');
    $(".zoomBtn a").addClass('scale-out');
    if (!thishasclass) {
        $('.zoom-btn_' + id).toggleClass('scale-out');

        if (!$('.zoom-btn_' + id).hasClass('scale-out')) {
            $('#iconi_' + id).removeClass('fa fa-ellipsis-h');
            $('#iconi_' + id).addClass('fa fa-times');
        }
        else {
            $('#iconi_' + id).removeClass('fa fa-times');
            $('#iconi_' + id).addClass('fa fa-ellipsis-h');
        }
    }
});