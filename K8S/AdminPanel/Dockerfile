FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble AS base
WORKDIR /app
EXPOSE 5185
EXPOSE 7294

USER root
RUN apt-get update && apt-get install -y --no-install-recommends \
    ca-certificates curl openssl && \
    rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AdminPanel/AdminPanel.csproj", "AdminPanel/"]
RUN dotnet restore "./AdminPanel/AdminPanel.csproj"
COPY . .
WORKDIR "/src/AdminPanel"
COPY AdminPanel/admin.dev.cer admin.dev.cer
COPY AdminPanel/admin.dev.pfx admin.dev.pfx
RUN dotnet build "./AdminPanel.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AdminPanel.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminPanel.dll"]
