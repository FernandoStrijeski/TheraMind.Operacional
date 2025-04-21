# Implementação de consultas no banco
Para implementar novos módulos de consulta no banco, crie novas entidades que de domínio e modelBuilders na pasta "Builders". 
Para isso, execute o comando

`dotnet ef dbcontext scaffold "<CONNECTION_STRING>" Microsoft.EntityFrameworkCore.SqlServer -o <LOCAL_PARA_ARMAZENAR_MODELO_E_CONTEXTO> -t [<NOME_DA_TABELA_PARA_GERAR_MODELOS>]`

Feito isso, mova os modelos para a pasta de entidades e copie os modelBuilders para um arquivo separado da pasta Builders. 
modifice o ApplicationDbContext.cs de acordo com os comentários. 