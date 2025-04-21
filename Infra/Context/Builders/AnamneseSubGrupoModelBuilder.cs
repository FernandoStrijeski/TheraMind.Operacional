using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class AnamneseSubGrupoModelBuilder : IEntityTypeConfiguration<AnamneseSubGrupo>
    {
        public void Configure(EntityTypeBuilder<AnamneseSubGrupo> builder)
        {
            builder.ToTable("AnamneseSubGrupo");

            builder.Property(e => e.AnamneseSubGrupoId).HasColumnName("AnamneseSubGrupoID");

            builder.Property(e => e.AnamneseGrupoId).HasColumnName("AnamneseGrupoID");

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
                .WithMany(p => p.AnamneseSubGrupos)
                .HasForeignKey(d => d.AnamneseGrupoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Anamn__2DE6D218");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.AnamneseSubGrupos)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Empre__2B0A656D");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AnamneseSubGrupos)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Filia__2BFE89A6");

            builder.HasOne(d => d.Profissional)
                .WithMany(p => p.AnamneseSubGrupos)
                .HasForeignKey(d => d.ProfissionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseS__Profi__2CF2ADDF");
        }        
    }
}
