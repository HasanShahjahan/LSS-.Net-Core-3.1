using System.Text.Json.Serialization;

namespace LSS.HCM.Core.Common.Exceptions
{
    public class ApplicationException 
    {
        [JsonPropertyName("error_code")]
        public string ErrorCode { get; set; } 

        [JsonPropertyName("data")]
        public ErrorData Data { get; set; }
        
    }
}