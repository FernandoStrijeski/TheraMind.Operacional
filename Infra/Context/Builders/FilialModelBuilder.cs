using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class FilialModelBuilder : IEntityTypeConfiguration<Filial>
    {
        public void Configure(EntityTypeBuilder<Filial> builder)
        {
            builder.ToTable("Filial");

            builder.Property(e => e.FilialId).HasColumnName("FilialID");

            builder.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.Bairro).HasMaxLength(255);

            builder.Property(e => e.Cep)
                .HasMaxLength(10)
                .HasColumnName("CEP");

            builder.Property(e => e.CidadeId).HasColumnName("CidadeID");

            builder.Property(e => e.Cnpj)
                .HasMaxLength(14)
                .HasColumnName("CNPJ");

            builder.Property(e => e.Complemento).HasMaxLength(255);

            builder.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("CPF");

            builder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            builder.Property(e => e.Endereco).HasMaxLength(255);

            builder.Property(e => e.InscricaoEstadual).HasMaxLength(30);

            builder.Property(e => e.InscricaoMunicipal).HasMaxLength(30);

            builder.Property(e => e.NomeFilial).HasMaxLength(255);

            builder.Property(e => e.Telefone).HasMaxLength(14);

            builder.Property(e => e.TipoLogradouroId)
                .HasMaxLength(5)
                .HasColumnName("TipoLogradouroID");

            builder.HasOne(d => d.Cidade)
                .WithMany(p => p.Filiais)
                .HasForeignKey(d => d.CidadeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Filial__CidadeID__0D7A0286");

            builder.HasOne(d => d.Empresa)
                .WithMany(p => p.Filials)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Filial__EmpresaI__0B91BA14");

            builder.HasOne(d => d.TipoLogradouro)
                .WithMany(p => p.Filials)
                .HasForeignKey(d => d.TipoLogradouroId)
                .HasConstraintName("FK__Filial__TipoLogr__0C85DE4D");
        }        
    }
}
