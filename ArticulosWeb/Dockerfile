FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ArticulosWeb/ArticulosWeb.csproj", "ArticulosWeb/"]
RUN dotnet restore "ArticulosWeb/ArticulosWeb.csproj"
COPY . .
WORKDIR "/src/ArticulosWeb"
RUN dotnet build "ArticulosWeb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ArticulosWeb.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD dotnet ArticulosWeb.dll