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

            builder.Property(e => e.AgendaId).HasColumnName("AgendaID");

            builder.Property(e => e.AgendaSessaoId).HasColumnName("AgendaSessaoID");

            builder.Property(e => e.Ativo).HasDefaultValueSql("((1))");

            builder.Property(e => e.CampoNome).HasMaxLength(100);

            builder.Property(e => e.ClienteId).HasColumnName("ClienteID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.FormularioSessaoId).HasColumnName("FormularioSessaoID");

            builder.Property(e => e.ProfissionalId).HasColumnName("ProfissionalID");

            builder.Property(e => e.ServicoId).HasColumnName("ServicoID");

            builder.HasOne(d => d.Agenda)
                .WithMany(p => p.AgendaSessaoItems)
                .HasForeignKey(d => d.AgendaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Agend__3B0BC30C");

            builder.HasOne(d => d.AgendaSessao)
                .WithMany(p => p.AgendaSessaoItems)
                .HasForeignKey(d => d.AgendaSessaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Agend__3EDC53F0");

            builder.HasOne(d => d.Cliente)
                .WithMany(p => p.AgendaSessaoItems)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__AgendaSes__Clien__3DE82FB7");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.AgendaSessaoItems)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Empre__382F5661");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AgendaSessaoItems)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Filia__39237A9A");

            builder.HasOne(d => d.FormularioSessao)
                .WithMany(p => p.AgendaSessaoItems)
                .HasForeignKey(d => d.FormularioSessaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Formu__3CF40B7E");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.AgendaSessaoItems)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Profi__3A179ED3");

            builder.HasOne(d => d.Servico)
                .WithMany(p => p.AgendaSessaoItems)
                .HasForeignKey(d => d.ServicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Servi__3BFFE745");
        }        
    }
}
