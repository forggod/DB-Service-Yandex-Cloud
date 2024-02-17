using Newtonsoft.Json;
using System;
using YaDBTest;
using Ydb.Sdk;
using Ydb.Sdk.Services.Table;

namespace YaDBTest
{
    class Programm
    {
        static async Task Main(string[] args)
        {
            // Чтение файла ключа и получение IAM-токена
            string json = File.ReadAllText("\\\\wsl.localhost\\Ubuntu\\home\\radzone\\key.json");
            YaDBTest.ServiceAccount account = JsonConvert.DeserializeObject<YaDBTest.ServiceAccount>(json);
            if (Config.IsWriteKey)
            {
                Console.WriteLine($"id: {account.id}");
                Console.WriteLine($"service account id: {account.service_account_id}");
                Console.WriteLine($"created at: {account.created_at}");
                Console.WriteLine($"key algorithm: {account.key_algorithm}");
                Console.WriteLine($"public key: {account.public_key}");
                Console.WriteLine($"private key: {account.private_key}");
            }
            var _ = new IAMToken();
            string? iamToken = await _.createAsync(account.private_key, account.id, account.service_account_id);

            // Обращение к YaDB
            var config = new DriverConfig(
                endpoint: "grpcs://ydb.serverless.yandexcloud.net:2135",
                database: "/ru-central1/b1g7j764i7vamqpln0vb/etnnu7nm5s0f2cu35q95",
                credentials: new Ydb.Sdk.Auth.TokenProvider(iamToken)
            );
            using var driver = new Driver(config: config);
            await driver.Initialize();
            using var tableClient = new TableClient(driver, new TableClientConfig());

        }
    }
}