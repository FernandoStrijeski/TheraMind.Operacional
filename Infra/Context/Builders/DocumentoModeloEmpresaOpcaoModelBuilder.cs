using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class DocumentoModeloEmpresaOpcaoModelBuilder : IEntityTypeConfiguration<DocumentoModeloEmpresaOpcao>
    {
        public void Configure(EntityTypeBuilder<DocumentoModeloEmpresaOpcao> builder)
        {
            builder.ToTable("DocumentoModeloEmpresaOpcao");

            builder.Property(e => e.DocumentoModeloEmpresaOpcaoId).HasColumnName("DocumentoModeloEmpresaOpcaoID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.Transparencia).HasColumnType("decimal(18, 0)");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.DocumentoModeloEmpresaOpcaos)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Empre__5F492382");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.DocumentoModeloEmpresaOpcaos)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documento__Filia__603D47BB");
        }        
    }
}
