using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Settings
{
    public class Microcontroller
    {
        public string CodeName { get; set; } 
        public SerialInterface LockControl { get; set; }
        public SerialInterface ObjectDetection { get; set; }
        public SerialInterface Scanner { get; set; }
        public string CommandHeader { get; set; }
        public Commands Commands{ get; set; }

    }
}
