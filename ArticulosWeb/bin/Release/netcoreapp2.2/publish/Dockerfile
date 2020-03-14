DE microsoft / dotnet: 2.2-sdk AS build-env
WORKDIR / app

# Copie csproj y restaure como capas distintas
COPY * .csproj ./
RUN dotNET restaurar

# Copia todo lo demás y construye
COPIA . ./
EJECUTAR dotnet Publique -c Lanzamiento -o fuera

# Crear imagen de tiempo de ejecución
DE microsoft / dotnet: 2.2-aspnetcore-runtime
WORKDIR / app
COPY --from = build-env / app / out.
CMD dotnet AspNetCoreHerokuDocker.dll