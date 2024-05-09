using ContatosService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContatosService.Infra.Contexts.Mappings;

public class ContatoMapping : IEntityTypeConfiguration<Contato>
{
    public void Configure(EntityTypeBuilder<Contato> builder)
    {
        builder.ToTable("Contatos");

        builder
            .Property(c => c.Id)
            .IsRequired();

        builder
            .HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .HasColumnType("varchar(300)")
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnType("varchar(200)")
            .IsRequired();

        builder.OwnsOne(x => x.Telefone, telefoneBuilder =>
        {
            telefoneBuilder.Property(c => c.Numero)
                .HasColumnName("Numero")
                .IsRequired();

            telefoneBuilder.Property(c => c.Ddd)
                .HasColumnName("Ddd")
                .IsRequired();
        });
    }
}