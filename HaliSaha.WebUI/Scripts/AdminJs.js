function myFunction() {
    // Declare variables 
    var input, filter, table, tr, td, i;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function remove(item) {
    $.ajax({
        url: '../Admin/Remove',
        type: 'Get',
        dataType: 'Json',
        data: { id: $(item).data('id') },
        contentType: 'application/json',
        success: function (message) {
            alert(message);
            $(item).parents('td').parents('tr').remove();
        },
        error: function () {
            alert("İşlem başarısız hata...");
        }
    });
}

function rezervasyon() {
    $('#RezerveModal').modal('show');
};

$('#BtnRezerve').click(function() {
    $.ajax({
        url: '../Admin/Add',
        type: 'Get',
        dataType: 'Json',
        data: {
            musteriAd: $('#musteriAd').val(),
            musteriSoyad: $('#musteriSoyad').val(),
            telefon: $('#telefon').val(),
            mail: $('#mail').val(),
            sahaId: $('#sahaId').val(),
            tarih: $('#tarih').val(),
            saat: $('#saat').val()
        },
        contentType: 'application/json',
        success: function(message) {
            alert(message);
        },
        error: function() {
            alert("İşlem başarısız hata...");
        }
    });
});

var changeImage;
function openModal(item) {
    changeImage = item;
    $('#resimModal').modal('show');
    $("#BtnYukle").html('Yukle');
    $('#myprogress').attr('aria-valuenow', 0).css('width', 0 + '%').text(0 + '%');

}


$('#BtnYukle').click(function () {
    var data = new FormData();
    var files = $('#fileToUpload').get(0).files;
    data.append('file', files[0]);
    data.append('path',changeImage);
    $("#BtnYukle").attr('value', 'Bekleyiniz...');
    $.ajax({
        xhr: function() {
            var xhr = new window.XMLHttpRequest();
            xhr.upload.addEventListener('progress',
                function(e) {

                    if (e.lengthComputable) {
                        var percent = Math.round((e.loaded / e.total) * 100);
                        $('#myprogress').attr('aria-valuenow', percent).css('width', percent + '%').text(percent + '%');

                    }
                });
            return xhr;


        },
        url: '../Admin/Yukle',
        type: 'POST',
        processData: false,
        data: data,
        dataType: 'json',
        contentType: false,
        success: function (turn) {
            location.reload();
        },
        complete: function() {
            location.reload();
        }


    });


});

