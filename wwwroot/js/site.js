// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$(function () {
    $("#movieName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/api/GetOnMovieName?keyword='"+$("#movieName").val()+"'",
                data:request,
                dataType: "Json",
                type: "GET",
                contentType: "application/json; charset-utf-8",
                success: function (data) {
                    response($.map(data, function (item) {       
                        return item.movieName + " : " + item.movieGenie;
                    }))
                },
            });
        },
        select: function (event, ui) {
            $("#movieName").val(ui.item.movieName);
            $("#Id").val(ui.item.id);
        },
        minLength: 3,
    });
});
