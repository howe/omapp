﻿function showMsg(msg) {
    $.fn.modal({
        title: '提示信息',
        content: function (o, e) {
            o.content.html(msg);
        },
        cancelText: '关闭',
    });
}

function hideAllButton() {
    $(":submit").css('display','none'); 
    return false;
}