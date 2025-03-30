using CadastroCliente.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class PessoaFisicaConfiguration : IEntityTypeConfiguration<PessoaFisica>
{
    public void Configure(EntityTypeBuilder<PessoaFisica> builder)
    {
        builder.Property(p => p.Cpf)
            .HasMaxLength(11)
            .IsRequired();

        builder.HasIndex(p => p.Cpf);

        builder.Property(p => p.Nome)
            .HasMaxLength(100)
            .IsRequired();
    }
}

