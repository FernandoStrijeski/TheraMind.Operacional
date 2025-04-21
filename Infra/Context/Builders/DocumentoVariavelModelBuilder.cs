using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class DocumentoVariavelModelBuilder : IEntityTypeConfiguration<DocumentoVariavel>
    {
        public void Configure(EntityTypeBuilder<DocumentoVariavel> builder)
        {
            builder.ToTable("DocumentoVariavel");

            builder.Property(e => e.DocumentoVariavelId).HasColumnName("DocumentoVariavelID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.NomeCampo).HasMaxLength(250);

            builder.Property(e => e.NomeTabela).HasMaxLength(250);

            builder.Property(e => e.NomeVariavel).HasMaxLength(250);
        }        
    }
}
