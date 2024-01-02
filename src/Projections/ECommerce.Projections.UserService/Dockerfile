#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Projections/ECommerce.Projections.UserService/ECommerce.Projections.UserService.csproj", "src/Projections/ECommerce.Projections.UserService/"]
COPY ["src/Common/E-CommerceApp.Common/E-CommerceApp.Common.csproj", "src/Common/E-CommerceApp.Common/"]
COPY ["src/Api/Core/E-CommerceApp.Api.Domain/E-CommerceApp.Api.Domain.csproj", "src/Api/Core/E-CommerceApp.Api.Domain/"]
RUN dotnet restore "./src/Projections/ECommerce.Projections.UserService/./ECommerce.Projections.UserService.csproj"
COPY . .
WORKDIR "/src/src/Projections/ECommerce.Projections.UserService"
RUN dotnet build "./ECommerce.Projections.UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ECommerce.Projections.UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Projections.UserService.dll"]