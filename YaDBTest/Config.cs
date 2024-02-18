namespace YaDBTest
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
