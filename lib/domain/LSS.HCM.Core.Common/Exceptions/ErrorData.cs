using System.Text.Json.Serialization;

namespace LSS.HCM.Core.Common.Exceptions
{
    public class ErrorData
    {
        [JsonPropertyName("valid_format")]
        public string ValidFormat { get; set; }
    }
}
