using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class AnamneseSubGrupoQuestaoModelBuilder : IEntityTypeConfiguration<AnamneseSubGrupoQuestao>
    {
        public void Configure(EntityTypeBuilder<AnamneseSubGrupoQuestao> builder)
        {
            builder.ToTable("AnamneseSubGrupoQuestao");

            builder.Property(e => e.AnamneseSubGrupoQuestaoId).HasColumnName("AnamneseSubGrupoQuestaoID");

            builder.Property(e => e.AnamneseGrupoId).HasColumnName("AnamneseGrupoID");

            builder.Property(e => e.AnamneseSubGrupoId).HasColumnName("AnamneseSubGrupoID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.ProfissionalId).HasColumnName("ProfissionalID");

            builder.Property(e => e.Titulo).HasMaxLength(250);

            builder.HasOne(d => d.AnamneseGrupo)
                .WithMany(p => p.AnamneseSubGrupoQuestoes)
                .HasForeignKey(d => d.AnamneseGrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Anamn__3587F3E0");

            builder.HasOne(d => d.AnamneseSubGrupo)
                .WithMany(p => p.AnamneseSubGrupoQuestoes)
                .HasForeignKey(d => d.AnamneseSubGrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Anamn__367C1819");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.AnamneseSubGrupoQuestoes)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Empre__32AB8735");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AnamneseSubGrupoQuestoes)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Filia__339FAB6E");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.AnamneseSubGrupoQuestoes)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Profi__3493CFA7");
        }        
    }
}
