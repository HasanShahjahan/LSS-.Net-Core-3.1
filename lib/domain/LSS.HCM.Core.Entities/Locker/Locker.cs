using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LSS.HCM.Core.Entities.Locker
{
    public class Locker : ObjectBase 
    {
        [JsonPropertyName("compartments")]
        public List<Compartment> Compartments { get; set; }
    }
}
