
$(function () {
   

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

});

function cadastrarUsuario() {
    if ($('#txtSenhaCadastro').val() != $('#txtSenhaConfirmacaoCadastro').val()) {
     
        toastr["error"]("Senhas diferentes!");
        return;
    }
    if ($('#txtSenhaCadastro').val() === "") {
    
        toastr["error"]("Por favor, digite a sua senha!");
        return;
    }
    if ($('#txtSenhaConfirmacaoCadastro').val() == "") {
     
        toastr["error"]("Por favor, digite a sua senha de confirmação!");
        return;
    }
    if ($('#txtLoginCadastro').val() == "") {
       
        toastr["error"]("Por favor, digite o seu e-mail!");
        return;
    }
    if ($('#txtNomeCadastro').val() == "") {
      
        toastr["error"]("Por favor, digite o seu nome!");
        return;
    }
    if ($('#txtCpf').val() == "") {
       
        toastr["error"]("Por favor, digite o seu cpf!");
        return;
    }
    if (!TestaCPF($('#txtCpfCadastro').val())) {
     
        toastr["error"]("Cpf inválido!");
        return;
    }
    $("#btnCadastroUsuario").prop("disabled", true);
    $("#btnCadastroUsuario").html("Aguarde...");
    var model = {
        Nome: $("#txtNomeCadastro").val(),
        Login: $('#txtLoginCadastro').val(),
        Senha: $('#txtSenhaCadastro').val(),
        SenhaConfirmacao: $('#txtSenhaConfirmacaoCadastro').val(),
        Cpf: $('#txtCpfCadastro').val(),

    };
    $.ajax({
        url: "../../../../Account/CadastrarUsuario",
        type: "POST",
        async: true,
        data: model,
        success: function (d) {
           
          
            if (d.success) {
                toastr["success"](d.message);
                limpaFormulario();
            }
            else {
                toastr["error"](d.message);
            }
            $("#btnCadastroUsuario").html("Cadastrar Usuário");
            $("#btnCadastroUsuario").prop("disabled", false);
        },
        error: function (e) {
          
            toastr["error"](e.message);
            $("#btnCadastroUsuario").html("Cadastrar Usuário");
            $("#btnCadastroUsuario").prop("disabled", true);
        }

    });
}


function limpaFormulario() {
    $("#txtNomeCadastro").val("")
    $('#txtLoginCadastro').val("")
    $('#txtSenhaCadastro').val("")
    $('#txtSenhaConfirmacaoCadastro').val("")
    $('#txtCpfCadastro').val("")
}
function TestaCPF(strCPF) {
    var Soma;
    var Resto;
    Soma = 0;
    if (strCPF == "00000000000") return false;

    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10))) return false;

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11))) return false;
    return true;
}