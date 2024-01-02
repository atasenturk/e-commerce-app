#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Api/WebApi/E-CommerceApp.Api.WebApi/E-CommerceApp.Api.WebApi.csproj", "src/Api/WebApi/E-CommerceApp.Api.WebApi/"]
COPY ["src/Api/Infrastructure/E-CommerceApp.Infrastructure.Persistence/E-CommerceApp.Infrastructure.Persistence.csproj", "src/Api/Infrastructure/E-CommerceApp.Infrastructure.Persistence/"]
COPY ["src/Common/E-CommerceApp.Common/E-CommerceApp.Common.csproj", "src/Common/E-CommerceApp.Common/"]
COPY ["src/Api/Core/E-CommerceApp.Api.Domain/E-CommerceApp.Api.Domain.csproj", "src/Api/Core/E-CommerceApp.Api.Domain/"]
COPY ["src/Api/Core/E-CommerceApp.Api.Application/E-CommerceApp.Api.Application.csproj", "src/Api/Core/E-CommerceApp.Api.Application/"]
RUN dotnet restore "./src/Api/WebApi/E-CommerceApp.Api.WebApi/./E-CommerceApp.Api.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Api/WebApi/E-CommerceApp.Api.WebApi"
RUN dotnet build "./E-CommerceApp.Api.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./E-CommerceApp.Api.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-CommerceApp.Api.WebApi.dll"]