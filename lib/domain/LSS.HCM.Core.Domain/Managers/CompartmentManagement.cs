using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using LSS.HCM.Core.DataObjects.Settings;
using Compartment = LSS.HCM.Core.Entities.Locker.Compartment;
using CompartmentConfig = LSS.HCM.Core.DataObjects.Settings.Compartment;

namespace LSS.HCM.Core.Domain.Managers
{
    public class CompartmentManagement : ICompartmentManagement
    {
        private readonly ICommunicationPortControl _communicationPortControl;
        private readonly AppSettings _appSettings;
        public CompartmentManagement(ICommunicationPortControl communicationPortControl, AppSettings appSettings)
        {
            _communicationPortControl = communicationPortControl;
            _appSettings = appSettings;
        }
        public Compartment DoorOpen(string compartmentId)
        {
            // Find the target compartment
            CompartmentConfig target_compartment = _appSettings.Locker.Compartments.Find(compartment => compartment.CompartmentId.Contains(compartmentId));
            // Commanding
            Dictionary<string, string> DoorOpenResult = _communicationPortControl.SendCommand(CommandType.DoorOpen, GetModuleIDMapping(target_compartment));
            //Dictionary<string, string> ItemDetectionResult = _communicationPortControl.SendCommand(CommandType.ItemDetection, GetModuleIDMapping(target_compartment));
            Compartment compartmentResult = new Compartment( _appSettings.Locker.LockerId,
                                                             target_compartment.CompartmentId,
                                                             target_compartment.CompartmentSize,
                                                             Convert.ToBoolean(DoorOpenResult["DoorOpen"]),
                                                             Convert.ToBoolean(DoorOpenResult["DoorAvailable"]),
                                                             false,//ItemDetectionResult["detect"],
                                                             "OFF");
            return compartmentResult;
        }

        private List<byte> GetModuleIDMapping(CompartmentConfig target_compartment)
        {
            List<byte> compartmentPINCODE = new List<byte>() {
                Convert.ToByte(target_compartment.CompartmentCode.Lcbmod),
                Convert.ToByte(target_compartment.CompartmentCode.Lcbid)
            };
            return compartmentPINCODE;
        }
    }
}
