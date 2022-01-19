const vm = Vue.createApp({
    //el: '#app',
    //data: {
    //    MemPicImg: '',
    //},
    data() {
        return {
            MemPicImg: '',
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
            this.MemPicImg = event.target.result;
        }
        
    }
}).mount('#app');