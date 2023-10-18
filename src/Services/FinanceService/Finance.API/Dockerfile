#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/FinanceService/Finance.API/Finance.API.csproj", "src/Services/FinanceService/Finance.API/"]
COPY ["src/BuildingBlocks/HealthChecks/Healthchecks/Healthchecks.csproj", "src/BuildingBlocks/HealthChecks/Healthchecks/"]
COPY ["src/Services/FinanceService/Finance.Data/Finance.Data.csproj", "src/Services/FinanceService/Finance.Data/"]
COPY ["src/Services/FinanceService/Finance.Domain/Finance.Domain.csproj", "src/Services/FinanceService/Finance.Domain/"]
COPY ["src/BuildingBlocks/SharedBase/SharedBase.csproj", "src/BuildingBlocks/SharedBase/"]
COPY ["src/Services/FinanceService/Finance.Services/Finance.Services.csproj", "src/Services/FinanceService/Finance.Services/"]
COPY ["src/BuildingBlocks/Dtos/FinanceDtos/FinanceDtos.csproj", "src/BuildingBlocks/Dtos/FinanceDtos/"]
COPY ["src/BuildingBlocks/WebHosts/WebHosts.csproj", "src/BuildingBlocks/WebHosts/"]
RUN dotnet restore "src/Services/FinanceService/Finance.API/Finance.API.csproj"
COPY . .
WORKDIR "/src/src/Services/FinanceService/Finance.API"
RUN dotnet build "Finance.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Finance.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Finance.API.dll"]