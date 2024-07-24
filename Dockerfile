FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ContatosService.Api/ContatosService.Api.csproj", "src/ContatosService.Api/"]
COPY ["src/ContatosService.Domain/ContatosService.Domain.csproj", "src/ContatosService.Domain/"]
COPY ["src/ContatosService.Infra/ContatosService.Infra.csproj", "src/ContatosService.Infra/"]
RUN dotnet restore "src/ContatosService.Api/ContatosService.Api.csproj"
COPY . .
WORKDIR "/src/src/ContatosService.Api"
RUN dotnet build "ContatosService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ContatosService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContatosService.Api.dll"]