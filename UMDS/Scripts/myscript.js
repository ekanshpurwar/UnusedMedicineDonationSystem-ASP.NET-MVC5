function getAllNGOes() {
    $.ajax({
        url: "http://localhost:65533/Home/GetAllNGOes",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        //data: "{ 'ecode':" + ec + "}",
        //dataType: "json",
        success: function (res) {
            console.log("success:", res);

            var htmlStr = "<table class='table'><tr><th>Name</th><th>Mobile</th><th>City</th><th>State</th><th>Pin</th><th>Email</th></tr>";
            $.each(res, function (index, ngo) {
                htmlStr += "<tr>";
                /*htmlStr += "<td>" + ngo.Id + "</td>";*/
                htmlStr += "<td>" + ngo.Name + "</td>";
                htmlStr += "<td>" + ngo.Mobile + "</td>";
                htmlStr += "<td>" + ngo.City + "</td>";
                htmlStr += "<td>" + ngo.State + "</td>";
                htmlStr += "<td>" + ngo.Pin + "</td>";
                htmlStr += "<td>" + ngo.Email + "</td>";
                //htmlStr += "<td>" + ngo.Password + "</td>";
                htmlStr += "</tr>";
            });
            htmlStr += "</table>";
            $("#h3").text("Registered NGOes")
            $("#d1").html(htmlStr);
        },
        error: function (err) {
            console.log("Error:", err);
        }
    });//ajax request ending
}