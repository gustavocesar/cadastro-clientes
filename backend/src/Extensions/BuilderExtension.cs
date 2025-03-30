using CadastroCliente.Application.Commands;
using CadastroCliente.Application.Commands.Interfaces;
using CadastroCliente.Application.Queries;
using CadastroCliente.Application.Queries.Interfaces;
using CadastroCliente.Domain.Repositories;
using CadastroCliente.Domain.Validators;
using CadastroCliente.Infrastructure;
using CadastroCliente.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
namespace CadastroCliente.Extensions;

[ExcludeFromCodeCoverage]
public static class BuilderExtension
{
    public static WebApplicationBuilder AddArchitectures(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services
            .AddValidatorsFromAssemblyContaining<ClienteValidator>()
            .AddValidatorsFromAssemblyContaining<EnderecoValidator>()
            .AddValidatorsFromAssemblyContaining<PessoaFisicaValidator>()
            .AddValidatorsFromAssemblyContaining<PessoaJuridicaValidator>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        AddDbContext(builder);

        AddServices(builder);

        return builder;
    }

    private static void AddDbContext(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ClienteContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
    }

    private static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPessoaFisicaCommand, PessoaFisicaCommand>();
        builder.Services.AddScoped<IPessoaJuridicaCommand, PessoaJuridicaCommand>();
        builder.Services.AddScoped<IClientesQuery, ClientesQuery>();
        builder.Services.AddScoped<IPessoaFisicaQuery, PessoaFisicaQuery>();
        builder.Services.AddScoped<IPessoaJuridicaQuery, PessoaJuridicaQuery>();
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
    }
}