function GetMessages() {
    var options = document.getElementsByName('options');
    var option;
    for (i = 0; i < options.length; i++) {
        if (options[i].checked) {
            option = options[i].id;
            break;
        }
    }
    $.ajax({
        type: 'GET',
        url: '/Home/' + option + '/',
        success: function (data) {
            $("#messages-table").html(data);
        }
    });
}