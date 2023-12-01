# NOTE: Use docker-compose to run image or run from solution root
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 58216

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /
COPY Shynest.Identity.csproj Shynest.Identity.csproj
RUN dotnet restore Shynest.Identity.csproj
COPY . .
WORKDIR /
RUN dotnet build Shynest.Identity.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Shynest.Identity.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Shynest.Identity.dll"]