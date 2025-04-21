using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class ProfissionalAcessoModelBuilder : IEntityTypeConfiguration<ProfissionalAcesso>
    {
        public void Configure(EntityTypeBuilder<ProfissionalAcesso> builder)
        {
            builder.ToTable("ProfissionalAcesso");

            builder.Property(e => e.ProfissionalAcessoId).HasColumnName("ProfissionalAcessoID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.ProfissionalId).HasColumnName("ProfissionalID");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.ProfissionalAcessos)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profissio__Empre__1DB06A4F");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.ProfissionalAcessos)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profissio__Filia__1EA48E88");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.ProfissionalAcessos)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profissio__Profi__1CBC4616");
        }        
    }
}
