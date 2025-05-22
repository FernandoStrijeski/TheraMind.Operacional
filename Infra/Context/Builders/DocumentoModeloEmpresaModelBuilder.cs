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
            builder.ToTable("DocumentoModeloEmpresa");
            builder.HasKey(e => e.DocumentoModeloEmpresaID)
                .HasName("PK__Document__5A4DAD1973444193");


            builder.Property(e => e.DocumentoModeloEmpresaID).HasColumnName("DocumentoModeloEmpresaID");

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
                .HasConstraintName("FK__Documento__Empre__405A880E");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.DocumentoModeloEmpresas)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Filia__414EAC47");

            builder.HasOne(d => d.TipoDocumento)
                .WithMany(p => p.DocumentoModeloEmpresas)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__TipoD__4242D080");
        }        
    }
}
