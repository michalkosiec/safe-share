FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SafeShare.Api/SafeShare.Api.csproj", "SafeShare.Api/"]
COPY ["SafeShare.Application/SafeShare.Application.csproj", "SafeShare.Application/"]
COPY ["SafeShare.Domain/SafeShare.Domain.csproj", "SafeShare.Domain/"]
COPY ["SafeShare.Infrastructure/SafeShare.Infrastructure.csproj", "SafeShare.Infrastructure/"]
RUN dotnet restore "SafeShare.Api/SafeShare.Api.csproj"
COPY . .
WORKDIR "/src/SafeShare.Api"
RUN dotnet build "./SafeShare.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SafeShare.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SafeShare.Api.dll"]
