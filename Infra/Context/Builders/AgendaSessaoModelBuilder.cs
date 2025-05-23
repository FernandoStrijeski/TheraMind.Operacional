using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class AgendaSessaoModelBuilder : IEntityTypeConfiguration<AgendaSessao>
    {
        public void Configure(EntityTypeBuilder<AgendaSessao> builder)
        {
            builder.ToTable("AgendaSessao");

            builder.Property(e => e.AgendaSessaoId)
                .HasColumnName("AgendaSessaoID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.AgendaProfissionalId).HasColumnName("AgendaProfissionalId");

            builder.Property(e => e.ClienteId).HasColumnName("ClienteID");

            builder.Property(e => e.DataCancelamento).HasColumnType("datetime");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.DataHoraFim).HasColumnType("datetime");

            builder.Property(e => e.DataHoraInicio).HasColumnType("datetime");

            builder.Property(e => e.DiaTodo).HasDefaultValueSql("((0))");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.FormularioSessaoId).HasColumnName("FormularioSessaoID");

            builder.Property(e => e.MantemCobranca).HasDefaultValueSql("((0))");

            builder.Property(e => e.MotivoCancelamento).HasMaxLength(255);

            builder.Property(e => e.PagamentoEfetuado).HasDefaultValueSql("((0))");

            builder.Property(e => e.ProfissionalId).HasColumnName("ProfissionalID");

            builder.Property(e => e.RecorrenciaDataTermino).HasColumnType("datetime");

            builder.Property(e => e.SalaId)
                .HasMaxLength(20)
                .HasColumnName("SalaID");

            builder.Property(e => e.ServicoId).HasColumnName("ServicoID");

            builder.HasOne(d => d.Agenda)
                .WithMany(p => p.AgendaSessoes)
                .HasForeignKey(d => d.AgendaProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Agend__2C538F61");

            builder.HasOne(d => d.Cliente)
                .WithMany(p => p.AgendaSessoes)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK__AgendaSes__Clien__2F2FFC0C");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.AgendaSessoes)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Empre__297722B6");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AgendaSessoes)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Filia__2A6B46EF");

            builder.HasOne(d => d.FormularioSessao)
                .WithMany(p => p.AgendaSessoes)
                .HasForeignKey(d => d.FormularioSessaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Formu__2E3BD7D3");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.AgendaSessoes)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Profi__2B5F6B28");

            builder.HasOne(d => d.Sala)
                .WithMany(p => p.AgendaSessoes)
                .HasForeignKey(d => d.SalaId)
                .HasConstraintName("FK__AgendaSes__SalaI__30242045");

            builder.HasOne(d => d.Servico)
                .WithMany(p => p.AgendaSessoes)
                .HasForeignKey(d => d.ServicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AgendaSes__Servi__2D47B39A");
        }        
    }
}
