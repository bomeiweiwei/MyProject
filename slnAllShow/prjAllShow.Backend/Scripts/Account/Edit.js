$(document).ready(function () {
    if ($('#ChangePwd').is(":checked")) {
        $("#Password").attr('disabled', false);
    } else {
        $("#Password").attr('disabled', true);
    }
});

$('#ChangePwd').change(function () {
    if (this.checked) {
        $("#Password").attr('disabled', false);
    } else {
        $("#Password").attr('disabled', true);
    }
});