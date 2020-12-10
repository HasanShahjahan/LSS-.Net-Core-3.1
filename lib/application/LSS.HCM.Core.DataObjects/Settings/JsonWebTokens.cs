using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Settings
{
    public class JsonWebTokens
    {
        public bool IsEnabled { get; set; }
        public string Secret { get; set; }
    }
}
