using CadastroCliente.Application.Queries;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using Moq;

namespace CadastroClienteTests.Application.Queries;

public class PessoaJuridicaQueryTests : BaseTests
{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly PessoaJuridicaQuery _pessoaJuridicaQuery;

    public PessoaJuridicaQueryTests()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _pessoaJuridicaQuery = new PessoaJuridicaQuery(_clienteRepositoryMock.Object);
    }

    [Fact]
    public async Task ObterPorId_QuandoClienteNaoEncontrado_DeveRetornarErro()
    {
        // Arrange
        var id = Guid.NewGuid();

        _clienteRepositoryMock.Setup(r => r.ObterPorId(id))
            .ReturnsAsync((Cliente)null!);

        // Act
        var resultado = await _pessoaJuridicaQuery.ObterPorId(id);

        // Assert
        Assert.Equal("NotFound", resultado.FirstError.Type.ToString());
    }

    [Fact]
    public async Task ObterPorId_QuandoClienteEncontrado_DeveRetornarCliente()
    {
        // Arrange
        var id = Guid.NewGuid();

        _clienteRepositoryMock.Setup(r => r.ObterPorIdAsNoTracking(id))
            .ReturnsAsync(_pessoaJuridicaValida);

        // Act
        var resultado = await _pessoaJuridicaQuery.ObterPorId(id);

        // Assert
        Assert.NotNull(resultado.Value);
        Assert.Equal(_pessoaJuridicaValida.Id, resultado.Value.Id);
    }
}
    
