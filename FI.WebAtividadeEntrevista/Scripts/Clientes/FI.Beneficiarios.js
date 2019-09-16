

$(function () {
    AtualizarListagem();
});

var Beneficiarios = obj ? obj.Beneficiarios : [];


function Adicionar() {
    var json = {
        Nome: $('#bnfNome').val(),
        CPF: $('#bnfCPF').val()
    };

    if (!json.CPF) {
        alert('Informe o CPF do beneficiário');
    }
    else if (!json.Nome) {
        alert('Informe o nome do beneficiário');
    }
    else {
        Beneficiarios.push(json);
        $('#bnfNome').val("");
        $('#bnfCPF').val("");
        AtualizarListagem();
    }
};

function Remover(cpf) {
    Beneficiarios = Beneficiarios.filter(function (item) { return item.CPF != cpf });
    AtualizarListagem();
}

function Alterar(cpf, nome) {
    Beneficiarios = Beneficiarios.filter(function (item) { return item.CPF != cpf });
    $('#bnfNome').val(nome);
    $('#bnfCPF').val(cpf);
    AtualizarListagem();
}

function AtualizarListagem() {
    var items = "";
    $('#hdnBeneficiarios').val(JSON.stringify(Beneficiarios));
    for (item of Beneficiarios) {
        items +=
            '<tr>' +
            '<td>' + item.CPF + '</td>' +
            '<td>' + item.Nome + '</td>' +
            '<td>' +
            '<button type="button" class="btn btn-primary" style="margin-right:10px"  onclick="Alterar(\'' + item.CPF.trim() + '\',\'' + item.Nome + '\');">Alterar</button>' +            
            '<button type="button" class="btn btn-primary" onclick="Remover(\'' + item.CPF + '\');" >Excluir</button>' +
            '</td>' +
            '</tr>';
    }
    $('#tbBeneficiarios').html(items);
};