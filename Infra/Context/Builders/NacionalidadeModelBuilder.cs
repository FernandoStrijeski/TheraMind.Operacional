using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class NacionalidadeModelBuilder : IEntityTypeConfiguration<Nacionalidade>
    {
        public void Configure(EntityTypeBuilder<Nacionalidade> builder)
        {
            builder.Property(e => e.NacionalidadeId).HasColumnName("NacionalidadeID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Descricao).HasMaxLength(255);
        }        
    }
}
