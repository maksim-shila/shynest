# NOTE: Use docker-compose to run image or run from solution root
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BuildYourHead.Api/BuildYourHead.Api.csproj", "BuildYourHead.Api/"]
COPY ["BuildYourHead.Persistence/BuildYourHead.Persistence.csproj", "BuildYourHead.Persistence/"]
RUN dotnet restore "BuildYourHead.Api/BuildYourHead.Api.csproj"
COPY . .
WORKDIR "/src/BuildYourHead.Api"
RUN dotnet build "BuildYourHead.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BuildYourHead.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuildYourHead.Api.dll"]