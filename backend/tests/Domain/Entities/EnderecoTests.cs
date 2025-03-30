using CadastroCliente.Domain.Entities;
using CadastroCliente.Extensions;

namespace CadastroClienteTests.Domain.Entities;

public class EnderecoTests : BaseTests
{
    [Fact]
    public void EnderecoConstrutorDeveInicializarPropriedades()
    {
        // Arrange
        var cep = "12345-678";
        var logradouro = "Rua A";
        var numero = "123";
        var bairro = "Bairro B";
        var cidade = "Cidade C";
        var estado = "GO";

        // Act
        var endereco = new Endereco(cep, logradouro, numero, bairro, cidade, estado);

        // Assert
        Assert.Equal("12345678", endereco.CEP);
        Assert.Equal(logradouro, endereco.Logradouro);
        Assert.Equal(numero, endereco.Numero);
        Assert.Equal(bairro, endereco.Bairro);
        Assert.Equal(cidade, endereco.Cidade);
        Assert.Equal(estado, endereco.Estado);
    }

    [Fact]
    public void EnderecoAtualizarCepDeveAtualizarCep()
    {
        // Arrange
        var novoCep = "87654-321";

        // Act
        _enderecoValido.AtualizarCep(novoCep);

        // Assert
        Assert.Equal(novoCep.RemoverCaracteresNaoNumericos(), _enderecoValido.CEP);
    }

    [Fact]
    public void EnderecoAtualizarLogradouroDeveAtualizarLogradouro()
    {
        // Arrange
        var novoLogradouro = "Rua B";

        // Act
        _enderecoValido.AtualizarLogradouro(novoLogradouro);

        // Assert
        Assert.Equal(novoLogradouro, _enderecoValido.Logradouro);
    }

    [Fact]
    public void EnderecoAtualizarNumeroDeveAtualizarNumero()
    {
        // Arrange
        var novoNumero = "456";

        // Act
        _enderecoValido.AtualizarNumero(novoNumero);

        // Assert
        Assert.Equal(novoNumero, _enderecoValido.Numero);
    }

    [Fact]
    public void EnderecoAtualizarBairroDeveAtualizarBairro()
    {
        // Arrange
        var novoBairro = "Bairro C";

        // Act
        _enderecoValido.AtualizarBairro(novoBairro);

        // Assert
        Assert.Equal(novoBairro, _enderecoValido.Bairro);
    }

    [Fact]
    public void EnderecoAtualizarCidadeDeveAtualizarCidade()
    {
        // Arrange
        var novaCidade = "Cidade D";

        // Act
        _enderecoValido.AtualizarCidade(novaCidade);

        // Assert
        Assert.Equal(novaCidade, _enderecoValido.Cidade);
    }

    [Fact]
    public void EnderecoAtualizarEstadoDeveAtualizarEstado()
    {
        // Arrange
        var novoEstado = "RJ";

        // Act
        _enderecoValido.AtualizarEstado(novoEstado);

        // Assert
        Assert.Equal(novoEstado, _enderecoValido.Estado);
    }
}
