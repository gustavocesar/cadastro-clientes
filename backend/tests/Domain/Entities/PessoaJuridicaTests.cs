using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Enums;
using CadastroCliente.Extensions;

namespace CadastroClienteTests.Domain.Entities;

public class PessoaJuridicaTests : BaseTests
{
    public PessoaJuridicaTests()
    {
    }

    [Theory]
    [InlineData("36837562000131", "Transportadora Teste", "11999998888", "transportadora@email.com")]
    [InlineData("91.169.766/0001-50", "Pizzaria Teste", "(31)77777-6666", "pizzaria@email.com")]
    public void DeveCriarPessoaJuridicaComSucesso(string cnpj, string razoSocial, string telefone, string email)
    {
        // Arrange & Act
        var pessoaJuridica = new PessoaJuridica(
            cnpj,
            razoSocial,
            telefone,
            email,
            _dataNascimentoValida,
            _inscricaoEstadualValida,
            false,
            _enderecoValido
        );

        var result = pessoaJuridica.Validate();

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("1234567890123")] // CNPJ com menos dígitos
    [InlineData("123456789012345")] // CNPJ com mais dígitos
    [InlineData("12345678901234")] // CNPJ inválido
    public void NaoDeveCriarPessoaJuridicaComCnpjInvalido(string cnpjInvalido)
    {
        // Act
        var pessoaJuridica = new PessoaJuridica(
            cnpjInvalido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _inscricaoEstadualValida,
            false,
            _enderecoValido
        );

        var result = pessoaJuridica.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "CNPJ inválido");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("A")] // Nome muito curto
    public void NaoDeveCriarPessoaJuridicaComNomeInvalido(string nomeInvalido)
    {
        // Act
        var pessoaJuridica = new PessoaJuridica(
            _cnpjValido,
            nomeInvalido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _inscricaoEstadualValida,
            false,
            _enderecoValido
        );

        var result = pessoaJuridica.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Razão social é obrigatória");
    }

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("123456789")]
    [InlineData("(61)12345678901")]
    public void NaoDeveCriarPessoaJuridicaComTelefoneInvalido(string telefoneInvalido)
    {
        // Act
        var pessoaJuridica = new PessoaJuridica(
             _cnpjValido,
             _nomeValido,
             telefoneInvalido,
             _emailValido,
             _dataNascimentoValida,
             _inscricaoEstadualValida,
             false,
             _enderecoValido
         );

        var result = pessoaJuridica.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Telefone deve conter 10 ou 11 dígitos");
    }

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    public void NaoDeveCriarPessoaJuridicaComEmailInvalido(string emailInvalido)
    {
        // Act
        var pessoaJuridica = new PessoaJuridica(
             _cnpjValido,
             _nomeValido,
             _telefoneValido,
             emailInvalido,
             _dataNascimentoValida,
             _inscricaoEstadualValida,
             false,
             _enderecoValido
         );

        var result = pessoaJuridica.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Email inválido");
    }

    [Fact]
    public void NaoDeveCriarPessoaJuridicaSemIsentoSemInscricaoMunicipal()
    {
        // Act
        var pessoaJuridica = new PessoaJuridica(
            _cnpjValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            "",
            false,
            _enderecoValido
        );

        var result = pessoaJuridica.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Inscrição estadual é obrigatória");
    }

    [Fact]
    public void DeveAtualizarCnpjComSucesso()
    {
        // Arrange
        var pessoaJuridica = new PessoaJuridica(
            _cnpjValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _inscricaoEstadualValida,
            false,
            _enderecoValido
        );

        var novoCnpj = "53.369.681/0001-26";

        // Act
        pessoaJuridica.AtualizarCnpj(novoCnpj);

        // Assert
        Assert.Equal(novoCnpj.RemoverCaracteresNaoNumericos(), pessoaJuridica.Cnpj);
        Assert.True(pessoaJuridica.Validate().IsValid);
    }

    [Fact]
    public void DeveAtualizarRazaoSocialComSucesso()
    {
        // Arrange
        var pessoaJuridica = new PessoaJuridica(
            _cnpjValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _inscricaoEstadualValida,
            false,
            _enderecoValido
        );

        var novoNome = "Novo nome da empresa";

        // Act
        pessoaJuridica.AtualizarRazaoSocial(novoNome);

        // Assert
        Assert.Equal(novoNome, pessoaJuridica.RazaoSocial);
        Assert.True(pessoaJuridica.Validate().IsValid);
    }

    [Fact]
    public void DeveAtualizarInscricaoEstadualComSucesso()
    {
        // Arrange
        var pessoaJuridica = new PessoaJuridica(
            _cnpjValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _inscricaoEstadualValida,
            false,
            _enderecoValido
        );

        var novaInscricaoMunicipal = "987654321";

        // Act
        pessoaJuridica.AtualizarInscricaoEstadual(novaInscricaoMunicipal);

        // Assert
        Assert.Equal(novaInscricaoMunicipal, pessoaJuridica.InscricaoEstadual);
        Assert.True(pessoaJuridica.Validate().IsValid);
    }

    [Fact]
    public void DeveAtualizarIsentoComSucesso()
    {
        // Arrange
        var pessoaJuridica = new PessoaJuridica(
            _cnpjValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _inscricaoEstadualValida,
            false,
            _enderecoValido
        );

        var novoIsento = true;

        // Act
        pessoaJuridica.AtualizarIsento(novoIsento);

        // Assert
        Assert.Equal(novoIsento, pessoaJuridica.Isento);
        Assert.True(pessoaJuridica.Validate().IsValid);
    }

    [Fact]
    public void DeveHerdarPropriedadesDeCliente()
    {
        // Arrange & Act
        var pessoaJuridica = new PessoaJuridica(
            _cnpjValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _inscricaoEstadualValida,
            false,
            _enderecoValido
        );

        // Assert
        Assert.IsAssignableFrom<Cliente>(pessoaJuridica);
        Assert.Equal(nameof(TipoCliente.PessoaJuridica), pessoaJuridica.Tipo);
        Assert.True(pessoaJuridica.Validate().IsValid);
    }
}