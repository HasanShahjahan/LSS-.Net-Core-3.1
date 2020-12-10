using System.Text.Json.Serialization;

namespace LSS.HCM.Core.Entities.Locker
{
    public class ObjectBase
    {
        [JsonPropertyName("transaction_id")]
        public string TransactionId { get; set; }
    }
}
