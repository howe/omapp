﻿function reset(o) {
    $('input:text').val('');
}

function showDetail(id) {
    window.location.href = "/PlanManage/ExperimentPlanDetail.aspx?id=" + id;
    return false;
}