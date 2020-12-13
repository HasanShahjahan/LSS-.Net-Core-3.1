using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using LSS.HCM.Core.DataObjects.Settings;
using Compartment = LSS.HCM.Core.Entities.Locker.Compartment;
using CompartmentConfig = LSS.HCM.Core.DataObjects.Settings.Compartment;
using LSS.HCM.Core.Infrastructure.Repository;
using MongoDB.Driver;

namespace LSS.HCM.Core.Domain.Managers
{
    public class CompartmentManagement : ICompartmentManagement
    {
        private readonly ICommunicationPortControl _communicationPortControl;
        private readonly AppSettings _appSettings;
        private readonly IRepository<LockerConfiguration> _repository;
        public CompartmentManagement(ICommunicationPortControl communicationPortControl, AppSettings appSettings, IRepository<LockerConfiguration> repository)
        {
            _communicationPortControl = communicationPortControl;
            _appSettings = appSettings;
            _repository = repository;
        }
        public Compartment DoorOpen(string compartmentId)
        {

            Console.WriteLine(compartmentId); //, _appSettings.Locker.Compartments.ForEach());

            LockerConfiguration lockerConfiguration = _repository.Get().Find(configuration => configuration.Id == _appSettings.LockerStationId).FirstOrDefault();

            // Find the target compartment
            CompartmentConfig target_compartment = lockerConfiguration.Compartments.Find(compartment => compartment.CompartmentId.Contains(compartmentId));

            
            // Commanding
            Dictionary<string, string> DoorOpenResult = _communicationPortControl.SendCommand(CommandType.DoorOpen, GetModuleIDMapping(target_compartment));
            //Dictionary<string, string> ItemDetectionResult = _communicationPortControl.SendCommand(CommandType.ItemDetection, GetModuleIDMapping(target_compartment));
            Compartment compartmentResult = new Compartment( lockerConfiguration.LockerId,
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
                Convert.ToByte(target_compartment.CompartmentCode.Lcbmod, 16),
                Convert.ToByte(target_compartment.CompartmentCode.Lcbid, 16)
            };
            return compartmentPINCODE;
        }
    }
}
