using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class TipoEtniaModelBuilder : IEntityTypeConfiguration<TipoEtnia>
    {
        public void Configure(EntityTypeBuilder<TipoEtnia> builder)
        {
            builder.ToTable("TipoEtnia");
            builder.HasKey(e => e.TipoEtniaId)
                    .HasName("PK__TipoEtni__69ACF1F6E2E6EFAF");

            builder.Property(e => e.TipoEtniaId).HasColumnName("TipoEtniaID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Descricao).HasMaxLength(255);
        }        
    }
}
