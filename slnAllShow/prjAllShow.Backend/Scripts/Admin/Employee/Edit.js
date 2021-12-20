$(document).ready(function () {
    if ($('#ChangePwd').is(":checked")) {
        $("#EmpPwd").attr('disabled', false);
    } else {
        $("#EmpPwd").attr('disabled', true);
    }
});

$('#ChangePwd').change(function () {
    if (this.checked) {
        $("#EmpPwd").attr('disabled', false);
    } else {
        $("#EmpPwd").attr('disabled', true);
    }
});