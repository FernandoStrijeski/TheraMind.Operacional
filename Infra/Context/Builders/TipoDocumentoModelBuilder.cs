using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class TipoDocumentoModelBuilder : IEntityTypeConfiguration<TipoDocumento>
    {
        public void Configure(EntityTypeBuilder<TipoDocumento> builder)
        {
            builder.ToTable("TipoDocumento");

            builder.Property(e => e.TipoDocumentoId).HasColumnName("TipoDocumentoID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Descricao).HasMaxLength(100);
        }        
    }
}
