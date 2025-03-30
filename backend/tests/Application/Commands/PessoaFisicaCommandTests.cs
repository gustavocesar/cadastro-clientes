using Moq;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using CadastroCliente.Application.Commands.Requests;
using CadastroCliente.Application.Commands;

namespace CadastroClienteTests.Application.Commands;

public class PessoaFisicaCommandTests : BaseTests
{
    private readonly Mock<IClienteRepository> _repositoryMock;
    private readonly PessoaFisicaCommand _command;

    public PessoaFisicaCommandTests()
    {
        _repositoryMock = new Mock<IClienteRepository>();
        _command = new PessoaFisicaCommand(_repositoryMock.Object);
    }

    [Fact]
    public async Task Criar_QuandoDadosValidos_DeveRetornarSucesso()
    {
        // Arrange
        var request = new PessoaFisicaCommandRequest
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
        var resultado = await _command.Criar(request);

        // Assert
        Assert.False(resultado.IsError);
        _repositoryMock.Verify(r => r.Criar(It.IsAny<PessoaFisica>()), Times.Once);
    }

    [Fact]
    public async Task Criar_QuandoClienteJaCadastrado_DeveRetornarErro()
    {
        // Arrange
        var request = new PessoaFisicaCommandRequest
        {
            Nome = _nomeValido,
            Cpf = _cpfValido,
            Email = _emailValido
        };

        _repositoryMock.Setup(r => r.EmailJaCadastrado(It.IsAny<string>(), It.IsAny<Guid?>()))
            .ReturnsAsync(true);

        // Act
        var resultado = await _command.Criar(request);

        // Assert
        Assert.True(resultado.IsError);
        Assert.Equal("Cliente já cadastrado", resultado.FirstError.Description);
    }

    [Fact]
    public async Task Atualizar_QuandoClienteNaoEncontrado_DeveRetornarErro()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new PessoaFisicaCommandRequest();

        _repositoryMock.Setup(r => r.ObterPorId(id))
            .ReturnsAsync((PessoaFisica)null!);

        // Act
        var resultado = await _command.Atualizar(id, request);

        // Assert
        Assert.True(resultado.IsError);
        Assert.Equal("NotFound", resultado.FirstError.Type.ToString());
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
        var resultado = await _command.Deletar(id);

        // Assert
        Assert.False(resultado.IsError);
        _repositoryMock.Verify(r => r.Deletar(id), Times.Once);
    }
}