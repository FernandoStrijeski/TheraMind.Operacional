using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class FormularioSessaoCampoModelBuilder : IEntityTypeConfiguration<FormularioSessaoCampo>
    {
        public void Configure(EntityTypeBuilder<FormularioSessaoCampo> builder)
        {
            builder.ToTable("FormularioSessaoCampo");

            builder.Property(e => e.FormularioSessaoCampoId).HasColumnName("FormularioSessaoCampoID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.FormularioSessaoId).HasColumnName("FormularioSessaoID");

            builder.Property(e => e.NomeCampo).HasMaxLength(255);

            builder.Property(e => e.ServicoId).HasColumnName("ServicoID");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.FormularioSessaoCampos)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Formulari__Empre__70A8B9AE");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.FormularioSessaoCampos)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Formulari__Filia__719CDDE7");

            builder.HasOne(d => d.FormularioSessao)
                .WithMany(p => p.FormularioSessaoCampos)
                .HasForeignKey(d => d.FormularioSessaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Formulari__Formu__73852659");

            builder.HasOne(d => d.Servico)
                .WithMany(p => p.FormularioSessaoCampos)
                .HasForeignKey(d => d.ServicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Formulari__Servi__72910220");
        }        
    }
}
