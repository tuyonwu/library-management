$(function () {
    'use strict'

    function setDatePicker(selector) {
        selector.datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'yy/mm/dd',
        });
    }

    function setIntevalDatePicker(selector, bindChangeSelector, bindChangeDateLimit) {
        selector.datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'yy/mm/dd',
        }).on("change", function () {
            bindChangeSelector.datepicker('option', bindChangeDateLimit, selector.datepicker("getDate"));
        });
    }

    //set datepicker
    setDatePicker($("#PublishDate"));
    setIntevalDatePicker($("#BorrowDate"), $("#ReturnDate"), "minDate");
    setIntevalDatePicker($("#ReturnDate"), $("#BorrowDate"), "maxDate");

    //預設書籍狀態為已借出時，借閱人、借閱日期、還書日期欄位可輸入
    if ($("#BookStatus").val() === "S002") {
        $("#Borrower").removeAttr("disabled");
        $("#BorrowDate").removeAttr("disabled");
        $("#ReturnDate").removeAttr("disabled");
    }

    //書籍狀態改變
    $("#BookStatus").on("change", function () {
        var today = new Date();
        if ($(this).val() === "S002") {
            //取消disabled狀態
            $("#Borrower").removeAttr("disabled");
            $("#BorrowDate").removeAttr("disabled");
            $("#ReturnDate").removeAttr("disabled");
            $("#BorrowDate").datepicker("setDate", today);
            today.setDate(today.getDate() + 30);//預設30天後還書
            $("#ReturnDate").datepicker("setDate", today);
        } else {
            $("#Borrower").attr("disabled", true);
            $("#BorrowDate").attr("disabled", true);
            $("#ReturnDate").attr("disabled", true);
            //清空值
            $("#Borrower").val("");
            $("#BorrowDate").datepicker().val("");
            $("#ReturnDate").datepicker().val("");
        }
    });

    //編輯按鈕
    $("#UpdateBtn").on("click", function () {
        //表單驗證
        if (!$("#UpdateBookFrom").valid()) {
            return;
        }

        $.ajax({
            type: "post",
            url: $(this).attr("url"),
            dateType: "json",
            data: {
                BookId: $("#BookId").val(),
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
                BookStatus: $("#BookStatus").val(),
                Borrower: $("#Borrower").val(),
                BorrowDate: $("#BorrowDate").val(),
                ReturnDate: $("#ReturnDate").val()
            },
            success: function (response) {
                switch (response.status) {
                    case "successed":
                        alert(response.msg);
                        window.location = "/Book/QueryBook";
                        break;
                    case "failed":
                        alert(response.msg);
                        break;
                }
            },
            error: function () {
                alert("系統發生錯誤");
            }
        });
    });
});