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



$(function () {
    $("#movieName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/api/GetOnMovieName?keyword='" + document.getElementById("movieName").value + "'",
                data:request,
                dataType: "Json",
                type: "GET",
                contentType: "application/json; charset-utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item.movieName + " : " + item.movieGenie + " : " + item.id;
                    }))
                },
            });
        },
        select: function (event, ui) {
            $("#movieName").val(ui.item.movieName);
            $("#Id").val(ui.item.id);
        },
       
    });
});

