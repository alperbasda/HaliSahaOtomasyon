var date = new Date();
date.setDate(date.getDate() - 1);
$("#Mypicker").datetimepicker({

    format: 'DD/MM/YYYY HH:mm',
    stepping: 30,
    sideBySide: true,
    minDate: new Date(),
    locale: 'tr'
});


$('#Sorgula').click(function () {
    $.ajax({
        url: '../Home/Sorgula',
        type: 'Get',
        dataType: 'Json',
        contentType: 'application/Json',
        data: { zaman: $('#Mypicker').children('input').val() },
        success: function(yanit) {
            alert(yanit);
        }

});

});