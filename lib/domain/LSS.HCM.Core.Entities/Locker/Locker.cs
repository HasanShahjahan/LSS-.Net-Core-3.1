using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LSS.HCM.Core.Entities.Locker
{
    public class Locker : ObjectBase 
    {
        public Locker() 
        {
            Compartments = new List<Compartment>();
        }
        [JsonPropertyName("compartments")]
        public List<Compartment> Compartments { get; set; }
    }
}
