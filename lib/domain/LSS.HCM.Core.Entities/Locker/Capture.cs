using System.Text.Json.Serialization;

namespace LSS.HCM.Core.Entities.Locker
{
    public class Capture : ObjectBase
    {
        [JsonPropertyName("locker_id")]
        public string LockerId { get; set; }

        [JsonPropertyName("capture_image")]
        public Image  CaptureImage{ get; set; }



    }
}
