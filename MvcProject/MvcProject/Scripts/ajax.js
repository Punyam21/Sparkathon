$(document).ready(function () {
    var html = '';
    $.ajax({
        url: '/Customer/GetProductAjax',
        type: 'GET',
        success: function (data) {
            $.each(data, function (key, item) {
                html += "<div class='card'><form method='post' id='myForm' action='/Customer/AddCart' enctype='multipart/form-data'>@{Html.AntiForgeryToken()}<input type='hidden' name='Id' id='Id' value='" + item.Pid + "' /><img src='@Url.Content(" + item.ImageUrl + ")' class='card-img-top' height='200px' alt='Product'><div class='card-body'><h3>" + item.Category + "</h3><p>Discount-<del>" + item.Discount + "</del> <p class='price'>$" + item.Price + "</p></p><p>Brand-" + item.Description + "</p><p><button>Add To Card</button></p></div></form></div>";

            });
            $('#viewcard').append(html);
        }
    });
});
