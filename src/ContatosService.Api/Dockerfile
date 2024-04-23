FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ContatosService.Api/ContatosService.Api.csproj", "src/ContatosService.Api/"]
RUN dotnet restore "src/ContatosService.Api/ContatosService.Api.csproj"
COPY . .
WORKDIR "/src/src/ContatosService.Api"
RUN dotnet build "ContatosService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContatosService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContatosService.Api.dll"]
