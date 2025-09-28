FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble AS base
WORKDIR /app
EXPOSE 5222
EXPOSE 7273

USER root
RUN apt-get update && apt-get install -y --no-install-recommends \
    ca-certificates curl openssl && \
    rm -rf /var/lib/apt/lists/*

COPY ./WebApp/K8S.Test.Root.CA.cer /usr/local/share/ca-certificates/K8S.Test.Root.CA.crt
RUN openssl x509 -inform DER -in /usr/local/share/ca-certificates/K8S.Test.Root.CA.crt \
    -out /usr/local/share/ca-certificates/K8S.Test.Root.CA.pem && \
    mv /usr/local/share/ca-certificates/K8S.Test.Root.CA.pem /usr/local/share/ca-certificates/K8S.Test.Root.CA.crt && \
    update-ca-certificates

FROM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApp/WebApp.csproj", "WebApp/"]
RUN dotnet restore "./WebApp/WebApp.csproj"
COPY . .
WORKDIR "/src/WebApp"
COPY WebApp/webapplication.dev.cer webapplication.dev.cer
COPY WebApp/webapplication.dev.pfx webapplication.dev.pfx
RUN dotnet build "./WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY WebApp/webapplication.dev.pfx ./webapplication.dev.pfx
COPY WebApp/webapplication.dev.cer ./webapplication.dev.cer
ENTRYPOINT ["dotnet", "WebApp.dll"]
