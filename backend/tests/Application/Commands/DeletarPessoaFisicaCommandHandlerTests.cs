using CadastroCliente.Application.Commands;
using CadastroCliente.Application.Commands.Handlers;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using Moq;

namespace CadastroClienteTests.Application.Commands;

public class DeletarPessoaFisicaCommandHandlerTests : BaseTests
{
    private readonly Mock<IClienteRepository> _repositoryMock = new();
    private readonly DeletarPessoaFisicaCommandHandler _handler;

    public DeletarPessoaFisicaCommandHandlerTests()
    {
        _handler = new DeletarPessoaFisicaCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Deletar_QuandoClienteExiste_DeveRetornarSucesso()
    {
        // Arrange
        var id = Guid.NewGuid();
        var pessoaFisica = new PessoaFisica(
            _cpfValido,
            _nomeValido,
            _telefoneValido,
            _emailValido,
            _dataNascimentoValida,
            _enderecoValido
        );

        _repositoryMock.Setup(r => r.Deletar(id))
            .ReturnsAsync(pessoaFisica);

        // Act
        var resultado = await _handler.Handle(new DeletarPessoaFisicaCommand { Id = id }, CancellationToken.None);

        // Assert
        Assert.False(resultado.IsError);
        _repositoryMock.Verify(r => r.Deletar(id), Times.Once);
    }
}

