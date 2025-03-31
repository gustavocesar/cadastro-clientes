using CadastroCliente.Application.Commands;
using CadastroCliente.Application.Commands.Handlers;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using Moq;

namespace CadastroClienteTests.Application.Commands;

public class AlterarPessoaFisicaCommandHandlerTests : BaseTests
{
    private readonly Mock<IClienteRepository> _repositoryMock = new();
    private readonly AlterarPessoaFisicaCommandHandler _handler;

    public AlterarPessoaFisicaCommandHandlerTests()
    {
        _handler = new AlterarPessoaFisicaCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Atualizar_QuandoClienteNaoEncontrado_DeveRetornarErro()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new AlterarPessoaFisicaCommand
        {
            Id = id,
            Nome = _nomeValido,
            Cpf = _cpfValido,
            Email = _emailValido,
            DataNascimento = _dataNascimentoValida,
            Telefone = _telefoneValido,
            Endereco = _enderecoCommandRequestValido,
        };

        _repositoryMock.Setup(r => r.ObterPorId(id))
            .ReturnsAsync((PessoaFisica)null!);

        // Act
        var resultado = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(resultado.IsError);
        Assert.Equal("NotFound", resultado.FirstError.Type.ToString());
    }
}

