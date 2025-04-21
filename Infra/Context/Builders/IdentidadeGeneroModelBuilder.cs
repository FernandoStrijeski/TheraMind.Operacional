using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class IdentidadeGeneroModelBuilder : IEntityTypeConfiguration<IdentidadeGenero>
    {
        public void Configure(EntityTypeBuilder<IdentidadeGenero> builder)
        {
            builder.ToTable("IdentidadeGenero");

            builder.Property(e => e.IdentidadeGeneroId).HasColumnName("IdentidadeGeneroID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Descricao).HasMaxLength(255);
        }        
    }
}
