using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class PacoteFechadoModelBuilder : IEntityTypeConfiguration<PacoteFechado>
    {
        public void Configure(EntityTypeBuilder<PacoteFechado> builder)
        {
            builder.ToTable("PacoteFechado");

            builder.Property(e => e.PacoteFechadoId).HasColumnName("PacoteFechadoID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.ValorTotal).HasColumnType("decimal(18, 0)");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.PacoteFechados)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PacoteFec__Empre__7849DB76");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.PacoteFechados)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PacoteFec__Filia__793DFFAF");
        }        
    }
}
