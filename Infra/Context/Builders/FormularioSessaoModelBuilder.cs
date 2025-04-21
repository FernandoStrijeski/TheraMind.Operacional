using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class FormularioSessaoModelBuilder : IEntityTypeConfiguration<FormularioSessao>
    {
        public void Configure(EntityTypeBuilder<FormularioSessao> builder)
        {
            builder.ToTable("FormularioSessao");

            builder.Property(e => e.FormularioSessaoId).HasColumnName("FormularioSessaoID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.Nome).HasMaxLength(255);

            builder.Property(e => e.ServicoId).HasColumnName("ServicoID");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.FormularioSessaos)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Formulari__Empre__69FBBC1F");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.FormularioSessaos)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Formulari__Filia__6AEFE058");

            builder.HasOne(d => d.Servico)
                .WithMany(p => p.FormularioSessaos)
                .HasForeignKey(d => d.ServicoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Formulari__Servi__6BE40491");
        }        
    }
}
