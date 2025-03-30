using CadastroCliente.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class PessoaJuridicaConfiguration : IEntityTypeConfiguration<PessoaJuridica>
{
    public void Configure(EntityTypeBuilder<PessoaJuridica> builder)
    {
        builder.Property(p => p.Cnpj)
            .HasMaxLength(14)
            .IsRequired();

        builder.HasIndex(p => p.Cnpj);

        builder.Property(p => p.Cnpj)
            .HasMaxLength(14)
            .IsRequired();

        builder.Property(p => p.RazaoSocial)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(p => p.InscricaoEstadual)
            .HasMaxLength(100)
            .IsRequired();
    }
}

