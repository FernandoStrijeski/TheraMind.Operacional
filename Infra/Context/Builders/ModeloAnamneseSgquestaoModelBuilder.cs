using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class ModeloAnamneseSgquestaoModelBuilder : IEntityTypeConfiguration<ModeloAnamneseSgquestao>
    {
        public void Configure(EntityTypeBuilder<ModeloAnamneseSgquestao> builder)
        {
            builder.ToTable("ModeloAnamneseSGQuestao");

            builder.Property(e => e.ModeloAnamneseSgquestaoId).HasColumnName("ModeloAnamneseSGQuestaoID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModeloAnamneseGid).HasColumnName("ModeloAnamneseGID");

            builder.Property(e => e.ModeloAnamneseSgid).HasColumnName("ModeloAnamneseSGID");

            builder.Property(e => e.Titulo).HasMaxLength(250);

            builder.HasOne(d => d.ModeloAnamneseG)
                .WithMany(p => p.ModeloAnamneseSgquestaos)
                .HasForeignKey(d => d.ModeloAnamneseGid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ModeloAna__Model__7A672E12");

            builder.HasOne(d => d.ModeloAnamneseSg)
                .WithMany(p => p.ModeloAnamneseSgquestaos)
                .HasForeignKey(d => d.ModeloAnamneseSgid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ModeloAna__Model__7B5B524B");
        }        
    }
}
