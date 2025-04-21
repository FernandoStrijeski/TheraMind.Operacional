using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class OrientacaoSexualModelBuilder : IEntityTypeConfiguration<OrientacaoSexual>
    {
        public void Configure(EntityTypeBuilder<OrientacaoSexual> builder)
        {
            builder.ToTable("OrientacaoSexual");

            builder.Property(e => e.OrientacaoSexualId).HasColumnName("OrientacaoSexualID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Descricao).HasMaxLength(255);
        }        
    }
}
