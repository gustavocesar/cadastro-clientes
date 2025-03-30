using CadastroCliente.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.Property(e => e.CEP)
            .HasMaxLength(8);

        builder.Property(e => e.Logradouro)
            .HasMaxLength(100);

        builder.Property(e => e.Numero)
            .HasMaxLength(10);

        builder.Property(e => e.Bairro)
            .HasMaxLength(100);

        builder.Property(e => e.Cidade)
            .HasMaxLength(100);

        builder.Property(e => e.Estado)
            .HasMaxLength(100);
    }
}

