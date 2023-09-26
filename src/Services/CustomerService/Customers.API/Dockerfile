#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/CustomerService/Customers.API/Customers.API.csproj", "src/Services/CustomerService/Customers.API/"]
COPY ["src/BuildingBlocks/AuthorizeAuthenticate/AuthorizeAuthenticate.csproj", "src/BuildingBlocks/AuthorizeAuthenticate/"]
COPY ["src/BuildingBlocks/HealthChecks/Healthchecks/Healthchecks.csproj", "src/BuildingBlocks/HealthChecks/Healthchecks/"]
COPY ["src/Services/CustomerService/Customers.Data/Customers.Data.csproj", "src/Services/CustomerService/Customers.Data/"]
COPY ["src/Services/CustomerService/Customers.Domain/Customers.Domain.csproj", "src/Services/CustomerService/Customers.Domain/"]
COPY ["src/BuildingBlocks/SharedBase/SharedBase.csproj", "src/BuildingBlocks/SharedBase/"]
COPY ["src/Services/CustomerService/Customers.Services/Customers.Services.csproj", "src/Services/CustomerService/Customers.Services/"]
COPY ["src/BuildingBlocks/Dtos/CustomerDtos/CustomerDtos.csproj", "src/BuildingBlocks/Dtos/CustomerDtos/"]
COPY ["src/BuildingBlocks/WebHosts/WebHosts.csproj", "src/BuildingBlocks/WebHosts/"]
RUN dotnet restore "src/Services/CustomerService/Customers.API/Customers.API.csproj"
COPY . .
WORKDIR "/src/src/Services/CustomerService/Customers.API"
RUN dotnet build "Customers.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customers.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customers.API.dll"]