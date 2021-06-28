$(function () {
    'use strict'

    function setDatePicker(selector) {
        $(selector).datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'yy/mm/dd'
        });
        $(selector).datepicker("setDate", new Date());
    }

    setDatePicker($("#PublishDate"));

    //新增按鈕
    $("#AddBookBtn").on("click", function (e) {
        //表單驗證
        if (!$("#AddBookForm").valid()) {
            return;
        }

        $.ajax({
            type: "post",
            url: $(this).attr("url"),
            dateType: "json",
            data: {
                ClassNo: $("#ClassNo").val(),
                BookName: $("#BookName").val(),
                Author: $("#Author").val(),
                Isbn: $("#Isbn").val(),
                StoreId: $("#StoreId").val(),
                StoreRoom: $("#StoreRoom").val(),
                Description: $("#Description").val(),
                VersionNum: $("#VersionNum").val(),
                PageNum: $("#PageNum").val(),
                PublishDate: $("#PublishDate").val(),
                PublishCountry: $("#PublishCountry").val(),
                Publisher: $("#Publisher").val(),
            },
            success: function (response) {
                switch (response.status) {
                    case "successed":
                        alert(response.msg);
                        window.location = "/Book/QueryBook";
                        break;
                }
            },
            error: function () {
                alert("系統發生錯誤");
            }
        });
    });
});