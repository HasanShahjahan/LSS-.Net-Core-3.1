using System.Text.Json.Serialization;

namespace LSS.HCM.Core.Entities.Locker
{
    public class Compartments 
    {
        [JsonPropertyName("locker_id")]
        public string LockerId { get; set; }

        [JsonPropertyName("compartment_ids")]
        public string[] CompartmentIds { get; set; }
    }
}
