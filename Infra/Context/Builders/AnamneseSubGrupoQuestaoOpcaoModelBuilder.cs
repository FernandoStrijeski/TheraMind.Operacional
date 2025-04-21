using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class AnamneseSubGrupoQuestaoOpcaoModelBuilder : IEntityTypeConfiguration<AnamneseSubGrupoQuestaoOpcao>
    {
        public void Configure(EntityTypeBuilder<AnamneseSubGrupoQuestaoOpcao> builder)
        {
            builder.ToTable("AnamneseSubGrupoQuestaoOpcao");

            builder.Property(e => e.AnamneseSubGrupoQuestaoOpcaoId).HasColumnName("AnamneseSubGrupoQuestaoOpcaoID");

            builder.Property(e => e.AnamneseGrupoId).HasColumnName("AnamneseGrupoID");

            builder.Property(e => e.AnamneseSubGrupoId).HasColumnName("AnamneseSubGrupoID");

            builder.Property(e => e.AnamneseSubGrupoQuestaoId).HasColumnName("AnamneseSubGrupoQuestaoID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.ProfissionalId).HasColumnName("ProfissionalID");

            builder.Property(e => e.Texto).HasMaxLength(250);

            builder.HasOne(d => d.AnamneseGrupo)
                .WithMany(p => p.AnamneseSubGrupoQuestaoOpcaos)
                .HasForeignKey(d => d.AnamneseGrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Anamn__3E1D39E1");

            builder.HasOne(d => d.AnamneseSubGrupo)
                .WithMany(p => p.AnamneseSubGrupoQuestaoOpcaos)
                .HasForeignKey(d => d.AnamneseSubGrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Anamn__3F115E1A");

            builder.HasOne(d => d.AnamneseSubGrupoQuestao)
                .WithMany(p => p.AnamneseSubGrupoQuestaoOpcaos)
                .HasForeignKey(d => d.AnamneseSubGrupoQuestaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Anamn__40058253");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.AnamneseSubGrupoQuestaoOpcaos)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Empre__3B40CD36");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AnamneseSubGrupoQuestaoOpcaos)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Filia__3C34F16F");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.AnamneseSubGrupoQuestaoOpcaos)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Profi__3D2915A8");
        }        
    }
}
