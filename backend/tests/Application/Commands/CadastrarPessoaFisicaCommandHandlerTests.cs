using CadastroCliente.Application.Commands;
using CadastroCliente.Application.Commands.Handlers;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using Moq;

namespace CadastroClienteTests.Application.Commands;

public class CadastrarPessoaFisicaCommandHandlerTests : BaseTests
{
    private readonly Mock<IClienteRepository> _repositoryMock = new();
    private readonly CadastrarPessoaFisicaCommandHandler _handler;

    public CadastrarPessoaFisicaCommandHandlerTests()
    {
        _handler = new CadastrarPessoaFisicaCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Criar_QuandoDadosValidos_DeveRetornarSucesso()
    {
        // Arrange
        var command = new CadastrarPessoaFisicaCommand
        {
            Nome = _nomeValido,
            Cpf = _cpfValido,
            Email = _emailValido,
            DataNascimento = _dataNascimentoValida,
            Telefone = _telefoneValido,
            Endereco = _enderecoCommandRequestValido,
        };

        _repositoryMock.Setup(r => r.EmailJaCadastrado(It.IsAny<string>(), It.IsAny<Guid?>()))
            .ReturnsAsync(false);
        _repositoryMock.Setup(r => r.CpfJaCadastrado(It.IsAny<string>(), It.IsAny<Guid?>()))
            .ReturnsAsync(false);
        _repositoryMock.Setup(r => r.Criar(It.IsAny<PessoaFisica>()))
            .ReturnsAsync((PessoaFisica pf) => pf);

        // Act
        var resultado = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(resultado.IsError);
        _repositoryMock.Verify(r => r.Criar(It.IsAny<PessoaFisica>()), Times.Once);
    }

    [Fact]
    public async Task Criar_QuandoClienteJaCadastrado_DeveRetornarErro()
    {
        // Arrange
        var request = new CadastrarPessoaFisicaCommand
        {
            Nome = _nomeValido,
            Cpf = _cpfValido,
            Email = _emailValido
        };

        _repositoryMock.Setup(r => r.EmailJaCadastrado(It.IsAny<string>(), It.IsAny<Guid?>()))
            .ReturnsAsync(true);

        // Act
        var resultado = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(resultado.IsError);
        Assert.Equal("Cliente já cadastrado", resultado.FirstError.Description);
    }
}

