FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 3007/tcp
EXPOSE 3008/tcp
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ContatosService.Api/ContatosService.Api.csproj", "ContatosService.Api/"]
COPY ["src/ContatosService.Domain/ContatosService.Domain.csproj", "ContatosService.Domain/"]
COPY ["src/ContatosService.Infra/ContatosService.Infra.csproj", "ContatosService.Infra/"]

RUN dotnet restore "ContatosService.Api/ContatosService.Api.csproj"
RUN dotnet restore "ContatosService.Domain/ContatosService.Domain.csproj"
RUN dotnet restore "ContatosService.Infra/ContatosService.Infra.csproj"
COPY . .
RUN dotnet build "src/ContatosService.Api.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "src/ContatosService.Api.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContatosService.Api.dll"]