using System;
using Newtonsoft.Json;
using Ydb.Sdk;
using Ydb.Sdk.Services.Table;

namespace YaDBTest
{
    class Programm
    {
        static async Task Main(string[] args)
        {
            // Чтение файла ключа и получение IAM-токена
            string json = File.ReadAllText(Config.keyPath);
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
                endpoint: Config.UserEndpoint,
                database: Config.UserDatabasePath,
                credentials: new Ydb.Sdk.Auth.TokenProvider(iamToken)
            );
            await using var driver = await Driver.CreateInitialized(config);

            using var tableClient = new TableClient(driver, new TableClientConfig());

            var response = await tableClient.SessionExec(async session =>
            {
                var query = @"
                    SELECT id, temp
                    FROM test;
                ";
                return await session.ExecuteDataQuery(
                    query: query,
                    txControl: TxControl.BeginSerializableRW().Commit()
    );
            });
            response.Status.EnsureSuccess();
            var queryResponse = (ExecuteDataQueryResponse)response;
            var resultSet = queryResponse.Result.ResultSets[0];

            foreach (var row in resultSet.Rows)
            {
                Console.WriteLine(row["id"].GetOptionalUint64());
                Console.WriteLine((string?)row["temp"]);
            }
        }
    }
}