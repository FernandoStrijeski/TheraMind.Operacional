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

            builder.Property(e => e.AgendaProfissionalId).HasColumnName("AgendaProfissionalId");

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
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.AgendaProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Agend__37C5420D");

            builder.HasOne(d => d.AgendaSessao)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.AgendaSessaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Agend__3B95D2F1");

            builder.HasOne(d => d.Cliente)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__AgendaSes__Clien__3AA1AEB8");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Empre__34E8D562");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Filia__35DCF99B");

            builder.HasOne(d => d.FormularioSessao)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.FormularioSessaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Formu__39AD8A7F");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Profi__36D11DD4");

            builder.HasOne(d => d.Servico)
                .WithMany(p => p.AgendaSessaoItens)
                .HasForeignKey(d => d.ServicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Servi__38B96646");
        }        
    }
}
