# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Contato.Delete.Worker.Service/Contato.Delete.Worker.Service.csproj", "Contato.Delete.Worker.Service/"]
COPY ["Contato.Delete.Worker.Application/Contato.Delete.Worker.Application.csproj", "Contato.Delete.Worker.Application/"]
COPY ["Contato.Delete.Worker.Domain/Contato.Delete.Worker.Domain.csproj", "Contato.Delete.Worker.Domain/"]
COPY ["Contato.Delete.Worker.Infra/Contato.Delete.Worker.Infra.csproj", "Contato.Delete.Worker.Infra/"]
RUN dotnet restore "./Contato.Delete.Worker.Service/Contato.Delete.Worker.Service.csproj"
COPY . .
WORKDIR "/src/Contato.Delete.Worker.Service"
RUN dotnet build "./Contato.Delete.Worker.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Contato.Delete.Worker.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contato.Delete.Worker.Service.dll"]