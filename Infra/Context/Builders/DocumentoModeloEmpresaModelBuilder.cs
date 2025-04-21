using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class DocumentoModeloEmpresaModelBuilder : IEntityTypeConfiguration<DocumentoModeloEmpresa>
    {
        public void Configure(EntityTypeBuilder<DocumentoModeloEmpresa> builder)
        {
            builder.HasKey(e => e.DocumentoModeloId)
                .HasName("PK__Document__BA5C66194C50DE4A");

            builder.ToTable("DocumentoModeloEmpresa");

            builder.Property(e => e.DocumentoModeloId).HasColumnName("DocumentoModeloID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.TipoDocumentoId).HasColumnName("TipoDocumentoID");

            builder.Property(e => e.Titulo).HasMaxLength(100);

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.DocumentoModeloEmpresas)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Empre__634EBE90");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.DocumentoModeloEmpresas)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Filia__6442E2C9");

            builder.HasOne(d => d.TipoDocumento)
                .WithMany(p => p.DocumentoModeloEmpresas)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__TipoD__65370702");
        }        
    }
}
