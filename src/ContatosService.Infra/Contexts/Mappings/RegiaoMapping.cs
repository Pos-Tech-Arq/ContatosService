using ContatosService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContatosService.Infra.Contexts.Mappings;

public class RegiaoMapping : IEntityTypeConfiguration<Regiao>
{
    public void Configure(EntityTypeBuilder<Regiao> builder)
    {
        builder.ToTable("Regioes");

        builder
            .Property(c => c.Id)
            .IsRequired();

        builder
            .HasKey(c => c.Id);

        builder.Property(c => c.Ddd)
            .HasColumnType("varchar(3)")
            .IsRequired();

        builder.Property(c => c.Estado)
            .HasColumnType("varchar(100)")
            .IsRequired();
    }
}