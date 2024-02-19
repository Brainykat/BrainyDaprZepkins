#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG NET_IMAGE=7.0-bullseye-slim
FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE} AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Web/WebStatus/WebStatus.csproj", "src/Web/WebStatus/"]
RUN dotnet restore "./src/Web/WebStatus/./WebStatus.csproj"
COPY . .
WORKDIR "/src/src/Web/WebStatus"
RUN dotnet build "./WebStatus.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebStatus.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebStatus.dll"]