using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace CadastroCliente.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasDiscriminator<string>("Tipo")
            .HasValue<Cliente>("Cliente")
            .HasValue<PessoaFisica>(nameof(TipoCliente.PessoaFisica))
            .HasValue<PessoaJuridica>(nameof(TipoCliente.PessoaJuridica));

        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.Property(p => p.Telefone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.DataNascimento)
            .HasColumnType("date")
            .IsRequired();

        builder.HasOne(c => c.Endereco)
            .WithOne()
            .HasForeignKey<Endereco>(e => e.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
