using Moq;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Repositories;
using CadastroCliente.Application.Commands.Requests;
using CadastroCliente.Application.Commands;

namespace CadastroClienteTests.Application.Commands;

public class PessoaJuridicaCommandTests : BaseTests
{
    private readonly Mock<IClienteRepository> _repositoryMock;
    private readonly PessoaJuridicaCommand _command;

    public PessoaJuridicaCommandTests()
    {
        _repositoryMock = new Mock<IClienteRepository>();
        _command = new PessoaJuridicaCommand(_repositoryMock.Object);
    }

    [Fact]
    public async Task Criar_QuandoDadosValidos_DeveRetornarSucesso()
    {
        // Arrange
        var request = new PessoaJuridicaCommandRequest
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
        var resultado = await _command.Criar(request);

        // Assert
        Assert.False(resultado.IsError);
        _repositoryMock.Verify(r => r.Criar(It.IsAny<PessoaJuridica>()), Times.Once);
    }

    [Fact]
    public async Task Criar_QuandoClienteJaCadastrado_DeveRetornarErro()
    {
        // Arrange
        var request = new PessoaJuridicaCommandRequest
        {
            RazaoSocial = _nomeValido,
            Cnpj = _cnpjValido,
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
        var request = new PessoaJuridicaCommandRequest();

        _repositoryMock.Setup(r => r.ObterPorId(id))
            .ReturnsAsync((PessoaJuridica)null!);

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
        var resultado = await _command.Deletar(id);

        // Assert
        Assert.False(resultado.IsError);
        _repositoryMock.Verify(r => r.Deletar(id), Times.Once);
    }
}