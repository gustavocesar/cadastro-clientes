using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Extensions;

[ExcludeFromCodeCoverage]
public static class AppExtensions
{
    public static WebApplication UseArchitecture(this WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseStatusCodePages();
        app.UseCors("CorsPolicy");

        return app;
    }
}