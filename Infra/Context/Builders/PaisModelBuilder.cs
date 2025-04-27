using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class PaisModelBuilder : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("Pais");
            builder.HasKey(e => e.PaisId)
                    .HasName("PK__Pais__B501E1A53A9B473B");

            builder.Property(e => e.PaisId)
                .ValueGeneratedNever()
                .HasColumnName("PaisID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Nome).HasMaxLength(255);
        }        
    }
}
