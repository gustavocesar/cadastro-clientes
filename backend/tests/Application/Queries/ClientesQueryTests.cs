using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using CadastroClienteTests;
using Moq;

namespace CadastroCliente.Application.Queries.Tests;

public class ClientesQueryTests : BaseTests
{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly ClientesQuery _clientesQuery;

    public ClientesQueryTests()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _clientesQuery = new ClientesQuery(_clienteRepositoryMock.Object);
    }

    [Fact(DisplayName = "ObterTodos deve retornar lista vazia quando não houver clientes")]
    public async Task ObterTodos_QuandoNaoHouverClientes_DeveRetornarListaVazia()
    {
        // Arrange
        _clienteRepositoryMock.Setup(r => r.ObterTodos())
            .ReturnsAsync([]);

        // Act
        var resultado = await _clientesQuery.ObterTodos();

        // Assert
        Assert.Empty(resultado);
    }

    [Fact(DisplayName = "ObterTodos deve retornar clientes quando existirem pessoas físicas e jurídicas")]
    public async Task ObterTodos_QuandoExistiremClientes_DeveRetornarTodosClientes()
    {
        // Arrange
        var clientes = new List<Cliente> { _pessoaFisicaValida, _pessoaJuridicaValida };

        _clienteRepositoryMock.Setup(r => r.ObterTodos())
            .ReturnsAsync(clientes);

        // Act
        var resultado = await _clientesQuery.ObterTodos();
        var listaResultado = resultado.ToList();

        // Assert
        Assert.Equal(2, listaResultado.Count);
        Assert.Contains(listaResultado, r => r.Nome == _pessoaFisicaValida.Nome);
        Assert.Contains(listaResultado, r => r.Nome == _pessoaJuridicaValida.RazaoSocial);
    }
}