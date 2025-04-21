using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class EmpresaModelBuilder : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {

            builder.ToTable("Empresa");
            builder.HasKey(e => e.EmpresaId);

            builder.Property(e => e.EmpresaId)
                .HasColumnName("EmpresaID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.NomeFantasia).HasMaxLength(255);

            builder.Property(e => e.RazaoSocial).HasMaxLength(255);
        }
    }
}
