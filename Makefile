#  dotnet ef dbcontext scaffold "${LOCAL_URI}" Microsoft.EntityFrameworkCore.SqlServer -o ..\Dominio\Models -t [TABLE-NAMES DEVIDED BY ]

include \.env

.PHONY: prepare
prepare:
	dotnet restore
	
demo-path:
	echo ${DEMO_PATH}

# .PHONY: scaffold-sqlServer
# scaffold:
# 	ifeq ("$(tabela)",); then\
# 		echo "Especifique o nome da tabela que deseja puxar do banco. Exemplo: $(MAKE) tabela=TABELA $@">&2;\
	
# 	else ifeq ($(LOCAL_URI),); then\
# 		echo "Adicione um arquivo .env com a variável LOCAL_URI que representa a URI do banco humanus padrão $@">&2;\
# 		exit 1;
# 	endif

# 	dotnet ef dbcontext scaffold "${LOCAL_URI}" Microsoft.EntityFrameworkCore.SqlServer -o ..\Dominio\Models -t [${TABELA} ]	


.PHONY: lerEnv
lerEnv:
	echo ${LOCAL_URI}