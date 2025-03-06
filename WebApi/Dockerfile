FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем проекты
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Восстанавливаем зависимости
RUN dotnet restore "WebApi/WebApi.csproj"

# Копируем остальной код
COPY . .

# Строим проект
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Применяем миграции
FROM build AS migrations
RUN dotnet ef database update --no-build --configuration Release --project /src/Infrastructure/Infrastructure.csproj --startup-project /src/WebApi/WebApi.csproj --context Infrastructure.Data.ApplicationDbContext

# Публикуем проект
FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Легкий образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Копируем из предыдущих этапов
FROM base AS final
COPY --from=publish /app/publish .

# Запускаем API
ENTRYPOINT ["dotnet", "WebApi.dll"]
