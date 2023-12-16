#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get -y update; apt-get -y install curl
ARG OTEL_VERSION=0.5.0
ADD https://github.com/open-telemetry/opentelemetry-dotnet-instrumentation/releases/download/v${OTEL_VERSION}/otel-dotnet-auto-install.sh otel-dotnet-auto-install.sh
RUN apt-get update && apt-get install -y unzip && \
    OTEL_DOTNET_AUTO_HOME="/otel-dotnet-auto" sh otel-dotnet-auto-install.sh

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ShopProduct.Api/ShopProduct.Api.csproj", "ShopProduct.Api/"]
COPY ["ShopProduct.Models/ShopProduct.Models.csproj", "ShopProduct.Models/"]
RUN dotnet restore "ShopProduct.Api/ShopProduct.Api.csproj"
COPY . .
WORKDIR "/src/ShopProduct.Api"
RUN dotnet build "ShopProduct.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopProduct.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopProduct.Api.dll"]