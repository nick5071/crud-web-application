
var app = new Vue({
    el: '#cadastroPessoa',
    data: {
        pessoasVue: [],
        novaPessoa: {
            nome: '',
            sobrenome: '',
            idade: ''
        }
    },
    methods: {
        cadastrar: function () {
            this.pessoasVue.push(this.novaPessoa);
            this.novaPessoa = {
                nome: '',
                sobrenome: '',
                idade: ''
            }
        },
        excluir: function (index) {
            this.pessoasVue.splice(index, 1)
        }
    }
});

let DataAtual = new Date();
var dataFormatada = DataAtual.toLocaleDateString('pt-BR', { day: '2-digit', month: '2-digit', year: "numeric" }).replace(/\//g, '/');

Vue.use(VueTheMask);
Vue.use(VeeValidate, {
    classes: true,
    classNames: {
        valid: 'is-valid',
        invalid: 'is-invalid'
    },
    dictionary: {
        en: {
            messages: {
                required: 'Campo Obrigatório',
                date_format: 'Data inválida',
                date_between: "A data de nascimento precisa estar entre 01/01/1900 e " + dataFormatada
            }
        }
    }
});

function ValidarCPF(strCPF) {
    var Soma;
    strCPF = strCPF.replace(/[^0-9]/g, '');
    var Resto;
    Soma = 0;
    if (strCPF == "00000000000" || strCPF == "11111111111" || strCPF == "22222222222" || strCPF == "33333333333" || strCPF == "44444444444" || strCPF == "55555555555" || strCPF == "66666666666" || strCPF == "77777777777" || strCPF == "88888888888" || strCPF == "99999999999")
        return false;


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

function IsCEP(strCEP, blnVazio) {
    // Padrão para validar o CEP: 12345-678
    var objER = /^[0-9]{5}-[0-9]{3}$/;

    strCEP = Trim(strCEP);

    if (strCEP.length > 0) {
        if (objER.test(strCEP))
            return true;
        else
            return false;
    } else {
        return blnVazio;
    }
}

// Função auxiliar para remover espaços em branco no início e no fim de uma string
function Trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}
function verificarCpfJaCadastrado(cpfDigitado) {
    return new Promise(function (resolve) {
        axios.get('/api/pessoa/verificar-cpf-ja-cadastrado', { params: { cpf: cpfDigitado } }).then(function (resposta) {
            resolve({
                valid: !resposta.data.resultado,
                data: 'CPF já cadastrado'
            });
        }).catch(function () {
            resolve({
                valid: false,
                data: 'Erro ao validar CPF'
            });
        });
    });
}


new Vue({
    el: '#formCadastroPessoas',
    data: {
        pessoa: {}
    },
    created: function () {
        // Função para remover espaços em branco no início e no fim de uma string
        function Trim(str) {
            return str.replace(/^\s+|\s+$/g, "");
        }

        // Função para validar o CEP
        function IsCEP(strCEP, blnVazio) {
            // Padrão para validar o CEP: 12345-678
            var objER = /^[0-9]{5}-[0-9]{3}$/;

            strCEP = Trim(strCEP);

            if (strCEP.length > 0) {
                return objER.test(strCEP);
            } else {
                return blnVazio;
            }
        }

        // Estendendo o validador para incluir a validação do CPF
        this.$validator.extend('validar-cpf', {
            getMessage: 'CPF inválido',
            validate: function (value) {
                return ValidarCPF(value);
            }
        });

        // Estendendo o validador para incluir a validação do CEP
        this.$validator.extend('validar-cep', {
            getMessage: 'CEP inválido',
            validate: function (value) {
                return IsCEP(value);
            }
        });

        this.$validator.extend('verificar-cpf-ja-cadastrado', {
            getMessage: function (campo, params, data) {
                return data;
            },
            validate: verificarCpfJaCadastrado
        });
    },
    methods: {
        salvar: function (event) {
            this.$validator.validateAll().then(function (valido) {
                if (valido) {
                    event.target.submit();
                    return;
                }
            });
        }
    }
});


function myFunction() {
    var element = document.body;
    element.dataset.bsTheme =
        element.dataset.bsTheme == "light" ? "dark" : "light"
}

document.addEventListener('DOMContentLoaded', function () {
    var idExclusao = null;

    document.querySelectorAll('.btnExcluir').forEach(function (btn) {
        btn.addEventListener('click', function () {
            idExclusao = this.getAttribute('data-id');
        });
    });

    document.querySelectorAll('.btnExcluirConfirmar').forEach(function (btn) {
        btn.addEventListener('click', function () {
            var formId = 'formExcluir_' + idExclusao;
            document.getElementById('id_' + idExclusao).value = idExclusao;
            document.getElementById(formId).submit();
        });
    });
});