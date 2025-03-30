using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Application.Commands.Requests;

[ExcludeFromCodeCoverage]
public class EnderecoCommandRequest
{
    public string Cep { get; set; } = null!;
    public string Logradouro { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
}
