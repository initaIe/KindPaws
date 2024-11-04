FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/KindPaws.API/KindPaws.API.csproj", "src/KindPaws.API/"]
COPY ["src/KindPaws.Application/KindPaws.Application.csproj", "src/KindPaws.Application/"]
COPY ["src/KindPaws.Domain/KindPaws.Domain.csproj", "src/KindPaws.Domain/"]
COPY ["src/KindPaws.Infrastructure/KindPaws.Infrastructure.csproj", "src/KindPaws.Infrastructure/"]
RUN dotnet restore "src/KindPaws.API/KindPaws.API.csproj"
COPY . .
WORKDIR "/src/src/KindPaws.API"
RUN dotnet build "KindPaws.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "KindPaws.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KindPaws.API.dll"]
