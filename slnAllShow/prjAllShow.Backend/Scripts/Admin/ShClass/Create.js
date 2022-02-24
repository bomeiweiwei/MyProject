var apiUrl = '/api/ShClass';

const app = {
    data() {
        return {
            List: null,
            ListData: [],
            ShClassName: "預設商店類別名稱"
        }
    },
    mounted() {
        baseInstance.get(apiUrl + "/GetByPage/all")
            .then(response => {
                this.List = response.data.resultData;
                this.ListData = [];
                this.List.forEach((item, index) => {
                    this.ListData.push(item.shClassName);
                });
            });
    },
    methods: {
        handleSubmit() {
            if (this.ListData.includes(this.ShClassName)) {
                alert('商店類別已存在');
            } else {
                var data = { "ShClassName": this.ShClassName };
                //console.log(data);
                baseInstance.post(apiUrl, data)
                    .then(function (response) {
                        alert("商店類別建立！！！");
                        window.parent.refreshPage();
                        window.parent.closePopupModal();
                    })
                    .catch(function (error) {
                        //console.log(error);
                        if (error.response) {
                            console.log(error.response.status);
                        }
                    });
                //$.ajax({
                //    type: "POST",
                //    url: apiUrl,
                //    headers: {
                //        'RequestVerificationToken': $('input:hidden[name="__RequestVerificationToken"]').val()
                //    },
                //    data: JSON.stringify(data),
                //    contentType: "application/json;charset=utf-8",
                //    statusCode: {
                //        400: function (xhr, status, error) {
                //            alert('400 Error happened');
                //        },
                //        415: function (xhr, status, error) {
                //            alert('415 Error happened');
                //        }
                //    }
                //}).done(function (obj) {
                //    alert("商店類別建立！！！");
                //    window.parent.closePopupModal();
                //}).fail(function (xhr, status, error) {

                //});                
            }
        }
    }
};

const vm = Vue.createApp(app).mount('#app');