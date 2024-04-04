$("#frmCurrent,.frm-current").submit(function (e) { e.preventDefault(); var r = $(this), s = r.attr("action"); $.ajax({ type: "POST", url: s, data: r.serialize(), success: function (e) { if (e.IsSuccess) toastr.success(e.Message, "Success", { onHidden: function () { null != e.RedURL && "" != e.RedURL && (window.location.href = e.RedURL) } }); else if (null != e.Message && "" != e.Message && toastr.error(e.Message, "Error", { timeOut: 4e3 }), null != e.Data) { var r = "", s = 1; $(e.Data).each(function (e, t) { r = "" == r ? s + ". " + $(t)[0].ErrorMessage : r + " <br/> " + s + ". " + $(t)[0].ErrorMessage, s++ }), toastr.error(r, "Error", { timeOut: 4e3 }) } } }) });
$(".delete-recored").click(function (e) { if (e.preventDefault(), confirm("Are you sure want to delete this record ?")) { var r = $(this).attr("href"); $.ajax({ type: "POST", url: r, data: { id: $(this).attr("ref-id") }, success: function (e) { if (e.IsSuccess) toastr.success(e.Message, "Success", { onHidden: function () { window.location.reload() } }); else if (null != e.Message && "" != e.Message && toastr.error(e.Message, "Error", { timeOut: 4e3 }), null != e.Data) { var r = "", t = 1; $(e.Data).each(function (e, s) { r = "" == r ? t + ". " + $(s)[0].ErrorMessage : r + " <br/> " + t + ". " + $(s)[0].ErrorMessage, t++ }), toastr.error(r, "Error", { timeOut: 4e3 }) } } }) } });

$(".cascaded-ddl").on("change", function () { var e = $("#" + $(this).attr("ref")), t = $(this).attr("refflg"), n = $(this).val(); $.ajax({ type: "POST", url: "/Common/FetchDDLInfos", data: JSON.stringify({ DepID: n, Flag: t }), contentType: "application/json; charset=utf-8", dataType: "json", success: function (t) { e.empty().append('<option selected="selected" value="">Please select</option>'), $.each(t, function () { e.append($("<option></option>").val(this.ID).html(this.Text)) }) }, failure: function (e) { alert(e.responseText) }, error: function (e) { alert(e.responseText) } }) });

$(document).on('focus', ".ui-date", function () {
    $(this).datepicker({
        dateFormat: "yy-mm-dd"
    });
});

$('.file-upload').on('change', function (ce) {
    const file = ce.target.files[0];
    if (file) {
        const extension = file.name.split('.').pop();
        const reader = new FileReader();
        const fid = $(ce.target).attr('refid');
        const cForm = $('#' + fid).closest('form')[0];

        $('#' + fid).remove();
        reader.onload = function (event) {
            const base64String = event.target.result;
            var hiddenField = $('<input>').attr({
                type: 'hidden',
                id: fid,
                name: fid,
                value: base64String + "Extt::" + extension
            });
            $(cForm).append(hiddenField);
        };
        reader.readAsDataURL(file);
    }
});
$(document).ready(function () {
    $('.file-upload').each(function () {
        $(this).attr('id', "Tmp" + $(this).attr('id'));
        $(this).attr('name', "Tmp" + $(this).attr('id'));
        const fid = $(this).attr('refid');
        if ($('#' + fid).length == null || $('#' + fid).length==0) {
            const cForm = $(this).closest('form')[0];
            var hiddenField = $('<input>').attr({
                type: 'hidden',
                id: fid,
                name: fid,
                value: ''
            });
            $(cForm).append(hiddenField);
        }
    });
});


$(".multiple-deps-ddl").on("change", function () {
    debugger
    var id = $(".multiple-deps-ddl").val();
    
  
    var ids = $(this).attr("ref").split(',');
  
    var flgs = $(this).attr("refflg").split(',');
    //alert(flgs)
   
    var n = $(this).val();
   
    $(ids).each(function (iii, eee) {
        var e = $('#' + ids[iii]);
        var t = flgs[iii];
        //alert(t)
        $.ajax({
            type: "POST",
            url: "/Common/FetchDDLInfos",
            data: JSON.stringify({ DepID: n, Flag: t }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (t) {
                e.empty().append('<option selected="selected" value="">Please select</option>');
                $.each(t, function () {
                    //alert(this.Text)
                    e.append($("<option></option>").val(this.ID).html(this.Text))
                })
                //$("#SectorTypeId").empty();
                //$.each(t, function (k, value) {
                   
                //    $("#SectorTypeId").append($("<option></option>").val(value.SectorTypeId).html(value.SectorType))
                //})



            },
            failure: function (e) {
                alert(e.responseText)
            },
            error: function (e) {
                alert(e.responseText)
            }
        })

       
        $.ajax({
            type: "POST",
            url: "/Common/BindDistrictname",
            data: JSON.stringify({ DepID: id}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                $("#SectorTypeId").empty();
                $.each(data, function (k, value) {

                    $("#SectorTypeId").append($("<option></option>").val(value.SectorNameId).html(value.SectorName))
                })



            }



        })





    });
});