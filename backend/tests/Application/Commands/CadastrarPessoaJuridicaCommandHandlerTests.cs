using CadastroCliente.Application.Commands;
using CadastroCliente.Application.Commands.Handlers;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using Moq;

namespace CadastroClienteTests.Application.Commands;

public class CadastrarPessoaJuridicaCommandHandlerTests : BaseTests
{
    private readonly Mock<IClienteRepository> _repositoryMock = new();
    private readonly CadastrarPessoaJuridicaCommandHandler _handler;

    public CadastrarPessoaJuridicaCommandHandlerTests()
    {
        _handler = new CadastrarPessoaJuridicaCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Criar_QuandoDadosValidos_DeveRetornarSucesso()
    {
        // Arrange
        var request = new CadastrarPessoaJuridicaCommand
        {
            RazaoSocial = _nomeValido,
            Cnpj = _cnpjValido,
            Email = _emailValido,
            DataNascimento = _dataNascimentoValida,
            Telefone = _telefoneValido,
            InscricaoEstadual = _inscricaoEstadualValida,
            Isento = false,
            Endereco = _enderecoCommandRequestValido,
        };

        _repositoryMock.Setup(r => r.EmailJaCadastrado(It.IsAny<string>(), It.IsAny<Guid?>()))
            .ReturnsAsync(false);
        _repositoryMock.Setup(r => r.CnpjJaCadastrado(It.IsAny<string>(), It.IsAny<Guid?>()))
            .ReturnsAsync(false);
        _repositoryMock.Setup(r => r.Criar(It.IsAny<PessoaJuridica>()))
            .ReturnsAsync((PessoaJuridica pf) => pf);

        // Act
        var resultado = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(resultado.IsError);
        _repositoryMock.Verify(r => r.Criar(It.IsAny<PessoaJuridica>()), Times.Once);
    }

    [Fact]
    public async Task Criar_QuandoClienteJaCadastrado_DeveRetornarErro()
    {
        // Arrange
        var request = new CadastrarPessoaJuridicaCommand
        {
            RazaoSocial = _nomeValido,
            Cnpj = _cnpjValido,
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

