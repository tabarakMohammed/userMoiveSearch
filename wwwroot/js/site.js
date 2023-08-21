// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.



/*Index section*/

var movieName;
var id;
$(function () {
    $("#movieName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/api/GetOnMovieName?keyword='" + document.getElementById("movieName").value + "'",
                data:request.trim,
                dataType: "Json",
                type: "GET",
                contentType: "application/json; charset-utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        var movieObject = new Object();
                        movieObject.label = item.movieName + " ( " + item.movieGenie + " ) ";
                        movieObject.value = item.movieName;
                        id = item.id;         
                        return movieObject;
                    }))
                },
            });
        },
        select: function (event, ui) {
            $("#id").val(id); 
        },
       
    });
    $.ui.autocomplete.prototype._renderItem = function (ul, item) {
        return $("<li>")
            .append("<div>" + item.label + "</div>")
            .append('<div hidden="hidden">' + id + '</div>')
            .appendTo(ul)
            .on("click", function () {
                sendDataToRazor(id)
            });
    };
  
});


function sendDataToRazor(data) {
    $.ajax({
        type: 'POST',
        url: '/Index?Handler=MouseClicked',
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: JSON.stringify(data) ,
        contentType: 'application/json',
        success: function (response) {  
        },
        error: function (error) {  
        }
    });
}