using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class AcessoAdministrativoModelBuilder : IEntityTypeConfiguration<AcessoAdministrativo>
    {
        public void Configure(EntityTypeBuilder<AcessoAdministrativo> builder)
        {
            builder.ToTable("AcessoAdministrativo");

            builder.Property(e => e.AcessoAdministrativoId)
                .HasColumnName("AcessoAdministrativoID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Celular).HasMaxLength(14);

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Email).HasMaxLength(255);

            builder.Property(e => e.NomeCompleto).HasMaxLength(255);
        }        
    }
}
