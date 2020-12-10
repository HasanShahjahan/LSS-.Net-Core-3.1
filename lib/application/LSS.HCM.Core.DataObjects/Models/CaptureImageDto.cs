using System.Text.Json.Serialization;

namespace LSS.HCM.Core.DataObjects.Models
{
    public class CaptureImageDto 
    {
        [JsonPropertyName("transaction_id")]
        public string TransactionId { get; set; }
    }
}
