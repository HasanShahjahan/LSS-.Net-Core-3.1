using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Settings
{
    public class ApiConfiguration
    {
        public string Mode { get; set; }
        public string Port { get; set; }
        public JsonWebTokens JsonWebTokens { get; set; } 
    }
}
