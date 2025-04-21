using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class EmpresaAssinaturaModelBuilder : IEntityTypeConfiguration<EmpresaAssinatura>
    {
        public void Configure(EntityTypeBuilder<EmpresaAssinatura> builder)
        {
            builder.ToTable("EmpresaAssinatura");

            builder.Property(e => e.EmpresaAssinaturaId)
                .HasColumnName("EmpresaAssinaturaID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.Ativo).HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DataExpiracao).HasColumnType("date");

            builder.Property(e => e.DescontoPromocional).HasColumnType("decimal(18, 0)");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.PlanoId).HasColumnName("PlanoID");

            builder.Property(e => e.ValorAtual).HasColumnType("decimal(18, 0)");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.EmpresaAssinaturas)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmpresaAs__Empre__4C364F0E");

            builder.HasOne(d => d.Plano)
                .WithMany(p => p.EmpresaAssinaturas)
                .HasForeignKey(d => d.PlanoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmpresaAs__Plano__4D2A7347");
        }        
    }
}
