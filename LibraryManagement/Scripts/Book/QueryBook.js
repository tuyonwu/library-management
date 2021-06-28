$(function () {
    'use strict'

    //書籍紀錄
    $("[name='bookRecordBtn']").on('click', function () {
        var url = $(this).attr('url'),
            bookId = $(this).attr('book-id');

        $.get(url, { bookId: bookId })
            .done(function (data) {
                $("#popupWindow").html(data);
            });
    });

    //刪除書籍
    $("[name='deleteBtn']").on('click', function () {
        var selectedBookId = $(this).attr('book-id'),
            url = $(this).attr('url');

        if (confirm("確認是否要刪除此書籍?")) {
            $.post(url, { bookId: selectedBookId })
                .done(function (response) {
                    switch (response.status) {
                        case "IsNotExist":
                            alert(response.msg);
                            window.location = "/Book/QueryBook";
                            break;
                        case "IsBorrow":
                            alert(response.msg);
                            break;
                        case "IsDelete":
                            alert(response.msg);
                            window.location = "/Book/QueryBook";
                            break;
                    }
                })
                .fail(function () {
                    alert('System Error.');
                });
        }
    })

});