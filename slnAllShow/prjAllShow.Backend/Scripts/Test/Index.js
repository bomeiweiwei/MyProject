var authUrl = 'api/GetAuth/getauthtoken';
var apiUrl = '/api/Weather';
var apiCheck = '/api/GetAuth/checktokenvalid';

//var dialog = new Object;

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

const modal = {
    props: ["parentTitle", "parentTxt", "parentVersion"],
    methods: {
        hidemodelInner() {
            console.log("close1");
            this.$emit("emit-hide");
        }
    },
    template: ` 
              <div class="modal fade" id="exampleModal" ref="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
              <div class="modal-dialog">
                <div class="modal-content">
                  <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel"> {{ parentTitle }}</h5>
                    <button type="button" class="btn-close" @click="hidemodelInner()" aria-label="Close"></button>
                  </div>
                  <div class="modal-body">
                    {{ parentTxt }}<br/>{{ parentVersion }}
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" @click="hidemodelInner()">關閉</button>
                  </div>
                </div>
              </div>
            </div>`           
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
            h1Title: "這是新視窗",
            isActive: true,
            modal: null,
            //tModal: null
        }
    },      
    mounted() {
        baseInstance.get(apiUrl)
            .then(response => {
                //console.log(response.data.resultData);
                this.WeatherList = response.data.resultData;
                this.pagination(this.WeatherList, 1, response.data.totalDataCount);
            });
        this.modal = new bootstrap.Modal(this.$refs.exampleModal.$el);
        //this.tModal = new bootstrap.Modal(this.$refs.serviceModal.$el);
    },
    computed: {
        dateFormat() {
            return moment(this.date).format('YYYY-MM-DD');
        }
    },
    components: {
        paginationBtn,
        modal,
        //modal_popup
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
        SetDateFormat(date, format) {
            return moment(date).format(format);
            //return `${salut} ${this.firstName} ${this.lastName}`
        },
        pagination(Data, page, totalCount) {
            this.WeatherListData = [];
            const dataTotal = totalCount;//Data.length;
            const perPage = 10;//15;
            const showPage = 10;
            const pageTotal = Math.ceil(dataTotal / perPage);
            let currentPage = page;
            if (currentPage > pageTotal) {
                currentPage = pageTotal;
            }
            //const minData = currentPage * perPage - perPage + 1;
            //const maxData = currentPage * perPage;

            //Data.forEach((item, index) => {
            //    const num = index + 1;
            //    if (num >= minData && num <= maxData) {
            //        this.WeatherListData.push(item);
            //    }
            //});
            Data.forEach((item, index) => {
                this.WeatherListData.push(item);
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
            baseInstance.get(apiUrl +"?page="+page)
                .then(response => {
                    this.WeatherList = response.data.resultData;
                    this.pagination(this.WeatherList, page, response.data.totalDataCount);
                });
            //this.pagination(this.WeatherList, page);
        },
        showModal() {
            console.log('1');
            this.modal.show();
        },
        //showModal2(id) {
        //    console.log('2');
        //    var url = "/Test/Index_Popup/"+id; //or anyother html page
        //    $(".modal-body").html('<iframe width="100%" height="100%" frameborder="0" scrolling="no" allowtransparency="true" src="' + url + '"></iframe>');
        //    this.modal2.show();
        //},
        hideModal() {
            this.modal.hide();
        },
        //showPopupModal() {
        //    this.tModal.show();
        //},
        //hidePopupModal() {
        //    this.tModal.hide();
        //}
    }
};

const vm = Vue.createApp(app).mount('#app');

const modal_popup = {
    methods: {
        hidemodelInner() {
            console.log("close");
            this.$emit("emit-hide");
        }
    },
    template: ` 
        <div class="modal fade" id="serviceModal" tabindex="-1" aria-labelledby="serviceModalLabel" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h3 class="modal-title" id="serviceModalLabel">古人云</h3>
                <button type="button" class="btn-close" @click="hidemodelInner()" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                <p class="text-center">
                    <img src="../Images/dtc-logo.png" class="w-50" />
                </p>
                <div>
                    臣亮言：先帝創業未半，而中道崩殂。今天下三分，益州
                    疲弊，此誠危急存亡之秋也。然侍衛之臣，不懈於內；忠志之
                    士，忘身於外者，蓋追先帝之殊遇，欲報之於陛下也。誠宜開
                    張聖聽，以光先帝遺德，恢弘志士之氣；不宜妄自菲薄，引喻
                    失義，以塞忠諫之路也。宮中府中，俱為一體，陟罰臧否，不
                    宜異同。若有作姦犯科，及為忠善者，宜付有司，論其刑賞，
                    以昭陛下平明之治，不宜篇私，使內外異法也。
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary">Save changes</button>
              </div>
            </div>
          </div>
        </div>`
};

const app2 = {
    //el: '#app',
    data() {
        return {
            tModal: null
        }
    },
    mounted() {
        this.tModal = new bootstrap.Modal(this.$refs.serviceModal.$el);
    },
    components: {
        modal_popup
    },
    methods: {
        showPopupModal() {
            this.tModal.show();
        },
        hidePopupModal() {
            this.tModal.hide();
        }
    }
};

const vm2 = Vue.createApp(app2).mount('#app2');

