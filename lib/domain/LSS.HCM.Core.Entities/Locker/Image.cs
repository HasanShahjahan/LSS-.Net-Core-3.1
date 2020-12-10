using System.Text.Json.Serialization;

namespace LSS.HCM.Core.Entities.Locker
{
    public class Image
    {
        [JsonPropertyName("image_extension")]
        public string ImageExtension { get; set; }

        [JsonPropertyName("image_data")]
        public byte[] ImageData { get; set; }
    }
}
