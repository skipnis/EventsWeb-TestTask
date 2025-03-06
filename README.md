**Инструкция по запуску**

1. Клонирование репозитория

Склонируйте репозиторий:

```git clone https://github.com/skipnis/EventsWeb-TestTask.git```

2. Замените ApiKey в appsettings.json и docker-compose.yml для отправки email после обновлении события

``"EmailServiceSettings": {
    "ApiKey": "",``

а также 

``- EMAIL_API_KEY=``

3. Перейдите в директорию проекта

```cd EventsWeb-TestTask```

4. Выполните команду

```docker-compose up --build```

4. Работа с WebApi

После успешного запуска контейнера приложение будет запущено на:
http://localhost:8080
