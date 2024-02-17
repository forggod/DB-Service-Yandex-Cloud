using Newtonsoft.Json.Linq;

namespace YaDBTest
{
    public class ServiceAccount
    {
        public string id { get; set; }
        public string service_account_id { get; set; }
        public string created_at { get; set; }
        public string key_algorithm { get; set; }
        public string public_key { get; set; }
        public string private_key { get; set; }
    }
}
