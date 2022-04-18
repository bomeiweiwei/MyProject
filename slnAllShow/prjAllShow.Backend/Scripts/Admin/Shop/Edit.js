const vm = Vue.createApp({
    //el: '#app',
    //data: {
    //    ShThePicImg: '',
    //    ShLogoPicImg: '',
    //    ShAdPicImg: ''
    //},
    data() {
        return {
            ShThePicImg: '',
            ShLogoPicImg: '',
            ShAdPicImg: ''
        }
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
            this.ShThePicImg = event.target.result;
        },
        handelFileUpload2(event) {
            var file = event.target.files[0];
            if (file && file.type.match(/^image\/(png|jpeg)$/)) {
                //this.preview = window.URL.createObjectURL(file);
                const reader = new FileReader(); //建立FileReader 監聽 Load 事件
                reader.addEventListener('load', this.imageLoader2);
                reader.readAsDataURL(file);
            }
        },
        imageLoader2(event) {
            this.ShLogoPicImg = event.target.result;
        },
        handelFileUpload3(event) {
            var file = event.target.files[0];
            if (file && file.type.match(/^image\/(png|jpeg)$/)) {
                //this.preview = window.URL.createObjectURL(file);
                const reader = new FileReader(); //建立FileReader 監聽 Load 事件
                reader.addEventListener('load', this.imageLoader3);
                reader.readAsDataURL(file);
            }
        },
        imageLoader3(event) {
            this.ShAdPicImg = event.target.result;
        }
    }
}).mount('#app');

$(document).ready(function () {
    if ($('#ChangePwd').is(":checked")) {
        $("#ShPwd").attr('disabled', false);
    } else {
        $("#ShPwd").attr('disabled', true);
    }
    //$("#ShClass").multiselect();
    new SlimSelect({
        select: '#ShClassListID'
    })
});

$('#ChangePwd').change(function () {
    if (this.checked) {
        $("#ShPwd").attr('disabled', false);
    } else {
        $("#ShPwd").attr('disabled', true);
    }
});