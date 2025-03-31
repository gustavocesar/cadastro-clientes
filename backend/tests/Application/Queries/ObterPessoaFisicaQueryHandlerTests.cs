using CadastroCliente.Application.Queries;
using CadastroCliente.Application.Queries.Handlers;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using Moq;

namespace CadastroClienteTests.Application.Queries;

public class ObterPessoaFisicaQueryHandlerTest : BaseTests
{
    private readonly ObterPessoaFisicaQueryHandler _handler;
    private readonly ObterPessoaFisicaQuery _query = new() { Id = Guid.NewGuid() };
    private readonly Mock<IClienteRepository> _clienteRepositoryMock = new();

    public ObterPessoaFisicaQueryHandlerTest()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _handler = new ObterPessoaFisicaQueryHandler(_clienteRepositoryMock.Object);
    }

    [Fact]
    public async Task ObterPorId_QuandoClienteNaoEncontrado_DeveRetornarErro()
    {
        // Arrange
        _clienteRepositoryMock.Setup(r => r.ObterPorId(It.IsAny<Guid>()))
            .ReturnsAsync((Cliente)null!);

        // Act
        var resultado = await _handler.Handle(_query, CancellationToken.None);

        // Assert
        Assert.Equal("NotFound", resultado.FirstError.Type.ToString());
    }

    [Fact]
    public async Task ObterPorId_QuandoClienteEncontrado_DeveRetornarCliente()
    {
        // Arrange
        _clienteRepositoryMock.Setup(r => r.ObterPorIdAsNoTracking(It.IsAny<Guid>()))
            .ReturnsAsync(_pessoaFisicaValida);

        // Act
        var resultado = await _handler.Handle(_query, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado.Value);
        Assert.Equal(_pessoaFisicaValida.Id, resultado.Value.Id);
    }
}

