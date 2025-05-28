using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class ModeloAnamneseSgModelBuilder : IEntityTypeConfiguration<ModeloAnamneseSg>
    {
        public void Configure(EntityTypeBuilder<ModeloAnamneseSg> builder)
        {
            builder.ToTable("ModeloAnamneseSG");

            builder.Property(e => e.ModeloAnamneseSgid).HasColumnName("ModeloAnamneseSGID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModeloAnamneseGid).HasColumnName("ModeloAnamneseGID");

            builder.Property(e => e.Titulo).HasMaxLength(250);

            builder.HasOne(d => d.ModeloAnamneseG)
                .WithMany(p => p.ModeloAnamneseSubGrupos)
                .HasForeignKey(d => d.ModeloAnamneseGid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ModeloAna__Model__75A278F5");
        }        
    }
}
