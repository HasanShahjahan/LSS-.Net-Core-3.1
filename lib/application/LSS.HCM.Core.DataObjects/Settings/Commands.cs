using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Settings
{
    public class Commands
    {
        public CommandProtocol OpenDoor {get;set;}
        public CommandProtocol DoorStatus { get; set; }
        public CommandProtocol DetectItem { get; set; }
        public CommandProtocol LedStatus { get; set; }
    }
}
