using CadastroCliente.Application.Commands.Requests;
using CadastroCliente.Domain.Entities;

namespace CadastroClienteTests;

public abstract class BaseTests
{
    protected static readonly DateTime _dataNascimentoValida = new(1990, 1, 1);
    protected static readonly string _nomeValido = "Nome do Cliente Teste";
    protected static readonly string _telefoneValido = "(11)99999-8888";
    protected static readonly string _emailValido = "nometeste@email.com";

    protected static readonly string _cpfValido = "123.456.789-09";
    protected static readonly string _cnpjValido = "91.169.766/0001-50";
    protected static readonly string _razaoSocialValida = "Empresa YYZ";
    protected static readonly string _inscricaoEstadualValida = "123456789";

    protected static readonly PessoaFisica _pessoaFisicaValida = new(
            _cpfValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _enderecoValido!
        );

    protected static readonly PessoaJuridica _pessoaJuridicaValida = new(
        _cnpjValido,
        _razaoSocialValida,
        _telefoneValido,
        _emailValido,
        _dataNascimentoValida,
        _inscricaoEstadualValida,
        false,
        _enderecoValido!
    );

    protected static readonly Endereco _enderecoValido = new(
        "12345-678",
        "Rua Teste",
        "123",
        "Bairro Teste",
        "Cidade Teste",
        "SP"
    );

    protected static readonly EnderecoCommandRequest _enderecoCommandRequestValido = new()
    {
        Cep = "12345-678",
        Logradouro = "Rua Teste",
        Numero = "123",
        Bairro = "Bairro Teste",
        Cidade = "Cidade Teste",
        Estado = "SP"
    };
}
