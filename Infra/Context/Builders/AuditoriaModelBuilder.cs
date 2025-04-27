using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class AuditoriaModelBuilder : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> builder)
        {
            builder.ToTable("Auditoria");
            builder.HasKey(e => e.AuditoriaId)
    .HasName("PK__Auditori__095694E3B033D81E");

            builder.Property(e => e.AuditoriaId)
                .HasColumnName("AuditoriaID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.DataHoraExecucao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.Ipacesso)
                .HasMaxLength(250)
                .HasColumnName("IPAcesso");

            builder.Property(e => e.PerfilAcesso).HasMaxLength(20);

            builder.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.EmpresaId)
                .HasConstraintName("FK__Auditoria__Empre__093F5D4E");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.FilialId)
                .HasConstraintName("FK__Auditoria__Filia__0A338187");

            builder.HasOne(d => d.Usuario)
                .WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Auditoria__Usuar__0B27A5C0");
        }        
    }
}
