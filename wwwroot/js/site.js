// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

/*
$('#movieName').keyup(function () {
    suggestions.innerHTML = ''
    keyword = document.getElementById("movieName").value;
    fetchDataAsync("/api/GetOnMovieName?keyword='" + keyword + "'")

        .then(data => moves.push(...data));


     html = moves.map(moviez => {
        return `
      <li>
        <span class="name">${moviez.movieName}, ${moviez.movieGenie}</span>
      </li>
    `;
    }).join('');

    suggestions.innerHTML = html;
   
    moves = [];
   
});


 async function fetchDataAsync(url) {
    var response = await fetch(url);
     return await response.json();
}  
*/



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
           // This function is triggered when an item is selected from the dropdown
            $("#id").val(id);
            id = "";
        },
       
    });
    $.ui.autocomplete.prototype._renderItem = function (ul, item) {
        return $("<li>")
            .append("<div>" + item.label + "</div>")
            .appendTo(ul)
            .on("click", function () {
                // This function is triggered when an item is clicked in the dropdown
                var clickedItem = item.value;     
            });
    };
  
});

