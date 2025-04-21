using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class EmpresaFaturaModelBuilder : IEntityTypeConfiguration<EmpresaFatura>
    {
        public void Configure(EntityTypeBuilder<EmpresaFatura> builder)
        {
            builder.ToTable("EmpresaFatura");

            builder.Property(e => e.EmpresaFaturaId).HasColumnName("EmpresaFaturaID");

            builder.Property(e => e.Ativo).HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DataExpiracao).HasColumnType("date");

            builder.Property(e => e.DataInicio).HasColumnType("date");

            builder.Property(e => e.DataPagamento).HasColumnType("date");

            builder.Property(e => e.Descricao).HasMaxLength(30);

            builder.Property(e => e.EmpresaAssinaturaId).HasColumnName("EmpresaAssinaturaID");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.PlanoId).HasColumnName("PlanoID");

            builder.Property(e => e.Valor).HasColumnType("decimal(18, 0)");

            builder.HasOne(d => d.EmpresaAssinatura)
                .WithMany(p => p.EmpresaFaturas)
                .HasForeignKey(d => d.EmpresaAssinaturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmpresaFa__Empre__51EF2864");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.EmpresaFaturas)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmpresaFa__Empre__52E34C9D");

            builder.HasOne(d => d.Plano)
                .WithMany(p => p.EmpresaFaturas)
                .HasForeignKey(d => d.PlanoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmpresaFa__Plano__53D770D6");
        }        
    }
}
