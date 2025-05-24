using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class AgendaSessaoItemModelBuilder : IEntityTypeConfiguration<AgendaSessaoItem>
    {
        public void Configure(EntityTypeBuilder<AgendaSessaoItem> builder)
        {            
            builder.ToTable("AgendaSessaoItem");

            builder.Property(e => e.AgendaSessaoItemId).HasColumnName("AgendaSessaoItemID");

            builder.Property(e => e.AgendaProfissionalId).HasColumnName("AgendaProfissionalID");

            builder.Property(e => e.AgendaSessaoId).HasColumnName("AgendaSessaoID");

            builder.Property(e => e.Ativo).HasDefaultValueSql("((1))");

            builder.Property(e => e.ClienteId).HasColumnName("ClienteID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.FormularioSessaoId).HasColumnName("FormularioSessaoID");

            builder.Property(e => e.FormularioSessaoCampoId).HasColumnName("FormularioSessaoCampoID");

            builder.Property(e => e.ProfissionalId).HasColumnName("ProfissionalID");

            builder.Property(e => e.ServicoId).HasColumnName("ServicoID");

            builder.HasOne(d => d.Agenda)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.AgendaProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Agend__5649C92D");

            builder.HasOne(d => d.AgendaSessao)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.AgendaSessaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Agend__5B0E7E4A");

            builder.HasOne(d => d.Cliente)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__AgendaSes__Clien__5A1A5A11");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Empre__536D5C82");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Filia__546180BB");

            builder.HasOne(d => d.FormularioSessao)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.FormularioSessaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Formu__5832119F");

            builder.HasOne(d => d.FormularioSessaoCampo)
                    .WithMany(p => p.AgendaSessaoItems)
                    .HasForeignKey(d => d.FormularioSessaoCampoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AgendaSes__Formu__592635D8");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Profi__5555A4F4");

            builder.HasOne(d => d.Servico)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.ServicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Servi__573DED66");
        }        
    }
}
