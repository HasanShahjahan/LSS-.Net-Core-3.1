using LSS.HCM.Core.Entities.Locker;
using System;
using System.Collections.Generic;
using System.Text;
using CompartmentConfig = LSS.HCM.Core.DataObjects.Settings.Compartment;

namespace LSS.HCM.Core.Domain.Interfaces
{
    public interface ICompartmentManagement
    {
        CompartmentConfig GetCompartmentConfig(string compartmentID);
        Compartment DoorOpen(string compartmentId);
        List<Compartment> GetCompartmentStatus();
        Dictionary<string, byte> GetDoorOpenStatusByModuleID(string compartmentModuleID);
        Dictionary<string, byte> GetOjecdetectionByModuleID(string compartmentModuleID);

    }
}
