using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class UsuarioModelBuilder : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.Property(e => e.UsuarioId)
                .HasColumnName("UsuarioID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Email).HasMaxLength(255);

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.PerfilAcesso).HasMaxLength(20);

            builder.Property(e => e.SenhaHash).HasMaxLength(255);

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EmpresaId)
                .HasConstraintName("FK__Usuario__Empresa__59904A2C");

            builder.HasOne(d => d.Filial)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FilialId)
                .HasConstraintName("FK__Usuario__FilialI__5A846E65");
        }        
    }
}
