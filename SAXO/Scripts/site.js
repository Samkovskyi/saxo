$(document).ready(function () {

    SAXO = document.SAXO || {};

    SAXO.OnBegin = function() {
        $("#prealoader").show();
        $("#submitBtn").hide();
    };

    SAXO.OnCmplete = function() {
        $("#prealoader").hide();
        $("#submitBtn").show();
    };

    SAXO.ClearInput = function(data) {
        $("#isbnForm").find("textarea").val("");
        var newBooksAmount = $(data).children().length;
        showModal(newBooksAmount);
    };
    
    function showModal(booksAmount) {
        var msg;
        
        switch (booksAmount) {
            case 0:
                msg = "No new books";
                break;
            case 1:
                msg = "Only one new book was added";
                break;
            default:
                msg = "There are " + booksAmount + " new books";
        }
        $("#modalMsg").text(msg);
        $('.modal').modal('show');
    }
})