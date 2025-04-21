using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class PlanoModelBuilder : IEntityTypeConfiguration<Plano>
    {
        public void Configure(EntityTypeBuilder<Plano> builder)
        {
            builder.ToTable("Plano");

            builder.Property(e => e.PlanoId)
                .HasColumnName("PlanoID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.Ativo).HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DescontoPromocional).HasColumnType("decimal(18, 0)");

            builder.Property(e => e.NomePlano).HasMaxLength(50);

            builder.Property(e => e.ValorPlanoAnual).HasColumnType("decimal(18, 0)");

            builder.Property(e => e.ValorPlanoMensal).HasColumnType("decimal(18, 0)");
        }        
    }
}
