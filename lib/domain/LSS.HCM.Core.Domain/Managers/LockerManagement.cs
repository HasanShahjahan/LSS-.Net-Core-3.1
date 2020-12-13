using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Interfaces;
using LSS.HCM.Core.Entities.Locker;
using LSS.HCM.Core.Infrastructure.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Compartment = LSS.HCM.Core.Entities.Locker.Compartment;
using CompartmentConfig = LSS.HCM.Core.DataObjects.Settings.Compartment;

namespace LSS.HCM.Core.Domain.Managers
{
    public class LockerManagement : ILockerManagement
    {
        private readonly IRepository<LockerConfiguration> _repository;
        private readonly AppSettings _appSettings;
        private readonly ICompartmentManagement _compartmentManagement;
        private readonly ICommunicationPortControl _communicationPortControl;
        public LockerManagement(IRepository<LockerConfiguration> repository, AppSettings appSettings, ICommunicationPortControl communicationPortControl, ICompartmentManagement compartmentManagement) 
        {
            _repository = repository;
            _appSettings = appSettings;
            _compartmentManagement = compartmentManagement;
            _communicationPortControl = communicationPortControl;
        }
        public Locker OpenCompartment(OpenCompartmentDto openCompartmentDto)
        {/*
            var lockerConfiguration = _repository.Get().Find(configuration => configuration.Id == _appSettings.LockerStationId).FirstOrDefault();
            // Find  object detection module id list by input list of compartment
            var odbModuleList = new List<string> { };
            List<CompartmentConfig> compartments = lockerConfiguration.Compartments;
            foreach (CompartmentConfig compatment in compartments)
            {
                if(openCompartmentDto.CompartmentIds.Contains(compatment.CompartmentId))
                {
                    // Get object detection module id
                    if (!odbModuleList.Contains(compatment.CompartmentCode.Odbmod))
                    {
                        odbModuleList.Add(compatment.CompartmentCode.Odbmod);
                    }
                }
            }
            // Update object detection status of selected modules
            var objectdetectStatusAry = new Dictionary<string, Dictionary<string, byte>> { };
            foreach (string moduleNo in odbModuleList)
            {
                objectdetectStatusAry[moduleNo] = _compartmentManagement.GetOjecdetectionByModuleID(moduleNo);
            }
            */
            // Open door and update status each compartment
            var result = new Locker();
            foreach (var compartmentId in openCompartmentDto.CompartmentIds)
            {
                Compartment targetCompartment = _compartmentManagement.DoorOpen(compartmentId);

                // Update objectdetection status
                //CompartmentConfig targetCompartmentConfig = _compartmentManagement.GetCompartmentConfig(compartmentId);
                //Dictionary<string, byte> objectdetectStatus = objectdetectStatusAry[targetCompartmentConfig.CompartmentCode.Odbmod];
                //targetCompartment.ObjectDetected = objectdetectStatus[targetCompartmentConfig.CompartmentCode.Odbid] == 0? true: false;

                result.Compartments.Add(targetCompartment);
            }
            result.TransactionId = openCompartmentDto.TransactionId;
            return result;
        }
        public List<Compartment> CompartmentStatus()
        {
            List<Compartment> Compartments = _compartmentManagement.GetCompartmentStatus();

            return Compartments;
        }

        public Compartments LockerStatus(string lockerId)
        {
            var lockerConfiguration = _repository.Get().Find(configuration => configuration.Id == _appSettings.LockerStationId).FirstOrDefault();
            List<string> list = new List<string>();
            foreach (var compartment in lockerConfiguration.Compartments) list.Add(compartment.CompartmentId);
            return new Compartments { LockerId = lockerId, CompartmentIds = list.ToArray() };
        }

        public Capture CaptureImage(string lockerId, string type)
        {
            throw new NotImplementedException();
        }
    }
}
