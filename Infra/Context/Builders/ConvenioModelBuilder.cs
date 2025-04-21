using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class ConvenioModelBuilder : IEntityTypeConfiguration<Convenio>
    {
        public void Configure(EntityTypeBuilder<Convenio> builder)
        {
            builder.ToTable("Convenio");

            builder.Property(e => e.ConvenioId).HasColumnName("ConvenioID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.Nome).HasMaxLength(150);

            builder.Property(e => e.ValorRepasse).HasColumnType("decimal(18, 0)");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.Convenios)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Convenio__Empres__4D5F7D71");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.Convenios)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Convenio__Filial__4E53A1AA");
        }        
    }
}
