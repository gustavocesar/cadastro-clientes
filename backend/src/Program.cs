using CadastroCliente.Endpoints;
using CadastroCliente.Extensions;
using CadastroCliente.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddArchitectures();

builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseArchitecture()
   .MapEndpointsClientes()
   .MapEndpointsPessoasFisicas()
   .MapEndpointsPessoasJuridicas()
   .MigrateDatabase<ClienteContext>();

app.Run();
