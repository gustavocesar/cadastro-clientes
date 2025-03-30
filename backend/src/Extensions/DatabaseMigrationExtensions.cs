using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
namespace CadastroCliente.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseMigrationExtensions
{
    public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var db = services.GetRequiredService<T>();

        db.Database.Migrate();

        return host;
    }
}