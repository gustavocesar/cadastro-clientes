using Application.Queries;
using CadastroCliente.Application.Queries.Handlers;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using CadastroClienteTests;
using Moq;


namespace CadastroCliente.Application.Queries.Tests;

public class ObterTodosClientesQueryHandlerTests : BaseTests
{
    private readonly ObterTodosClientesQueryHandler _handler;
    private readonly Mock<IClienteRepository> _clienteRepositoryMock = new();

    public ObterTodosClientesQueryHandlerTests()
    {
        _handler = new ObterTodosClientesQueryHandler(_clienteRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_DeveRetornarListaDeClientes()
    {
        // Arrange
        var clientes = new List<Cliente> { _pessoaFisicaValida, _pessoaJuridicaValida };

        _clienteRepositoryMock.Setup(r => r.ObterTodos())
            .ReturnsAsync(clientes);

        var query = new ObterTodosClientesQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(clientes.Count, result.Count());
    }
}

