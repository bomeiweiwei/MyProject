var authUrl = 'api/GetAuth/getauthtoken';
var apiUrl = '/api/Weather';
var apiCheck = '/api/GetAuth/checktokenvalid';

const paginationBtn = {
    props: ["pageItem"],
    template: `<nav aria-label="Page navigation" v-if="pageItem.currentPage!=0">
              <ul class="pagination justify-content-center">
                <li class="page-item" :class="{'disabled':!pageItem.hasPage}">
                  <a
                    class="page-link"
                    href="#"
                    aria-label="Previous"
                    @click.prevent="toPage(Number(pageItem.currentPage)-1)"
                  >
                    <span aria-hidden="true">&laquo;</span>
                  </a>
                </li>
                <li class="page-item" v-if="pageItem.pageTagHasPre"><a href="#" class="page-link" @click.prevent="toPage((pageItem.currentPageTag-1)*pageItem.showPage)">...</a></li>
                <li class="page-item" v-for="i in pageItem.pageCurrent" :key="i"
                :class="{'active':pageItem.currentPage===i}">
                  <a class="page-link" href="#" @click.prevent="toPage(i)"
                    >{{i}}</a
                  >
                </li>
                <li class="page-item" v-if="pageItem.pageTagHasNext"><a href="#" class="page-link" @click.prevent="toPage((pageItem.currentPageTag+1)*pageItem.showPage)">...</a></li>
                <li class="page-item" :class="{'disabled':!pageItem.hasNext}">
                  <a
                    class="page-link"
                    href="#"
                    aria-label="Next"
                    @click.prevent="toPage(Number(pageItem.currentPage)+1)"
                  >
                    <span aria-hidden="true">&raquo;</span>
                  </a>
                </li>
              </ul>
            </nav>`,
    methods: {
        toPage(item) {
            this.$emit("goToPage", item);
        },
    },
};

const app = {
    //el: '#app',
    data() {
        return {
            WeatherList: null,
            WeatherListData: [],
            pageItem: {
                pageTotal: 0,
                currentPage: 0,
                hasPage: true,
                hasNext: false,
                showPage: 10,
                pageCurrent: [],
                currentPageTag: 1,
                PageTagTotal: 0,
                pageTagHasPre: false,
                pageTagHasNext: false,
            },
            isLoading: false,
        }
    },      
    mounted() {
        baseInstance.get(apiUrl)
            .then(response => {
                //console.log(response);
                this.WeatherList = response.data.resultData;
                this.pagination(this.WeatherList, 1);
            });
    },
    computed: {
        dateFormat() {
            return moment(this.date).format('YYYY-MM-DD');
        }
    },
    components: {
        paginationBtn,
    },
    methods: {
        GetAuth() {
            baseInstance.get(authUrl)
                .then(response => {
                    //console.log(response);
                    window.localStorage.setItem("token", "none"/*response.data.access_token*/);
                    window.localStorage.setItem("refreshtoken", "none"/*response.data.refresh_token*/);
                    alert('驗證完成');

                    window.location.reload();
                });
            //$.ajax({
            //    url: authUrl,
            //    type: 'GET',
            //    success: function (obj, textStatus, xhr) {   
            //        window.localStorage.setItem("token", obj.access_token); 
            //        window.localStorage.setItem("refreshtoken", obj.refresh_token); 
            //        alert('驗證完成');
            //    },
            //    error: function (xhr, textStatus, errorThrown) {
            //        var jsonResponse = JSON.parse(xhr.responseText);
            //        var errors = jsonResponse.errors;
            //        alert(errors[0]);
            //    }
            //});
        },
        CheckAuth() {
            var data = {
                "Token": window.localStorage.getItem("token"),
                "RefreshToken": window.localStorage.getItem("refreshtoken")
            };

            //baseInstance.post(apiCheck, data)
            baseInstance.get(apiCheck)
                .then((response) => {
                    //window.localStorage.setItem("token", response.data.access_token); 
                    //window.localStorage.setItem("refreshtoken", response.data.refresh_token); 
                    alert('重新驗證完成');

                    window.location.reload();
                })
                .catch(function (error) {

                    if (error.response) {
                        // Request made and server responded
                        var statusCode = error.response.status;
                        if (statusCode == 401) {
                            var errors = error.response.data.errors;
                            alert('Error happened: ' + errors[0]);
                        } else {
                            console.log(error.response.data.errors);
                        }
                    }

                });
            /*
            $.ajax({
                type: "POST",
                url: apiCheck,
                headers:{
                    'RequestVerificationToken':$('input:hidden[name="__RequestVerificationToken"]').val()
                },
                data: JSON.stringify(data),
                contentType : 'application/json;',
                statusCode: {
                    401: function(xhr, status, error) {
                            var jsonResponse = JSON.parse(xhr.responseText);
                            var errors = jsonResponse.errors;
                            alert('Error happened: ' + errors[0]);
                    }
                }
            }).done(function( obj ) {
                window.localStorage.setItem("token", obj.access_token); 
                window.localStorage.setItem("refreshtoken", obj.refresh_token); 
                alert('重新驗證完成');                   
            }).fail(function(xhr, status, error) {

            });  
            */
        },
        PageReload() {
            window.location.reload();
        },
        SetDateFormat(format) {
            return moment(this.date).format(format);
            //return `${salut} ${this.firstName} ${this.lastName}`
        },
        pagination(Data, page) {
            this.WeatherListData = [];
            const dataTotal = Data.length;
            const perPage = 10;//15;
            const showPage = 10;
            const pageTotal = Math.ceil(dataTotal / perPage);
            let currentPage = page;
            if (currentPage > pageTotal) {
                currentPage = pageTotal;
            }
            const minData = currentPage * perPage - perPage + 1;
            const maxData = currentPage * perPage;

            Data.forEach((item, index) => {
                const num = index + 1;
                if (num >= minData && num <= maxData) {
                    this.WeatherListData.push(item);
                }
            });

            //分頁一頁顯示十筆頁碼
            this.pageItem.pageCurrent = [];
            const PageTagTotal = Math.ceil(pageTotal / showPage);
            const currentPageTag = Math.ceil(page / showPage);
            let pageCurrentItem = [];

            const minPage = currentPageTag * showPage - showPage + 1;
            const maxPage = currentPageTag * showPage;

            for (let i = 1; i < pageTotal + 1; i++) {
                if (i >= minPage && i <= maxPage) {
                    pageCurrentItem.push(i);
                }
            }

            this.pageItem = {
                pageTotal,
                currentPage,
                hasPage: currentPage > 1,
                hasNext: currentPage < pageTotal,
                showPage: showPage,
                pageCurrent: Array.from(pageCurrentItem),
                currentPageTag,
                PageTagTotal,
                pageTagHasPre: currentPageTag > 1,
                pageTagHasNext: currentPageTag < PageTagTotal,
            };
        },
        toPage(page) {
            this.pagination(this.WeatherList, page);
        }
    }
};

const vm = Vue.createApp(app).mount('#app');