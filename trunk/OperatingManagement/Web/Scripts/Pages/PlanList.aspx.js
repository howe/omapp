﻿function reset(o) {
    $('input:text').val('');
    return false;
}

function showMsg(msg) {
    $.fn.modal({
        title: '提示信息',
        content: function (o, e) {
            o.content.html(msg);
        },
        cancelText: '关闭',
    });
}

function showDetail(id) {
    //window.location.href = "/Views/PlanManage/PlanDetail.aspx?id=" + id;
    window.open("/Views/PlanManage/PlanDetail.aspx?id=" + id);
    return false;
}

function showEdit(planid,plantype) {
    //window.location.href = "/Views/PlanManage/PlanEdit.aspx?id=" + id;
    window.open("/Views/PlanManage/PlanEdit.aspx?planid=" + planid + "&infotype="+plantype);
    return false;
}

function showSend(id,plantype) {
//    var divData = $('#divData');
//    divData.hide();
//    var indicator = $('#tartgetPanel');
//    indicator.show();

    var txtPlanID = $('#txtPlanID'); 
    txtPlanID.val(id);
    var txtPlanID = $('#txtPlanType'); 
    txtPlanID.val(plantype);
    var btn = $('#btnHidden');
    btn.click();
    return false;
}
