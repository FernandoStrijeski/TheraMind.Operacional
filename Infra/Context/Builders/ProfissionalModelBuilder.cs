using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class ProfissionalModelBuilder : IEntityTypeConfiguration<Profissional>
    {
        public void Configure(EntityTypeBuilder<Profissional> builder)
        {
            builder.ToTable("Profissional");

            builder.Property(e => e.ProfissionalId)
                .HasColumnName("ProfissionalID")
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.AreaAtuacao).HasMaxLength(30);

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Celular).HasMaxLength(14);

            builder.Property(e => e.Cnpj)
                .HasMaxLength(14)
                .HasColumnName("CNPJ");

            builder.Property(e => e.Coffito)
                .HasMaxLength(20)
                .HasColumnName("COFFITO");

            builder.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("CPF");

            builder.Property(e => e.Crefito)
                .HasMaxLength(20)
                .HasColumnName("CREFITO");

            builder.Property(e => e.Crfa)
                .HasMaxLength(20)
                .HasColumnName("CRFA");

            builder.Property(e => e.Crm)
                .HasMaxLength(20)
                .HasColumnName("CRM");

            builder.Property(e => e.Crn)
                .HasMaxLength(20)
                .HasColumnName("CRN");

            builder.Property(e => e.Crp)
                .HasMaxLength(20)
                .HasColumnName("CRP");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Email).HasMaxLength(255);

            builder.Property(e => e.NomeCompleto).HasMaxLength(255);

            builder.Property(e => e.Sexo).HasMaxLength(10);

            builder.Property(e => e.TipoPessoa).HasMaxLength(2);

            builder.Property(e => e.TipoProfissional).HasMaxLength(50);
        }        
    }
}
