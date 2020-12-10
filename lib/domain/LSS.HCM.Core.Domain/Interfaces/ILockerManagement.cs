using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.Entities.Locker;
using System.Collections.Generic;

namespace LSS.HCM.Core.Domain.Interfaces
{
    public interface ILockerManagement
    {
        Locker OpenCompartment(OpenCompartmentDto openCompartmentDto);
        List<Compartment> CompartmentStatus(); 
        Compartments LockerStatus(string lockerId);
        Capture CaptureImage(string lockerId, string type);
    }
}
