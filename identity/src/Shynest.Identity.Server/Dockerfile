FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7264

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Shynest.Identity.Server/Shynest.Identity.Server.csproj", "Shynest.Identity.Server/"]
RUN dotnet restore "Shynest.Identity.Server/Shynest.Identity.Server.csproj"
COPY . .
WORKDIR "/src/Shynest.Identity.Server/"
RUN dotnet build "Shynest.Identity.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shynest.Identity.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shynest.Identity.Server.dll"]