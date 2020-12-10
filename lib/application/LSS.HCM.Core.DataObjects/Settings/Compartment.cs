using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Settings
{
    public class Compartment
    {
        public string CompartmentId { get; set; }
        public Code CompartmentCode { get; set; }
        public string CompartmentSize { get; set; }
    }
}
