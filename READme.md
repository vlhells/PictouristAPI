## ru:
	Это — backend для мини социальной сети типа Instagram для обмена фотографиями.
	В настоящее время проект всё ещё разрабатывается.
	Готовый функционал можно протестировать по пути localhost:port/swagger
	Подробнее - в папке /Docs.

## en: 
	This is a backend for mini social web like Instagram for photo exchange.
	Now still WIP.
	You can test ready functions via localhost:port/swagger
	More information in /Docs folder.
	
## Swagger previews:
![alt text](swaggerPreviews/previewSwagger.PNG "swagger preview")
![alt text](swaggerPreviews/previewSwagger_2.PNG "swagger preview 2")

## Инструкция по развёртке, самый простой вариант:
	1) Скачать на компьютер Visual Studio (2019/2022 - за более ранние версии не ручаюсь).
	2) При установке добавить пакеты .NET (C#) классические приложения, ASP.NET.
	3) Скачать PostgreSQL. Запомнить пароль мастер-пользователя и открытый порт. Либо позже придется копаться в файлах настроек)
	4) По пути ~/PictouristAPI/Pictourist/appsettings.json заменить данные (пароль и порт, в общем случае) на свои из postgres.
	5) Запустить приложение в VS: http либо docker-compose (сначала запустить на ПК сам Docker).
	
	При первых запусках может быть долгая сборка.

## Возможные ошибки:
	1) Проблема с пакетами nuget - нужно их просто перегрузить. В VS - в одной из верхних вкладок (на русском - Средства).  
	Примерно посередине верхнего меню.
	Либо же в меню с файлами проекта (ПКМ по проекту, Nuget).
	
## Deployment instructions, the simplest option:
	1) Download Visual Studio to your computer (2019/2022 - I can't vouch for earlier versions).
	2) Add packages during installation.NET (C#) classic applications, ASP.NET .
	3) Download PostgreSQL. Remember the master user's password and the open port. Or later you will have to dig into the settings files)
	4) Along the way ~/PictouristAPI/Pictourist/appsettings.json replace the data (password and port, in general) with your own from postgres.
	5) Launch the application in VS: http or docker-compose (start Docker on pc at first).
	
	During the first launches, there may be a long build.

## Possible errors:
	1) The problem with nuget packages is that you just need to overload them. In VS - in one of the upper tabs (may be called Instruments or smth like).  
	In the middle part of upper menu.
	Or in the menu with the project files (mouse right click on project, Nuget).

	