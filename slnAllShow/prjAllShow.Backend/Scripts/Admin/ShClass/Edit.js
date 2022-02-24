var apiUrl = '/api/ShClass';

const app = {
    data() {
        return {
            Id: 0,
            ShClassName: "",
            List: null,
            ListData: [],
        }
    },
    mounted() {
        this.Id = $("#Id").val();
        this.ShClassName = $("#ShClassName").val();
        //baseInstance.get(apiUrl + "/GetById/" + this.Id)
        //    .then(response => {
        //        var obj = response.data.resultData;
        //        this.ShClassName = obj.shClassName;
        //    });
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
            var newName = $("#ShClassName").val();
            if (newName != this.ShClassName && this.ListData.includes(newName)) {
                alert('商店類別已存在');
            } else {
                var data = { "ShClassName": newName };
                var _url = apiUrl + "/" + this.Id;

                baseInstance.put(_url, data)
                    .then(function (response) {
                        alert('修改完成');
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
                //    type: "PUT",
                //    url: _url,
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
                //    alert("修改完成！！！");
                //    window.parent.refreshPage();
                //    window.parent.closePopupModal();
                //}).fail(function (xhr, status, error) {

                //}); 
            }
        }
    }
};

const vm = Vue.createApp(app).mount('#app');