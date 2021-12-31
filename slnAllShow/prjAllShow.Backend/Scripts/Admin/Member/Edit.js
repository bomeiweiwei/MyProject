var vm = new Vue({
    el: '#app',
    data: {
        MemPicImg: '',
    },
    methods: {
        handelFileUpload(event) {
            var file = event.target.files[0];
            if (file && file.type.match(/^image\/(png|jpeg)$/)) {
                //this.preview = window.URL.createObjectURL(file);
                const reader = new FileReader(); //建立FileReader 監聽 Load 事件
                reader.addEventListener('load', this.imageLoader);
                reader.readAsDataURL(file);
            }
        },
        imageLoader(event) {
            this.MemPicImg = event.target.result;
        }

    }
});

$(document).ready(function () {
    if ($('#ChangePwd').is(":checked")) {
        $("#MemPwd").attr('disabled', false);
    } else {
        $("#MemPwd").attr('disabled', true);
    }
});

$('#ChangePwd').change(function () {
    if (this.checked) {
        $("#MemPwd").attr('disabled', false);
    } else {
        $("#MemPwd").attr('disabled', true);
    }
});