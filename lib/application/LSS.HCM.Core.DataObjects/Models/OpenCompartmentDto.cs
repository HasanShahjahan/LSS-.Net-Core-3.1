using System.Text.Json.Serialization;

namespace LSS.HCM.Core.DataObjects.Models
{
    public class OpenCompartmentDto
    {
        [JsonPropertyName("transaction_id")] 
        public string TransactionId { get; set; }

        [JsonPropertyName("compartment_ids")] 
        public string[] CompartmentIds { get; set; }
    }
}
