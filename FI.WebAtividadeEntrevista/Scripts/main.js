$(function () {
    $('.js-cpf').mask('000.000.000-00');

    $('.js-cep').on('blur', function () {
        var cep = $('.js-cep').val().replace(/[^A-Z\d\s]/gi, '');
        $.get('https://viacep.com.br/ws/' + cep + '/json', function (data) {
            if (!("erro" in data)) {
                $('.js-estado').val(data.uf);
                $('.js-cidade').val(data.localidade);
                $('.js-logradouro').val(data.logradouro);
            }
        });
    });
});