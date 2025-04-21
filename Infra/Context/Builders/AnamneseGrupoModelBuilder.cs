using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class AnamneseGrupoModelBuilder : IEntityTypeConfiguration<AnamneseGrupo>
    {
        public void Configure(EntityTypeBuilder<AnamneseGrupo> builder)
        {
            builder.ToTable("AnamneseGrupo");

            builder.Property(e => e.AnamneseGrupoId).HasColumnName("AnamneseGrupoID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.Privado)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.ProfissionalId).HasColumnName("ProfissionalID");

            builder.Property(e => e.Titulo).HasMaxLength(100);

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.AnamneseGrupos)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseG__Empre__25518C17");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.AnamneseGrupos)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AnamneseG__Filia__2645B050");
        }        
    }
}
