using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class ModeloAnamneseGModelBuilder : IEntityTypeConfiguration<ModeloAnamneseG>
    {
        public void Configure(EntityTypeBuilder<ModeloAnamneseG> builder)
        {
            builder.ToTable("ModeloAnamneseG");

            builder.Property(e => e.ModeloAnamneseGid).HasColumnName("ModeloAnamneseGID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Privado)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Titulo).HasMaxLength(100);
        }        
    }
}
