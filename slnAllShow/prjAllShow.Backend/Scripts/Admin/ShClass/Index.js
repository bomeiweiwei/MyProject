var apiUrl = '/api/ShClass';
var shopApiUrl = '/api/Shop';

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
            this.$emit("paged", item);
        },
    },
};

const modal_popup = {
    props: ["parentTitle"],
    methods: {
        hidemodelInner() {
            console.log("close");
            this.$emit("emit-hide");
        }
    },
    template: ` 
        <div class="modal fade" id="serviceModal" data-bs-backdrop="static" tabindex="-1" aria-labelledby="serviceModalLabel" aria-hidden="true">
          <div class="modal-dialog" id="divTargetModalDialog">
            <div class="modal-content">
              <div class="modal-header">
                <h3 class="modal-title" id="serviceModalLabel">{{ parentTitle }}</h3>
                <button type="button" class="btn-close" @click="hidemodelInner()" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" >Close</button>
              </div>
            </div>
          </div>
        </div>`
};

const app = {
    data() {
        return {
            List: null,
            ListData: [],
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
            popupTitle: "商店類別",
            popupModal: null,
            Id: 0,
            ShClassName: "",
            ShopList: [],
            ShopListData:[]
        }
    },
    mounted() {
        baseInstance.get(apiUrl +"/GetByPage/1")
            .then(response => {
                this.List = response.data.resultData;
                //console.log(this.List);
                this.pagination(this.List, 1, response.data.totalDataCount);
            });
        this.popupModal = new bootstrap.Modal(this.$refs.serviceModal.$el);
    },
    components: {
        paginationBtn,
        modal_popup
    },
    methods: {
        pagination(Data, page, totalCount) {
            this.ListData = [];
            const dataTotal = totalCount;//Data.length;
            const perPage = 10;//15;
            const showPage = 10;
            const pageTotal = Math.ceil(dataTotal / perPage);
            let currentPage = page;
            if (currentPage > pageTotal) {
                currentPage = pageTotal;
            }
            
            Data.forEach((item, index) => {
                this.ListData.push(item);
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
            baseInstance.get(apiUrl + "/GetByPage/" + page)
                .then(response => {
                    this.List = response.data.resultData;
                    this.pagination(this.List, page, response.data.totalDataCount);
                });
        },
        AddShClass() {
            if ($('#divTargetModalDialog').hasClass('modal-lg')) {
                $('#divTargetModalDialog').removeClass("modal-lg");
            }

            this.popupTitle = "新增商店類別";
            var url = "/Admin/ShClass/Create"; //or anyother html page
            var pageContent = String.format("<iframe width=\"100%\" frameborder=\"0\"  allowtransparency=\"true\" src=\"{0}\"></iframe>", url);
            //var pageContent = String.format("<iframe width=\"100%\" height=\"{1}\" frameborder=\"0\"  allowtransparency=\"true\" src=\"{0}\"></iframe>", url, $(window).innerHeight());
            $(".modal-body").html(pageContent);
            this.popupModal.show();
        },
        EditShClass(id) {
            if ($('#divTargetModalDialog').hasClass('modal-lg')) {
                $('#divTargetModalDialog').removeClass("modal-lg");
            }

            this.popupTitle = "修改商店類別";
            var url = "/Admin/ShClass/Edit/"+id;
            var pageContent = String.format("<iframe width=\"100%\" frameborder=\"0\"  allowtransparency=\"true\" src=\"{0}\"></iframe>", url);
            $(".modal-body").html(pageContent);
            this.popupModal.show();
        },
        DeleteShClass(id) {
            var yes = confirm('你確定要刪除嗎？');
            if (yes) {
                var _url = apiUrl + "/" + id;
                baseInstance.delete(_url)
                    .then(function (response) {
                        alert('刪除完成');
                        //this.refreshVuePage();
                        refreshPage();
                    })
                    .catch(function (error) {
                        //console.log(error);
                        if (error.response) {
                            console.log(error.response.status);
                        }
                    });
                //$.ajax({
                //    url: _url,
                //    type: 'DELETE',
                //    async: false,
                //    headers: {
                //        'RequestVerificationToken': $('input:hidden[name="__RequestVerificationToken"]').val()
                //    },
                //    }).done(function (obj) {
                //        alert("刪除完成！！！");
                //        refreshPage();
                //    }).fail(function (xhr, status, error) {

                //    });
            }                    
        },
        hidePopupModal() {
            this.popupModal.hide();
        },
        refreshVuePage() {
            this.toPage(this.pageItem.currentPage);
        },
        SelectShClass(id) {           
            this.Id = id;
            baseInstance.get(shopApiUrl + "/GetByShclass/" + this.Id)
                .then(response => {
                    this.ShopListData = response.data.resultData;
                });
        },
        SelectShop(id) {
            if (!$('#divTargetModalDialog').hasClass('modal-lg')) {
                $('#divTargetModalDialog').addClass("modal-lg");
            }

            this.popupTitle = "商店內容";
            var url = "/Admin/Shop/SimpleDetails/" + id;
            var pageContent = String.format("<iframe width=\"100%\" height=\"{1}\" frameborder=\"0\"  allowtransparency=\"true\" src=\"{0}\"></iframe>", url, $(window).innerHeight()*0.5);
            $(".modal-body").html(pageContent);
            this.popupModal.show();
        }
    }
};

const vm = Vue.createApp(app).mount('#app');

function closePopupModal() {
    vm.hidePopupModal();
}

function refreshPage() {
    vm.refreshVuePage();
}