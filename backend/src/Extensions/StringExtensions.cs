using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;
namespace CadastroCliente.Extensions;

[ExcludeFromCodeCoverage]
public static class StringExtensions
{
    public static string RemoverCaracteresNaoNumericos(this string? value) =>
        string.IsNullOrWhiteSpace(value) ? string.Empty : Regex.Replace(value, @"[^0-9]", string.Empty, default, TimeSpan.FromSeconds(3));
}
