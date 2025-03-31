using CadastroCliente.Application.Commands;
using CadastroCliente.Application.Commands.Handlers;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using Moq;

namespace CadastroClienteTests.Application.Commands;

public class AlterarPessoaJuridicaCommandHandlerTests : BaseTests
{
    private readonly Mock<IClienteRepository> _repositoryMock = new();
    private readonly AlterarPessoaJuridicaCommandHandler _handler;

    public AlterarPessoaJuridicaCommandHandlerTests()
    {
        _handler = new AlterarPessoaJuridicaCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Atualizar_QuandoClienteNaoEncontrado_DeveRetornarErro()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new AlterarPessoaJuridicaCommand
        {
            Id = id,
            RazaoSocial = _nomeValido,
            Cnpj = _cnpjValido,
            InscricaoEstadual = _inscricaoEstadualValida,
        };

        _repositoryMock.Setup(r => r.ObterPorId(id))
            .ReturnsAsync((PessoaJuridica)null!);

        // Act
        var resultado = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(resultado.IsError);
        Assert.Equal("NotFound", resultado.FirstError.Type.ToString());
    }
}

