using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class TipoLogradouroModelBuilder : IEntityTypeConfiguration<TipoLogradouro>
    {
        public void Configure(EntityTypeBuilder<TipoLogradouro> builder)
        {
            builder.ToTable("TipoLogradouro");

            builder.Property(e => e.TipoLogradouroId)
                .HasMaxLength(5)
                .HasColumnName("TipoLogradouroID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Descricao).HasMaxLength(255);
        }        
    }
}
