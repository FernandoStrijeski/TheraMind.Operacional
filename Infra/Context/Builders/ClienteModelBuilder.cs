using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infra.Context.Builders
{
    public class ClienteModelBuilder : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> duilder)
        {
            duilder.ToTable("Cliente");

            duilder.Property(e => e.ClienteId)
                .HasColumnName("ClienteID")
                .HasDefaultValueSql("(newid())");

            duilder.Property(e => e.Bairro).HasMaxLength(255);

            duilder.Property(e => e.Celular).HasMaxLength(14);

            duilder.Property(e => e.CelularResponsavel).HasMaxLength(14);

            duilder.Property(e => e.Cep)
                .HasMaxLength(10)
                .HasColumnName("CEP");

            duilder.Property(e => e.CidadeId).HasColumnName("CidadeID");

            duilder.Property(e => e.Complemento).HasMaxLength(255);

            duilder.Property(e => e.ConvenioId).HasColumnName("ConvenioID");

            duilder.Property(e => e.ConvenioValorRepasse).HasColumnType("decimal(18, 0)");

            duilder.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("CPF");

            duilder.Property(e => e.Cpfresponsavel)
                .HasMaxLength(11)
                .HasColumnName("CPFResponsavel");

            duilder.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            duilder.Property(e => e.DataNascimento).HasColumnType("date");

            duilder.Property(e => e.DataNascimentoResponsavel).HasColumnType("date");

            duilder.Property(e => e.DataNaturalizacao).HasColumnType("date");

            duilder.Property(e => e.Deficiencia).HasDefaultValueSql("((0))");

            duilder.Property(e => e.Email).HasMaxLength(255);

            duilder.Property(e => e.EmailResponsavel).HasMaxLength(255);

            duilder.Property(e => e.EmpresaId).HasColumnName("EmpresaID");

            duilder.Property(e => e.Endereco).HasMaxLength(255);

            duilder.Property(e => e.EscolaridadeId).HasColumnName("EscolaridadeID");

            duilder.Property(e => e.EstadoCivilId)
                .HasMaxLength(1)
                .HasColumnName("EstadoCivilID");

            duilder.Property(e => e.FilialId).HasColumnName("FilialID");

            duilder.Property(e => e.IdentidadeGeneroId).HasColumnName("IdentidadeGeneroID");

            duilder.Property(e => e.MotivoDesativacao).HasMaxLength(255);

            duilder.Property(e => e.NacionalidadeId).HasColumnName("NacionalidadeID");

            duilder.Property(e => e.Naturalizado).HasDefaultValueSql("((0))");

            duilder.Property(e => e.NomeCompleto).HasMaxLength(255);

            duilder.Property(e => e.NomeResponsavel).HasMaxLength(255);

            duilder.Property(e => e.NomeSocial).HasMaxLength(255);

            duilder.Property(e => e.OrientacaoSexualId).HasColumnName("OrientacaoSexualID");

            duilder.Property(e => e.PacoteFechadoDataInicio).HasColumnType("date");

            duilder.Property(e => e.PacoteFechadoDataVencimento).HasColumnType("date");

            duilder.Property(e => e.PacoteFechadoId).HasColumnName("PacoteFechadoID");

            duilder.Property(e => e.PaisId).HasColumnName("PaisID");

            duilder.Property(e => e.ParenteCelular).HasMaxLength(14);

            duilder.Property(e => e.ParenteGrauParentescoId).HasColumnName("ParenteGrauParentescoID");

            duilder.Property(e => e.ParenteNome).HasMaxLength(100);

            duilder.Property(e => e.PlanoMensalDataVencimento).HasColumnType("date");

            duilder.Property(e => e.Profissao).HasMaxLength(100);

            duilder.Property(e => e.Rg)
                .HasMaxLength(20)
                .HasColumnName("RG");

            duilder.Property(e => e.Rgresponsavel)
                .HasMaxLength(20)
                .HasColumnName("RGResponsavel");

            duilder.Property(e => e.Sexo).HasMaxLength(10);

            duilder.Property(e => e.TipoEtniaId).HasColumnName("TipoEtniaID");

            duilder.Property(e => e.TipoLogradouroId)
                .HasMaxLength(5)
                .HasColumnName("TipoLogradouroID");

            duilder.Property(e => e.Uf)
                .HasMaxLength(2)
                .HasColumnName("UF");

            duilder.Property(e => e.ValorPagamento).HasColumnType("decimal(18, 0)");

            duilder.HasOne(d => d.Cidade)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.CidadeId)
                .HasConstraintName("FK__Cliente__CidadeI__0A688BB1");

            duilder.HasOne(d => d.Convenio)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.ConvenioId)
                .HasConstraintName("FK__Cliente__Conveni__0F2D40CE");

            duilder.HasOne(d => d.Empresa)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cliente__Empresa__02C769E9");

            duilder.HasOne(d => d.Escolaridade)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.EscolaridadeId)
                .HasConstraintName("FK__Cliente__Escolar__078C1F06");

            duilder.HasOne(d => d.EstadoCivil)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.EstadoCivilId)
                .HasConstraintName("FK__Cliente__EstadoC__0697FACD");

            duilder.HasOne(d => d.Filial)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.FilialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cliente__FilialI__03BB8E22");

            duilder.HasOne(d => d.IdentidadeGenero)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdentidadeGeneroId)
                .HasConstraintName("FK__Cliente__Identid__04AFB25B");

            duilder.HasOne(d => d.Nacionalidade)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.NacionalidadeId)
                .HasConstraintName("FK__Cliente__Naciona__0D44F85C");

            duilder.HasOne(d => d.OrientacaoSexual)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.OrientacaoSexualId)
                .HasConstraintName("FK__Cliente__Orienta__05A3D694");

            duilder.HasOne(d => d.PacoteFechado)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PacoteFechadoId)
                .HasConstraintName("FK__Cliente__PacoteF__10216507");

            duilder.HasOne(d => d.Pais)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PaisId)
                .HasConstraintName("FK__Cliente__PaisID__0C50D423");

            duilder.HasOne(d => d.ParenteGrauParentesco)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.ParenteGrauParentescoId)
                .HasConstraintName("FK__Cliente__Parente__0E391C95");

            duilder.HasOne(d => d.TipoEtnia)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.TipoEtniaId)
                .HasConstraintName("FK__Cliente__TipoEtn__0880433F");

            duilder.HasOne(d => d.TipoLogradouro)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.TipoLogradouroId)
                .HasConstraintName("FK__Cliente__TipoLog__09746778");

            duilder.HasOne(d => d.UfNavigation)
                .WithMany(p => p.Clientes)
                .HasForeignKey(d => d.Uf)
                .HasConstraintName("FK__Cliente__UF__0B5CAFEA");
        }        
    }
}
