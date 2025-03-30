using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Application.Query.Responses;

[ExcludeFromCodeCoverage]
public class EnderecoQueryResponse
{
    public string? CEP { get; set; } = null!;
    public string? Logradouro { get; set; } = null!;
    public string? Numero { get; set; } = null!;
    public string? Bairro { get; set; } = null!;
    public string? Cidade { get; set; } = null!;
    public string? Estado { get; set; } = null!;
}
