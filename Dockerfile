# Фаза сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем остальные файлы и публикуем приложение
COPY . ./
RUN dotnet publish -c Release -o out

# Фаза выполнения
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Задаем порт для приложения
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Запускаем приложение
ENTRYPOINT ["dotnet", "Podsosus2.dll"]