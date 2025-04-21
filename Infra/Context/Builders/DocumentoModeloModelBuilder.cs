using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class DocumentoModeloModelBuilder : IEntityTypeConfiguration<DocumentoModelo>
    {
        public void Configure(EntityTypeBuilder<DocumentoModelo> builder)
        {
            builder.ToTable("DocumentoModelo");

            builder.Property(e => e.DocumentoModeloId).HasColumnName("DocumentoModeloID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.TipoDocumentoId).HasColumnName("TipoDocumentoID");

            builder.Property(e => e.Titulo).HasMaxLength(100);

            builder.HasOne(d => d.TipoDocumento)
                .WithMany(p => p.DocumentoModelos)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__TipoD__6B24EA82");
        }        
    }
}
