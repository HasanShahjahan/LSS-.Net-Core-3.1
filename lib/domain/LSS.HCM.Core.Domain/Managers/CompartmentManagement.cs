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
        public CompartmentConfig GetCompartmentConfig(string compartmentID)
        {
            // Get the configuration
            LockerConfiguration lockerConfiguration = _repository.Get().Find(configuration => configuration.Id == _appSettings.LockerStationId).FirstOrDefault();

            // Find the target compartment
            CompartmentConfig target_compartment = lockerConfiguration.Compartments.Find(compartment => compartment.CompartmentId.Contains(compartmentID));

            return target_compartment;
        }
        public Compartment DoorOpen(string compartmentId)
        {
            // Get the configuration
            LockerConfiguration lockerConfiguration = _repository.Get().Find(configuration => configuration.Id == _appSettings.LockerStationId).FirstOrDefault();

            // Find the target compartment
            CompartmentConfig target_compartment = lockerConfiguration.Compartments.Find(compartment => compartment.CompartmentId.Contains(compartmentId));


            // Commanding
            Dictionary<string, string> DoorOpenResult = _communicationPortControl.SendCommand(CommandType.DoorOpen, GetModuleIDMapping(target_compartment));
            //Dictionary<string, string> ItemDetectionResult = _communicationPortControl.SendCommand(CommandType.ItemDetection, GetModuleIDMapping(target_compartment));
            Compartment compartmentResult = new Compartment(lockerConfiguration.LockerId,
                                                             target_compartment.CompartmentId,
                                                             target_compartment.CompartmentSize,
                                                             Convert.ToBoolean(DoorOpenResult["DoorOpen"]),
                                                             !Convert.ToBoolean(DoorOpenResult["DoorOpen"]),
                                                             false,
                                                             Convert.ToBoolean(DoorOpenResult["DoorOpen"])?"ON": "OFF");
            return compartmentResult;
        }
        public List<Compartment> GetCompartmentStatus()
        {
            // Get the configuration
            LockerConfiguration lockerConfiguration = _repository.Get().Find(configuration => configuration.Id == _appSettings.LockerStationId).FirstOrDefault();

            // Find locker controller board module id and object detection module id list
            var lcbModuleList = new List<string> { };
            var odbModuleList = new List<string> { };
            List<CompartmentConfig> compartments = lockerConfiguration.Compartments;
            foreach (CompartmentConfig compatment in compartments)
            {
                // Get locker controller module id
                if (!lcbModuleList.Contains(compatment.CompartmentCode.Lcbmod))
                {
                    lcbModuleList.Add(compatment.CompartmentCode.Lcbmod);
                }
                // Get object detection module id
                if (!odbModuleList.Contains(compatment.CompartmentCode.Odbmod))
                {
                    odbModuleList.Add(compatment.CompartmentCode.Odbmod);
                }
            }

            // Get all module update
            List<Compartment> compartmentList = new List<Compartment>();
            var opendoorStatusAry = new Dictionary<string, Dictionary<string, byte>> { };
            foreach (string moduleNo in lcbModuleList)
            {
                opendoorStatusAry[moduleNo] = GetDoorOpenStatusByModuleID(moduleNo);
            }
            var objectdetectStatusAry = new Dictionary<string, Dictionary<string, byte>> { };
            foreach (string moduleNo in odbModuleList)
            {
                objectdetectStatusAry[moduleNo] = GetOjecdetectionByModuleID(moduleNo);
            }

            foreach (CompartmentConfig compatment in compartments)
            {
                Dictionary<string, byte> opendoorStatus = opendoorStatusAry[compatment.CompartmentCode.Lcbmod]; //[compatment.CompartmentCode.Lcbid];
                Dictionary<string, byte> objectdetectStatus = objectdetectStatusAry[compatment.CompartmentCode.Odbmod]; //[compatment.CompartmentCode.Odbid];
                Compartment compartmentResult = new Compartment(lockerConfiguration.LockerId,
                                                                 compatment.CompartmentId,
                                                                 compatment.CompartmentSize,
                                                                 opendoorStatus[compatment.CompartmentCode.Lcbid] == 0 ? true : false,
                                                                 opendoorStatus[compatment.CompartmentCode.Lcbid] == 0 ? false : true,
                                                                 objectdetectStatus[compatment.CompartmentCode.Odbid] == 0 ? false : true,
                                                                 opendoorStatus[compatment.CompartmentCode.Lcbid] == 0 ? "ON" : "OFF");
                compartmentList.Add(compartmentResult);
            }

            return compartmentList;
        }

        public Dictionary<string, byte> GetDoorOpenStatusByModuleID(string compartmentModuleID)
        {
            List<byte> commandPINCODE = new List<byte>() {
                Convert.ToByte(compartmentModuleID, 16),
                Convert.ToByte("FF", 16)
            };

            // Command to get status string
            Dictionary<string, string> result = _communicationPortControl.SendCommand(CommandType.DoorStatus, commandPINCODE);

            // Convert statius string to byte array
            Dictionary<string, byte> statusArray = GetStatusListFromString(result["statusAry"]);

            return statusArray;
        }
        public Dictionary<string, byte> GetOjecdetectionByModuleID(string compartmentModuleID)
        {
            List<byte> commandPINCODE = new List<byte>() {
                Convert.ToByte(compartmentModuleID, 16),
                Convert.ToByte("FF", 16)
            };

            // Command to get status string
            Dictionary<string, string> result = _communicationPortControl.SendCommand(CommandType.ItemDetection, commandPINCODE);

            // Convert statius string to byte array
            Dictionary<string, byte> detectionArray = GetStatusListFromString(result["detectionAry"]);

            return detectionArray;
        }

        private Dictionary<string, byte> GetStatusListFromString(string strInput)
        {
            int chunkSize = 1;
            Dictionary<string, byte> StatusList = new Dictionary<string, byte>();
            int stringLength = strInput.Length;
            for (int i = 0; stringLength > 0; i += chunkSize)
            {
                string subStringInput = strInput.Substring(i, chunkSize);
                StatusList[i.ToString("X2")] = (byte)Convert.ToInt16(subStringInput);
                stringLength -= 2;
            }
            return StatusList;
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
