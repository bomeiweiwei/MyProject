function checktoken() {
    $.ajax({
        type: "GET",
        url: "/api/GetAuth/checktokenexpires",
        async: false,
        headers: {
            'RequestVerificationToken': $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        statusCode: {
            400: function (xhr, status, error) {
                console.log('check token expires Error happened');
            },
            401: function (xhr, status, error) {
                var jsonResponse = JSON.parse(xhr.responseText);
                var errors = jsonResponse.errors;
                console.log('Error happened: ' + errors[0]);
            }
        }
    }).done(function (obj) {
        console.log('check token expires finish');
    }).fail(function (xhr, status, error) {

    });
}