using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class CidadeModelBuilder : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.ToTable("Cidade");

            builder.HasIndex(e => e.CodigoIbge, "UQ__Cidade__BA925472D9B3B853")
                .IsUnique();

            builder.Property(e => e.CidadeId).HasColumnName("CidadeID");

            builder.Property(e => e.CodigoIbge).HasColumnName("CodigoIBGE");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Nome).HasMaxLength(255);

            builder.Property(e => e.PaisId).HasColumnName("PaisID");

            builder.Property(e => e.Uf)
                .HasMaxLength(2)
                .HasColumnName("UF");

            builder.HasOne(c => c.Estado)
             .WithMany(e => e.Cidades)
             .HasForeignKey(c => c.Uf)
             .HasPrincipalKey(e => e.Uf)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK__Cidade__UF__46E78A0C");
            }        
    }
}
