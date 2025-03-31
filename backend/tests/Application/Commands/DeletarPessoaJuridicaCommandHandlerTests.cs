using CadastroCliente.Application.Commands;
using CadastroCliente.Application.Commands.Handlers;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using Moq;

namespace CadastroClienteTests.Application.Commands;

public class DeletarPessoaJuridicaCommandHandlerTests : BaseTests
{
    private readonly Mock<IClienteRepository> _repositoryMock = new();
    private readonly DeletarPessoaJuridicaCommandHandler _handler;

    public DeletarPessoaJuridicaCommandHandlerTests()
    {
        _handler = new DeletarPessoaJuridicaCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Deletar_QuandoClienteExiste_DeveRetornarSucesso()
    {
        // Arrange
        var id = Guid.NewGuid();
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

        _repositoryMock.Setup(r => r.Deletar(id))
            .ReturnsAsync(pessoaJuridica);

        // Act
        var resultado = await _handler.Handle(new DeletarPessoaJuridicaCommand { Id = id }, CancellationToken.None);

        // Assert
        Assert.False(resultado.IsError);
        _repositoryMock.Verify(r => r.Deletar(id), Times.Once);
    }
}

