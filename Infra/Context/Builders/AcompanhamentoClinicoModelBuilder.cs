using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class AcompanhamentoClinicoModelBuilder : IEntityTypeConfiguration<AcompanhamentoClinico>
    {
        public void Configure(EntityTypeBuilder<AcompanhamentoClinico> builder)
        {
            builder.ToTable("AcompanhamentoClinico");

            builder.Property(e => e.AcompanhamentoClinicoId)
                .HasColumnName("AcompanhamentoClinicoID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.ClienteId).HasColumnName("ClienteID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.ProfissionalId).HasColumnName("ProfissionalID");

            builder.HasOne(d => d.Cliente)
                .WithMany(p => p.AcompanhamentoClinicos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__Acompanha__Clien__467D75B8");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.AcompanhamentoClinicos)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Acompanha__Empre__43A1090D");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AcompanhamentoClinicos)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Acompanha__Filia__44952D46");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.AcompanhamentoClinicos)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Acompanha__Profi__4589517F");
        }        
    }
}
