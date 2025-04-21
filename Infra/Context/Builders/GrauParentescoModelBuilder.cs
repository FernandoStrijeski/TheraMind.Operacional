using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class GrauParentescoModelBuilder : IEntityTypeConfiguration<GrauParentesco>
    {
        public void Configure(EntityTypeBuilder<GrauParentesco> builder)
        {
            builder.ToTable("GrauParentesco");

            builder.Property(e => e.GrauParentescoId).HasColumnName("GrauParentescoID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Descricao).HasMaxLength(255);
        }        
    }
}
