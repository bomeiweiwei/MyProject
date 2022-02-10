$("[name='sapnCircle']").on('click', function () {
    var num = $(this).find("span").text();
    var searchStr = $("#txtSearchString").val();
    location.href = encodeURI('/admin/Shop/Index?currentFilter=' + searchStr + '&pageNumber=' + num);
});

$("[name='sapnCircle'] span").each(function (index) {
    //console.log( index + ": " + $( this ).text() );
    //var num = $(this).find("span").text();
    var num = $(this).text();
    if (num == "@Model.PageIndex") {
        $(this).css('color', '#2263B7');
    }
});