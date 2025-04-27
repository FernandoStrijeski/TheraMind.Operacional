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
                .ValueGeneratedOnAdd()
                .HasColumnName("AnamneseSubGrupoQuestaoID");

            builder.Property(e => e.ClienteId).HasColumnName("ClienteID");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.AnamneseGrupo)
                .WithMany(p => p.AnamneseRespostaClientes)
                .HasForeignKey(d => d.AnamneseGrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Anamn__1A69E950");

            builder.HasOne(d => d.AnamneseSubGrupo)
                .WithMany(p => p.AnamneseRespostaClientes)
                .HasForeignKey(d => d.AnamneseSubGrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Anamn__1B5E0D89");

            builder.HasOne(d => d.AnamneseSubGrupoQuestao)
                .WithMany()
                .HasForeignKey(d => d.AnamneseSubGrupoQuestaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Anamn__1C5231C2");

            builder.HasOne(d => d.Cliente)
                .WithMany()
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Clien__1D4655FB");

            builder.HasOne(d => d.Empresa)
            .WithMany(p => p.AnamneseRespostaClientes)
            .HasForeignKey(d => d.EmpresaId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__AnamneseR__Empre__1E3A7A34");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AnamneseRespostaClientes)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Filia__1F2E9E6D");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.AnamneseRespostaClientes)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseR__Profi__2022C2A6");
        }
    }        
}
