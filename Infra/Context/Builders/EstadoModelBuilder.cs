using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class EstadoModelBuilder : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estado");
            builder.HasKey(e => e.Uf)
                    .HasName("PK__Estado__32150FAED627618A");


            builder.Property(e => e.Uf)
                .HasMaxLength(2)
                .HasColumnName("UF");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Descricao).HasMaxLength(100);

            builder.Property(e => e.PaisId).HasColumnName("PaisID");

            builder.HasOne(d => d.Pais)
                .WithMany(p => p.Estados)
                .HasForeignKey(d => d.PaisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estado__PaisID__412EB0B6");
        }        
    }
}
