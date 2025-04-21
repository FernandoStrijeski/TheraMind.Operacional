using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class ModeloAnamneseSgquestaoOModelBuilder : IEntityTypeConfiguration<ModeloAnamneseSgquestaoO>
    {
        public void Configure(EntityTypeBuilder<ModeloAnamneseSgquestaoO> builder)
        {
            builder.ToTable("ModeloAnamneseSGQuestaoO");

            builder.Property(e => e.ModeloAnamneseSgquestaoOid).HasColumnName("ModeloAnamneseSGQuestaoOID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModeloAnamneseGid).HasColumnName("ModeloAnamneseGID");

            builder.Property(e => e.ModeloAnamneseSgid).HasColumnName("ModeloAnamneseSGID");

            builder.Property(e => e.ModeloAnamneseSgquestaoId).HasColumnName("ModeloAnamneseSGQuestaoID");

            builder.Property(e => e.Texto).HasMaxLength(250);

            builder.HasOne(d => d.ModeloAnamneseG)
                .WithMany(p => p.ModeloAnamneseSgquestaoOs)
                .HasForeignKey(d => d.ModeloAnamneseGid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ModeloAna__Model__00200768");

            builder.HasOne(d => d.ModeloAnamneseSg)
                .WithMany(p => p.ModeloAnamneseSgquestaoOs)
                .HasForeignKey(d => d.ModeloAnamneseSgid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ModeloAna__Model__01142BA1");

            builder.HasOne(d => d.ModeloAnamneseSgquestao)
                .WithMany(p => p.ModeloAnamneseSgquestaoOs)
                .HasForeignKey(d => d.ModeloAnamneseSgquestaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ModeloAna__Model__02084FDA");
        }        
    }
}
