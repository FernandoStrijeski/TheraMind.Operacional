using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class AgendaProfissionalModelBuilder : IEntityTypeConfiguration<AgendaProfissional>
    {
        public void Configure(EntityTypeBuilder<AgendaProfissional> builder)
        {
            builder.ToTable("AgendaProfissional");

            builder.HasKey(e => e.AgendaId)
                .HasName("PK__AgendaPr__B9D4363458871882");

            builder.Property(e => e.AgendaId).HasColumnName("AgendaID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DiasOcultados).HasMaxLength(60);

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.ExibeComparecimento)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.ExibeFeriadosNacionais)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.ExibePagamento)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.ExibeSessoesAusentesCanc)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.ProfissionalId).HasColumnName("ProfissionalID");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.AgendaProfissionals)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaPro__Empre__2334397B");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AgendaProfissionals)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaPro__Filia__24285DB4");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.AgendaProfissionals)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaPro__Profi__251C81ED");
        }
    }
}
