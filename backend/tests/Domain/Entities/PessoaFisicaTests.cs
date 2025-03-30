using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Enums;
using CadastroCliente.Extensions;

namespace CadastroClienteTests.Domain.Entities;

public class PessoaFisicaTests : BaseTests
{
    public PessoaFisicaTests()
    {
    }

    [Theory]
    [InlineData("12345678909", "João da Silva", "11999998888", "joao@email.com")]
    [InlineData("521.373.927-04", "Severino da Silva", "(31)77777-6666", "severino@email.com")]
    public void DeveCriarPessoaFisicaComSucesso(string cpf, string nome, string telefone, string email)
    {
        // Arrange & Act
        var pessoaFisica = new PessoaFisica(
            cpf,
            nome,
            telefone,
            email,
            _dataNascimentoValida,
            _enderecoValido
        );

        var result = pessoaFisica.Validate();

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("123456789")] // CPF com menos dígitos
    [InlineData("123456789012")] // CPF com mais dígitos
    [InlineData("12345678901")] // CPF inválido
    public void NaoDeveCriarPessoaFisicaComCpfInvalido(string cpfInvalido)
    {
        // Act
        var pessoaFisica = new PessoaFisica(
            cpfInvalido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _enderecoValido
        );

        var result = pessoaFisica.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "CPF inválido");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")] // Nome muito curto
    public void NaoDeveCriarPessoaFisicaComNomeInvalido(string nomeInvalido)
    {
        // Act
        var pessoaFisica = new PessoaFisica(
            _cpfValido,
            nomeInvalido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _enderecoValido
        );

        var result = pessoaFisica.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Nome é obrigatório");
    }

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    public void NaoDeveCriarPessoaFisicaComEmailInvalido(string emailInvalido)
    {
        // Act
        var pessoaFisica = new PessoaFisica(
            _cpfValido,
            _nomeValido,
            _telefoneValido,
            emailInvalido,
            _dataNascimentoValida,
            _enderecoValido
        );

        var result = pessoaFisica.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Email inválido");
    }

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("123456789")]
    [InlineData("(61)12345678901")]
    public void NaoDeveCriarPessoaFisicaComTelefoneInvalido(string telefoneInvalido)
    {
        // Act
        var pessoaFisica = new PessoaFisica(
            _cpfValido,
            _nomeValido,
            telefoneInvalido,
            _emailValido,
            _dataNascimentoValida,
            _enderecoValido
        );

        var result = pessoaFisica.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Telefone deve conter 10 ou 11 dígitos");
    }

    [Fact]
    public void NaoDeveCriarPessoaFisicaComIdadeMenorQue18Anos()
    {
        // Arrange
        var dataNascimentoMenor = DateTime.Now.AddYears(-17);

        // Act
        var pessoaFisica = new PessoaFisica(
            _cpfValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            dataNascimentoMenor,
            _enderecoValido
        );

        var result = pessoaFisica.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "O cliente deve ter pelo menos 18 anos");
    }

    [Fact]
    public void DeveAtualizarCpfComSucesso()
    {
        // Arrange
        var pessoaFisica = new PessoaFisica(
            _cpfValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _enderecoValido
        );

        var novoCpf = "317.283.949-45";

        // Act
        pessoaFisica.AtualizarCpf(novoCpf);

        // Assert
        Assert.Equal(novoCpf.RemoverCaracteresNaoNumericos(), pessoaFisica.Cpf);
        Assert.True(pessoaFisica.Validate().IsValid);
    }

    [Fact]
    public void DeveAtualizarNomeComSucesso()
    {
        // Arrange
        var pessoaFisica = new PessoaFisica(
            _cpfValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _enderecoValido
        );

        var novoNome = "João Silva Santos";

        // Act
        pessoaFisica.AtualizarNome(novoNome);

        // Assert
        Assert.Equal(novoNome, pessoaFisica.Nome);
        Assert.True(pessoaFisica.Validate().IsValid);
    }

    [Fact]
    public void DeveHerdarPropriedadesDeCliente()
    {
        // Arrange & Act
        var pessoaFisica = new PessoaFisica(
            _cpfValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _enderecoValido
        );

        // Assert
        Assert.IsAssignableFrom<Cliente>(pessoaFisica);
        Assert.Equal(nameof(TipoCliente.PessoaFisica), pessoaFisica.Tipo);
        Assert.True(pessoaFisica.Validate().IsValid);
    }
}