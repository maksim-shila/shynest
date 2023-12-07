# NOTE: Use docker-compose to run image or run from solution root
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7264

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Shynest.Identity.Api/Shynest.Identity.Api.csproj", "Shynest.Identity.Api/"]
RUN dotnet restore "Shynest.Identity.Api/Shynest.Identity.Api.csproj"
COPY . .
WORKDIR "/src/Shynest.Identity.Api/"
RUN dotnet build "Shynest.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shynest.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shynest.Identity.Api.dll"]