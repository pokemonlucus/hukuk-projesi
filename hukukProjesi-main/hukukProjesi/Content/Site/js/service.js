$(document).ready(function () {

    $('#ceza').click(function () {
        $('#cezaDiv').show();
        $('#aileDiv').hide();
        $('#ticaretDiv').hide();
        $('#gayrimenkulDiv').hide();
        $('#isDiv').hide();
    });

    $('#aile').click(function () {
        $('#aileDiv').show();
        $('#cezaDiv').hide();
        $('#ticaretDiv').hide();
        $('#gayrimenkulDiv').hide();
        $('#isDiv').hide();
    });
    $('#ticaret').click(function () {
        $('#ticaretDiv').show();
        $('#aileDiv').hide();
        $('#cezaDiv').hide();
        $('#gayrimenkulDiv').hide();
        $('#isDiv').hide();
    });
    $('#gayrimenkul').click(function () {
        $('#gayrimenkulDiv').show();
        $('#aileDiv').hide();
        $('#cezaDiv').hide();
        $('#ticaretDiv').hide();
        $('#isDiv').hide();
        
    });
    $('#is').click(function () {
        $('#isDiv').show();
        $('#aileDiv').hide();
        $('#cezaDiv').hide();
        $('#ticaretDiv').hide();
        $('#gayrimenkulDiv').hide();
    });

})

$('.service-catergory li').on('click', function () {

    $('li').removeClass('active');
    $(this).addClass('active');


});