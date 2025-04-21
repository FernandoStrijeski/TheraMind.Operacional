using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class DocumentoModelBuilder : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.Property(e => e.DocumentoId).HasColumnName("DocumentoID");

            builder.Property(e => e.CandidatoId).HasColumnName("CandidatoID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.TipoDocumentoId)
                .HasMaxLength(15)
                .HasColumnName("TipoDocumentoID");
            
            builder.HasOne(d => d.TipoDocumento)
                .WithMany()
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__TipoD__2B0A656D");
        }        
    }
}
