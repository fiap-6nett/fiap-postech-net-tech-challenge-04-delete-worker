# Imagem base do ambiente de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8090

# Imagem para build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o arquivo da solução
COPY Contato.Delete.Worker.sln .

# Copia os arquivos dos projetos
COPY Contato.Delete.Worker.Service/Contato.Delete.Worker.Service.csproj Contato.Delete.Worker.Service/
COPY Contato.Delete.Worker.Application/Contato.Delete.Worker.Application.csproj Contato.Delete.Worker.Application/
COPY Contato.Delete.Worker.Domain/Contato.Delete.Worker.Domain.csproj Contato.Delete.Worker.Domain/
COPY Contato.Delete.Worker.Infra/Contato.Delete.Worker.Infra.csproj Contato.Delete.Worker.Infra/

# Restaura os pacotes NuGet
RUN dotnet restore Contato.Delete.Worker.sln

# Copia o restante dos arquivos
COPY . .

# Compila o projeto
WORKDIR /src/Contato.Delete.Worker.Service
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publica a aplicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final com o runtime e a aplicação publicada
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Contato.Delete.Worker.Service.dll"]