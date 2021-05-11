using System.Text.Json.Serialization;

namespace LSS.HCM.Core.Entities.Locker
{
    public class Compartment
    {
        public Compartment()
        {
            LockerId = string.Empty;
            CompartmentId = string.Empty;
            CompartmentDoorOpen = false;
            CompartmentSize = string.Empty;
            CompartmentDoorAvailable = false;
            ObjectDetected = false;
            StatusLed = string.Empty;
        }
        public Compartment(string lockerId, string compartmentId, string compartmentSize, bool compartmentDoorOpen, bool compartmentDoorAvailable, bool objectDetected, string statusLed)
        {
            LockerId = lockerId;
            CompartmentId = compartmentId;
            CompartmentSize = compartmentSize;
            CompartmentDoorOpen = compartmentDoorOpen;
            CompartmentDoorAvailable = compartmentDoorAvailable;
            ObjectDetected = objectDetected;
            StatusLed = statusLed;

        }
        [JsonPropertyName("locker_id")]
        public string LockerId { get; set; }

        [JsonPropertyName("compartment_id")]
        public string CompartmentId { get; set; }

        [JsonPropertyName("compartment_size")]
        public string CompartmentSize { get; set; }

        [JsonPropertyName("compartment_door_open")]
        public bool CompartmentDoorOpen { get; set; }

        [JsonPropertyName("compartment_available")]
        public bool CompartmentDoorAvailable { get; set; }

        [JsonPropertyName("object_detected")]
        public bool ObjectDetected { get; set; }

        [JsonPropertyName("status_led")]
        public string StatusLed { get; set; }
    }
}
