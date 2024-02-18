# DB Service Yandex Cloud 

### Это простое C#-приложение, позволяющее получить IAM-токен сервисного аккаунта от сервиса Yandex Cloud и выполнить простой запрос к YaDb.

## Подготовка ключа
 Для создания сервисного аккаунта Yandex необходимо перейти в свой профиль организации и выбрать вкладку **Сервисные аккаунты**

![Screenshot 2024-02-18 161154](https://github.com/forggod/DB-Service-Yandex-Cloud/assets/91021642/f007442e-1ea3-4ac4-9217-807e4d75028a)

Далее необходимо создать сервисный аккаунт, если его еще нет. При создании нужно указать роль **ydb.admin** для управления базой данных.
Затем нажимаем на созданный аккаунт, переходим в управление аккаунтом.
Далее необходимо нажать на **Создать авторизованный ключ**, выбрать алгоритм шифрования RSA_2048, при жедании добавить описание.

![Screenshot 2024-02-18 162252](https://github.com/forggod/DB-Service-Yandex-Cloud/assets/91021642/690a54d4-b0b6-408f-9067-992529ae9584)
> [!CAUTION]
> **При создании обязательно скачайте ключ в виде файла!!**

Запомните путь к ключу, он пригодится далее.

## Настройка приложения

> [!WARNING]
> Целевая платформа
> * .NET core 7.0
> 
> Приложение использует следующие пакеты NuGet:
>
> * jose-jwt 4.1.0
> * Newtonsoft.Json 13.0.3
> * Yandex.Cloud.SDK 1.2.0
> * Ydb.Sdk 0.2.0

Перед запуском приложения необходимо настроить файл Config.cs.

```
﻿namespace YaDBTest
{
    public static class Config
    {
        public const string keyPath = "D:";
        public const string UserEndpoint = "grpcs://ydb.serverless.yandexcloud.net:2135";
        public const string UserDatabasePath = "/ru-central1/";

        public static bool IsDebugMode = true;
        public static bool IsWriteKey = false;
        public static bool IsWriteResponse = false;
    }
}
```

* Указываем полный путь к ключу.
* Добавляем Endpoint, который находится на странице управления базой данных.
* Добавляем путь к базе данных, который находится в той же ссылке
> [!NOTE]
> ![Screenshot 2024-02-18 163017](https://github.com/forggod/DB-Service-Yandex-Cloud/assets/91021642/037ca2ae-0757-4975-862a-b5777479dd53)
> > Зелёным - UserEndpoint
> > 
> > Голубым - UserDatabasePath

Перед запуском приложения, необходимо создать тестовую базу данных, с которой будут выводится данные в консоль.
> id - Uint64
> 
> temp Utf8 

![image](https://github.com/forggod/DB-Service-Yandex-Cloud/assets/91021642/5bf731e4-d7f5-4b92-b80c-67e37da4fc2c)

Проверьте корректность измененых данных и запускайте приложение.

## Вывод косоли

![image](https://github.com/forggod/DB-Service-Yandex-Cloud/assets/91021642/fe660bf0-7dfd-40ec-8e59-48f9c790a625)
