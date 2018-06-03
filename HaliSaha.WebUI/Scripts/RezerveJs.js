
$("#Mypicker").datetimepicker({
    widgetPositioning: {
        vertical: 'top'
    },
    format: 'DD/MM/YYYY',
    stepping: 30,
    sideBySide: true,
    minDate: new Date(),
    locale: 'tr'

});

$(document).ready(function () {
    $('#sahaId').val("1");
    var a = getUrlVars()['message'];
    switch (a) {
        case '1':
            alert("Lütfen tüm alanları eksiksiz doldurunuz");
            window.location.href = window.location.href.split('?')[0];
        break;
        case '2':
            alert("İstenilen saat dolu lütfen değiştiriniz");
            window.location.href = window.location.href.split('?')[0];
        break;
        case '3':
            alert("Rezervasyonunuz alınmıştır");
            window.location.href = window.location.href.split('?')[0];
        break;
        case '4':
            alert("Geçirli tarih veya saat giriniz");
            window.location.href = window.location.href.split('?')[0];
        break;
    default:
    }
});

$('#Sorgula').click(function () {
    $.ajax({
        url: '../Home/Sorgula',
        type: 'Get',
        dataType: 'Json',
        contentType: 'application/Json',
        data: { zaman: $('#Mypicker').children('input').val() },
        beforeSend: function () {
            $('#loading').show();
        },
        success: function (yanit) {

            $('#Section1').children('div').remove();
            $('#Section2').children('div').remove();
            $('#Section3').children('div').remove();
            $('#Section1').append(yanit[0]);
            $('#Section2').append(yanit[1]);
            $('#Section3').append(yanit[2]);
            $('#loading').hide();
            $('#first').click();
            degistir(1);
        }

    });

});

function reserve(item) {
    $('#tarih').val($("#Mypicker").find("input").val());
    $('#RezerveModal').modal('show');
    $('#saat').val($(item).data('saat'));
};


function degistir(item) {
    $('#sahaId').val(item);
};

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}


