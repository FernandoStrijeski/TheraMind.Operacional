using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infra.Core.Extensoes
{
    public static class ModelBuilderExtensoes
    {
        public static ModelBuilder UpperCaseModelBuilder(this ModelBuilder builder)
        {
            foreach (var entidade in builder.Model.GetEntityTypes())
            {
                var nomeTabela = entidade.GetTableName();
                var schema = entidade.GetSchema();

                foreach (var propriedade in entidade.GetProperties())
                {
                    var nomeColuna = propriedade.GetColumnName(StoreObjectIdentifier.Table(nomeTabela, schema));
                    propriedade.SetColumnName(nomeColuna.ToUpper());
                }
            }

            return builder;
        }
    }
}
