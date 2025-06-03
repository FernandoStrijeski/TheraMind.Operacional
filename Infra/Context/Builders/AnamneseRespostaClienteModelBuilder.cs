using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class AnamneseRespostaClienteModelBuilder : IEntityTypeConfiguration<AnamneseRespostaCliente>
    {
        public void Configure(EntityTypeBuilder<AnamneseRespostaCliente> builder)
        {
            builder.ToTable("AnamneseRespostaCliente");
            builder.HasKey(e => new { e.EmpresaId, e.FilialId, e.ProfissionalId, e.AnamneseGrupoId, e.AnamneseSubGrupoId, e.AnamneseSubGrupoQuestaoId, e.ClienteId });

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.ProfissionalId).HasColumnName("ProfissionalID");

            builder.Property(e => e.AnamneseGrupoId).HasColumnName("AnamneseGrupoID");

            builder.Property(e => e.AnamneseSubGrupoId).HasColumnName("AnamneseSubGrupoID");

            builder.Property(e => e.AnamneseSubGrupoQuestaoId)
                .HasColumnName("AnamneseSubGrupoQuestaoID");

            builder.Property(e => e.ClienteId).HasColumnName("ClienteID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.AnamneseGrupo)
                .WithMany(p => p.AnamneseRespostaClientes)
                .HasForeignKey(d => d.AnamneseGrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Anamn__61BB7BD9");

            builder.HasOne(d => d.AnamneseSubGrupo)
                .WithMany(p => p.AnamneseRespostaClientes)
                .HasForeignKey(d => d.AnamneseSubGrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Anamn__62AFA012");

            builder.HasOne(d => d.AnamneseSubGrupoQuestao)
                .WithMany()
                .HasForeignKey(d => d.AnamneseSubGrupoQuestaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Anamn__63A3C44B");

            builder.HasOne(d => d.Cliente)
                .WithMany()
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Clien__6497E884");

            builder.HasOne(d => d.Empresa)
            .WithMany(p => p.AnamneseRespostaClientes)
            .HasForeignKey(d => d.EmpresaId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__AnamneseR__Empre__5EDF0F2E");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AnamneseRespostaClientes)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Filia__5FD33367");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.AnamneseRespostaClientes)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Profi__60C757A0");
        }
    }        
}
